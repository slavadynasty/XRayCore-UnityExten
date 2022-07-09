using System;
using System.Runtime.InteropServices;
using UnityEngine;
using static XRay.Core.Wrapper;

namespace XRay.Core
{
    public class xr_object
    {
        public IntPtr pointer { get; private set; }
        public xr_mesh[] meshes;
        public xr_bone[] bones;
        public xr_bone root_bone;

        public static xr_object Load(string path)
        {
            if (get_xr_object(path, out IntPtr ptr))
            {
                if (ptr != IntPtr.Zero)
                {
                    return new xr_object(ptr);
                }
                return null;
            }

            return null;
        }

        public static bool Load(string path, out xr_object _object)
        {
            _object = null;
            if(get_xr_object(path, out IntPtr ptr))
            {
                if (ptr != IntPtr.Zero)
                {
                    _object = new xr_object(ptr);
                    return true;
                }
                return false;
            }

            return false;
        }

        public xr_object(string path)
        {
            pointer = IntPtr.Zero;
            meshes = new xr_mesh[0];
            bones = new xr_bone[0];
            root_bone = null;
            if (get_xr_object(path, out IntPtr ptr)) 
            {
                pointer = ptr;
                InternalParse();
            }
        }

        public xr_object(IntPtr ptr)
        {
            pointer = ptr;
            meshes = new xr_mesh[0];
            bones = new xr_bone[0];
            root_bone = null;
            InternalParse();
        }

        private void InternalParse()
        {
            if (pointer != IntPtr.Zero)
            {
                Debug.Log("xr_object -> 0x"+pointer.ToString("X"));
                Array.Resize(ref meshes, xr_object_meshes_count(pointer));

                for (int i = 0; i < meshes.Length; i++)
                {
                    IntPtr mesh_ptr = xr_object_get_mesh(pointer, i);
                    Debug.Log("xr_object -> 0x" + pointer.ToString("X")+" -> xr_mesh 0x"+mesh_ptr.ToString("X"));
                    meshes[i] = new xr_mesh();
                    meshes[i].name = xr_mesh_name(mesh_ptr).GetString();
                    meshes[i].bbox = xr_mesh_bbox(mesh_ptr);

                    meshes[i].points = new fvector3[xr_mesh_points_count(mesh_ptr)];
                    for (int j = 0; j < meshes[i].points.Length; j++)
                    {
                        meshes[i].points[j] = xr_mesh_get_point(mesh_ptr, j);
                    }

                    meshes[i].faces = new lw_face[xr_mesh_faces_count(mesh_ptr)];
                    for (int j = 0; j < meshes[i].faces.Length; j++)
                    {
                        meshes[i].faces[j] = xr_mesh_get_face(mesh_ptr, j);
                    }

                    meshes[i].vmrefs = new lw_vmref[xr_mesh_vmrefs_count(mesh_ptr)];
                    for (int j = 0; j < meshes[i].vmrefs.Length; j++)
                    {
                        IntPtr vmref_ptr = xr_mesh_get_vmref(mesh_ptr, j);
                        meshes[i].vmrefs[j] = (lw_vmref)Marshal.PtrToStructure(vmref_ptr, typeof(lw_vmref));
                    }

                    meshes[i].surfmaps = new xr_surfmap[xr_mesh_surfmaps_count(mesh_ptr)];
                    for (int j = 0; j < meshes[i].surfmaps.Length; j++)
                    {
                        IntPtr surfmap_ptr = xr_mesh_get_surfmap(mesh_ptr, j);
                        meshes[i].surfmaps[j].surface = new xr_surface();
                        xr_mesh_get_surfmap_surface(surfmap_ptr, out IntPtr name_ptr, out IntPtr gamemtl_ptr, out IntPtr texture_ptr, out IntPtr vmap_ptr);
                        meshes[i].surfmaps[j].surface.m_name = name_ptr.GetString();
                        meshes[i].surfmaps[j].surface.m_gamemtl = gamemtl_ptr.GetString();
                        meshes[i].surfmaps[j].surface.m_texture = texture_ptr.GetString();
                        meshes[i].surfmaps[j].surface.m_vmap = vmap_ptr.GetString();

                        meshes[i].surfmaps[j].faces = new uint[xr_mesh_get_surfmap_faces_count(surfmap_ptr)];

                        for(int k = 0; k < meshes[i].surfmaps[j].faces.Length; k++)
                        {
                            meshes[i].surfmaps[j].faces[k] = xr_mesh_get_surfmap_get_face(surfmap_ptr, k);
                        }
                    }

                    meshes[i].uvs = new xr_uv_vmap[xr_mesh_vmaps_count(mesh_ptr)];
                    for (int j = 0; j < meshes[i].uvs.Length; j++)
                    {
                        meshes[i].uvs[j].m_uvs = new fvector2[xr_mesh_get_vmap_uvs_count(mesh_ptr, j)];
                        for (int k = 0; k < meshes[i].uvs[j].m_uvs.Length; k++)
                        {
                            meshes[i].uvs[j].m_uvs[k] = xr_mesh_get_vmap_uvs(mesh_ptr, j, k);
                        }
                    }

                    meshes[i].weights.m_weights = new float[xr_mesh_get_vmap_weight_count(mesh_ptr, 0)];
                    for (int j = 0; j < meshes[i].weights.m_weights.Length; j++)
                    {
                        meshes[i].weights.m_weights[j] = xr_mesh_get_vmap_weight(mesh_ptr, 0, j);
                    }
                }

                /* BONES */
                Array.Resize(ref bones, xr_object_bones_count(pointer));
                for (int i = 0; i < bones.Length; i++)
                {
                    bones[i] = new xr_bone(xr_object_get_bone(pointer, i));
                    if (bones[i].is_root_bone && root_bone == null)
                    {
                        root_bone = bones[i];
                    }
                    bones[i].Setup(this);
                }
            }
        }

        public xr_bone FindBone(string name)
        {
            for (int i = 0; i < bones.Length; i++)
            {
                if (bones[i] != null && bones[i].name == name)
                {
                    return bones[i];
                }
            }

            return null;
        }

        ~xr_object()
        {
            free_xr_object(pointer);
        }
    }
}

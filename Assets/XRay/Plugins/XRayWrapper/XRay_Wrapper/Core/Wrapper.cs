using System;
using System.Security;
using System.Runtime.InteropServices;

namespace XRay.Core
{
    public static class Wrapper
    {

        public static string GetString(this IntPtr ptr)
        {
            try
            {
                return Marshal.PtrToStringAnsi(ptr);
            }
            catch
            {
                return string.Empty;
            }
        }

        /* Dummy */
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern IntPtr get_version();

        /* XR_OBJECT */
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern bool get_xr_object(string path, out IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern bool free_xr_object(IntPtr ptr);

        /* XR_MESH */
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern int xr_object_meshes_count(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern IntPtr xr_object_get_mesh(IntPtr ptr, int index);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern IntPtr xr_mesh_name(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern bbox xr_mesh_bbox(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern int xr_mesh_points_count(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern fvector3 xr_mesh_get_point(IntPtr ptr, int index);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern int xr_mesh_faces_count(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern lw_face xr_mesh_get_face(IntPtr ptr, int index);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern int xr_mesh_vmrefs_count(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern IntPtr xr_mesh_get_vmref(IntPtr ptr, int index);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern int xr_mesh_surfmaps_count(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern IntPtr xr_mesh_get_surfmap(IntPtr ptr, int index);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern IntPtr xr_mesh_get_surfmap_surface(IntPtr ptr, out IntPtr name_ptr, out IntPtr gamemtl_ptr, out IntPtr texture_ptr, out IntPtr vmap_ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern int xr_mesh_get_surfmap_faces_count(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern uint xr_mesh_get_surfmap_get_face(IntPtr ptr, int index);

        /* XR_BONE */
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern int xr_object_bones_count(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern IntPtr xr_object_get_bone(IntPtr ptr, int index);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern ushort xr_bone_id(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern bool xr_bone_is_root(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern fmatrix xr_bone_bind_xform(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern fmatrix xr_bone_bind_i_xform(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern IntPtr xr_bone_name(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern IntPtr xr_bone_parent_name(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern IntPtr xr_bone_vmap_name(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern fvector3 xr_bone_bind_rotate(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern fvector3 xr_bone_bind_offset(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern IntPtr xr_bone_gamemtl(IntPtr ptr);

        /* XR_OGF */
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern bool get_xr_ogf(string path, out ogf_version version, out ogf_model_type model_type, out IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern bool free_xr_ogf(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern bbox xr_ogf_bbox(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern bsphere xr_ogf_bsphere(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern void xr_ogf_vb(IntPtr ptr, out bool has_points, out bool has_normals, out bool has_texcoords, out bool has_influences, out bool has_colors, out bool has_lightmaps);
    }
}

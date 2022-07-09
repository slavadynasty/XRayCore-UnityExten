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
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern int xr_mesh_vmaps_count(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern int xr_mesh_get_vmap_uvs_count(IntPtr ptr, int index);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern fvector2 xr_mesh_get_vmap_uvs(IntPtr ptr, int index, int uvs_index);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern int xr_mesh_get_vmap_weight_count(IntPtr ptr, int index);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern float xr_mesh_get_vmap_weight(IntPtr ptr, int index, int weight_index);

        /* XR_BONE */
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern int xr_object_bones_count(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern IntPtr xr_object_get_bone(IntPtr ptr, int index);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern int xr_ogf_bones_count(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern IntPtr xr_ogf_get_bone(IntPtr ptr, int index);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern ushort xr_bone_id(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern bool xr_bone_is_root(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern fmatrix xr_bone_bind_xform(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern void xr_bone_calculate_bind(IntPtr ptr, fmatrix parent_xform);
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
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern IntPtr xr_ogf_get_child(IntPtr ptr, int index);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern int xr_ogf_child_count(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern int xr_ogf_vb_size(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern void xr_ogf_vb_types(IntPtr ptr, ref bool has_points, ref bool has_normals, ref bool has_texcoords, ref bool has_influences, ref bool has_colors, ref bool has_lightmaps);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern void xr_ogf_struct(IntPtr ptr, ref bool hierarchical, ref bool skeletal, ref bool animated, ref bool progressive, ref bool versioned);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern IntPtr xr_ogf_texture(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern IntPtr xr_ogf_shader(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern fvector3 xr_ogf_vb_get_p(IntPtr ptr, int index);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern fvector2 xr_ogf_vb_get_tc(IntPtr ptr, int index);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern fvector3 xr_ogf_vb_get_n(IntPtr ptr, int index);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern int xr_ogf_vb_get_w_size(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern int xr_ogf_vb_get_w_element_size(IntPtr ptr, int index);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern UInt32 xr_ogf_vb_w_get_element_bone(IntPtr ptr, int fIndex, int fbIndex);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern float xr_ogf_vb_w_get_element_weight(IntPtr ptr, int fIndex, int fbIndex);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern void xr_ogf_vb_w_get_fbone_weight(IntPtr ptr, int fIndex, 
            ref UInt32 b0, ref UInt32 b1, ref UInt32 b2, ref UInt32 b3, ref float w0, ref float w1, ref float w2, ref float w3);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern int xr_ogf_ib_size(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern ushort xr_ogf_ib_get_indice(IntPtr ptr, int index);
        
        /* XR_SKL */
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern bool get_xr_ogf_omf(string path, out IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern int xr_ogf_motions_size(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern IntPtr xr_ogf_motions_get_skl(IntPtr ptr, int index);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern float xr_ogf_skl_get_fps(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern int xr_skl_bone_motions_size(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern IntPtr xr_skl_get_bone_motion(IntPtr ptr, int index);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern IntPtr xr_bone_motion_name(IntPtr ptr);
        [SuppressUnmanagedCodeSecurity] [DllImport("xrCore")] public static extern void xr_bone_motion_evaluate(IntPtr ptr, float fps, int xframe, ref fvector3 t, ref fvector3 r);
    }
}

using System;
using System.Collections.Generic;
using static XRay.Core.Wrapper;

namespace XRay.Core
{
    [System.Serializable]
    public class xr_bone
    {
        public IntPtr pointer { get; private set; }

        public xr_bone(IntPtr ptr)
        {
            pointer = ptr;
            parent = null;
            children = new List<xr_bone>();
            InternalParse(pointer);
        }

        private void InternalParse(IntPtr ptr)
        {
            if (pointer != IntPtr.Zero)
            {
                id = xr_bone_id(pointer);
                is_root_bone = xr_bone_is_root(pointer);
                name = xr_bone_name(pointer).GetString();
                parent_name = xr_bone_parent_name(pointer).GetString();
                vmap_name = xr_bone_vmap_name(pointer).GetString();
                bind_xform = xr_bone_bind_xform(pointer);
                bind_i_xform = xr_bone_bind_i_xform(pointer);
                bind_offset = xr_bone_bind_offset(pointer);
                bind_rotate = xr_bone_bind_rotate(pointer);
                gamemtl = xr_bone_gamemtl(pointer).GetString();
            }
        }

        public void InternalReparse()
        {
            if (pointer != IntPtr.Zero)
            {
                bind_xform = xr_bone_bind_xform(pointer);
                bind_i_xform = xr_bone_bind_i_xform(pointer);
            }
        }

        public void Setup(xr_object _object)
        {
            xr_bone bone = _object.FindBone(parent_name);

            if(bone != null)
            {
                bone.children.Add(this);
            }
        }
        
        public void Setup(xr_ogf _ogf)
        {
            xr_bone bone = _ogf.FindBone(parent_name);

            if(bone != null)
            {
                bone.children.Add(this);
            }
        }

        public ushort id { get; private set; }
        public bool is_root_bone { get; private set; }
        public bool has_parent { get { return parent != null; } }
        public xr_bone parent { get; private set; }
        public List<xr_bone> children { get; private set; }
        public string name { get; private set; } = string.Empty;
        public string parent_name { get; private set; } = string.Empty;
        public string vmap_name { get; private set; } = string.Empty;
        public fmatrix bind_xform;
        public fmatrix bind_i_xform { get; private set; }
        public fvector3 bind_offset { get; private set; }
        public fvector3 bind_rotate { get; private set; }
        public string gamemtl { get; private set; } = string.Empty;
    }
}

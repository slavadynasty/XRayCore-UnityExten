using System;
using UnityEngine;
using static XRay.Core.Wrapper;

namespace XRay.Core
{
    [System.Serializable]
    public class xr_ogf
    {
        public IntPtr pointer { get; private set; }
        public xr_ogf[] children;
        public xr_vbuf vb;
        public xr_ibuf ib;
        public xr_bone[] bones;
        public xr_bone root_bone;
        public bbox bbox { get; private set; }
        public bsphere bsphere { get; private set; }
        public ogf_version version { get; private set; }
        public ogf_model_type model_type { get; private set; }

        public bool hierarchical, skeletal, animated, progressive, versioned;

        public string texture, shader;

        public static xr_ogf Load(string path)
        {
            if (get_xr_ogf(path, out ogf_version version, out ogf_model_type model_type, out IntPtr ptr))
            {
                if (ptr != IntPtr.Zero)
                {
                    return new xr_ogf(ptr, version, model_type);
                }
                return null;
            }

            return null;
        }

        public static bool Load(string path, out xr_ogf _ogf)
        {
            _ogf = null;
            if (get_xr_ogf(path, out ogf_version version, out ogf_model_type model_type, out IntPtr ptr))
            {
                if (ptr != IntPtr.Zero)
                {
                    _ogf = new xr_ogf(ptr, version, model_type);
                    return true;
                }
                return false;
            }

            return false;
        }

        public xr_ogf(string path)
        {
            pointer = IntPtr.Zero;

            if (get_xr_ogf(path, out ogf_version version, out ogf_model_type model_type, out IntPtr ptr))
            {
                pointer = ptr;
                this.version = version;
                this.model_type = model_type;
                InternalParse();
            }
        }

        public xr_ogf(IntPtr ptr, ogf_version version, ogf_model_type model_type)
        {
            pointer = ptr;
            this.version = version;
            this.model_type = model_type;
            InternalParse();
        }

        private void InternalParse()
        {
            if (pointer != IntPtr.Zero)
            {
                xr_ogf_struct(pointer, ref hierarchical, ref skeletal, ref animated, ref progressive, ref versioned);

                int ogf_count = xr_ogf_child_count(pointer);
                children = new xr_ogf[ogf_count];
                for (int i = 0; i < ogf_count; i++)
                {
                    children[i] = new xr_ogf(xr_ogf_get_child(pointer, i), version, model_type);
                }
                
                /* BONES */
                Array.Resize(ref bones, xr_ogf_bones_count(pointer));
                for (int i = 0; i < bones.Length; i++)
                {
                    bones[i] = new xr_bone(xr_ogf_get_bone(pointer, i));
                    if (bones[i].is_root_bone && root_bone == null)
                    {
                        root_bone = bones[i];
                    }
                    bones[i].Setup(this);
                }
                    
                InternalParseChild();
            }
        }

        private void InternalParseChild()
        {
            for (int i = 0; i < children.Length; i++)
            {
                xr_ogf_struct(children[i].pointer, ref children[i].hierarchical, ref children[i].skeletal, ref children[i].animated, ref children[i].progressive, ref children[i].versioned);
                
                children[i].bbox = xr_ogf_bbox(children[i].pointer);
                children[i].bsphere = xr_ogf_bsphere(children[i].pointer);
                children[i].texture = xr_ogf_texture(children[i].pointer).GetString();
                children[i].shader = xr_ogf_shader(children[i].pointer).GetString();

                children[i].vb = new xr_vbuf(children[i].pointer);
                children[i].ib = new xr_ibuf(children[i].pointer);
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

        public void ReimportBones()
        {
            for (int i = 0; i < bones.Length; i++)
            {
                bones[i].InternalReparse();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using static XRay.Core.Wrapper;

namespace XRay.Core
{
    public class xr_skl
    {
        public IntPtr pointer { get; private set; }
        public float fps { get; private set; }

        public bone_motion[] bone_motions;
        
        public xr_skl(IntPtr ptr)
        {
            pointer = ptr;
            InternalParse();
        }

        private void InternalParse()
        {
            fps = xr_ogf_skl_get_fps(pointer);
            bone_motions = new bone_motion[xr_skl_bone_motions_size(pointer)];
            for (int i = 0; i < bone_motions.Length; i++)
            {
                bone_motions[i] = new bone_motion();
                
                IntPtr bmot = xr_skl_get_bone_motion(pointer, i);
                Debug.Log("0x"+xr_bone_motion_name(pointer).ToString("X"));
                xr_bone_motion_evaluate(bmot, Time.time, i, ref bone_motions[i].t, ref bone_motions[i].r);
                //bone_motions[i].bname = xr_bone_motion_name(pointer).GetString();
            }
        }

        public class bone_motion
        {
            public fvector3 t = new ();
            public fvector3 r = new ();
            public string bname = String.Empty;
        }
    }
}
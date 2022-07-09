using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static XRay.Core.Wrapper;

namespace XRay.Core
{
    [System.Serializable]
    public class xr_vbuf
    {
        private IntPtr pointer;

        public finfluence[] w { get; private set; }
        public fvector3[] p{ get; private set; }
        public fvector3[] n{ get; private set; }
        public fvector2[] tc{ get; private set; }

        public bool has_points;
        public bool has_normals;
        public bool has_texcoords;
        public bool has_influences;
        public bool has_colors;
        public bool has_lightmaps;

        public xr_vbuf(IntPtr ptr)
        {
            pointer = ptr;
            InternalParse();
        }

        private void InternalParse()
        {
            xr_ogf_vb_types(pointer, ref has_points, ref has_normals, ref has_texcoords, ref has_influences, ref has_colors, ref has_lightmaps);

            int num_verts = xr_ogf_vb_size(pointer);
            
            if (has_points)
            {
                p = new fvector3[num_verts];
                    
                for (int i = 0; i < num_verts; i++)
                {
                    p[i] = xr_ogf_vb_get_p(pointer, i);
                }
            }
            
            if (has_texcoords)
            {
                tc = new fvector2[num_verts];
                    
                for (int i = 0; i < num_verts; i++)
                {
                    tc[i] = xr_ogf_vb_get_tc(pointer, i);
                }
            }
            
            if (has_normals)
            {
                n = new fvector3[num_verts];
                    
                for (int i = 0; i < num_verts; i++)
                {
                    n[i] = xr_ogf_vb_get_n(pointer, i);
                }
            }
            
            if(has_influences)
            {
                w = new finfluence[num_verts];
                for (int i = 0; i < w.Length; i++)
                {
                    w[i] = new finfluence();

                    xr_ogf_vb_w_get_fbone_weight(pointer, i, 
                        ref w[i].fbw[0].bone, ref w[i].fbw[1].bone, ref w[i].fbw[2].bone, ref w[i].fbw[3].bone,
                        ref w[i].fbw[0].weight, ref w[i].fbw[1].weight, ref w[i].fbw[2].weight, ref w[i].fbw[3].weight);
                }
            }
        }
    }

    [System.Serializable]
    public class finfluence
    {
        public fbone_weight[] fbw = new fbone_weight[4];
    }
}

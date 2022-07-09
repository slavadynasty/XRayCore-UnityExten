using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static XRay.Core.Wrapper;

namespace XRay.Core
{
    [System.Serializable]
    public class xr_ibuf
    {
        private IntPtr pointer;
        
        public ushort[] m_indices;

        public xr_ibuf(IntPtr ptr)
        {
            pointer = ptr;
            InternalParse();
        }

        private void InternalParse()
        {
            m_indices = new ushort[xr_ogf_ib_size(pointer)];
            for (int i = 0; i < m_indices.Length; i++)
            {
                m_indices[i] = xr_ogf_ib_get_indice(pointer, i);
            }
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

namespace XRay.Core
{
    [System.Serializable]
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct fbone_weight
    {
        public UInt32 bone;
        public float weight;
    }
}


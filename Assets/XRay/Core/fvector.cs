using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace XRay.Core
{
    [System.Serializable]
    [StructLayout(LayoutKind.Explicit)]
    public struct fvector2
    {
        [FieldOffset(0)]
        public float x;
        [FieldOffset(4)]
        public float y;

        public Vector2 ToVector2()
        {
            return new Vector2(x, y);
        }
    }

    [System.Serializable]
    [StructLayout(LayoutKind.Explicit)]
    public struct fvector3
    {
        [FieldOffset(0)]
        public float x;
        [FieldOffset(4)]
        public float y;
        [FieldOffset(8)]
        public float z;

        public Vector3 ToVector3()
        {
            return new Vector3(x, y, z);
        }
    }
}

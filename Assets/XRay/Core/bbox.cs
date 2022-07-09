using System.Runtime.InteropServices;
using UnityEngine;

namespace XRay.Core
{
    [StructLayout(LayoutKind.Explicit)]
    public struct bbox
    {
        [FieldOffset(0)]
        public float x1;
        [FieldOffset(4)]
        public float y1;
        [FieldOffset(8)]
        public float z1;

        [FieldOffset(12)]
        public float x2;
        [FieldOffset(16)]
        public float y2;
        [FieldOffset(20)]
        public float z2;

        public Vector3 max() { return new Vector3(x1, y1, z1); }
        public Vector3 min() { return new Vector3(x2, y2, z2); }
    }
}

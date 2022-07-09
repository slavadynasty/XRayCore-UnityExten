using System.Runtime.InteropServices;

namespace XRay.Core
{
    [StructLayout(LayoutKind.Explicit)]
    public struct fvector2
    {
        [FieldOffset(0)]
        public float x;
        [FieldOffset(4)]
        public float y;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct fvector3
    {
        [FieldOffset(0)]
        public float x;
        [FieldOffset(4)]
        public float y;
        [FieldOffset(8)]
        public float z;
    }
}

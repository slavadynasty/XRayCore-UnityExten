using System.Runtime.InteropServices;

namespace XRay.Core
{
    [StructLayout(LayoutKind.Explicit)]
    public struct bsphere
    {
        [FieldOffset(0)]
        public float x;
        [FieldOffset(4)]
        public float y;
        [FieldOffset(8)]
        public float z;
        [FieldOffset(12)]
        public float radius;
    }
}

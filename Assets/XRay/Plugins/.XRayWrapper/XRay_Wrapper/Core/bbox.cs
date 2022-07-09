using System.Runtime.InteropServices;

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
    }
}

using System.Runtime.InteropServices;

namespace XRay.Core
{
    [StructLayout(LayoutKind.Explicit)]
    public struct lw_face
    {
        [FieldOffset(0)]
        public int v0;
        [FieldOffset(4)]
        public int v1;
        [FieldOffset(8)]
        public int v2;

        [FieldOffset(12)]
        public int ref0;
        [FieldOffset(16)]
        public int ref1;
        [FieldOffset(20)]
        public int ref2;
    }
}

using System.Runtime.InteropServices;

namespace XRay.Core
{
    [StructLayout(LayoutKind.Sequential)]
    public struct lw_vmref
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5, ArraySubType = UnmanagedType.Struct)]
        public lw_vmref_entry[] array;
        public int count;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct lw_vmref_entry
    {
        public uint vmap;
        public uint offset;
    }
}

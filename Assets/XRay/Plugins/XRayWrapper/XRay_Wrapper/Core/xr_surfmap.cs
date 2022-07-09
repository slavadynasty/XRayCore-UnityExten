using System.Runtime.InteropServices;

namespace XRay.Core
{
	[StructLayout(LayoutKind.Sequential)]
    public struct xr_surfmap
    {
		public xr_surface surface;
		public uint[] faces;
	}
}

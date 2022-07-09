using System.Runtime.InteropServices;

namespace XRay.Core
{
    [StructLayout(LayoutKind.Sequential)]
    public struct fmatrix
    {
        public float _11, _12, _13, _14;
        public float _21, _22, _23, _24;
        public float _31, _32, _33, _34;
        public float _41, _42, _43, _44;
    }
}

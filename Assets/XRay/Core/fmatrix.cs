using System.Runtime.InteropServices;
using UnityEngine;

namespace XRay.Core
{
    [StructLayout(LayoutKind.Sequential)]
    public struct fmatrix
    {
        public float _11, _12, _13, _14;
        public float _21, _22, _23, _24;
        public float _31, _32, _33, _34;
        public float _41, _42, _43, _44;

        public Matrix4x4 ToMatrix4x4()
        {
            return new Matrix4x4
            {
                m00 = _11, m01 = _12, m02 = _13, m03 = _14,
                m10 = _21, m11 = _22, m12 = _23, m13 = _24,
                m20 = _31, m21 = _32, m22 = _33, m23 = _34,
                m30 = _41, m31 = _42, m32 = _43, m33 = _44,
            };
        }
    }

    public static class FmatrixExtension
    {
        public static fmatrix ToFmatrix(this Matrix4x4 m4)
        {
            return new fmatrix
            {
                _11 = m4.m00, _12 = m4.m01, _13 = m4.m02, _14 = m4.m03,
                _21 = m4.m10, _22 = m4.m11, _23 = m4.m12, _24 = m4.m13,
                _31 = m4.m20, _32 = m4.m21, _33 = m4.m22, _34 = m4.m23,
                _41 = m4.m30, _42 = m4.m31, _43 = m4.m32, _44 = m4.m33,
            };
        }
    }
}

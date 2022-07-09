using System;
using System.IO;
using Mathf = System.Math;

namespace XRay.Math
{
    public class Matrix4x4
    {
        public float _11, _12, _13, _14;
        public float _21, _22, _23, _24;
        public float _31, _32, _33, _34;
        public float _41, _42, _43, _44;

        public Matrix4x4(
            float _11, 
            float _12, 
            float _13, 
            float _14, 
            float _21, 
            float _22, 
            float _23, 
            float _24,
            float _31,
            float _32,
            float _33,
            float _34,
            float _41,
            float _42,
            float _43,
            float _44
            )
        {
            this._11 = _11;
            this._12 = _12;
            this._13 = _13;
            this._14 = _14;

            this._21 = _21;
            this._22 = _22;
            this._23 = _23;
            this._24 = _24;

            this._31 = _31;
            this._32 = _32;
            this._33 = _33;
            this._34 = _34;

            this._41 = _41;
            this._42 = _42;
            this._43 = _43;
            this._44 = _44;
        }

        public Matrix4x4(BinaryReader reader)
        {
            this._11 = reader.ReadSingle();
            this._12 = reader.ReadSingle();
            this._13 = reader.ReadSingle();
            this._14 = reader.ReadSingle();

            this._21 = reader.ReadSingle();
            this._22 = reader.ReadSingle();
            this._23 = reader.ReadSingle();
            this._24 = reader.ReadSingle();

            this._31 = reader.ReadSingle();
            this._32 = reader.ReadSingle();
            this._33 = reader.ReadSingle();
            this._34 = reader.ReadSingle();

            this._41 = reader.ReadSingle();
            this._42 = reader.ReadSingle();
            this._43 = reader.ReadSingle();
            this._44 = reader.ReadSingle();
        }

        public static Matrix4x4 Identity { get; private set; } = new Matrix4x4(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

        public void SetIdentity()
        {
            this._11 = 0;
            this._12 = 0;
            this._13 = 0;
            this._14 = 0;

            this._21 = 0;
            this._22 = 0;
            this._23 = 0;
            this._24 = 0;

            this._31 = 0;
            this._32 = 0;
            this._33 = 0;
            this._34 = 0;

            this._41 = 0;
            this._42 = 0;
            this._43 = 0;
            this._44 = 0;
        }

        public void setI(Vector3 vec)
        {
            _11 = vec.x;
            _12 = vec.y;
            _13 = vec.z;
            _14 = 0;
        }

        public Vector3 getI()
        {
            return new Vector3(_11, _12, _13);
        }

        public void setJ(Vector3 vec)
        {
            _21 = vec.x;
            _22 = vec.y;
            _23 = vec.z;
            _24 = 0;
        }

        public Vector3 getJ()
        {
            return new Vector3(_21, _22, _23);
        }

        public void setK(Vector3 vec)
        {
            _31 = vec.x;
            _32 = vec.y;
            _33 = vec.z;
            _34 = 0;
        }

        public Vector3 getK()
        {
            return new Vector3(_31, _32, _33);
        }

        public void setC(Vector3 vec)
        {
            _41 = vec.x;
            _42 = vec.y;
            _43 = vec.z;
            _44 = 0;
        }

        public Vector3 getC()
        {
            return new Vector3(_41, _42, _43);
        }

        public void set_xyz_i(Vector3 vec) => set_hpb(-vec.y, -vec.x, -vec.z);

        public void set_xyz(Vector3 vec) => set_hpb(vec.y, vec.x, vec.z);

        public void set_euler_xyz(float x, float y, float z)
        {
            x = -x;
            y = -y;
            z = -z;

            float sx = (float)Mathf.Sin(z);
            float cx = (float)Mathf.Cos(z);
            float sy = (float)Mathf.Sin(y);
            float cy = (float)Mathf.Cos(y);
            float sz = (float)Mathf.Sin(x);
            float cz = (float)Mathf.Cos(x);

            _11 = cx * cy;
            _12 = -cy * sx;
            _13 = sy;
            _14 = 0;

            _21 = cx * sz * sy + sx * cz;
            _22 = cx * cz - sx * sy * sz;
            _23 = -cy * sz;
            _24 = 0;

            _31 = sx * sz - sy * cx * cz;
            _32 = sy * sx * cz + cx * sz;
            _33 = cz * cy;
            _34 = 0;

            _41 = 0;
            _42 = 0;
            _43 = 0;
            _44 = 1;
        }

        public void set_euler_xyz(Vector3 vec) => set_euler_xyz(vec.x, vec.y, vec.z);

        public void mul_43(Matrix4x4 a, Matrix4x4 b)
        {
            _11 = a._11 * b._11 + a._21 * b._12 + a._31 * b._13;
            _12 = a._12 * b._11 + a._22 * b._12 + a._32 * b._13;
            _13 = a._13 * b._11 + a._23 * b._12 + a._33 * b._13;
            _14 = 0;

            _21 = a._11 * b._21 + a._21 * b._22 + a._31 * b._23;
            _22 = a._12 * b._21 + a._22 * b._22 + a._32 * b._23;
            _23 = a._13 * b._21 + a._23 * b._22 + a._33 * b._23;
            _24 = 0;

            _31 = a._11 * b._31 + a._21 * b._32 + a._31 * b._33;
            _32 = a._12 * b._31 + a._22 * b._32 + a._32 * b._33;
            _33 = a._13 * b._31 + a._23 * b._32 + a._33 * b._33;
            _34 = 0;

            _41 = a._11 * b._41 + a._21 * b._42 + a._31 * b._43 + a._41;
            _42 = a._12 * b._41 + a._22 * b._42 + a._32 * b._43 + a._42;
            _43 = a._13 * b._41 + a._23 * b._42 + a._33 * b._43 + a._43;
            _44 = 1;
        }
        public void mul_a_43(Matrix4x4 b) => mul_43(this, b);
        public void mul_b_43(Matrix4x4 a) => mul_43(a, this);

        public void set_hpb(float x, float y, float z)
        {
            float sh = (float)Mathf.Sin(x);
            float ch = (float)Mathf.Cos(x);
            float sp = (float)Mathf.Sin(y);
            float cp = (float)Mathf.Cos(y);
            float sb = (float)Mathf.Sin(z);
            float cb = (float)Mathf.Cos(z);

            _11 = ch * cb - sh * sp * sb;
            _12 = -cp * sb;
            _13 = ch * sb * sp + sh * cb;
            _14 = 0;

            _21 = sp * sh * cb + ch * sb;
            _22 = cb * cp;
            _23 = sh * sb - sp * ch * cb;
            _24 = 0;

            _31 = -cp * sh;
            _32 = sp;
            _33 = ch * cp;
            _34 = 0;

            _41 = 0;
            _42 = 0;
            _43 = 0;
            _44 = 1;
        }

        public void invert_43(Matrix4x4 a)
        {
            float cf1 = a._22 * a._33 - a._23 * a._32;
            float cf2 = a._21 * a._33 - a._23 * a._31;
            float cf3 = a._21 * a._32 - a._22 * a._31;

            float det = a._11 * cf1 - a._12 * cf2 + a._13 * cf3;


            _11 = cf1 / det;
            _21 = -cf2 / det;
            _31 = cf3 / det;

            //	_11 = (a._22*a._33 - a._23*a._32)/det;
            _12 = -(a._12 * a._33 - a._13 * a._32) / det;
            _13 = (a._12 * a._23 - a._13 * a._22) / det;
            _14 = 0;

            //	_21 =-(a._21*a._33 - a._23*a._31)/det;
            _22 = (a._11 * a._33 - a._13 * a._31) / det;
            _23 = -(a._11 * a._23 - a._13 * a._21) / det;
            _24 = 0;

            //	_31 = (a._21*a._32 - a._22*a._31)/det;
            _32 = -(a._11 * a._32 - a._12 * a._31) / det;
            _33 = (a._11 * a._22 - a._12 * a._21) / det;
            _34 = 0;

            _41 = -(a._41 * _11 + a._42 * _21 + a._43 * _31);
            _42 = -(a._41 * _12 + a._42 * _22 + a._43 * _32);
            _43 = -(a._41 * _13 + a._42 * _23 + a._43 * _33);
            _44 = 1;
        }

        public void invert_43() => invert_43(this);
    }
}

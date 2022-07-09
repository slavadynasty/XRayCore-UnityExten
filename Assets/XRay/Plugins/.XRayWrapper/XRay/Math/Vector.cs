using System.IO;

namespace XRay.Math
{
    public class Vector2
    {
        public float x, y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2(BinaryReader reader)
        {
            x = reader.ReadSingle();
            y = reader.ReadSingle();
        }

        public void ReadVector2(BinaryReader reader)
        {
            x = reader.ReadSingle();
            y = reader.ReadSingle();
        }

        public override string ToString()
        {
            return string.Format("X:{0} Y:{1}", x, y);
        }
        public static Vector2 Zero { get; private set; } = new Vector2(0, 0);
    }

    public class Vector3
    {
        public float x, y, z;

        public Vector3(float x, float y , float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3(BinaryReader reader)
        {
            x = reader.ReadSingle();
            y = reader.ReadSingle();
            z = reader.ReadSingle();
        }

        public void ReadVector3(BinaryReader reader)
        {
            x = reader.ReadSingle();
            y = reader.ReadSingle();
            z = reader.ReadSingle();
        }

        public override string ToString()
        {
            return string.Format("X:{0} Y:{1} Z:{2}", x, y, z);
        }

        public static Vector3 Zero { get; private set; } = new Vector3(0, 0, 0);
    }
}

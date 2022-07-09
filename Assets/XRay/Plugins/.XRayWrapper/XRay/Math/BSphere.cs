using System.IO;

namespace XRay.Math
{
    /// <summary>
    /// Bounding Sphere
    /// </summary>
    public class BSphere
    {
        public Vector3 point;
        public float radius;

        public BSphere(Vector3 point, float radius)
        {
            this.point = point;
            this.radius = radius;
        }

        public void ReadBSphere(BinaryReader reader)
        {
            point.ReadVector3(reader);
            radius = reader.ReadSingle();
        }

        public static BSphere Zero { get; private set; } = new BSphere(Vector3.Zero, 0);
    }
}

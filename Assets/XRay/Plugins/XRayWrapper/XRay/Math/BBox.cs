using System.IO;

namespace XRay.Math
{
    /// <summary>
    /// Bounding Box
    /// </summary>
    public class BBox
    {
        public Vector3 min;
        public Vector3 max;

        public BBox(Vector3 min, Vector3 max)
        {
            this.min = min;
            this.max = max;
        }

        public void ReadBBox(BinaryReader reader)
        {
            min.ReadVector3(reader);
            max.ReadVector3(reader);
        }

        public static BBox Zero { get; private set; } = new BBox(Vector3.Zero, Vector3.Zero);
    }
}

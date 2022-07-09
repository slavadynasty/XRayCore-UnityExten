using System.IO;

namespace XRay.Core
{
    public class lw_face
    {
        public uint v0, v1, v2;
        public uint ref0, ref1, ref2;

        public void ReadLWFace(BinaryReader reader)
        {
            v0 = reader.ReadUInt32();
            ref0 = reader.ReadUInt32();
            v1 = reader.ReadUInt32();
            ref1 = reader.ReadUInt32();
            v2 = reader.ReadUInt32();
            ref2 = reader.ReadUInt32();
        }
    }
}

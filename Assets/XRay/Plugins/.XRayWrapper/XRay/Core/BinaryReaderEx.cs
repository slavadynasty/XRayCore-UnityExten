using System.IO;

namespace XRay.Core
{
    public static class BinaryReaderEx
    {
        public static void Reset(this BinaryReader reader)
        {
            reader.BaseStream.Position = 0;
        }

        public static float ReadSingle16(this BinaryReader reader, float min, float max)
        {
            return reader.ReadInt16() * ((max - min) / 65535f) + min;
        }

        public static float ReadSingle8(this BinaryReader reader, float min, float max)
        {
            return reader.ReadInt16() * ((max - min) / 255f) + min;
        }

        public static string ReadStringT(this BinaryReader reader, char terminator = '\0')
        {
            string s = string.Empty;
            char c = char.MaxValue;

            while (c != terminator)
            {
                c = reader.ReadChar();
                if (c == char.MinValue)
                {
                    return s;
                }
                s += c;
            }

            return s;
        }
    }
}

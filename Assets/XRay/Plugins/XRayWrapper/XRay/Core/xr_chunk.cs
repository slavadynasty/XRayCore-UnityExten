using System;
using System.IO;

namespace XRay.Core
{
    public class xr_chunk
    {
        public xr_chunk(int id, int size, byte[] data, string name = "unknown")
        {
            this.id = id;
            this.size = size;
            this.data = data;
            this.reader = new BinaryReader(new MemoryStream(data));
            this.name = name;
        }

        ~xr_chunk()
        {
            Array.Clear(data,0, data.Length);
            reader.Close();
        }

        public int id { get; private set; }
        public int size { get; private set; }
        public byte[] data { get; private set; }
        public BinaryReader reader { get; private set; }
        public string name { get; private set; }
    }
}

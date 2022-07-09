using XRay.Core.Enums;
using XRay.Math;

namespace XRay.Core
{
    public class xr_mesh : xr_file
    {
        public string name { get; private set; }
        public BBox bbox { get; private set; } = BBox.Zero;
        public Vector3[] points { get; private set; }
        public lw_face[] faces { get; private set; }
		public xr_mesh(byte[] data) : base(true)//make virtual file
        {
            ProcessMeshChunks(data);
        }

        protected override void OnReady()
        {
            {
                if (GetChunk((int)(emesh_chunk_id.EMESH_CHUNK_MESHNAME), out xr_chunk chunk))
                {
                    name = chunk.reader.ReadStringT();
                    Console.Log("Mesh name " + name);
                }
            }
            {
                if (GetChunk((int)(emesh_chunk_id.EMESH_CHUNK_VERSION), out xr_chunk chunk))
                {
                    Console.Log("Mesh version " + chunk.reader.ReadInt16());
                }
            }
            {
                if (GetChunk((int)(emesh_chunk_id.EMESH_CHUNK_BBOX), out xr_chunk chunk))
                {
                    bbox.ReadBBox(chunk.reader);

                    Console.Log("Mesh BBox min " + bbox.min.ToString());
                    Console.Log("Mesh BBox max " + bbox.max.ToString());
                }
            }

            {
                if (GetChunk((int)(emesh_chunk_id.EMESH_CHUNK_VERTS), out xr_chunk chunk))
                {
                    points = new Vector3[chunk.reader.ReadInt32()];

                    for(int i = 0; i < points.Length; i++)
                    {
                        points[i] = Vector3.Zero;
                        points[i].ReadVector3(chunk.reader);
                    }

                    Console.Log(+points.Length+" points (verts) cached...");
                }
            }

            {
                if (GetChunk((int)(emesh_chunk_id.EMESH_CHUNK_FACES), out xr_chunk chunk))
                {
                    faces = new lw_face[chunk.reader.ReadInt32()];

                    for (int i = 0; i < faces.Length; i++)
                    {
                        faces[i] = new lw_face();
                        faces[i].ReadLWFace(chunk.reader);
                    }

                    Console.Log(+faces.Length + " faces cached...");
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using XRay.Core.Enums;
using XRay.Math;

namespace XRay.Core
{
    public class xr_object : xr_file
    {
        public string owner_name;
        public int creating_time;
        public string modif_name;
        public int modified_time;

        public xr_bone root_bone = null;
        public List<xr_surface> surfaces = new List<xr_surface>();
        public List<xr_mesh> mesh = new List<xr_mesh>();
        public List<xr_bone> bones = new List<xr_bone>();
        public List<xr_skl_motion> motions = new List<xr_skl_motion>();
        public string motion_refs = string.Empty;
        public Vector3 position = Vector3.Zero;
        public Vector3 rotation = Vector3.Zero;

        protected override int OnDumpChunks(string path)
        {
            if (mesh == null)
            {
                return 0;
            }
            int dumped = 0;
            for (int i = 0; i < mesh.Count; i++) 
            {
                for (int j = 0; j < mesh[i].Chunks.Count; j++) 
                {
                    string chunk_name = Enum.GetName(typeof(emesh_chunk_id), mesh[i].Chunks[j].id);

                    if (chunk_name == null || chunk_name.Length == 0)
                    {
                        chunk_name = "UNKNOWNN_" + mesh[i].Chunks[j].id;
                    }

                    string path2 = Path.Combine(path, mesh[i].name+"_"+chunk_name);

                    try
                    {
                        using (FileStream fs = new FileStream(path2, FileMode.Create))
                        {
                            fs.Write(mesh[i].Chunks[j].data, 0, mesh[i].Chunks[j].size);
                            fs.Close();
                        }

                        Console.Log("Chunk " + chunk_name + " of mesh "+mesh[i].name+" dumped  Size " + mesh[i].Chunks[j].data.Length + " bytes");
                        dumped++;
                    }
                    catch
                    {
                        Console.Error("Can't dump chunk " + chunk_name+ "of mesh "+mesh[i].name);
                    }
                }
            }

            Console.Log("Mesh chunks dumping complete! " + dumped + " chunks dumped...");

            return dumped;
        }

        protected override void OnReady()
        {
            {
                if (GetChunk((int)(eobj_chunk_id.EOBJ_CHUNK_TRANSFORM), out xr_chunk chunk))
                {
                    position = new Vector3(chunk.reader);
                    rotation = new Vector3(chunk.reader);
                }
            }

            {
                if (GetChunk((int)(eobj_chunk_id.EOBJ_CHUNK_MESHES), out xr_chunk chunk))
                {
                    mesh.AddRange(ReadObjectMeshes(chunk.data));
                }
            }

            {
                if (GetChunk((int)(eobj_chunk_id.EOBJ_CHUNK_MOTIONS), out xr_chunk chunk))
                {
                    int sz = chunk.reader.ReadInt32();

                    for(int i = 0; i < sz; i++)
                    {
                        motions.Add(new xr_skl_motion(chunk.reader));
                    }
                }
            }

            {
                if (GetChunk((int)(eobj_chunk_id.EOBJ_CHUNK_MOTION_REFS), out xr_chunk chunk))
                {
                    motion_refs = chunk.reader.ReadStringT();
                }
            }

            {
                if (GetChunk((int)(eobj_chunk_id.EOBJ_CHUNK_REVISION), out xr_chunk chunk))
                {
                    owner_name = chunk.reader.ReadStringT();
                    creating_time = chunk.reader.ReadInt32();
                    modif_name = chunk.reader.ReadStringT();
                    modified_time = chunk.reader.ReadInt32();
                }
            }

            {
                if (GetChunk((int)(eobj_chunk_id.EOBJ_CHUNK_BONES_1), out xr_chunk chunk1))
                {
                    bones.AddRange(ReadObjectBones(chunk1.data));
                    SetupBones();
                }
                else if(GetChunk((int)(eobj_chunk_id.EOBJ_CHUNK_BONES_0), out xr_chunk chunk2))
                {
                    int sz = chunk2.reader.ReadInt32();

                    for (int i = 0; i < sz; i++)
                    {
                        bones.Add(new xr_bone(chunk2.reader));
                    }
                    SetupBones();
                }
            }

            {
                if (GetChunk((int)(eobj_chunk_id.EOBJ_CHUNK_SURFACES_2), out xr_chunk chunk))
                {
                    int sz = chunk.reader.ReadInt32();

                    for (int i = 0; i < sz; i++)
                    {
                        surfaces.Add(new xr_surface(
                            chunk.reader.ReadStringT(),
                            chunk.reader.ReadStringT(),
                            chunk.reader.ReadStringT(),
                            chunk.reader.ReadStringT(),
                            chunk.reader.ReadStringT(),
                            chunk.reader.ReadStringT(),
                            chunk.reader.ReadUInt32(),
                            chunk.reader.ReadUInt32()
                            ));
                    }
                }
            }

            {
                if (GetChunk((int)(eobj_chunk_id.EOBJ_CHUNK_SURFACES_1), out xr_chunk chunk))
                {
                    int sz = chunk.reader.ReadInt32();

                    for (int i = 0; i < sz; i++)
                    {
                        surfaces[i] = new xr_surface(
                            chunk.reader.ReadStringT(),
                            chunk.reader.ReadStringT(),
                            chunk.reader.ReadStringT(),
                            "default",
                            chunk.reader.ReadStringT(),
                            chunk.reader.ReadStringT(),
                            chunk.reader.ReadUInt32(),
                            chunk.reader.ReadUInt32()
                            );
                    }
                }
            }

            {
                if (GetChunk((int)(eobj_chunk_id.EOBJ_CHUNK_SURFACES_0), out xr_chunk chunk))
                {
                    int sz = chunk.reader.ReadInt32();

                    for (int i = 0; i < sz; i++)
                    {
                        surfaces[i] = new xr_surface(
                            chunk.reader.ReadStringT(),
                            chunk.reader.ReadStringT(),
                            "default",
                            "default",
                            chunk.reader.ReadStringT(),
                            chunk.reader.ReadStringT(),
                            chunk.reader.ReadUInt32(),
                            chunk.reader.ReadUInt32()
                            );
                    }
                }
            }
        }

        public void SetupBones()
        {
            Console.Log("Setup bones...");
            for(int i = 0; i < bones.Count; i++)
            {
                bones[i].Setup((ushort)i, this);
                if(bones[i].m_parent == null)
                {
                    if(root_bone != null)
                    {
                        Console.Warning("Object contains more than one skeleton");
                    }
                    else
                    {
                        root_bone = bones[i];
                        Console.Log("Bone "+root_bone.m_name+" set as root bone...");
                    }
                }
                else
                {
                    Console.Log("Bone "+bones[i].m_name+" as children of "+bones[i].m_parent.m_name);
                }
            }

            if (root_bone!=null)
            {
                root_bone.CalculateBind(Matrix4x4.Identity);
            }
        }

        public xr_bone FindBone(string name)
        {
            for(int i = 0; i < bones.Count; i++)
            {
                if(bones[i].m_name == name)
                {
                    return bones[i];
                }
            }

            return null;
        }
    }
}

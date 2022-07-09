using System;
using System.Collections.Generic;
using System.IO;
using XRay.Core.Enums;
using XRay.Math;

namespace XRay.Core
{
    public partial class xr_file
    {
        protected void ProcessObjectChunks(byte[] data)
        {
            Console.Log("Reading object chunks...");
            Chunks.Clear();
            using (BinaryReader reader = new BinaryReader(new MemoryStream(data)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    int chunk_id = reader.ReadInt32();
                    int chunk_size = reader.ReadInt32();
                    byte[] chunk_data = reader.ReadBytes(chunk_size);
                    string chunk_name = Enum.GetName(typeof(eobj_chunk_id), chunk_id);

                    if(chunk_name == null || chunk_name.Length == 0)
                    {
                        chunk_name = "UNKNOWNN_"+chunk_id;
                    }

                    xr_chunk chunk = new xr_chunk(chunk_id, chunk_size, chunk_data, chunk_name);
                    Chunks.Add(chunk);
                    SetLoadedChunk(chunk.id);
                    Console.Log("Chunk " + chunk.name + " ("+chunk_id+ ") processed... Size " + chunk_size + " bytes");
                }
            }
        }

        protected void ProcessOGFChunks(byte[] data)
        {
            Console.Log("Reading OGF...");
            Chunks.Clear();
            void LoadHeader(BinaryReader reader)
            {
                ogf_version version = (ogf_version)reader.ReadByte();
                ogf_model_type model_type = (ogf_model_type)reader.ReadByte();
                ushort shader_id = reader.ReadUInt16();

                Console.Log("OGF Header (" + version + ") (" + model_type + ") (" + shader_id + ")");

                if (version == ogf_version.OGF4_VERSION)
                {
                    BBox bbox = BBox.Zero;
                    bbox.ReadBBox(reader);
                    Console.Log("BBox Min: " + bbox.min.ToString());
                    Console.Log("BBox Max: " + bbox.max.ToString());

                    BSphere bsphere = BSphere.Zero;
                    bsphere.ReadBSphere(reader);
                    Console.Log("BSphere Point: " + bsphere.point.ToString() + "  Radius: " + bsphere.radius);
                }

                switch (model_type)
                {
                    case ogf_model_type.MT4_NORMAL:
                        //load_visual(r);
                        break;
                    case ogf_model_type.MT4_HIERRARHY:
                        //load_hierrarhy_visual(r);
                        break;
                    case ogf_model_type.MT4_PROGRESSIVE:
                        // load_progressive(r);
                        // m_flags = EOF_PROGRESSIVE;
                        break;
                    case ogf_model_type.MT4_SKELETON_ANIM:
                        //load_kinematics_animated(r);
                        //m_flags = EOF_DYNAMIC;
                        break;
                    case ogf_model_type.MT4_SKELETON_GEOMDEF_PM:
                        //load_skeletonx_pm(r);
                        //m_flags = EOF_PROGRESSIVE;
                        break;
                    case ogf_model_type.MT4_SKELETON_GEOMDEF_ST:
                        //load_skeletonx_st(r);
                        break;
                    case ogf_model_type.MT4_LOD:
                        //load_lod(r);
                        //m_flags = EOF_MULTIPLE_USAGE;
                        break;
                    case ogf_model_type.MT4_TREE_ST:
                        //load_tree_visual_st(r);
                        // m_flags = EOF_STATIC;
                        break;
                    case ogf_model_type.MT4_PARTICLE_EFFECT:
                        //load_particle_effect(r);
                        break;
                    case ogf_model_type.MT4_PARTICLE_GROUP:
                        //load_particle_group(r);
                        break;
                    case ogf_model_type.MT4_SKELETON_RIGID:
                        //load_kinematics(r);
                        //m_flags = EOF_DYNAMIC;
                        break;
                    case ogf_model_type.MT4_TREE_PM:
                        //load_tree_visual_pm(r);
                        //m_flags = EOF_PROGRESSIVE;
                        break;
                    default:
                        Console.Warning("Unknown model type!");
                        break;
                }
            }

            using (BinaryReader reader = new BinaryReader(new MemoryStream(data)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    LoadHeader(reader);
                }
            }
        }

        protected xr_mesh[] ReadObjectMeshes(byte[] data)
        {
            Console.Log("Reading meshes...");
            List<xr_mesh> array = new List<xr_mesh>();
            using (BinaryReader reader = new BinaryReader(new MemoryStream(data)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    int chunk_id = reader.ReadInt32();
                    int chunk_size = reader.ReadInt32();
                    byte[] chunk_data = reader.ReadBytes(chunk_size);
                    xr_mesh mesh = new xr_mesh(chunk_data);
                    mesh.OnReady();
                    array.Add(mesh);
                }
            }
            Console.Log(array.Count+ " mesh processed...");
            return array.ToArray();
        }

        protected void ProcessMeshChunks(byte[] data)
         {
            Console.Log("Reading mesh chunk... (Size " + data.Length+")");
            Chunks.Clear();
            using (BinaryReader reader = new BinaryReader(new MemoryStream(data)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    int chunk_id = reader.ReadInt32();
                    int chunk_size = reader.ReadInt32();
                    byte[] chunk_data = reader.ReadBytes(chunk_size);
                    string chunk_name = Enum.GetName(typeof(emesh_chunk_id), chunk_id);

                    if (chunk_name == null || chunk_name.Length == 0)
                    {
                        chunk_name += "UNKNOWNN_" + chunk_id;
                    }

                    xr_chunk chunk = new xr_chunk(chunk_id, chunk_size, chunk_data, chunk_name);
                    Chunks.Add(chunk);
                    SetLoadedChunk(chunk.id);
                    Console.Log("Chunk " + chunk.name + " (" + chunk_id + ") processed... Size " + chunk_size + " bytes");
                }
            }
        }

        protected xr_bone[] ReadObjectBones(byte[] data)
        {
            Console.Log("Reading bones...");
            List<xr_bone> array = new List<xr_bone>();
            using (BinaryReader reader = new BinaryReader(new MemoryStream(data)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    int chunk_id = reader.ReadInt32();
                    int chunk_size = reader.ReadInt32();
                    byte[] chunk_data = reader.ReadBytes(chunk_size);
                    xr_bone bone = new xr_bone(chunk_data);
                    bone.OnReady();
                    array.Add(bone);
                }
            }
            Console.Log(array.Count + " bones processed...");
            return array.ToArray();
        }

        protected void ProcessBoneChunks(byte[] data)
        {
            Console.Log("Reading bone chunk... (Size " + data.Length + ")");
            Chunks.Clear();
            using (BinaryReader reader = new BinaryReader(new MemoryStream(data)))
            {
                while (reader.BaseStream.Position < reader.BaseStream.Length)
                {
                    int chunk_id = reader.ReadInt32();
                    int chunk_size = reader.ReadInt32();
                    byte[] chunk_data = reader.ReadBytes(chunk_size);
                    string chunk_name = Enum.GetName(typeof(ebone_chunk_id), chunk_id);

                    if (chunk_name == null || chunk_name.Length == 0)
                    {
                        chunk_name += "UNKNOWNN_" + chunk_id;
                    }

                    xr_chunk chunk = new xr_chunk(chunk_id, chunk_size, chunk_data, chunk_name);
                    Chunks.Add(chunk);
                    SetLoadedChunk(chunk.id);
                    Console.Log("Chunk " + chunk.name + " (" + chunk_id + ") processed... Size " + chunk_size + " bytes");
                }
            }
        }
    }
}

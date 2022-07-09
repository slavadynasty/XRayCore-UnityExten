using System;
using System.Collections.Generic;
using System.IO;
using XRay.Core.Enums;

namespace XRay.Core
{
    public abstract partial class xr_file
    {
        public string FileName { get; private set; }
        public xr_file_type FileType { get; private set; } = xr_file_type._unknown;
        public List<xr_chunk> Chunks { get; private set; } = new List<xr_chunk>();

        public bool GetChunk(int chunk_id, out xr_chunk chunk)
        {
            chunk = null;

            if (!IsChunkLoaded(chunk_id))
            {
                return false;
            }

            for (int i = 0; i < Chunks.Count; i++)
            {
                if (Chunks[i].id == chunk_id)
                {
                    chunk = Chunks[i];
                    return true;
                }
            }

            return false;
        }

        private int ChunksMask = 0;

        public bool IsChunkLoaded(int id)
        {
            return (ChunksMask & (1 << id)) != 0;
        }

        private void SetLoadedChunk(int id)
        {
            ChunksMask |= 1 << id;
        }

        public bool IsLoaded { get; private set; } = false;

        public bool Load(string path)
        {
            if (FileType == xr_file_type._virtual)
            {
                Console.Log("Can't load virtual file!");
                return false;
            }

            if (IsLoaded)
            {
                Console.Warning("Can't load " + FileName + "! Already loaded!");
                return false;
            }
            IsLoaded = true;
            FileName = Path.GetFileName(path);

            FileType = xr_file_type._unknown;
            if (Path.GetExtension(path).ToLower() == ".object") { FileType = xr_file_type._object; }
            else if (Path.GetExtension(path).ToLower() == ".ogf") { FileType = xr_file_type._ogf; }
            else { FileType = xr_file_type._unknown; Console.Warning("Unsupported file format!"); return false; }

            //processing chunks
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open);
                Console.Log("Opening file " + FileName + "  Size " + fs.Length + " bytes");

                if (FileType == xr_file_type._object)
                {
                    using (BinaryReader reader = new BinaryReader(fs))
                    {
                        int chunk_id = reader.ReadInt32();
                        int chunk_size = reader.ReadInt32();
                        byte[] chunk_data = reader.ReadBytes(chunk_size);
                        xr_chunk chunk = new xr_chunk(chunk_id, chunk_size, chunk_data, Enum.GetName(typeof(eobj_chunk_id), chunk_id));
                        Chunks.Add(chunk);
                        SetLoadedChunk(chunk.id);
                        Console.Log("Chunk " + chunk.name + " cached... Size " + chunk_size + " bytes");

                        ProcessObjectChunks(chunk.data);
                    }

                    Console.Log("File closed, " + Chunks.Count + " chunks cached in memory...");
                    fs.Close();
                    OnReady();
                    return true;
                }
                else if (FileType == xr_file_type._ogf)
                {
                    using (BinaryReader reader = new BinaryReader(fs))
                    {
                        int chunk_id = reader.ReadInt32();

                        if ((ogf_chunk_id)chunk_id == ogf_chunk_id.OGF_HEADER)
                        {
                            int chunk_size = reader.ReadInt32();
                            byte[] chunk_data = reader.ReadBytes(chunk_size);

                            xr_chunk chunk = new xr_chunk(chunk_id, chunk_size, chunk_data, Enum.GetName(typeof(ogf_chunk_id), chunk_id));
                            Chunks.Add(chunk);
                            SetLoadedChunk(chunk.id);
                            Console.Log("Chunk " + chunk.name + " cached... Size " + chunk_size + " bytes");

                            ProcessOGFChunks(chunk.data);
                        }
                        else
                        {
                            Console.Log("Can't find ogf header!");
                        }
                    }

                    Console.Log("File closed, " + Chunks.Count + " chunks cached in memory...");
                    fs.Close();
                    OnReady();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.Error("Error on read file " + FileName + " (" + ex + ")");
            }
            return false;
        }

        protected virtual int OnDumpChunks(string path) { return 0; }

        public void DumpChunks()
        {
            string path = Path.Combine(Environment.CurrentDirectory, FileName);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            int dumped = 0;
            for (int i = 0; i < Chunks.Count; i++)
            {
                try
                {
                    string chunk_path = Path.Combine(path, Chunks[i].name);
                    using (FileStream fs = File.OpenWrite(chunk_path))
                    {
                        fs.Write(Chunks[i].data, 0, Chunks[i].size);
                        fs.Close();
                    }
                    Console.Log("Chunk " + Chunks[i].name + " dumped  Size " + Chunks[i].data.Length + " bytes");
                    dumped++;
                }
                catch
                {
                    Console.Error("Can't dump chunk " + Chunks[i].name);
                }
            }

            dumped += OnDumpChunks(path);

            Console.Log("Dumping complete! Total " + dumped + " chunks dumped...");
        }

        public xr_file() { }
        public xr_file(bool virtual_file = true)
        {
            FileName = "VirtualFile";
            FileType = xr_file_type._virtual;
            IsLoaded = true;
        }

        ~xr_file()
        {
            Chunks.Clear();
        }

        protected abstract void OnReady();
    }
}

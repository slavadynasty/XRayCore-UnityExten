using System;
using System.Collections.Generic;
using System.IO;
using XRay.Core.Enums;
using XRay.Math;

namespace XRay.Core
{
    public class xr_bone : xr_file
    {
        public ushort m_id;
        public xr_bone m_parent;
        public List<xr_bone> m_children = new List<xr_bone>();
        public Vector3 m_mot_offset = Vector3.Zero;
        public Vector3 m_mot_rotate = Vector3.Zero;
        public float m_mot_length;

        public Matrix4x4 m_mot_xform = Matrix4x4.Identity;
        public Matrix4x4 m_mot_i_xform = Matrix4x4.Identity;

        public Matrix4x4 m_bind_xform = Matrix4x4.Identity;
        public Matrix4x4 m_bind_i_xform = Matrix4x4.Identity;

        public void CalculateBind(Matrix4x4 parent_xform)
        {
            m_bind_xform.set_xyz_i(m_bind_rotate);
            m_bind_xform.setC(m_bind_offset);
            m_bind_xform.mul_a_43(parent_xform);
            m_bind_i_xform.invert_43(m_bind_xform);

            for(int i = 0; i < m_children.Count; i++)
            {
                m_children[i].CalculateBind(m_bind_xform);
            }
        }

        public void Setup(ushort id, xr_object _object)
        {
            m_id = id;
            if(m_parent_name.Length > 0)
            {
                m_parent = _object.FindBone(m_parent_name);

                if (m_parent != null)
                {
                    m_parent.m_children.Add(this);
                }
                else
                {
                    m_parent_name = string.Empty;
                }
            }
        }

        public Matrix4x4 m_last_xform = Matrix4x4.Identity;
        public Matrix4x4 m_render_xform = Matrix4x4.Identity;

        public string m_name = string.Empty;
        public string m_parent_name = string.Empty;
        public string m_vmap_name = string.Empty;

        public Vector3 m_bind_rotate = Vector3.Zero;
        public Vector3 m_bind_offset = Vector3.Zero;
        public float m_bind_length;

        public string m_gamemtl;

        public float m_mass;
        public Vector3 m_center_of_mass;

        public bool is_root() { return m_parent == null; }

        public xr_bone() : base(true) 
        { 
            m_name = string.Empty;
            m_parent_name = string.Empty;
            m_vmap_name = string.Empty;
        }
        public xr_bone(BinaryReader reader) : base(true) 
        {
            m_name = reader.ReadStringT();
            m_parent_name = reader.ReadStringT();
            m_vmap_name = reader.ReadStringT();
            m_bind_offset = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            m_bind_rotate = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());
            m_bind_length = reader.ReadSingle();
        }

        public xr_bone(byte[] data) : base(true)
        {
            ProcessBoneChunks(data);
        }

        protected override void OnReady()
        {
            {
                if (GetChunk((int)(ebone_chunk_id.BONE_CHUNK_DEF), out xr_chunk chunk))
                {
                    m_name = chunk.reader.ReadStringT();
                    m_parent_name = chunk.reader.ReadStringT();
                    m_vmap_name = chunk.reader.ReadStringT();

                    Console.Log("Bone: " + m_name + " | Parent: " + m_parent_name + " | VMap: " + m_vmap_name);
                }
            }
            {
                if(GetChunk((int)(ebone_chunk_id.BONE_CHUNK_BIND_POSE), out xr_chunk chunk))
                {
                    m_bind_offset = new Vector3(chunk.reader);
                    m_bind_rotate = new Vector3(chunk.reader);
                    m_bind_length = chunk.reader.ReadSingle();
                }
            }
            {
                if (GetChunk((int)(ebone_chunk_id.BONE_CHUNK_MATERIAL), out xr_chunk chunk))
                {
                    m_gamemtl = chunk.reader.ReadStringT();
                }
            }
        }
    }
}

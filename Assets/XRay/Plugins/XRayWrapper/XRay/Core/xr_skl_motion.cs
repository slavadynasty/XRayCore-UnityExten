using System;
using System.IO;

namespace XRay.Core
{
    public struct motion_mark
    {
        public float t0, t1;
    }

    public class xr_motion_marks
    {
        public string m_name;
        public motion_mark[] marks;
    }

    public class xr_bone_motion
    {
        [Flags]
        public enum bone_motion_flag
        {
            BMF_WORLD_ORIENT = 0x1,
        };

        public string m_name;
        public xr_envelope[] m_envelopes = new xr_envelope[6];
        public bone_motion_flag m_flags;

        public void load_0(BinaryReader reader)
        {
            m_flags = (bone_motion_flag)reader.ReadInt32();
            Console.Log(m_name+" flags: "+m_flags);
            for(int i = 0; i < m_envelopes.Length; i++)
            {
                m_envelopes[i] = new xr_envelope();
                m_envelopes[i].load_1(reader);
            }
        }

        public void load_1(BinaryReader reader)
        {
            m_flags = (bone_motion_flag)reader.ReadInt32();
            Console.Log(m_name + " flags: " + m_flags);
            for (int i = 0; i < m_envelopes.Length; i++)
            {
                m_envelopes[i] = new xr_envelope();
                m_envelopes[i].load_1(reader);
            }
        }

        public void load_2(BinaryReader reader)
        {
            m_name = reader.ReadStringT();
            m_flags = (bone_motion_flag)reader.ReadByte();
            Console.Log(m_name + " flags: " + m_flags);
            for (int i = 0; i < m_envelopes.Length; i++)
            {
                m_envelopes[i] = new xr_envelope();
                m_envelopes[i].load_2(reader);
            }
        }
    }



    public enum SMOTION_VERSION
    {
        SMOTION_VERSION_4 = 4,
        SMOTION_VERSION_5 = 5,
        SMOTION_VERSION_6 = 6,
        SMOTION_VERSION_7 = 7,  // guessed 3120 (Clear Sky)
    };

    [Flags]
    public enum motion_flag
    {
        SMF_FX = 0x1,
        SMF_STOP_AT_END = 0x2,
        SMF_NO_MIX = 0x4,
        SMF_SYNC_PART = 0x8,
    };

    public class xr_skl_motion : xr_motion
    {
        public xr_bone_motion[] m_bone_motions;
        public ushort m_bone_or_part;
        public float m_speed;
        public float m_accrue;
        public float m_falloff;
        public float m_power;
        public motion_flag m_flags;
        public xr_motion_marks[] m_marks;

        public xr_skl_motion(BinaryReader reader) : base(reader)
        {
            SMOTION_VERSION version = (SMOTION_VERSION)reader.ReadUInt16();
            Console.Log("Version "+version);
            if (version == SMOTION_VERSION.SMOTION_VERSION_4)
            {
                m_bone_or_part = reader.ReadUInt16();

                if (reader.ReadBoolean()) { m_flags |= motion_flag.SMF_FX; } else { m_flags &= ~motion_flag.SMF_FX; }
                if (reader.ReadBoolean()) { m_flags |= motion_flag.SMF_STOP_AT_END; } else { m_flags &= ~motion_flag.SMF_STOP_AT_END; }

                m_speed = reader.ReadSingle();
                m_accrue = reader.ReadSingle();
                m_falloff = reader.ReadSingle();
                m_power = reader.ReadSingle();

                m_bone_motions = new xr_bone_motion[reader.ReadInt32()];
                for (int i = 0; i < m_bone_motions.Length; i++)
                {
                    m_bone_motions[i] = new xr_bone_motion();
                    m_bone_motions[i].m_name = "motion_"+i;
                    m_bone_motions[i].load_0(reader);

                }
            }
            else if (version == SMOTION_VERSION.SMOTION_VERSION_5)
            {
                m_flags = (motion_flag)reader.ReadInt32();
                m_bone_or_part = ((ushort)(reader.ReadInt32() & ushort.MaxValue));
                m_speed = reader.ReadSingle();
                m_accrue = reader.ReadSingle();
                m_falloff = reader.ReadSingle();
                m_power = reader.ReadSingle();

                m_bone_motions = new xr_bone_motion[reader.ReadInt32()];
                for (int i = 0; i < m_bone_motions.Length; i++)
                {
                    m_bone_motions[i] = new xr_bone_motion();
                    m_bone_motions[i].m_name = "motion_" + i;
                    m_bone_motions[i].load_1(reader);

                }
            }
            else if (version == SMOTION_VERSION.SMOTION_VERSION_6)
            {
                m_flags = (motion_flag)reader.ReadUInt16();
                m_bone_or_part = reader.ReadUInt16();
                m_speed = reader.ReadSingle();
                m_accrue = reader.ReadSingle();
                m_falloff = reader.ReadSingle();
                m_power = reader.ReadSingle();

                m_bone_motions = new xr_bone_motion[reader.ReadInt16()];
                for (int i = 0; i < m_bone_motions.Length; i++)
                {
                    m_bone_motions[i] = new xr_bone_motion();
                    m_bone_motions[i].load_2(reader);

                }
            }
            else
            {
                Console.Warning("Version " + version + " not implemented!");
            }
        }
    }
}

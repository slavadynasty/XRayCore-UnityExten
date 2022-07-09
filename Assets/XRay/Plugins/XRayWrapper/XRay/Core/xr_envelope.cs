using System.IO;

namespace XRay.Core
{
    public enum shape_type
	{
		SHAPE_TCB = 0,
		SHAPE_HERM = 1,
		SHAPE_BEZI = 2,
		SHAPE_LINE = 3,
		SHAPE_STEP = 4,
		SHAPE_BEZ2 = 5,
	};

	public enum behaviour
	{
		BEH_RESET = 0,
		BEH_CONSTANT = 1,
		BEH_REPEAT = 2,
		BEH_OSCILLATE = 3,
		BEH_OFFSET = 4,
		BEH_LINEAR = 5,
	};

	public class xr_key
    {
		public float value;
		public float time;
		public shape_type shape;
		public float tension;
		public float continuity;
		public float bias;
		public float[] param;

		public void load_1(BinaryReader reader)
        {
			value = reader.ReadSingle();
			time = reader.ReadSingle();
			shape = (shape_type)reader.ReadInt32();
			tension = reader.ReadSingle();
			continuity = reader.ReadSingle();
			bias = reader.ReadSingle();
			param = new float[4];

			for (int i = 0; i < param.Length; i++)
			{
				param[i] = reader.ReadSingle();
			}
		}

		public void load_2(BinaryReader reader)
		{
			value = reader.ReadSingle();
			time = reader.ReadSingle();
			shape = (shape_type)reader.ReadByte();
			if (shape != shape_type.SHAPE_STEP)
			{
				tension = reader.ReadSingle16(-32f,32);
				continuity = reader.ReadSingle16(-32f, 32);
				bias = reader.ReadSingle16(-32f, 32);
				param = new float[4];

				for (int i = 0; i < param.Length; i++)
				{
					param[i] = reader.ReadSingle16(-32f, 32);
				}
			}
		}
	}

	public class xr_envelope
    {
		public xr_key[] m_keys;
		public behaviour m_behaviour0;
		public behaviour m_behaviour1;

		public void load_1(BinaryReader reader)
        {
			m_behaviour0 = (behaviour)reader.ReadByte();
			m_behaviour1 = (behaviour)reader.ReadByte();
			m_keys = new xr_key[reader.ReadInt32()];

			for(int i = 0; i < m_keys.Length; i++)
            {
				m_keys[i].load_1(reader);
            }

			Console.Log(m_keys.Length+" keys of envelope processed...");
        }

		public void load_2(BinaryReader reader)
		{
			m_behaviour0 = (behaviour)reader.ReadByte();
			m_behaviour1 = (behaviour)reader.ReadByte();
			m_keys = new xr_key[reader.ReadInt16()];

			for (int i = 0; i < m_keys.Length; i++)
			{
				m_keys[i].load_2(reader);
			}

			Console.Log(m_keys.Length + " keys of envelope processed...");
		}
	}
}

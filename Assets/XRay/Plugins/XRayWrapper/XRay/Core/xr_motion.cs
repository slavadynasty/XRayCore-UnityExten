using System.IO;

namespace XRay.Core
{
    public class xr_motion
    {
        public int m_frame_start;
        public int m_frame_end;
        public float m_fps;
        public string m_name = string.Empty;

        public xr_motion(BinaryReader reader)
        {
            m_name = reader.ReadStringT();
            m_frame_start = reader.ReadInt32();
            m_frame_end = reader.ReadInt32();
            m_fps = reader.ReadSingle();

            Console.Log("Motion "+m_name+" FPS: "+m_fps+"   Start frame: "+m_frame_start+"  end: "+m_frame_end);
        }
    }
}

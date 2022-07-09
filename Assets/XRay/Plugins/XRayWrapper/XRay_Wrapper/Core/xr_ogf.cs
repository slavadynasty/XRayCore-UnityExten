using System;
using static XRay.Core.Wrapper;

namespace XRay.Core
{
    public class xr_ogf
    {
        public IntPtr pointer { get; private set; }
        public ogf_version version { get; private set; }
        public ogf_model_type model_type { get; private set; }
        public bbox bbox { get; private set; }
        public bsphere bsphere { get; private set; }

        public static xr_ogf Load(string path)
        {
            if (get_xr_ogf(path, out ogf_version version, out ogf_model_type model_type, out IntPtr ptr))
            {
                if (ptr != IntPtr.Zero)
                {
                    return new xr_ogf(ptr, version, model_type);
                }
                return null;
            }

            return null;
        }

        public static bool Load(string path, out xr_ogf _ogf)
        {
            _ogf = null;
            if (get_xr_ogf(path, out ogf_version version, out ogf_model_type model_type, out IntPtr ptr))
            {
                if (ptr != IntPtr.Zero)
                {
                    _ogf = new xr_ogf(ptr, version, model_type);
                    return true;
                }
                return false;
            }

            return false;
        }

        public xr_ogf(string path)
        {
            pointer = IntPtr.Zero;

            if (get_xr_ogf(path, out ogf_version version, out ogf_model_type model_type, out IntPtr ptr))
            {
                pointer = ptr;
                this.version = version;
                this.model_type = model_type;
                InternalParse();
            }
        }

        public xr_ogf(IntPtr ptr, ogf_version version, ogf_model_type model_type)
        {
            pointer = ptr;
            this.version = version;
            this.model_type = model_type;
            InternalParse();
        }

        private void InternalParse()
        {
            if (pointer != IntPtr.Zero)
            {
                bbox = xr_ogf_bbox(pointer);
                bsphere = xr_ogf_bsphere(pointer);
                xr_ogf_vb(pointer, out bool has_points, out bool has_normals, out bool has_texcoords, out bool has_influences, out bool has_colors, out bool has_lightmaps);
                Console.Log("vb info -> has_points: "+ has_points + " has_normals: "+has_normals+" has_texcoords: "+has_texcoords+" has_influences: "+has_influences+" has_colors: "+has_colors+" has_lightmaps: "+has_lightmaps);
            }
        }
    }
}

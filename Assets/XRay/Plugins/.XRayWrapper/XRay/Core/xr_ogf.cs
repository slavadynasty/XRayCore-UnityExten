using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XRay.Core.Enums;

namespace XRay.Core
{
    public class xr_ogf : xr_file
    {
        protected override void OnReady()
        {
            {
                if(GetChunk((int)(ogf_chunk_id.OGF4_VERTICES), out xr_chunk chunk))
                {
                    Console.Log("Vertices sz "+chunk.size);
                }
            }
        }
    }
}

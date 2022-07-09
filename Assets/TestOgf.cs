using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XRay.Core;

public class TestOgf : MonoBehaviour
{
    public xr_ogf ogf;
    
    [ContextMenu("Load OGF")]
    public void LoadOgf()
    {
        ogf = new xr_ogf("C:\\Users\\Slava\\Desktop\\stalker_exoskeleton.ogf");
    }
}

namespace XRay.Core
{
    public struct xr_mesh
    {
        public string name;
        public bbox bbox;
        public fvector3[] points;
        public lw_face[] faces;
        public lw_vmref[] vmrefs;
        public xr_surfmap[] surfmaps;
    }
}

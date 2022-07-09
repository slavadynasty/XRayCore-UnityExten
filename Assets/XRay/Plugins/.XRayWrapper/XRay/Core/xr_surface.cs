namespace XRay.Core
{
    public struct xr_surface
    {
        public string name;
        public string eshader;
        public string cshader;
        public string gamemtl;
        public string texture;
        public string vmap;
        public uint flags;
        public uint fvf;

        public xr_surface(string name, string eshader, string cshader, string gamemtl, string texture, string vmap, uint flags, uint fvf)
        {
            this.name = name;
            this.eshader = eshader;
            this.cshader = cshader;
            this.gamemtl = gamemtl;
            this.texture = texture;
            this.vmap = vmap;
            this.flags = flags;
            this.fvf = fvf;
        }
    }
}

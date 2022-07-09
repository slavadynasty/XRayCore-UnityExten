using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XRay.Core.Enums
{
    public enum xr_file_type
    {
        _unknown,
        _virtual,
        _object,
        _ogf
    }

    public enum eobj_chunk_id
    {
        EOBJ_CHUNK_MAIN = 0x7777,

        EOBJ_CHUNK_VERSION = 0x0900,
        EOBJ_CHUNK_FLAGS = 0x0903,
        EOBJ_CHUNK_SURFACES_0 = 0x0905, // old format
        EOBJ_CHUNK_SURFACES_1 = 0x0906, // old format
        EOBJ_CHUNK_SURFACES_2 = 0x0907,
        EOBJ_CHUNK_MESHES = 0x0910,
        EOBJ_CHUNK_0911 = 0x0911,   // ignored by AE
        EOBJ_CHUNK_USERDATA = 0x0912,
        EOBJ_CHUNK_BONES_0 = 0x0913,    // old format
        EOBJ_CHUNK_MOTIONS = 0x0916,
        EOBJ_CHUNK_SHADERS_0 = 0x0918,  // old format
        EOBJ_CHUNK_PARTITIONS_0 = 0x0919,   // old format
        EOBJ_CHUNK_TRANSFORM = 0x0920,
        EOBJ_CHUNK_BONES_1 = 0x0921,
        EOBJ_CHUNK_REVISION = 0x0922,   // file revision
        EOBJ_CHUNK_PARTITIONS_1 = 0x0923,
        EOBJ_CHUNK_MOTION_REFS = 0x0924,
        EOBJ_CHUNK_LOD_REF = 0x0925,    // LOD\Reference
    };

    public enum ogf_chunk_id
    {
        OGF_HEADER = 0x1,

        //build 729
        OGF2_TEXTURE = 0x2,
        OGF2_TEXTURE_L = 0x3,
        OGF2_BBOX = 0x6,
        OGF2_VERTICES = 0x7,
        OGF2_INDICES = 0x8,
        OGF2_VCONTAINER = 0xb,
        OGF2_BSPHERE = 0xc,

        OGF3_TEXTURE = 0x2,
        OGF3_TEXTURE_L = 0x3,
        OGF3_CHILD_REFS = 0x5,
        OGF3_BBOX = 0x6,
        OGF3_VERTICES = 0x7,
        OGF3_INDICES = 0x8,
        OGF3_LODDATA = 0x9, // not sure
        OGF3_VCONTAINER = 0xa,
        OGF3_BSPHERE = 0xb,
        OGF3_CHILDREN_L = 0xc,
        OGF3_S_BONE_NAMES = 0xd,
        OGF3_S_MOTIONS = 0xe,// build 1469 - 1580
        OGF3_DPATCH = 0xf,  // guessed name
        OGF3_LODS = 0x10,   // guessed name
        OGF3_CHILDREN = 0x11,
        OGF3_S_SMPARAMS = 0x12,// build 1469
        OGF3_ICONTAINER = 0x13,// build 1865
        OGF3_S_SMPARAMS_NEW = 0x14,// build 1472 - 1865
        OGF3_LODDEF2 = 0x15,// build 1865
        OGF3_TREEDEF2 = 0x16,// build 1865
        OGF3_S_IKDATA_0 = 0x17,// build 1475 - 1580
        OGF3_S_USERDATA = 0x18,// build 1537 - 1865
        OGF3_S_IKDATA = 0x19,// build 1616 - 1829, 1844
        OGF3_S_MOTIONS_NEW = 0x1a,// build 1616 - 1865
        OGF3_S_DESC = 0x1b,// build 1844
        OGF3_S_IKDATA_2 = 0x1C,// build 1842 - 1865
        OGF3_S_MOTION_REFS = 0x1D,// build 1842

        OGF4_TEXTURE = 0x2,
        OGF4_VERTICES = 0x3,
        OGF4_INDICES = 0x4,
        OGF4_P_MAP = 0x5,
        OGF4_SWIDATA = 0x6,
        OGF4_VCONTAINER = 0x7,
        OGF4_ICONTAINER = 0x8,
        OGF4_CHILDREN = 0x9,
        OGF4_CHILDREN_L = 0xa,
        OGF4_LODDEF2 = 0xb,
        OGF4_TREEDEF2 = 0xc,
        OGF4_S_BONE_NAMES = 0xd,
        OGF4_S_MOTIONS = 0xe,
        OGF4_S_SMPARAMS = 0xf,
        OGF4_S_IKDATA = 0x10,
        OGF4_S_USERDATA = 0x11,
        OGF4_S_DESC = 0x12,
        OGF4_S_MOTION_REFS_0 = 0x13,    // pre-CS format
        OGF4_SWICONTAINER = 0x14,
        OGF4_GCONTAINER = 0x15,
        OGF4_FASTPATH = 0x16,
        OGF4_S_LODS = 0x17,
        OGF4_S_MOTION_REFS_1 = 0x18,    // introduced in clear sky
    };

    public enum ogf_version
    {
        OGF2_VERSION = 2,
        OGF3_VERSION = 3,
        OGF4_VERSION = 4,
    };

    public enum ogf_model_type
    {
        MT3_NORMAL = 0, // Fvisual
        MT3_HIERRARHY = 0x1,    // FHierrarhyVisual
        MT3_PROGRESSIVE = 0x2,  // FProgressiveFixedVisual
        MT3_SKELETON_GEOMDEF_PM = 0x3,  // CSkeletonX_PM
        MT3_SKELETON_ANIM = 0x4,    // CKinematics
        MT3_DETAIL_PATCH = 0x6, // FDetailPatch
        MT3_SKELETON_GEOMDEF_ST = 0x7,  // CSkeletonX_ST
        MT3_CACHED = 0x8,   // FCached
        MT3_PARTICLE = 0x9, // CPSVisual
        MT3_PROGRESSIVE2 = 0xa, // FProgressive
        MT3_LOD = 0xb,  // FLOD build 1472 - 1865
        MT3_TREE = 0xc, // FTreeVisual build 1472 - 1865
                        //				= 0xd,	// CParticleEffect 1844
                        //				= 0xe,	// CParticleGroup 1844
        MT3_SKELETON_RIGID = 0xf,   // CSkeletonRigid 1844

        MT4_NORMAL = 0, // Fvisual
        MT4_HIERRARHY = 0x1,    // FHierrarhyVisual
        MT4_PROGRESSIVE = 0x2,  // FProgressive
        MT4_SKELETON_ANIM = 0x3,    // CKinematicsAnimated
        MT4_SKELETON_GEOMDEF_PM = 0x4,  // CSkeletonX_PM
        MT4_SKELETON_GEOMDEF_ST = 0x5,  // CSkeletonX_ST
        MT4_LOD = 0x6,  // FLOD
        MT4_TREE_ST = 0x7,  // FTreeVisual_ST
        MT4_PARTICLE_EFFECT = 0x8,  // PS::CParticleEffect
        MT4_PARTICLE_GROUP = 0x9,   // PS::CParticleGroup
        MT4_SKELETON_RIGID = 0xa,   // CKinematics
        MT4_TREE_PM = 0xb,  // FTreeVisual_PM

        MT4_OMF = 0x40, // fake model type to distinguish .omf
    };

    public enum emesh_chunk_id
    {
        EMESH_CHUNK_VERSION = 0x1000,
        EMESH_CHUNK_MESHNAME = 0x1001,
        EMESH_CHUNK_FLAGS = 0x1002,
        EMESH_CHUNK_BBOX = 0x1004,
        EMESH_CHUNK_VERTS = 0x1005,
        EMESH_CHUNK_FACES = 0x1006,
        EMESH_CHUNK_VMAPS_0 = 0x1007,
        EMESH_CHUNK_VMREFS = 0x1008,
        EMESH_CHUNK_SFACE = 0x1009,
        EMESH_CHUNK_OPTIONS = 0x1010,
        EMESH_CHUNK_VMAPS_1 = 0x1011,
        EMESH_CHUNK_VMAPS_2 = 0x1012,
        EMESH_CHUNK_SG = 0x1013,
    };

    public enum ebone_chunk_id
    {
        BONE_CHUNK_VERSION = 0x0001,
        BONE_CHUNK_DEF = 0x0002,
        BONE_CHUNK_BIND_POSE = 0x0003,
        BONE_CHUNK_MATERIAL = 0x0004,
        BONE_CHUNK_SHAPE = 0x0005,
        BONE_CHUNK_IK_JOINT = 0x0006,
        BONE_CHUNK_MASS_PARAMS = 0x0007,
        BONE_CHUNK_IK_FLAGS = 0x0008,
        BONE_CHUNK_BREAK_PARAMS = 0x0009,
        BONE_CHUNK_FRICTION = 0x0010,
    };
}

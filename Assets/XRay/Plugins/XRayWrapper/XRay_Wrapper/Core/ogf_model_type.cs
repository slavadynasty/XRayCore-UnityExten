namespace XRay.Core
{
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
	}
}

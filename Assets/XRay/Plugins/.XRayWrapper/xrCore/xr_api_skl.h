#pragma once

#include "xr_api.h"

using namespace xray_re;

XR_API bool get_xr_ogf_omf(const char* path, xr_ogf_v4** out_ogf)
{
	xr_ogf_v4* ogf = new xr_ogf_v4;
	if (ogf->load_omf(path))
	{
		*out_ogf = ogf;
		return true;
	}
	delete ogf;
	return false;
}

XR_API size_t xr_ogf_motions_size(xr_ogf_v4* out_ogf)
{
	return out_ogf->motions().size();
}

XR_API xr_skl_motion* xr_ogf_motions_get_skl(xr_ogf_v4* out_ogf, size_t index)
{
	return out_ogf->motions().at(index);
}

XR_API float xr_ogf_skl_get_fps(xr_skl_motion* skl)
{
	return skl->fps();
}

XR_API size_t xr_skl_bone_motions_size(xr_skl_motion* skl)
{
	return skl->bone_motions().size();
}

XR_API xr_bone_motion* xr_skl_get_bone_motion(xr_skl_motion* skl, size_t index)
{
	return skl->bone_motions().at(index);
}

XR_API const char* xr_bone_motion_name(xr_bone_motion* bmotion)
{
	return bmotion->name().size() > 0 ? bmotion->name().c_str() : "no_name";
}

XR_API void xr_bone_motion_evaluate(xr_bone_motion* bmotion, float fps, size_t xframe, fvector3* t, fvector3* r)
{
	bmotion->evaluate(xframe / fps, *t, *r);
}
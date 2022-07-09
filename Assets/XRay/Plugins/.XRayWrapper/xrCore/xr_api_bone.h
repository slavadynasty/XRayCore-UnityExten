#pragma once

#include "xr_api.h"
#include "xr_bone.h"
#include "xr_matrix.h"
using namespace xray_re;

XR_API size_t xr_object_bones_count(xr_object* object)
{
	return object->bones().size();
}

XR_API xr_bone* xr_object_get_bone(xr_object* object, size_t index)
{
	return object->bones().at(index);
}

XR_API size_t xr_ogf_bones_count(xr_ogf* ogf)
{
	return ogf->bones().size();
}

XR_API xr_bone* xr_ogf_get_bone(xr_ogf* ogf, size_t index)
{
	return ogf->bones().at(index);
}

XR_API uint16_t xr_bone_id(xr_bone* bone) 
{
	return bone->id();
}

XR_API bool xr_bone_is_root(xr_bone* bone)
{
	return bone->is_root();
}

XR_API fmatrix xr_bone_bind_xform(xr_bone* bone)
{
	return bone->bind_xform();
}

XR_API fmatrix xr_bone_bind_i_xform(xr_bone* bone)
{
	return bone->bind_i_xform();
}

XR_API void xr_bone_calculate_bind(xr_bone* bone, fmatrix parent_xform)
{
	bone->calculate_bind(parent_xform);
}

XR_API void xr_bone_calculate_motion(xr_bone* bone, xr_skl_motion* skl, fmatrix parent_xform)
{
	bone->calculate_motion(skl, parent_xform);
}

XR_API const char* xr_bone_name(xr_bone* bone)
{
	return bone->name().c_str();
}

XR_API const char* xr_bone_parent_name(xr_bone* bone)
{
	return bone->parent_name().c_str();
}

XR_API const char* xr_bone_vmap_name(xr_bone* bone)
{
	return bone->vmap_name().c_str();
}

XR_API fvector3 xr_bone_bind_rotate(xr_bone* bone)
{
	return bone->bind_rotate();
}

XR_API fvector3 xr_bone_bind_offset(xr_bone* bone)
{
	return bone->bind_offset();
}

XR_API const char* xr_bone_gamemtl(xr_bone* bone)
{
	return bone->gamemtl().c_str();
}
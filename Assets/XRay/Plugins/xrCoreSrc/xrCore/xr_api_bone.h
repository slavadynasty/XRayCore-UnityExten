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

XR_API void xr_bone_bind_xform(xr_bone* bone, float* _11, float* _12, float* _13, float* _14, float* _21, float* _22, float* _23, float* _24, float* _31, float* _32, float* _33, float* _34, float* _41, float* _42, float* _43, float* _44)
{
	*_11 = bone->bind_xform()._11;
	*_12 = bone->bind_xform()._12;
	*_13 = bone->bind_xform()._13;
	*_14 = bone->bind_xform()._14;

	*_21 = bone->bind_xform()._21;
	*_22 = bone->bind_xform()._22;
	*_23 = bone->bind_xform()._23;
	*_24 = bone->bind_xform()._24;

	*_31 = bone->bind_xform()._31;
	*_32 = bone->bind_xform()._32;
	*_33 = bone->bind_xform()._33;
	*_34 = bone->bind_xform()._34;

	*_41 = bone->bind_xform()._41;
	*_42 = bone->bind_xform()._42;
	*_43 = bone->bind_xform()._43;
	*_44 = bone->bind_xform()._44;
}

XR_API fmatrix xr_bone_bind_i_xform(xr_bone* bone)
{
	return bone->bind_i_xform();
}

XR_API void xr_bone_calculate_bind(xr_bone* bone, fmatrix parent_xform)
{
	bone->calculate_bind(parent_xform);
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
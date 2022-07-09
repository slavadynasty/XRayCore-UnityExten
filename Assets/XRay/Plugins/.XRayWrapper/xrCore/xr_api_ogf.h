#pragma once

#include "xr_api.h"
#include "xr_ogf_format.h"
#include "xr_ogf.h"
#include "xr_ogf_v3.h"
#include "xr_ogf_v4.h"

using namespace xray_re;

XR_API bool get_xr_ogf(const char* path, ogf_version* version, ogf_model_type* model_type, xr_ogf** out_ogf) 
{
	xr_ogf* ogf = xr_ogf::load_ogf(path);
	if (ogf != NULL)
	{
		*out_ogf = ogf;
		*version = ogf->version();
		*model_type = ogf->model_type();
		return true;
	}
	delete ogf;
	return false;
}

XR_API bool free_xr_ogf(xr_ogf* ogf) 
{
	if (ogf != NULL)
	{
		free(ogf);
		return true;
	}
	return false;
}

XR_API fbox xr_ogf_bbox(xr_ogf* ogf) 
{
	return ogf->bbox();
}

XR_API fsphere xr_ogf_bsphere(xr_ogf* ogf) 
{
	return ogf->bsphere();
}

XR_API size_t xr_ogf_vb_size(xr_ogf* ogf)
{
	return ogf->vb().size();
}

XR_API void xr_ogf_vb_types(xr_ogf* ogf, bool* has_points, bool* has_normals, bool* has_texcoords, bool* has_influences, bool* has_colors, bool* has_lightmaps)
{
	if (ogf != NULL)
	{
		*has_points = ogf->vb().has_points();
		*has_normals = ogf->vb().has_normals();
		*has_texcoords = ogf->vb().has_texcoords();
		*has_influences = ogf->vb().has_influences();
		*has_colors = ogf->vb().has_colors();
		*has_lightmaps = ogf->vb().has_lightmaps();
	}
}

XR_API void xr_ogf_struct(xr_ogf* ogf, bool* hierarchical, bool* skeletal, bool* animated, bool* progressive, bool* versioned)
{
	*hierarchical = ogf->hierarchical();
	*skeletal = ogf->skeletal();
	*animated = ogf->animated();
	*progressive = ogf->progressive();
	*versioned = ogf->versioned();
}

XR_API xr_ogf* xr_ogf_get_child(xr_ogf* ogf, size_t index)
{
	return ogf->children().at(index);
}

XR_API size_t xr_ogf_child_count(xr_ogf* ogf)
{
	return ogf->children().size();
}

XR_API const char* xr_ogf_texture(xr_ogf* ogf)
{
	return ogf->texture().c_str();
}

XR_API const char* xr_ogf_shader(xr_ogf* ogf)
{
	return ogf->shader().c_str();
}

XR_API fvector3 xr_ogf_vb_get_p(xr_ogf* ogf, size_t index)
{
	return ogf->vb().p(index);
}

XR_API fvector2 xr_ogf_vb_get_tc(xr_ogf* ogf, size_t index)
{
	return ogf->vb().tc(index);
}

/*XR_API finfluence xr_ogf_vb_get_w(xr_ogf* ogf, size_t index)
{
	return ogf->vb().w(index);
}*/

XR_API size_t xr_ogf_vb_get_w_size(xr_ogf* ogf)
{
	return ogf->vb().w()->size();
}

XR_API uint32_t xr_ogf_vb_w_get_element_bone(xr_ogf* ogf, size_t fIndex, size_t fbIndex)
{
	finfluence xinfl = ogf->vb().w(fIndex);
	int num_xinfls = int(xinfl.size() & INT_MAX);
	if (num_xinfls == 1)
	{
		return xinfl[0].bone;
	}
	else
	{
		return xinfl[fbIndex].bone;
	}
}

XR_API float xr_ogf_vb_w_get_element_weight(xr_ogf* ogf, size_t fIndex, size_t fbIndex)
{
	finfluence xinfl = ogf->vb().w(fIndex);
	int num_xinfls = int(xinfl.size() & INT_MAX);
	if (num_xinfls == 1)
	{
		return xinfl[0].weight;
	}
	else
	{
		return xinfl[fbIndex].weight;
	}
}

XR_API void xr_ogf_vb_w_get_fbone_weight(xr_ogf* ogf, size_t fIndex, 
	uint32_t* b0, uint32_t* b1, uint32_t* b2, uint32_t* b3, float* w0, float* w1, float* w2, float* w3)
{
	finfluence xinfl = ogf->vb().w(fIndex);

	if (xinfl.size() == 1)
	{
		*b0 = xinfl[0].bone;

		*w0 = xinfl[0].weight;
	} 
	else if (xinfl.size() == 2)
	{
		*b0 = xinfl[0].bone;
		*b1 = xinfl[1].bone;

		*w0 = xinfl[0].weight;
		*w1 = xinfl[1].weight;
	}
	else if (xinfl.size() == 3)
	{
		*b0 = xinfl[0].bone;
		*b1 = xinfl[1].bone;
		*b2 = xinfl[2].bone;

		*w0 = xinfl[0].weight;
		*w1 = xinfl[1].weight;
		*w2 = xinfl[2].weight;
	}
	else if (xinfl.size() == 4)
	{
		*b0 = xinfl[0].bone;
		*b1 = xinfl[1].bone;
		*b2 = xinfl[2].bone;
		*b3 = xinfl[3].bone;

		*w0 = xinfl[0].weight;
		*w1 = xinfl[1].weight;
		*w2 = xinfl[2].weight;
		*w3 = xinfl[3].weight;
	}
}

XR_API size_t xr_ogf_vb_get_w_element_size(xr_ogf* ogf, size_t index)
{
	return ogf->vb().w(index).size();
}

XR_API fvector3 xr_ogf_vb_get_n(xr_ogf* ogf, size_t index)
{
	return ogf->vb().n(index);
}

XR_API size_t xr_ogf_ib_size(xr_ogf* ogf)
{
	return ogf->ib().size();
}

XR_API uint16_t xr_ogf_ib_get_indice(xr_ogf* ogf, size_t index)
{
	return ogf->ib()[index];
}

////////////////////////// OGF_V4 //////////////////////////

/*XR_API fbox xr_ogf_v4_bbox(xr_ogf_v4* ogf)
{
	return ogf->bbox();
}

XR_API fsphere xr_ogf_v4_bsphere(xr_ogf_v4* ogf)
{
	return ogf->bsphere();
}

XR_API size_t xr_ogf_v4_vb_size(xr_ogf_v4* ogf)
{
	return ogf->vb().size();
}

XR_API void xr_ogf_v4_vb_types(xr_ogf_v4* ogf, bool* has_points, bool* has_normals, bool* has_texcoords, bool* has_influences, bool* has_colors, bool* has_lightmaps)
{
	if (ogf != NULL)
	{
		*has_points = ogf->vb().has_points();
		*has_normals = ogf->vb().has_normals();
		*has_texcoords = ogf->vb().has_texcoords();
		*has_influences = ogf->vb().has_influences();
		*has_colors = ogf->vb().has_colors();
		*has_lightmaps = ogf->vb().has_lightmaps();
	}
}

XR_API void xr_ogf_v4_struct(xr_ogf_v4* ogf, bool* hierarchical, bool* skeletal, bool* animated, bool* progressive, bool* versioned)
{
	*hierarchical = ogf->hierarchical();
	*skeletal = ogf->skeletal();
	*animated = ogf->animated();
	*progressive = ogf->progressive();
	*versioned = ogf->versioned();
}

XR_API xr_ogf* xr_ogf_v4_get_child(xr_ogf_v4* ogf, size_t index)
{
	return ogf->children().at(index);
}

XR_API size_t xr_ogf_v4_child_count(xr_ogf_v4* ogf)
{
	return ogf->children().size();
}

XR_API const char* xr_ogf_v4_texture(xr_ogf_v4* ogf)
{
	return ogf->texture().c_str();
}

XR_API const char* xr_ogf_v4_shader(xr_ogf_v4* ogf)
{
	return ogf->shader().c_str();
}

XR_API fvector3 xr_ogf_v4_vb_get_p(xr_ogf_v4* ogf, size_t index)
{
	return ogf->vb().p(index);
}

XR_API fvector2 xr_ogf_v4_vb_get_tc(xr_ogf_v4* ogf, size_t index)
{
	return ogf->vb().tc(index);
}

XR_API finfluence xr_ogf_v4_vb_get_w(xr_ogf_v4* ogf, size_t index)
{
	return ogf->vb().w(index);
}

XR_API size_t xr_ogf_v4_vb_get_w_size(xr_ogf_v4* ogf)
{
	return ogf->vb().w()->size();
}

XR_API uint32_t xr_ogf_v4_vb_w_get_element_bone(xr_ogf_v4* ogf, size_t fIndex, size_t fbIndex)
{
	return ogf->vb().w(fIndex)[fbIndex].bone;
}

XR_API float xr_ogf_v4_vb_w_get_element_weight(xr_ogf_v4* ogf, size_t fIndex, size_t fbIndex)
{
	return ogf->vb().w(fIndex)[fbIndex].weight;
}

XR_API fbone_weight xr_ogf_v4_vb_w_get_fbone_weight(xr_ogf_v4* ogf, size_t fIndex, size_t fbIndex)
{
	return ogf->vb().w(fIndex)[fbIndex];
}

XR_API size_t xr_ogf_v4_vb_get_w_element_size(xr_ogf_v4* ogf, size_t index)
{
	return ogf->vb().w(index).size();
}

XR_API fvector3 xr_ogf_v4_vb_get_n(xr_ogf_v4* ogf, size_t index)
{
	return ogf->vb().n(index);
}

XR_API size_t xr_ogf_v4_ib_size(xr_ogf_v4* ogf)
{
	return ogf->ib().size();
}

XR_API uint16_t xr_ogf_v4_ib_get_indice(xr_ogf_v4* ogf, size_t index)
{
	return ogf->ib()[index];
}
*/
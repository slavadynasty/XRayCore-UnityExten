#pragma once
#include "xr_api.h"
#include "xr_object.h"
using namespace xray_re;

/* XR_OBJECT */
XR_API bool get_xr_object(const char* path, xr_object** out_object)
{
	xr_object* object = new xr_object();
	if (object->load_object(path))
	{
		*out_object = object;
		return true;
	}
	delete object;
	return false;
}

XR_API bool free_xr_object(xr_object* object)
{
	if (object != NULL)
	{
		delete object;
		return true;
	}
	return false;
}
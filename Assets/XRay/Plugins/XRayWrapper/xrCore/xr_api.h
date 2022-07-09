#pragma once

#define XR_API extern "C" __declspec(dllexport)
#define XR_API_CPLUS __declspec(dllexport)

#include "xr_api_object.h"
#include "xr_api_ogf.h"
#include "xr_api_mesh.h"
#include "xr_api_bone.h"
#include "CryptoXor.h"

XR_API const char* get_version() 
{
	return (const char*)("xrCore API (" __DATE__ " " __TIME__ ") alpha_5\ncredits: GSC, @std_handle, @redheadgektor");
}
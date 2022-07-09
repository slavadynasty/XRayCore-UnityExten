#pragma once
#define WIN32_LEAN_AND_MEAN

#include <windows.h>
#include "xr_api.h"

BOOL WINAPI DllMain(HINSTANCE hDllHandle, DWORD nReason, LPVOID Reserved)
{
    switch (nReason)
    {
        case DLL_PROCESS_ATTACH:
        DisableThreadLibraryCalls(hDllHandle);

        break;

    case DLL_PROCESS_DETACH:

        break;
    }

    return true;

}

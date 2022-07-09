#pragma once
#include "imgui/imgui.h"
#include "imgui/imgui_impl_dx9.h"
#include "imgui/imgui_impl_win32.h"
#include <d3d9.h>
#include <d3d9helper.h>
#include <d3d9types.h>
#include <d3d9caps.h>
#include <tchar.h>
#include "globals.h"
#include "rendering.h"

class d3d9_render 
{
public:
    LPDIRECT3D9              g_pD3D = NULL;
    LPDIRECT3DDEVICE9        g_pd3dDevice = NULL;
    D3DPRESENT_PARAMETERS    g_d3dpp = {};

    bool Init(HWND hwnd, WNDCLASSEX wc);

    bool CreateDeviceD3D(HWND hWnd);

    void CleanupDeviceD3D();

    void ResetDevice();

    LRESULT WndProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam);
};

static d3d9_render* m_d3d9;
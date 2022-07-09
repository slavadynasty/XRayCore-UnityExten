#pragma once

#include "imgui/imgui.h"
#include "imgui/imgui_impl_win32.h"
#include "imgui/imgui_impl_dx10.h"
#include <d3d10_1.h>
#include <d3d10.h>
#include <tchar.h>
#include "globals.h"
#include "rendering.h"

class d3d10_render
{
public:
    ID3D10Device* g_pd3dDevice = NULL;
    IDXGISwapChain* g_pSwapChain = NULL;
    ID3D10RenderTargetView* g_mainRenderTargetView = NULL;

    bool Init(HWND hwnd, WNDCLASSEX wc);

    bool CreateDeviceD3D(HWND hWnd);

    void CleanupDeviceD3D();

    void CreateRenderTarget();

    void CleanupRenderTarget();

    LRESULT WndProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam);
};

static d3d10_render* m_d3d10 = NULL;

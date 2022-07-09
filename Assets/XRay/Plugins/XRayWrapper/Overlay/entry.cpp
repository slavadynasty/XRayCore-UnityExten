#pragma once
#define WIN32_LEAN_AND_MEAN

#include <stdio.h>
#include "d3d9_render.h"

LRESULT WINAPI WndProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam);

int main(int, char** args)
{
    WNDCLASSEX wc = { sizeof(WNDCLASSEX), CS_CLASSDC, WndProc, 0L, 0L, GetModuleHandle(NULL), NULL, NULL, NULL, NULL, _T(WINDOW_CLASSNAME), NULL };
    ::RegisterClassEx(&wc);
    HWND hwnd = ::CreateWindow(wc.lpszClassName, _T(WINDOW_NAME), WS_OVERLAPPEDWINDOW, 100, 100, WINDOW_WIDTH, WINDOW_HEIGHT, NULL, NULL, wc.hInstance, NULL);

    printf("Starting...\n");

    xray_re::xr_ogf* ogf = xray_re::xr_ogf::load_ogf("C:\\Users\\Slava\\Desktop\\stalker_neutral_1.ogf");
    printf(ogf->userdata().c_str());

    m_d3d9 = new d3d9_render();
    m_d3d9->Init(hwnd, wc);
    return 0;
}

LRESULT WINAPI WndProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam) 
{
    if (m_d3d9 != NULL) 
    {
        return m_d3d9->WndProc(hWnd, msg, wParam, lParam);
    }

    return ::DefWindowProc(hWnd, msg, wParam, lParam);
}

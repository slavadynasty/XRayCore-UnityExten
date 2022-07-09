#pragma once
#include <stdio.h>
#include <vector>
#include <d3d9.h>

#include "imgui/imgui.h"
#include "imgui/DroidSans.h"
#include "file_browser.h"

//x-ray
#include "OGFViewer.h"
#include "ObjectViewer.h"

using namespace std;
using namespace xray_re;

static ImGui::FileBrowser fileDialog;

static void OnSelectedFile(const char* path, const char* filename, const char* format)
{
	printf("Selected file %s | Format: %s\n", path, format);

	if (strcmp(format, ".ogf") == 0)
	{
		ogfViewer->Load(filename, path);
	}
	if (strcmp(format, ".object") == 0)
	{
		objectViewer->Load(filename, path);
	}
}

static void RenderUI_Init(ImGuiIO* io)
{
	ImFont* font = io->Fonts->AddFontFromMemoryCompressedTTF(DroidSans_compressed_data, DroidSans_compressed_size, 18.0f, NULL);
	if (font == NULL)
	{
		printf("Font missing!\n");
	}
	else
	{
		printf("Font loaded! size = %d\n", DroidSans_compressed_size);
		ImGui::GetIO().FontDefault = font;
	}
}

static void RenderUI()
{
	if (ImGui::BeginMainMenuBar())
	{
		if (ImGui::BeginMenu("Tools"))
		{
			if (ImGui::MenuItem("OGF Viewer", ""))
			{
				fileDialog.SetTitle("Select .ogf file");
				fileDialog.SetTypeFilters({ ".ogf" });
				fileDialog.Open();
			}
			if (ImGui::MenuItem("Object Viewer", ""))
			{
				fileDialog.SetTitle("Select .object file");
				fileDialog.SetTypeFilters({ ".object" });
				fileDialog.Open();
			}
			ImGui::EndMenu();
		}
		ImGui::EndMainMenuBar();
	}
	fileDialog.Display();

	if (fileDialog.HasSelected())
	{
		OnSelectedFile(
			fileDialog.GetSelected().generic_string().c_str(),
			fileDialog.GetSelected().filename().generic_string().c_str(),
			fileDialog.GetSelected().extension().generic_string().c_str());
		fileDialog.ClearSelected();
	}

	ogfViewer->DrawUI();
	objectViewer->DrawUI();
}

static void Render(LPDIRECT3DDEVICE9 device)
{
	ogfViewer->DrawModel(device);
	objectViewer->DrawModel(device);
}
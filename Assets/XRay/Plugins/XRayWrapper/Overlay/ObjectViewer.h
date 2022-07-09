#pragma once
#include <d3d9.h>
#include "imgui/imgui.h"
#include "xr_object.h"
#include "xr_ogf.h"

using namespace xray_re;
using namespace std;

class ObjectViewer
{
private:
	void BuildBonesTree(const xr_bone* bone)
	{
		xr_bone_vec childrens = bone->children();
		if (!childrens.empty())
		{
			if (ImGui::TreeNode(bone->name().c_str()))
			{
				xr_bone_vec childrens = bone->children();

				for (int i = 0; i < childrens.size(); i++)
				{
					BuildBonesTree(childrens.at(i));
				}

				ImGui::TreePop();
			}
		}
		else
		{
			ImGui::BulletText(bone->name().c_str());
		}
	}

	void BuildMeshesTree()
	{
		for (int i = 0; i < _object->meshes().size(); i++) 
		{
			if (ImGui::TreeNode(_object->meshes().at(i)->name().c_str())) 
			{
				ImGui::Text("Faces: %i, Points: %i", _object->meshes().at(i)->faces().size(), _object->meshes().at(i)->points().size());
				
				if (ImGui::TreeNode("VMRefs")) 
				{
					for (int j = 0; j < _object->meshes().at(i)->vmrefs().size(); j++)
					{
						lw_vmref* v = &_object->meshes().at(i)->vmrefs().at(j);

						if (ImGui::TreeNode((const void*)j, "%i  (0x%p) (count : %i)", j, v, v->count))
						{
							for (int k = 0; k < 5; k++) 
							{
								ImGui::Text("vmap: %i  offset: %i", v->array[k].vmap, v->array[k].offset);
							}
							ImGui::TreePop();
						}
					}

					ImGui::TreePop();
				}
				ImGui::TreePop();
			}
		}
	}

	void BuildSurfaceTree()
	{
		for (int i = 0; i < _object->surfaces().size(); i++)
		{
			if (ImGui::TreeNode(_object->surfaces().at(i)->name().c_str()))
			{
				ImGui::Text("Texture: %s\nMaterial: %s", _object->surfaces().at(i)->texture().c_str(), _object->surfaces().at(i)->gamemtl().c_str());
				ImGui::TreePop();
			}
		}
	}

	void OnLoad()
	{
		fvector3 p = fvector3();
		fvector2 uv = fvector2();
		int point_index = 0;
		int total_points = 0;
		for (int i = 0; i < _object->meshes().size(); i++)
		{
			xr_mesh* mesh = _object->meshes().at(i);

			for (int j = 0; j < mesh->points().size(); j++) 
			{
				total_points++;
			}
		}

		VertexBuffer = new Vertex[total_points];

		for (int i = 0; i < _object->meshes().size(); i++)
		{
			xr_mesh* mesh = _object->meshes().at(i);

			for (int j = 0; j < mesh->points().size(); j++)
			{
				VertexBuffer[point_index].position = mesh->points().at(j);
				point_index++;
			}
		}

		printf("ObjectViewer -> Processed %i vertex\n", total_points);

		if (D3D_DEVICE->CreateVertexBuffer(sizeof(Vertex) * total_points, 0, D3D_FVF_XYZ, D3DPOOL_DEFAULT, &D3D_VERTEX_BUFFER, 0) == D3D_OK)
		{
			void* temp = NULL;
			D3D_VERTEX_BUFFER->Lock(0, sizeof(Vertex) * total_points, (void**)&temp, 0);
			memcpy(temp, VertexBuffer, sizeof(Vertex) * total_points);
			D3D_VERTEX_BUFFER->Unlock();
			printf("ObjectViewer -> Vertex buffer created sz=%d\n", sizeof(Vertex) * total_points);
		}
	}

	const char* file_name = NULL;
	xr_object* _object = NULL;

	struct Vertex
	{
		fvector3 position;
		//texture
		fvector2 uv;
	};

	//d3d
	LPDIRECT3DDEVICE9 D3D_DEVICE;
	LPDIRECT3DVERTEXBUFFER9 D3D_VERTEX_BUFFER;
	Vertex* VertexBuffer;

public:
	bool IsLoaded() { return _object != NULL; }
	bool Load(const char* filename, const char* path)
	{
		if (IsLoaded())
		{
			Reset();
		}
		_object = new xr_object();
		_object->load_object(path);
		if (_object != NULL)
		{
			file_name = filename;
			printf("ObjectViewer -> loaded %s  (0x%X)\n", path, &_object);

			OnLoad();
			return true;
		}
		else
		{
			printf("ObjectViewer -> bad file!\n");
			return false;
			Reset();
		}
	}

	void Reset()
	{
		file_name = NULL;
		printf("ObjectViewer -> reset...\n");
		_object->clear();
		delete _object;
		_object = 0;

		delete VertexBuffer;
		VertexBuffer = 0;
	}

	void DrawUI()
	{
		if (IsLoaded())
		{
			ImGui::Begin("Object Viewer", NULL);
			ImGui::BulletText(file_name);
			if (ImGui::TreeNode("Bones"))
			{
				if (_object->root_bone() != NULL) 
				{
					BuildBonesTree(_object->root_bone());
				}
				ImGui::TreePop();
			}

			if (ImGui::TreeNode("Meshes"))
			{
				BuildMeshesTree();
				ImGui::TreePop();
			}
			
			if (ImGui::TreeNode("Surface"))
			{
				BuildSurfaceTree();
				ImGui::TreePop();
			}

			if (ImGui::Button("Close"))
			{
				Reset();
			}
			ImGui::End();
		}
	}

	void DrawModel(LPDIRECT3DDEVICE9 device) 
	{
		D3D_DEVICE = device;
		
		if (_object != NULL) 
		{
			D3D_DEVICE->SetStreamSource(0, D3D_VERTEX_BUFFER, 0, 0);
			D3D_DEVICE->DrawPrimitive(D3DPT_TRIANGLESTRIP, 0, sizeof(Vertex));
		}
	}
};

static ObjectViewer* objectViewer = new ObjectViewer();


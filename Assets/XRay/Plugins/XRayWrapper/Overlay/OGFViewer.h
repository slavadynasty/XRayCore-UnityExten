#pragma once
#include <d3d9.h>
#include "imgui/imgui.h"
#include "rendering.h"
#include "ObjectViewer.h"
#include "xr_ogf.h"
#include "xr_ogf_format.h"
#include "xr_ogf_v3.h"
#include "xr_ogf_v4.h"

using namespace xray_re;
using namespace std;

class OGFViewer
{
private:
	const char* ogf2_chunk_id_to_string(ogf_chunk_id id)
	{
		switch (id)
		{
		case OGF_HEADER:return "OGF_HEADER"; break;
		case OGF2_TEXTURE:return "OGF2_TEXTURE"; break;
		case OGF2_TEXTURE_L:return "OGF2_TEXTURE_L"; break;
		case OGF2_BBOX:return "OGF2_BBOX"; break;
		case OGF2_VERTICES:return "OGF2_VERTICES"; break;
		case OGF2_INDICES:return "OGF2_INDICES"; break;
		case OGF2_VCONTAINER:return "OGF2_VCONTAINER"; break;
		}
		return "unknown";
	}

	const char* ogf3_chunk_id_to_string(ogf_chunk_id id)
	{
		switch (id)
		{
		case OGF_HEADER:return "OGF_HEADER"; break;
		case OGF3_TEXTURE:return "OGF3_TEXTURE"; break;
		case OGF3_TEXTURE_L:return "OGF3_TEXTURE_L"; break;
		case OGF3_CHILD_REFS:return "OGF3_CHILD_REFS"; break;
		case OGF3_BBOX:return "OGF3_BBOX"; break;
		case OGF3_VERTICES:return "OGF3_VERTICES"; break;
		case OGF3_INDICES:return "OGF3_INDICES"; break;
		case OGF3_LODDATA:return "OGF3_LODDATA"; break;
		case OGF3_VCONTAINER:return "OGF3_VCONTAINER"; break;
		case OGF3_BSPHERE:return "OGF3_BSPHERE"; break;
		case OGF3_CHILDREN_L:return "OGF3_CHILDREN_L"; break;
		case OGF3_S_BONE_NAMES:return "OGF3_S_BONE_NAMES"; break;
		case OGF3_S_MOTIONS:return "OGF3_S_MOTIONS"; break;
		case OGF3_DPATCH:return "OGF3_DPATCH"; break;
		case OGF3_LODS:return "OGF3_LODS"; break;
		case OGF3_CHILDREN:return "OGF3_CHILDREN"; break;
		case OGF3_S_SMPARAMS:return "OGF3_S_SMPARAMS"; break;
		case OGF3_ICONTAINER:return "OGF3_ICONTAINER"; break;
		case OGF3_S_SMPARAMS_NEW:return "OGF3_S_SMPARAMS_NEW"; break;
		case OGF3_LODDEF2:return "OGF3_LODDEF2"; break;
		case OGF3_TREEDEF2:return "OGF3_TREEDEF2"; break;
		case OGF3_S_IKDATA_0:return "OGF3_S_IKDATA_0"; break;
		case OGF3_S_USERDATA:return "OGF3_S_USERDATA"; break;
		case OGF3_S_IKDATA:return "OGF3_S_IKDATA"; break;
		case OGF3_S_MOTIONS_NEW:return "OGF3_S_MOTIONS_NEW"; break;
		case OGF3_S_DESC:return "OGF3_S_DESC"; break;
		case OGF3_S_IKDATA_2:return "OGF3_S_IKDATA_2"; break;
		case OGF3_S_MOTION_REFS:return "OGF3_S_MOTION_REFS"; break;
		}
		return "unknown";
	}

	const char* ogf4_chunk_id_to_string(ogf_chunk_id id)
	{
		switch (id)
		{
		case OGF_HEADER:return "OGF_HEADER"; break;
		case OGF4_TEXTURE:return "OGF4_TEXTURE"; break;
		case OGF4_VERTICES:return "OGF4_VERTICES"; break;
		case OGF4_INDICES:return "OGF4_INDICES"; break;
		case OGF4_P_MAP:return "OGF4_P_MAP"; break;
		case OGF4_SWIDATA:return "OGF4_SWIDATA"; break;
		case OGF4_VCONTAINER:return "OGF4_VCONTAINER"; break;
		case OGF4_ICONTAINER:return "OGF4_ICONTAINER"; break;
		case OGF4_CHILDREN:return "OGF4_CHILDREN"; break;
		case OGF4_CHILDREN_L:return "OGF4_CHILDREN_L"; break;
		case OGF4_LODDEF2:return "OGF4_LODDEF2"; break;
		case OGF4_TREEDEF2:return "OGF4_TREEDEF2"; break;
		case OGF4_S_BONE_NAMES:return "OGF4_S_BONE_NAMES"; break;
		case OGF4_S_MOTIONS:return "OGF4_S_MOTIONS"; break;
		case OGF4_S_SMPARAMS:return "OGF4_S_SMPARAMS"; break;
		case OGF4_S_IKDATA:return "OGF4_S_IKDATA"; break;
		case OGF4_S_USERDATA:return "OGF4_S_USERDATA"; break;
		case OGF4_S_DESC:return "OGF4_S_DESC"; break;
		case OGF4_S_MOTION_REFS_0:return "OGF4_S_MOTION_REFS_0"; break;
		case OGF4_SWICONTAINER:return "OGF4_SWICONTAINER"; break;
		case OGF4_GCONTAINER:return "OGF4_GCONTAINER"; break;
		case OGF4_FASTPATH:return "OGF4_FASTPATH"; break;
		case OGF4_S_LODS:return "OGF4_S_LODS"; break;
		case OGF4_S_MOTION_REFS_1:return "OGF4_S_MOTION_REFS_1"; break;
		}
		return "unknown";
	}

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
		for (int i = 0; i < _ogf->meshes().size(); i++)
		{
			if (ImGui::TreeNode(_ogf->meshes().at(i)->name().c_str()))
			{
				ImGui::Text("Faces: %i, Points: %i", _ogf->meshes().at(i)->faces().size(), _ogf->meshes().at(i)->points().size());
				ImGui::TreePop();
			}
		}
	}

	void BuildVertexBufferTree()
	{
		for (int i = 0; i < _ogf->vb().size(); i++)
		{
			ImGui::Text("X: %.3f Y: %.3f Z: %.3f", _ogf->vb().p(i).x, _ogf->vb().p(i).y, _ogf->vb().p(i).z);
		}
	}

	void OnLoad()
	{
		VertexBuffer = new Vertex[_ogf->vb().size()];
		fvector3 p = fvector3();
		fvector2 uv = fvector2();

		for (int i = 0; i < _ogf->vb().size(); i++)
		{
			p = _ogf->vb().p(i);
			if (_ogf->vb().has_texcoords())
			{
				fvector2 uv = _ogf->vb().tc(i);
			}
			VertexBuffer[i].position = p;
			VertexBuffer[i].uv = uv;
		}

		printf("OGFViewer -> Processed %i vertex\n", _ogf->vb().size());

		if (D3D_DEVICE->CreateVertexBuffer(sizeof(Vertex) * _ogf->vb().size(), 0, D3D_FVF_XYZ, D3DPOOL_DEFAULT, &D3D_VERTEX_BUFFER, 0) == D3D_OK)
		{
			void* temp = NULL;
			D3D_VERTEX_BUFFER->Lock(0, sizeof(Vertex) * _ogf->vb().size(), (void**)&temp, 0);
			memcpy(temp, VertexBuffer, sizeof(Vertex) * _ogf->vb().size());
			D3D_VERTEX_BUFFER->Unlock();
			printf("OGFViewer -> Vertex buffer created sz=%d\n", sizeof(VertexBuffer) * _ogf->vb().size());
		}
	}

	const char* file_name = NULL;
	xr_ogf* _ogf = NULL;

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
	bool IsLoaded() { return _ogf != NULL; }

	bool Load(const char* filename, const char* path)
	{
		if (IsLoaded())
		{
			Reset();
		}
		_ogf = xr_ogf::load_ogf(path);
		if (_ogf != NULL)
		{
			file_name = filename;
			printf("OGFViewer -> loaded %s  (0x%X)\n", path, &_ogf);

			switch (_ogf->version())
			{
			case OGF2_VERSION:
				printf("OGFViewer -> version 2\n");
				for (int i = 0; i < 12; i++)
				{
					if (_ogf->is_chunk_loaded(i))
					{
						printf("OGFViewer -> chunk %s loaded!\n", ogf2_chunk_id_to_string((ogf_chunk_id)i));
					}
				}
				break;
			case OGF3_VERSION:
				printf("OGFViewer -> version 3\n");
				for (int i = 0; i < 29; i++)
				{
					if (_ogf->is_chunk_loaded(i))
					{
						printf("OGFViewer -> chunk %s loaded!\n", ogf3_chunk_id_to_string((ogf_chunk_id)i));
					}
				}
				break;
			case OGF4_VERSION:
				printf("OGFViewer -> version 4\n");
				for (int i = 0; i < 24; i++)
				{
					if (_ogf->is_chunk_loaded(i))
					{
						printf("OGFViewer -> chunk %s loaded!\n", ogf4_chunk_id_to_string((ogf_chunk_id)i));
					}
				}
				break;
			default:
				printf("OGFViewer -> unknown version\n");
				break;
			}

			switch (_ogf->model_type())
			{
			case MT4_NORMAL:
				printf("OGFViewer -> Model Type MT4_NORMAL\n");
				break;
			case MT4_HIERRARHY:
				printf("OGFViewer -> Model Type MT4_HIERRARHY\n");
				break;
			case MT4_PROGRESSIVE:
				printf("OGFViewer -> Model Type MT4_PROGRESSIVE\n");
				break;
			case MT4_SKELETON_ANIM:
				printf("OGFViewer -> Model Type MT4_SKELETON_ANIM\n");
				break;
			case MT4_SKELETON_GEOMDEF_PM:
				printf("OGFViewer -> Model Type MT4_SKELETON_GEOMDEF_PM\n");
				break;
			case MT4_SKELETON_GEOMDEF_ST:
				printf("OGFViewer -> Model Type MT4_SKELETON_GEOMDEF_ST\n");
				break;
			case MT4_LOD:
				printf("OGFViewer -> Model Type MT4_LOD\n");
				break;
			case MT4_TREE_ST:
				printf("OGFViewer -> Model Type MT4_TREE_ST\n");
				break;
			case MT4_PARTICLE_EFFECT:
				printf("OGFViewer -> Model Type MT4_PARTICLE_EFFECT\n");
				break;
			case MT4_PARTICLE_GROUP:
				printf("OGFViewer -> Model Type MT4_PARTICLE_GROUP\n");
				break;
			case MT4_SKELETON_RIGID:
				printf("OGFViewer -> Model Type MT4_SKELETON_RIGID\n");
				break;
			case MT4_TREE_PM:
				printf("OGFViewer -> Model Type MT4_TREE_PM\n");
				break;
			default:
				printf("OGFViewer -> Unknown Model Type\n");
				break;
			}

			OnLoad();
			return true;
		}
		else
		{
			printf("OGFViewer -> bad file!\n");
			return false;
			Reset();
		}
	}

	void Reset()
	{
		file_name = NULL;
		printf("OGFViewer -> reset...\n");
		_ogf->clear();
		delete _ogf;
		_ogf = 0;

		delete VertexBuffer;
		VertexBuffer = 0;
	}

	void DrawUI()
	{
		if (IsLoaded())
		{
			ImGui::Begin("OGF Viewer", NULL);
			ImGui::BulletText(file_name);
			if (ImGui::TreeNode("Bones"))
			{
				if (_ogf->root_bone() != NULL)
				{
					BuildBonesTree(_ogf->root_bone());
				}
				else
				{
					ImGui::TextColored(ImVec4(1.0, 0, 0, 1), "No bones!");
				}
				ImGui::TreePop();
			}

			if (ImGui::TreeNode("Meshes"))
			{
				BuildMeshesTree();
				ImGui::TreePop();
			}

			if (ImGui::TreeNode("Vertex Buffer"))
			{
				BuildVertexBufferTree();
				ImGui::TreePop();
			}

			if (ImGui::Button("Close"))
			{
				Reset();
			}
			if (ImGui::Button("Convert to object"))
			{
				_ogf->to_object();
			}
			ImGui::End();
		}
	}

	void DrawModel(LPDIRECT3DDEVICE9 device)
	{
		D3D_DEVICE = device;

		if (_ogf != NULL)
		{
			D3D_DEVICE->SetStreamSource(0, D3D_VERTEX_BUFFER, 0, 0);
			D3D_DEVICE->DrawPrimitive(D3DPT_TRIANGLESTRIP, 0, sizeof(Vertex));
		}
	}
};

static OGFViewer* ogfViewer = new OGFViewer();


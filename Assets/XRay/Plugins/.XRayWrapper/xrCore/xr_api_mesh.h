#pragma once
#include "xr_api.h"
#include "xr_mesh.h"
using namespace xray_re;

/* XR_MESH */
XR_API size_t xr_object_meshes_count(xr_object* object) 
{
	return object->meshes().size();
}

XR_API xr_mesh* xr_object_get_mesh(xr_object* object, size_t index) 
{
	return object->meshes().at(index);
}

XR_API const char* xr_mesh_name(xr_mesh* mesh)
{
	return mesh->name().c_str();
}

XR_API fbox xr_mesh_bbox(xr_mesh* mesh)
{
	return mesh->bbox();
}

XR_API size_t xr_mesh_points_count(xr_mesh* mesh)
{
	return mesh->points().size();
}

XR_API fvector3 xr_mesh_get_point(xr_mesh* mesh, size_t index)
{
	return mesh->points().at(index);
}

XR_API size_t xr_mesh_faces_count(xr_mesh* mesh)
{
	return mesh->faces().size();
}

XR_API lw_face xr_mesh_get_face(xr_mesh* mesh, size_t index)
{
	return mesh->faces().at(index);
}

XR_API size_t xr_mesh_vmrefs_count(xr_mesh* mesh)
{
	return mesh->vmrefs().size();
}

XR_API lw_vmref* xr_mesh_get_vmref(xr_mesh* mesh, size_t index)
{
	return &mesh->vmrefs().at(index);
}

/* SURFMAPS & SURFACE */
XR_API size_t xr_mesh_surfmaps_count(xr_mesh* mesh)
{
	return mesh->surfmaps().size();
}

XR_API xr_surfmap* xr_mesh_get_surfmap(xr_mesh* mesh, size_t index)
{
	return mesh->surfmaps().at(index);
}

XR_API void xr_mesh_get_surfmap_surface(xr_surfmap* surfmap, const char** name, const char** gamemtl, const char** texture, const char** vmap)
{
	if (surfmap != NULL)
	{
		*name = surfmap->surface->name().c_str();
		*gamemtl = surfmap->surface->gamemtl().c_str();
		*texture = surfmap->surface->texture().c_str();
		*vmap = surfmap->surface->vmap().c_str();
	}
}

XR_API size_t xr_mesh_get_surfmap_faces_count(xr_surfmap* surfmap)
{
	return surfmap->faces.size();
}

XR_API uint32_t xr_mesh_get_surfmap_get_face(xr_surfmap* surfmap, size_t index)
{
	return surfmap->faces.at(index);
}

/* VMAPS */
XR_API size_t xr_mesh_vmaps_count(xr_mesh* mesh)
{
	return mesh->vmaps().size();
}

XR_API unsigned int xr_mesh_get_vmap_type(xr_mesh* mesh, size_t index)
{
	return mesh->vmaps().at(index)->type();
}

XR_API size_t xr_mesh_get_vmap_vertices_count(xr_mesh* mesh, size_t index)
{
	return mesh->vmaps().at(index)->vertices().size();
}

XR_API uint32_t xr_mesh_get_vmap_vertices(xr_mesh* mesh, size_t index, size_t vertices_index)
{
	return mesh->vmaps().at(index)->vertices().at(vertices_index);
}

XR_API size_t xr_mesh_get_vmap_uvs_count(xr_mesh* mesh, size_t index)
{
	return ((xr_uv_vmap*)(mesh->vmaps().at(index)))->uvs().size();
}

XR_API fvector2 xr_mesh_get_vmap_uvs(xr_mesh* mesh, size_t index, size_t uvs_index)
{
	return ((xr_uv_vmap*)(mesh->vmaps().at(index)))->uvs().at(uvs_index);
}

XR_API size_t xr_mesh_get_vmap_faces_count(xr_mesh* mesh, size_t index)
{
	return ((xr_face_uv_vmap*)(mesh->vmaps().at(index)))->faces().size();
}

XR_API uint32_t xr_mesh_get_vmap_faces(xr_mesh* mesh, size_t index, size_t faces_index)
{
	return ((xr_face_uv_vmap*)(mesh->vmaps().at(index)))->faces().at(faces_index);
}

XR_API size_t xr_mesh_get_vmap_weight_count(xr_mesh* mesh, size_t index)
{
	return ((xr_weight_vmap*)(mesh->vmaps().at(index)))->weights().size();
}

XR_API float xr_mesh_get_vmap_weight(xr_mesh* mesh, size_t index, size_t weight_index)
{
	return ((xr_weight_vmap*)(mesh->vmaps().at(index)))->weights().at(weight_index);
}
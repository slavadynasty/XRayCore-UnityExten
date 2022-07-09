using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.AssetImporters;
using XRay.Core;
using System;

[ScriptedImporter(0, "object")]
public class XRObjectImport : ScriptedImporter
{
    public override void OnImportAsset(AssetImportContext ctx)
    {
        if (xr_object.Load(ctx.assetPath, out xr_object _object))
        {
            InstantiateAsset(ref ctx, _object);
        }
        else
        {
            Debug.LogError("Error while loading object file...");
        }
    }

    private void InstantiateAsset(ref AssetImportContext ctx, xr_object obj)
    {
        GameObject gameObj = new GameObject(obj.root_bone.name);

        ctx.AddObjectToAsset(gameObj.name, gameObj);
        ctx.SetMainObject(gameObj);

        Mesh[] objMeshes = new Mesh[obj.meshes.Length];
        for (int i = 0; i < obj.meshes.Length; i++)
        {
            objMeshes[i] = new Mesh();
            objMeshes[i].name = obj.meshes[i].name;
            objMeshes[i].indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            objMeshes[i].SetVertices(obj.meshes[i].points.ToVector3Array());
            //objMeshes[i].SetUVs(0, obj.meshes[i].uvs.m_uvs.ToVector2Array());
            objMeshes[i].SetIndices(obj.meshes[i].faces.VFaceToIntArray(), MeshTopology.Triangles, 0);
            objMeshes[i].RecalculateNormals();

            List<Vector3> uvs = new List<Vector3>();
            for (int j = 0; j < 1; j++)
            {
                for (int k = 0; k < obj.meshes[i].uvs[j].m_uvs.Length; k++)
                {
                    uvs.Add(obj.meshes[i].uvs[j].m_uvs[k].ToVector2());
                }
            }

            objMeshes[i].SetUVs(0, uvs);

            /*CombineInstance[] meshCombine = new CombineInstance[obj.meshes[i].surfmaps.Length];
            for (int j = 0; j < obj.meshes[i].surfmaps.Length; j++)
            {
                Vector3[] verts = new Vector3[obj.meshes[i].surfmaps[j].faces.Length];
                Array.Copy(obj.meshes[i].points.ToVector3Array(), 0, verts, 0, verts.Length);

                Vector2[] uvs = new Vector2[obj.meshes[i].surfmaps[j].faces.Length];
                Array.Copy(obj.meshes[i].uvs.m_uvs.ToVector2Array(), 0, uvs, 0, uvs.Length);

                Mesh mesh = new Mesh();
                mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
                mesh.SetVertices(verts);
                mesh.SetUVs(0, uvs);

            }*/
        }

        if (obj.meshes.Length == 1)
        {
            MeshRenderer mr = gameObj.AddComponent<MeshRenderer>();
            mr.sharedMaterials = new Material[obj.meshes[0].surfmaps.Length];

            for (int s = 0; s < obj.meshes[0].surfmaps.Length; s++)
            {
                Material mat = new Material(Shader.Find("Standard"));
                mat.name = obj.meshes[0].surfmaps[s].surface.m_name;
                mr.sharedMaterials[s] = mat;

                ctx.AddObjectToAsset(mat.name, mat);
            }

            MeshFilter mf = gameObj.AddComponent<MeshFilter>();
            mf.sharedMesh = objMeshes[0];

            ctx.AddObjectToAsset(objMeshes[0].name, objMeshes[0]);
        }
        else if (obj.meshes.Length > 1)
        {
            for (int i = 0; i < objMeshes.Length; i++)
            {
                GameObject meshObject = new GameObject(objMeshes[i].name);
                meshObject.transform.parent = gameObj.transform;

                MeshRenderer mr = meshObject.AddComponent<MeshRenderer>();
                mr.sharedMaterials = new Material[obj.meshes[0].surfmaps.Length];

                for (int s = 0; s < obj.meshes[0].surfmaps.Length; s++)
                {
                    Material mat = new Material(Shader.Find("Standard"));
                    mat.name = obj.meshes[0].surfmaps[s].surface.m_name;
                    mr.sharedMaterials[s] = mat;

                    ctx.AddObjectToAsset(mat.name, mat);
                }

                MeshFilter mf = meshObject.AddComponent<MeshFilter>();
                mf.sharedMesh = objMeshes[i];

                ctx.AddObjectToAsset(objMeshes[i].name, objMeshes[i]);
                ctx.AddObjectToAsset(meshObject.name, meshObject);
            }
        }
    }
}

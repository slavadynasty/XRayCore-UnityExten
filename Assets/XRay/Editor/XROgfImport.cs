using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AssetImporters;
using UnityEditor.Build.Content;
using UnityEngine;
using XRay.Core;
using System.Linq;

[ScriptedImporter(0, "ogf")]
public class XROgfImport : ScriptedImporter
{
    private Transform modelTransform = null;
    private Transform modelRoot = null;
    private GameObject modelObject = null;
    private List<Transform> modelBones = new List<Transform>();

    public override void OnImportAsset(AssetImportContext ctx)
    {
        Debug.Log(Wrapper.get_version().GetString());
        if (xr_ogf.Load(ctx.assetPath, out xr_ogf _ogf))
        {
            InstantiateAsset(ref ctx, _ogf);
            Debug.Log("Succes");
        }
        else
        {
            Debug.LogError("Error while loading ogf file...");
        }
    }

    private void InstantiateAsset(ref AssetImportContext ctx, xr_ogf ogf)
    {
        string[] modelNameTemp = ctx.assetPath.Split("/");
        string modelName = modelNameTemp[^1];

        if (ogf.skeletal)
        {
            for (int i = 0; i < ogf.bones.Length; i++)
            {
                if (ogf.bones[i].is_root_bone)
                {
                    ImportBones(ogf, ref modelTransform, modelBones, modelName);
                    break;
                }
            }
        }
        
        if (ogf.hierarchical)
        {
            GameObject[] meshesObjects = new GameObject[ogf.children.Length];
            Mesh[] meshes = new Mesh[ogf.children.Length];
            SkinnedMeshRenderer[] smrs = new SkinnedMeshRenderer[ogf.children.Length];

            for (int i = 0; i < ogf.children.Length; i++)
            {
                xr_ogf child = ogf.children[i];

                string[] textureName = child.texture.Split(@"\");
                meshesObjects[i] = new GameObject(textureName[^1]);
                meshesObjects[i].transform.SetParent(modelTransform);
                smrs[i] = meshesObjects[i].AddComponent<SkinnedMeshRenderer>();

                meshes[i] = ImportMesh(child);
                meshes[i].name = textureName[^1]  + "_mesh";

                smrs[i].sharedMesh = meshes[i];
                smrs[i].sharedMaterial = new Material(Shader.Find("Standard"))
                {
                    name = textureName[^1] + "_mat"
                };
                
                ctx.AddObjectToAsset(smrs[i].sharedMaterial.name, smrs[i].sharedMaterial);

                smrs[i].rootBone = modelRoot;
                smrs[i].bones = modelBones.ToArray();

                List<BoneWeight> ww = new List<BoneWeight>();
                for (int j = 0; j < child.vb.w.Length; j++)
                {
                    if (!child.vb.has_influences) continue;
                    
                    finfluence xinfl = child.vb.w[j];
                    const int num_xinfls = 4;

                    BoneWeight bw = new BoneWeight();
                            
                    for (int k = 0; k != num_xinfls; k++)
                    {
                        int bone = (int)xinfl.fbw[k].bone;

                        switch (k)
                        {
                            case 0:
                                bw.boneIndex0 = bone;
                                break;
                            case 1:
                                bw.boneIndex1 = bone; 
                                break;
                            case 2: 
                                bw.boneIndex2 = bone; 
                                break;
                            case 3: 
                                bw.boneIndex3 = bone; 
                                break;
                        }
                    }
                            
                    for (int k = 0; k != num_xinfls; k++) 
                    {
                        float weight = xinfl.fbw[k].weight;

                        switch (k)
                        {
                            case 0:
                                bw.weight0 = weight;
                                break;
                            case 1:
                                bw.weight1 = weight;
                                break;
                            case 2:
                                bw.weight2 = weight;
                                break;
                            case 3:
                                bw.weight3 = weight;
                                break;
                        }
                    }
                            
                    ww.Add(bw);
                }

                Wrapper.xr_bone_calculate_bind(ogf.root_bone.pointer, smrs[i].bones[0].worldToLocalMatrix.ToFmatrix());
                ogf.ReimportBones();
                
                Matrix4x4[] bindposes = new Matrix4x4[ogf.bones.Length];

                //smrs[i].bones[0].transform.localPosition += new Vector3(0, 1, 0); // fix rig tr for SOC
                
                for (int j = 0; j < smrs[i].bones.Length; j++)
                {
                    if (ogf.bones[j].is_root_bone)
                        smrs[i].bones[j].rotation = ogf.bones[j].bind_xform.ToMatrix4x4().rotation;
                    else
                        smrs[i].bones[j].rotation = ogf.bones[j].bind_i_xform.ToMatrix4x4().rotation;
                }

                for (int j = 0; j < bindposes.Length; j++)
                    bindposes[j] = smrs[i].bones[j].worldToLocalMatrix * meshesObjects[i].transform.localToWorldMatrix;
                
                smrs[i].sharedMesh.boneWeights = ww.ToArray();
                smrs[i].sharedMesh.bindposes = bindposes;
                
                ctx.AddObjectToAsset(smrs[i].sharedMesh.name, smrs[i].sharedMesh);
            }
        }
        
        ctx.AddObjectToAsset(modelTransform.name, modelTransform.gameObject);
        ctx.SetMainObject(modelTransform.gameObject);
    }

    private void ImportBones(xr_ogf ogf, ref Transform root, List<Transform> modelBones, string rootName)
    {
        if (ogf.root_bone == null) return;
        
        root = new GameObject(rootName.Replace(".ogf", "")).transform;
        root.localPosition = ogf.root_bone.bind_offset.ToVector3();
        root.localEulerAngles = ogf.root_bone.bind_rotate.ToVector3();

        GameObject node = new GameObject(ogf.root_bone.name);
        modelRoot = node.transform;
        modelBones.Add(modelRoot);

        BuildTree(node.transform, ogf.root_bone, modelBones);

        node.transform.SetParent(root);
    }

    void BuildTree(Transform node, xr_bone bone, List<Transform> modelBones)
    {
        List<xr_bone> childrens = bone.children;

        if (childrens.Count > 0)
        {
            for (int i = 0; i < childrens.Count; i++)
            {
                GameObject child_node = new GameObject(childrens[i].name);
                child_node.transform.SetParent(node);
                child_node.transform.localPosition = childrens[i].bind_offset.ToVector3();
                //child_node.transform.localEulerAngles = childrens[i].bind_rotate.ToVector3() * Mathf.Rad2Deg; //check InstantiateAsset -> ogf hierarchical -> bind matrix
                modelBones.Add(child_node.transform);

                BuildTree(child_node.transform, childrens[i], modelBones);
            }
        }
    }

    private Mesh ImportMesh(xr_ogf ogf)
    {
        Mesh chunk = new Mesh();
        
        chunk.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;

        chunk.SetVertices(ogf.vb.p.ToVector3Array());

        int[] indices = new int[ogf.ib.m_indices.Length];
        for (int i = 0; i < ogf.ib.m_indices.Length;)
        {
            indices[i] = ogf.ib.m_indices[i++];
            indices[i] = ogf.ib.m_indices[i++];
            indices[i] = ogf.ib.m_indices[i++];
        }
        chunk.SetIndices(indices, MeshTopology.Triangles, 0);

        if (ogf.vb.has_texcoords)
            chunk.SetUVs(0, ogf.vb.tc.ToVector2Array());

        if(ogf.vb.has_normals)
            chunk.SetNormals(ogf.vb.n.ToVector3Array());

        chunk.bounds = new Bounds
        {
            min = ogf.bbox.min(),
            max = ogf.bbox.max()
        };

        return chunk;
    }
}

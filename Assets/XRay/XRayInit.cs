using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace XRay.Core
{
    public class XRayInit : MonoBehaviour
    {
        private string path = "C:\\Users\\Slava\\Desktop\\stalker_neutral_1.object";
        private bool xrCoreFound = false;

        private Transform root;
        private List<Transform> bones = new List<Transform>();
        private List<Matrix4x4> bindPoses = new List<Matrix4x4>();

        void Awake()
        {
            try
            {
                Debug.Log(Wrapper.get_version().GetString());
                xrCoreFound = true;
            }
            catch
            {
                xrCoreFound = false;
                Debug.LogError("xrCore not found!");
            }
            
            Load();
        }

        void BuildTree(Transform node, xr_bone bone)
        {
            List<xr_bone> childrens = bone.children;

            if (childrens.Count > 0)
            {
                for (int i = 0; i < childrens.Count; i++)
                {
                    GameObject child_node = new GameObject(childrens[i].name);
                    child_node.transform.SetParent(node);
                    child_node.transform.localPosition = childrens[i].bind_offset.ToVector3();
                    child_node.transform.localEulerAngles = childrens[i].bind_rotate.ToVector3() * 57.3f;
                    bindPoses.Add(child_node.transform.worldToLocalMatrix * root.worldToLocalMatrix);
                    bones.Add(child_node.transform);
                    
                    BuildTree(child_node.transform, childrens[i]);
                }
            }
        }


        xr_object b;
        [ContextMenu("Load Object")]
        private void Load()
        {
            //if (!xrCoreFound)
            //    return;

            if (Path.GetExtension(path).ToLower() == ".object")
            {
                if (xr_object.Load(path, out xr_object _object))
                {
                    b = _object;
                    if (_object.root_bone != null)
                    {
                        root = new GameObject(b.meshes[0].name).transform;
                        root.localPosition = b.root_bone.bind_offset.ToVector3();
                        root.localEulerAngles = b.root_bone.bind_rotate.ToVector3();

                        Debug.Log("Building transform tree...");
                        GameObject node = new GameObject(_object.root_bone.name);
                        BuildTree(
                            node.transform,
                            _object.root_bone);

                        node.transform.SetParent(root);

                        Mesh mesh = new Mesh();
                        mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
                        mesh.SetVertices(b.meshes[0].points.ToVector3Array());
                        //mesh.SetUVs(0, b.meshes[0].uvs.m_uvs.ToVector2Array());
                        mesh.SetIndices(b.meshes[0].faces.VFaceToIntArray(), MeshTopology.Triangles, 0);
                        mesh.RecalculateNormals();

                        mesh.bindposes = bindPoses.ToArray();

                        mesh.bounds = new Bounds()
                        {
                            max = b.meshes[0].bbox.max(),
                            min = b.meshes[0].bbox.min()
                        };

                        BoneWeight bw = new BoneWeight()
                        {
                            
                        };

                        print(mesh.vertexCount);

                        SkinnedMeshRenderer smr = root.gameObject.AddComponent<SkinnedMeshRenderer>();
                        smr.bones = bones.ToArray();
                        smr.sharedMesh = mesh;
                        smr.sharedMaterial = new Material(Shader.Find("Standard"));
                    }
                    Debug.Log("Complete!");
                }
                else
                {
                    Debug.LogError("Error while loading object file...");
                }
            }
            else if (Path.GetExtension(path).ToLower() == ".ogf")
            {
                if (xr_ogf.Load(path, out xr_ogf ogf))
                {
                    Debug.Log("OGF Version is " + ogf.version);
                    Debug.Log("OGF Model Type is " + ogf.model_type);
                }
                else
                {
                    Debug.LogError("Error while loading ogf file...");
                }
            }
        }
    }
}

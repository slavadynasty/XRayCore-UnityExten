using System;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEngine;
using XRay.Core;

namespace XRay.Editor
{
    [ScriptedImporter(0, "omf")]
    public class XrOmfImport : ScriptedImporter
    {
        public xr_skl[] motions;
        
        public override void OnImportAsset(AssetImportContext ctx)
        {
            if (Wrapper.get_xr_ogf_omf(ctx.assetPath, out IntPtr ptr))
            {
                motions = new xr_skl[Wrapper.xr_ogf_motions_size(ptr)];
                for (int i = 0; i < motions.Length; i++)
                {
                    motions[i] = new xr_skl(Wrapper.xr_ogf_motions_get_skl(ptr, i));
                    //CreateAnimationClip(i);
                }
            }
        }
        
        private void CreateAnimationClip(int motIndex)
        {
            AnimationCurve curvePosX = new AnimationCurve();
            AnimationCurve curvePosY = new AnimationCurve();
            AnimationCurve curvePosZ = new AnimationCurve();
            
            curvePosX.keys = new Keyframe[motions[motIndex].bone_motions.Length];
            curvePosY.keys = new Keyframe[motions[motIndex].bone_motions.Length];
            curvePosZ.keys = new Keyframe[motions[motIndex].bone_motions.Length];
            
            for (int i = 0; i < motions[motIndex].bone_motions.Length; i++)
            {
                curvePosX.keys[i] = new Keyframe
                {
                    time = i,
                    value = motions[motIndex].bone_motions[i].t.x
                };

                curvePosY.keys[i] = new Keyframe
                {
                    time = i,
                    value = motions[motIndex].bone_motions[i].t.y
                };

                curvePosZ.keys[i] = new Keyframe
                {
                    time = i,
                    value = motions[motIndex].bone_motions[i].t.z
                };
            }

            for (int i = 0; i < motions[motIndex].bone_motions.Length; i++)
            {
                AnimationClip clip = new AnimationClip();
                clip.SetCurve(motions[motIndex].bone_motions[i].bname, typeof(Transform), "m_LocalPosition.x", curvePosX);
                clip.SetCurve(motions[motIndex].bone_motions[i].bname, typeof(Transform), "m_LocalPosition.y", curvePosY);
                clip.SetCurve(motions[motIndex].bone_motions[i].bname, typeof(Transform), "m_LocalPosition.z", curvePosZ);
            }
        }
    }
}
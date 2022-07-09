using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using XRay.Core;

namespace XRay.Editor
{
    public class XRMenu : EditorWindow
    {
        private string git = "github repo: XRayCore-UnityExten";
        private string giturl = "https://github.com/slavadynasty/XRayCore-UnityExten";
        
        [MenuItem("XRayCore/About")]
        private static void Init()
        {
            XRMenu window = (XRMenu)EditorWindow.GetWindow(typeof(XRMenu));
            window.titleContent = new GUIContent("About");
            window.maxSize = new Vector2(270, 100);
            window.minSize = new Vector2(270, 100);
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.BeginVertical();
            
            GUILayout.Label("XRay Core Unity Extension", EditorStyles.boldLabel);
            EditorGUILayout.Separator();
            
            GUILayout.Label(Wrapper.get_version().GetString());
            EditorGUILayout.Separator();

            if (GUILayout.Button(git))
            {
                Application.OpenURL(giturl);
            }
            
            GUILayout.EndVertical();
        }
    }
}

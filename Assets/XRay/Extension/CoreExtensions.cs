using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XRay.Core
{
    public static class CoreExtensions
    {
        public static Vector3[] ToVector3Array(this fvector3[] fvectorArray)
        {
            Vector3[] vArray = new Vector3[fvectorArray.Length];
            for (int i = 0; i < vArray.Length; i++) vArray[i] = fvectorArray[i].ToVector3();
            return vArray;
        }

        public static Vector2[] ToVector2Array(this fvector2[] fvectorArray)
        {
            Vector2[] vArray = new Vector2[fvectorArray.Length];
            for (int i = 0; i < vArray.Length; i++) vArray[i] = fvectorArray[i].ToVector2();
            return vArray;
        }

        public static int[] VFaceToIntArray(this lw_face[] faceArray)
        {
            List<int> faceList = new List<int>();
            for (int i = 0; i < faceArray.Length; i++)
            {
                faceList.Add(faceArray[i].v0);
                faceList.Add(faceArray[i].v1);
                faceList.Add(faceArray[i].v2);
            }

            return faceList.ToArray();
        }
    }
}

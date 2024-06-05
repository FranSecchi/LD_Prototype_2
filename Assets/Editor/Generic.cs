using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomEditor(typeof(MonoBehaviour), true)]
public class Generic : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MonoBehaviour targetMonoBehaviour = (MonoBehaviour)target;
        MethodInfo methodInfo = targetMonoBehaviour.GetType().GetMethod("PerformRaycastAndUpdatePosition", BindingFlags.Public | BindingFlags.Instance);

        if (methodInfo != null)
        {
            if (GUILayout.Button("Snap"))
            {
                methodInfo.Invoke(targetMonoBehaviour, null);
            }
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(targetMonoBehaviour);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimSpeedTest))]
public class AnimSpeedTestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        AnimSpeedTest target = (AnimSpeedTest)this.target;

        DrawDefaultInspector();

        if (GUILayout.Button("Reset"))
        {
            target.Reset();
        }
        
    }
}

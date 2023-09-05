using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(WorldBuilder))]
public class WorldBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        WorldBuilder worldBuilder = (WorldBuilder)target;

        if (GUILayout.Button("Build World"))
        {
            worldBuilder.BuildWorld();
        }

        if (GUILayout.Button("Clear World"))
        {
            worldBuilder.ClearTransform();
        }
    }
}

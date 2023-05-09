using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbstractMapGenerator),true)]
public class MapGeneratorEditor : Editor
{
    AbstractMapGenerator m_Generator;

    private void Awake()
    {
        
        m_Generator = (AbstractMapGenerator)target;

    }

    public override void OnInspectorGUI()
    {
        
        base.OnInspectorGUI();
        if (GUILayout.Button("Generate Map"))
        {
            m_Generator.GenerateMap();
        }
    }
}

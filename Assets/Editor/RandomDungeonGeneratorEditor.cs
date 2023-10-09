using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AbstractDungeongenerator), true)]

public class RandomDungeonGeneratorEditor : Editor
{
    AbstractDungeongenerator generator;
    private void Awake()
    {
        generator = (AbstractDungeongenerator)target;

    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Create Dungeon"))
        {
            generator.GenerateDungeon();
        }
    }
}

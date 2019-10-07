using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CollectionItemSpawner))]
[CanEditMultipleObjects]
public class CustomInspectorCollectionSpawner : Editor
{
    private CollectionItemSpawner collectionItemSpawner;

    private bool spawnInfoOpen = true;
    private bool itemInfoOpen;

    private void OnEnable()
    {
        collectionItemSpawner = (CollectionItemSpawner) target;
    }

    public override void OnInspectorGUI()
    {
        CollectionItemSpawner collectionItemSpawner = (CollectionItemSpawner) target;

        spawnInfoOpen = EditorGUILayout.BeginFoldoutHeaderGroup(spawnInfoOpen, "Spawn Info");

        if (spawnInfoOpen)
        {
            GUILayout.Label("Spawn Interval");
            //GUILayout.Label("Min Value");
            //collectionItemSpawner.minTime = EditorGUILayout.Slider(collectionItemSpawner.minTime, 0, collectionItemSpawner.maxTime);
            //GUILayout.Label("Max Value");
            //collectionItemSpawner.maxTime = EditorGUILayout.Slider(collectionItemSpawner.maxTime, collectionItemSpawner.minTime, 10);

            collectionItemSpawner.minTime = EditorGUILayout.FloatField("Min. Value:", collectionItemSpawner.minTime);
            collectionItemSpawner.maxTime = EditorGUILayout.FloatField("Max. Value", collectionItemSpawner.maxTime);

            EditorGUILayout.MinMaxSlider("Min/Max Slider", ref collectionItemSpawner.minTime, ref collectionItemSpawner.maxTime, 1, 10);
        }
    }
}

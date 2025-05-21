// using UnityEditor;
// using UnityEngine;
// using GameBase.Scripts.Gameplay.TempQuest;
//
// [CustomEditor(typeof(Quest))]
// public class QuestEditor : Editor
// {
//     SerializedProperty questTypeProperty;
//     SerializedProperty questTitleProperty;
//     SerializedProperty descriptionProperty;
//     
//     // Collection
//     SerializedProperty itemsToCollectProperty;
//     SerializedProperty itemPrefabProperty;
//     
//     // Combat
//     SerializedProperty enemiesToDefeatProperty;
//     SerializedProperty combatTimeLimitProperty;
//
//     SerializedProperty locationsToVisitProperty;
//     
//     void OnEnable()
//     {
//         questTypeProperty = serializedObject.FindProperty("questType");
//         questTitleProperty = serializedObject.FindProperty("questTitle");
//         descriptionProperty = serializedObject.FindProperty("description");
//  
//         itemsToCollectProperty = serializedObject.FindProperty("itemsToCollect");
//         
//         locationsToVisitProperty = serializedObject.FindProperty("locationToVisit");
//     }
//     
//     public override void OnInspectorGUI()
//     {
//         serializedObject.Update();
//         
//         EditorGUILayout.PropertyField(questTypeProperty);
//         EditorGUILayout.PropertyField(questTitleProperty);
//         EditorGUILayout.PropertyField(descriptionProperty);
//         
//         QuestType questType = (QuestType)questTypeProperty.enumValueIndex;
//         
//         EditorGUILayout.Space();
//         EditorGUILayout.LabelField("Специфичные параметры", EditorStyles.boldLabel);
//         
//         switch(questType)
//         {
//             case QuestType.Collection:
//                 EditorGUILayout.PropertyField(itemsToCollectProperty);
//                 break;
//         }
//         
//         serializedObject.ApplyModifiedProperties();
//     }
// }
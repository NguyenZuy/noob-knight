//using UnityEditor;
//using UnityEngine;
//using Zuy.NoobKnight.Animated;
//using Zuy.NoobKnight.Editor;

//namespace Zuy.NoobKnight.Editor
//{
//    [CustomEditor(typeof(AnimatedCollectionSO))]
//    public class AnimatedCollectionSOEditor : Editor
//    {
//        public override void OnInspectorGUI()
//        {
//            DrawDefaultInspector();

//            AnimatedCollectionSO myScriptableObject = (AnimatedCollectionSO)target;
//            if (GUILayout.Button("Update CSV"))
//            {
//                CSVToScriptableObject.UpdateCSV(myScriptableObject);
//            }
//        }
//    }
//}
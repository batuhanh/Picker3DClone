using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.EditorTools;

[CustomEditor(typeof(LevelCreatorObject))]

public class LevelCreatorEditor : Editor
{
    private LevelCreatorObject myTarget;
    private bool isExec = false;
    private string completedLevelStr;

    void OnSceneGUI()
    {
      
        myTarget = (LevelCreatorObject)target;
        if (!isExec)
        {
            completedLevelStr = PlayerPrefs.GetInt("Level", 0).ToString();
            isExec = true;
        }


        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        Handles.BeginGUI();
        {
            GUILayout.BeginArea(new Rect(10, 10, 600, 190), new GUIStyle("window"));
            {
                GUIStyle guiStyle = new GUIStyle("BoldLabel");
                guiStyle.fontSize = 15;
                GUILayout.BeginArea(new Rect(10, 10, 580, 80), new GUIStyle("box"));
                {
                    GUI.Label(new Rect(200, -30, 200, 100), "Current Level Manager", guiStyle);
                    GUILayout.Space(35);
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Completed Level Count: ", GUILayout.Width(150), GUILayout.Height(15));
                    completedLevelStr = EditorGUILayout.TextArea(completedLevelStr, EditorStyles.textArea, GUILayout.Width(50));
                    GUILayout.Space(35);
                    if (GUILayout.Button("Update Complated Level", GUILayout.Width(250)))
                    {
                        int newCompLevel = PlayerPrefs.GetInt("Level", 0);
                        int.TryParse(completedLevelStr,out newCompLevel);
                        PlayerPrefs.SetInt("Level", newCompLevel);
                    }
                    EditorGUILayout.EndHorizontal();
                }
                GUILayout.EndArea();
                GUILayout.BeginArea(new Rect(10, 100, 580, 80), new GUIStyle("box"));
                {
                    GUI.Label(new Rect(230, -30, 200, 100), "Level Creator", guiStyle);
                    GUILayout.Space(35);
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Total Level Count is: ", GUILayout.Width(150), GUILayout.Height(15));

                    GUILayout.Space(35);
                    if (GUILayout.Button("Create New Level", GUILayout.Width(150)))
                    {
                        myTarget.CreateNewLevel();
                    }
                    EditorGUILayout.EndHorizontal();
                }
                GUILayout.EndArea();


            }
            GUILayout.EndArea();
        }
        Handles.EndGUI();
    }
}

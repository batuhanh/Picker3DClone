using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.EditorTools;

[CustomEditor(typeof(LevelInfoManager))]
public class LevelDesigner : Editor
{
    private LevelInfoManager myTarget;
    private Vector2 scrollPosition = Vector2.zero;
    private int tmpStagesCount = 0;
    private int stagesCount = 0;
    private string stagesCountStr = "";
    private bool isExec = false;

    private List<string> spawnedBallCountTexts;
    private List<string> targetBallCountTexts;
    private List<string> platformLengthTexts;
    private List<Color> platformColors;

    void OnSceneGUI()
    {
        Scene scene = SceneManager.GetActiveScene();
       
        if (scene.name == "LevelEditorScene")
        {
            myTarget = (LevelInfoManager)target;
            if (!isExec)
            {
                stagesCount = myTarget.GetStagesCount();
                tmpStagesCount = stagesCount;

                spawnedBallCountTexts = new List<string>();
                targetBallCountTexts = new List<string>();
                platformLengthTexts = new List<string>();
                platformColors = new List<Color>();

                for (int i = 0; i < stagesCount; i++)
                {
                    Stage curStage = myTarget.GetStage(i);

                    spawnedBallCountTexts.Add(curStage.SpawnedBallCount.ToString());
                    targetBallCountTexts.Add(curStage.TargetBallCount.ToString());
                    platformLengthTexts.Add(curStage.PlatformLength.ToString());
                    platformColors.Add(curStage.PlatformColor);

                }
                isExec = true;
            }


            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
            Handles.BeginGUI();
            {
                GUILayout.BeginArea(new Rect(10, 10, 400, 400), new GUIStyle("window"));
                {
                    GUIStyle guiStyle = new GUIStyle("BoldLabel");
                    guiStyle.fontSize = 15;
                    GUILayout.Label(myTarget.gameObject.name, guiStyle, GUILayout.Height(20));

                    DisplayStageCountPanel();

                    DisplayStages();

                    DisplayButtons();

                }
                GUILayout.EndArea();
            }
            Handles.EndGUI();
        }
        
    }
    private void DisplayStageCountPanel()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Stage Count: ", GUILayout.Width(100), GUILayout.Height(15));

        stagesCountStr = tmpStagesCount.ToString();
        stagesCountStr = EditorGUILayout.TextArea(stagesCountStr, EditorStyles.textArea, GUILayout.Width(100));
        int.TryParse(stagesCountStr, out tmpStagesCount);
        
        if (GUILayout.Button("Update Stage Count", GUILayout.Width(150)))
        {
            myTarget.SetStagesCount(tmpStagesCount);
            if (tmpStagesCount < stagesCount)
            {
                for (int i = 0; i < stagesCount - tmpStagesCount; i++)
                {
                    spawnedBallCountTexts.RemoveAt(spawnedBallCountTexts.Count - 1);
                    targetBallCountTexts.RemoveAt(targetBallCountTexts.Count - 1);
                    platformColors.RemoveAt(platformColors.Count - 1);
                    platformLengthTexts.RemoveAt(platformLengthTexts.Count - 1);
                }
            }
            else if (tmpStagesCount > stagesCount)
            {
                for (int i = 0; i < tmpStagesCount - stagesCount; i++)
                {
                    spawnedBallCountTexts.Add("10");
                    targetBallCountTexts.Add("10");
                    platformColors.Add(Color.white);
                    platformLengthTexts.Add("1");
                }
            }
            stagesCount = tmpStagesCount;

        }
        EditorGUILayout.EndHorizontal();
    }
    private void DisplayStages()
    {
        scrollPosition = GUI.BeginScrollView(new Rect(10, 70, 380, 300), scrollPosition, new Rect(0, 0, 350, stagesCount * 180));

        for (int i = 0; i < stagesCount; i++)
        {

            GUILayout.BeginArea(new Rect(0, (i * 160), 380, 150), new GUIStyle("box"));
            {
                GUILayout.Label("Stage " + (i + 1).ToString(), new GUIStyle("BoldLabel"), GUILayout.Width(120), GUILayout.Height(15));
                GUILayout.Space(10);
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Spawned Ball Count: ", GUILayout.Width(120), GUILayout.Height(15));
                spawnedBallCountTexts[i] = EditorGUILayout.TextArea(spawnedBallCountTexts[i], EditorStyles.textArea, GUILayout.Width(50));
                GUILayout.Label("Target Ball Count: ", GUILayout.Width(110), GUILayout.Height(15));
                targetBallCountTexts[i] = EditorGUILayout.TextArea(targetBallCountTexts[i], EditorStyles.textArea, GUILayout.Width(50));
                EditorGUILayout.EndHorizontal();
                GUILayout.Space(10);
                platformColors[i] = EditorGUILayout.ColorField("Platform Color: ", platformColors[i], GUILayout.Width(300));
                GUILayout.Space(10);
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Platform Length: ", GUILayout.Width(110), GUILayout.Height(15));
                platformLengthTexts[i] = EditorGUILayout.TextArea(platformLengthTexts[i], EditorStyles.textArea, GUILayout.Width(50));
                EditorGUILayout.EndHorizontal();

            }
            GUILayout.EndArea();

        }
        GUI.EndScrollView();
    }
    private void DisplayButtons()
    {
        GUILayout.FlexibleSpace();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Delete Level", GUILayout.Height(30)))
        {
            string prefabPath = AssetDatabase.GetAssetPath(PrefabUtility.GetCorrespondingObjectFromOriginalSource(myTarget.gameObject));
            FileUtil.DeleteFileOrDirectory(prefabPath);
            AssetDatabase.Refresh();
            DestroyImmediate(myTarget.gameObject);


        }
        if (GUILayout.Button("Update Level", GUILayout.Height(30)))
        {
            for (int i = 0; i < platformColors.Count; i++)
            {
                int tmpInt = 0;
                Stage curStage = myTarget.GetStage(i);
                curStage.StageIndex = i;
                int.TryParse(spawnedBallCountTexts[i], out tmpInt);
                curStage.SpawnedBallCount = tmpInt;
                int.TryParse(targetBallCountTexts[i], out tmpInt);
                curStage.TargetBallCount = tmpInt;
                curStage.PlatformColor = platformColors[i];
                float tmpFloat = 0;
                float.TryParse(platformLengthTexts[i], out tmpFloat);
                curStage.PlatformLength = tmpFloat;
            }
            myTarget.UpdateStagesInfo();
            myTarget.SetupStartEndObjects();
            
            PrefabUtility.ApplyPrefabInstance(myTarget.gameObject, InteractionMode.AutomatedAction);
        }
        if (GUILayout.Button("Apply To Prefab", GUILayout.Height(30)))
        {
            PrefabUtility.ApplyPrefabInstance(myTarget.gameObject, InteractionMode.AutomatedAction);
        }
        EditorGUILayout.EndHorizontal();
    }
}
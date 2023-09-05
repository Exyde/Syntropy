using UnityEditor;
using UnityEngine;

public class DebugWindow : EditorWindow {

    [MenuItem("Tools/Debug Window")]
    public static void Init(){
        DebugWindow window = GetWindow<DebugWindow>("Debug Editor Menu");
    }

    private void OnGUI() {
        if (GUILayout.Button("Select Player")){
            Selection.activeGameObject = GameObject.FindGameObjectWithTag("Player");
        }

        if (GUILayout.Button("Multi-Selection Rename")){
            string baseName = Selection.gameObjects[0].name;
            for (int i =0; i < Selection.gameObjects.Length; i++){
                Selection.gameObjects[i].name = $"{baseName} - {i}";
            }
        }

        GUILayout.Space(20);
        GUI.enabled = Application.isPlaying; //Only play mode option

        if (GUILayout.Button("Game speed to normal")) Time.timeScale = 1;
        if (GUILayout.Button("Game speed ++")) Time.timeScale -= 0.25f;
        if (GUILayout.Button("Game speed --")) Time.timeScale += 0.25f;
    }

}

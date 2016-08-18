using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(GameBase))]
public class GameBaseEditor : Editor
{
    private GameBase _gameBase;

    // Персонажи
    private bool _charactersTrigger = false;

    void OnEnable()
    {
        _gameBase = (GameBase)target;
    }

    // Создание базы
    [MenuItem("Assets/Create/Игровая база")]
    static void CreateGameBaseAsset()
    {
        GameBase _base = ScriptableObject.CreateInstance<GameBase>();
        string _basePath = AssetDatabase.GenerateUniqueAssetPath("Assets/Base/GameBase.asset");

        AssetDatabase.CreateAsset(_base, _basePath);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = _base;
    }

    // Изменяем внешний вид в редакторе
    public override void OnInspectorGUI()
    {
        if(_charactersTrigger = EditorGUILayout.Foldout(_charactersTrigger, "Персонажи"))
        {
            EditorGUI.BeginChangeCheck();
            EditorGUI.indentLevel++;
            if(_gameBase.characters != null)
            {
                if(_gameBase.characters.Count > 0)
                {
                    for(int i = 0; i < _gameBase.characters.Count; i++)
                    {
                        

                        _gameBase.characters[i].id = EditorGUILayout.IntField("Id", _gameBase.characters[i].id);
                        _gameBase.characters[i].name = EditorGUILayout.TextField("Название", _gameBase.characters[i].name);
                        _gameBase.characters[i].model = (GameObject)EditorGUILayout.ObjectField("Префаб", _gameBase.characters[i].model, typeof(GameObject), true);
                        _gameBase.characters[i].price = EditorGUILayout.IntField("Стоимость", _gameBase.characters[i].price);
                        EditorGUILayout.Space();

                        if(GUILayout.Button("Удалить"))
                        {
                            _gameBase.characters.RemoveAt(i);
                        }

                        Line();
                    }
                }
            }

            if(GUILayout.Button("Добавить"))
            {
                _gameBase.characters.Add(new Character());
            }

            if(EditorGUI.EndChangeCheck())
            {
                Save();
            }
            EditorGUI.indentLevel--;
        }
        Line();
    }

    private void Save()
    {
        EditorUtility.SetDirty(_gameBase);
        EditorApplication.SaveAssets();
    }

    // Линия (для редактора)
    private void Line()
    {
        EditorGUILayout.Space();
        GUILayout.Box("", GUILayout.ExpandWidth(true), GUILayout.Height(1f));
        EditorGUILayout.Space();
    }
}
using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;

namespace com.AmazingFusion.LoveOrDeath.Editor
{
    public class CharacterActionsEditorWindow : EditorWindow
    {
        #region Constants

        public const string ENTITY_ASSETS_PATH = "Assets/_AmazingFusion/_LoveOrDeath/Resources/" + CharacterAction.ACTIONS_PATH + "{0}.asset";

        #endregion

        #region Skins

        static GUISkin _skin = null;
        static public GUISkin Skin
        {
            get
            {
                if (_skin == null)
                    _skin = Resources.Load<GUISkin>("GUISkins/EditorGUISkin");
                return _skin;
            }
        }

        static GUIStyle _tileBoxStyle;
        static public GUIStyle TileBoxStyle
        {
            get
            {
                if (_tileBoxStyle == null)
                    _tileBoxStyle = Skin.FindStyle("TileBox");
                return _tileBoxStyle;
            }
        }

        static GUIStyle _tileBackgroundBoxStyle;
        static public GUIStyle TileBackgroundBoxStyle
        {
            get
            {
                if (_tileBackgroundBoxStyle == null)
                    _tileBackgroundBoxStyle = Skin.FindStyle("TileBackgroundBox");
                return _tileBackgroundBoxStyle;
            }
        }

        static GUIStyle _tileEmptyBoxStyle;
        static public GUIStyle TileEmptyBoxStyle
        {
            get
            {
                if (_tileEmptyBoxStyle == null)
                    _tileEmptyBoxStyle = Skin.FindStyle("TileEmptyBox");
                return _tileEmptyBoxStyle;
            }
        }

        static GUIStyle _tileSelectedBoxStyle;
        static public GUIStyle TileSelectedBoxStyle
        {
            get
            {
                if (_tileSelectedBoxStyle == null)
                    _tileSelectedBoxStyle = Skin.FindStyle("TileSelectedBox");
                return _tileSelectedBoxStyle;
            }
        }

        static GUIStyle _titleLabelStyle = null;
        static public GUIStyle TitleLabelStyle
        {
            get
            {
                if (_titleLabelStyle == null)
                    _titleLabelStyle = Skin.FindStyle("TitleLabel");
                return _titleLabelStyle;
            }
        }

        static GUIStyle _okLabelStyle = null;
        static public GUIStyle OkLabelStyle
        {
            get
            {
                if (_okLabelStyle == null)
                    _okLabelStyle = Skin.FindStyle("OkLabel");
                return _okLabelStyle;
            }
        }

        static GUIStyle _errorLabelStyle;
        static public GUIStyle ErrorLabelStyle
        {
            get
            {
                if (_errorLabelStyle == null)
                    _errorLabelStyle = Skin.FindStyle("ErrorLabel");
                return _errorLabelStyle;
            }
        }

        #endregion

        #region Private Members

        //Dictionary<string, Dictionary<string, string>> _languagesDictionaries;

        Dictionary<CharacterAction, string> _entitiesOptionsDictionary = new Dictionary<CharacterAction, string>();

        bool _needRepaint = false;

        List<CharacterAction> _entities = new List<CharacterAction>();
        CharacterAction _currentEntity;

        float _top = 0;
        float _columnWidth;

        //string _localizationKey = "Building.Workshop.";
        string _entityName = "Character Action";
        string _entitiesName = "Character Actions";
        string _entityPath = "";

        #region Draw values

        Vector2 _scrollPosition;
        //List<Vector2> _randomOrder = new List<Vector2>();
        string _currentEntityName;
        string _newEntityKey;
        string[] _entitiesOptions;
        int _selectedEntityIndex = 0;
        //int _newEntityKey;
        string _optionsInfoLabelText;
        GUIStyle _optionsInfoLabelStyle;
        string _randomGeneratorInfoLabelText;
        GUIStyle _randomGeneratorInfoLabelStyle;

        #endregion

        #endregion

        [MenuItem("Love Or Death/Actions Editor", false, 1)]
        public static void ShowWindow()
        {
            //Show existing window instance. If one doesn't exist, make one.
            CharacterActionsEditorWindow window = EditorWindow.GetWindow<CharacterActionsEditorWindow>();
            window.Show();
        }

        void OnEnable()
        {
            titleContent.text = string.Format("{0} Editor", _entitiesName);
            //_languagesDictionaries = SmartLocalization.Editor.LanguageHandlerEditor.LoadAllLanguageFiles();
            UpdateEntities();
        }

        void OnGUI()
        {
            //if (_needRepaint) { 
            //    _needRepaint = false;
            //   return;
            //}
            _needRepaint = false;

            _top = 0;
            _columnWidth = position.width - 24;
            /*_firstColumnLeft = 12;
            _firstColumnWidth = BLOCK_SIZE * HomeBuildingModel.COLS + Skin.box.padding.left + Skin.box.padding.right;
            _secondColumnLeft = 12 + _firstColumnWidth + 12;
            _secondColumnWidth = position.width - _secondColumnLeft - 12;*/

            GUI.skin = Skin;

            if (EditorApplication.isCompiling)
            {
                GUILayout.Label("Compiling...");
                return;
            }

            SpaceGUI(12);
            TitleGUI();

            SpaceGUI(12);

            if (_needRepaint)
            {
                Repaint();
                return;
            }

            OptionsAreaGUI();

            if (_currentEntity != null)
            {
                //SpaceGUI(12);
                //LocalizationInspectorAreaGUI();

                SpaceGUI(12);
                EntityInspectorAreaGUI();
            }

        }

        void OptionsAreaGUI()
        {
            float height = 120f;
            float top = _top;

            GUILayout.BeginArea(new Rect(12, _top, _columnWidth, height), GUI.skin.box);
            _top += height;

            EditorGUILayout.LabelField("<b>Options</b>", Skin.label);

            EditorGUI.BeginChangeCheck();
            int selectedEntityIndex = EditorGUILayout.Popup(String.Format("Select {0}", _entityName), _selectedEntityIndex, _entitiesOptions, GUI.skin.button);  /* new GUILayoutOption[] { GUILayout.ExpandHeight }*/;
            if (EditorGUI.EndChangeCheck())
            {
                if (selectedEntityIndex > 0)
                {
                    SelectEntity(selectedEntityIndex);

                    _optionsInfoLabelStyle = OkLabelStyle;
                    _optionsInfoLabelText = String.Format("{0} loaded succesfully!", _entityName);
                    //inspectorInfoLabelText = "";
                    //levelGeneratorInfoLabelText = "";
                }
            }


            GUILayout.BeginHorizontal();
            _newEntityKey = EditorGUILayout.TextField(_entityName, _newEntityKey, Skin.textField);

            if (GUILayout.Button("Create"))
            {
                if (ExistsEntity(_newEntityKey))
                {
                    _optionsInfoLabelStyle = ErrorLabelStyle;
                    _optionsInfoLabelText = String.Format("A {0} already exists with this key", _entityName);
                    //inspectorInfoLabelText = "";
                    //levelGeneratorInfoLabelText = "";
                    return;
                }
                else {
                    CreateEntity(_newEntityKey);
                    return;
                }
            }
            GUILayout.EndHorizontal();

            if (!String.IsNullOrEmpty(_optionsInfoLabelText))
            {
                GUILayout.Space(6f);
                GUILayout.Label(_optionsInfoLabelText, _optionsInfoLabelStyle);
            }

            GUILayout.EndArea();
        }

        /*void LocalizationInspectorAreaGUI()
        {
            if (_currentEntity != null)
            {
                float height = 24 + _languagesDictionaries.Count * 100f;

                GUILayout.BeginArea(new Rect(12, _top, _columnWidth, height), GUI.skin.box);

                //float scrollHeight = Mathf.Max(0, position.height - _top - GUI.skin.box.padding.bottom - GUI.skin.box.padding.top - 12);
                //_scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.Height(scrollHeight), GUILayout.ExpandHeight(true));
                _top += height;

                EditorGUILayout.LabelField("<b>Localization</b>", Skin.label);
                //EditorGUILayout.LabelField(String.Format("<b>{0} Localization</b>", _entityName), Skin.label);

                foreach (KeyValuePair<string, Dictionary<string, string>> languageDictionary in _languagesDictionaries)
                {
                    string nameKey = String.Format("{0}{1}.Name", _localizationKey, _currentEntity.Key);
                    string entityName = languageDictionary.Value.ContainsKey(nameKey)
                            ? languageDictionary.Value[nameKey]
                            : String.Format("<color=red>Name not localized for \"{0}\". Create key \"{1}\" in Smart Localization</color>",
                                    nameKey, _currentEntity.Key);

                    string descriptionKey = String.Format("{0}{1}.Description", _localizationKey, _currentEntity.Key);
                    string entityDescription = languageDictionary.Value.ContainsKey(descriptionKey)
                            ? languageDictionary.Value[descriptionKey]
                            : String.Format("<color=red>Description not localized for \"{0}\". Create key \"{1}\" in Smart Localization</color>",
                                    _currentEntity.Key, descriptionKey);

                    GUILayout.Label(String.Format("<b><color=blue>Language: {0}</color></b>", languageDictionary.Key), Skin.label);
                    GUILayout.Label(String.Format("<b>Name</b>: {0}", entityName), Skin.label);
                    GUILayout.Label(String.Format("<b>Description</b>: {0}", entityDescription), Skin.label);
                }

                SpaceGUI(6);
                if (GUILayout.Button("Open Smart Localization"))
                {
                    SmartLocalization.Editor.SmartLocalizationWindow window = (SmartLocalization.Editor.SmartLocalizationWindow)EditorWindow.GetWindow(typeof(SmartLocalization.Editor.SmartLocalizationWindow));
                    window.Show();
                }

                GUILayout.EndArea();
            }
        }*/

        void EntityInspectorAreaGUI()
        {
            if (_currentEntity != null)
            {
                float height = position.height - _top - 12;

                GUILayout.BeginArea(new Rect(12, _top, _columnWidth, height), GUI.skin.box);

                float scrollHeight = Mathf.Max(0, position.height - _top - GUI.skin.box.padding.bottom - GUI.skin.box.padding.top - 12);
                _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUILayout.Height(scrollHeight), GUILayout.ExpandHeight(true));

                EditorGUILayout.LabelField("<b>Inspector</b>", Skin.label);
                //EditorGUILayout.LabelField(String.Format("<b>{0} Inspector</b>", _entityName), Skin.label);

                UnityEditor.Editor editor = UnityEditor.Editor.CreateEditor(_currentEntity);
                editor.DrawDefaultInspector();

                SpaceGUI(12);

                EditorGUILayout.EndScrollView();
                GUILayout.EndArea();
            }
        }

        void SpaceGUI(float space)
        {
            GUILayout.Space(space);
            _top += space;
        }

        void TitleGUI()
        {
            GUIContent content = new GUIContent(_currentEntity == null ? String.Format("New {0}", _entityName) : _currentEntityName);
            GUILayout.Label(content, TitleLabelStyle);

            Vector2 size = TitleLabelStyle.CalcSize(content);
            _top += size.y;
        }

        void SelectEntity(int selectedEntityIndex)
        {
            if (!EditorApplication.isCompiling && _currentEntity != null)
            {
                EditorUtility.SetDirty(_currentEntity);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            _selectedEntityIndex = selectedEntityIndex;
            _currentEntity = _entitiesOptionsDictionary.ElementAt(_selectedEntityIndex - 1).Key;

            //string nameKey = String.Format("{0}{1}.Name", _localizationKey, _currentEntity.Key);
            //_currentEntityName = _languagesDictionaries["en"].ContainsKey(nameKey) ? _languagesDictionaries["en"][nameKey] : _currentEntity.Key;
            _currentEntityName = _currentEntity.Key;

            //_randomOrder.Clear();

            //string nameKey = String.Format("{0}.Name", _currentEntity.Key);
            //_currentEntityName = _languagesDictionaries["en"].ContainsKey(nameKey) ? _languagesDictionaries["en"][nameKey] : _currentEntity.Key;

            //DrawCurrentLevel();
        }

        void CreateEntity(string entityKey)
        {
            if (!EditorApplication.isCompiling && _currentEntity != null)
            {
                EditorUtility.SetDirty(_currentEntity);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            _currentEntity = ScriptableObject.CreateInstance<CharacterAction>();
            _currentEntity.Key = entityKey;

            //string nameKey = String.Format("{0}.Name", _currentEntity.Key);
            //_currentCharacterName = _languagesDictionaries["en"].ContainsKey(nameKey) ? _languagesDictionaries["en"][nameKey] : _currentEntity.Key;

            //string nameKey = String.Format("{0}{1}.Name", _localizationKey, _currentEntity.Key);
            //_currentEntityName = _languagesDictionaries["en"].ContainsKey(nameKey) ? _languagesDictionaries["en"][nameKey] : _currentEntity.Key;
            _currentEntityName = _currentEntity.Key;

            //_randomOrder.Clear();

            if (_currentEntity != null && !EditorApplication.isCompiling)
            {
                AssetDatabase.CreateAsset(_currentEntity, String.Format(ENTITY_ASSETS_PATH, _currentEntity.Key));
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }

            //EditorUtility.FocusProjectWindow();
            //Selection.activeObject = newLevel;
            /*optionsInfoLabelStyle = OkLabelStyle;
            optionsInfoLabelText = String.Format("¡Nivel {0} creado correctamente!", currentLevel.id);
            inspectorInfoLabelText = "";
            levelGeneratorInfoLabelText = "";*/
            UpdateEntities();
        }

        bool ExistsEntity(string key)
        {
            return _entities.Where(e => e.Key == key).Count() > 0;
        }

        void UpdateEntities()
        {
            if (!EditorApplication.isCompiling)
            {
                _entities.Clear();
                _entities = CharacterAction.LoadAll().ToList<CharacterAction>();
                UpdateEntitiesOptions();
            }
        }

        void UpdateEntitiesOptions()
        {
            _entitiesOptions = new string[_entities.Count + 1];
            _entitiesOptions[0] = String.Format("-- Select a {0} --", _entityName);
            _entitiesOptionsDictionary.Clear();

            for (int i = 0; i < _entities.Count; ++i)
            {
                int optionsIndex = i + 1;

                //string nameKey = String.Format("{0}{1}.Name", _localizationKey, _entities[i].Key);
                //string entityName = _languagesDictionaries["en"].ContainsKey(nameKey) ? _languagesDictionaries["en"][nameKey] : _entities[i].Key;

                _entitiesOptionsDictionary.Add(_entities[i], _entities[i].Key);

                _entitiesOptions[optionsIndex] = _entities[i].Key;
                if (_currentEntity == _entities[i])
                {
                    _selectedEntityIndex = optionsIndex;
                }
            }
        }

        void OnDestroy()
        {
            if (!EditorApplication.isCompiling && _currentEntity != null)
            {
                EditorUtility.SetDirty(_currentEntity);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}
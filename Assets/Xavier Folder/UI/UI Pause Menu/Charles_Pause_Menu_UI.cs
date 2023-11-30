using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Charles_Pause_Menu_UI : EditorWindow
{
    [SerializeField]
    private VisualTreeAsset m_VisualTreeAsset = default;

    [MenuItem("Window/UI Toolkit/Charles_Pause_Menu_UI")]
    public static void ShowExample()
    {
        Charles_Pause_Menu_UI wnd = GetWindow<Charles_Pause_Menu_UI>();
        wnd.titleContent = new GUIContent("Charles_Pause_Menu_UI");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // VisualElements objects can contain other VisualElement following a tree hierarchy.
        VisualElement label = new Label("Hello World! From C#");
        root.Add(label);

        // Instantiate UXML
        VisualElement labelFromUXML = m_VisualTreeAsset.Instantiate();
        root.Add(labelFromUXML);
    }
}

using UnityEditor;
using UnityEngine;

public class DotProductEditor : CommonEditor, IUpdateSceneGUI
{
    public Vector3 m_v0;
    public Vector3 m_v1;
    public Vector3 m_relative;

    private SerializedObject obj;
    private SerializedProperty propV0;
    private SerializedProperty propV1;
    private SerializedProperty propRef;

    private GUIStyle guiStyle = new GUIStyle();


    [MenuItem("Tools/Dot Product")]
    public static void ShowWindow()
    {
        DotProductEditor window = (DotProductEditor)GetWindow(typeof(DotProductEditor), true, "Dot Product");
        window.Show();
    }


    private void OnEnable()
    {
        if(m_v0 == Vector3.zero && m_v1 == Vector3.zero)
        {
            m_v0 = new Vector3(0.0f, 1.0f, 0.0f);
            m_v1 = new Vector3(0.5f, 0.5f, 0.0f);
            m_relative = Vector3.zero;
        }

        obj = new SerializedObject(this);
        propV0 = obj.FindProperty("m_v0");
        propV1 = obj.FindProperty("m_v1");
        propRef = obj.FindProperty("m_relative");

        guiStyle.fontSize = 25;
        guiStyle.fontStyle = FontStyle.Bold;
        guiStyle.normal.textColor = Color.white;

        SceneView.duringSceneGui += SceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= SceneGUI;
    }

    private void OnGUI()
    {
        obj.Update();

        DrawBlockGUI("v0", propV0);
        DrawBlockGUI("v1", propV1);
        DrawBlockGUI("relative", propRef);

        if (obj.ApplyModifiedProperties())
        {
            SceneView.RepaintAll();
        }
    }

    void DrawLabel(Vector3 v0, Vector3 v1, Vector3 relativeV)
    {
        Handles.Label(relativeV, MathFunctions.DotProduct(v0, v1, relativeV).ToString("F1"), guiStyle);
        Handles.color = Color.black;

        Vector3 cLef = MathFunctions.WorldRotation(v0, relativeV, new Vector3(0f, 1f, 0f));
        Vector3 cRig = MathFunctions.WorldRotation(v0, relativeV, new Vector3(0f, -1f, 0f));

        Handles.DrawAAPolyLine(3f, v0, relativeV);
        Handles.DrawAAPolyLine(3f, v1, relativeV);
        Handles.DrawAAPolyLine(3f, relativeV, cLef);
        Handles.DrawAAPolyLine(3f, relativeV, cRig);
    }

    public void SceneGUI(SceneView view)
    {
        Handles.color = Color.red;
        Vector3 v0 = SetMovePoint(m_v0);
        Handles.color = Color.green;
        Vector3 v1 = SetMovePoint(m_v1);
        Handles.color = Color.white;
        Vector3 refV = SetMovePoint(m_relative);

        if(m_v0 != v0 || m_v1 != v1 || m_relative != refV)
        {
            m_v0 = v0;
            m_v1 = v1;
            m_relative = refV;

            Repaint();
        }

        DrawLabel(v0, v1, refV);
    }

    Vector3 SetMovePoint(Vector3 pos)
    {
        float size = HandleUtility.GetHandleSize(Vector3.zero) * 0.15f;
        return Handles.FreeMoveHandle(pos, size, Vector3.zero, Handles.SphereHandleCap);
    }
}

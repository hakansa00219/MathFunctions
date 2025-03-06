using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EulerAnglesEditor : CommonEditor, IUpdateSceneGUI
{
    [Range(-180, 180)]
    public float m_angleX = 0;
    [Range(-180, 180)]
    public float m_angleY = 0;
    [Range(-180, 180)]
    public float m_angleZ = 0;

    private SerializedObject obj;
    private SerializedProperty propAngleX;
    private SerializedProperty propAngleY;
    private SerializedProperty propAngleZ;


    private List<Vector3> circleX;
    private List<Vector3> circleY;
    private List<Vector3> circleZ;
    private List<Vector3> arrow;

    [MenuItem("Tools/Euler Angles")]
    public static void ShowWindow()
    {
        GetWindow(typeof(EulerAnglesEditor), true, "Euler Angles");
    }

    private void OnEnable()
    {
        obj = new SerializedObject(this);

        propAngleX = obj.FindProperty("m_angleX");
        propAngleY = obj.FindProperty("m_angleY");
        propAngleZ = obj.FindProperty("m_angleZ");

        SceneView.duringSceneGui += SceneGUI;   
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= SceneGUI;
    }

    private void OnGUI()
    {
        obj.Update();

        DrawBlockGUI("X", propAngleX);
        DrawBlockGUI("Y", propAngleY);
        DrawBlockGUI("Z", propAngleZ);

        if (obj.ApplyModifiedProperties())
        {
            SceneView.RepaintAll();
        }
    }

    public void SceneGUI(SceneView view)
    {
        circleY = new List<Vector3>()
        {
            new Vector3( 0.00f, 0f, -1.00f),
            new Vector3( 0.71f, 0f, -0.71f),
            new Vector3( 1.00f, 0f,  0.00f),
            new Vector3( 0.71f, 0f,  0.71f),
            new Vector3( 0.00f, 0f,  1.00f),
            new Vector3(-0.71f, 0f,  0.71f),
            new Vector3(-1.00f, 0f,  0.00f),
            new Vector3(-0.71f, 0f, -0.71f),
        };

        circleX = new List<Vector3>()
        {
            new Vector3( 0f,  1.00f,  0.00f) * 0.9f,
            new Vector3( 0f,  0.71f, -0.71f) * 0.9f,
            new Vector3( 0f,  0.00f, -1.00f) * 0.9f,
            new Vector3( 0f, -0.71f, -0.71f) * 0.9f,
            new Vector3( 0f, -1.00f,  0.00f) * 0.9f,
            new Vector3( 0f, -0.71f,  0.71f) * 0.9f,
            new Vector3( 0f,  0.00f,  1.00f) * 0.9f,
            new Vector3( 0f,  0.71f,  0.71f) * 0.9f,
        };

        circleZ = new List<Vector3>()
        {
            new Vector3( 0.00f,  1.00f, 0f) * 0.8f,
            new Vector3( 0.71f,  0.71f, 0f) * 0.8f,
            new Vector3( 1.00f,  0.00f, 0f) * 0.8f,
            new Vector3( 0.71f, -0.71f, 0f) * 0.8f,
            new Vector3( 0.00f, -1.00f, 0f) * 0.8f,
            new Vector3(-0.71f, -0.71f, 0f) * 0.8f,
            new Vector3(-1.00f,  0.00f, 0f) * 0.8f,
            new Vector3(-0.71f,  0.71f, 0f) * 0.8f,
        };

        float degreeY = -m_angleY * Mathf.PI / 180f;
        float degreeX = m_angleX * Mathf.PI / 180f;
        float degreeZ = m_angleZ * Mathf.PI / 180f;

        for (int i = 0; i < 8; i++)
        {
            circleY[i] = MathFunctions.GetPitch(degreeY) * circleY[i];
            Handles.color = Color.green;
            Handles.SphereHandleCap(0, circleY[i], Quaternion.identity, 0.05f, EventType.Repaint);

            circleX[i] = MathFunctions.GetPitch(degreeY) * (MathFunctions.GetRoll(degreeX) * circleX[i]);
            Handles.color = Color.red;
            Handles.SphereHandleCap(0, circleX[i], Quaternion.identity, 0.05f, EventType.Repaint);

            circleZ[i] = MathFunctions.GetPitch(degreeY) * (MathFunctions.GetRoll(degreeX) * (MathFunctions.GetYaw(degreeZ) * circleZ[i]));
            Handles.color = Color.cyan;
            Handles.SphereHandleCap(0, circleZ[i], Quaternion.identity, 0.05f, EventType.Repaint);

        }

        for (int i = 0; i < 8; i++)
        {
            Handles.color = Color.green;
            Handles.DrawAAPolyLine(circleY[i], circleY[(i + 1) % circleY.Count]);

            Handles.color = Color.red;
            Handles.DrawAAPolyLine(circleX[i], circleX[(i + 1) % circleX.Count]);

            Handles.color = Color.blue;
            Handles.DrawAAPolyLine(circleZ[i], circleZ[(i + 1) % circleZ.Count]);
        }

        arrow = new List<Vector3>
        {
            new Vector3( 0.20f, 0f, 0.00f) * 0.5f,
            new Vector3( 0.20f, 0f,-0.50f) * 0.5f,
            new Vector3( 0.35f, 0f,-0.50f) * 0.5f,
            new Vector3( 0.00f, 0f,-1.00f) * 0.5f,
            new Vector3(-0.35f, 0f,-0.50f) * 0.5f,
            new Vector3(-0.20f, 0f,-0.50f) * 0.5f,
            new Vector3(-0.20f, 0f, 0.00f) * 0.5f
        };

        for (int i = 0; i < arrow.Count; i++)
        {
            arrow[i] = MathFunctions.GetPitch(degreeY) * (MathFunctions.GetRoll(degreeX) * (MathFunctions.GetYaw(degreeZ) * arrow[i]));
        }
        for (int i = 0; i < arrow.Count; i++)
        {
            Handles.color = Color.white;
            Handles.DrawAAPolyLine(arrow[i], arrow[(i + 1) % arrow.Count]);
        }
    }

}

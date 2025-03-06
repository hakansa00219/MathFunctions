using UnityEngine;

public static class MathFunctions
{
    public static float DotProduct(Vector4 v1, Vector4 v2, Vector4 relativeVector)
    {
        Vector4 a = (v1 - relativeVector).normalized;
        Vector4 b = (v2 - relativeVector).normalized;

        return (a.x * b.x) + (a.y * b.y) + (a.z * b.z) + (a.w * b.w);
    }

    public static Vector3 WorldRotation(Vector3 p, Vector3 c, Vector3 pos)
    {
        Vector2 dir = (p - c).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion rot = Quaternion.AngleAxis(angle, Vector3.forward);

        return c + rot * pos;
    } 

    public static Vector3 CrossProduct(Vector3 p, Vector3 q)
    {
        float x = p.y * q.z - p.z * q.y;
        float y = p.z * q.x - p.x * q.z;
        float z = p.x * q.y - p.y * q.x;

        return new Vector3(x, y, z);
    }

    public static Vector3 CrossProductMatrix(Vector3 p, Vector3 q)
    {
        Matrix4x4 m = new Matrix4x4();

        m[0, 0] = 0;
        m[0, 1] = q.z;
        m[0, 2] = -q.y;

        m[1, 0] = -q.z;
        m[1, 1] = 0;
        m[1, 2] = q.x;

        m[2, 0] = q.y;
        m[2, 1] = -q.x;
        m[2, 2] = 0;

        return m * p;

    }

    public static Quaternion ScalarVector(Quaternion q1, Quaternion q2)
    {
        float s1 = q1.w;
        float s2 = q2.w;

        Vector3 v1 = new Vector3(q1.x, q1.y, q1.z);
        Vector3 v2 = new Vector3(q2.x, q2.y, q2.z);

        float s = s1 * s2 - Vector3.Dot(v1, v2);
        Vector3 v = s1 * v2 + s2 * v1 + Vector3.Cross(v1, v2);

        return new Quaternion(v.x, v.y, v.z, s);
    }

    public static Quaternion Conjugate(Quaternion q)
    {
        return new Quaternion(-q.x, -q.y, -q.z, q.w);
    }

    public static Quaternion Create(float angle, Vector3 axis)
    {
        float r = Mathf.Sin(angle / 2f);
        float s = Mathf.Cos(angle / 2f);
        Vector3 v = Vector3.Normalize(axis) * r;

        return new Quaternion(v.x, v.y, v.z, s);
    }

    public static Vector2 RotationMatrix2D(Vector2 p, float angle)
    {
        float a = angle * (Mathf.PI / 180);
        Matrix4x4 m = new Matrix4x4();

        m[0, 0] = Mathf.Cos(a);
        m[0, 1] = - Mathf.Sin(a);
        m[1, 0] = Mathf.Sin(a);
        m[1, 1] = Mathf.Cos(a);

        return m * p;
    }

    public static Matrix4x4 GetYaw(float angle)
    {
        float cosTheta = Mathf.Cos(angle);
        float sinTheta = Mathf.Sin(angle);

        Matrix4x4 m = new Matrix4x4();

        m[0, 0] = cosTheta;
        m[0, 1] = -sinTheta;
        m[0, 2] = 0;

        m[1, 0] = sinTheta;
        m[1, 1] = cosTheta;
        m[1, 2] = 0;

        m[2, 0] = 0;
        m[2, 1] = 0;
        m[2, 2] = 1;


        return m;
    }

    public static Matrix4x4 GetPitch(float angle)
    {
        float cosTheta = Mathf.Cos(angle);
        float sinTheta = Mathf.Sin(angle);

        Matrix4x4 m = new Matrix4x4();

        m[0, 0] = cosTheta;
        m[0, 1] = 0;
        m[0, 2] = -sinTheta;

        m[1, 0] = 0;
        m[1, 1] = 1;
        m[1, 2] = 0;

        m[2, 0] = sinTheta;
        m[2, 1] = 0;
        m[2, 2] = cosTheta;


        return m;
    }

    public static Matrix4x4 GetRoll(float angle)
    {
        float cosTheta = Mathf.Cos(angle);
        float sinTheta = Mathf.Sin(angle);

        Matrix4x4 m = new Matrix4x4();

        m[0, 0] = 1;
        m[0, 1] = 0;
        m[0, 2] = 0;

        m[1, 0] = 0;
        m[1, 1] = cosTheta;
        m[1, 2] = -sinTheta;

        m[2, 0] = 0;
        m[2, 1] = sinTheta;
        m[2, 2] = cosTheta;


        return m;
    }
}

using UnityEngine;

public class debug : MonoBehaviour
{
    public static void log_vec(string name, Vector3 vec)
    {
        Debug.Log(name + ": " + vec.x + ", " + vec.y + ", " + vec.z);
    }
}
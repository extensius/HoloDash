using UnityEngine;

public class hud_grid_handler : MonoBehaviour {

    public Transform widget;
    public float number_of_steps;
    public float height;
    public float verticleBuffer;

    // Use this for initialization
    void Start()
    {
        /*
        Transform t = Camera.main.gameObject.transform;
        float distance = 2f;
        number_of_steps = 5;
        float step_size = (Mathf.PI/2) / (number_of_steps - 1);//NOTE: Step size must be > 1
        for (int y = -1; y <= 1; y++)
        {
            for (float angle = -Mathf.PI/4; angle <= Mathf.PI/4 + step_size/2; angle += step_size)//The + step_size/2 is because of rounding errors
            {
                float x = distance * Mathf.Sin(angle);
                float z = distance * Mathf.Cos(angle);

                Vector3 posVec = new Vector3(x, y * (height + verticleBuffer), z);
                Instantiate(widget, posVec, Quaternion.LookRotation(posVec - t.position));
            }
        }
        */
    }
}

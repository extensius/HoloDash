using UnityEngine;
using System.Collections;

//using UnityEngine.SceneManagement;

public class widget_container_manager : MonoBehaviour {

    public float radius = 2;
    public float width = 0.48f;
    public float height = 0.27f;
    private float[] angleR = {-0.366519f, -0.122173f, 0.122173f, 0.366519f}; //Rad
    private float[] angleD = { -21, -7, 7, 21};//Deg
    public float verticle_buffer = 0.01f;

    public GameObject widget_Prefab;
    public GameObject widget_Placement_Grid;
    private widget_placement_grid_manager widget_placement_grid_manager_script;


    //    public GameObject[] widgets;
    public int index = 0;

    // Use this for initialization
    void Start () {
        Debug.Log("Widget_Container_Manager::Start");

        //Get access to the widget_placement_grid_manager script on the widget_placement_grid game object
        widget_placement_grid_manager_script = GameObject.Find("Widget_Placement_Grid").GetComponent<widget_placement_grid_manager>();

        widget_placement_grid_manager_script.test();

        //    public IEnumerator FadeTo(float aValue, float aTime)
        //        widget_placement_grid_manager_script.FadeTo2(0.0f, 1.0f);

//        widget_Placement_Grid.

//        float alpha = transform.GetComponent<Renderer>().material.color.a;
//        Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, 0.0f, 1.0f));
//        transform.GetComponent<Renderer>().material.color = newColor;


        /* * /

        float alpha = transform.GetComponent<Renderer>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 1.0f)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, 0.0f, t));
            transform.GetComponent<Renderer>().material.color = newColor;
            yield return null;
        }

        /* */


    }

    public void addIncWidget() {
        int row = index / 4;
        int col = index % 4;

        Debug.Log("Widget_Container_Manager::AddIncWidget");

        Vector3 positionVec = getNewWidgetVec3(row, col);
        Quaternion rotation = Quaternion.Euler(0, angleD[col], 0);

        GameObject insertedWidget = Instantiate(widget_Prefab, positionVec, rotation) as GameObject;
        insertedWidget.transform.parent = this.gameObject.transform;
        
        debug.log_vec("Index " + index +" :: " + countChildren(this.gameObject), positionVec);
        index++;
    }

    public void addRayCastWidget(GameObject focusedObject)
    {
        if (focusedObject != null)
        {
            Debug.Log("Widget_Container_Manager::AddRayCastWidget");
            Vector3 iw1v = focusedObject.transform.position;
            iw1v.z = 1.8f;
            Transform insertedWidget1 = Instantiate(widget_Prefab, iw1v, focusedObject.transform.rotation) as Transform;
            insertedWidget1.parent = this.gameObject.transform;


            Vector3 positionVec = getNewWidgetVec3(0, 2);
//            Quaternion rotation = Quaternion.identity;
//            rotation.SetEulerRotation(0, angleR[2], 0);//TODO: replace with no Depricated method...
            Quaternion rotation = Quaternion.Euler(0, angleD[2], 0);

            Transform insertedWidget = Instantiate(widget_Prefab, positionVec, rotation) as Transform;
            insertedWidget.parent = this.gameObject.transform;





            debug.log_vec("PosVec", positionVec);

        }

    }

    public void addWidget() {
        Debug.Log("Widget_Container_Manager::AddWidget");
        Vector3 positionVec = getNewWidgetVec3(0, 2);
        Quaternion rotation = Quaternion.Euler(0, angleD[2], 0);
        Transform insertedWidget = Instantiate(widget_Prefab, positionVec, rotation) as Transform;
        insertedWidget.parent = this.gameObject.transform;

        debug.log_vec("PosVec", positionVec);

        traverse(this.gameObject);

    }

    public Vector3 getNewWidgetVec3(int row, int col) {
        float x, y, z;
        x = radius * Mathf.Sin(angleR[col]);
        z = radius * Mathf.Cos(angleR[col]);
        y = (height + verticle_buffer) * (1 - row);
        Vector3 vec = new Vector3(x, y, z);//2+ is a tmp change...
        return vec;
    }

    public Transform snapTransform(Transform t) {
        int col = 0, row = 2;

        //Snap x
        if (t.position.x > .5){col = 3;}//Col 3
        else if (t.position.x > 0){col = 2;}//Col 2
        else if (t.position.x > -.5){col = 1;}//Col 1
        //else col is 0 which is default

        //Snap y
        if (t.position.y > .15) { row = 0; }//Row 2
        else if (t.position.y > -.15) { row = 1; }//Row 1
        //else row is 0 which is default

        //Return the snap_transform
        t.position = getNewWidgetVec3(row, col);
        t.rotation = Quaternion.Euler(0, angleD[col], 0);
        return t;
    }

    static void traverse(GameObject obj)
    {
        if (obj) { 
            Debug.Log(obj.name);
            foreach (Transform child in obj.transform)
            {
                traverse(child.gameObject);
            }
        }
    }
    static int countChildren(GameObject obj)
    {
        int children = 0;
        if (obj)
        {
            children++; //initial node is counted
            //            Debug.Log(obj.name);
            foreach (Transform child in obj.transform)
            {
                children += countChildren(child.gameObject);
            }
        }
        return children;
    }
}

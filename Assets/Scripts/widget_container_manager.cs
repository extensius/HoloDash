using UnityEngine;
using System.Collections;

//using UnityEngine.SceneManagement;

public class widget_container_manager : MonoBehaviour {

    public float radius = 2;
    public float width = 0.48f;
    public float height = 0.27f;
    private float[] angle = {-0.366519f, -0.122173f, 0.122173f, 0.366519f}; //Rad; Deg{ -21, -7, 7, 21};
    private float[] angleD = { -21, -7, 7, 21};
    public float verticle_buffer = 0.01f;

    public GameObject WidgetPrefab;
    public GameObject Widget_Placement_Grid;
    public GameObject Object_To_Insert;
    public Transform widget;

    public GameObject[] widgets;
    public int index = 0;

    // Use this for initialization
    void Start () {
        Debug.Log("Widget_Container_Manager::Start");

        /*

        Transform camera = Camera.main.gameObject.transform;
        Vector3 positionVec = getNewWidgetVec3(0, 3);
        Object newWidget = Instantiate(widget, positionVec, Quaternion.LookRotation(positionVec - camera.position));
        //this.transform.parent = yourParentObject;
        debug.log_vec("PosVec", positionVec);

        */

        //meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>();

        ///        Object_To_Insert = this.gameObject.GetComponentInChildren<GameObject>();
        //        GameObject wp = gameObject.GetComponentInParent("Widget_Placeholder") as GameObject;


        //HingeJoint hinge = gameObject.GetComponentInParent( "HingeJoint" ) as HingeJoint;



        //        traverse(Placed_Widgets);

        //        foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
        //        {
        //            Debug.Log(obj.name);
        //        }
    }

    public void addIncWidget() {
            int row = 0, col = 0;
            switch (index) {
                case 0:
                    row = 0;col = 0;
                    break;
                case 1:
                    row = 0; col = 1;
                    break;
                case 2:
                    row = 0; col = 2;
                    break;
                case 3:
                    row = 0; col = 3;
                    break;
                case 4:
                    row = 1; col = 0;
                    break;
                case 5:
                    row = 1; col = 1;
                    break;
                case 6:
                    row = 1; col = 2;
                    break;
                case 7:
                    row = 1; col = 3;
                    break;
                case 8:
                    row = 2; col = 0;
                    break;
                case 9:
                    row = 2; col = 1;
                    break;
                case 10:
                    row = 2; col = 2;
                    break;
                case 11:
                    row = 2; col = 3;
                    break;
            }

            Debug.Log("Widget_Container_Manager::AddIncWidget");
            Vector3 positionVec = getNewWidgetVec3(row, col);
            Quaternion rotation = Quaternion.Euler(0, angleD[col], 0);

        GameObject insertedWidget = Instantiate(WidgetPrefab, positionVec, rotation) as GameObject;
        insertedWidget.transform.parent = this.gameObject.transform;
        


        debug.log_vec("Index " + index +" :: " + countChildren(this.gameObject), positionVec);
        //        traverse(Parent);
        index++;
    }

    public void addRayCastWidget(GameObject focusedObject)
    {
        if (focusedObject != null)
        {
            Debug.Log("Widget_Container_Manager::AddRayCastWidget");
            Vector3 iw1v = focusedObject.transform.position;
            iw1v.z = 1.8f;
            Transform insertedWidget1 = Instantiate(widget, iw1v, focusedObject.transform.rotation) as Transform;
            insertedWidget1.parent = this.gameObject.transform;


            Vector3 positionVec = getNewWidgetVec3(0, 2);
            Quaternion rotation = Quaternion.identity;
            rotation.SetEulerRotation(0, angle[2], 0);//TODO: replace with no Depricated method...
            Transform insertedWidget = Instantiate(widget, positionVec, rotation) as Transform;
            insertedWidget.parent = this.gameObject.transform;





            debug.log_vec("PosVec", positionVec);

        }

    }

    public void addWidget() {
        Debug.Log("Widget_Container_Manager::AddWidget");
        Vector3 positionVec = getNewWidgetVec3(0, 2);
        Quaternion rotation = Quaternion.identity;
        rotation.SetEulerRotation(0, angle[2], 0);//TODO: replace with no Depricated method...
        Transform insertedWidget = Instantiate(widget, positionVec, rotation) as Transform;
        insertedWidget.parent = this.gameObject.transform;

        debug.log_vec("PosVec", positionVec);

        traverse(this.gameObject);


        /*
        Debug.Log("Widget_Container_Manager::AddWidget");
//        Transform widget = Object_To_Insert.transform;
        Transform camera = Camera.main.gameObject.transform;
        //        Vector3 positionVec = camera.position + 2 * camera.forward;
        Vector3 positionVec = getNewWidgetVec3(0, 2);
        //        Object newWidget = Instantiate(widget, positionVec, Quaternion.LookRotation(positionVec - camera.position));
 //       Quaternion rotation = new Quaternion(0, angle[2], 0, 0);
        Quaternion rotation = Quaternion.identity;
        rotation.SetEulerRotation(0, angle[2], 0);//TODO: replace with no Depricated method...
        //Object newWidget = Instantiate(widget, positionVec, rotation);
        Object newWidget = Instantiate(widget, positionVec, rotation);

        Transform insertedWidget = Instantiate(widget, positionVec, rotation) as Transform;
        insertedWidget.parent = Parent.transform;

        traverse(insertedWidget.parent.gameObject);

        debug.log_vec("PosVec", positionVec);

        traverse(Parent);
        */


        /*
        Debug.Log("Widget_Container_Manager::AddWidget");
        Transform widget = Object_To_Insert.transform;
        Transform camera = Camera.main.gameObject.transform;
        Vector3 positionVec = camera.position + 2 * camera.forward;
        Object newWidget = Instantiate(widget, positionVec, Quaternion.LookRotation(positionVec - camera.position));
        //this.transform.parent = yourParentObject;
        debug.log_vec("PosVec", positionVec);

        traverse(Placed_Widgets);

        */
    }

    public Vector3 getNewWidgetVec3(int row, int col) {
        float x, y, z;
        x = radius * Mathf.Sin(angle[col]);
        z = radius * Mathf.Cos(angle[col]);
        y = (height + verticle_buffer) * (1 - row);
        Vector3 vec = new Vector3(x, y, z);//2+ is a tmp change...
        return vec;
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

using UnityEngine;
using UnityEngine.UI;

public class widget_placeholder_manager : MonoBehaviour
{

    //    private MeshRenderer meshRenderer;
    private Text text;
    private string original_text;
    private Vector3 manipulationStartPosition;

    // Use this for initialization
    void Start()
    {
        // Grab the mesh renderer that's on the same object as this script.
        //        meshRenderer = this.gameObject.GetComponentInChildren<MeshRenderer>();
        text = this.gameObject.GetComponentInChildren<Text>();
        original_text = text.text;
    }

    // Called by GazeGestureManager when the user performs a Select gesture
    void OnSelect()
    {
        text.text = "Selected";
    }

    void OnFocusLoss()
    {
        text.text = original_text;
    }

    void OnDrop()
    {
        // Just do the same logic as a Select gesture.
        OnSelect();
    }

    void OnDelete()
    {
        //        Canvas canvas = this.GetComponentInChildren<Canvas>();
        //        GameObject.Destroy(canvas);

        GameObject.Destroy(this.gameObject);
    }

    void PerformManipulationStart(Vector3 position)
    {
        text.text = "Move started?";
        manipulationStartPosition = position;
    }

    void PerformManipulationCanceled(Vector3 position)
    {
        text.text = "Move Canceled";
    }

    void PerformManipulationCompleted(Transform position) {
        text.text = "Moving ended!";

//        if(transform.position.x > )


        //Snap to grid...

        /*
        Vector3 moveVector = Vector3.zero;
        //Calculate the moveVector as position - manipulationPreviousPosition.
        moveVector = position - manipulationStartPosition;
        //Update the manipulationPreviousPosition with the current position.
        manipulationStartPosition = position;
        //Increment this transform's position by the moveVector.
        transform.position += 4f * moveVector;

        Quaternion look_rotation = Quaternion.LookRotation(transform.position - camera.position);
        transform.rotation = Quaternion.Euler(0, look_rotation.eulerAngles.y, 0);
        */

    }

    void PerformManipulationUpdate(Vector3 position)
    {
        text.text = "Moving...?";
        if (global_input_manager.Instance.IsManipulating)
        {
            Transform camera = Camera.main.gameObject.transform;

            /* Version 3 */

            Vector3 moveVector = Vector3.zero;
            //Calculate the moveVector as position - manipulationPreviousPosition.
            moveVector = position - manipulationStartPosition;
            //Update the manipulationPreviousPosition with the current position.
            manipulationStartPosition = position;
            //Increment this transform's position by the moveVector.
            transform.position += 4f * moveVector;

            Quaternion look_rotation = Quaternion.LookRotation(transform.position - camera.position);
            transform.rotation = Quaternion.Euler(0, look_rotation.eulerAngles.y, 0);

            /* */

            /* Version 2 * /
            transform.position = camera.position + 2 * camera.forward;
            transform.rotation = Quaternion.LookRotation(transform.position - camera.position);
            /* */

            /* Version 1 * /
            Vector3 moveVector = Vector3.zero;
            //Calculate the moveVector as position - manipulationPreviousPosition.
            moveVector = position - manipulationStartPosition;
            //Update the manipulationPreviousPosition with the current position.
            manipulationStartPosition = position;
            //Increment this transform's position by the moveVector.
            transform.position += 4f * moveVector;
            transform.rotation = Quaternion.LookRotation(transform.position - camera.position);
            /* */
        }
    }
}
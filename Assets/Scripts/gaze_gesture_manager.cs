using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VR.WSA.Input;
using UnityEngine.Windows.Speech;

public class gaze_gesture_manager : MonoBehaviour
{
    public static gaze_gesture_manager Instance { get; private set; }

    // Represents the hologram that is currently being gazed at.
    public GameObject FocusedObject { get; private set; }

    // Manipulation gesture recognizer.
    public GestureRecognizer ManipulationRecognizer { get; private set; }
    public bool IsManipulating { get; private set; }
    public Vector3 ManipulationPosition { get; private set; }

    // Defines which function to call when a keyword is recognized.
    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    public Transform widget;

    // Use this for initialization
    void Start()
    {
        Instance = this;

        //Speech Rec
        keywords.Add("exit", () => { Application.Quit(); });
        keywords.Add("add grid", () => {
            Transform t = Camera.main.gameObject.transform;
            float distance = 2f;
            float number_of_steps = 5;
            float height = .3f;
            float verticleBuffer = .1f;
            float step_size = (Mathf.PI / 2) / (number_of_steps - 1);//NOTE: Step size must be > 1
            for (int y = -1; y <= 1; y++)
            {
                for (float angle = -Mathf.PI / 4; angle <= Mathf.PI / 4 + step_size / 2; angle += step_size)//The + step_size/2 is because of rounding errors
                {
                    float x = distance * Mathf.Sin(angle);
                    float z = distance * Mathf.Cos(angle);

                    Vector3 posVec = new Vector3(x + t.position.x, y * (height + verticleBuffer), z + t.position.z);
                    Instantiate(widget, posVec, Quaternion.LookRotation(posVec - t.position));
                }
            }
        });
        keywords.Add("remove all", () => { /* TODO: add */ });
        keywords.Add("remove", () => {
            if (FocusedObject != null) {
                FocusedObject.SendMessageUpwards("OnDelete");
            }
        });
        keywords.Add("add", () => {
            Transform camera = Camera.main.gameObject.transform;
            float distance = 2f;
            float angle = camera.rotation.y *2;
            float x = distance * Mathf.Sin(angle);
            float z = distance * Mathf.Cos(angle);
            Vector3 posVec = new Vector3(x + camera.position.x, camera.position.y, z + camera.position.z);
            Instantiate(widget, posVec, Quaternion.LookRotation(posVec - camera.position));
        });

        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizer_OnPhraseRecognized;
        keywordRecognizer.Start();


        // Instantiate the ManipulationRecognizer.
        ManipulationRecognizer = new GestureRecognizer();

        // Add the ManipulationTranslate GestureSetting to the ManipulationRecognizer's RecognizableGestures.
        ManipulationRecognizer.SetRecognizableGestures(GestureSettings.ManipulationTranslate);

        // Register for the Manipulation events on the ManipulationRecognizer.
        ManipulationRecognizer.ManipulationStartedEvent += ManipulationRecognizer_ManipulationStartedEvent;
        ManipulationRecognizer.ManipulationUpdatedEvent += ManipulationRecognizer_ManipulationUpdatedEvent;
        ManipulationRecognizer.ManipulationCompletedEvent += ManipulationRecognizer_ManipulationCompletedEvent;
        ManipulationRecognizer.ManipulationCanceledEvent += ManipulationRecognizer_ManipulationCanceledEvent;

        //start recognizer...
        ManipulationRecognizer.StartCapturingGestures();

        /*
        // Set up a GestureRecognizer to detect Select gestures.
        recognizer = new GestureRecognizer();
        recognizer.TappedEvent += (source, tapCount, ray) =>
        {
            // Send an OnSelect message to the focused object and its ancestors.
            if (FocusedObject != null)
            {
                FocusedObject.SendMessageUpwards("OnSelect");
            }
        };
        recognizer.StartCapturingGestures();
        */
    }


    // Update is called once per frame
    void Update()
    {
        // Figure out which hologram is focused this frame.
        GameObject oldFocusObject = FocusedObject;

        // Do a raycast into the world based on the user's
        // head position and orientation.
        var headPosition = Camera.main.transform.position;
        var gazeDirection = Camera.main.transform.forward;

        RaycastHit hitInfo;
        if (Physics.Raycast(headPosition, gazeDirection, out hitInfo))
        {
            // If the raycast hit a hologram, use that as the focused object.
            FocusedObject = hitInfo.collider.gameObject;
        }
        else
        {
            // If the raycast did not hit a hologram, clear the focused object.
            FocusedObject = null;
        }

        // If the focused object changed this frame,
        // start detecting fresh gestures again.
        if (FocusedObject != oldFocusObject)
        {
            if (oldFocusObject != null) {
                oldFocusObject.SendMessageUpwards("OnFocusLoss");
            }
//            recognizer.CancelGestures();
//            recognizer.StartCapturingGestures();
        }
    }

    void OnDestroy()
    {
        // Unregister?
        keywordRecognizer.OnPhraseRecognized -= KeywordRecognizer_OnPhraseRecognized;

        // Unregister the Manipulation events on the ManipulationRecognizer.
        ManipulationRecognizer.ManipulationStartedEvent -= ManipulationRecognizer_ManipulationStartedEvent;
        ManipulationRecognizer.ManipulationUpdatedEvent -= ManipulationRecognizer_ManipulationUpdatedEvent;
        ManipulationRecognizer.ManipulationCompletedEvent -= ManipulationRecognizer_ManipulationCompletedEvent;
        ManipulationRecognizer.ManipulationCanceledEvent -= ManipulationRecognizer_ManipulationCanceledEvent;
    }

    private void KeywordRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;
        // if the keyword recognized is in our dictionary, call that Action.
        if (keywords.TryGetValue(args.text, out keywordAction))                                     //<<<
        {
            keywordAction.Invoke();
//            text.text += "Request Recognized\n";
        }
    }


    private void ManipulationRecognizer_ManipulationStartedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
    {
        if (FocusedObject != null) {
            IsManipulating = true;
            ManipulationPosition = position;
            FocusedObject.SendMessageUpwards("PerformManipulationStart", position);
        }
    }

    private void ManipulationRecognizer_ManipulationUpdatedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
    {
        if (FocusedObject != null) {
            IsManipulating = true;
            ManipulationPosition = position;
            FocusedObject.SendMessageUpwards("PerformManipulationUpdate", position);
        }
    }

    private void ManipulationRecognizer_ManipulationCompletedEvent(InteractionSourceKind source, Vector3 position, Ray ray)
    {
        IsManipulating = false;
    }

    private void ManipulationRecognizer_ManipulationCanceledEvent(InteractionSourceKind source, Vector3 position, Ray ray)
    {
        IsManipulating = false;
    }

}
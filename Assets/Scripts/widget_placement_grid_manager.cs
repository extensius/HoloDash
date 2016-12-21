using UnityEngine;
using System.Collections;

public class widget_placement_grid_manager : MonoBehaviour {

    public GameObject widget_placement_grid_gameObject;

    // Use this for initialization
    void Start () {
	    
	}

/*
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.T))
        {
            StartCoroutine(FadeTo(0.0f, 1.0f));
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            StartCoroutine(FadeTo(1.0f, 1.0f));
        }
    }
*/

    public void test() {
        Debug.Log("Widget_Placement_Grid_Manager::Test");

    }

    public IEnumerator FadeTo2(float aValue, float aTime)
    {
        float alpha = widget_placement_grid_gameObject.transform.GetComponent<Renderer>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            widget_placement_grid_gameObject.transform.GetComponent<Renderer>().material.color = newColor;
            yield return null;
        }
    }

    public IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = transform.GetComponent<Renderer>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            transform.GetComponent<Renderer>().material.color = newColor;
            yield return null;
        }
    }
}

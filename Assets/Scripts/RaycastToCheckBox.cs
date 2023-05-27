using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastToCheckBox : MonoBehaviour
{
    LayerMask layerMask;
    [SerializeField] Light light;
    [SerializeField] private int raycastRange = 10;
    // Update is called once per frame
    void Update()
    {
        // The light color on standby
        light.color = Color.red;

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

		if (Physics.Raycast(ray, out hit, raycastRange))
		{
            if (hit.transform.gameObject.tag == "Box")
			{
                // When the ray hit the box code can written here
                // The light color that changes when ray hits a box
                light.color = Color.green;
			}
		}
    }
}

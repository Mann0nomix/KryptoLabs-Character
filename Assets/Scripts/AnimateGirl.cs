using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateGirl : MonoBehaviour {
    public GameObject magicLeft;
    public GameObject magicRight;
    public GameObject letter;
    public GameObject movie;

	// NOTE: You need a collider in order to use raycast on character
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                if (hit.transform.tag == "MagicGirl") {
                    Debug.Log("This is my girl");
                    StartJump();
                    StartMagic();
                } else { //for any other colliders in scene
                    Debug.Log("This isn't my girl");
                }
            }
        }
    }

    void StartMagic()
    {
        if (!magicLeft.activeInHierarchy) {
            magicLeft.SetActive(true);
            magicRight.SetActive(true);
            letter.SetActive(true);
        }
    }

    void StartJump()
    {
        transform.Translate(transform.position + new Vector3(0f, -4.4f, 4.0f));
    }
}

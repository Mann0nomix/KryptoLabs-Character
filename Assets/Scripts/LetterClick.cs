using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterClick : MonoBehaviour {
	public GameObject movie;
    // NOTE: You need a collider in order to use raycast on character
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                if (hit.transform.tag == "Letter") {
                    Debug.Log("This is a letter");
                    StartVideo();
                } else { //for any other colliders in scene
                    Debug.Log("This isn't a letter");
                }
            }
        }
    }

    void StartVideo()
    {

		if (!movie.activeInHierarchy) { //movie only loads once when it does not exist in scene
            AnimateGirl.VideoSetup(); //Dismiss Letters when movie starts
			Instantiate(movie);
        }
    }
}

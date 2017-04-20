using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class AnimateGirl : MonoBehaviour {
    public GameObject magicLeft;
    public GameObject magicRight;
    public GameObject letter;
    public GameObject movie;
    public char[] letters;

    private Animator anim;
    private bool landed = false;

    public static Action VideoSetup;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        letters = new char[26] {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'};
        //demonstrating use of deferred callback delegate / action
        VideoSetup += DespawnLetters;
        VideoSetup += DisableMagic;
    }
    // NOTE: You need a collider in order to use raycast on character
    void Update () {
        if (Input.GetMouseButtonDown(0) && !landed) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                if (hit.transform.tag == "MagicGirl") {
                    Debug.Log("This is my girl");
                    StartJump();
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
        }

        StartCoroutine(SpawnLetters());
    }

    void StartJump()
    {
        Vector3 target = new Vector3(0f, -0.4f, -4.0f);
        StartCoroutine(JumpCoroutine(target));

        //transform.Translate(transform.position + new Vector3(0f, -4.4f, 4.0f));
    }

    void DisableMagic()
    {
        magicLeft.SetActive(false);
        magicRight.SetActive(false);
		//disable girl for video
		gameObject.SetActive (false);
    }

    void DespawnLetters()
    {
        foreach(GameObject l in GameObject.FindGameObjectsWithTag("Letter")) {
            Destroy(l);
        }
    }
    IEnumerator SpawnLetters()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 26; i++) {
            GameObject current = Instantiate(letter);
            if (current.GetComponent<TextMesh>() != null) {
                current.GetComponent<TextMesh>().text = letters[i].ToString().ToUpper();
            }
            yield return new WaitForSeconds(.15f);
        }
    }

    IEnumerator JumpCoroutine(Vector3 target)
    {
        anim.Play("JUMP01B");
        float trajectoryRate = 0.25f;

        while (Vector3.Distance(transform.position, target) > 0.2f) {
            Vector3 currentPos = transform.position;
            currentPos.y += trajectoryRate;
            if(currentPos.y > 4.25) {
                trajectoryRate = -0.05f;
            }
            transform.position = Vector3.Lerp(currentPos, target, Time.deltaTime * 3);
            yield return null;
        }
        
        Debug.Log("Landed");
        landed = true;

        yield return new WaitForSeconds(1f);
        //Start Magic when the character is grounded

        StartMagic();
        anim.Play("WIN00");

        yield return new WaitForSeconds(.25f);

        Vector3 magicLeftTarget = new Vector3(-2, 6, 0);
        Vector3 magicRightTarget = new Vector3(2, 6, 0);
        while (Vector3.Distance(magicLeft.transform.position, magicLeftTarget) > 0) { //only need to check one side
            magicLeft.transform.position = Vector3.Lerp(magicLeft.transform.position, magicLeftTarget, Time.deltaTime * 5);
            magicRight.transform.position = Vector3.Lerp(magicRight.transform.position, magicRightTarget, Time.deltaTime * 5);
            yield return null;
        }
    }

    private void OnDestroy()
    {
        VideoSetup -= DespawnLetters;
        VideoSetup -= DisableMagic;
    }
}

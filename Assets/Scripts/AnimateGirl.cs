using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateGirl : MonoBehaviour {
    public GameObject magicLeft;
    public GameObject magicRight;
    public GameObject letter;
    public GameObject movie;

    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    // NOTE: You need a collider in order to use raycast on character
    void Update () {
        if (Input.GetMouseButtonDown(0)) {
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
            letter.SetActive(true);
        }
    }

    void StartJump()
    {
        Vector3 target = new Vector3(0f, -0.4f, -4.0f);
        StartCoroutine(JumpCoroutine(target));

        //transform.Translate(transform.position + new Vector3(0f, -4.4f, 4.0f));
    }

    IEnumerator JumpCoroutine(Vector3 target)
    {
        anim.Play("JUMP01B");
        float timeToStart = Time.time;
        float trajectoryRate = 0.25f;

        while (Vector3.Distance(transform.position, target) > 0.1f) {
            Vector3 currentPos = transform.position;
            currentPos.y += trajectoryRate;
            if(currentPos.y > 4.25) {
                trajectoryRate = -0.05f;
            }
            transform.position = Vector3.Lerp(currentPos, target, Time.deltaTime * 3);
            yield return null;
        }
        
        Debug.Log("Landed");
        
        yield return new WaitForSeconds(1f);
        //Start Magic when the character is grounded
        StartMagic();
        anim.Play("WIN00");
    }
}

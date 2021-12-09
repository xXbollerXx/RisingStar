using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
           
            ChangeState("SwordSwing1");
                
        }
    }

    void ChangeState(string newState)
    {
        anim.Play(newState);
    }

    private void OnCollisionEnter(Collision collision)
    {
       // Debug.Log(collision.gameObject.name);
    }

}

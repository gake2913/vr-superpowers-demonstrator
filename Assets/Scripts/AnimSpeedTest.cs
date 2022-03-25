using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimSpeedTest : MonoBehaviour
{

    [Range(-2, 2)] public float AnimationSpeed = 1;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("AnimSpeed", AnimationSpeed);
    }

    public void Reset()
    {
        //AnimationSpeed = 1;
        animator.SetTrigger("Reset");
    }
}

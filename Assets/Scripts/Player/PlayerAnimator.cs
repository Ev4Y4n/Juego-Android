using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }


    // Start is called before the first frame update
    void Start()
    {
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnable()
    {

        PlayerController.OnHurt += PlayerOnHurt;
    }

    public void OnDisable()
    {
        PlayerController.OnHurt -= PlayerOnHurt;
    }

    private void PlayerOnHurt(bool hasHurt)
    {
        if (hasHurt)
        {
            Time.timeScale = 0.5f;
        }
        else
        {
            Time.timeScale = 1;
        }
        anim.SetBool("hasHurt", hasHurt);
    }
}

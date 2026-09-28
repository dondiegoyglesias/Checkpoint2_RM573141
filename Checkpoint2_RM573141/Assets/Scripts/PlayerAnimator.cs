using UnityEngine;
using System.Collections;

public class PlayerAnimator : MonoBehaviour
{



    private Animator animator;
    private PlayerControls playerControls;

    void Awake()
    {

        animator = GetComponent<Animator>();
        playerControls = GetComponent<PlayerControls>();

    }

    void Start()
    {
        
    }


    void Update()
    {
        animator.SetInteger("RunMovement", playerControls.MoveValueX() + playerControls.MoveValueY());
        animator.SetInteger("JumpMovement", playerControls.JumpValue());

    }

    IEnumerator TurnRed()
    {
        /*colisionFruit = true;

        yield return new WaitForSeconds(tempo);



        colisionFruit = false;
        */
        animator.SetBool("colisionFruit", playerControls.ColisionFruit());
        animator.SetBool("colisionFruit", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("colisionFruit", false);
    }

}

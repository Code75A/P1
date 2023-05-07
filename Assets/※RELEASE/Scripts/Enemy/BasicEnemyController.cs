using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyController : MonoBehaviour
{
    private enum State
    {
        Idling,
        Walking,
        Knocked,
        Dead
    }

    private State CurrentState;

    [SerializeField]
    private float
        WallDistance,
        MoveSpeed,
        MaxHealth,
        KnockbackDuration;
    [SerializeField]
    private Transform
        WallCheck;
    [SerializeField]
    private LayerMask
        WhichisGround;
    [SerializeField]
    private Vector2 KnockbackSpeed;


    private GameObject thisguy;
    private Rigidbody2D thisrb;
    private Animator thisanim;

    private int Facing=1;//direction
    private int Damaged;

    private float CurrentHealth;
    private float KnockbackTime;

    private Vector2 Movement;

    private bool WallDetected;
    private bool PlayerDetected;
    private void Start()
    {
        thisguy = transform.Find("第一个转变者").gameObject;
        thisrb = thisguy.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        switch (CurrentState)
        {
            case State.Idling:
                UpdateIdlingState();
                break;
            case State.Walking:
                UpdateWalkingState();
                break;
            case State.Knocked:
                UpdateKnockedState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }

    }

    //---Walking---------------------------------------------
    private void EnterWalkingState()
    {

    }
    private void UpdateWalkingState()
    {
        WallDetected = Physics2D.Raycast(WallCheck.position, transform.right, WallDistance,WhichisGround);
        if (WallDetected)
        {
            Flip();
        }
        else
        {
            Movement.Set(MoveSpeed * Facing, thisrb.velocity.y);
            thisrb.velocity = Movement; 
        }
    }
    private void ExitWalkingState()
    {

    }
    //---Idling---------------------------------------------
    private void EnterIdlingState()
    {

    }
    private void UpdateIdlingState()
    {

    }
    private void ExitIdlingState()
    {

    }
    //---Knocked---------------------------------------------
    private void EnterKnockedState()
    {
        KnockbackTime = Time.time;
        Movement.Set(KnockbackSpeed.x * Damaged,KnockbackSpeed.y);
        thisrb.velocity = Movement;
        thisanim.SetBool("Knocked", true);
    }
    private void UpdateKnockedState()
    {
        if(Time.time>=KnockbackTime+KnockbackDuration)
        {
            SwitchState(State.Walking);
        }
    }
    private void ExitKnockedState()
    {
        thisanim.SetBool("Knocked", false);
    }
    //---Dead---------------------------------------------
    private void EnterDeadState()
    {

    }
    private void UpdateDeadState()
    {

    }
    private void ExitDeadState()
    {

    }
    //---OtherFunctions-------------------------------------
    private void Damage(float []AttackDetails)
    {
        CurrentHealth -= AttackDetails[0];//伤害

        if (AttackDetails[1] > thisguy.transform.position.x)
            Damaged = -1;
        else
            Damaged = 1;

        //hurt

        if (CurrentHealth > 0.0f)
            SwitchState(State.Knocked);
        else
            SwitchState(State.Dead);  
    }

    private void SwitchState(State state)
    {
        switch (CurrentState)
        {
            case State.Idling:
                ExitIdlingState();
                break;
            case State.Walking:
                ExitWalkingState();
                break;
            case State.Knocked:
                ExitKnockedState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        switch (state)
        {
            case State.Idling:
                EnterIdlingState();
                break;
            case State.Walking:
                EnterWalkingState();
                break;
            case State.Knocked:
                EnterKnockedState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        CurrentState = state;

    }
    private void Flip()
    {
        Facing *= -1;
        thisguy.transform.Rotate(0.0f, 180.0f, 0.0f);
    }

}

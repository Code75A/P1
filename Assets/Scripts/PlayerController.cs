using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private enum State
    {
        Idling,
        LightAttack,
        HeavyAttack,
        Reflect
    }
    private float timer = 0;
    private State CurrentState;

    float lightAttackWindUpTime = 0.5f;
    float lightAttackActiveTime = 0.5f;
    float lightAttackRecoveryTime = 0.25f;

    float heavyAttackWindUpTime = 0.5f;
    float heavyAttackActiveTime = 1f;
    float heavyAttackRecoveryTime = 1f;

    float reflectWindUpTime = 0.5f;
    float reflectActiveTime = 0.5f;
    float reflectRecoveryTime = 0.25f;

    public GameObject lightAttackArea;
    public GameObject heavyAttackArea;
    public GameObject reflectArea;
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        lightAttackArea.SetActive(false);
        heavyAttackArea.SetActive(false);
        reflectArea.SetActive(false);
        CurrentState = State.Idling;
        playerMovement = GetComponent<PlayerMovement>();//获取玩家身上的playerMovement
    }

    // Update is called once per frame
    void Update()
    {
        switch (CurrentState)
        {
            case State.Idling:
                UpdateIdlingState();
                break;
            case State.LightAttack:
                UpdateLightAttackState();
                break;
            case State.HeavyAttack:
                UpdateHeavyAttackState();
                break;
            case State.Reflect:
                UpdateReflectState();
                break;
        }

    }

    // Idling
    void EnterIdlingState()
    {

    }
    void UpdateIdlingState()
    {
        //检测键盘
        if (Input.GetKeyDown(KeyCode.J))
        {
            CurrentState = State.LightAttack;
            EnterLightAttackState();
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            CurrentState = State.HeavyAttack;
            EnterHeavyAttackState();
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            CurrentState = State.Reflect;
            EnterReflectState();
        }
    }
    void ExitIdlingState()
    {

    }

    // LightAttack

    void EnterLightAttackState()
    {
        timer = lightAttackWindUpTime + lightAttackActiveTime + lightAttackRecoveryTime;
    }
    void UpdateLightAttackState()
    {
        if (timer <= lightAttackRecoveryTime + lightAttackActiveTime)
        {
            if (timer >= lightAttackRecoveryTime)//攻击中
            {
                lightAttackArea.SetActive (true);
            }
            else //后摇
            {
                lightAttackArea.SetActive (false);
            }
        }
        else //前摇
        {
            //...
        }
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            CurrentState = State.Idling;
            ExitLightAttackState();
        }
    }
    void ExitLightAttackState()
    {
        //lightAttackArea.SetActive (false); 看实际是否需要
    }

    // HeavyAttack
    void EnterHeavyAttackState()
    {
        timer = heavyAttackWindUpTime + heavyAttackActiveTime + heavyAttackRecoveryTime;
    }
    void UpdateHeavyAttackState()
    {
        if(timer <= heavyAttackRecoveryTime + heavyAttackActiveTime)
        {
            if(timer >= heavyAttackRecoveryTime)//攻击中
            {
                heavyAttackArea.SetActive(true);
            }
            else //后摇
            {
                heavyAttackArea.SetActive(false);
            }
        }
        else //前摇
        {
            //...
        }
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            CurrentState = State.Idling;
            ExitHeavyAttackState();
        }
    }
    void ExitHeavyAttackState()
    {
        // heavyAttackArea.SetActive(false); 看实际是否需要
    }

    // Reflect
    void EnterReflectState()
    {
        timer = reflectWindUpTime + reflectActiveTime + reflectRecoveryTime;
    }
    void UpdateReflectState()
    {
        if(timer <= reflectRecoveryTime + reflectActiveTime)
        {
            if(timer >= reflectRecoveryTime)//盾反中
            {
                reflectArea.SetActive(true);
            }
            else //后摇
            {
                reflectArea.SetActive(false);
            }
        }
        else //前摇
        {
            //...
        }
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            CurrentState = State.Idling;
            ExitReflectState();
        }
    }
    void ExitReflectState()
    {
        // reflectArea.SetActive(false); 看实际是否需要
    }


}

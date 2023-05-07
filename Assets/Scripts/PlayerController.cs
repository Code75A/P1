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
        playerMovement = GetComponent<PlayerMovement>();//��ȡ������ϵ�playerMovement
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
        //������
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
            if (timer >= lightAttackRecoveryTime)//������
            {
                lightAttackArea.SetActive (true);
            }
            else //��ҡ
            {
                lightAttackArea.SetActive (false);
            }
        }
        else //ǰҡ
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
        //lightAttackArea.SetActive (false); ��ʵ���Ƿ���Ҫ
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
            if(timer >= heavyAttackRecoveryTime)//������
            {
                heavyAttackArea.SetActive(true);
            }
            else //��ҡ
            {
                heavyAttackArea.SetActive(false);
            }
        }
        else //ǰҡ
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
        // heavyAttackArea.SetActive(false); ��ʵ���Ƿ���Ҫ
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
            if(timer >= reflectRecoveryTime)//�ܷ���
            {
                reflectArea.SetActive(true);
            }
            else //��ҡ
            {
                reflectArea.SetActive(false);
            }
        }
        else //ǰҡ
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
        // reflectArea.SetActive(false); ��ʵ���Ƿ���Ҫ
    }


}

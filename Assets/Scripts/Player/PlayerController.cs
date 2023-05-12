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
    private WeaponController weaponCtrl;
    private AbstractWeapon weapon;
    private GameObject weaponArea;

    // Start is called before the first frame update
    void Start()
    {
        weaponCtrl = GetComponent<WeaponController>();
        weapon = weaponCtrl.GetWeapon();
        weaponArea = null;
        CurrentState = State.Idling;
    }
    public void setStatics()
    {

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
        //¼ì²â¼üÅÌ
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
        else if(Input.GetKeyDown(KeyCode.Z))
        {
            weaponCtrl.NextWeapon();
            weapon = weaponCtrl.GetWeapon();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            weaponCtrl.LastWeapon();
            weapon = weaponCtrl.GetWeapon();
        }
        else if(Input.GetKeyDown(KeyCode.V))
        {
            weaponCtrl.AddNewWeapon(2);
        }
    }
    void ExitIdlingState()
    {

    }

    // LightAttack

    void EnterLightAttackState()
    {
        timer = weapon.lightAttackWindUpTime + weapon.lightAttackActiveTime + weapon.lightAttackRecoveryTime;
    }
    void UpdateLightAttackState()
    {
        if (timer <= weapon.lightAttackRecoveryTime + weapon.lightAttackActiveTime)
        {
            if (timer >= weapon.lightAttackRecoveryTime)//¹¥»÷ÖÐ
            {
                if (weaponArea == null)
                {
                    // ÊµÀý»¯¹¥»÷·¶Î§
                    weaponArea = Instantiate((GameObject)Resources.Load(weapon.lightAttackAreaName));
                    weaponArea.transform.SetParent(transform);
                    weaponArea.transform.localPosition = new Vector3(0, 0, 0);
                }
            }
            else //ºóÒ¡
            {
                if(weaponArea != null)
                Destroy(weaponArea);
            }
        }
        else //Ç°Ò¡
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
        if (weaponArea != null)
            Destroy(weaponArea);
    }

    // HeavyAttack
    void EnterHeavyAttackState()
    {
        timer = weapon.heavyAttackWindUpTime + weapon.heavyAttackActiveTime + weapon.heavyAttackRecoveryTime;
    }
    void UpdateHeavyAttackState()
    {
        if(timer <= weapon.heavyAttackRecoveryTime + weapon.heavyAttackActiveTime)
        {
            if(timer >= weapon.heavyAttackRecoveryTime)//¹¥»÷ÖÐ
            {
                if(weaponArea == null)
                {
                    // ÊµÀý»¯¹¥»÷·¶Î§
                    weaponArea = Instantiate((GameObject)Resources.Load(weapon.heavyAttackAreaName));
                    weaponArea.transform.SetParent(transform);
                    weaponArea.transform.localPosition = new Vector3(0, 0, 0);
                }
            }
            else //ºóÒ¡
            {
                if (weaponArea != null)
                    Destroy(weaponArea);
            }
        }
        else //Ç°Ò¡
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
        if (weaponArea != null)
            Destroy(weaponArea);
    }

    // Reflect
    void EnterReflectState()
    {
        timer = weapon.reflectWindUpTime + weapon.reflectActiveTime + weapon.reflectRecoveryTime;
    }
    void UpdateReflectState()
    {
        if(timer <= weapon.reflectRecoveryTime + weapon.reflectActiveTime)
        {
            if(timer >= weapon.reflectRecoveryTime)//¶Ü·´ÖÐ
            {
                if(weaponArea == null)
                {
                    // ÊµÀý»¯¹¥»÷·¶Î§
                    weaponArea = Instantiate((GameObject)Resources.Load(weapon.reflectAreaName));
                    weaponArea.transform.SetParent(transform);
                    weaponArea.transform.localPosition = new Vector3(0, 0, 0);
                }
            }
            else //ºóÒ¡
            {
                if (weaponArea != null)
                    Destroy(weaponArea);
            }
        }
        else //Ç°Ò¡
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
        if (weaponArea != null)
            Destroy(weaponArea);
    }


}

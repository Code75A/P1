using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController: MonoBehaviour
{
    [SerializeField]
    private List<int> weaponsInHand = new();//����ӵ�е�������ID
    private int weaponIndex = 0;//�����õ�������weaponsInHand�е�����
    private int weaponId = 1;//�����õ�������ID
    private AbstractWeapon weapon = null;//�����õ�����

    private void Awake()
    {
        weaponsInHand.Add(1);
        weaponId = 1;
        weaponIndex = 0;
        weapon = GetWeaponById(weaponId);
    }
    public void NextWeapon()
    {
        if(weaponIndex == weaponsInHand.Count - 1)
        {
            weaponIndex = 0;
        }
        else weaponIndex++;
        Debug.Log("�л���������" + weaponIndex);
        SetWeaponId(weaponsInHand[weaponIndex]);
    }
    public void LastWeapon()
    {
        if(weaponIndex == 0)
        {
            weaponIndex = weaponsInHand.Count - 1;
        }
        else weaponIndex--;
        Debug.Log("�л���������" + weaponIndex);
        SetWeaponId(weaponsInHand[weaponIndex]);
    }
    private void SetWeaponId(int id)
    {
        weaponId = id;
        weapon = GetWeaponById(weaponId);
    }
    public int GetWeaponId()
    {
        return weaponId;
    }
    public AbstractWeapon GetWeapon()
    {
        return weapon;
    }
    private AbstractWeapon GetWeaponById(int id)
    {
        return id switch
        {
            1 => WeaponStatics.Axe(),
            2 => WeaponStatics.Sword(),
            _ => WeaponStatics.Axe(),
        };
    }
    public int CalculateDamage()
    {
        Debug.Log(weapon.damage);
        return weapon.damage;
    }
    public void AddNewWeapon(int id)//��������
    {
        weaponsInHand.Add(id);
    }
    public void RemoveWeaponUsing()//ɾ�������õ�����
    {
        weaponsInHand.RemoveAt(weaponIndex);
        weapon = GetWeaponById(weaponIndex);
    }
}

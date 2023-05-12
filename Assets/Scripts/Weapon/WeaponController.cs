using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController: MonoBehaviour
{
    [SerializeField]
    private List<int> weaponsInHand = new();//所有拥有的武器的ID
    private int weaponIndex = 0;//正在用的武器在weaponsInHand中的索引
    private int weaponId = 1;//正在用的武器的ID
    private AbstractWeapon weapon = null;//正在用的武器

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
        Debug.Log("切换武器序列" + weaponIndex);
        SetWeaponId(weaponsInHand[weaponIndex]);
    }
    public void LastWeapon()
    {
        if(weaponIndex == 0)
        {
            weaponIndex = weaponsInHand.Count - 1;
        }
        else weaponIndex--;
        Debug.Log("切换武器序列" + weaponIndex);
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
    public void AddNewWeapon(int id)//新增武器
    {
        weaponsInHand.Add(id);
    }
    public void RemoveWeaponUsing()//删除正在用的武器
    {
        weaponsInHand.RemoveAt(weaponIndex);
        weapon = GetWeaponById(weaponIndex);
    }
}

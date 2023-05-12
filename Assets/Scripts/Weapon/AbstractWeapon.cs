using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.IMGUI.Controls.PrimitiveBoundsHandle;

public class AbstractWeapon
{
    public int weaponType;
    public float lightAttackWindUpTime;
    public float lightAttackActiveTime;
    public float lightAttackRecoveryTime;
        
    public float heavyAttackWindUpTime;
    public float heavyAttackActiveTime;
    public float heavyAttackRecoveryTime;

    public float reflectWindUpTime;
    public float reflectActiveTime;
    public float reflectRecoveryTime;

    public string lightAttackAreaName;
    public string heavyAttackAreaName;
    public string reflectAreaName;

    public int damage=2;
}
public static class WeaponStatics
{
    public static AbstractWeapon Axe()
    {
        return new AbstractWeapon()
        {
            weaponType = 1,
            lightAttackWindUpTime = 0.2f,
            lightAttackActiveTime = 0.2f,
            lightAttackRecoveryTime = 0.2f,
            heavyAttackWindUpTime = 0.2f,
            heavyAttackActiveTime = 0.2f,
            heavyAttackRecoveryTime = 0.2f,
            reflectWindUpTime = 0.2f,
            reflectActiveTime = 0.2f,
            reflectRecoveryTime = 0.2f,
            lightAttackAreaName = "Prefabs/Areas/LAA",
            heavyAttackAreaName = "Prefabs/Areas/HAA",
            reflectAreaName = "Prefabs/Areas/FA",
            damage = 1
        };
    }
    public static AbstractWeapon Sword()
    {
        return new AbstractWeapon()
        {
            weaponType = 1,
            lightAttackWindUpTime = 0.2f,
            lightAttackActiveTime = 0.2f,
            lightAttackRecoveryTime = 0.2f,
            heavyAttackWindUpTime = 0.2f,
            heavyAttackActiveTime = 0.2f,
            heavyAttackRecoveryTime = 0.2f,
            reflectWindUpTime = 0.2f,
            reflectActiveTime = 0.2f,
            reflectRecoveryTime = 0.2f,
            lightAttackAreaName = "Prefabs/Areas/LAA",
            heavyAttackAreaName = "Prefabs/Areas/HAA",
            reflectAreaName = "Prefabs/Areas/FA",
            damage = 3
        };
    }

}

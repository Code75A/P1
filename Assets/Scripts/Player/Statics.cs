using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statics : MonoBehaviour
{
    public int LEVEL;
    public float HP = 100f;
    public float CurrentHP;
    public float MP = 25f;
    public float TP = 50f;
    public float DEFENSE;
    public float TENACITY;
    // Start is called before the first frame update
    void Start()
    {
        CurrentHP = 100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.O))//DEBUG O ¿ÛÑª
            if(CurrentHP>0)
            {
                CurrentHP -= 0.1f;
            }
        if (Input.GetKey(KeyCode.P))//DEBUG P ¼ÓÑª
            if (CurrentHP < HP )
            {
                CurrentHP += 0.1f;
            }
    }
}

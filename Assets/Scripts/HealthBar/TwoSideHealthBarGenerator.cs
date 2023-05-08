using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoSideHealthBarGenerator : MonoBehaviour
{
   [SerializeField]
    public GameObject Master;
    [SerializeField]
    public float MaxHealth;
    public float CurrentHealth;
    [SerializeField]
    public GameObject MaxHealthBar;
    public GameObject CurHealthBar;

    private Statics MasterStatics;

    private float MaxHealthBarLenth;
    private float CurHealthBarLenth;

    private float LastHealth;//�ղŵ�Ѫ�������������ӳ�Ѫ��
    
    private void Start()
    {
        MasterStatics = Master.GetComponent<Statics>();
        CurrentHealth=MaxHealth = MasterStatics.HP;
        MaxHealthBarLenth = CurHealthBarLenth = MaxHealthBar.transform.lossyScale.x;
    }

    void Update()
    {
        MasterStatics = Master.GetComponent<Statics>();
        CurrentHealth = MasterStatics.CurrentHP;
        HealthBarUpdate();
    }

    void HealthBarUpdate()
    {
        CurHealthBarLenth = CurrentHealth / MaxHealth * 10.0f;
        transform.localScale = new Vector3(CurHealthBarLenth, transform.localScale.y, transform.localScale.z);
    }
}

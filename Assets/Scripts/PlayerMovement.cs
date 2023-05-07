using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D RB;         //��λ��edit->input_manager
    private float MovementInputH;   //���ˮƽ����
    private float MovementInputV;   //�����ֱ����
    private bool IsFacingR=true;         //����

    public float BasicSpeedX = 2.0f;
    public float BasicSpeedY = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        transform.rotation=Camera.main.transform.rotation;
        RB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckFacingDirection();
    }
    private void FixedUpdate()//�̶�Ƶ�ƶ�
    {
        ImplyMovement();
    }
    private void ImplyMovement()//RB<-�ٶ�
    {
        RB.velocity = new Vector2(MovementInputH, MovementInputV).normalized* new Vector2(BasicSpeedX, BasicSpeedY);
    }
    private void CheckInput()
    {
        MovementInputH = Input.GetAxisRaw("Horizontal");
        MovementInputV = Input.GetAxisRaw("Vertical");
    }
    private void CheckFacingDirection()
    {
        if (IsFacingR && MovementInputH < 0)
            Flip();
        else if(!IsFacingR && MovementInputH > 0)
            Flip();
    }
    private void Flip()
    {
        IsFacingR = !IsFacingR;
        transform.Rotate(0.0f,180.0f,0.0f);
    }
}

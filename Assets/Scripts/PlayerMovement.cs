using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement: MonoBehaviour {

    public float speed; //скорость игрока
    private Rigidbody2D myRigidbody;
    private Vector3 change; //позиция игрока
    private Animator animator; //анимация игрока

    // Это для инициализации
    void Start () {
        animator = GetComponent<Animator>(); //ссылка на анимацию персонажа в Unity 
        myRigidbody = GetComponent<Rigidbody2D>(); //ссылка на тело в Unity 
	}

    // Обновление вызывается один раз за кадр
    void Update () {
        change = Vector3.zero; // делаем начальную позицию(0,0)
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        UpdateAnimationAndMove();
    }

    void UpdateAnimationAndMove() //обновление анимации и перемещения
    {
        if (change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }else{
            animator.SetBool("moving", false);
        }
    }

    void MoveCharacter() //метод движения
    {
        myRigidbody.MovePosition(
                transform.position + change * speed * Time.deltaTime //формула движения
            );
    }
}

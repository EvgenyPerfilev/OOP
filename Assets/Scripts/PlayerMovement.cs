using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger, 
    idle
} 

public class PlayerMovement: MonoBehaviour {

    public PlayerState currentState; //текущее состояние
    public float speed; //скорость игрока
    private Rigidbody2D myRigidbody;
    private Vector3 change; //позиция игрока
    private Animator animator; //анимация игрока
    public FloatValue currentHealth; //cостояние здоровья
    public Signal playerHealthSignal; //сигнал о здоровье

    // Это для инициализации
    void Start () {
        currentState = PlayerState.walk;
        animator = GetComponent<Animator>(); //ссылка на анимацию персонажа в Unity 
        myRigidbody = GetComponent<Rigidbody2D>(); //ссылка на тело в Unity 
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
	}

    // Обновление вызывается один раз за кадр
    void Update () {
        change = Vector3.zero; // делаем начальную позицию(0,0)
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        if(Input.GetButtonDown("attack") && currentState != PlayerState.attack && currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if(currentState == PlayerState.walk || currentState == PlayerState.idle)
            {
                UpdateAnimationAndMove();
            }
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        currentState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        currentState = PlayerState.walk;
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
        change.Normalize();
        myRigidbody.MovePosition(
                transform.position + change * speed * Time.deltaTime //формула движения
            );
    }

    public void Knock (float knockTime, float damage)
    {
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.RuntimeValue > 0)
        {
            StartCoroutine(KnockCo(knockTime));
        }else{
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator KnockCo(float knockTime)
    {
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.idle;
            myRigidbody.velocity = Vector2.zero;
        }
    }
}

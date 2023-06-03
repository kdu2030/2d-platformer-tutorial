using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField]
    private AudioSource deathSoundEffect;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Die()
    {
        deathSoundEffect.Play();
        animator.SetTrigger("death");
        // 当我们把玩家的bodyType设为static时，玩家就无法移动了。
        rb.bodyType = RigidbodyType2D.Static;
    }

    private void RestartLevel()
    {
        // 这个函数调用会重新开始关卡。
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }
}

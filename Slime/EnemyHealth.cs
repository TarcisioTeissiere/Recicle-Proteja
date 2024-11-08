using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 2;
    public int currentHealth;
    private Animator animator;
    public GameObject trashItemPrefab;
    private float dropChance = 0.8f;

    private void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Inimigo tomou dano. Vida atual: " + currentHealth);
        animator.SetTrigger("slimeHit");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Inimigo derrotado!");
        animator.SetTrigger("isDead");

        if (Random.value <= dropChance)
        {
            Instantiate(trashItemPrefab, transform.position, Quaternion.identity);
        }
        
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            // Verifica se o jogador está atacando antes de aplicar dano
            PlayerAttack playerAttack = collision.collider.GetComponent<PlayerAttack>();
            if (playerAttack != null && playerAttack.isAttacking)
            {
                TakeDamage(1); // Aplica o dano ao inimigo
            }
            else
            {
                Debug.Log("O jogador colidiu, mas não está atacando.");
            }
        }
    }
}

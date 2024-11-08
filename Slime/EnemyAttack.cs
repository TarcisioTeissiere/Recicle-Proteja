using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 0.5f; // Distância mínima para atacar
    public int damage = 1; // Dano causado ao jogador
    private Transform player;
    private bool isAttacking = false;
    private Animator animator; // Referência ao Animator
    private float attackCooldown = 5f; // Tempo de cooldown entre ataques
    private float lastAttackTime = 0f; // Armazena o tempo do último ataque

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>(); // Inicializa o Animator
    }

    private void Update()
    {
        CheckAttackRange();
    }

    private void CheckAttackRange()
    {
        if (player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= attackRange && !isAttacking)
        {
            // Verifica se o cooldown foi cumprido
            if (Time.time >= lastAttackTime + attackCooldown)
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        isAttacking = true;
        lastAttackTime = Time.time; // Atualiza o tempo do último ataque
        animator.SetTrigger("isAttacking"); // Chama a animação de ataque

        // Reduz a vida do jogador
        player.GetComponent<PlayerHealth>()?.TakeDamage(damage);

        // Reinicia o estado de ataque após um intervalo
        Invoke("ResetAttack", 1f);
    }

    private void ResetAttack()
    {
        isAttacking = false;
    }
}

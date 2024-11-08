using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public Animator animator;          // Referência ao Animator
    public float attackRange = 1f;     // Distância do ataque
    public LayerMask enemyLayer;       // Camada dos inimigos
    public bool isAttacking = false;   // Controle do estado de ataque

    private void Update()
    {
        // Verifica se a tecla de ataque foi pressionada
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    private void Attack()
    {
        // Inicia a animação de ataque
        animator.SetTrigger("playerAttack");

        // Marca o jogador como atacando
        isAttacking = true;

        // Detecta inimigos dentro do alcance de ataque e aplica dano
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
        
        foreach (Collider2D enemy in hitEnemies)
        {
            // Passa a posição do jogador como segundo argumento
            enemy.GetComponent<EnemyHealth>()?.TakeDamage(1);
        }

        // Reinicia o estado de ataque após a animação (ajuste o tempo conforme necessário)
        Invoke("ResetAttack", 0.5f); // Duração da animação de ataque
    }

    private void ResetAttack()
    {
        // Quando a animação terminar, o jogador não está mais atacando
        isAttacking = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

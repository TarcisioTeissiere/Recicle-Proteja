using UnityEngine;

public class TrashItem : MonoBehaviour
{
    public TrashType trashType; // Tipo de lixo usando enum em vez de string

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Verifica se o jogador colidiu com o item de lixo
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Colisão com o Player detectada.");

            // Tenta obter o PlayerInventory do jogador
            PlayerInventory playerInventory = collision.collider.GetComponent<PlayerInventory>();
            if (playerInventory != null)
            {
                Debug.Log("PlayerInventory encontrado. Tentando coletar o lixo...");

                // Adiciona o item ao inventário do jogador
                playerInventory.PickUpTrash(trashType); // Passa o enum diretamente

                // Remove o objeto de lixo do jogo após ser coletado
                Destroy(gameObject);
                Debug.Log("Lixo coletado e destruído: " + trashType);
            }
            else
            {
                Debug.LogWarning("PlayerInventory não encontrado no Player.");
            }
        }
    }

    // Método opcional para descarte
    public void Discard()
    {
        Debug.Log("Descarte do lixo: " + trashType);
        Destroy(gameObject);
    }
}

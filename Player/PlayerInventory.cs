using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInventory : MonoBehaviour
{
    private TrashType currentTrash; // Tipo de lixo atualmente carregado pelo jogador
    public int maxItems = 3; // Número máximo de itens no inventário
    private int currentItemCount = 0; // Contador de itens atuais no inventário

    public void PickUpTrash(TrashType trashType)
    {
        if (currentItemCount < maxItems) // Verifica se ainda pode coletar itens
        {
            currentTrash = trashType;
            currentItemCount++; // Incrementa o contador de itens
            Debug.Log("Lixo coletado: " + trashType + ". Itens no inventário: " + currentItemCount);
        }
        else
        {
            Debug.Log("Inventário cheio! Não é possível coletar mais lixo.");
        }
    }

    public bool IsInventoryFull() // Método para verificar se o inventário está cheio
    {
        return currentItemCount >= maxItems;
    }

    private void Update()
{
    if (Input.GetKeyDown(KeyCode.R) && currentTrash != null) // Verifica se o jogador pressionou 'R' para descartar
    {
        TrashBin closestBin = FindClosestTrashBin();
        if (closestBin != null && closestBin.IsCorrectBin(currentTrash)) // Passa diretamente o tipo TrashType
        {
            GameManager.instance.AddScore(100); // Adiciona pontuação para descarte correto
            Debug.Log("Lixo descartado corretamente!");
        }
        else
        {
            GameManager.instance.AddScore(-100); // Penaliza para descarte incorreto
            Debug.Log("Lixo descartado incorretamente!");
        }

        // Verifica se a pontuação está abaixo de zero
        if (GameManager.instance.GetScore() < 0)
        {
            SceneManager.LoadScene("Lose"); // Carrega a cena de derrota
        }

        currentTrash = default; // Limpa o lixo após o descarte
        currentItemCount--; // Decrementa o contador de itens ao descartar
    }
}

    private TrashBin FindClosestTrashBin()
    {
        TrashBin[] bins = FindObjectsOfType<TrashBin>();
        TrashBin closestBin = null;
        float minDistance = Mathf.Infinity;

        foreach (TrashBin bin in bins)
        {
            float distance = Vector3.Distance(transform.position, bin.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestBin = bin;
            }
        }

        return closestBin;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Verifica se o objeto que colidiu é um item de lixo
        if (collision.gameObject.CompareTag("Trash")) // Certifique-se de que o objeto de lixo tenha a tag "Trash"
        {
            Debug.Log("Colisão com lixo detectada."); // Verificação extra para ver se a colisão está sendo detectada

            TrashItem trashItem = collision.gameObject.GetComponent<TrashItem>();
            if (trashItem != null)
            {
                PickUpTrash(trashItem.trashType); // Coleta o lixo com o tipo especificado
                Destroy(collision.gameObject); // Destrói o objeto de lixo para "coleta" e desaparecimento
            }
            else
            {
                Debug.LogWarning("Objeto de lixo não possui o componente TrashItem.");
            }
        }
    }
}

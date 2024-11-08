using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public int Score { get; private set; } = 0; // Propriedade apenas para leitura pública da pontuação

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Preserva o GameManager entre cenas
            Debug.Log("GameManager inicializado.");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        if (amount != 0)
        {
            Score += amount;
            Debug.Log("Pontuação atual: " + Score);
        }
        else
        {
            Debug.LogWarning("A tentativa de adicionar pontuação foi ignorada, pois o valor é zero.");
        }
    }

    public int GetScore()
    {
        return Score;
    }
}

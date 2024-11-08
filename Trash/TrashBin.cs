using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public TrashType acceptedTrashType; // Tipo de lixo que essa lixeira aceita

    public bool IsCorrectBin(TrashType trashType) // Mudou de int para TrashType
    {
        // Compara o tipo de lixo diretamente, sem precisar de cast
        return trashType == acceptedTrashType;
    }

}

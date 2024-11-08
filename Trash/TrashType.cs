using System;
using UnityEngine;

public class TrashType : MonoBehaviour
{
    public static explicit operator int(TrashType v)
    {
        throw new NotImplementedException();
    }

    public enum trashType
    {
        Plastic,
        Glass,
        Metal,
        Paper,
        Organic,
        // Adicione outros tipos conforme necessário
    }
}
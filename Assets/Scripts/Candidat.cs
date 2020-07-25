using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Metier
{
    Coloriste,
    Ingenieur,
    Stenographe,
    Historien,
    GardeChausse,
    Batteur,
    Gramatteur,
    Planteur,
    Animateur,
    Comptable
}

[CreateAssetMenu(fileName = "Candidat", menuName = "Candidat")]
public class Candidat : ScriptableObject
{
    public int id;
    public Metier metier;
    public Sprite avatar;
    public AudioClip voice;
}

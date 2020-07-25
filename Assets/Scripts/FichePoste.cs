using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Fiche", menuName = "Fiche de poste")]
public class FichePoste : ScriptableObject
{
    public int id;
    public Metier poste;
    public Sprite visual;
    public Sprite logo;
    public List<Candidat> linkedCandidatsList = new List<Candidat>();
}

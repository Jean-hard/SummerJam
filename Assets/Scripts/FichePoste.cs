using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Fiche", menuName = "Fiche de poste")]
public class FichePoste : ScriptableObject
{
    public int id;
    public string metierName;
    public Metier poste;
    public Sprite visual;
    public Sprite logo;
    public Sprite hoverLogo;
    public List<Candidat> linkedCandidatsList = new List<Candidat>();
}

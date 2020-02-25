using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public string title;
    public Entidade stats;
    public Sprite artwork;
    public List<Card> enemyCards = new List<Card>();
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Define um criador de Inimigos (Factory Method) onde cria instâncias para armazenar dados de inimigos em um asset do jogo (Scriptable Obect)
/// Cada inimigo armazena seu nome, arte, cartas usáveis, cartas de recompensa possível, e tipo de inimigo (seja voador ou larva por exemplo)
/// 
/// Autor: Lucas Lima da Silva Santos
/// Data de criação: 12/02/2020
/// </summary>





[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public enum Tipo
    {
        None,
        Fly,
        Maggot
    }
    public string title;
    public Entidade stats;
    public Sprite artwork;
    public Tipo tipo;

    public List<Card> enemyCards = new List<Card>();


    public List<Card> prizeCards = new List<Card>();

}

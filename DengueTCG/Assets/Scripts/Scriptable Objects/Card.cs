using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Define um criador de cartas (Factory Method)ria instâncias para armazenar dados de cartas em um asset do jogo (Scriptable Obect)
/// Cada carta armazena seu nome, arte, custo de uso, descrição, texto opcional, valor do efeito (seja para atacar ou defender)
/// 
/// Autor: Lucas Lima da Silva Santos
/// Data de criação: 12/02/2020
/// </summary>
/// 



[CreateAssetMenu(fileName = "New Card" , menuName = "Card")]
public class Card : ScriptableObject {

    private enum Tipos
    {
        Ataque,
        Defesa,
        Especial
    }

    public string title;
    public int cost;
    public Sprite artwork;
    [TextArea]
    public string description;
    [TextArea]
    public string flavorText;
    [SerializeField]
    private Tipos tipo;
    [Range(0,2)]
    public int type; 

    public Sprite backgroundEffect; //O simbolo atras do valor de efeito muda de arcodor com tipo de carta?(ataque, defesa)
    public int effect;


}

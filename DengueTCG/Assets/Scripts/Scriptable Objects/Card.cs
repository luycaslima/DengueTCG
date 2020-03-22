using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Define um criador de cartas (Factory Method) ria instâncias para armazenar dados de cartas em um asset do jogo (Scriptable Obect)
/// Cada carta armazena seu nome, arte, custo de uso, descrição, texto opcional, valor do efeito (seja para atacar ou defender)
/// Pode possui um efeito especial ou não a carta (precisa ser configurado para ser utilizado no BattleManager)
/// 
/// Autor: Lucas Lima da Silva Santos
/// Data de criação: 12/02/2020
/// </summary>
/// 


[CreateAssetMenu(fileName = "New Card" , menuName = "Card")]
public class Card : ScriptableObject {

    public enum Types
    {
        
        Atack,
        RecoverHp,
        ShieldUp,
        Especial
    }

    public enum EspecialEffect
    {
        None,
        Buff,
        Debuff,
        PickCard,
        FindCard
    }

    public string title;
    public int cost;
    public Sprite artwork;
    [TextArea]
    public string description;
    [TextArea]
    public string flavorText;
    public Types type;

    public Enemy.Tipo target;
    public EspecialEffect especialEffect;

    public int effect;


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Classe que recebe os atributos da instância ENEMY na tela de batalha
/// mostra na tela o nome, o HP(pontos de vida) atual, o pontos de escudo(shield) atual.
/// 
/// Autor: Lucas Lima da Silva Santos
/// Data de criação: 07/03/2020
/// </summary>

public class EnemyController : MonoBehaviour
{
    [Header("Inimigo:")]
    public Enemy enemy;

    [Header("Referência a Batalha:")]
    public BattleManager battle;

    [Header("Status Atual:")]
    public int currentHp;
    public int currentShield;

    [Header("Referência dos Textos:")]
    public Text nameText;
    public Text hpText;
    public Text shieldText;

    [Header("Prefab da Carta:")]
    public GameObject cardDisplay;

    // Start is called before the first frame update
    void Start()
    {
        currentHp = enemy.stats.max_HP;
        currentShield = 0;

        nameText.text = enemy.title;
        hpText.text = enemy.stats.max_HP.ToString();
    }

    //Escolhe uma carta dentre as cartas que possui
    public void ChooseCard()
    {
        int rndCardIndex = Random.Range(0, enemy.enemyCards.Count);
        Card pickedCard = enemy.enemyCards[rndCardIndex];
        
        battle.ApplyCardEffect(pickedCard);
    }

    //Atualiza o texto na tela dos pontos de vida
    public void UpdateHpText()
    {
        hpText.text = currentHp.ToString();
    }

    //Atualiza o texto na tela dos pontos de escudo
    public void UpdateShieldText()
    {
        shieldText.text = currentShield.ToString();
    }

}

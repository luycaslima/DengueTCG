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
    public Enemy enemy;

    [Header("Status Atual:")]
    public int currentHp;
    public int currentShield;

    [Header("Referência dos Textos:")]
    public Text nameText;
    public Text hpText;


    // Start is called before the first frame update
    void Start()
    {
        nameText.text = enemy.title;
        hpText.text = enemy.stats.max_HP.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

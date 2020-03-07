using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
        nameText.text = enemy.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

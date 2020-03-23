using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Define os pontos de vida, escudo e custo máximo que uma entidade pode possuir
/// 
/// Autor: Lucas Lima da Silva Santos
/// Data de Criação: 02/02/2020
/// </summary>

[System.Serializable]
public class Entity
{
    [Tooltip("Hp máximo da entidade")]
    public int max_HP;
    [Tooltip("Escudo máximo da entidade ")]
    public int max_Shield;
    [Tooltip("Energias para cartas máximo")]
    public int max_Cost;

}

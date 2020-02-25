using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Entidade
{
    [Tooltip("Hp máximo da entidade")]
    public int max_HP;
    [Tooltip("Escudo máximo da entidade ")]
    public int max_Shield;
    [Tooltip("Energias para cartas máximo")]
    public int max_cost;

}

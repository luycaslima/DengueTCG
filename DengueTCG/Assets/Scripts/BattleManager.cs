using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// Classe que administra máquina de estados da batalha e define qual turno é de quem e quem ganhou e perdeu
/// 
/// </summary>


public class BattleManager : MonoBehaviour
{
    private enum State
    {
        PlayerTurn,
        EnemyTurn,
        Victory,
        Defeat
    }
    private State state;

    [Header("Referências dos Atores da Batalha:")]
    public PlayerController player;
    public EnemyController enemy;

    [Header("Botão de Encerrar turno")]
    public RectTransform botaoEncerraTurno;

    private bool playerTurn;
    private bool podeEncerrarTurno;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case State.PlayerTurn:
                break;
            case State.EnemyTurn:
                break;
            case State.Defeat:
                break;
            case State.Victory:
                break;
        }

        //Checar se é o turno do player, 
        //se sim ve se o baralho ta vazio para pegar da pilha de descarte e por no deck de volta
        //senão continua normalmente
        if (!podeEncerrarTurno)
        {
            if (player.currentCost == 0 /*|| myHand.cards.Count == 0 Chechar se eu não puxei cartas antes pra n da positivo logo de primeira*/ )
            {

                MostrarBotaoTurno();
                podeEncerrarTurno = true;

            }
        }
    }

    public void EncerrarTurno()
    {
        //Resetar o valor dos custos aqui
        EsconderBotaoTurno();
        //Chamar função da mão Animar as cartas indo pra pilha de descarte aqui
        player.ResetCost();
        player.myHand.DiscardHand();
        state = State.EnemyTurn;
        //podeEncerrarTurno = false;
    }

    private void EsconderBotaoTurno()
    {
        botaoEncerraTurno.DOKill();
        botaoEncerraTurno.DOAnchorPos(new Vector2(300f, 0), 0.30f);
    }

    private void MostrarBotaoTurno()
    {
        botaoEncerraTurno.DOAnchorPos(Vector2.zero, 0.30f);
    }
}

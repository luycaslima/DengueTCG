using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

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
    [Header("Estado da Batalha:")]
    [SerializeField]
    private State state;

    [Header("Referências dos Atores da Batalha:")]
    public PlayerController player;
    public EnemyController enemy;


    [Header("Ref de Texto de Status")]

    public RectTransform playerStatus;
    public RectTransform enemyStatus;
    public RectTransform battleStatus; //Mostrar o Se Ganhou ou perdeu Aqui (criar um menuzin mais elaborado pra isso


    private RectTransform initialPlayerStatusPos;
    private RectTransform initialEnemyStatusPos;

    private Text playerStatusText;
    private Text enemyStatusText;
    private Text battleStatsText; 

    [Header("Cores do Status do texto:")]
    public List<Color> statusColors;

    [Header("Botão de Encerrar turno")]
    public RectTransform buttonEndTurn;

    private bool canEndTurn;
    private bool endGame = false;

    // Start is called before the first frame update
    void Start()
    {
        state = State.PlayerTurn;
        initialEnemyStatusPos = enemyStatus;
        initialPlayerStatusPos = playerStatus;

        playerStatusText = playerStatus.GetComponent<Text>();
        enemyStatusText = enemyStatus.GetComponent<Text>();
        battleStatsText = battleStatus.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //Checa se o jogo acabou
        if (!endGame)
        {
            if(player.currentHp == 0)
            {
                state = State.Defeat;
                endGame = true;
            }else if(enemy.currentHp == 0)
            {
                state = State.Victory;
                endGame = true;
            }
           
        }

        //Máquina de estados do jogo
        switch (state)
        {
            case State.PlayerTurn:

                //Checar se é o turno do player, 
                //se sim ve se o baralho ta vazio para pegar da pilha de descarte e por no deck de volta
                //senão continua normalmente
               
                if (!canEndTurn)
                {
                    if (player.currentCost == 0 /*|| player.myHand.cards.Count == 0 Chechar se eu não puxei cartas antes pra n da positivo logo de primeira*/ )
                    {

                        ShowTurnButton();
                        canEndTurn = true;

                    }

                }
                else
                {
                    if (player.currentCost != 0 /*|| myHand.cards.Count == 0 Chechar se eu não puxei cartas antes pra n da positivo logo de primeira*/ )
                    {

                        HideTurnButton();
                        canEndTurn = false;

                    }
                }

                break;
            case State.EnemyTurn:

                enemy.ChooseCard();
                state = State.PlayerTurn;
                break;
            case State.Defeat:
                Defeat();
                break;
            case State.Victory:
                Victory();
                break;
        }

        

    }

    //Aplica o efeito da carta do inimigo ou do Player
    public void ApplyCardEffect(Card card)
    {
        if(state == State.PlayerTurn)
        {
            CheckCardEffectPlayer(card);
        }
        else
        {
            //Mostra a carta ao player depois causa o efeito e passa o turno
            CheckCardEffectEnemy(card);
        }
    }

    private void CheckCardEffectPlayer(Card card)
    {
        switch (card.type)
        {
            case Card.Types.Atack:
                DamageEnemy(card.effect);
                CallStatusText(enemyStatus, initialEnemyStatusPos, enemyStatusText, card.effect, "-", statusColors[0], initialEnemyStatusPos.position.x, 2, .30f);
                //Chamar função da animacao aqui
                break;
            case Card.Types.RecoverHp:
                RecoverHpPlayer(card.effect);
                CallStatusText(playerStatus, initialPlayerStatusPos, playerStatusText, card.effect, "+", statusColors[1], initialPlayerStatusPos.position.x, 1, .30f);
                break;
            case Card.Types.ShieldUp:
                ShieldUpPlayer(card.effect);
                CallStatusText(playerStatus, initialPlayerStatusPos, playerStatusText, card.effect, "+", statusColors[2], initialPlayerStatusPos.position.x, 1, .30f);
                break;
            case Card.Types.Especial:
                //TODO Criar metodo especial
                break;
        }
    }
    private void CheckCardEffectEnemy(Card card)
    {
        switch (card.type)
        {
            case Card.Types.Atack:
                DamagePlayer(card.effect);
                CallStatusText(playerStatus, initialPlayerStatusPos, playerStatusText, card.effect, "-", statusColors[0], initialPlayerStatusPos.position.x, 1, .30f);
                break;
            case Card.Types.RecoverHp:

                break;
            case Card.Types.ShieldUp:
                ShieldUpEnemy(card.effect);
                CallStatusText(enemyStatus, initialEnemyStatusPos, enemyStatusText, card.effect, "+", statusColors[2], initialEnemyStatusPos.position.x, 2, .30f);
                break;
            case Card.Types.Especial:
                //TODO Criar metodo especial
                break;
        }
    }

    private void CallStatusText(RectTransform Postext, RectTransform initialPos, Text text, int value, string signal, Color color, float x, float y, float time)
    {
        /*StopCoroutine(ResetCallStatusText(Postext, initialPos));
        Postext.DOKill();
        Postext.position = initialPos.position;
        */
        Postext.DOKill();
        Postext.DOMove(initialPos.position, time);

        Postext.gameObject.SetActive(true);
        text.color = color;
        text.text = signal + value.ToString();
        Postext.DOMove(new Vector3(x, y), time);
        StartCoroutine(ResetCallStatusText(Postext, initialPos));
    }

    IEnumerator ResetCallStatusText(RectTransform Postext , RectTransform targetPos)
    {
        //TODO O texto não está voltando para posição original
        yield return new WaitForSeconds(.5f);

        Postext.DOKill();
        Postext.DOMove(targetPos.position, .35f);
        //Postext.position = new Vector2(targetPos.position.x, targetPos.position.y);
       
        Postext.gameObject.SetActive(false);
    }

    public void EndTurn()
    {
        //Pode possuir uma chamada de uma função que anime as cartas indo a pilha de descarte aqui
  
        HideTurnButton();
        player.ResetCost();
        player.myHand.DiscardHand();

        if (player.playerDeck.deck.Count == 0)
        {
            player.ResetDeck();
        }

        state = State.EnemyTurn;
    }

    private void HideTurnButton()
    {
        buttonEndTurn.DOKill();
        buttonEndTurn.DOAnchorPos(new Vector2(300f, 0), 0.30f);
    }

    private void ShowTurnButton()
    {
        buttonEndTurn.DOAnchorPos(Vector2.zero, 0.30f);
    }


    public void ShieldUpPlayer(int shieldValor)
    {
        if (shieldValor + player.currentShield < player.status.max_Shield)
        {
            player.currentShield = player.currentShield + shieldValor;
        }
        else
        {
            player.currentShield = player.status.max_Shield;
        }

        player.UpdateShieldText();
    }

    public void RecoverHpPlayer(int hpValor)
    {
        if (hpValor + player.currentHp < player.status.max_HP)
        {
            player.currentHp = player.currentHp + hpValor;
        }
        else
        {
            player.currentShield = player.status.max_Shield;
        }

        player.UpdateHpText();
    }

    private void DamageEnemy(int damage)
    {
        if (enemy.currentShield > 0)
        {
            if((uint)enemy.currentShield - (uint)damage > enemy.currentShield)
            {
                enemy.currentHp = enemy.currentHp - ( damage - enemy.currentShield );
                enemy.currentShield = 0;
            }
            else
            {
                enemy.currentShield = enemy.currentShield - damage;
            }
            enemy.UpdateShieldText();

        }
        else
        {
            enemy.currentHp = enemy.currentHp - damage;
        }


        enemy.UpdateHpText();

    }

    public void ShieldUpEnemy(int shieldValor)
    {
        if (shieldValor + enemy.currentShield < enemy.enemy.stats.max_Shield)
        {
            enemy.currentShield = enemy.currentShield + shieldValor;
        }
        else
        {
            enemy.currentShield = enemy.enemy.stats.max_Shield;
        }

        enemy.UpdateShieldText();
    }

    private void DamagePlayer(int damage)
    {
        if (player.currentShield > 0)
        {
            if ( (uint)damage - (uint)player.currentShield  > player.currentShield)
            {
                player.currentHp = player.currentHp - (damage - player.currentShield);
                player.currentShield = 0;
            }
            else
            {
                player.currentShield = player.currentShield - damage;
            }
            player.UpdateShieldText();

        }
        else
        {
            player.currentHp = player.currentHp - damage;
        }
        player.UpdateHpText();

    }


    private void Victory()
    {

    }

    private void Defeat()
    {

    }

}

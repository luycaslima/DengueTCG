using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// Classe que administra máquina de estados da batalha e define qual turno é de quem e quem ganhou e perdeu
/// Também aplica os efeitos das cartas e chama a janela de fim de batalha
/// 
/// Autor: Lucas Lima da Silva Santos
/// Data de criação: 12/03/2020
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
    [Header("Número de cartas Prêmio Limite:")]
    [Range(1,4)]
    public int prizeLimit; //ser baseado na metade das cartas disponíveis do inimigo derrotado

    [Header("Estado da Batalha:")]
    [SerializeField]
    private State state;

    [Header("Referências dos Atores da Batalha:")]
    public PlayerController player;
    public EnemyController enemy;
    public UIManager uIManager;


    [Header("Ref de Texto de Status")]

    public RectTransform playerStatus;
    public RectTransform enemyStatus;
    
    private RectTransform initialPlayerStatusPos;
    private RectTransform initialEnemyStatusPos;

    private Text playerStatusText;
    private Text enemyStatusText;
    
    [Header("Cores do Status do texto:")]
    public List<Color> statusColors;

    [Header("Botão de Encerrar turno")]
    public RectTransform buttonEndTurn;

    [Header("Referencia a display da carta:")]
    public GameObject cardDisplay;
    public GameObject showCardPos;

    private bool canEndTurn;
    private bool endGame = false;
    private bool endScreenCalled = false;
    private bool enemyHasChoosed = false;

    // Start is called before the first frame update
    void Start()
    {
        state = State.PlayerTurn;
        initialEnemyStatusPos = enemyStatus;
        initialPlayerStatusPos = playerStatus;

        playerStatusText = playerStatus.GetComponent<Text>();
        enemyStatusText = enemyStatus.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        //Checa se o jogo acabou
        if (!endGame)
        {
            if(player.currentHp <= 0)
            {
                state = State.Defeat;
                endGame = true;
            }else if(enemy.currentHp <= 0)
            {
                state = State.Victory;
                endGame = true;
            }
           
        }

        //Máquina de estados do jogo
        switch (state)
        {
            case State.PlayerTurn:
               
                // O jogador pode puxar até cinco cartas na sua rodada e descartar até uma 
                //Tratar melhor essa configuração para quando ele n possuir mais cartas ou poder puxar para aparecer o botao de fim de turno
                if (!canEndTurn)
                {
                    if (player.currentCost == 0  )/*|| player.myHand.cards.Count == 0 Chechar se eu não puxei cartas antes pra n da positivo logo de primeira*/ 
                    {
                        ShowTurnButton();
                        canEndTurn = true;
                    }

                }
                else
                {
                    if (player.currentCost != 0 /*|| myHand.cards.Count == 0 */)
                    {
                        HideTurnButton();
                        canEndTurn = false;
                    }
                }

                break;
            case State.EnemyTurn:

                if (!enemyHasChoosed)
                {
                    enemy.ChooseCard();
                    enemyHasChoosed = true;
                }
                
                //state = State.PlayerTurn;
                break;
            case State.Defeat:
                if (!endScreenCalled)
                {
                    uIManager.Defeat();
                    endScreenCalled = true;
                }
                break;
            case State.Victory:
                if (!endScreenCalled)
                {
                    uIManager.Victory(enemy.enemy.prizeCards);
                    endScreenCalled = true;
                }
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
            
            InstantiateEnemyCard(card);
        }
    }

    //Mostra a carta ao player 
    private void InstantiateEnemyCard(Card card)
    {
        GameObject cardInstance = Instantiate(cardDisplay,showCardPos.transform);
        cardInstance.transform.localPosition = new Vector3(0f, 500f);
       
        cardInstance.GetComponent<cardDisplay>().card = card;
        cardInstance.GetComponent<cardDisplay>().isAEnemyCard = true;


        cardInstance.transform.localScale = Vector3.one * 1.2f;
        cardInstance.transform.DOMove(Vector3.zero, .35f);

        //Colocar também que se clicar em algum ponto da tela essa carta seja removida de uma vez ou não deixar so por timer
        StartCoroutine(DestroyEnemyCard(cardInstance, card));
    }

    //Ao ser chamada, destroi a carta apresentada, aplica o efeito dela e passa o turno
    private IEnumerator DestroyEnemyCard(GameObject cardDisplay, Card card)
    {
        yield return new WaitForSeconds(1.15f);
            Destroy(cardDisplay);
            CheckCardEffectEnemy(card);
            state = State.PlayerTurn;
            enemyHasChoosed = false;
    }

    //Checa o efeito da carta do player e aplica nele mesmo ou inimigo
    private void CheckCardEffectPlayer(Card card)
    {
        switch (card.type)
        {
            case Card.Types.Atack:
                DamageEnemy(card.effect);
                CallStatusText(enemyStatus, initialEnemyStatusPos, enemyStatusText, card.effect, "-", statusColors[0], initialEnemyStatusPos.position.x, 2, .30f);
              
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

    //Checa o efeito da carta do inimigo e aplica nele mesmo ou player
    private void CheckCardEffectEnemy(Card card)
    {
        switch (card.type)
        {
            case Card.Types.Atack:
                DamagePlayer(card.effect);
                CallStatusText(playerStatus, initialPlayerStatusPos, playerStatusText, card.effect, "-", statusColors[0], initialPlayerStatusPos.position.x, 1, .30f);
                break;
            case Card.Types.RecoverHp:
                RecoverHpEnemy(card.effect);
                CallStatusText(enemyStatus, initialEnemyStatusPos, enemyStatusText, card.effect, "+", statusColors[1], initialEnemyStatusPos.position.x, 2, .30f);
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

    //Mostra o efeito aplicado na tela sob uma entidade(dano, escudo, recuperar hp)
    private void CallStatusText(RectTransform Postext, RectTransform initialPos, Text text, int value, string signal, Color color, float x, float y, float time)
    {
        /*StopCoroutine(ResetCallStatusText(Postext, initialPos));
        Postext.DOKill();
        
        */

        Postext.gameObject.SetActive(true);
        text.color = color;
        text.text = signal + value.ToString();
        Postext.DOMove(new Vector3(x, y), time);
        StartCoroutine(ResetCallStatusText(Postext, initialPos));
    }

    //Esconde o texto do efeito aplicado depois de meio segundo
    IEnumerator ResetCallStatusText(RectTransform Postext , RectTransform targetPos)
    {
        
        yield return new WaitForSeconds(.5f);
        Postext.localPosition = Vector3.zero;
       
        Postext.gameObject.SetActive(false);
    }

    //Termina o turno do player
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

    //Esconde o butao de encerrar turno
    private void HideTurnButton()
    {
        buttonEndTurn.DOKill();
        buttonEndTurn.DOAnchorPos(new Vector2(300f, 0), 0.30f);
    }

    //Mostra o botao de encerrar turno
    private void ShowTurnButton()
    {
        buttonEndTurn.DOAnchorPos(Vector2.zero, 0.30f);
    }

    //Aumenta o escudo do jogador
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

    //Aumenta os pontos de vida do jogador

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

    //Causa dano ao inimigo

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

    //Recupera os pontos de vida do inimigo
    public void RecoverHpEnemy(int hpValor)
    {
        if (hpValor + enemy.currentHp < enemy.enemy.stats.max_HP)
        {
            enemy.currentHp = enemy.currentHp + hpValor;
        }
        else
        {
            enemy.currentShield = enemy.enemy.stats.max_Shield;
        }

        enemy.UpdateHpText();
    }

    //aumenta o escudo do inimigo
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

    //causa dano no player
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

}

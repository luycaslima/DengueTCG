using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardPile : MonoBehaviour
{
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //ligando o player a esse deck
    public void DiscardSetup(PlayerController playerToSet)
    {
        player = playerToSet;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

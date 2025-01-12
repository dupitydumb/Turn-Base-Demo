
using System;
using UnityEngine;
using UnityEngine.UI;
namespace TurnBasedGame {
    

public class ActionPanelUI : MonoBehaviour
{
    
    [SerializeField] GameObject[] actionSlot;
    [SerializeField] GameObject[] abilitySlot;

    private int actionIndex = 0;
    private int abilityIndex = 0;
    
    [SerializeField] Player player;
    [SerializeField] GameObject abilityPanel;
    private bool isAbilityOpen = false;
    private bool isActionSelected = false;
    void Start()
    {
        actionIndex = 1;
        PrevIndex();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            PrevIndex();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            NextIndex();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            SelectAction();
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            OpenAbilityPanel();
        }
    }

    void NextIndex()
    {

        if (!isAbilityOpen)
        {
            actionSlot[actionIndex].GetComponentInChildren<Image>().enabled = false;
            actionIndex = (actionIndex + 1) % actionSlot.Length;
            actionSlot[actionIndex].GetComponentInChildren<Image>().enabled = true;
        }
        else
        {
            abilitySlot[abilityIndex].GetComponentInChildren<Image>().enabled = false;
            abilityIndex = (abilityIndex + 1) % abilitySlot.Length;
            abilitySlot[abilityIndex].GetComponentInChildren<Image>().enabled = true;
        }
        
    }

    void PrevIndex()
    {
        if (!isAbilityOpen)
        {   
            actionSlot[actionIndex].GetComponentInChildren<Image>().enabled = false;
            actionIndex = (actionIndex + actionSlot.Length - 1) % actionSlot.Length;
            actionSlot[actionIndex].GetComponentInChildren<Image>().enabled = true;
        }
        else
        {
            abilitySlot[abilityIndex].GetComponentInChildren<Image>().enabled = false;
            abilityIndex = (abilityIndex + abilitySlot.Length - 1) % abilitySlot.Length;
            abilitySlot[abilityIndex].GetComponentInChildren<Image>().enabled = true;
        }
        
    }

    void SelectAction()
    {
        if (GameManager.Instance.turnManager.turnState != TurnState.PlayerTurn)
        {
            return;
        }
        if (!isAbilityOpen)
        {
            switch (actionIndex)
            {
                case 0:
                    player.AttackTarget();
                    break;
                case 1:
                    player.Defend();
                    break;
                case 2:
                    OpenAbilityPanel();
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (abilityIndex)
            {
                case 0:
                    SelectAbility(0);
                    break;
                case 1:
                    SelectAbility(1);
                    break;
                case 2:
                    SelectAbility(2);
                    break;
                default:
                    break;
            }
            
        }
    }

    void SelectAbility(int index)
    {
        if (isAbilityOpen)
        player.UseBuff(index);
        isAbilityOpen = false;
        abilityPanel.SetActive(false);
    }

    void OpenAbilityPanel()
    {
        if (isAbilityOpen)
        {
            abilityPanel.SetActive(false);
            isAbilityOpen = false;
        }
        else
        {
            abilityPanel.SetActive(true);
            isAbilityOpen = true;
        }
    }
}
}
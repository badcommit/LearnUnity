using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
public class PlayerController : MonoBehaviour
{
    private GameSpecific.AINPCImpl role;
    readonly private Systems.AISystem aISystem = Systems.AISystem.Instance;
    readonly private Systems.DialogSystem dialogSystem = Systems.DialogSystem.Instance;
    private PlayerAttack playerAttack;
    private PlayerMovement playerMovement;
    private GameObject ui;
    void Start()
    {
        playerAttack = GetComponent<PlayerAttack>();
        playerMovement = GetComponent<PlayerMovement>();
        ui = GameObject.FindWithTag("Dialog");

        var controller = ui.GetComponent<DialogController>();
        dialogSystem.RegisterDialogController(controller);
        role = new GameSpecific.AINPCImpl(dialogSystem, this);
     
        aISystem.RegisterRole(role);
        StartCoroutine("StartSending");
        StartCoroutine("StartRecving");
    }

    private async void StartSending()
    {
        await aISystem.StartSending();
    }

    private async void StartRecving()
    {
        await aISystem.StartRecving();
    }

    public void Attack()
    {
        playerAttack.Attack();
    }

    public void MoveLeft()
    {
        playerMovement.MoveLeft();
    }

    public void MoveRight()
    {
        playerMovement.MoveRight();
    }

    public async void FireRight()
    {
        playerMovement.FaceRight();
        await Task.Delay(1000);
        playerAttack.Attack();
    }

    public async void FireLeft()
    {
        playerMovement.FaceLeft();
        await Task.Delay(1000);
        playerAttack.Attack();
    }

}


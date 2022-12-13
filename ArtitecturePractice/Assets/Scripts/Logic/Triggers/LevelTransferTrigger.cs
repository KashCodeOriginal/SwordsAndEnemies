using System;
using Infrastructure.StateMachine;
using Services.ServiceLocator;
using UnityEngine;

public class LevelTransferTrigger : MonoBehaviour
{
    private string _levelToLoad = "Dungeon";
    private IGameStateMachine _stateMachine;
    
    private bool _isTriggered;

    private void Awake()
    {
        _stateMachine = AllServices.Container.Single<IGameStateMachine>();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player") && !_isTriggered)
        {
            _stateMachine.SwitchState<LevelLoadingState, string>(_levelToLoad);
            _isTriggered = true;     
        }
    }
}

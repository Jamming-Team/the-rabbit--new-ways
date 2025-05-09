using System.Collections.Generic;
using Rabbit.Gameplay.InterfaceState;
using UnityEngine;

namespace Rabbit.Gameplay {
    public class ActionState : State<GP_SceneController> {
        [SerializeField] StateMachine _stateMachine;

        public IActionStateData stateData => _currentBlock[_blockStateIndex];
        public float currentTime;
        
        List<IActionStateData> _currentBlock;
        int _blockStateIndex;

        List<IActionStateData> _defeatBlock;

        public GP_SceneController core => _core;
        
        
        public override void Init(MonoBehaviour core) {
            _core = (GP_SceneController)core;

            currentTime = 0;
            

            _stateMachine.Init(this, false);
            StartBlock();
            DecideOnNextAction_State();
        }

        protected override void OnEnter() {
            base.OnEnter();
            
            GameEvents.UI.OnButtonPressed += InputReaderOnOnPausePressed;
            GameEvents.Gameplay.MaxGrowthReached += DefeatTheGame;
        }



        protected override void OnExit() {
            base.OnExit();
            
            GameEvents.UI.OnButtonPressed -= InputReaderOnOnPausePressed;
            GameEvents.Gameplay.MaxGrowthReached -= DefeatTheGame;
        }

        void InputReaderOnOnPausePressed(GC.UI.ButtonTypes type) {
            if (type != GC.UI.ButtonTypes.Pause)
                return;
            
            _core.interfaceType = typeof(PauseState);
            RequestTransition<InterfaceState.InterfaceState>();
        }

        public override void UpdateState(float delta) {
            base.UpdateState(delta);
            _stateMachine.UpdateSM(delta);
        }

        public void StartBlock() {
            _blockStateIndex = -1;
            _currentBlock = _core.data.gameBlocks[_core.data.currentBlockNum].states;
        }

        public void DecideOnNextAction_State() {
            _blockStateIndex++;
            
            if (_blockStateIndex >= _currentBlock.Count) {
                DecideOnNextAction_Block();
                return;
            }

            switch (stateData.type) {
                case ActionStateTypes.SetupScene: {
                    _stateMachine.ChangeState(typeof(LevelSetupState));
                    break;
                }
                case ActionStateTypes.Gameplay: {
                    _stateMachine.ChangeState(typeof(GameplayState));
                    break;
                }
                case ActionStateTypes.Narrative: {
                    _stateMachine.ChangeState(typeof(NarrativeState));
                    break;
                }
                case ActionStateTypes.DoStuff: {
                    _stateMachine.ChangeState(typeof(DoStuffState));
                    break;
                }
            }
            
        }

        public void DecideOnNextAction_Block() {
            _core.data.currentBlockNum++;
            if (_core.data.currentBlockNum == _core.data.gameBlocks.Count) {
                // Some endgame logics here
                GameEvents.UI.OnSetPostGameText?.Invoke("VICTORY!");
                _core.interfaceType = typeof(PostGameState);
                RequestTransition<InterfaceState.InterfaceState>();
                // Debug.LogError("Game is Over");
                // return;
            }

            // GameManager.Instance.RequestSceneLoad(GC.Scenes.GAMEPLAY, true);
        }
        
        void DefeatTheGame() {
            GameEvents.UI.OnSetPostGameText?.Invoke("DEFEAT");
            _blockStateIndex = -1;
            _currentBlock = _core.data.defeatBlock;
            DecideOnNextAction_State();
        }
        
    }
}
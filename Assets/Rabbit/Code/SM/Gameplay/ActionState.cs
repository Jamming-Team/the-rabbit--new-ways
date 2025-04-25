using System.Collections.Generic;
using UnityEngine;

namespace Rabbit.Gameplay {
    public class ActionState : State<GP_SceneController> {
        [SerializeField] StateMachine _stateMachine;

        public IActionStateData stateData => _currentBlock[_blockStateIndex];
        
        List<IActionStateData> _currentBlock;
        int _blockStateIndex;
        
        
        public override void Init(MonoBehaviour core) {
            _core = (GP_SceneController)core;
            
            _stateMachine.Init(this, false);
            StartBlock();
            DecideOnNextAction_State();
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
            
            if (_blockStateIndex > _currentBlock.Count) {
                DecideOnNextAction_Block();
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
            }
            
        }

        public void DecideOnNextAction_Block() {
            _core.data.currentBlockNum++;
            if (_core.data.currentBlockNum == _core.data.gameBlocks.Count) {
                // Some endgame logics here
                Debug.LogError("Game is Over");
                // return;
            }

            GameManager.Instance.RequestSceneLoad(GC.Scenes.GAMEPLAY);
        }
        
    }
}
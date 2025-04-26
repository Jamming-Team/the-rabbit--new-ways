using System;
using System.Collections.Generic;
using PrimeTween;
using UnityEngine;
using UnityEngine.UI;

namespace Rabbit {
    public class ComicsPage : MonoBehaviour {
        public event Action OnReadyForTheNextPage; 
        
        [SerializeField] List<Image> _comicsPieces;

        int _curPieceNum;
        Coroutine _curCoroutine;

        Sequence _sequence;
        Color _transpColor = new Color(1, 1, 1, 0);
        bool _shouldTurnPage = false;

        public void Init() {
            foreach (var piece in _comicsPieces) {
                piece.color = _transpColor;
            }

            _curPieceNum = -1;
        }

        public void NextPiece() {
            if (_sequence.isAlive) {
                _sequence.Complete();
                // return;
            }

            _curPieceNum++;

            if (_curPieceNum >= _comicsPieces.Count) {
                if (_shouldTurnPage) {
                    OnReadyForTheNextPage?.Invoke();
                    return;
                }
                _shouldTurnPage = true;
                return;
            }

            _sequence = Sequence.Create()
                .Chain(Tween.Custom(_transpColor, Color.white, 1f,
                    newVal => _comicsPieces[_curPieceNum].color = newVal));

        }

    }
}
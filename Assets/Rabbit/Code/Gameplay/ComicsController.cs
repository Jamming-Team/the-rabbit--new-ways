using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Rabbit {
    public class ComicsController : NarrativeBase {
        [SerializeField] Button _skipButton;
        [SerializeField] List<ComicsPage> _pages;

        int _curPageNum;

        void Awake() {
            _curPageNum = -1;
            _skipButton.onClick.AddListener(SkipPiece);

            _pages.ForEach(x=> {
                x.gameObject.SetActive(false);
                x.OnReadyForTheNextPage += NextPage;
            });
            
            NextPage();
        }

        void OnDestroy() {
            _skipButton.onClick.RemoveListener(SkipPiece);
            
            _pages.ForEach(x=> {
                x.OnReadyForTheNextPage += NextPage;
            });
        }

        void NextPage() {
            _curPageNum++;
            
            if (_curPageNum >= _pages.Count) {
                NarrativeEnded?.Invoke();
                _skipButton.onClick.RemoveListener(SkipPiece);
                return;
            }

            if (_curPageNum - 1 != -1)
                _pages[_curPageNum - 1].gameObject.SetActive(false);

            _pages[_curPageNum].gameObject.SetActive(true);
            _pages[_curPageNum].Init();
            _pages[_curPageNum].NextPiece();
        }

        void SkipPiece() {
            _pages[_curPageNum].NextPiece();
        }
        
        
    }
}
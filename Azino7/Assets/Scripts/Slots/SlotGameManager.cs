using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotGameManager : MonoBehaviour
{

    [SerializeField]
    private InfiniteScroll _topInfiniteScroll;
    [SerializeField]
    private InfiniteScroll _bottomInfiniteScroll;

    [SerializeField]
    private WinZone _winZone;

    public delegate void GameFinished();
    public event GameFinished OnFinished;

    public delegate void ScoreUpdated(int score);
    public event ScoreUpdated OnScoreUpdate;

    private int _score;
    private int _defaultScore = 10;
    private bool _isStoped = false;

  



    private void Start()
    {
        _score = _defaultScore;
        _topInfiniteScroll.OnStoped += GameStoped;
        _bottomInfiniteScroll.OnStoped += GameStoped;
    }
   

    public int Score
    {
        get { return _score;}
    }

    public void Play()
    {
        
        _isStoped = false;
        _topInfiniteScroll.Play();
        _bottomInfiniteScroll.Play();
        StateManager.Instance.StartPlaying();
        
    }

    private void GameStoped()
    {
       

        Debug.Log("GameStoped");
        if (_isStoped)
        {
            Debug.Log("Game finished");

            if (_winZone.CheckWin())
            {
                OnWin();
            }
            else
            {
                OnLose();
            }
            StateManager.Instance.StopPlaying();
            OnFinished?.Invoke();
        }
        else
        {
            _isStoped = true;
        }
    }

    private void OnWin()
    {
        ScoreUpdate(true);
    }
    private void OnLose()
    {
        ScoreUpdate(false);
    }

    private void ScoreUpdate(bool win)
    {
        if (win)
        {
            _score *= 2;
        }
        else
        {
            _score = _defaultScore;
        }
        OnScoreUpdate?.Invoke(_score);
        
    }

    public void StopGame()
    {
        _bottomInfiniteScroll.ToStop(true);
        _topInfiniteScroll.ToStop(true);
    }
  
    public void ResetScore()
    {
        ScoreUpdate(false);
    }

}

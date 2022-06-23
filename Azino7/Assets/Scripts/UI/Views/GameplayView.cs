using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameplayView : View
{
    [SerializeField]
    private TMP_Text _scoreTextField;
    [SerializeField]
    private Button _buttonPlay;
    [SerializeField]
    private Button _buttonPlus;
    [SerializeField]
    private Button _buttonMinus;
    [SerializeField]
    private Button _buttonBack;
    [SerializeField]
    private SlotGameManager _slotGameManager;

    public override void Initialize()
    {
        _buttonPlay.onClick.AddListener(()=> 
        { 
            _slotGameManager.Play();
            StateManager.Instance.StartPlaying();
            _buttonPlay.interactable = false;
        });

        _slotGameManager.OnFinished += GameFinished;
        _slotGameManager.OnScoreUpdate += UpdateScoreField;
        _buttonBack.onClick.AddListener(() => 
        { 
            ViewManager.ShowLast();
            StateManager.Instance.StopPlaying();
        }); ;
    }

    private void GameFinished()
    {
        //_buttonPlay.interactable = true;
        StartCoroutine(EnableButton());
    }
    private IEnumerator EnableButton()
    {
        yield return new WaitForSeconds(1f);
        _buttonPlay.interactable = true;
    }


    private void UpdateScoreField(int score)
    {
        _scoreTextField.text = score.ToString();
    }

    public override void Show(object parameter = null)
    {
        base.Show(parameter);
        _buttonPlay.interactable = true;
        _slotGameManager.ResetScore();
    }

}

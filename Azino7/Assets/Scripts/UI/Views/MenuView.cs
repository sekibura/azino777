using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuView : View
{
    [SerializeField]
    private Button _btnStart;
    [SerializeField]
    private Button _btnExit;
    [SerializeField]
    private Toggle _togglePrivacy;

    //private bool _isAgreeTerms = false;

    public override void Initialize()
    {
        _btnExit.onClick.AddListener(ExitApplication);
        _btnStart.onClick.AddListener(StartGame);
        _togglePrivacy.onValueChanged.AddListener(ToggleChanged);
    }

    private void StartGame()
    {
        Debug.Log("Start Game");
        ViewManager.Show<GameplayView>();
    }

    public void OnClickTermsPrivacy()
    {
        ViewManager.Show<TermsPrivacyView>();
    }

    private void ExitApplication()
    {
        Application.Quit();
    }

    private void ToggleChanged(bool value)
    {
        Debug.Log(value);
        _btnStart.interactable = value;
    }
    
  
}

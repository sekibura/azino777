using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TermsPrivacyView : View
{
    [SerializeField]
    private Button _buttonBack;

    public override void Initialize()
    {
        _buttonBack.onClick.AddListener(() => { ViewManager.ShowLast(); });
    }


}

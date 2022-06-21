using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ClickableText : MonoBehaviour, IPointerClickHandler
{
    //[SerializeField]
    //public  LinkAction[] _linkActions;
    [SerializeField]
    private UnityEvent _linkActions;
    public void OnPointerClick(PointerEventData eventData)
    {
        var text = GetComponent<TextMeshProUGUI>();
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(text, Input.mousePosition, null);
            //Debug.Log(linkIndex);
            if (linkIndex > -1)
            {
                var linkedInfo = text.textInfo.linkInfo[linkIndex].GetLinkID();
                DoOnClick(linkedInfo);
            }
        }
    }

    private void DoOnClick(string linkID)
    {
        //Debug.Log(linkID);
        _linkActions?.Invoke();
       
    }

}

//[Serializable]
//public struct LinkAction
//{
//    public string Link;
//    public UnityEvent Events;
//}

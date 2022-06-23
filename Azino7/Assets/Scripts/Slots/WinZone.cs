using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    private GameObject _lastTopObject;
    private GameObject _lastBottomObject;

    [SerializeField]
    private InfiniteScroll _topInfiniteScroll;
    [SerializeField]
    private InfiniteScroll _bottomInfiniteScroll;
    private bool _isPlaying = false;


    private void Start()
    {
        StateManager.Instance.Playing += () => { _isPlaying = true; };
        StateManager.Instance.Stoped += () => { _isPlaying = false; };
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isPlaying)
            return;

        SideSlot sideSlot = collision.gameObject.GetComponent<SlotItem>().SideSlot;
        Debug.Log(collision.gameObject.name);
        if (sideSlot == SideSlot.Top)
        {
            _lastTopObject = collision.gameObject;
            _topInfiniteScroll.ToStop();

        }
        else
        {
            _lastBottomObject = collision.gameObject;
            _bottomInfiniteScroll.ToStop();
        }

    }
    public bool CheckWin()
    {
        string nameBottom = _lastBottomObject.GetComponent<UnityEngine.UI.Image>().sprite.name;
        string nameTop = _lastTopObject.GetComponent<UnityEngine.UI.Image>().sprite.name;
        if(nameBottom == nameTop)
        {
            _lastBottomObject.GetComponent<UnityEngine.UI.Image>().color = Color.green;
            _lastTopObject.GetComponent<UnityEngine.UI.Image>().color = Color.green;
        }
        else
        {
            _lastBottomObject.GetComponent<UnityEngine.UI.Image>().color = Color.red;
            _lastTopObject.GetComponent<UnityEngine.UI.Image>().color = Color.red;
        }

        StartCoroutine(ReturnColor());
        return nameBottom == nameTop;
    }

    private IEnumerator ReturnColor()
    {
        yield return new WaitForSeconds(0.5f);
        _lastBottomObject.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        _lastTopObject.GetComponent<UnityEngine.UI.Image>().color = Color.white;
    }
}

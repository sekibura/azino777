using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotItem : MonoBehaviour
{
    [SerializeField]
    private SideSlot _sideSlot ;

    public SideSlot SideSlot
    {
        get { return _sideSlot; }
    }


}
public enum SideSlot
{
    Top,
    Bottom
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
#nullable enable
public class SelectableItemDriver : MonoBehaviour
{
    public List<Transform> pieceOptions;
    SlotsStyleDriver slotsSettings;
    float FadeInTime = 1f;
    Sequence seq;

    [ContextMenu("Init")]
    public void InitialSetUp()
    {
        seq = DOTween.Sequence();
        slotsSettings = GetComponent<SlotsStyleDriver>();

        //this is iterating backwards because going forwards the list is decreasing making the pointer move forwards but the number is shrinking
        //making it miss some. while loop could also work
        for (int i = slotsSettings.GetSlotsParent.childCount - 1; i >= 0; i--)
        {
            Transform? Item = SelectRandomizedItem();
            if (Item == null)
                return;

            Transform targetSlot = slotsSettings.GetSlotsParent.GetChild(i);

            // Append the movement + fade for each item to the sequence (play one after another)
            seq.Join(MoveItemToSlot(Item, targetSlot.position));
        }

    }
    
    public void PassOnSlotSelections(List<Transform> _slots)
    {
        pieceOptions.AddRange(_slots);
    }

    private Transform? SelectRandomizedItem()
    {
        if (pieceOptions.Count == 0) return null;

        int randomIndex = Random.Range(0, pieceOptions.Count);
        Transform selected = pieceOptions[randomIndex];
        pieceOptions.RemoveAt(randomIndex);
        return selected;

    }

    private Tween? MoveItemToSlot(Transform item, Vector3 targetSlotPos)
    {
        SpriteRenderer sr = item.GetComponent<SpriteRenderer>();
        if (sr == null) return null;

        // Set alpha to 0 (fully transparent)
        Color c = sr.color;
        c.a = 0f;
        sr.color = c;

        item.transform.position = targetSlotPos;
        // Return a tween that moves and fades the item simultaneously
        return DOTween.Sequence()
            .Join(sr.DOFade(1f, FadeInTime)).OnComplete(() => MakeDragable(item, targetSlotPos));
    }

    private void AssignNewDragableToSlot(Vector3 avaiableLocation)
    {
        Transform? item = SelectRandomizedItem();
        if(item == null) return;

        seq = DOTween.Sequence();
        seq.Join(MoveItemToSlot(item, avaiableLocation));
    }
    void MakeDragable(Transform item, Vector3 slotLocation)
    {
        item.gameObject.AddComponent<Dragable>().Init(slotLocation, AssignNewDragableToSlot);
        //assign function to this delegate that deletes the dragable script on the dragable script
        //and returns the new avaiable slot position
    }
}

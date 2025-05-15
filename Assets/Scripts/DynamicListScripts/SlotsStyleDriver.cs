using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsStyleDriver : MonoBehaviour
{
    [SerializeField] Transform SlotsBackground;
    [SerializeField] Transform SlotsParent;
    [SerializeField] Vector2 SlotsSpacing; // X = Horizontal spacing, Y = Vertical spacing
    [SerializeField] int SlotCount;
    [SerializeField] Transform SlotPrefab;
    [SerializeField] Orientation orientation;
    [SerializeField] SpacingType spacingType;
    public Transform GetSlotsParent => SlotsParent;

    private enum Orientation
    {
        Horizontal,
        Vertical,
    }

    private enum SpacingType
    {
        None,
        Center
    }

    private void Start()
    {
        HandleAvailableSlots();
    }

    private void HandleAvailableSlots()
    {
        int slotsToHandle = SlotCount - SlotsParent.childCount;

        if (slotsToHandle < 0)
        {
            for (int i = 0; i < Mathf.Abs(slotsToHandle); i++)
            {
                Destroy(SlotsParent.GetChild(i).gameObject);
            }
        }
        else if (slotsToHandle > 0)
        {
            for (int i = 0; i < Mathf.Abs(slotsToHandle); i++)
            {
                Instantiate(SlotPrefab, SlotsParent.position, Quaternion.identity, SlotsParent).name = "Slot";
            }
        }

        ResizeBackground();
    }

    private void ResizeBackground()
    {
        var slotSize = SlotPrefab.GetComponent<SpriteRenderer>().bounds.size;

        float totalLength = (orientation == Orientation.Vertical)
            ? SlotCount * slotSize.y
            : SlotCount * slotSize.x;

        float spacingValue = (orientation == Orientation.Vertical) ? SlotsSpacing.y : SlotsSpacing.x;

        if (spacingType == SpacingType.Center)
        {
            totalLength += (SlotCount + 1) * spacingValue; // extra spacing on both ends
        }
        else if (spacingType == SpacingType.None)
        {
            // No spacing
        }
        else
        {
            totalLength += (SlotCount - 1) * spacingValue;
        }

        // Apply size to background
        if (orientation == Orientation.Vertical)
        {
            SlotsBackground.localScale = new Vector3(slotSize.x + SlotsSpacing.x * 2f, totalLength, 1f);
        }
        else
        {
            SlotsBackground.localScale = new Vector3(totalLength, slotSize.y + SlotsSpacing.y * 2f, 1f);
        }

        OrganizeSlots();
    }

    private void OrganizeSlots()
    {
        if (SlotCount == 0) return;

        var slotSize = SlotPrefab.GetComponent<SpriteRenderer>().bounds.size;
        var bgRenderer = SlotsBackground.GetComponent<SpriteRenderer>();
        var bgSize = bgRenderer.bounds.size;

        Vector3 startPos = orientation switch
        {
            Orientation.Vertical => new Vector3(SlotsBackground.position.x, SlotsBackground.position.y + bgSize.y / 2f, SlotsBackground.position.z),
            Orientation.Horizontal => new Vector3(SlotsBackground.position.x - bgSize.x / 2f, SlotsBackground.position.y, SlotsBackground.position.z),
            _ => SlotsBackground.position
        };

        float spacing = (orientation == Orientation.Vertical) ? SlotsSpacing.y : SlotsSpacing.x;
        float slotLength = (orientation == Orientation.Vertical) ? slotSize.y : slotSize.x;
        float totalSlotSize = SlotCount * slotLength;

        float availableSpace = (orientation == Orientation.Vertical) ? bgSize.y : bgSize.x;

        if (spacingType == SpacingType.Center)
        {
            spacing = (availableSpace - totalSlotSize) / (SlotCount + 1);
        }
        else if (spacingType == SpacingType.None)
        {
            spacing = 0f;
        }

        for (int i = 0; i < SlotCount; i++)
        {
            float offset = spacing * (i + 1) + slotLength * i;

            Vector3 slotPos;
            if (orientation == Orientation.Vertical)
            {
                float yPos = startPos.y - offset - slotLength / 2f;
                slotPos = new Vector3(startPos.x, yPos, 0f);
            }
            else // Horizontal
            {
                float xPos = startPos.x + offset + slotLength / 2f;
                slotPos = new Vector3(xPos, startPos.y, 0f);
            }

            if (i < SlotsParent.childCount)
            {
                SlotsParent.GetChild(i).position = slotPos;
            }
        }
    }
}
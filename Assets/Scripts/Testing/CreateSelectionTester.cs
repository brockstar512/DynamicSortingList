using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSelectionTester : MonoBehaviour
{
    [SerializeField] SelectableItemDriver slotsManager;
    [SerializeField] Sprite sprite;
    [SerializeField] int count;
    // Start is called before the first frame update
    void Start()
    {
        CreateSelectableOptions();
    }

    private void CreateSelectableOptions()
    {
        List<Transform> options = new List<Transform>();
        for (int i = 0; i < count; i++)
        {
            GameObject go = new GameObject("test selection");
            go.transform.parent = transform;
            go.transform.localPosition = Vector3.zero;

            SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
            sr.sprite = sprite;

            sr.color = new Color(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f)
            );
            options.Add(go.transform);
        }
        slotsManager.PassOnSlotSelections(options);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotsManager : MonoBehaviour
{
    public List<Transform> slots;

    public void PassOnSlotSelections(List<Transform> _slots)
    {
        slots.AddRange(_slots);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

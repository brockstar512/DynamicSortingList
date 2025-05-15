using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSlot : MonoBehaviour
{

    public SpriteRenderer rendr;
    [SerializeField] AudioClip _placed;
    private AudioSource _source;
    private int _id;

    
    void Start()
    {
        rendr = GetComponent<SpriteRenderer>();
        _source = this.transform.gameObject.AddComponent<AudioSource>();


    }

    public void Placed()
    {
        _source?.PlayOneShot(_placed);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//pieces snes is 4X6

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] List<PuzzleSlot> _slots;
    [SerializeField] PuzzlePiece _piecePrefab;
    [SerializeField] List<PuzzlePiece> _pieces;
    [SerializeField] private Transform _slotParent, _pieceParent;

    void Spawn()
    {
        var randomSet = _slots.OrderBy(s=> Random.value).Take(3).ToList();

        for(int i =0; i < randomSet.Count; i++)
        {
            var spawnedSlot = Instantiate(randomSet[i], _slotParent.GetChild(i).position, Quaternion.identity);

            var spawnPiece = Instantiate(_piecePrefab, _pieceParent.GetChild(i).position, Quaternion.identity);

            spawnPiece.Init(spawnedSlot);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }


}

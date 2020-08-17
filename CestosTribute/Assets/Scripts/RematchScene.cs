using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RematchScene : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] trophyPositions;
    public GameObject trophy;
    public int playerId;

    void Update(){
        PositionTrophy(playerId);
    }

    public void PositionTrophy(int _playerId){
        trophy.transform.position = trophyPositions[_playerId -1].transform.position;
    }

}

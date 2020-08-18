using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHolder : MonoBehaviour
{

    public GameObject PlayerSprite;

    private void Update()
    {
        transform.position = new Vector3(PlayerSprite.transform.position.x, PlayerSprite.transform.position.y, PlayerSprite.transform.position.z);
    }

}

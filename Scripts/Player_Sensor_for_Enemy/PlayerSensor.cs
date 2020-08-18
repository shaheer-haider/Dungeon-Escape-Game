using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSensor : MonoBehaviour
{
    string AttackSide = "left";
    void Start()
    {
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (other.gameObject.transform.position.x > transform.position.x)
            {
                AttackSide = "right";
            }
            else if (other.gameObject.transform.position.x < transform.position.x)
            {
                AttackSide = "left";
            }
            if (transform.parent.gameObject.name == "Skeleton")
            {
                Skeleton.instance.Attack(AttackSide);
            }
            else if (transform.parent.gameObject.name == "Moss Giant")
            {
                MossGiant.instance.Attack(AttackSide);
            }
            else if(transform.parent.gameObject.name == "Spider")
            {
                Spider.instance.Attack(AttackSide);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (transform.parent.gameObject.name == "Skeleton")
            {
                Skeleton.instance.AttackToWalk();
            }
            else if (transform.parent.gameObject.name == "Moss Giant")
            {
                MossGiant.instance.AttackToWalk();
            } else if(transform.parent.gameObject.name == "Spider")
            {
                Spider.instance.AttackToWalk();
            }

        }
    }
}

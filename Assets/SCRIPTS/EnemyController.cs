using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Killable {
    public override void healthChanged() {
        throw new NotImplementedException();
    }

    public override void killed() {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D obj)
    {

        //if the tag is something that is a respawn, set the players position to what ever to is designated as the RespawnTransform
        if (obj.tag == "Respawn")
        {
            gameObject.SetActive(false);

        }

    }
}

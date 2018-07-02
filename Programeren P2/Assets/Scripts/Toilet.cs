using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toilet : Locations {

    public override void GetNeed(Needs myNeeds)
    {
        myNeeds.toiletLife -= Time.deltaTime * rechargeSpeed; //gebruikt toilet
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Locations {

    public override void GetNeed(Needs myNeeds)
    {
        myNeeds.foodLife -= Time.deltaTime * rechargeSpeed; // gebruik eten
    }
}

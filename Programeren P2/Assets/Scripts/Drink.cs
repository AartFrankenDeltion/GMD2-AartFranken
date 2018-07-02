using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drink : Locations {

    public override void GetNeed(Needs myNeeds)
    {
        myNeeds.drinkLife -= Time.deltaTime * rechargeSpeed; // gebruik drinken
    }
}

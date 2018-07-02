using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;   //belangrijk unityengine.AI te gebruiken voor NavMeshAgent.

public class Needs : MonoBehaviour
{                       
    public float foodLife;  //daadwerkelijke stats
    public float drinkLife;  
    public float toiletLife;  

    public float foodGoingDownSpeed; // snelheid dat stats omhoog gaan
    public float drinkGoingDownSpeed;
    public float toiletGoingDownSpeed;

    public List<Locations> placesToBe = new List<Locations>();  //alle locaties

    bool goingSomewhere; //ben je onderweg

    NavMeshAgent navMeshAgent;

    public enum NeedType
    {             //na een entry een komma, behalve de laatste. de needs die mr dude nu heeft
        Nothing,
        Food,
        Drink,
        Toilet
    }

    public NeedType needType;

    Type currentType;


	void Start ()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();                       //find the navmeshagent van dit component.
	}

    void Update ()
    {
        NeedsGoingDown(); //stats die naar beneden gaan
        if (goingSomewhere == false)
        {
            NeedsCheck();  //checken wat je nodig hebt
        }
        else
        {
            WaitOnDestination(); //ga naar de plek en wacht tot die need gevult is
        }
	}
    void NeedsGoingDown()
    {
        foodLife += Time.deltaTime * foodGoingDownSpeed;  // food gaat naar boven (cap onder 0)
        if(foodLife < 0)
        {
            foodLife = 0;
        }
        drinkLife += Time.deltaTime * drinkGoingDownSpeed; // drink gaat naar boven (cap onder 0)
        if (drinkLife < 0)
        {
            drinkLife = 0;
        }
        toiletLife += Time.deltaTime * toiletGoingDownSpeed; // toilet gaat naar boven (cap onder 0)
        if (toiletLife < 0)
        {
            toiletLife = 0;
        }
    }

    void NeedsCheck()
    {
        if (foodLife >= 10)  // checkt of je eten nodig hebt
        {
            needType = NeedType.Food;
        }
        else if (drinkLife >= 10)  // checkt of je drinken nodig hebt
        {
            needType = NeedType.Drink;
        }
        else if (toiletLife >= 10) // checkt of je naar het toilet moet
        {
            needType = NeedType.Toilet;
        }


        switch (needType)                                                       //kijkt welke needtype het is
        {
            case NeedType.Drink:                                                //denk aan, "in case...."
                navMeshAgent.SetDestination(CalculateDestination(typeof(Drink)));
                goingSomewhere = true;
                break;                                                          //na drink, is hij klaar met drinken, etc etc.
            case NeedType.Food:
                navMeshAgent.SetDestination(CalculateDestination(typeof(Food)));
                goingSomewhere = true;
                break;
            case NeedType.Toilet:
                navMeshAgent.SetDestination(CalculateDestination(typeof(Toilet)));
                goingSomewhere = true;
                break;
        }
    }

    Vector3 CalculateDestination(Type type)
    {
        currentType = type;
        for (int i = 0; i < placesToBe.Count; i++)  
        {
            if(placesToBe[i].GetType() == type)   // kijkt of het de goeie need is
            {
                return placesToBe[i].transform.position;  // returned de position van de need location
            }
        }
        return transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        Locations newLoc = other.GetComponent<Locations>();  //trigger stay werkt niet altijd daarom dubbel 
        newLoc.GetNeed(this); // verlaag de goede need
    }
    private void OnTriggerStay(Collider other)
    {
        Locations newLoc = other.GetComponent<Locations>(); // same as trigger enter
        newLoc.GetNeed(this);
    }

    void WaitOnDestination()
    {
        switch (needType)
        {
            case NeedType.Drink:
                if(drinkLife<= 0)  // check of drinken gevuld is
                {
                    drinkLife = 0;
                    goingSomewhere = false;  
                }
                break;
            case NeedType.Food:// check of eten gevuld is
                if (foodLife <= 0)
                {
                    foodLife = 0;
                    goingSomewhere = false;
                }
                break;
            case NeedType.Toilet:  // check of toilet gevuld is
                if (toiletLife <= 0)
                {
                    toiletLife = 0;
                    goingSomewhere = false;
                }
                break;
        }
    }
}

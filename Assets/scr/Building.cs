using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Building_basicSCR
{


    private SpriteRenderer mySprite;
    private BoxCollider2D myCollider;

    private void Start()
    {
        isnotCol = true;
        TryGetComponent(out mySprite);
        TryGetComponent(out myCollider);

        WhoAmI("House");

        switch (this.name)
        {
            case "House":
                break;

            case "Cafe":
                gameObject.transform.localScale = new Vector2(0.5f, 0.5f);
                myCollider.size = new Vector2(3f, 3f);
                break;
            case "Grocery":
                gameObject.transform.localScale = new Vector2(0.5f, 0.5f);         
                myCollider.size = new Vector2(3f, 3f);
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (gameObject.tag == "Building")
        {
            switch (this.name)
            {
                case "House":
                    //BuildingSystem.build_system.MoneyOutput = 30;
                    break;

                case "Cafe":
                    BuildingSystem.build_system.MoneyOutput = 50;
                    break;
                case "Grocery":
                    BuildingSystem.build_system.MoneyOutput = 100;
                    break;
                default:
                    break;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isnotCol = true;
        mySprite.color = Color.white;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        isnotCol = false;
        mySprite.color = Color.magenta;
    }

    public void BuildingMove()
    {
        StartCoroutine(BuildingSystem.build_system.FollowMouse(this.gameObject,2));
    }
    
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam_TouchOBJ : MonoBehaviour
{
    public GameObject[] Particles;

    public GameObject cook_Ui;

    private GameObject store_Ui;

    Vector2 Touch_start_pos;

    public Collider2D BeganCol;

    private void Start()
    {
        store_Ui = ArchitectureSystem.build_system.Store_Ui;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch toto = Input.GetTouch(0);

            #region Currently Clicked Obj in : Collider2D clickCol
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 clickPos = new Vector2(worldPos.x, worldPos.y);
            Collider2D clickCol = Physics2D.OverlapPoint(clickPos);
            #endregion

            if (ArchitectureSystem.build_system.isCameraMode == false)
            {
                if (toto.phase == TouchPhase.Began)
                {
                    if (clickCol != null)
                    {
                        BeganCol = clickCol;


                        if (clickCol.tag == "Building")
                        {
                            if (clickCol.TryGetComponent(out Building bb) && bb.RearrangeNow == true)
                            {
                                Debug.Log("설치");
                                bb.BuildingMove();
                            }
                        }
                    }
                }

                if (toto.phase == TouchPhase.Ended && BeganCol == clickCol && clickCol != null && InteractionSystem.Interaction_system.is_Interaction_Mode == false)
                {
                    if (clickCol.tag == "store" && ArchitectureSystem.build_system.isRearrangeMode == false)
                    {
                        store_Ui.SetActive(true);
                    }
                    else if (clickCol.tag == "Building" && clickCol != null)
                    {
                        if (clickCol.TryGetComponent(out Building bb) && bb.RearrangeNow == true)
                        {
                            Debug.Log("설치");
                            bb.BuildingMove();
                        }

                        if (clickCol.TryGetComponent(out Food_Generator generator))
                        {
                            if (generator.current_food_count > 0)
                            {
                                Instantiate(Particles[1], clickCol.transform.position, Particles[1].transform.rotation);
                            }

                            generator.Get_Food();
                        }
                    }
                    else if(clickCol.tag == "Animal" && ArchitectureSystem.build_system.isRearrangeMode == false)
                    {
                        Instantiate(Particles[0], clickCol.transform.position, Particles[0].transform.rotation);

                        ArchitectureSystem.build_system.Money += 1;
                    }
                    else if (clickCol.tag == "Cook" && ArchitectureSystem.build_system.isRearrangeMode == false)
                    {
                        cook_Ui.SetActive(true);
                    }
                    else if (clickCol.tag == "Interaction" && ArchitectureSystem.build_system.isRearrangeMode == false)
                    {
                        clickCol.GetComponent<InteractionButton>().InteractionStart(); //4
                        InteractionSystem.Interaction_system.is_Interaction_Mode = true;
                    }
                    else if (clickCol.tag == "Pat")
                    {
                        Touch_start_pos = toto.position;
                    }
                }

                if (clickCol != null && toto.phase == TouchPhase.Moved)
                {
                    if (clickCol.tag == "Pat")
                    {
                        Debug.Log(Vector2.Distance(Touch_start_pos, toto.position));
                        if (Vector2.Distance(Touch_start_pos, toto.position) >= 300)
                        {
                            Touch_start_pos = toto.position;
                            InteractionSystem.Interaction_system.StartCoroutine(InteractionSystem.Interaction_system.PatInteraction2(clickCol.transform));
                        }
                    }
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Building_basicSCR
{
    private void Start()
    {
        isnotCol = true;
        TryGetComponent(out mySprite);
        TryGetComponent(out myCollider);
        GetComponent<SpriteRenderer>().sprite = mySprite.sprite;
        WhoAmI(this.name);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isnotCol = true;
        mySprite.color = Color.white;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag != "Animal")
        {
            isnotCol = false;
            mySprite.color = Color.magenta;
        }
    }

    public void BuildingMove()
    {
        StartCoroutine(ArchitectureSystem.build_system.FollowMouse(this.gameObject,2));
    }

    public void Interaction_Building(GameObject cell)
    {
        //cell.transform.localRotation = Quaternion.Euler(90, 0, 0);

        Debug.Log("Move!");
        StartCoroutine(WalkToHome(cell.transform));
    }

    private IEnumerator WalkToHome(Transform cell)
    {
        Vector2 Target;
        while (true)
        {
            Target = this.transform.localPosition - cell.localPosition;
            float distance = Target.magnitude;

            if (distance > 0.0f)
            {
                // 이동 방향을 정합니다.
                Target = Target.normalized;

                // 이동 속도를 계산합니다.
                float moveSpeed = Mathf.Min(distance, 1) * Time.deltaTime;

                // A 오브젝트를 이동시킵니다.
                cell.transform.Translate(Target * moveSpeed);
            }

            yield return null;
        }
    }
}


using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class SquareIndicator : MonoBehaviour
{
    Collider2D floorCollider;
    public GameObject SquareIndicat;
    private Vector2 lastMousePos;

    void Start()
    {
        floorCollider = GetComponent<Collider2D>();
        lastMousePos = Mouse.current.position.ReadValue();
    }

    void Update()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();

        if (mousePos != lastMousePos)
        {
            lastMousePos = mousePos;

            Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
            worldPos.z = 0f; // Make sure Z is 0 for 2D
            Collider2D[] hits = Physics2D.OverlapPointAll(worldPos);

            bool ifFloor = false;
            foreach (Collider2D hit in hits)
            {
                if (hit == floorCollider)
                {
                    ifFloor = true;
                    break;
                }
            }

            SetIndicator(ifFloor, worldPos);
        }
    }

    void SetIndicator(bool ifFloor, Vector2 worldPos)
    {
        SquareIndicat.SetActive(ifFloor);
        SquareIndicat.transform.position = new Vector2(Mathf.Round(worldPos.x), Mathf.Round(worldPos.y));
    }
}
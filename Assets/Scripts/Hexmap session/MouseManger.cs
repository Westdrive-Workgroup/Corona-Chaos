using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManger : MonoBehaviour
{
    // Start is called before the first frame update
        
    private Pawn selectedPawn;

    private Vector3 lastMousePosition;

    public float panSpeed = 0.2f;
    public float cameraRotationStep = 5f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MapInteraction();
        }

        if (Input.GetMouseButton(2))
        {
            Vector3 panDirection = Input.mousePosition - lastMousePosition;
            Camera.main.transform.Translate(panDirection.normalized * panSpeed * Time.deltaTime);
        }
        if (Input.GetMouseButton(1))
        {
            Vector3 panDirection = Input.mousePosition - lastMousePosition;
             Camera.main.transform.Rotate(Vector3.up,panDirection.normalized.x * cameraRotationStep * Time.deltaTime);
             Camera.main.transform.Rotate(Vector3.right,panDirection.normalized.y * cameraRotationStep * Time.deltaTime);
            
        }

        
        Camera.main.fieldOfView += Input.mouseScrollDelta.y;
    
        
    }

    void LateUpdate()
    {
        lastMousePosition = Input.mousePosition;
    }
    void Pan()
    {
        
    }
    
    void MapInteraction()
    {
        Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(mouseRay, out hitInfo))
        {
            Debug.Log("hit detected on: " + hitInfo.collider.name);
            
                
                if (hitInfo.collider.CompareTag("Tile"))
                {
                    MeshRenderer mr = hitInfo.collider.GetComponent<MeshRenderer>();
                    if (mr.material.color == Color.red)
                    {
                        mr.material.color = Color.white;
                    }
                    else
                    {
                        mr.material.color = Color.red;
                    }

                    if (selectedPawn != null)
                    {
                        selectedPawn.setDest(hitInfo.transform.position);
                        
                    }
                }

                if (hitInfo.collider.CompareTag("Pawn"))
                {
                    selectedPawn = hitInfo.collider.GetComponent<Pawn>();
                }
            
        }
    }
}

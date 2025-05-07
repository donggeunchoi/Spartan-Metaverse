using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    private Camera camera;
    private GameObject currentInteractable;
    private NPController npc;
    public GameObject EnterUI;
    
    protected override void Start()
    {
        base.Start();
        camera = Camera.main;
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.F)&& currentInteractable != null&& npc != null)
        {
            Debug.Log("npc상호작용");
            
            npc.Interact();
            
        }
    }    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("NPC"))
        {
            Debug.Log("npc부딫힘");
            currentInteractable = other.gameObject;
            npc = currentInteractable.GetComponent<NPController>();

            if(npc != null)
            {
                 EnterUI.SetActive(true);
            }
            else
            {
                Debug.LogError("NPC가 null입니다.");
            }
           

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //안전성을 위해 없으면 건너뛰기;
        if(other == null || other.gameObject == null) return;


        if(other.CompareTag("NPC"))
        {
            if(currentInteractable != null)
            {
                currentInteractable = null;
                npc = null;
                EnterUI.SetActive(false);
            }
            
        }
    }

    protected override void HandleAction()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        movementDirection = new Vector2(horizontal, vertical).normalized;

        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPos = camera.ScreenToWorldPoint(mousePosition);
        lookDirection = (worldPos - (Vector2)transform.position);

        if(lookDirection.magnitude < 0.9f)
        {
            lookDirection = Vector2.zero;
        }
        else
        {
            lookDirection = lookDirection.normalized;
        }
    }
}

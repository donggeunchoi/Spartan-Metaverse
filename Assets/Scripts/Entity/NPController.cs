using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPController : MonoBehaviour
{
    public string npcName = "NPC";
    
    public void Interact()
    {
        if(npcName == "NPC")
        {
             SceneManager.LoadScene("MiniGameScene");
        }
        else if(npcName == "NPC1")
        {
            SceneManager.LoadScene("MiniGameScene2");
        }
        else
        {
            Debug.Log("Interacting with " + npcName);
        }
        //여기에 대화칭열기, 퀘스트 상호작용 구현.
       
      

    }

}

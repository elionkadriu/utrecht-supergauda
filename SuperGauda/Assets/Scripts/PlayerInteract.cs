using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public KeyCode interactKey;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(interactKey)){
            Debug.Log(gameObject.name + " pressed interact with button " + interactKey);
            //do something
        }
    }
}

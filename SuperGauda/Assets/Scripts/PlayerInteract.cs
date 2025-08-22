using UnityEngine;
using UnityEngine.Events;

public class PlayerInteract : MonoBehaviour
{
    public KeyCode interactKey;

    public Interact interactField;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(interactKey) && interactField){
            Debug.Log(gameObject.name + " pressed interact with button " + interactKey);
            interactField.OnInteract.Invoke(this);
        }
    }
}

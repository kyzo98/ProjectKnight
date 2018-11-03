using UnityEngine;
using UnityEngine.EventSystems;

public class DoorGuy_OptionsButtons : MonoBehaviour, IPointerEnterHandler
{
    public GameObject NPC;

    DoorGuy scriptRuneSeller;

    void Update()
    {
        scriptRuneSeller = NPC.GetComponent<DoorGuy>();
    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (gameObject.name == "No_Button")
        {
            scriptRuneSeller.noOptionSelected();
        }
        if (gameObject.name == "Yes_Button")
        {
            scriptRuneSeller.yesOptionSelected();
        }
    }
}
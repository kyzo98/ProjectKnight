using UnityEngine;
using UnityEngine.EventSystems;

public class RuneSeller_OptionsButtons : MonoBehaviour, IPointerEnterHandler {
    public GameObject NPC;

    RuneSeller scriptRuneSeller;

    void Update()
    {
        scriptRuneSeller = NPC.GetComponent<RuneSeller>();
    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if (gameObject.name == "Buy_Button" && scriptRuneSeller.optionSelected == 1)
        {
            scriptRuneSeller.buyOptionSelected();
        }
        if (gameObject.name == "Exit_Button" && scriptRuneSeller.optionSelected == 0)
        {
            scriptRuneSeller.exitOptionSelected();
        }
    }
}

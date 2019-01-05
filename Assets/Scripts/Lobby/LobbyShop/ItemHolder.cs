using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHolder : MonoBehaviour {

    public static ItemHolder itemHolder;

    public Text itemName;
    public Text itemPrice;
    public Text description;
    public Text type;
    public Image itemImg;
    public Button buyButton;

    // Use this for initialization
    void Start () {
        itemHolder = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour {

    public GameObject Inventory;
    public int Space; //maximum space of items inside the invenotory
    public List<ObjetosInventario> items = new List<ObjetosInventario>();


	// Use this for initialization
	void Start () {
        Inventory.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void ShowInventory()
    {
        if(Input.GetKeyDown("i") || Input.GetKeyDown("I"))
        {
            Inventory.SetActive(!Inventory.activeSelf);
        }
    }

    public bool Add(ObjetosInventario item)
    {
        if(items.Count >= Space)
        {
            return false;
        }

        items.Add(item);

        return true;
    }
}

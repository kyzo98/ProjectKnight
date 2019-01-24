using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LobbyShop : MonoBehaviour {

    public GameObject player;
    public Inventario inventario;

    void Start()
    {
        inventario = player.GetComponent<Inventario>();
    }

    //FUNCTIONS TO BUY SORROWS
    public void BuyRage()
    {
        inventario.AddRage();
        //Restar precio de venta a las monedas que tiene el player.
    }

    public void BuyTerror()
    {
        inventario.AddTerror();
        //Restar precio de venta a las monedas que tiene el player.
    }

    public void BuyGrief()
    {
        inventario.AddGrief();
        //Restar precio de venta a las monedas que tiene el player.
    }

    //FUNCTIONS TO BUY DRIVES
    public void BuyCourage()
    {
        inventario.AddCourage();
        //Restar precio de venta a las monedas que tiene el player.
    }

    public void BuyFocus()
    {
        inventario.AddFocus();
        //Restar precio de venta a las monedas que tiene el player.
    }

    public void BuyWill()
    {
        inventario.AddWill();
        //Restar precio de venta a las monedas que tiene el player.
    }

    public void BuyRemembrance()
    {
        inventario.AddRemembrance();
        //Restar precio de venta a las monedas que tiene el player.
    }

    public void BuySpiritualHealing()
    {
        inventario.AddSpiritualHealing();
        //Restar precio de venta a las monedas que tiene el player.
    }

    public void BuyClarity()
    {
        inventario.AddClarity();
        //Restar precio de venta a las monedas que tiene el player.
    }

    public void BuyGrace()
    {
        inventario.AddGrace();
        //Restar precio de venta a las monedas que tiene el player.
    }
}

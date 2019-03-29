using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CinematicPlayer : MonoBehaviour {

    
    public GameObject player;
    public GameObject teleportAura;
    public GameObject teleportOrb;


    void Start()
    {

        player.SetActive(false);
        teleportAura.SetActive(false);
        teleportOrb.SetActive(false);

    }


    void Update()
    {
        StartCoroutine(AuraWaiter());
        StartCoroutine(OrbWaiter());
        StartCoroutine(PlayerWaiter()); 
        
    }

    IEnumerator AuraWaiter()
    {
        yield return new WaitForSecondsRealtime(2);
        teleportAura.SetActive(true);
        yield return new WaitForSecondsRealtime(8.3f);
        teleportAura.SetActive(false);
        // yield return new WaitForSecondsRealtime(0.5f);
        DestroyTeleportAura(); 
    }
    IEnumerator OrbWaiter()
    {
        yield return new WaitForSecondsRealtime(2);
        teleportOrb.SetActive(true);
      
        yield return new WaitForSecondsRealtime(8.5f);
        teleportOrb.SetActive(false);
        yield return new WaitForSecondsRealtime(0.3f);
        DestroyTeleportOrb(); 
        
    }

    IEnumerator PlayerWaiter()
    {
        yield return new WaitForSecondsRealtime(7);
        player.SetActive(true); 
    }

    void DestroyTeleportAura()
    {
        Destroy(teleportAura);
    }

    void DestroyTeleportOrb()
    {
        Destroy(teleportOrb);
    }


}

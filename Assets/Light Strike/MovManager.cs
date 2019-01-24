using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovManager : MonoBehaviour {

    public GameObject projectile;
    public ParticleSystem particles;

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            projectile.SetActive(true);
        }

        if (particles.IsAlive() == false)
            projectile.SetActive(false);
	}
}

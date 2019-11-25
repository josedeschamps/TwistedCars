using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes : MonoBehaviour {

    private int maxHealth = 1500;
    public int currentHealth { get; private set; }

    public Stats ATK;
    public Stats DEF;

    private void Awake()
    {
        currentHealth = maxHealth;
      
    }

}

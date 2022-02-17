using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShuttleScript : MonoBehaviour
{
    [Header("Shuttle Stats")]
    [SerializeField] float StartingHealth;
    public float CurrentHealth;
    [Header(" ")]

    [Header("UI")]
    public Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = StartingHealth;  
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        healthBar.fillAmount = CurrentHealth / StartingHealth;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundSystem : MonoBehaviour
{
    [Header("Tick Settings")]

    [SerializeField] float CurrentTick;
    [SerializeField] float TickSpeed;
    [SerializeField] float TickValue;


    [Header(" ")]
    [SerializeField] bool TickActive;
    [Header(" ")]


    [Header("DayNight cycle Settings")]
    [SerializeField] float DayTicks;
    [SerializeField] float NightTicks;
    [Header(" ")]


    [Header("Round Settings")]
    [SerializeField] int CurrentRound;
    [Header(" ")]


    [Header("Enemy Settings")]
    [SerializeField] GameObject Enemy;
    [SerializeField] Transform[] SpawnPoints;
    [SerializeField] float SpawnInterval;
    [SerializeField] int CurrentEnemiesToSpawn;
    [SerializeField] int DefaultEnemiesToSpawn;
    [SerializeField] int RoundEnemyIncreaser;
    [Header(" ")]


    [Header("Day Night Cycle")]
    [SerializeField] GameObject RotationPivot;
    [SerializeField] float LightAngle;
    [SerializeField] float OffsetAngle;



    private void Start()
    {
        TickActive = true;

        CurrentTick = 0;
        CurrentRound = 0;

        CurrentEnemiesToSpawn = DefaultEnemiesToSpawn;

        StartCoroutine(TickPass());
    }

    private void Update()
    {
        DayCycle();
        RoundCheck();
        AngleRotation();

        // Update Light angle
        RotationPivot.transform.rotation = Quaternion.Euler(LightAngle, OffsetAngle, 0);
    }

    void DayCycle()
    {
        if (CurrentTick >= 0 && CurrentTick <= DayTicks)
        {
            //Debug.Log("Its day time baby");
        }

        if (CurrentTick >= DayTicks && CurrentTick <= NightTicks)
        {
            //Debug.Log("Its night time baby");
        }

        if (CurrentEnemiesToSpawn <= 0)
        {
            CancelInvoke("SpawnDelay");
            StartCoroutine(ResetEnemiesSpawn());
        }
    }

    void RoundCheck()
    {
        if (CurrentTick >= NightTicks)
        {
            CurrentTick = 0f;
            CurrentRound++;
            DefaultEnemiesToSpawn = DefaultEnemiesToSpawn + RoundEnemyIncreaser;
            InvokeRepeating("SpawnDelay", 0, SpawnInterval);
        }
    }

    private void SpawnDelay()
    {
        int rand = Random.Range(0, SpawnPoints.Length);
        Instantiate(Enemy, SpawnPoints[rand].transform.position, Quaternion.identity);
        CurrentEnemiesToSpawn--;
    }

    void AngleRotation()
    {
        LightAngle = CurrentTick / (NightTicks / 360);
    }

    IEnumerator TickPass()
    {
        while (TickActive == true)
        {
            CurrentTick = CurrentTick + TickValue;
            yield return new WaitForSeconds(TickSpeed);
        }
    }

    IEnumerator ResetEnemiesSpawn()
    {
        yield return new WaitForSeconds(1f);
        CurrentEnemiesToSpawn = DefaultEnemiesToSpawn;
    }
}

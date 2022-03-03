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


    [Header("DayNight Cycle Settings")]
    [SerializeField] float DayTicks;
    [SerializeField] float NightTicks;


    [Header("Round Settings")]
    [SerializeField] int CurrentRound;
    [Header(" ")]
    [SerializeField] float Round1;
    [SerializeField] float Round2;
    [SerializeField] float Round3;
    [SerializeField] float Round4;
    [SerializeField] float Round5;


    [Header("Enemy Settings")]
    [SerializeField] GameObject Enemy;
    [Header(" ")]

    public Transform spawnPoint;

    [SerializeField] Transform[] SpawnPoints;
    [SerializeField] float SpawnInterval;
    [SerializeField] int CurrentEnemiesToSpawn;
    [SerializeField] int DefaultEnemiesToSpawn;
    [SerializeField] int RoundEnemyIncreaser;


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
        }

        if (CurrentTick == Round1)
        {
            Debug.Log("Starting round number " + CurrentRound);
            RoundMechanics();
        }
        if (CurrentTick == Round2)
        {
            Debug.Log("Starting round number " + CurrentRound);
            RoundMechanics();
        }
        if (CurrentTick == Round3)
        {
            Debug.Log("Starting round number " + CurrentRound);
            RoundMechanics();
        }
        if (CurrentTick == Round4)
        {
            Debug.Log("Starting round number " + CurrentRound);
            RoundMechanics();
        }
        if (CurrentTick == Round5)
        {
            Debug.Log("Starting round number " + CurrentRound);
            RoundMechanics();
        }
    }

    void RoundMechanics()
    {
        CurrentRound+=1;
        DefaultEnemiesToSpawn = DefaultEnemiesToSpawn + RoundEnemyIncreaser;
        InvokeRepeating("SpawnDelay", 0, SpawnInterval);
    }

    private void SpawnDelay()
    {
        int rand = Random.Range(0, SpawnPoints.Length);
        //Instantiate(Enemy, SpawnPoints[rand].transform.position, Quaternion.identity);

        //Start of Jack Code

        GameObject creep = ObjectPool.sharedInstance.GetObjectPooled();
        if (creep != null)
        {
            creep.transform.position = spawnPoint.position;
            creep.GetComponent<SplineFinding>().tParam = 0;
            creep.GetComponent<SplineFinding>().routeToGO = 0;
            creep.GetComponent<EnemyStats>().health = creep.GetComponent<EnemyStats>().baseHealth;
            creep.SetActive(true);
        }

        //End of Jack Code

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

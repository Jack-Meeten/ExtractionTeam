using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    [SerializeField] int MaxRounds = 50;
    [Header(" ")]
    [SerializeField] float Round1;
    [SerializeField] float Round2;
    [SerializeField] float Round3;
    [SerializeField] float Round4;
    [SerializeField] float Round5;
    [SerializeField] TextMeshProUGUI RoundText;

    [Header("Shuttle Settings")]
    [SerializeField] GameObject Shuttle;
    ShuttleScript ShuttleScr;
    [Header(" ")]

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

        ShuttleScr = Shuttle.GetComponent<ShuttleScript>();
    }

    private void Update()
    {
        DayCycle();
        RoundCheck();
        AngleRotation();
        SpeedStep();
        WinLose();

        // Update Light angle
        RotationPivot.transform.rotation = Quaternion.Euler(LightAngle, OffsetAngle, 0);

        // Update the round number on the UI
        RoundText.text = CurrentRound.ToString();
    }

    void DayCycle()
    {
        if (CurrentEnemiesToSpawn <= 0)
        {
            CancelInvoke("SpawnDelay");
            StartCoroutine(ResetEnemiesSpawn());
        }
    }

    void WinLose()
    {
        //Lose condition
        if (ShuttleScr.CurrentHealth <= 0)
        {
            SceneManager.LoadScene(0);
            Debug.Log("Player loses!");
        }

        //Win condition
        if (CurrentRound >= MaxRounds)
        {
            SceneManager.LoadScene(0);
            Debug.Log("Player wins!");
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

        //Start of yak Code

        GameObject creep = ObjectPool.sharedInstance.GetObjectPooled();//gets creep being managed
        if (creep != null)
        {
            //resets creep health and spline finding, reactivates it
            creep.GetComponent<SplineFinding>().tParam = 0;
            creep.GetComponent<SplineFinding>().routeToGO = 0;
            creep.GetComponent<SplineFinding>().coroutineAllowed = true;
            creep.GetComponent<EnemyStats>().health = creep.GetComponent<EnemyStats>().baseHealth * 1.002f;
            creep.SetActive(true);
            //Debug.Log(creep.name + " : is ACTIVE");
        }

        //End of yak Code

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

    public void SpeedStep()
    {
        if (Input.GetKeyDown(KeyCode.Period) && Time.timeScale < 99)
        {
            Debug.Log(Time.timeScale);
            Time.timeScale += 1;
            TickValue += 1;
        }
        if (Input.GetKeyDown(KeyCode.Comma) && Time.timeScale > 1)
        {
            Debug.Log(Time.timeScale);
            Time.timeScale -= 1;
            TickValue -= 1;
        }
        if (Input.GetKeyDown("/"))
        {
            Debug.Log(Time.timeScale);
            Time.timeScale = 1;
            TickValue = 1;
        }

        if (CurrentTick >= Round1 - 50 && CurrentTick <= Round1)
        {
            TickValue = 1;
            Time.timeScale = 1;
        }
        if (CurrentTick >= Round2 - 50 && CurrentTick <= Round2)
        {
            TickValue = 1;
            Time.timeScale = 1;
        }
        if (CurrentTick >= Round3 - 50 && CurrentTick <= Round3)
        {
            TickValue = 1;
            Time.timeScale = 1;
        }
        if (CurrentTick >= Round4 - 50 && CurrentTick <= Round4)
        {
            TickValue = 1;
            Time.timeScale = 1;
        }
        if (CurrentTick >= Round5 - 50 && CurrentTick <= Round5)
        {
            TickValue = 1;
            Time.timeScale = 1;
        }
    }
}

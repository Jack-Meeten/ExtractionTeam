using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class RoundSystem : MonoBehaviour
{
    [Header("Tick Settings")]
    [Header(" ")]
    [SerializeField] float CurrentTick;
    [SerializeField] float TickSpeed;
    [SerializeField] float TickValue;


    [Header(" ")]
    [SerializeField] bool TickActive;


    [Header("DayNight Cycle Settings")]
    [Header(" ")]
    [SerializeField] float DayTicks;
    [SerializeField] float NightTicks;


    [Header("Round Settings")]
    [Header(" ")]
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
    [Header(" ")]
    [SerializeField] GameObject Shuttle;
    ShuttleScript ShuttleScr;
    [Header(" ")]


    [Header("Enemy Settings")]
    [Header(" ")]
    [SerializeField] GameObject Enemy;
    [Header(" ")]

    public Transform spawnPoint;

    [SerializeField] Transform[] SpawnPoints;
    [SerializeField] float SpawnInterval;
    [SerializeField] int CurrentEnemiesToSpawn;
    [SerializeField] int DefaultEnemiesToSpawn;
    [SerializeField] int RoundEnemyIncreaser;
    [Header(" ")]


    [Header("Day Night Cycle")]
    [Header(" ")]
    [SerializeField] GameObject RotationPivot;
    [SerializeField] float LightAngle;
    [SerializeField] float OffsetAngle;
    [Header(" ")]

    [Header("Win/Lose Screens")]
    [SerializeField] GameObject Options_Holder;
    OptionsMenu OptionsScr;
    [Header(" ")]

    // Yak's magic code
    int rand;

    List<int> hashList = new List<int>();
    int techNum = -1;

    [SerializeField] GameObject CameraHolder;

    float expHealth = 1.02f;

    void Start()
    {
        TickActive = true;

        CurrentTick = 0;
        CurrentRound = 0;

        CurrentEnemiesToSpawn = DefaultEnemiesToSpawn;


        ShuttleScr = Shuttle.GetComponent<ShuttleScript>();
        OptionsScr = Options_Holder.GetComponent<OptionsMenu>();

        StartCoroutine(TickPass());
       
        ButtonSetUp();

        Time.timeScale = 1;
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

        // Enemy Cap
        if (CurrentEnemiesToSpawn >= 65) CurrentEnemiesToSpawn = 65;
        if (CurrentRound == 25)
        {
            expHealth = 1.06f;
        }
        if (CurrentRound == 25)
        {
            expHealth = 1.12f;
        }
        if (CurrentRound == 45)
        {
            expHealth = 1.24f;
        }
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
            OptionsScr.Lose();
        }

        //Win condition
        if (CurrentRound >= MaxRounds)
        {
            OptionsScr.Win();
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void RoundCheck()
    {
        if (CurrentTick >= NightTicks)
        {
            CurrentTick = 0f;
        }

        if (CurrentTick == Round1)
        {
            //Debug.Log("Starting round number " + CurrentRound);
            RoundMechanics();            
        }
        if (CurrentTick == Round2)
        {
            //Debug.Log("Starting round number " + CurrentRound);
            RoundMechanics();
        }
        if (CurrentTick == Round3)
        {
            //Debug.Log("Starting round number " + CurrentRound);
            RoundMechanics();
        }
        if (CurrentTick == Round4)
        {
            //Debug.Log("Starting round number " + CurrentRound);
            RoundMechanics();
        }
        if (CurrentTick == Round5)
        {
            //Debug.Log("Starting round number " + CurrentRound);
            RoundMechanics();

            techNum ++;
            CheckTechnology(techNum);
        }
    }

    void RoundMechanics()
    {
        CurrentRound+=1;
        rand = Random.Range(0, SpawnPoints.Length);
        DefaultEnemiesToSpawn = DefaultEnemiesToSpawn + RoundEnemyIncreaser;
        InvokeRepeating("SpawnDelay", 0, SpawnInterval);
        foreach (var item in FindObjectOfType<ObjectPool>().GetComponent<ObjectPool>().pooledObjects)
        {
            item.GetComponent<EnemyStats>().baseHealth = item.GetComponent<EnemyStats>().baseHealth * expHealth;
        }
    }

    private void SpawnDelay()
    {
        //Start of yak Code

        GameObject creep = ObjectPool.sharedInstance.GetObjectPooled();//gets creep being managed
        if (creep != null)
        {
            //resets creep health and spline finding, reactivates it
            creep.GetComponent<SplineFinding>().routes = SpawnPoints[rand];
            creep.GetComponent<SplineFinding>().speedModifier = 0.1f;
            creep.GetComponent<SplineFinding>().tParam = 0;
            creep.GetComponent<SplineFinding>().routeToGO = 0;
            creep.GetComponent<SplineFinding>().coroutineAllowed = true;
            creep.GetComponent<EnemyStats>().health = creep.GetComponent<EnemyStats>().baseHealth;
            creep.SetActive(true);
            //Debug.Log(creep.name + " : is ACTIVE");
        }

        //End of yak's magic Code

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
            CurrentTick += TickValue;
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
        //if (Input.GetKeyDown(KeyCode.Period) && Time.timeScale < 99)
        //{
        //    //Debug.Log(Time.timeScale);
        //    Time.timeScale += 1;
        //    TickValue += 1;
        //}
        //if (Input.GetKeyDown(KeyCode.Comma) && Time.timeScale > 1)
        //{
        //    //Debug.Log(Time.timeScale);
        //    Time.timeScale -= 1;
        //    TickValue -= 1;
        //}
        //if (Input.GetKeyDown("/"))
        //{
        //    //Debug.Log(Time.timeScale);
        //    Time.timeScale = 1;
        //    TickValue = 1;
        //}

        //if (CurrentTick >= Round1 - 50 && CurrentTick <= Round1)
        //{
        //    TickValue = 1;
        //    Time.timeScale = 1;
        //}
        //if (CurrentTick >= Round2 - 50 && CurrentTick <= Round2)
        //{
        //    TickValue = 1;
        //    Time.timeScale = 1;
        //}
        //if (CurrentTick >= Round3 - 50 && CurrentTick <= Round3)
        //{
        //    TickValue = 1;
        //    Time.timeScale = 1;
        //}
        //if (CurrentTick >= Round4 - 50 && CurrentTick <= Round4)
        //{
        //    TickValue = 1;
        //    Time.timeScale = 1;
        //}
        //if (CurrentTick >= Round5 - 50 && CurrentTick <= Round5)
        //{
        //    TickValue = 1;
        //    Time.timeScale = 1;
        //}
    }

    void ButtonSetUp()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Tech"))
        {
            //Debug.Log(item + " Hash ID:   " + item.GetHashCode());
            hashList.Add(item.GetHashCode());
            item.name = item.GetHashCode().ToString();
        }
    }

    void CheckTechnology(int techNum)
    {
        GameObject unlocking = GameObject.Find(hashList[techNum].ToString());
        //Debug.Log(unlocking);
        unlocking.GetComponent<Button>().enabled = true;
        unlocking.transform.GetChild(1).gameObject.SetActive(false);
    }
}
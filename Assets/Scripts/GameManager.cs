using Game.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

    public Transform playerSpawn;
    public Transform objectiveSpawn;

    public AudioClip beginSound;
    public AudioClip winSound;
    public AudioClip loseSound;

    public Objective objective;
    public uGUI_Result gameResult;

    public float lampBreakLerp = 2f;
    public float lampBreakWait = 5f;


    public float currentTime;
    public float endTime;
    public bool isPlaying;
    public bool hasLampBroke;

    public MatchPoint[] matchPoints;
    public int currentMatchPoint;

    public delegate void OnGameEnd();
    public event OnGameEnd onGameEnd;

    public UnityEvent onBegin;
    public UnityEvent onEnd;
    public UnityEvent onLampBreak;

    [SerializeField] GameObject objective_prefab;

    private void Awake()
    {
        main = this;
        currentTime = 0.0f;
        isPlaying = false;
        hasLampBroke = false;

        foreach (MatchPoint mp in matchPoints)
        {
            mp.Disactivate();
        }

    }

    public void Begin()
    {
        if (isPlaying)
        {
            return;
        }
        onBegin?.Invoke();
        AudioSystem.PlaySound(beginSound, transform.position, 1f, 128);
        isPlaying = true;
        currentTime = 0.0f;
        SetMatchPoint(0);
        hasLampBroke = false;
        if (objective == null)
        {
            objective = GameObject.FindObjectOfType<Objective>();
            if (objective == null)
            {
                objective = GameObject.Instantiate(objective_prefab).GetComponent<Objective>();
            }
        }
        Player.main.transform.position = playerSpawn.position;
        Player.main.look.canLook = true;
        Player.main.movement.canMove = true;
        objective.transform.position = objectiveSpawn.position;
        onBegin?.Invoke();
    }
    public void DoLampEvent()
    {
        onLampBreak?.Invoke();
        StartCoroutine(GameManager.main.LampBreakEvent());
    }
    public IEnumerator LampBreakEvent()
    {
        hasLampBroke = true;
        Player.main.look.canLook = false;
        Player.main.movement.canMove = false; ;
        Transform aimpoint = Player.main.look.aimPoint.transform;
        aimpoint.SetParent(null);
        aimpoint.transform.position = objectiveSpawn.transform.position;
        AudioSystem.PlaySound(loseSound, transform.position, 1f, 128);
        yield return new WaitForSeconds(loseSound.length + lampBreakWait);
        Debug.Log("AAH");
        aimpoint.SetParent(Player.main.transform);
        End(false, playSound: false);
    }
    public void End(bool success, bool playSound = true)
    {
        if (!isPlaying)
        {
            return;
        }
        StartCoroutine(c_end(success, playSound));
    }
    private IEnumerator c_end(bool success, bool playSound)
    {
        onGameEnd?.Invoke();
        hasLampBroke = false;
        MusicManager.main.StopMusic();
        Player.main.mixin.Heal(1000);
        Gun g = Player.main.GetComponentInChildren<Gun>();
        g.currentAmmo = g.gun.AmmoLimit;
        Player.main.look.aimPoint.transform.position = Vector3.zero;
        Player.main.look.canLook = false;
        Player.main.movement.canMove = false;
        matchPoints[currentMatchPoint].Disactivate();
        isPlaying = false;
        currentTime = 0f;
        currentMatchPoint = 0;
        AudioClip cl;
        if (success)
        {
            gameResult.SetWin();
            cl = winSound;
        }
        else
        {
            gameResult.SetLose();
            cl = loseSound;
        }
        if (playSound)
        {
            AudioSystem.PlaySound(cl, transform.position, 1f, 128);
            yield return new WaitForSeconds(cl.length * 1.5f);
        }
        if (!isPlaying)
        {
            onEnd?.Invoke();
        }
        yield return null;
    }
    public void Update()
    {
        if (isPlaying)
        {
            currentTime += Time.deltaTime;
            if (currentTime > endTime)
            {
                End(true);
                return;
            }
            if (currentMatchPoint + 1 < matchPoints.Length)
            {
                if (!(currentTime >= matchPoints[currentMatchPoint].timePoint && currentTime < matchPoints[currentMatchPoint + 1].timePoint))
                {
                    SetMatchPoint(currentMatchPoint + 1);
                }
            }
        }
    }
    private void SetMatchPoint(int point)
    {
        int oldPoint = currentMatchPoint;
        currentMatchPoint = point;
        matchPoints[oldPoint].Disactivate();
        matchPoints[currentMatchPoint].Activate();
    }

    [Serializable]
    public class MatchPoint
    {
        public float timePoint = 0.0f;
        public bool isBoss = false;
        public GameObject[] objects;

        public MatchPoint(float timePoint, bool isBossBattle)
        {
            this.timePoint = timePoint;
            this.isBoss = isBossBattle;
        }
        public void Activate()
        {
            foreach (GameObject objj in objects)
            {
                objj.SetActive(true);
            }
        }
        public void Disactivate()
        {
            foreach (GameObject oldObj in objects)
            {
                oldObj.SetActive(false);
            }
        }
    }
}

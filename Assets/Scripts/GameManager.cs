using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

    public Transform playerSpawn;
    public Transform objectiveSpawn;

    public Objective objective;
    public float currentTime;
    public float endTime;
    public bool isPlaying;

    public MatchPoint[] matchPoints;
    public int currentMatchPoint;

    public UnityEvent onBegin;
    public UnityEvent onEnd;

    private void Awake()
    {
        main = this;
        currentTime = 0.0f;
        isPlaying = false;

        foreach (MatchPoint mp in matchPoints)
        {
            mp.Disactivate();
        }

    }
    private void OnEnable()
    {
        Debug.Log("OnBegin");
        Begin();
    }

    public void Begin()
    {
        if (isPlaying)
        {
            return;
        }
        onBegin?.Invoke();
        isPlaying = true;
        currentTime = 0.0f;
        SetMatchPoint(0);
        if (objective == null)
        {
            objective = GameObject.FindObjectOfType<Objective>();
            if (objective == null)
            {
                //TODO SPAWN OBJECTIVE IN CASE THERE ARE NO OBJECTIVE
                throw new NotImplementedException();
            }
        }
        onBegin?.Invoke();
    }

    public void End(bool success)
    {
        if (!isPlaying)
        {
            return;
        }
        onEnd?.Invoke();
        matchPoints[currentMatchPoint].Disactivate();
        isPlaying = false;
        currentTime = 0f;
        currentMatchPoint = 0;
        onEnd?.Invoke();
        if (success)
        {

        }
        else
        {

        }
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

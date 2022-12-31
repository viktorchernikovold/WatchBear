using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

    public Objective objective;
    public float currentTime;
    public float endTime;
    public bool isPlaying;

    public MatchPoint[] matchPoints;
    public int currentMatchPoint;

    private void Awake()
    {
        main = this;
        currentTime = 0.0f;
        isPlaying = false;

        foreach (MatchPoint mp in matchPoints)
            mp.ObjectToEnable.SetActive(false);

        Begin();
    }

    public void Begin()
    {
        if (isPlaying)
        {
            return;
        }
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
    }

    public void End(bool success)
    {
        if (!isPlaying)
        {
            return;
        }
        isPlaying = false;
        currentTime = 0f;
        currentMatchPoint = 0;

        if (success)
        {
            throw new NotImplementedException();
        }
        else
        {
            throw new NotImplementedException();
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
            if (currentMatchPoint < matchPoints.Length)
            {
                if (!(currentTime >= matchPoints[currentMatchPoint].timePoint && currentTime < matchPoints[currentMatchPoint + 1].timePoint))
                {
                    SetMatchPoint(++currentMatchPoint);
                }
            }
        }
    }
    private void SetMatchPoint(int point)
    {
        currentMatchPoint = point;

        foreach (MatchPoint mp in matchPoints)
            mp.ObjectToEnable.SetActive(false);

        matchPoints[point].ObjectToEnable.SetActive(true);
    }

    [Serializable]
    public class MatchPoint
    {
        public float timePoint = 0.0f;
        public bool isBoss = false;
        public GameObject ObjectToEnable;

        public MatchPoint(float timePoint, bool isBossBattle)
        {
            this.timePoint = timePoint;
            this.isBoss = isBossBattle;
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager main;

    public Transform playerSpawn;
    public Transform objectiveSpawn;

    public Objective objective;
    public uGUI_Result gameResult;

    public float currentTime;
    public float endTime;
    public bool isPlaying;

    public MatchPoint[] matchPoints;
    public int currentMatchPoint;

    public UnityEvent onBegin;
    public UnityEvent onEnd;


    [SerializeField] GameObject objective_prefab;

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
            objective = FindObjectOfType<Objective>();
            if (objective == null)
            {
                objective = Instantiate(objective_prefab).GetComponent<Objective>();
            }
        }
        Player.main.transform.position = playerSpawn.position;
        objective.transform.position = objectiveSpawn.position;
        onBegin?.Invoke();
    }

    public void End(bool success)
    {
        if (!isPlaying)
        {
            return;
        }
        onEnd?.Invoke();
        Player.main.transform.position = new Vector3(0, 10000, 0);
        Player.main.mixin.Heal(1000);
        Player.main.look.aimPoint.transform.position = Vector3.zero;
        matchPoints[currentMatchPoint].Disactivate();
        isPlaying = false;
        currentTime = 0f;
        currentMatchPoint = 0;
        onEnd?.Invoke();
        if (success)
        {
            gameResult.SetWin();
        }
        else
        {
            gameResult.SetLose();
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

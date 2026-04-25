using UnityEngine;
using Debug = UnityEngine.Debug;

public class Powerplant : MonoBehaviour
{
    int hp;

    [SerializeField]
    int maxHp;

    long timeLimit;

    [SerializeField]
    long maxTimeSeconds;

    [SerializeField]
    TimeController timeController;
    [SerializeField]
    HealthController healthController;

    internal GameLoopController gameLoopController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartPowerplant();   
    }

    public void StartPowerplant()
    {
        hp = maxHp;
        timeLimit = maxTimeSeconds * 1000;
        healthController.UpdateHealth(hp);
        timeController.SetTime(timeLimit);

        Debug.Log("Starting at " + timeLimit);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTime();

        if (timeLimit == 0)
            WinGame();

        if (hp == 0)
            GameOver();
    }

    private void WinGame()
    {
        gameLoopController.WinGame();
    }

    private void GameOver()
    {
        gameLoopController.EndGame();
    }

    private void UpdateTime()
    {
        timeLimit -= (int)(Time.deltaTime * 1000);
        if (timeLimit < 0)
            timeLimit = 0;
        timeController.SetTime(timeLimit);
    }

    public void TakeDamage(int hitPoints)
    {
        this.hp -= hitPoints;
        if (this.hp < 0)
            this.hp = 0;
        healthController.UpdateHealth(hp);
    }
}

using UnityEngine;

public class GameLoopController : MonoBehaviour
{
    [SerializeField]
    Powerplant powerplant;

    [SerializeField]
    GameObject deathPanel;

    private void Update()
    {
        if (powerplant.isExploded)
            EndGame();
    }

    public void StartGame()
    {
        powerplant.StartPowerplant();
    }

    public void EndGame()
    {
        deathPanel.SetActive(true);
    }


}

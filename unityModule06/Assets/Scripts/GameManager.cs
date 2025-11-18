using UnityEngine;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    public bool preLifeIsGameOvered = false;
    public int continueCount = 0;
    
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(this);
        instance = this;
    }

    public void GameOver()
    {
        preLifeIsGameOvered = true;
        continueCount += 1;
    }

    public void GameClear()
    {
        preLifeIsGameOvered = false;
        continueCount += 1;
    }
}

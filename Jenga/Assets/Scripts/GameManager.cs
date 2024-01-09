using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static bool hasLost;
    public static GameManager self;
    [SerializeField] private string[] loseMessages;
    [SerializeField] private string keepTag;
    [SerializeField] private UnityEvent<string> onLose;
    [SerializeField] private UnityEvent<string> onTurn;
    private int turn;

    private int GetPlayer(int newTurn)
    {
        return newTurn % 2 + 1;
    }
    private void LoadTurn()
    {
        onTurn.Invoke($"Player {GetPlayer(turn)}");
    }
    public void NextTurn()
    {
        turn++;
        LoadTurn();
    }
    private void Start()
    {
        self = this;
        hasLost = false;
        Time.timeScale = 1.0f;
        LoadTurn();
    }
    public void Lose()
    {
        hasLost = true;
        onLose.Invoke($"Player { GetPlayer(turn + 1) } won!\n{ loseMessages[Random.Range(0, loseMessages.Length)] }");
        Time.timeScale = 0.0f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(keepTag)) return;
        Lose();
    }
}

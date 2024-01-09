using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static bool hasLost;
    [SerializeField] private string keepTag;
    [SerializeField] private UnityEvent onLose;

    public void Lose()
    {
        hasLost = true;
        onLose.Invoke();
        Time.timeScale = 0.0f;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(keepTag)) return;

        Lose();
    }
}

using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class WeightWriter : MonoBehaviour
{
    [SerializeField] private PostProcessVolume post;

    public void SetWeight(float newWeight)
    {
        post.weight= newWeight;
    }
}

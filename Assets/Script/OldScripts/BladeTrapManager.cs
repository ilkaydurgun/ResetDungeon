using UnityEngine;

public class BladeTrapManager : MonoBehaviour
{
    public BladeTrapAuto[] blades;
    public float delayBetweenBlades = 0.5f;

    void Start()
    {
        if (blades == null || blades.Length == 0)
            blades = FindObjectsOfType<BladeTrapAuto>();

        for (int i = 0; i < blades.Length; i++)
        {
            blades[i].startDelay = i * delayBetweenBlades;
        }
    }
}

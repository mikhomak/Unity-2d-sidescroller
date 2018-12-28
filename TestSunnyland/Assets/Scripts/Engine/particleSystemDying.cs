

using UnityEngine;

public class particleSystemDying : MonoBehaviour {

    void Awake()
    {
        Destroy(gameObject, 0.9f);
    }
}

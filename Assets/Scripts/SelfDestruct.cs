using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField]
    private float timer;

    public void setTimer(float time)
    {
        timer = time;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}

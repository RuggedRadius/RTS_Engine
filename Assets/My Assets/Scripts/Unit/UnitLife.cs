using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitLife : MonoBehaviour
{
    public bool alive;
    [SerializeField]
    public int lifeCurrent;
    public int lifeMaximum;

    private void Awake()
    {
        alive = true;
        lifeCurrent = lifeMaximum;
    }

    void Start()
    {

    }

    void Update()
    {
        if (alive)
        {
            if (lifeCurrent <= 0)
            {
                alive = false;
            }
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void TakeDamage(int amount)
    {
        amount = Mathf.Clamp(amount, 0, amount);
        lifeCurrent -= amount;
    }

    public void TakeHealing(int amount)
    {
        amount = Mathf.Clamp(amount, 0, amount);
        lifeCurrent += amount;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitLife : MonoBehaviour
{
    public bool alive;
    [SerializeField]
    public int lifeCurrent;
    public int lifeMaximum;

    [SerializeField]
    private Image worldLifeBar;

    //[SerializeField]
    private Transform modelTransform;

    [SerializeField]
    private Material materialDamage;

    private void Awake()
    {
        alive = true;
        lifeCurrent = lifeMaximum;
        modelTransform = this.transform.Find("Model");

        worldLifeBar.gameObject.SetActive(true);
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

        worldLifeBar.color = GetCurrentLifeBarColour();
    }


    public void TakeDamage(int amount)
    {
        StartCoroutine(damageRoutine(amount));
    }

    bool displayingDamage;
    private IEnumerator damageRoutine(int amount)
    {
        amount = Mathf.Clamp(amount, 0, amount);
        lifeCurrent -= amount;

        if (!displayingDamage)
        {
            displayingDamage = true;

            // Affect shaders
            MeshRenderer[] renderers = modelTransform.GetComponentsInChildren<MeshRenderer>();

            // Store original materials
            Material[] originalMats = new Material[renderers.Length];
            for (int i = 0; i < renderers.Length; i++)
            {
                originalMats[i] = renderers[i].material;
            }

            // Change all materials to red
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material = materialDamage;
            }

            yield return new WaitForSeconds(0.01f);

            // Revert to original materials
            for (int i = 0; i < renderers.Length; i++)
            {
                renderers[i].material = originalMats[i];
            }

            displayingDamage = false;
        }

        yield return null;
    }

    public void TakeHealing(int amount)
    {
        amount = Mathf.Clamp(amount, 0, amount);
        lifeCurrent += amount;
    }

    float currentLifeRatio;
    private Color GetCurrentLifeBarColour()
    {
        currentLifeRatio = (float)lifeCurrent / (float)lifeMaximum;
        return Color.Lerp(Color.red, Color.green, currentLifeRatio);
    }
}

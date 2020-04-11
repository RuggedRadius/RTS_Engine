using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Resource
{
    [SerializeField]
    public ResourceType type;
    [SerializeField]
    public int currentAmount;
    [SerializeField]
    public Sprite sprite;

    public void IncreaseAmount(int amount)
    {
        amount = Mathf.Clamp(amount, 0, amount);
        currentAmount += amount;
    }
    public void DecreaseAmount(int amount)
    {
        amount = Mathf.Clamp(amount, 0, amount);
        currentAmount -= amount;
    }
}

[Serializable]
public enum ResourceType
{
    Gold,
    Wood,
    Food
}

public class ResourcesManager : MonoBehaviour
{
    [SerializeField]
    public List<Resource> resources;

    [Header("Required References")]
    [SerializeField]
    private GameObject topPanel;
    [SerializeField]
    private GameObject resourcePanelPrefab;

    private List<GameObject> activeResourcePanels;


    void Start()
    {
        createUIPanels();
    }

    void Update()
    {
        updateUIValues();
    }

    private void updateUIValues()
    {
        for (int i = 0; i < resources.Count; i++)
        {
            // Create new panel
            GameObject currentResource = activeResourcePanels[i];

            // Populate new panel
            Text resourceValue = currentResource.GetComponentInChildren<Text>();
            resourceValue.text = string.Format("{0}: {1}", resources[i].type.ToString(), resources[i].currentAmount);
        }
    }

    private void createUIPanels()
    {
        // Create new list
        activeResourcePanels = new List<GameObject>();

        foreach (Resource resource in resources)
        {
            // Create new panel
            GameObject newResourcePanel = Instantiate(resourcePanelPrefab);
            newResourcePanel.transform.SetParent(topPanel.transform);
            newResourcePanel.name = resource.type.ToString();

            // Populate new panel
            Text resourceValue = newResourcePanel.GetComponentInChildren<Text>();
            resourceValue.text = string.Format("{0}: {1}", resource.type.ToString(), resource.currentAmount);

            Image resourceImage = newResourcePanel.GetComponentInChildren<Image>();
            resourceImage.sprite = resource.sprite;
            Destroy(resourceImage);

            // Add panel to active list
            activeResourcePanels.Add(newResourcePanel);
        }
    }

    public Resource getResource(ResourceType _type)
    {
        foreach (Resource resource in resources)
        {
            if (resource.type == _type)
            {
                return resource;
            }
        }
        return null;
    }
    public void DecreaseResource(ResourceType type, int amount)
    {
        foreach (Resource resource in resources)
        {
            if (resource.type == type)
            {
                resource.DecreaseAmount(amount);
            }
        }
    }
    public void IncreaseResource(ResourceType type, int amount)
    {
        foreach (Resource resource in resources)
        {
            if (resource.type == type)
            {
                resource.IncreaseAmount(amount);
            }
        }
    }
}


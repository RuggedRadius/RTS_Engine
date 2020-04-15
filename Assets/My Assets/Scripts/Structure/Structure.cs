using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using static Interfaces;
using static Unit;

[Serializable]
public class Structure : MonoBehaviour, IUnitProducing
{
    [SerializeField]
    public string structureName;
    [SerializeField]
    public List<GameObject> producableUnits;

    private Team team;

    [SerializeField]
    public GameObject productionFXPrefab;
    [SerializeField]
    public Sprite uiTileSprite;

    [SerializeField]
    public Queue<Unit> productionQueue;
    public float currentProductionProgress;

    [SerializeField]
    private Text productionText;



    public bool creatingUnit;

    [SerializeField]
    Terrain terrain;

    private ResourcesManager resourcesManager;

    private void Awake()
    {
        // Initialise script references
        resourcesManager = GameObject.FindGameObjectWithTag("ResourceManager").GetComponent<ResourcesManager>();
        productionQueue = new Queue<Unit>();
        team = transform.root.GetComponent<TeamScript>().team;
    }

    void Update()
    {
        // If queue contains units, produce them
        if (productionQueue.Count > 0)
        {
            if (!creatingUnit)
            {
                StartCoroutine(unitCreation(productionQueue.Peek()));
                productionQueue.Dequeue();
            }
        }
    }

    public void createUnit(Unit unit)
    {
        if (productionQueue.Count < 24)
        {
            // If not returned by now, resources are available
            productionQueue.Enqueue(unit);
        }
        else
        {
            print("Queue is full!");
            return;
        }

        foreach (ResourceCost rc in unit.resourceCosts)
        {
            // Determine amounts
            int requiredAmount = rc.cost;
            int currentAmount = resourcesManager.getResource(rc.type).currentAmount;

            // Determine is current amount is enough
            if ((currentAmount - requiredAmount) < 0)
            {
                // Not enough resources
                Debug.Log("Not enough " + rc.type + " to create " + unit.unitName);
                return;
            }
            else
            {
                // Reduce resource by cost amounts
                resourcesManager.DecreaseResource(rc.type, rc.cost);
            }
        }
    }

    private IEnumerator unitCreation(Unit unit)
    {
        creatingUnit = true;
        currentProductionProgress = 0f;

        // Start FX
        GameObject fx = Instantiate(productionFXPrefab);
        fx.transform.parent = this.transform;
        fx.transform.localPosition = Vector3.zero;
        fx.transform.localScale = Vector3.one * 10f;

        // Build time wait
        float timer = 0f;
        while (timer <= unit.unitBuildTime)
        {
            productionText.transform.parent.LookAt(Camera.main.transform);
            currentProductionProgress = (timer / unit.unitBuildTime) * 100f;
            productionText.text = (int)currentProductionProgress + "%";
            timer += Time.deltaTime;
            yield return null;
        }
        productionText.text = "";

        // Spawn Unit
        GameObject newUnit = Instantiate(unit.unitPrefab);
        newUnit.transform.parent = this.transform.root.Find("Units");
        newUnit.GetComponent<Unit>().team = team;

        // Position unit
        Vector3 finalPosition = this.transform.position;
        finalPosition += -transform.forward * 6;
        newUnit.transform.position = finalPosition;
        newUnit.GetComponent<NavMeshAgent>().enabled = false;
        newUnit.GetComponent<NavMeshAgent>().enabled = true;

        // Stop FX
        Destroy(fx);

        creatingUnit = false;
        yield return null;
    }
}

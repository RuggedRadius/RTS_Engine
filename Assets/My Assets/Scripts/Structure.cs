using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using static Interfaces;
using static Unit;

[Serializable]
public class Structure : MonoBehaviour, IUnitProducing
{
    [SerializeField]
    private ResourcesManager resourcesManager;

    [SerializeField]
    public string structureName;

    [SerializeField]
    public List<GameObject> producables;

    [SerializeField]
    public GameObject productionFXPrefab;

    public bool creatingUnit;

    [SerializeField]
    Terrain terrain;

    private void Awake()
    {
        // Initialise script references
        resourcesManager = GameObject.FindGameObjectWithTag("ResourceManager").GetComponent<ResourcesManager>();
    }

    public void createUnit(Unit unit)
    {
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

        // If not returned by now, resources are available
        if (creatingUnit)
        {
            // Add new unit to que
            //..
        }
        else
        {
            // Train unit
            StartCoroutine(unitCreation(unit));
        }
    }

    private IEnumerator unitCreation(Unit unit)
    {
        creatingUnit = true;

        // Start FX
        GameObject fx = Instantiate(productionFXPrefab);
        fx.transform.parent = this.transform;
        fx.transform.localPosition = Vector3.zero;
        fx.transform.localScale = Vector3.one * 10f;

        // UI Stuff
        //...

        // Build time wait
        yield return new WaitForSeconds(unit.unitBuildTime);

        // UI Stuff
        //...

        // Spanw Unit
        GameObject newUnit = Instantiate(unit.prefab);
        newUnit.transform.parent = this.transform.root.Find("Units");

        // Position unit
        Vector3 positionRandomisation = new Vector3(UnityEngine.Random.Range(-3f, 3f), 0f, UnityEngine.Random.Range(-3f, 3f));
        Vector3 finalPosition = this.transform.position + positionRandomisation;
        //NavMesh.sa
        bool success = NavMesh.SamplePosition(finalPosition, out NavMeshHit hit, 1000, 8);
        if (success)
        {
            newUnit.transform.position = hit.position;
        }
        else
        {
            newUnit.transform.position = finalPosition;

        }

        // Stop FX
        Destroy(fx);

        creatingUnit = false;
        yield return null;
    }
}

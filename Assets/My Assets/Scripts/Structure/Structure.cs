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
    private GameManager gm;
    private SelectionManager sm;


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

    [SerializeField]
    private Vector3 rallyPointLocation;
    [SerializeField]
    private GameObject rallyPointLocationPrefab;
    private GameObject rallyPoint;

    private Vector3 defaultSpawnLocation;



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
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        sm = gm.GetComponentInChildren<SelectionManager>();
    }

    public void Start()
    {
        // Set default spawn location
        defaultSpawnLocation = this.transform.position + (30 * -transform.forward);

        // Check height at location
        Ray ray = new Ray(defaultSpawnLocation + (100 * Vector3.up), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 10000, 256, QueryTriggerInteraction.Ignore))
        {
            defaultSpawnLocation = new Vector3(
                defaultSpawnLocation.x,
                hit.point.y,
                defaultSpawnLocation.z
                );
        }

        // Set default rally point to spawn location
        rallyPointLocation = defaultSpawnLocation;
    }

    void Update()
    {
        ProcessQueue();
        DisplayRallyPoint();
    }

    public void UpdateRallyPoint(Vector3 worldPosition)
    {
        rallyPointLocation = worldPosition;
        if (rallyPoint == null)
        {
            rallyPoint = Instantiate(rallyPointLocationPrefab);
            rallyPoint.transform.SetParent(this.transform);
        }
        rallyPoint.transform.position = rallyPointLocation;
    }

    private void DisplayRallyPoint()
    {
        if (sm.currentSelection.Contains(this))
        {
            // Selected, show rally point
            if (rallyPoint == null)
            {
                rallyPoint = Instantiate(rallyPointLocationPrefab);
                rallyPoint.transform.SetParent(this.transform);
                rallyPoint.transform.position = rallyPointLocation;
            }
        }
        else
        {
            // Not selected, hide rally point
            if (rallyPoint != null)
            {
                Destroy(rallyPoint);
            }    
        }
    }

    private void ProcessQueue()
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

        Ray ray = new Ray(finalPosition + (100 * Vector3.up), Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 10000, 256, QueryTriggerInteraction.Ignore))
        {
            finalPosition = new Vector3(
                finalPosition.x,
                hit.point.y,
                finalPosition.z
                );
        }

        // Place unit out front of structure
        newUnit.transform.position = finalPosition;

        // Toggle NavMesh agent to initialise it to baked NavMesh
        newUnit.GetComponent<NavMeshAgent>().enabled = false;
        newUnit.GetComponent<NavMeshAgent>().enabled = true;

        // Small pause to let unit intialise
        yield return new WaitForSeconds(0.1f);

        // Move to rally point
        newUnit.GetComponent<Unit>().move(rallyPointLocation);

        // Stop FX
        Destroy(fx);

        creatingUnit = false;
        yield return null;
    }
}

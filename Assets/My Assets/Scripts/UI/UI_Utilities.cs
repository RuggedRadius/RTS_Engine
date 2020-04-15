using UnityEngine;
using UnityEngine.UI;

public class UI_Utilities : MonoBehaviour
{
    public UI_Manager uiManager;

    [SerializeField]
    private GameObject uiTilePrefab;

    public GameObject createTile(Unit unit)
    {
        // Create button prefab
        GameObject newTile = Instantiate(uiTilePrefab);
        newTile.transform.SetParent(uiManager.panelSelection.transform);

        // Set Sprite
        Button btn = newTile.GetComponent<Button>();
        Image btnImage = newTile.GetComponent<Image>();
        btnImage.sprite = unit.uiTileSprite;

        // Add OnClick Events
        //...

        return newTile;
    }
    public GameObject createTile(Structure structure)
    {
        // Create button prefab
        GameObject newTile = Instantiate(uiTilePrefab);
        newTile.transform.parent = uiManager.panelSelection.transform;

        // Set Sprite
        Button btn = newTile.GetComponent<Button>();
        Image btnImage = newTile.GetComponent<Image>();
        btnImage.sprite = structure.uiTileSprite;

        // Add OnClick Events
        //...

        return newTile;
    }
    public GameObject createTile(Action action)
    {
        // Create button prefab
        GameObject newTile = Instantiate(uiTilePrefab);
        newTile.transform.SetParent(uiManager.panelAction.transform);

        // Set Sprite
        Button btn = newTile.GetComponent<Button>();
        Image btnImage = newTile.GetComponent<Image>();
        btnImage.sprite = action.uiTileSprite;

        // Add OnClick Events
        //...

        return newTile;
    }
}

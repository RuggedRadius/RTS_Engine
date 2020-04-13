using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBarDisplay : MonoBehaviour
{
    private Unit unit;

    [SerializeField]
    private RawImage imgValue;

    private void Start()
    {
        unit = this.transform.root.GetComponent<Unit>();
    }

    void Update()
    {
        float lifePercentage = (unit.unitLife.lifeCurrent / unit.unitLife.lifeMaximum);
        imgValue.GetComponent<RectTransform>().sizeDelta = new Vector2(500 * lifePercentage, imgValue.GetComponent<RectTransform>().sizeDelta.y);
        imgValue.transform.parent.LookAt(Camera.main.transform);
    }
}

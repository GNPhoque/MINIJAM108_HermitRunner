using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowsShowingShells : MonoBehaviour
{
    [SerializeField] GameObject[] arrowParents = new GameObject[3];

    [SerializeField] BonusController[] shells = new BonusController[3];

    void Start()
    {
        
    }

    void Update()
    {
        shells = GameObject.FindObjectsOfType<BonusController>();

        for (int i = 0; i < shells.Length; i++)
        {
            if (shells[i].GetComponent<BonusController>() != null)
            {
                arrowParents[i].SetActive(true);
                float angle = Mathf.Atan2(shells[i].transform.position.y - transform.position.y, shells[i].transform.position.x - transform.position.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                Debug.Log(angle);
                arrowParents[i].transform.rotation = rotation;
            }
            else
            {
                arrowParents[i].SetActive(false);
            }
        }
        





    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowsShowingShells : MonoBehaviour
{
    [SerializeField] GameObject[] arrowParents;
    float greenColor;
    [SerializeField] float closeDistance;
    [SerializeField] float veryCloseDistance;
    [SerializeField] BonusController[] shells = new BonusController[3];

    void Start()
    {
        
    }

    void Update()
    {
        shells = GameObject.FindObjectsOfType<BonusController>();

        for (int i = 0; i < 2; i++)
        {
            if(i< shells.Length)
            {
                if (shells[i].GetComponent<BonusController>() != null)
                {
                    arrowParents[i].SetActive(true);
                    float angle = Mathf.Atan2(shells[i].transform.position.y - transform.position.y, shells[i].transform.position.x - transform.position.x) * Mathf.Rad2Deg;
                    Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    arrowParents[i].transform.rotation = rotation;
                    float distance = Vector2.Distance(shells[i].transform.position, transform.position);
                    
                    //marche po
                    if(distance <veryCloseDistance)
                    {
                        arrowParents[i].transform.GetComponentInChildren<SpriteRenderer>().color = Color.red;
                    }
                    else if(distance<closeDistance)
                    {
                        arrowParents[i].transform.GetComponentInChildren<SpriteRenderer>().color = new Color(1, 0.65f, 0);
                    }
                    else
                    {
                        arrowParents[i].transform.GetComponentInChildren<SpriteRenderer>().color = Color.yellow;
                    }
                }
            }
            else
            {
                arrowParents[i].SetActive(false);
                Debug.Log(arrowParents[i]);
            }
        }
    }
}

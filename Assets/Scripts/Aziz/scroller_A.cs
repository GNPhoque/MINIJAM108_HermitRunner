using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scroller_A : MonoBehaviour
{
    Transform _transform;
    [SerializeField] float _scrollSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        _transform = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
        _transform.position = new Vector2(_transform.position.x + _scrollSpeed * Time.deltaTime, _transform.position.y);
    }
}

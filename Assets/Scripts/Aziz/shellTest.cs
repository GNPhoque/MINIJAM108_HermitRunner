using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shellTest : MonoBehaviour
{

    private Animator _animatorController;
    [SerializeField] bool _hasShell;

    void Start()
    {
        _animatorController = GetComponent<Animator>();
        _hasShell = _animatorController.GetBool("HasShell");
    }

    void Update()
    {
        _animatorController.SetBool("HasShell",_hasShell);
    }
}

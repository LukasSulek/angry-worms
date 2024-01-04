using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AnimationController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Animator _animator;
    private string _defaultTrigger = "Normal";
    private string _scaleDownTrigger = "ScaleDown";
    private string _scaleUpTrigger = "ScaleUp";

    void Start()
    {
        _animator.applyRootMotion = false;
        _animator.SetTrigger(_defaultTrigger);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _animator.SetTrigger(_scaleDownTrigger);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _animator.SetTrigger(_scaleUpTrigger);

      
    }
    
}

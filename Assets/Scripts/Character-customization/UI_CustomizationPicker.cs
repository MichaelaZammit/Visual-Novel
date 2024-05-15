using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CustomizationPicker : MonoBehaviour
{
    [SerializeField] 
    private CustomizableElement _customizableElement;
    
    [SerializeField] 
    private Button _previousSpriteButton;
    
    [SerializeField] 
    private Button _nextSpriteButton;
    
    [SerializeField] 
    private Button _previousColorButton;
    
    [SerializeField] 
    private Button _nextColorButton;

    [SerializeField]
    private TMP_Text _spriteId;

    [SerializeField] 
    private Image _colorIcon;

    private void Start()
    {
        UpdateSpriteId();
        UpdateColorIcon();
        
        _previousSpriteButton.onClick.AddListener(() =>
        {
            _customizableElement.PreviousSprite();
            UpdateSpriteId();
        });
        _nextSpriteButton.onClick.AddListener(() =>
        {
            _customizableElement.NextSprite();
            UpdateSpriteId();
        });
        
        _previousColorButton.onClick.AddListener(() =>
        {
            _customizableElement.PreviousColor();
            UpdateColorIcon();
        });
        
        _nextColorButton.onClick.AddListener(() =>
        {
            _customizableElement.NextColor();
            UpdateColorIcon();
        });
    }

    public void UpdateSpriteId()
    {
        _spriteId.SetText(_customizableElement.SpriteIndex.ToString().PadLeft(2,'0'));
    }

    public void UpdateColorIcon()
    {
        _colorIcon.color = _customizableElement.CurrentColor;
    }
    
}

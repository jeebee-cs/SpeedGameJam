using System.Collections;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChanger
{

    private Color _currentColor;
    private Color _startColor;
    private Vector2 _startSize;
    private SpriteRenderer _spriteRenderer;
    private float _multiplicationFactor = 1f;
    private float _multiplicationMax = 3f;

    public SpriteChanger(SpriteRenderer spriteRenderer)
    {
        this._spriteRenderer = spriteRenderer;
        this._currentColor = spriteRenderer.color;
        this._startColor = this._currentColor;
        this._startSize = spriteRenderer.size;
    }
    
    public void IncreaseVisibility()
    {
        if (this._currentColor.a < this._startColor.a)
        {
            this._currentColor.a = this._startColor.a;
        }

        this._currentColor.a += 0.01f;
        this._spriteRenderer.color = this._currentColor;
    }

    public void MakeInvisible()
    {
        this._currentColor.a = 0f;
        this._spriteRenderer.color = this._currentColor;
    }

    public void MakeVisible()
    {
        this._currentColor.a = 1f;
        this._spriteRenderer.color = this._currentColor;
    }

    public void IncreaseSize()
    {
        this._multiplicationFactor += 0.003f;
        
        if (this._multiplicationFactor >= this._multiplicationMax)
        {
            this._multiplicationFactor = this._multiplicationMax;
        }

        this._spriteRenderer.size = this._startSize * this._multiplicationFactor;
    }

    public void ResetSprite()
    {
        this._spriteRenderer.size = this._startSize;
        this._spriteRenderer.color = this._startColor;
        this._multiplicationFactor = 1f;
    }

}

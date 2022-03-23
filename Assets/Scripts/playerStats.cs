using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class playerStats : MonoBehaviour
{
    public int currentValue;
    public int maxValue;

    public int currentTemperature;
    public int maxTemperature;
    private int _startTemperature;

    public int hotTemperature;
    private bool _isHot = false;
    private bool _isDries = false;
    public float maxDelayBeforeDrying;
    private Coroutine DryingProcess;
    
    private bool _isDead = false;
    private bool _isRepeatDie = false;

    private void checkValue()
    {
        if (currentValue >= maxValue) 
            currentValue = maxValue;

        if (currentValue <= 0)
        {
            currentValue = 0;
            _isDead = true;
        }
    }
    private void checkTemperature()
    {
        if (currentTemperature >= maxTemperature)
        {
            currentTemperature = maxTemperature;
            _isDead = true;
        }
        if (currentTemperature <= 0)
        {
            currentTemperature = 0;
            _isDead = true;
        }
        if (currentTemperature > hotTemperature) _isHot = true;
        if (currentTemperature <= hotTemperature) _isHot = false;
    }

    public void Drink(int drunkValue)
    {
        currentValue += drunkValue;
        checkValue();
    }
    public void Dry(int dryCount)
    {
        currentValue -= dryCount;
        checkValue();
    }

    public void RaiseTemperature()
    {
        currentTemperature ++;
        checkTemperature();
    }
    public void CoolTemperature()
    {
        currentTemperature --;
        checkTemperature();
    }

    public void die()
    {
        Debug.Log("you die");
        _isRepeatDie = true;
    }

    IEnumerator Drying()
    {
        _isDries = true;

        while( _isHot && ! _isDead )   
        {
            float difTemp = currentTemperature - hotTemperature;
            float timeBeforeDrying = maxDelayBeforeDrying * 0.01f * (101f - difTemp);
            Debug.Log(timeBeforeDrying);
            yield return new WaitForSeconds(timeBeforeDrying);
            Dry(1);
            CoolTemperature();
        }
    }

    void Start()
    {
        maxValue = 100;
        currentValue = maxValue;

        maxTemperature = 100;
        hotTemperature = 30;
        _startTemperature = 25;
        currentTemperature = _startTemperature;

        maxDelayBeforeDrying = 3;

        checkTemperature();
        checkValue();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) RaiseTemperature();

        if  ( ! _isDries ) if ( _isHot && ! _isDead ) 
            DryingProcess = StartCoroutine(Drying());

        if  ( _isDries ) if ( ! _isHot || _isDead )
        {
            StopCoroutine( DryingProcess );
            _isDries = false;
        } 

        if ( _isDead && ! _isRepeatDie ) die();
    }
    
}
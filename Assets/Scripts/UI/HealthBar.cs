using System;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    [SerializeField] private Character target;
    public Image hpBar;
    private float _currHp, _maxHp;
    
    private void Start() {
        target.OnDamageReceived += UpdateHealthBar;
        _currHp = target.HealthPoints;
        _maxHp = target.HealthPoints;
        UpdateHealthBar(_currHp);
    }

    void UpdateHealthBar(float currHealth) {
        hpBar.fillAmount = currHealth / _maxHp;
    }
}
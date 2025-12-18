using System;
using UnityEngine;
using PrimeTween;
using TMPro;

public class Popup : MonoBehaviour {
    private UnitInstance _unit;
    public TextMeshProUGUI popupBoxPrefab;
    public Canvas container;
    public float duration;

    private void Start() {
        _unit = GetComponent<UnitInstance>();
        _unit.Events.OnDamageReceived += DisplayPopup;
    }

    void DisplayPopup(DamagePacket dmgPacket) {
        TextMeshProUGUI popup = Instantiate(popupBoxPrefab, container.transform);
        popup.transform.position = Camera.main.WorldToScreenPoint(_unit.transform.position);
        
        popup.SetText(dmgPacket.DamageAmount.ToString("F1"));
        
        float finalDuration = dmgPacket.IsIndirect ? duration * 0.5f : duration;
        float finalHeight = popup.transform.position.y + 100;
        finalHeight -= dmgPacket.IsIndirect ? 50 : 0;
        Tween.PositionY(popup.transform, endValue: finalHeight, duration: finalDuration, ease: Ease.InOutSine)
            .OnComplete(() => Destroy(popup.gameObject));
    }
}
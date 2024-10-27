using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class DefenseEffectHud : MonoBehaviour, IEffectHud
{
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    private float durationLeft;

    public UnityEvent OnInitialize;
    public UnityEvent OnDiscard;

    private void Awake()
    {
        if (_textMeshPro != null)
            _textMeshPro = gameObject.transform.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Initialize(float duration)
    {
        durationLeft = duration;
        Invoke(nameof(Discrad), duration);

        OnInitialize?.Invoke();
    }

    public void Discrad()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        _textMeshPro.text = Convert.ToInt32(durationLeft).ToString();
        if (durationLeft > 0)
            durationLeft -= Time.deltaTime;
    }
}

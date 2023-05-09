using TMPro;
using UnityEngine;

public class UICoinsText : MonoBehaviour
{
    private TextMeshProUGUI tmproText;

    private void Awake()
    {
        tmproText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        GameManager.Instance.OnCoinsChanged += HandleOnCoinsChanged;
        tmproText.text = GameManager.Instance.Coins.ToString();
    }

    private void OnDestroy()
    {
        GameManager.Instance.OnCoinsChanged -= HandleOnCoinsChanged;
    }

    private void HandleOnCoinsChanged(int coins)
    {
        tmproText.text = coins.ToString();
    }
}

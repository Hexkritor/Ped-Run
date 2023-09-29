using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _levelText;
    [SerializeField]
    private TMP_Text _levelCompletedText;
    [SerializeField]
    private TMP_Text _coinText;
    [SerializeField]
    private TMP_Text _coinCollectedText;
    [SerializeField]
    private GameObject _panel;

    public void Start()
    {
        _levelText.text = $"LEVEL { SaveManager.Data.level + 1}";
        _coinText.text = $"Coins\n{ SaveManager.Data.coins}";
        _levelCompletedText.gameObject.SetActive(false);
        _panel.SetActive(false);
    }

    public void ShowCoinsCollected(int value)
    {
        _coinCollectedText.text = $"Coins collected\n{value}";
    }
    public void ShowLevelCompletion()
    {
        StartCoroutine(LevelCompletionCoroutine());
    }

    private IEnumerator LevelCompletionCoroutine()
    {
        _levelCompletedText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        _panel.SetActive(true);
    }
}

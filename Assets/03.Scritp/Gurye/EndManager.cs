using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class EndManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _gameOverTXT;
    [SerializeField] private LoopType _loop;
    private void Awake()
    {
        _gameOverTXT = _gameOverTXT.GetComponent<TextMeshProUGUI>();
        _gameOverTXT.DOFade(0f, 1).SetLoops(-1, _loop);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DoBlank()
    {

    }
}

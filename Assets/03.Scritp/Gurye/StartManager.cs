using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    [SerializeField] private Button _fadeButton;
    [SerializeField] private Image _fade;
    private bool _isFade;
    private void Awake()
    {
    }

    // Update is called once per frame
    private void Update()
    {

    }

    public void FadeIn(float _duration)
    {
        _fade.gameObject.SetActive(true);
        _isFade = !_isFade;
        _fade.DOFade(_isFade ? 1 : 0, _duration).SetEase(Ease.OutBounce).OnComplete(() =>
        ChangeSceen(SceneManager.GetActiveScene().buildIndex));
    }

    public void ButtonInteractable(bool a)
    {
        _fadeButton.GetComponent<Button>().interactable = a;
    }
    public void ChangeSceen(int _index)
    {
        SceneManager.LoadScene(_index);
    }
}

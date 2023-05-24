using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartManager : MonoBehaviour
{
    [SerializeField] private Button _fadeButton;
    [SerializeField] private Image _fade;
    [SerializeField] private TextMeshProUGUI _tapTXT;
    [SerializeField] private LoopType _loop;
    private bool _isFade;

    private float _shake = 0;
    private float _shakeAmount;

    Sequence seq;
    private void Awake()
    {
        _tapTXT.gameObject.SetActive(true);

        seq = DOTween.Sequence()
        .OnStart(() =>
        {
            _tapTXT.DOFade(1f, 0);
        })
        .Append(_tapTXT.DOFade(0f, 0.8f))
        .Append(_tapTXT.DOFade(1f, 0.8f))
        .OnComplete(() =>
        {
            seq.Restart();
        });
    }
    // Update is called once per frame
    private void Update()
    {

        //_tapTXT.color = new Color(1, 1, 1, Mathf.Sin(Time.deltaTime * 1));

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
        _tapTXT.gameObject.SetActive(false);
        _fadeButton.GetComponent<Button>().interactable = a;
    }
    public void ChangeSceen(int _index)
    {
        SceneManager.LoadScene(_index);
    }
}

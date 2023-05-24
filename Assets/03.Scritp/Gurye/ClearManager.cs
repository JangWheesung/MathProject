using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearManager : MonoBehaviour
{
    private MeshRenderer _mesh;
    [SerializeField] private float _speed = 3;
    private float _offset;

    private void Awake()
    {
        _mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _offset += Time.deltaTime * _speed;
        _mesh.material.mainTextureOffset = new Vector2(_offset, 0);
    }

    public void changeSceen(int num)
    {
        SceneManager.LoadScene(num);
    }
}

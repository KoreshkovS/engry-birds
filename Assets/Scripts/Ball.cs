using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private SpringJoint2D _sj;
    [SerializeField] private Rigidbody2D _hookRb;
    [SerializeField] private GameObject _nextBall;
    [SerializeField] private float _delay = 0.15f;
    [SerializeField] private float _maxDragDistance = 2f;
    [SerializeField] private float _spawnDelay = 2f;

    private bool isPressed = false;
   

    private void Update()
    {
        if (isPressed)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (Vector3.Distance(mousePos, _hookRb.position) > _maxDragDistance)
            {
                _rb.position = _hookRb.position + (mousePos - _hookRb.position).normalized * _maxDragDistance;
            }
            else
            {
                _rb.position = mousePos;

            }
        }
    }

    private void OnMouseDown()
    {
        isPressed = true;
        _rb.isKinematic = true;
    }
    private void OnMouseUp()
    {
        isPressed = false;
        _rb.isKinematic = false;

        StartCoroutine(Release());
    }

    IEnumerator Release ()
    {
        yield return new WaitForSeconds(_delay);

        _sj.enabled = false;
        this.enabled = false;

        yield return new WaitForSeconds(_spawnDelay);

        if (_nextBall != null)
        {
            _nextBall.SetActive(true);
        }
        else
        {
            Enemy.EnemiesAlive = 0;
            SceneManager.LoadScene("SampleScene");
        }
        
    }
}

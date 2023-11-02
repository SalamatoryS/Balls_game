using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour
{
    [SerializeField] Transform _tube;
    [SerializeField] Transform _spawner;
    [SerializeField] Ball _ballPrefab;
    [SerializeField] CollapseManager _collapseManager;
    [SerializeField] RayDown _rayMat;

    Ball _ballInTube;
    Ball _ballInSpawner;

    private void Start()
    {
        CreateBallInTube();
        StartCoroutine(MoveToSpawner());
    }

    private void Update()
    {
        if (_ballInSpawner)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Drop();
            }
        }
    }

    void CreateBallInTube()
    {
        int ballLevel = Random.Range(0, 3);
        _ballInTube = Instantiate(_ballPrefab, _tube.position, Quaternion.identity);
        _ballInTube.SetLevel(ballLevel);
        _ballInTube.SetToTube();
        _ballInTube.Init(_collapseManager);
        
        if (_ballInSpawner)
        {
            _rayMat.SetData(_ballInSpawner.level,_ballInSpawner.radius);
        }    
    }

    IEnumerator MoveToSpawner()
    {
        _ballInTube.transform.parent = _spawner;

        for (float t = 0; t < 1; t += Time.deltaTime / 0.3f)
        {
            _ballInTube.transform.position = Vector3.Lerp(_tube.position, _spawner.position, t);
            yield return null;
        }
        _ballInTube.transform.localPosition = Vector3.zero;
        _ballInSpawner = _ballInTube;
        _ballInTube = null;
        CreateBallInTube();
    }

    void Drop()
    {
        _ballInSpawner.Drop();
        _ballInSpawner = null;
        if (_ballInTube)
        {
            StartCoroutine(MoveToSpawner());
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TaskIcon _taskIconPrefab;
    [SerializeField] Transform _parent;
    [SerializeField] FlyingIcon _flyIconPrefab;
    [SerializeField] Camera _camera;
    [SerializeField] ItemIcons itemIcons;
    [SerializeField] GameManager _gameManager;

    TaskIcon[] taskIcons;
    
    public static ScoreManager instance;

    private int _ballTask;
    private int _ballIndex;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Level _level = FindObjectOfType<Level>();

        taskIcons = new TaskIcon[_level.tasks.Length];

        for (int i = 0; i < _level.tasks.Length; i++)
        {
            TaskIcon newTaskIcon =  Instantiate(_taskIconPrefab, _parent);
            newTaskIcon.Setup(_level.tasks[i].itemType, _level.tasks[i].number);
            taskIcons[i] = newTaskIcon;
            
            if (taskIcons[i].itemType == ItemType.Ball)
            {
                _ballTask = taskIcons[i].currentScore;
                _ballIndex = i;
            }
        }
    }

    public void AddScore(ItemType itemType, Vector3 position)
    {
        for (int i = 0;i < taskIcons.Length;i++)
        {
            if (taskIcons[i].itemType == itemType)
            {
                if (taskIcons[i].currentScore != 0)
                {
                    StartCoroutine(FlyAnimation(taskIcons[i], position));
                }
            }
        }
    }

    public void CheckBall(int ballNumber, Vector3 position)
    {
        if (ballNumber == _ballTask)
        {
            taskIcons[_ballIndex].currentScore = 0;
            StartCoroutine(FlyAnimation(taskIcons[_ballIndex], position));
        }
    }

    private IEnumerator FlyAnimation(TaskIcon taskIcon, Vector3 position)
    {
        FlyingIcon newFlyingIcon = Instantiate(_flyIconPrefab, _parent);
        Sprite sprite = itemIcons.GetSprite(taskIcon.itemType);
        newFlyingIcon.Setup(sprite);

        Vector3 a = _camera.WorldToScreenPoint(position);
        Vector3 b = taskIcon.transform.position;

        for (float t = 0; t < 1f; t += Time.deltaTime)
        {
            newFlyingIcon.transform.position = Vector3.Lerp(a, b, t);
            yield return null;
        }
        Destroy(newFlyingIcon.gameObject);
        taskIcon.AddOne();
        CheckWin();
    }

    void CheckWin()
    {      
        for (int i =0; i < taskIcons.Length; i++)
        {
            if (taskIcons[i].currentScore != 0)
            {
                return;
            }
        }
        _gameManager.Win();
    }
}

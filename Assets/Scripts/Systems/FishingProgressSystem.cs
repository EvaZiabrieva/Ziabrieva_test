using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingProgressSystem : MonoBehaviour, ISystem, IUpdatable
{
    private const float DESIRED_ANGLE = 180;
    private const float CASHED_POINTS_COUNT = -0.5f;

    [SerializeField] private Canvas _progressUI;
    [SerializeField] private Image _progressBar;
    [SerializeField] private float _availableCatchingAngleOffset;
    [SerializeField] private RangeFloat _finishPointsRange;

    private FishingPole _fishingPole;
    private Transform _bobber;
    private Fish _fish;
    private FishInteractionSystem _interactionSystem;
    private UpdatableSystem _updatableSystem;

    private Vector3 _fishingPoleDirection;
    private Vector3 _fishDirection;

    private float _currentPoints;

    public bool IsInitialized => _interactionSystem != null;
    public event Action<bool> OnFishingFinished;

    public void RegisterFishingPole(FishingPole fishingPole)
    {
        _fishingPole = fishingPole;
        _bobber = _fishingPole.Bobber.View.Rigidbody.transform;
    }

    public void Initialize()
    {
        _updatableSystem = SystemsContainer.GetSystem<UpdatableSystem>();
        _interactionSystem = SystemsContainer.GetSystem<FishInteractionSystem>();
        _interactionSystem.OnFishBit += OnFishBitHandler;
        OnFishingFinished += OnFishingFinishedHandler;
    }

    private void OnFishingFinishedHandler(bool result)
    {
        _updatableSystem.UnRegisterUpdatable(this);
        _progressUI.gameObject.SetActive(false);

        if(result)
            CalculateResults();
    }

    private void OnFishBitHandler(Fish fish)
    {
        _fish = fish;
        _progressUI.gameObject.SetActive(true);
        _updatableSystem.RegisterUpdatable(this);
        _currentPoints = _finishPointsRange.max / 2;
    }

    public void Shutdown()
    {
        _interactionSystem.OnFishBit -= OnFishBitHandler;
    }

    public void ExecuteUpdate()
    {
        _fishingPoleDirection = GetDirection(_fishingPole.PoleTip.position, _bobber.position);
        _fishDirection = GetDirection(_bobber.position, _fish.transform.position);

        float angle = Vector3.Angle(_fishingPoleDirection, _fishDirection);
        Debug.Log(">>>" + angle);
        float absAngleOffset = Mathf.Abs(angle);

        float earnedPoints = Mathf.InverseLerp(_availableCatchingAngleOffset * 2, 0, absAngleOffset);
        _currentPoints += (earnedPoints + CASHED_POINTS_COUNT) * Time.deltaTime;
        _progressBar.fillAmount = _currentPoints/ _finishPointsRange.max;

        if(_currentPoints >= _finishPointsRange.max)
        {
            OnFishingFinished?.Invoke(true);
        }

        if(_currentPoints <= _finishPointsRange.min)
        {
            OnFishingFinished?.Invoke(false);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(_fishingPole.PoleTip.position, _fishingPoleDirection);
        Gizmos.color = Color.white;
        Gizmos.DrawRay(_fish.transform.position, _fishDirection);
    }
    private Vector3 GetDirection(Vector3 from, Vector3 to)
    {
        Vector3 direction = (to - from).normalized;
        return direction;
    } 

    private void CalculateResults()
    {

    }
}

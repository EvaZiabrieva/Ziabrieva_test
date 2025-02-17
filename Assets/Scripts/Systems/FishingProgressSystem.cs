using System;
using UnityEngine;
using UnityEngine.UI;

public class FishingProgressSystem : MonoBehaviour, ISystem, IUpdatable
{
    private const float DESIRED_ANGLE = 180;
    private const float CASHED_POINTS_COUNT = -0.5f;
    private const float REELING_MULTIPLIER_POWER = 150f;

    [SerializeField] private Transform _fishBucket;
    [SerializeField] private Canvas _progressUI;

    [SerializeField] private Image _progressBar;
    [SerializeField] private float _availableCatchingAngleOffset;
    //TODO: in configs
    [SerializeField] private RangeFloat _finishPointsRange;

    private FishingPole _fishingPole;
    private Transform _bobber;
    private Fish _fish;
    private FishInteractionSystem _interactionSystem;
    private UpdatableSystem _updatableSystem;

    private Vector2 _fishingPoleDirection;
    private Vector2 _fishDirection;

    private float _currentPoints;
    private float _previousLength;

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
        _previousLength = _fishingPole.FishingReel.GetLength();
        _updatableSystem.RegisterUpdatable(this);
        _currentPoints = _finishPointsRange.max / 2;
    }

    public void Shutdown()
    {
        _interactionSystem.OnFishBit -= OnFishBitHandler;
    }

    public void ExecuteUpdate()
    {
        _fishingPoleDirection = _fishingPole.PoleTip.forward;
        _fishDirection = GetDirection(_fishingPole.PoleTip.position, _fish.transform.position);

        float currLenght = _fishingPole.FishingReel.GetLength();
        float reelingDelta = (_previousLength - currLenght);

        float pointsMultiplier = (reelingDelta * REELING_MULTIPLIER_POWER * _fish.Data.Weight);

        float angle = Vector2.Angle(_fishingPoleDirection, _fishDirection);
        float absAngleOffset = Mathf.Abs(angle);

        float earnedPoints = Mathf.InverseLerp(_availableCatchingAngleOffset * 2, 0, absAngleOffset) * pointsMultiplier;
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

        _previousLength = currLenght;
    }
    private Vector3 GetDirection(Vector3 from, Vector3 to)
    {
        Vector3 direction = (to - from).normalized;
        return direction;
    } 
    public void OnContinue()
    {
        _fish.Reattach(_fishBucket);
    }
    private void CalculateResults()
    {

    }
}

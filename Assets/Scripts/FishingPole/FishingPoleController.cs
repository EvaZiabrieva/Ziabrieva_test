using UnityEngine;
using UnityEngine.InputSystem;

public class FishingPoleController : BaseFishingPoleController
{
    private Vector3 _castingDirection;
    private float _castingSpeed;
    private InputSystem _inputSystem;
    private InputAction _castingInput;

    private float currLenght = 0;
    public FishingPoleController(FishingPole pole, CastingTracker tracker) : base(pole, tracker) 
    {
        _inputSystem = SystemsContainer.GetSystem<InputSystem>();
        _castingInput = _inputSystem.GetInputAction(Inputs.CASTING_NAME);

        _castingInput.started += AddSubscriber;
    }

    public override void ExecuteUpdate()
    {
        _fishingPole.FishingLine.View.SetLenght(GetCurrentLenght());
        _fishingPole.Bobber.UpdateOffset(GetCurrentLenght());

        //Can be removed because used for debug
        float lenght = GetCurrentLenght();
        if (Input.GetKey(KeyCode.Space))
        {
            currLenght = lenght / _fishingPole.FishingLine.View.MaxLength;
            currLenght += Time.deltaTime / 5;
            _fishingPole.FishingReel.SetAngle(currLenght);
        }
        if (Input.GetKey(KeyCode.RightControl))
        {
            currLenght = lenght / _fishingPole.FishingLine.View.MaxLength;
            currLenght -= Time.deltaTime / 5;
            _fishingPole.FishingReel.SetAngle(currLenght);
        }

        if (_castingInput.IsPressed())
        {
            if (_castingTracker.TrackedDistance >= _trackedDistanceTreshold)
            {
                _fishingPole.FishingReel.SetAngle(_fishingPole.FishingLine.View.MaxLength * 360);
                _castingDirection = _fishingPole.transform.forward + _fishingPole.transform.up;
                _castingSpeed = _castingTracker.TrackedDistance * _fishingPole.CastingSensitivity;
                _fishingPole.Bobber.Cast(_castingDirection, _castingSpeed);
                RemoveSubscriber();
            }
        }
    }
    private float GetCurrentLenght() => _fishingPole.FishingReel.GetLength();

    private void AddSubscriber(InputAction.CallbackContext callbackContext)
    {
        _castingTracker.OnBeforeCast();
    }

    private void RemoveSubscriber()
    {
        _castingTracker.OnAfterCast();
    }

    public void Shutdown()
    {
        _castingInput.started -= AddSubscriber;
    }
}

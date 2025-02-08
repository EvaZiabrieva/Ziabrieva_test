using System.Collections;
using System.Collections.Generic;
using Unity.VRTemplate;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.XR.OpenXR.Input;

public class FishingPoleFactory
{
    private Pole _pole;
    private PoleView _poleView;

    private FishingReel _fishingReel;
    private FishingReelView _fishingReelView;

    private FishingLine _fishingLine;
    private FishingLineView _fishingLineView;

    private Hook _hook;
    private HookView _hookView;

    private Bobber _bobber;
    private BobberView _bobberView;

    private FishingPole _fishingPole;
    private FishingPoleController _fishingPoleController;

    private GameObject _poleObject;
    private GameObject _hookObject;
    private PoleVisualsContainer _poleVisualsContainer;
    public void CreateFishingPole(GameObject pole, GameObject fishingReel, GameObject fishingLine,
                                  GameObject hook, GameObject bobber, Transform spawnPoint)
    {
        CreatePole(pole, spawnPoint.position);
        CreateReel(fishingReel, _poleVisualsContainer.ReelPlacement.position);
        CreateHook(hook, _poleVisualsContainer.HookPlacement.position);
        CreateBobber(bobber, _poleVisualsContainer.BobberPlacement.position);
        CreateFishingLine(fishingLine, _poleVisualsContainer.FishingLinePlacement.position);

        _fishingPole = _poleObject.GetComponent<FishingPole>();

        CastingTracker tracker = CreateCastingTracker();
        _fishingPoleController = new FishingPoleController(_fishingPole, tracker);

        _fishingPole.Initialize(_hook, _bobber, _fishingLine, _fishingReel, _pole, _fishingPoleController);
    }

    public void RemoveFishingPole()
    {
        _fishingPoleController.Shutdown();
    }

    //TODO make an abstractions to use one spawn method
    private void CreatePole(GameObject pole, Vector3 spawnPoint)
    {
        _poleObject = GameObject.Instantiate(pole, spawnPoint, Quaternion.identity);
        _poleVisualsContainer = _poleObject.GetComponent<PoleVisualsContainer>();
        _poleView = new PoleView(_poleVisualsContainer);
        _pole = new Pole(10f, _poleView);
    }
    private void CreateReel(GameObject fishingReel, Vector3 spawnPoint)
    {
        GameObject reelObject = GameObject.Instantiate(fishingReel, spawnPoint, Quaternion.identity, _poleObject.transform);
        FishingReelVisualsContainer reelVisualsContainer = reelObject.GetComponent<FishingReelVisualsContainer>();
        XRKnob knob = reelObject.GetComponentInChildren<XRKnob>();
        _fishingReelView = new FishingReelView(reelVisualsContainer);
        _fishingReel = new FishingReel(_fishingReelView, knob, 1);
    }
    private void CreateHook(GameObject hook, Vector3 spawnPoint)
    {
        _hookObject = GameObject.Instantiate(hook, spawnPoint, Quaternion.identity);
        HookVisualsContainer hookVisualsContainer = _hookObject.GetComponent<HookVisualsContainer>();

        Rigidbody connectedRigidbody = GameObject.Instantiate(hookVisualsContainer.HookJointPoint, spawnPoint, Quaternion.identity, _poleObject.transform);
        ConfigurableJoint configurableJoint = _hookObject.GetComponent<ConfigurableJoint>();
        configurableJoint.connectedBody = connectedRigidbody;
        Rigidbody hoohRigidbody = _hookObject.GetComponent<Rigidbody>();

        _hookView = new HookView(hookVisualsContainer);
        _hook = new Hook(1, 0.3f, _hookView, configurableJoint, hoohRigidbody);
    }
    private void CreateBobber(GameObject bobber, Vector3 spawnPoint)
    {
        GameObject.Instantiate(bobber, spawnPoint, Quaternion.identity, _poleObject.transform);
        _bobberView = new BobberView();
        _bobber = new Bobber(_bobberView);
    }
    private void CreateFishingLine(GameObject fishingLine, Vector3 spawnPoint)
    {
        GameObject lineObject = GameObject.Instantiate(fishingLine, spawnPoint, Quaternion.identity, _poleObject.transform);

        PointsData pointsData = new PointsData()
        {
            StartPoint = _poleVisualsContainer.FishingLinePlacement,
            EndPoint = _hookObject.transform
        };
        LineRenderer lineRenderer = lineObject.GetComponent<LineRenderer>();

        _fishingLineView = new FishingLineView(pointsData, lineRenderer);
        _fishingLine = new FishingLine(5, 1, 10, _fishingLineView);
    }

    private CastingTracker CreateCastingTracker()
    {
        GameObject trackerObject = new GameObject("Tracker");
        trackerObject.transform.position = _poleVisualsContainer.PoleTip.position;
        trackerObject.transform.parent = _poleObject.transform;
        return new CastingTracker(trackerObject.transform);
    }
}

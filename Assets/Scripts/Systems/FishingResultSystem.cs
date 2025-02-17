using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishingResultSystem : MonoBehaviour, ISystem
{
    [SerializeField] private Canvas _resultsCanvas;
    [SerializeField] private Transform _playerCameraTransform;
    [SerializeField] private Text _resultTitle;
    [SerializeField] private Button _continueButton;
    [SerializeField] private string _successesfulTitle;
    [SerializeField] private string _failTitle;

    private FishingProgressSystem _progressSystem;
    public bool IsInitialized => _progressSystem != null;

    public void Initialize()
    {
        _progressSystem = SystemsContainer.GetSystem<FishingProgressSystem>();
        _progressSystem.OnFishingFinished += ShowResults;
        _continueButton.onClick.AddListener(_progressSystem.OnContinue);
    }

    public void Shutdown()
    {
        _progressSystem.OnFishingFinished -= ShowResults;
        _continueButton.onClick.RemoveListener(_progressSystem.OnContinue);
    }

    private void ShowResults(bool isSuccessful)
    {
        _resultsCanvas.gameObject.SetActive(true);
        //TODO: normal view + do offset + not needed position
        _resultsCanvas.transform.position = new Vector3 (_playerCameraTransform.forward.x, _playerCameraTransform.forward.y + 1, _playerCameraTransform.forward.z);
        if (isSuccessful)
        {
            _resultTitle.text = _successesfulTitle;
        }
        else
        {
            _resultTitle.text = _failTitle;
        }
    }
}

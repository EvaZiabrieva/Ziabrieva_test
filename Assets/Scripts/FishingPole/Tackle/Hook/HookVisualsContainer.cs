using UnityEngine;

public class HookVisualsContainer : MonoBehaviour
{
    [field: SerializeField]
    public Transform SnapPlacement { get; private set; }
    [field: SerializeField]
    public Rigidbody HookJointPoint { get; private set; }
}

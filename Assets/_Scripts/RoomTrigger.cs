using UnityEngine;
using Cinemachine;
using MoreMountains.Feel;
using DG.Tweening;
using Ink.Runtime;
using static UnityEngine.Rendering.DebugUI;

public class RoomTrigger : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public Transform roomCenter; // Assign the center of the room in the inspector
    public Transform playerTransform;
    public float roomOrthoSize = 10f; // How much to zoom out to show the room
    public float roomDamping = 5f; // room Camera Dampling Transition
    public float transitionSpeed = 2f;

    private bool isInRoom = false;
    private float originalOrthoSize;
    private float originalDamping;
    private Transform originalFollow;
    private Coroutine coroutine;
    private Tween t_OrthoSize;
    private Tween t_Damping;

    void Start()
    {
        if (virtualCamera != null)
        {
            originalOrthoSize = virtualCamera.m_Lens.OrthographicSize;
            originalFollow = virtualCamera.Follow;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && virtualCamera != null)
        {
            isInRoom = true;
            t_OrthoSize?.Kill();
            t_Damping?.Kill();
            t_OrthoSize = DOVirtual.Float(originalOrthoSize, roomOrthoSize, transitionSpeed, value =>
            {
                virtualCamera.m_Lens.OrthographicSize = value;
            });
            roomCenter.transform.position = playerTransform.position;
            virtualCamera.Follow = roomCenter;
            roomCenter.DOMove(new Vector3(transform.position.x, transform.position.y, 0), transitionSpeed);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && virtualCamera != null)
        {
            isInRoom = false;
            virtualCamera.Follow = playerTransform;
            t_OrthoSize?.Kill();
            t_Damping?.Kill();
            t_OrthoSize = DOVirtual.Float(virtualCamera.m_Lens.OrthographicSize, originalOrthoSize, transitionSpeed, value =>
            {
                virtualCamera.m_Lens.OrthographicSize = value;
            });
            roomCenter.DOMove(new Vector3(playerTransform.position.x, playerTransform.position.y, 0), transitionSpeed).OnComplete(() =>
            {
                virtualCamera.Follow = playerTransform;
                roomCenter.position = transform.position;
            });
        }
    }
}

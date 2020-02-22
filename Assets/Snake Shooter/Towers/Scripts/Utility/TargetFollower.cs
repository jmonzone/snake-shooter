using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    public Transform target;

    private void Update()
    {
        if (!target) return;

        gameObject.SetActive(target.gameObject.activeSelf);
        transform.position = target.position;
    }
}
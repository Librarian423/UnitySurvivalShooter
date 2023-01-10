using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Gun gun;

    private PlayerInput playerInput;

    private void Start()
    {
        // 사용할 컴포넌트들을 가져오기
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (playerInput.fire)
            gun.Fire();
    }
}

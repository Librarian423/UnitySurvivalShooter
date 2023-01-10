using UnityEngine;
using UnityEngine.UI; // UI ���� �ڵ�

// �÷��̾� ĳ������ ����ü�μ��� ������ ���
public class PlayerHealth : LivingEntity
{
    //public Slider healthSlider; // ü���� ǥ���� UI �����̴�

    public AudioClip deathClip; // ��� �Ҹ�
    public AudioClip hitClip; // �ǰ� �Ҹ�

    private AudioSource playerAudioPlayer; // �÷��̾� �Ҹ� �����
    private Animator playerAnimator; // �÷��̾��� �ִϸ�����

    private PlayerMovement playerMovement; // �÷��̾� ������ ������Ʈ
    private PlayerShooter playerShooter; // �÷��̾� ���� ������Ʈ

    public RawImage fading;
    public float flashSpeed = 5f;
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    private bool hit = false;

    private void Awake()
    {
        // ����� ������Ʈ�� ��������
    }

    private void Update()
    {
        if (hit)
        {
            fading.color = flashColor;
        }
        else
        {   // 원래의 화면으로 서서히 돌아옴
            fading.color = Color.Lerp(fading.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        hit = false;
    }

    protected override void OnEnable()
    {
        // LivingEntity�� OnEnable() ���� (���� �ʱ�ȭ)
        base.OnEnable();
    }

    // ü�� ȸ��
    public override void RestoreHealth(float newHealth)
    {
        // LivingEntity�� RestoreHealth() ���� (ü�� ����)
        base.RestoreHealth(newHealth);
    }

    // ������ ó��
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        // LivingEntity�� OnDamage() ����(������ ����)
        base.OnDamage(damage, hitPoint, hitDirection);
        hit = true;
    }

    // ��� ó��
    public override void Die()
    {
        // LivingEntity�� Die() ����(��� ����)
        base.Die();
    }

    private void OnTriggerEnter(Collider other)
    {
        // �����۰� �浹�� ��� �ش� �������� ����ϴ� ó��
    }
}

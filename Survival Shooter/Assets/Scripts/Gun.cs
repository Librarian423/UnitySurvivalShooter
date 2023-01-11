using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunData gunData;
    private float lastShot;
    public Transform gunBarrelEndTransform;
    public LineRenderer bulletLine;
    public ParticleSystem fireEffect;
    private AudioSource gunAudioPlayer;

    // Start is called before the first frame update
    void Start()
    {
        bulletLine = GetComponent<LineRenderer>();
        gunAudioPlayer = GetComponent<AudioSource>();
        bulletLine.positionCount = 2;
        bulletLine.enabled = false;
    }

    public void Fire()
    {
        if (Time.time - lastShot < gunData.attackDelay)
            return;

        lastShot = Time.time;
        RaycastHit hit;
        Vector3 hitPos;
        var ray = new Ray(gunBarrelEndTransform.position, gunBarrelEndTransform.forward);
        if (Physics.Raycast(ray, out hit, gunData.attackDistance))
        {
            hitPos = hit.point;
            var target = hit.collider.GetComponent<IDamageable>();
            if (target != null)
                target.OnDamage(gunData.attackDmg, hitPos, hit.normal);
        }
        else
            hitPos = gunBarrelEndTransform.position + gunBarrelEndTransform.forward * gunData.attackDistance;
        StartCoroutine(FireEffect(hitPos));
    }

    private IEnumerator FireEffect(Vector3 hitPos)
    {
        fireEffect.Play();
        gunAudioPlayer.PlayOneShot(gunData.shotClip);

        bulletLine.SetPosition(0, gunBarrelEndTransform.position);
        bulletLine.SetPosition(1, hitPos);
        bulletLine.enabled = true;
        yield return new WaitForSeconds(0.03f);
        bulletLine.enabled = false;
    }
}
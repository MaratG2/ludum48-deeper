using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    public Transform bulletPivotPoint;
    public AudioClip[] shootSounds;
    [SerializeField] private float[] damage;
    [Range(0.2f, 5f)][SerializeField] private float[] shootDelay;
    [SerializeField] private float[] travelTime;
    [SerializeField] private float[] travelSpeed;
    [SerializeField] private Sprite[] bulletSprites;
    [SerializeField] KeyCode keyShoot;
    private Player player;

    [Range(0, 4)] public int upgradeTier = 0;

    private bool canShoot = true;

    void Start()
    {
        player = GetComponent<Player>();
    }
    void Update()
    {
        if(Input.GetKeyDown(keyShoot) && canShoot)
            StartCoroutine(Fire());
    }
    private IEnumerator Fire()
    {
        canShoot = false;

        player.damageAudioSource.PlayOneShot(shootSounds[Mathf.FloorToInt(Random.Range(0, shootSounds.Length - 0.01f))]);
        GameObject bullet = Instantiate(bulletPrefab, bulletPivotPoint.position, transform.rotation);
        bullet.transform.localScale = -transform.localScale;
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bullet.GetComponent<SpriteRenderer>().sprite = bulletSprites[upgradeTier];
        bulletScript.damage = damage[upgradeTier];
        bulletScript.travelTime = travelTime[upgradeTier];
        bulletScript.travelSpeed = travelSpeed[upgradeTier];

        yield return new WaitForSecondsRealtime(shootDelay[upgradeTier]);
        canShoot = true;
    }
}

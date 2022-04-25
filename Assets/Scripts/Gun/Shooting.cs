using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Shooting : MonoBehaviour
{
    [Header("Prefab Refrences")]
    public GameObject bulletPrefab;
    public GameObject casingPrefab;
    public GameObject muzzleFlashPrefab;

    [Header("Location Refrences")]
    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;

    [Header("Settings")]
    [Tooltip("Specify time to destory the casing object")] [SerializeField] private float destroyTimer = 2f;
    [Tooltip("Bullet Speed")] [SerializeField] private float shotPower = 500f;
    [Tooltip("Casing Ejection Speed")] [SerializeField] private float ejectPower = 150f;


    void Start()
    {
        if (barrelLocation == null)
            barrelLocation = transform;

        if (gunAnimator == null)
            gunAnimator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        
    }


    //총알의 움직임 구현
    void Shoot()
    {
        if (muzzleFlashPrefab)
        {
            //총알 섬광 만들기
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);

            //섬광 이펙트 삭제
            Destroy(tempFlash, destroyTimer);
        }

        //총알 프리팹이 없으면 그대로 리턴
        if (!bulletPrefab)
        { return; }

        //총알을 생성하고 총구가 보고있는 방향으로 총알에 힘을 가함
        Instantiate(bulletPrefab, barrelLocation.position, barrelLocation.rotation).GetComponent<Rigidbody>().AddForce(barrelLocation.forward * shotPower);

    }

    //탄피가 나가는 움직임 구현
    void CasingRelease()
    {
        //탄피 프리팹이 없으면 그대로 리턴
        if (!casingExitLocation || !casingPrefab)
        { return; }

        //탄피 생성
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        //탄피날아가는 움직임, 랜덤으로 힘을 가함
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        //회전력을 가함. 랜덤한 방향으로 날아가게 함
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);

        //destroyTimer 시간만큼 지난 후 탄피를 제거함
        Destroy(tempCasing, destroyTimer);
    }

}

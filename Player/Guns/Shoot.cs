using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class Shoot : MonoBehaviour
{
    [SerializeField] public string weaponKind;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] float firingSpeed = 0.3f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject impactVFX;
    [SerializeField] AudioClip gunFiringSFX;
    [SerializeField] bool invokeActive = false;
    [SerializeField] float sprayRecoil = 0.1f;
    [SerializeField] float ADSAjustment = 0;
    [SerializeField] Camera fpsCamera;
    [SerializeField] public int magCapacity;
    [SerializeField] Text ammoText;
    [SerializeField] GameObject reticle;

    AudioSource audioSource;
    Fase1Health target2;
    bool gunSoundIsPlaying = false;
    public int ammoInChamber;
    public bool ADSActive;
    bool isReloading;

    private void Start() 
    {
        ammoInChamber = magCapacity;
        audioSource = GetComponent<AudioSource>();
        ammoText.text = ammoInChamber.ToString();
        ADSActive = false;
    }

    private void Update() 
    {
        ADSHandler();

        if(Input.GetButton("Fire1") && !invokeActive && !CheckFlags()){
            if (!AmmoHandler()) return;

            invokeActive = true;
            Invoke("Fire", firingSpeed);
        }
    }

    private void ADSHandler()
    {
        if(Input.GetButton("Fire2") && !ADSActive && !CheckFlags())
        {
            reticle.GetComponent<RawImage>().enabled = false;
            ADSActive = true;
            GetComponent<Animator>().SetBool("ADS", true);
        }
        else if (!Input.GetButton("Fire2"))
        {
            GetComponent<Animator>().SetBool("ADS", false);
            reticle.GetComponent<RawImage>().enabled = true;
            ADSActive = false;
        }
    }

    private bool AmmoHandler()
    {
        if(ammoInChamber > 0)
        {
            ammoInChamber -=1;
            return true;
        } else
        {
            return false;
        }
    }

    private void Fire()
    {
        ammoText.text = ammoInChamber.ToString();
        PlayGunSound();
        if (ADSActive){
            Vector3 rawADS = fpsCamera.transform.forward;
            rawADS.y -= ADSAjustment;
            RaycasterHandler(rawADS);
        } else{
            RaycasterHandler(RaycasterSpread());
        }
    }

    private void RaycasterHandler(Vector3 raycasterLine)
    {
        RaycastHit hit;
        PlayShootVFX();

        if (Physics.Raycast(fpsCamera.transform.position, raycasterLine, out hit, range) && hit.transform.gameObject.tag != "Projectile")
        {
            DealDamage(hit);
        }
        else{
            DisableBools();
            return;
        }
    }

    private void PlayGunSound()
    {
        if( !gunSoundIsPlaying ){
            audioSource.PlayOneShot(gunFiringSFX);
            gunSoundIsPlaying = true;
        }
    }

    Vector3 RaycasterSpread(){
        Vector3 direction = fpsCamera.transform.forward;
        direction.x += Random.Range(-sprayRecoil, sprayRecoil);
        direction.y += Random.Range(-sprayRecoil, sprayRecoil);
        direction.z += Random.Range(-sprayRecoil, sprayRecoil);
        return direction;
    }

    void PlayShootVFX(){
        muzzleFlash.Play();
    }

    void DealDamage(RaycastHit hit){
        EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
        CreateHitImpact(hit);
        if(target == null)
        {
            target2 = hit.transform.GetComponent<Fase1Health>();
        }

        if (target == null && target2 == null){
            DisableBools();
            return;
        } else if (target == null && target2 != null)
        {
            target2.TakeDamage(damage);
            DisableBools();
        }else
        {
            target.TakeDamage(damage);
            DisableBools();
        }
    }

    private void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(impactVFX, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, .1f);
    }

    private void DisableBools(){
        gunSoundIsPlaying = false;
        invokeActive = false;
    }

    bool CheckFlags()
    {
        bool[] flagsArray = {
            GetComponent<Reload>().IsReloading(),
            GetComponentInParent<PotionHandler>().isUsingPotion,
        };

        foreach(bool flag in flagsArray)
        {
            if(flag) return true;
        }
        return false;
    }

    public bool IsADSActive()
    {
        return ADSActive;
    }
}

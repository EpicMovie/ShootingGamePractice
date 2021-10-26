using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public void EquipGun(Gun InGun)
    {
        if(EquippedGun != null)
        {
            Destroy(EquippedGun.gameObject);
        }

        EquippedGun = Instantiate(InGun, WeaponSocket.position, WeaponSocket.rotation) as Gun;
        EquippedGun.transform.parent = WeaponSocket;
    }

    public void Shoot()
    {
        if(EquippedGun != null)
        {
            EquippedGun.Shoot();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if(StartingGun != null)
        {
            EquipGun(StartingGun);
        }
    }

    public Transform WeaponSocket;
    public Gun StartingGun;

    protected Gun EquippedGun;
}

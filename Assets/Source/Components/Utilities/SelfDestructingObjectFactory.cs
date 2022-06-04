using Assets.Source.Components.Finders;
using Assets.Source.Lambda;
using Cinemachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Source.Components.Utilities
{
    /// <summary>
    /// This behavior is for creating one time, single use prefabs that fulfil a single purpose then 
    /// destroy themselves
    /// </summary>
    [RequireComponent(typeof(PlayerAware))]
    public class SelfDestructingObjectFactory : MonoBehaviour
    {
        private PlayerAware playerAware;

        private void Start()
        {
            playerAware = GetComponent<PlayerAware>();    
        }


        private Deferred<GameObject> cameraImpulsePrefab = new Deferred<GameObject>(() => Resources.Load<GameObject>("Prefabs/SingleUseObjects/CAMERA-IMPULSE-SOURCE"));

        public GameObject InstantiateCameraImpulse(float impulseAmount) {

            var impulseObj = Instantiate(cameraImpulsePrefab.Value, playerAware.Player.transform.position, new Quaternion(), null);

            var impulse = impulseObj.GetComponent<CinemachineImpulseSource>();

            impulse.GenerateImpulse(impulseAmount);

            return impulseObj;
        
        }



    }
}

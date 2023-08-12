using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Varyu.ExclusiveSpawner
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.Manual)]
    public class FogManager : UdonSharpBehaviour
    {
        [SerializeField] Color startColor;
        [SerializeField] Color endColor;
        [SerializeField] float startDensity = 1f;
        [SerializeField] float endDensity = 2f;
        [SerializeField] [Range(0f, 400f)] float lerpTime;
        [UdonSynced] bool changeFlag = false;
        float _time = 0;

        void Start()
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = startColor;
            RenderSettings.fogDensity = startDensity;
            _time = 0;
        }
        void Update()
        {
            if (changeFlag)
            {
                if (!RenderSettings.fog)
                {
                    RenderSettings.fog = true;
                }
                if (_time < lerpTime)
                {
                    RenderSettings.fogColor = Color.Lerp(startColor, endColor, _time / lerpTime);
                    RenderSettings.fogDensity = Mathf.Lerp(startDensity, endDensity, _time / lerpTime);
                    _time += Time.deltaTime;
                }
            }
        }
        public override void Interact()
        {
            //　同期(インスタンス内のみ)
            if (!Networking.IsOwner(Networking.LocalPlayer, this.gameObject)) Networking.SetOwner(Networking.LocalPlayer, this.gameObject);

            changeFlag = true;
            RequestSerialization();
        }
        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            RequestSerialization();
        }
    }
}
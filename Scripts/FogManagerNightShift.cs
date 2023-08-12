using UdonSharp;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace Varyu.Fogmanager
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public class FogManagerNightShift : UdonSharpBehaviour
    {
        [SerializeField] Color startColor;
        [SerializeField] Color endColor;
        [SerializeField] float startDensity = 1f;
        [SerializeField] float endDensity = 2f;
        [SerializeField] GameObject[] nightObject;
        [SerializeField] GameObject[] dayObject;

        private bool nightFlag = false;

        void Start()
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = startColor;
            RenderSettings.fogDensity = startDensity;
        }

        void Update()
        {
            if(nightFlag)
            {
                NightObjectShift();
                NightFogShift();
            }
            else
            {
                DayObjectShift();
                DayFogShift();
            }
        }

        public override void Interact()
        {
            nightFlag = !nightFlag;
        }

        public void NightObjectShift()
        {
            foreach (GameObject nightobj in nightObject)
            {
                nightobj.SetActive(true);
            }
            foreach (GameObject dayobj in nightObject)
            {
                dayobj.SetActive(false);
            }
        }

        public void NightFogShift()
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = endColor;
            RenderSettings.fogDensity = endDensity;
        }

        public void DayObjectShift()
        {
            foreach (GameObject dayobj in nightObject)
            {
                dayobj.SetActive(true);
            }
            foreach (GameObject nightobj in nightObject)
            {
                nightobj.SetActive(false);
            }
        }

        public void DayFogShift()
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = startColor;
            RenderSettings.fogDensity = startDensity;
        }
    }
}

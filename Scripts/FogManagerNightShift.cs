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

        private bool _nightFlag = false; // 実際の変数をプライベートとして保持

        public bool NightFlag // パブリックなプロパティ
        {
            get
            {
                return _nightFlag;
            }
            set
            {
                if (_nightFlag != value)
                {
                    _nightFlag = value;
                    if (_nightFlag)
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
            }
        }

        void Start()
        {
        }

        public override void Interact()
        {
            NightFlag = !NightFlag;  // NightFlagをsetterを通じて切り替える
        }

        public void NightObjectShift()
        {
            foreach (GameObject nightobj in nightObject)
            {
                nightobj.SetActive(true);
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
            foreach (GameObject nightobj in nightObject)
            {
                nightobj.SetActive(false); //昼の場合はオブジェクトを非アクティブにする
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

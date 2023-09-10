using DG.Tweening;
using Runtime.Data.ValueObjects;
using TMPro;
using UnityEngine;

namespace Runtime.controller.Player
{
    public class playermeshcontroller : MonoBehaviour
    {
        [SerializeField] private new Renderer _renderer;
        [SerializeField] private TextMeshPro scaletext;
        [SerializeField] private ParticleSystem confetti;
        private playermeshdata _data;

        internal void setdata(playermeshdata data)
        {
            _data = data;
        }

        internal void scaleUpPlayer()
        {
            _renderer.gameObject.transform.DOScaleX(_data.scaleCounter, 1f).SetEase(Ease.Flash);
        }

        internal void ShowUptext()
        {
            scaletext.DOFade(1, 0).SetEase(Ease.Flash).OnComplete(() =>
            {
                scaletext.DOFade(0, 0).SetDelay(.35f);
                scaletext.rectTransform.DOAnchorPosY(1f, .65f).SetEase(Ease.Linear);
            });
        }

        internal void PlayConffeti()
        {
            confetti.Play();
        }

        internal void Onreset()
        {
            GetComponent<Renderer>().gameObject.transform.DOScaleX(1, 1).SetEase(Ease.Linear);
        }
    }
}
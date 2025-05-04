using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace GameBase.Scripts.Gameplay.DialogSystem
{
    public class ScreenFader : MonoBehaviour
    {
        [Tooltip("Image на весь экран, изначально прозрачный")]
        [SerializeField] private Image _fadeImage;
        [Tooltip("Длительность затухания в секундах")]
        [SerializeField] private float _duration = 1f;
        
        public IEnumerator FadeToBlack(Action onComplete)
        {
            float t = 0f;
            
            while (t < _duration)
            {
                t += Time.deltaTime;
                _fadeImage.color = new Color(0, 0, 0, Mathf.Clamp01(t / _duration));
                
                yield return null;
            }
            
            onComplete?.Invoke();
        }
        
        public IEnumerator FadeFromBlack(Action onComplete)
        {
            float t = 0f;
            
            while (t < _duration)
            {
                t += Time.deltaTime;
                _fadeImage.color = new Color(0, 0, 0, 1f - Mathf.Clamp01(t / _duration));
                
                yield return null;
            }
            onComplete?.Invoke();
        }
    }
}
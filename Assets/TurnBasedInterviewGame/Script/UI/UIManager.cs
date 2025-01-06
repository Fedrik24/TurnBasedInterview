using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedGame.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance;

        [SerializeField] private Image img;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        public void FadeIn()
        {
            // Implement FadeIn
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(31, 26, 26, i);
            }
        }

        public void FadeOut()
        {
            // Implement FadeOut
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                img.color = new Color(31, 26, 26, i);
            }
        }
    }
}


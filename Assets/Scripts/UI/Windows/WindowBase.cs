using UnityEngine;
using UnityEngine.UI;

namespace UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        public Button CloseButton;

        public void Construct()
        {
            
        }
        
        private void Awake()
        {
            OnAwake();
        }

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy()
        {
            Cleanup();
        }

        protected virtual void OnAwake()
        {
            if (CloseButton)
            {
                CloseButton.onClick.AddListener(Close);
            }
        }

        public void Close()
        {
            Destroy(gameObject);
        }

        protected virtual void Initialize()
        {
        }

        protected virtual void SubscribeUpdates()
        {
        }

        protected virtual void Cleanup()
        {
        }
    }
}
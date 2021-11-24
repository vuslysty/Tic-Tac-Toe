using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class ChooseBoardWindow : WindowBase
    {
        public Button ThreeButton;
        public Button FiveButton;
        public Button EightButton;
        public Button TenButton;

        public void AddListenersForAllButtons(UnityAction action)
        {
            ThreeButton.onClick.AddListener(action);
            FiveButton.onClick.AddListener(action);
            EightButton.onClick.AddListener(action);
            TenButton.onClick.AddListener(action);
        }
    }
}
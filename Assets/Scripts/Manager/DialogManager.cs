using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Crad.UIManager
{
    public class DialogManager : Singleton<DialogManager>
    {
        [SerializeField]private DialogUI DialogPanel;

        public void OpDialog(string title,string des,string close)
        {
            DialogPanel.Init(title,des,close);
            LayoutRebuilder.ForceRebuildLayoutImmediate(DialogPanel.GetComponent<RectTransform>());
            DialogPanel.gameObject.SetActive(true);
        }

        public void CloseDialog()
        {
            DialogPanel.gameObject.SetActive(false);
        }
    }
}


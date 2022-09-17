using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Crad.UIManager
{
    public class DialogUI : MonoBehaviour
    {
        [SerializeField]private TextMeshProUGUI titleText;
        [SerializeField]private TextMeshProUGUI DesText;
        [SerializeField]private TextMeshProUGUI CloseText;
        [SerializeField]private Button CloseButton;

        public void Init(string title, string des, string cloase)
        {
            titleText.text = title;
            DesText.text = des;
            CloseText.text = cloase;
            CloseButton.onClick.AddListener(()=>gameObject.SetActive(false));
        }
    }
}


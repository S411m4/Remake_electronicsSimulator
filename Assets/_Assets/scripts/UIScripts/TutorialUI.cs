using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] Button img1NextButton;
    [SerializeField] Button img2CloseButton;
    [SerializeField] GameObject TutorialCanvas;
    [SerializeField] GameObject Img1;
    [SerializeField] GameObject Img2;

    private void Awake()
    {
        Img1.SetActive(true);
        Img2.SetActive(false);
    }

    private void Start()
    {
        img1NextButton.onClick.AddListener(()=> { Img2.SetActive(true);Img1.SetActive(false); });
        img2CloseButton.onClick.AddListener(()=> { TutorialCanvas.SetActive(false); });
        
    }

}

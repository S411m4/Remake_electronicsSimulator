using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class KeypadUI : MonoBehaviour
{
    public event EventHandler<OnEnterPressedEventArgs> OnEnterPressed;
    public event EventHandler OnValueChange;
    public class OnEnterPressedEventArgs:EventArgs
    {
        public TextMeshProUGUI noText;
        public TextMeshProUGUI unitText;
    }

    [SerializeField] Button btn_1; 
    [SerializeField] Button btn_2; 
    [SerializeField] Button btn_3; 
    [SerializeField] Button btn_4; 
    [SerializeField] Button btn_5; 
    [SerializeField] Button btn_6; 
    [SerializeField] Button btn_7; 
    [SerializeField] Button btn_8; 
    [SerializeField] Button btn_9; 
    [SerializeField] Button btn_0; 
    [SerializeField] Button btn_ohm; 
    [SerializeField] Button btn_kilo; 
    [SerializeField] Button btn_mega; 
    [SerializeField] Button btn_point; 
    [SerializeField] Button btn_enter; 
    [SerializeField] Button btn_backspace;

    [SerializeField] TextMeshProUGUI noText;
    [SerializeField] TextMeshProUGUI unitText;
    const int MaxLettersNoOfNoText = 3;

    private void Start()
    {
        btn_0.onClick.AddListener(() => { NoButtonPressed(btn_0); ResetFeedbackTextOnValueChange(); SoundEffectsManager.Instance.KeyPadClick(); }) ;
        btn_1.onClick.AddListener(()=>{NoButtonPressed(btn_1); ResetFeedbackTextOnValueChange(); SoundEffectsManager.Instance.KeyPadClick(); });
        btn_2.onClick.AddListener(()=>{NoButtonPressed(btn_2); ResetFeedbackTextOnValueChange(); SoundEffectsManager.Instance.KeyPadClick(); });
        btn_3.onClick.AddListener(()=>{NoButtonPressed(btn_3); ResetFeedbackTextOnValueChange(); SoundEffectsManager.Instance.KeyPadClick(); });
        btn_4.onClick.AddListener(()=>{NoButtonPressed(btn_4); ResetFeedbackTextOnValueChange(); SoundEffectsManager.Instance.KeyPadClick(); });
        btn_5.onClick.AddListener(()=>{NoButtonPressed(btn_5); ResetFeedbackTextOnValueChange(); SoundEffectsManager.Instance.KeyPadClick(); });
        btn_6.onClick.AddListener(()=>{NoButtonPressed(btn_6); ResetFeedbackTextOnValueChange(); SoundEffectsManager.Instance.KeyPadClick(); });
        btn_7.onClick.AddListener(()=>{NoButtonPressed(btn_7); ResetFeedbackTextOnValueChange(); SoundEffectsManager.Instance.KeyPadClick(); });
        btn_8.onClick.AddListener(()=>{NoButtonPressed(btn_8); ResetFeedbackTextOnValueChange(); SoundEffectsManager.Instance.KeyPadClick(); });
        btn_9.onClick.AddListener(()=>{NoButtonPressed(btn_9); ResetFeedbackTextOnValueChange(); SoundEffectsManager.Instance.KeyPadClick(); });
        btn_point.onClick.AddListener(()=>{NoButtonPressed(btn_point); ResetFeedbackTextOnValueChange(); SoundEffectsManager.Instance.KeyPadClick(); });

        btn_ohm.onClick.AddListener(()=>{unityButtonPressed(btn_ohm); ResetFeedbackTextOnValueChange(); SoundEffectsManager.Instance.KeyPadClick(); });
        btn_kilo.onClick.AddListener(()=>{unityButtonPressed(btn_kilo); ResetFeedbackTextOnValueChange(); SoundEffectsManager.Instance.KeyPadClick(); });
        btn_mega.onClick.AddListener(()=>{unityButtonPressed(btn_mega); ResetFeedbackTextOnValueChange(); SoundEffectsManager.Instance.KeyPadClick(); });

        btn_enter.onClick.AddListener(EnterButtonPressed);
        btn_backspace.onClick.AddListener(()=>{BackspaceButtonPressed(); ResetFeedbackTextOnValueChange(); SoundEffectsManager.Instance.KeyPadClick(); });
    }
    private void NoButtonPressed(Button btn)
    {
        string previousText = noText.text;
        if(noText.text.Length < MaxLettersNoOfNoText)
        {
            noText.text = previousText + btn.name;
        }    
    }

    private void unityButtonPressed(Button btn)
    {
        unitText.text = btn.name;
        
    }

    private void EnterButtonPressed()
    {
        //send noText and unitText to parse its values
        OnEnterPressed?.Invoke(this, new OnEnterPressedEventArgs { noText = noText, unitText = unitText }) ;

    }

    private void BackspaceButtonPressed()
    {
        string previousText = noText.text;

        if (previousText.Length > 1)
        {
            noText.text = previousText.Substring(0, previousText.Length - 1);
        }
        else { noText.text = ""; }
    }

    private void ResetFeedbackTextOnValueChange() { OnValueChange?.Invoke(this, EventArgs.Empty); }
}

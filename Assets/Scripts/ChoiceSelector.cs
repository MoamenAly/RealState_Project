using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ChoiceSelector : MonoBehaviour
{
    [System.Serializable]
    public class ChoiceButton
    {
        public Button button;
        public GameObject border;
        public UnityEvent onComplete;
    }

    [Header("Choice Buttons")]
    public List<ChoiceButton> choices;

    [Header("Continue Button")]
    public Button continueButton;

    private ChoiceButton selectedChoice = null;

    void Start()
    {
        foreach (var choice in choices)
        {
            if (choice.border != null)
                choice.border.SetActive(false);

            ChoiceButton capturedChoice = choice;
            choice.button.onClick.AddListener(() =>
            {
                SelectChoice(capturedChoice);
            });
        }

        continueButton.interactable = false;
        continueButton.onClick.AddListener(OnContinueClicked);
    }

    private void SelectChoice(ChoiceButton selected)
    {
        foreach (var choice in choices)
        {
            if (choice.border != null)
                choice.border.SetActive(choice == selected);
        }

        selectedChoice = selected;
        continueButton.interactable = true;
    }

    private void OnContinueClicked()
    {
        if (selectedChoice != null)
        {
            selectedChoice.onComplete?.Invoke();
        }
    }
}

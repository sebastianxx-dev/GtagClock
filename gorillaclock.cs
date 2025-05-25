using BepInEx;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[BepInPlugin("com.seb.gorillaclock", "gorillaclock", "1.2.2")]
public class GorillaClock : BaseUnityPlugin
{
    private GameObject canvasObject;
    private TextMeshProUGUI timeText;
    private TextMeshProUGUI dateText;
    private GameObject timeObj;
    private GameObject dateObj;
    private Button toggleButton;

    private bool isVisible = true;

    void OnEnable()
    {
        Logger.LogInfo("GorillaClock enabled.");
        CreateClockUI();
        CreateToggleButton();
    }

    void Update()
    {
        if (timeText != null && isVisible)
            timeText.text = DateTime.Now.ToString("hh:mm tt");

        if (dateText != null && isVisible)
            dateText.text = DateTime.Now.ToString("dddd, MMMM dd");
    }

    private void CreateClockUI()
    {
        // Create Canvas
        canvasObject = new GameObject("ClockCanvas");
        DontDestroyOnLoad(canvasObject); // 🔒 Prevent destruction on scene load
        Canvas canvas = canvasObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasObject.AddComponent<CanvasScaler>();
        canvasObject.AddComponent<GraphicRaycaster>();

        // Create Time Text
        timeObj = new GameObject("ClockTimeText");
        timeObj.transform.SetParent(canvasObject.transform);
        timeText = timeObj.AddComponent<TextMeshProUGUI>();
        timeText.fontSize = 36;
        timeText.color = Color.white;
        timeText.alignment = TextAlignmentOptions.TopRight;
        timeText.text = DateTime.Now.ToString("hh:mm tt");

        RectTransform timeRect = timeText.GetComponent<RectTransform>();
        timeRect.anchorMin = new Vector2(1, 1);
        timeRect.anchorMax = new Vector2(1, 1);
        timeRect.pivot = new Vector2(1, 1);
        timeRect.anchoredPosition = new Vector2(-20, -20);
        timeRect.sizeDelta = new Vector2(400, 60);

        // Create Date Text
        dateObj = new GameObject("ClockDateText");
        dateObj.transform.SetParent(canvasObject.transform);
        dateText = dateObj.AddComponent<TextMeshProUGUI>();
        dateText.fontSize = 24;
        dateText.color = Color.white;
        dateText.alignment = TextAlignmentOptions.TopRight;
        dateText.text = DateTime.Now.ToString("dddd, MMMM dd");

        RectTransform dateRect = dateText.GetComponent<RectTransform>();
        dateRect.anchorMin = new Vector2(1, 1);
        dateRect.anchorMax = new Vector2(1, 1);
        dateRect.pivot = new Vector2(1, 1);
        dateRect.anchoredPosition = new Vector2(-20, -60);  // Below time
        dateRect.sizeDelta = new Vector2(400, 50);
    }

    private void CreateToggleButton()
    {
        // Create Button
        GameObject buttonObj = new GameObject("ToggleClockButton");
        buttonObj.transform.SetParent(canvasObject.transform);

        toggleButton = buttonObj.AddComponent<Button>();
        Image image = buttonObj.AddComponent<Image>();
        image.color = new Color(0.2f, 0.2f, 0.2f, 0.8f);

        // Add white outline
        Outline outline = buttonObj.AddComponent<Outline>();
        outline.effectColor = Color.white;
        outline.effectDistance = new Vector2(2, 2);

        RectTransform rect = buttonObj.GetComponent<RectTransform>();
        rect.anchorMin = new Vector2(1, 0);
        rect.anchorMax = new Vector2(1, 0);
        rect.pivot = new Vector2(1, 0);
        rect.anchoredPosition = new Vector2(-20, 20);
        rect.sizeDelta = new Vector2(100, 35);

        // Add label
        GameObject labelObj = new GameObject("ButtonLabel");
        labelObj.transform.SetParent(buttonObj.transform);
        TextMeshProUGUI label = labelObj.AddComponent<TextMeshProUGUI>();
        label.text = "Toggle";
        label.alignment = TextAlignmentOptions.Center;
        label.fontSize = 18;
        label.color = Color.white;

        RectTransform labelRect = label.GetComponent<RectTransform>();
        labelRect.anchorMin = new Vector2(0, 0);
        labelRect.anchorMax = new Vector2(1, 1);
        labelRect.offsetMin = Vector2.zero;
        labelRect.offsetMax = Vector2.zero;

        // Add listener
        toggleButton.onClick.AddListener(() =>
        {
            isVisible = !isVisible;
            timeObj.SetActive(isVisible);
            dateObj.SetActive(isVisible);
        });
    }
}

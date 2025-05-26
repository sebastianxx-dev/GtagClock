using BepInEx;
using UnityEngine;
using System;

[BepInPlugin("com.seb.GtagClock", "GtagClock", "1.0.0")]
public class TimeDisplayMod : BaseUnityPlugin
{
    private GUIStyle timeStyle;
    private int positionIndex = 0;
    private bool visible = true;
    private Vector2 boxSize = new Vector2(230, 90);

    void OnGUI()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown)
        {
            if (e.keyCode == KeyCode.K)
            {
                visible = !visible;
                Logger.LogInfo("Toggled clock visibility: " + visible);
            }
            else if (e.keyCode == KeyCode.M)
            {
                positionIndex = (positionIndex + 1) % 4;
                Logger.LogInfo("Moved clock position to: " + positionIndex);
            }
        }

        if (!visible) return;

        if (timeStyle == null)
        {
            timeStyle = new GUIStyle(GUI.skin.label)
            {
                alignment = TextAnchor.MiddleCenter,
                fontSize = 24,
                normal = { textColor = Color.white }
            };
        }

        string currentTime = DateTime.Now.ToString("hh:mm tt");
        string currentDay = DateTime.Now.ToString("dddd, MMMM dd");

        Rect boxRect = GetBoxRect(positionIndex);

        GUI.color = new Color(0, 0, 0, 0.75f);
        GUI.Box(boxRect, GUIContent.none);
        GUI.color = Color.white;

        GUI.Label(boxRect, $"{currentTime}\n{currentDay}", timeStyle);
    }

    private Rect GetBoxRect(int pos)
    {
        float x = 0, y = 0;
        switch (pos)
        {
            case 0: x = Screen.width - boxSize.x - 20; y = 20; break; 
            case 1: x = 20; y = 20; break; 
            case 2: x = 20; y = Screen.height - boxSize.y - 20; break; 
            case 3: x = Screen.width - boxSize.x - 20; y = Screen.height - boxSize.y - 20; break; 
        }
        return new Rect(x, y, boxSize.x, boxSize.y);
    }
}
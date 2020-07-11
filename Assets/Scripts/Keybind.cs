using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keybind : MonoBehaviour
{
    BoxCollider2D coll;
    bool dragging;
    bool disabled;

    public Color disabledColor;

    bool isConfiguring;
    KeyCode newKey;
    public Text keyText;

    public KeyCode key;

    public ControlSlot currentSlot;

    public InputUIManager manager;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //print(GameInputManager.isConfiguringControls);
        if (isConfiguring)
        {
            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyUp(kcode))
                {
                    key = kcode;
                    keyText.text = kcode.ToString();
                    isConfiguring = false;
                    GameInputManager.SetKeyMap(currentSlot.actionName, kcode);
                    GameInputManager.isConfiguringControls = false;
                    break;
                }
            }
            return;
        }

        if (coll.bounds.Contains(Input.mousePosition) && Input.GetMouseButtonDown(0) && !disabled)
        {
            dragging = true;
            GameInputManager.isConfiguringControls = true;
        }
        else
        {
            if (coll.bounds.Contains(Input.mousePosition) && Input.GetMouseButtonUp(0) && dragging)
            {
                SnapToSlot();
                dragging = false;
            }
        }


        if (dragging)
        {
            transform.position = Input.mousePosition;
        }
        else
        {
            //transform.position = currentSlot.position;
            GetComponent<RectTransform>().position = currentSlot.GetComponent<RectTransform>().position;
            //GetComponent<RectTransform>().anchorMax = currentSlot.GetComponent<RectTransform>().anchorMax;
        }
    }

    void SnapToSlot()
    {
        ControlSlot closestSlot = manager.slotPositions[0];
        float minDistance = (manager.slotPositions[0].position - transform.position).sqrMagnitude;

        foreach (ControlSlot t in manager.slotPositions)
        {
            float dist = (t.position - transform.position).sqrMagnitude;

            if (dist < minDistance)
            {
                minDistance = dist;
                closestSlot = t;
            }
        }

        if (closestSlot.actionName.Equals(currentSlot.actionName))
        {
            isConfiguring = true;
            GameInputManager.isConfiguringControls = true;
        }
        else
        {
            Keybind otherKey = closestSlot.button;

            closestSlot.button = this;
            currentSlot.button = otherKey;
            if (otherKey != null)
            {
                otherKey.SetSlot(currentSlot);
            }
            else
            {
                GameInputManager.SetKeyMap(currentSlot.actionName, KeyCode.None);
            }
            SetSlot(closestSlot);

            GameInputManager.isConfiguringControls = false;
        }
    }

    public void SetSlot(ControlSlot slot)
    {
        currentSlot = slot;
        GameInputManager.SetKeyMap(slot.actionName, key);
    }

    public void Disable()
    {
        disabled = true;
        GetComponent<Image>().color = disabledColor;
    }

    public void Enable()
    {
        disabled = false;
        GetComponent<Image>().color = Color.white;
    }
}

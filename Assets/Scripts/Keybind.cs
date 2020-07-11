using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keybind : MonoBehaviour
{
    BoxCollider2D coll;
    bool dragging;

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
        if (coll.bounds.Contains(Input.mousePosition) && Input.GetMouseButtonDown(0))
        {
            dragging = true;
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                SnapToSlot();
                dragging = false;
            }
        }
        //Vector3[] test = new Vector3[4];
        //print("this: " + coll.bounds.ToString());
        //GetComponent<RectTransform>().GetWorldCorners(test);
        //print("this: "+test[0]);
        //print("mouse: " + Input.mousePosition);
        if (dragging)
        {
            transform.position = Input.mousePosition;
        }
        else
        {
            transform.position = currentSlot.position;
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


    }

    public void SetSlot(ControlSlot slot)
    {
        currentSlot = slot;
        GameInputManager.SetKeyMap(slot.actionName, key);
    }
}

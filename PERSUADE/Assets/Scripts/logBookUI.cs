using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logBookUI : MonoBehaviour {

    public Canvas logBook;
    public List<CanvasRenderer> canvasElements;
    public List<GameObject> selectiveArray;
    public List<GameObject> clickableArray;
    private int max;
    private int min;
    public int current = 0;
    public ConversationHandler cHandler;
    public bool isLogBook = false;
    private Vector3 mousePos;

	void Start () {

        canvasElements.AddRange(logBook.GetComponentsInChildren<CanvasRenderer>());
        for (int i = 0; i < canvasElements.Count-1; i++)
        {
            if (canvasElements[i].gameObject.name.Contains("Outline"))
                selectiveArray.Add(canvasElements[i].gameObject);
        }

        for (int i = 0; i < canvasElements.Count-1; i++)
        {
            if (canvasElements[i].gameObject.name.Contains("SubMenu"))
                clickableArray.Add(canvasElements[i].gameObject);
        }

        for (int i = 0; i < clickableArray.Count; i++)
        {
            clickableArray[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < selectiveArray.Count; i++)
        {
            selectiveArray[i].gameObject.SetActive(false);
        }

        cHandler = FindObjectOfType<ConversationHandler>();

        canvasElements[1].gameObject.SetActive(false);
        min = 0;
        max = selectiveArray.Count;
	}
	
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.F2))
        {
            canvasElements[0].gameObject.SetActive(!canvasElements[0].gameObject.activeInHierarchy);
            canvasElements[1].gameObject.SetActive(!canvasElements[1].gameObject.activeInHierarchy);
            isLogBook = !isLogBook;
            cHandler.lockCursor = !cHandler.lockCursor;
        }

        if(isLogBook)
        {

            cHandler.playerControl.input = new Vector2(0, 0);
            cHandler.PlayerController.GetComponent<Invector.CharacterController.vThirdPersonInput>().enabled = false;

            //if (Input.GetAxis("Mouse ScrollWheel") >= 0.05 && current <= max) {
            //    current += 1;
            //    switchSelection();

            //}
            //else if(current > max)
            //{
            //    current = min;
            //    switchSelection();
            //}

            //if(Input.GetAxis("Mouse ScrollWheel") <= -0.05 && current >= min)
            //{
            //    current -= 1;
            //    switchSelection();
            //}
            //else if(current < min)
            //{
            //    current = max-1;
            //    switchSelection();
            //}

        }

	}

    //void switchSelection()
    //{
    //    for (int i = 0; i < selectiveArray.Count; i++)
    //    {
    //        if(i != current)
    //        {
    //            selectiveArray[i].gameObject.SetActive(false);
    //        }
    //        else if(i == current)
    //        {
    //            selectiveArray[i].gameObject.SetActive(true);
    //        }
                
    //    }
    //}

    //public void setCurrent(int val)
    //{
    //    current = val;
    //    switchSelection();
    //}

    public void clickCurrent(int val)
    {
        current = val;
        clickSelection();
    }

    void clickSelection()
    {
        for (int i = 0; i < clickableArray.Count; i++)
        {
            if (i != current)
            {
                clickableArray[i].gameObject.SetActive(false);
            }
            else if (i == current)
            {
                clickableArray[i].gameObject.SetActive(true);
            }

        }
    }
}

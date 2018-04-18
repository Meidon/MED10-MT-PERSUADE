using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class logBookUI : MonoBehaviour
{

    public Canvas logBook;
    public List<CanvasRenderer> canvasElements;
    public List<GameObject> selectiveArray;
    public List<GameObject> clickableArray;
    public List<TMPro.TextMeshProUGUI> chatbotLogUIArray;
    private List<string> tempTextHolder;
    private Narrator n;
    private int max;
    private int min;
    public int current = 0;
    public ConversationHandler cHandler;
    public bool isLogBook = false;
    private Vector3 mousePos;
    public Canvas PuzzleCanvas;

    void Start()
    {
        
        canvasElements.AddRange(logBook.GetComponentsInChildren<CanvasRenderer>());
        for (int i = 0; i < canvasElements.Count - 1; i++)
        {
            if (canvasElements[i].gameObject.name.Contains("Outline"))
                selectiveArray.Add(canvasElements[i].gameObject);
        }

        for (int i = 0; i < canvasElements.Count - 1; i++)
        {
            if (canvasElements[i].gameObject.name.Contains("SubMenu"))
                clickableArray.Add(canvasElements[i].gameObject);
        }

        for (int i = 0; i < clickableArray.Count; i++)
        {
            chatbotLogUIArray.Add(clickableArray[i].GetComponentInChildren<TMPro.TextMeshProUGUI>());
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
        n = FindObjectOfType<Narrator>();
        canvasElements[1].gameObject.SetActive(false);
        StartCoroutine(DeactivationDelay());
        min = 0;
        max = selectiveArray.Count;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F2))
        {
            canvasElements[0].gameObject.SetActive(!canvasElements[0].gameObject.activeInHierarchy);
            canvasElements[1].gameObject.SetActive(!canvasElements[1].gameObject.activeInHierarchy);
            PuzzleCanvas.enabled = !PuzzleCanvas.enabled;
            isLogBook = !isLogBook;
            //cHandler.lockCursor = !cHandler.lockCursor;
        }

        if (isLogBook)
        {

            //cHandler.playerControl.input = new Vector2(0, 0);
            //cHandler.PlayerController.GetComponent<Invector.CharacterController.vThirdPersonInput>().enabled = false;

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


        for (int i = 0; i < n.DataLog.Count; i++)
        {
            if (n.DataLog[i].Contains("Dorian:") && !chatbotLogUIArray[0].text.Contains(n.DataLog[i].ToString()))
                 chatbotLogUIArray[0].text += ("\n- " + n.DataLog[i].ToString());
            if (n.DataLog[i].Contains("Collene:") && !chatbotLogUIArray[1].text.Contains(n.DataLog[i].ToString()))
                chatbotLogUIArray[1].text += ("\n- " + n.DataLog[i].ToString());
            if (n.DataLog[i].Contains("Eryn:") && !chatbotLogUIArray[2].text.Contains(n.DataLog[i].ToString()))
                chatbotLogUIArray[2].text += ("\n- " + n.DataLog[i].ToString());
            if (n.DataLog[i].Contains("Shenna:") && !chatbotLogUIArray[3].text.Contains(n.DataLog[i].ToString()))
                chatbotLogUIArray[3].text += ("\n- " + n.DataLog[i].ToString());
            if (n.DataLog[i].Contains("Keno:") && !chatbotLogUIArray[4].text.Contains(n.DataLog[i].ToString()))
                chatbotLogUIArray[4].text += ("\n- " + n.DataLog[i].ToString());
            if (n.DataLog[i].Contains("Tanya:") && !chatbotLogUIArray[5].text.Contains(n.DataLog[i].ToString()))
                chatbotLogUIArray[5].text += ("\n- " + n.DataLog[i].ToString());
            if (n.DataLog[i].Contains("Keira:") && !chatbotLogUIArray[6].text.Contains(n.DataLog[i].ToString()))
                chatbotLogUIArray[6].text += ("\n- " + n.DataLog[i].ToString());
            if (n.DataLog[i].Contains("Denna:") && !chatbotLogUIArray[7].text.Contains(n.DataLog[i].ToString()))
                chatbotLogUIArray[7].text += ("\n- " + n.DataLog[i].ToString());
            if (n.DataLog[i].Contains("Veronika:") && !chatbotLogUIArray[8].text.Contains(n.DataLog[i].ToString()))
                chatbotLogUIArray[8].text += ("\n- " + n.DataLog[i].ToString());
            if (n.DataLog[i].Contains("Gavin:") && !chatbotLogUIArray[9].text.Contains(n.DataLog[i].ToString()))
                chatbotLogUIArray[9].text += ("\n- " + n.DataLog[i].ToString());
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

    IEnumerator DeactivationDelay()
    {
        yield return new WaitForSeconds(0.5f);
        PuzzleCanvas.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpSystem : MonoBehaviour
{

    public Text txt;

    // Start is called before the first frame update
    public void PopUp(string text)
    {

        txt.text = text;
        txt.enabled = true;
        StartCoroutine(waiter());

    }
    IEnumerator waiter()
    {
        yield return new WaitForSeconds(3);
        txt.enabled = false;
    }
}

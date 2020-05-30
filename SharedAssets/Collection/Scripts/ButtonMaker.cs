using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ON{

public class ButtonMaker : MonoBehaviour
{
    public Vector2 howManyButtons;
    public Vector2 spacing;
    public GameObject button;
    public List<GameObject> buttons;
    public GameObject container;
    public int colorChild;
    public int interactableChild;
    public bool addToDisableList = true;

    void Start()
    {
        buttons = new List<GameObject>();
        for (int i = 0; i < howManyButtons.x; i++)
        {
            for (int j = 0; j < howManyButtons.y; j++)
            {
                GameObject g = Instantiate(button);
                g.transform.position = new Vector3(i*spacing.x, j*spacing.y, 0);
                buttons.Add(g);
                g.transform.parent = container.transform;
            }

        }
        for (int i = 0; i < buttons.Count; i++)
        {

            GameObject g = buttons[i];
            g.transform.GetChild(interactableChild).GetComponent<Interactable_EnableDisable>().enable = new GameObject[1];
            g.transform.GetChild(interactableChild).GetComponent<Interactable_EnableDisable>().enable[0] =
                g.transform.GetChild(colorChild).gameObject;
            if (addToDisableList)
            {
                g.transform.GetChild(interactableChild).GetComponent<Interactable_EnableDisable>().disable = new GameObject[buttons.Count-1];
                int c = 0;

                for (int j = 0; j < buttons.Count; j++)
                {
                    if (j != i)
                    {
                        g.transform.GetChild(interactableChild).GetComponent<Interactable_EnableDisable>().disable[c] =
                            buttons[j].transform.GetChild(colorChild).gameObject;
                        c++;
                    }

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


}
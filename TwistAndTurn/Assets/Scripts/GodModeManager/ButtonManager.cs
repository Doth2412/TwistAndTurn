using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    public GameObject[] buttons;
    public GameObject[] chooseButtons;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // Capture the current index for the lambda
            chooseButtons[i].SetActive(false);
            GameObject prefab = buttons[i].GetComponent<Icon>().buidablePrefab;
            buttons[i].GetComponent<Button>().onClick.AddListener(() =>
            {
                for (int j = 0; j < chooseButtons.Length; j++)
                {
                    chooseButtons[j].SetActive(false);
                }
                // Activate the clicked button's chooseButton
                if (prefab != GridSystem.instance.currentBuildableObject)
                    chooseButtons[index].SetActive(true);
                GridSystem.instance.SelectBuildablePrefab(prefab);
            });
        }
    }
    
    public void ResetChoice()
    {
        for (int j = 0; j < chooseButtons.Length; j++)
        {
            chooseButtons[j].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }



}

using UnityEngine;
using UnityEngine.UI;

public class Brush : MonoBehaviour
{
    [SerializeField]
    GameObject panel;
    bool panel_active = false;


    Button brush;

    private void Awake()
    {
        brush = GetComponent<Button>();
    }
    public void Button_up()
    {
        panel_active = !panel_active;
        panel.SetActive(panel_active);

        if(panel_active == false)
        {
            ColorBlock change_Button_Color = brush.colors;
            change_Button_Color.normalColor = Color.white;
            change_Button_Color.selectedColor = Color.white;
            brush.colors = change_Button_Color;
        }
        else
        {
            ColorBlock change_Button_Color = brush.colors;
            change_Button_Color.normalColor = Color.green;
            change_Button_Color.selectedColor = Color.green;
            brush.colors = change_Button_Color;
        }

    }
}

using UnityEngine;
using UnityEngine.UI;

public class UIBar : MonoBehaviour
{
    public PersistentData persistentData;

    public Text details;
    public Image bar;

    public void UpdateBar(float currentValue, float maxValue)
    {
        if (maxValue != 0 && currentValue > 0)
        {
            details.text = ((int)currentValue).ToString() + " / " + maxValue.ToString();
            bar.fillAmount = currentValue / maxValue;
        }
        else
        {
            details.text = "";
            bar.fillAmount = 0.0f;
        }
    }

    private void Update()
    {
        UpdateBar(persistentData.currentStamina, persistentData.STAMINA_DURATION);
    }
}

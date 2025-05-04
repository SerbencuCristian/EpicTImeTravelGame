using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class EnergyBarUI : MonoBehaviour
{
    public float currentEnergy;
    public TextMeshProUGUI energyText;
    public float maxEnergy = 100f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        energyText.text = currentEnergy.ToString("0") + " / " + maxEnergy.ToString("0");
    }

    // Update is called once per frame
    void Update()
    {
        energyText.text = currentEnergy.ToString("0") + " / " + maxEnergy.ToString("0");
        GetComponent<Image>().fillAmount = currentEnergy / maxEnergy;
    }


}

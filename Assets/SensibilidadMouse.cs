using UnityEngine;
using UnityEngine.UI;

public class SensibilidadMouse : MonoBehaviour
{
    public Slider slider;
    public float sliderValue;
    private RotationCamara rotationCamara;

    void Start()
    {
        rotationCamara = FindObjectOfType<RotationCamara>();


        if (PlayerPrefs.HasKey("MouseSensitivity"))
        {
            sliderValue = PlayerPrefs.GetFloat("MouseSensitivity");
        }
        else
        {
            sliderValue = 200f; 
        }

        slider.value = sliderValue;
        AplicarSensibilidad(sliderValue);


        slider.minValue = 50f;  
        slider.maxValue = 500f; 


        slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
    }

    void OnSliderValueChanged()
    {
        sliderValue = slider.value;
        AplicarSensibilidad(sliderValue);


        PlayerPrefs.SetFloat("MouseSensitivity", sliderValue);
        PlayerPrefs.Save();
    }

    void AplicarSensibilidad(float valor)
    {
        if (rotationCamara != null)
        {
            rotationCamara.mouseSensitivity = valor;
        }
    }
}

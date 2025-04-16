using UnityEngine;
using UnityEngine.UI;

public class SensibilidadMouse : MonoBehaviour
{
    public Slider sliderMouse;
    public float sliderValueMouse;
    private RotationCamara rotationCamara;

    void Start()
    {
        rotationCamara = FindObjectOfType<RotationCamara>();


        if (PlayerPrefs.HasKey("MouseSensitivity"))
        {
            sliderValueMouse = PlayerPrefs.GetFloat("MouseSensitivity");
        }
        else
        {
            sliderValueMouse = 200f; 
        }

        sliderMouse.value = sliderValueMouse;
        AplicarSensibilidad(sliderValueMouse);


        sliderMouse.minValue = 50f;  
        sliderMouse.maxValue = 500f; 


        sliderMouse.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });
    }

    void OnSliderValueChanged()
    {
        sliderValueMouse = sliderMouse.value;
        AplicarSensibilidad(sliderValueMouse);


        PlayerPrefs.SetFloat("MouseSensitivity", sliderValueMouse);
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

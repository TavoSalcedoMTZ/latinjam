using UnityEngine;
using TMPro;

public class TextoParpadeante : MonoBehaviour
{
    public float velocidadParpadeo = 20F; 
    public float alphaMin = 0;       
    public float alphaMax = 1f;          

    private TextMeshProUGUI texto;
    private float tiempo;

    void Start()
    {
        texto = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        tiempo += Time.deltaTime * velocidadParpadeo;


        float alpha = Mathf.Lerp(alphaMin, alphaMax, Mathf.PingPong(tiempo, 1));

   
        Color colorActual = texto.color;
        colorActual.a = alpha;
        texto.color = colorActual;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; 

public class Cambiarnivel : MonoBehaviour
{
    public Image panelMuerte;
    public Image panelGanar;
    public float fadeDuration = 2f;

    public void CambiarEscena(string nombre)
    {

        SceneManager.LoadSceneAsync(nombre);
    }

    public void ActivarMuerte()
    {
        StartCoroutine(Muerte());
    }

    public void ActivarGanar()
    {
        StartCoroutine(Ganar());
    }

    IEnumerator Muerte()
    {
        yield return new WaitForSeconds(2);
        yield return StartCoroutine(FadeIn(panelMuerte));
        yield return new WaitForSeconds(2);
        SceneManager.LoadSceneAsync("Perder");
    }

    IEnumerator Ganar()
    {
         yield return new WaitForSeconds(2);
        yield return StartCoroutine(FadeIn(panelGanar));
        yield return new WaitForSeconds(2);

        SceneManager.LoadSceneAsync("victoria");
    }

    IEnumerator FadeIn(Image image)
    {
        float time = 0f;
        Color color = image.color;
        color.a = 0f;
        image.color = color;
        image.gameObject.SetActive(true); // Por si estaba desactivado

        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(0f, 1f, time / fadeDuration);
            image.color = new Color(color.r, color.g, color.b, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        image.color = new Color(color.r, color.g, color.b, 1f);
    }
}

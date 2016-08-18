using UnityEngine;
using System.Collections;

public class UISpecialEventWindow : UIWindow
{
    public AudioSource music;
    public SpecialEvent specEvent;

    public override void Show()
    {
        base.Show();
        music.Play();
        StartCoroutine(FadeIn());
    }

    public override void Hide()
    {
        music.Stop();
        StartCoroutine(FadeOut());
        base.Hide();
    }

    public void OnChangeYes()
    {
        specEvent.Run(music.time);
        Hide();
    }

    IEnumerator FadeIn()
    {
        CanvasRenderer[] canvasRenderers = this.GetComponentsInChildren<CanvasRenderer>();
        /*foreach(CanvasRenderer cr in canvasRenderers)
        {
            cr.SetAlpha(1.0f);
        }*/

        float _alpha = 0;
        while(_alpha < 1.0f)
        {
            foreach(CanvasRenderer cr in canvasRenderers)
            {
                cr.SetAlpha(_alpha);

            }
            _alpha = Mathf.MoveTowards(_alpha, 1.0f, Time.deltaTime * 1.0f);
            yield return null;
        }

        yield return null;
    }

    IEnumerator FadeOut()
    {
        CanvasRenderer[] canvasRenderers = this.GetComponentsInChildren<CanvasRenderer>();
        /*foreach(CanvasRenderer cr in canvasRenderers)
        {
            cr.SetAlpha(0.0f);
        }*/

        float _alpha = 1.0f;
        while(_alpha > 0)
        {
            foreach(CanvasRenderer cr in canvasRenderers)
            {
                cr.SetAlpha(_alpha);

            }
            _alpha = Mathf.MoveTowards(_alpha, 0, Time.deltaTime * 1.0f);
            yield return null;
        }

        yield return null;
    }
}

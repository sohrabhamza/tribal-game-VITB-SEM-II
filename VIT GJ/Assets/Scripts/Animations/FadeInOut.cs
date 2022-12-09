using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    [SerializeField] bool show;
    [SerializeField] float fadeSpeed = 1.25f;
    SpriteRenderer ren;
    float alph;
    bool isOn = true;
    public List<int> en;

    private void Awake()
    {
        ren = GetComponent<SpriteRenderer>();
        if (!show)
        {
            Color newCol = ren.color;
            newCol.a = 0;
            ren.color = newCol;
        }
    }

    private void Update()
    {
        Color newCol = ren.color;
        if (Mathf.Abs(ren.color.a - alph) > 0.2f)
        {
            newCol.a = Mathf.Lerp(newCol.a, alph, Time.deltaTime * fadeSpeed);
        }
        else
        {
            newCol.a = alph;
            if ((alph == 0 ? false : true) != isOn)
            {
                // foreach (Transform gam in transform)
                // {
                //     gam.gameObject.SetActive(!isOn);
                // }
                EnableObjects(!isOn);
                isOn = !isOn;
            }
        }
        ren.color = newCol;
    }

    void EnableObjects(bool enable)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(en.Contains(i) && enable);
        }
    }

    public void FadeOut()
    {
        alph = 0;
    }

    public void FadeIn()
    {
        alph = 1;
    }
}

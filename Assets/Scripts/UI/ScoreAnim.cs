using UnityEngine;
using System.Collections;

public class ScoreAnim : MonoBehaviour {

    RectTransform rt;
    Vector2 from;
    Vector2 to;

    void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
        from = new Vector2(rt.anchoredPosition.x, 130f);
        to = new Vector2(rt.anchoredPosition.x, 10f);
    }

    public void Move()
    {
        iTween.ValueTo(gameObject, iTween.Hash(
            "from", from,
            "to", to,
            "time", 1f,
            "easetype", "easeOutElastic",
            "oncomplete", "Finish",
            "onupdate", "Up"
            ));
    }

    private void Up(Vector2 pos)
    {
        rt.anchoredPosition = pos;
    }

    public IEnumerator Finish()
    {
        yield return Wait(4);
        iTween.MoveTo(gameObject, iTween.Hash(
            "y", 80f,
            "time", 1f,
            "islocal", true
        ));
    }


    public IEnumerator Wait(float t)
    {
        float curT = 0f;
        while (curT < t)
        {
            curT += Time.deltaTime;
            yield return 0;
        }
    }
}

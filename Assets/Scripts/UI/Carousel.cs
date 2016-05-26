using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Carousel : MonoBehaviour {

    public List<Image> images;

    private Vector3 moveNext;
    private Vector3 movePrevious;

    void Awake()
    {
        int index = 1;
        Vector3 posInit = new Vector3(0f, 0f, 0f);
        foreach (Image im in images)
        {
            // Position
            posInit.x = (index * (Screen.width));
            im.transform.localPosition = posInit;
            index++;
        }
    }

    void Start () {

        moveNext = new Vector3(-(Screen.width), 0f);
        movePrevious = new Vector3((Screen.width), 0f);

        iTween.MoveBy(gameObject, iTween.Hash("amount", moveNext,
            "time", 1,
            "islocal", true,
            "easetype", "easeInOutSine",
            "ignoretimescale", true));
    }

    public void NextImage()
    {
        iTween.MoveBy(gameObject, iTween.Hash("amount", moveNext,
            "time", 1,
            "islocal", true,
            "easetype", "easeInOutSine",
            "ignoretimescale", true));
    }

    public void Previous()
    {
        iTween.MoveBy(gameObject, iTween.Hash("amount", movePrevious,
            "time", 1,
            "islocal", true,
            "easetype", "easeInOutSine",
            "ignoretimescale", true));
    }

    public void ReinitPosition()
    {
        Awake();
    }
}
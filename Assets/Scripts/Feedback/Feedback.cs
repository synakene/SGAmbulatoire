using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Feedback
{
    private int idConv;
    private int idNode;
    private int idGoodAnswer;
    private string text;

    public Feedback(int _idConv, int _idNode, int _idGoodAnswer, string _text)
    {
        idConv = _idConv;
        idNode = _idNode;
        idGoodAnswer = _idGoodAnswer;
        text = _text;
    }

    public override string ToString()
    {
        return text;
    }

    public int IdConv
    {
        get
        {
            return idConv;
        }

        set
        {
            idConv = value;
        }
    }

    public int IdNode
    {
        get
        {
            return idNode;
        }

        set
        {
            idNode = value;
        }
    }

    public int IdGoodAnswer
    {
        get
        {
            return idGoodAnswer;
        }

        set
        {
            idGoodAnswer = value;
        }
    }

    public string Text
    {
        get
        {
            return text;
        }

        set
        {
            text = value;
        }
    }
}

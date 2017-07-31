using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipRandomizer : MonoBehaviour
{
    public string[] tips;
    private Text textComp;

	// Use this for initialization
	void Start ()
    {
        textComp = GetComponent<Text>();
        ChageTip();
    }

    void OnEnable()
    {
            ChageTip();
    }

    private void ChageTip()
    {
        //TODO FIX NON EXISTING ERROR
        textComp.text = tips[Random.Range(0, tips.Length)];
    }
}

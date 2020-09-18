using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulationUI : MonoBehaviour
{
    public Text genText;
    public Text countText;
    public Text birthsText;
    public Text deathsText;
    
    void Update()
    {
        genText.text = Population.instance.maxGen.ToString();
        countText.text = Population.instance.individualCount.ToString();
        birthsText.text = Population.instance.births.ToString();
        deathsText.text = Population.instance.deaths.ToString();
    }
}

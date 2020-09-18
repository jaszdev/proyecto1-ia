using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

public class StatsManager : MonoBehaviour
{
    public float sampleTime = 5f;
    public string fileName;
    StringBuilder csvContent;
    void Start()
    {
        csvContent = new StringBuilder();
        csvContent.AppendLine("Time,IndCount,Births,Deaths,MaxGen,AvgHunger,AvgDrive,AvgHealth,AvgAge");

        StartCoroutine(SampleCoroutine());
    }

    IEnumerator SampleCoroutine()
    {
        while(true)
        {
            Sample();
            yield return new WaitForSeconds(sampleTime);
        }
    }

    void Sample()
    {
        int time = Mathf.RoundToInt(Time.time);
        int indCount = Population.instance.individualCount;
        int births = Population.instance.births;
        int deaths = Population.instance.deaths;
        int maxGen = Population.instance.maxGen;
        int avgHunger = Population.instance.AvgHunger();
        int avgDrive = Population.instance.AvgDrive();
        int avgHealth = Population.instance.AvgHealth();
        int avgAge = Mathf.RoundToInt(Population.instance.AvgAge());

        //constantes


        csvContent.AppendLine(time + "," + indCount + "," + births + "," + deaths + "," 
            + maxGen + "," + avgHunger + "," + avgDrive + "," + avgHealth + "," + avgAge);
    }

    private void OnDestroy()
    {
        File.AppendAllText(fileName, csvContent.ToString());
    }

}

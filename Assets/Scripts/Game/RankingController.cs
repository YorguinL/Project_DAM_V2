using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RankingController : MonoBehaviour
{
     string [] results = new string [5];

    string [] pos1 = new string[2];
    string [] pos2 = new string[2];
    string [] pos3 = new string[2];
    string [] pos4 = new string[2];
    string [] pos5 = new string[2];
    public Text [] top5Nickname = new Text [5];
    public Text [] top5Score = new Text [5];
    
    // Start is called before the first frame update
    void Start()
    {
        DataBaseController bbdd = new DataBaseController();
        bbdd.Start();
        results = bbdd.Top5();
        SeparateValues();
        Results();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SeparateValues(){
        for(int i = 0; i < results.Length; i++){
            if(i == 0){
                pos1 = results[i].Split(' ');
            } 
            if(i == 1){
                pos2 = results[i].Split(' ');
            }
            if(i == 2){
                pos3 = results[i].Split(' ');
            }
            if(i == 3){
                pos4 = results[i].Split(' ');
            }
            if(i == 4){
                pos5 = results[i].Split(' ');
            }
        }
    }

    void Results(){
        top5Nickname[0].text = pos1[0];
        top5Score[0].text = pos1[1];

        top5Nickname[1].text = pos2[0];
        top5Score[1].text = pos2[1];

        top5Nickname[2].text = pos3[0];
        top5Score[2].text = pos3[1];

        top5Nickname[3].text = pos4[0];
        top5Score[3].text = pos4[1];

        top5Nickname[4].text = pos5[0];
        top5Score[4].text = pos5[1];
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InputsController : MonoBehaviour
{
    public InputField user;
    static string nickName;
    public static string NickName { get => nickName; set => nickName = value;}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        nickName = user.text;
    }

    // Register player
    public  void SavePlayer(){
        if(nickName != " "){
            DataBaseController ddbb = new DataBaseController();
            ddbb.Start();
            ddbb.CheckPlayer(nickName);
            SceneManager.LoadScene(5);
        }
    }

    // Search player game
    public void SelectGame(){
        if(nickName != " "){
            DataBaseController ddbb = new DataBaseController();
            ddbb.Start();
            ddbb.CheckSavedGame(nickName);
            SceneManager.LoadScene(5);
        }
    }
}

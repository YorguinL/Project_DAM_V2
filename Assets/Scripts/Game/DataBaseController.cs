using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Mono.Data.Sqlite;
using System.Data;
using System;

public class DataBaseController : MonoBehaviour
{
    private string conn, sqlQuery;
    private IDbConnection dbconn;
    private IDbCommand dbcmd;
    private IDataReader reader;
    string dataBaseName = "Project_DAM.db";
    private string[] results = new string[5];
    private int idPlayer;
    private int idGame;
    private int score;
    private int gameSaved;

    // Start is called before the first frame update
    public void Start()
    {
        //Path to database.
        string filepath = Application.dataPath + "/BBDD/" + dataBaseName; 
        
        //Open connection to the database.
        conn = "URI=file:"  + filepath;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open(); 
        print("Connectat a la base de dades");

    }

    // Update is called once per frame
    void Update()
    {
 

    }

    // Select id player
    public void SelectIdPlayer(string nickName){

        using(dbconn = new SqliteConnection(conn)){

            dbconn.Open();
            dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT Id FROM Players WHERE NickName = \"" + nickName +"\";";
            dbcmd.CommandText = sqlQuery;
            reader = dbcmd.ExecuteReader();

            while(reader.Read()){
                idPlayer = reader.GetInt32(0);
            }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
        }
    }

    // Select id and saved game
     public void SelectIdAndSavedGame(int idPl){

        using(dbconn = new SqliteConnection(conn)){

            dbconn.Open();
            dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT Id, Saved FROM Games WHERE Id = (SELECT MAX(Id) FROM Games WHERE IdPlayer = " + idPl + ");";
            dbcmd.CommandText = sqlQuery;
            reader = dbcmd.ExecuteReader();

            while(reader.Read()){
                idGame = reader.GetInt32(0);
                gameSaved = reader.GetInt32(1);
            }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
        }
        
    }

    // Select score
    private void SelectScore(int idPl){

        using(dbconn = new SqliteConnection(conn)){

            dbconn.Open();
            dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT Score FROM Games " +
                              "WHERE IdPlayer = " + idPl + 
                              " ORDER BY Score LIMIT 1;";
            dbcmd.CommandText = sqlQuery;
            reader = dbcmd.ExecuteReader();

            while(reader.Read()){
                score = reader.GetInt32(0);
            }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
        }

    }

    // Load game
    private void SelectGame(int health, int healthItems, int fireRate, int playerSpeed, int killedEnemies, int level, int score, int saved){

        using(dbconn = new SqliteConnection(conn)){

            dbconn.Open();
            dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT * FROM Games " +
                              "WHERE Id = " + idGame + ";";
            dbcmd.CommandText = sqlQuery;
            reader = dbcmd.ExecuteReader();

            while(reader.Read()){
                idGame = reader.GetInt32(0);
                idPlayer = reader.GetInt32(1);
                health = reader.GetInt32(2);
                healthItems = reader.GetInt32(3);
                fireRate = reader.GetInt32(4);
                playerSpeed = reader.GetInt32(5);
                killedEnemies = reader.GetInt32(6);
                level = reader.GetInt32(7);
                score = reader.GetInt32(8);
                gameSaved = reader.GetInt32(9);
            }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
        }

    }

    // Insert game
    private void InsertGame(int idPl) {

        using (dbconn = new SqliteConnection(conn)){

            dbconn.Open(); 
            dbcmd = dbconn.CreateCommand();
            sqlQuery = "INSERT INTO Games (IdPlayer, Health, HealthItems, FireRate, PlayerSpeed, KilledEnemies, Level, Score, Saved) " + 
                       "VALUES(\"" + idPl + "\", 6, 0, 0, 0, 0, 1, 0, 0);";
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            dbconn = null;
        }

    }

    // Update game record
    private void UpdateGameRecord(int id, int health, int healthItems, int fireRate, int playerSpeed, int killedEnemies, int level, int scr, int saved){

         using (dbconn = new SqliteConnection(conn)){

            dbconn.Open(); 
            dbcmd = dbconn.CreateCommand();
            sqlQuery = string.Format("UPDATE Games " + 
                                     "SET Health = @health, " + 
                                     "HealthItems = @healthItems, " +
                                     "FireRate = @fireRate, " +
                                     "PlayerSpeed = @playerSpeed, " +
                                     "KilledEnemies = @killedEnemies, " +
                                     "Level = @level, " +
                                     "Score = @score, " +
                                     "Saved = @saved " +
                                     "WHERE Id = @id");
            
            SqliteParameter p_id =  new SqliteParameter("@id", id);
            SqliteParameter p_health =  new SqliteParameter("@health", health);
            SqliteParameter p_healthItems =  new SqliteParameter("@healthItems", healthItems);
            SqliteParameter p_fireRate = new SqliteParameter("@fireRate", fireRate);
            SqliteParameter p_playerSpeed = new SqliteParameter("@playerSpeed", playerSpeed);
            SqliteParameter p_killedEnemies = new SqliteParameter("@killedEnemies", killedEnemies);
            SqliteParameter p_level =  new SqliteParameter("@level", level);
            SqliteParameter p_score =  new SqliteParameter("@score", scr);
            SqliteParameter p_saved =  new SqliteParameter("@saved", saved);

            dbcmd.Parameters.Add(p_id);
            dbcmd.Parameters.Add(p_health);
            dbcmd.Parameters.Add(p_healthItems);
            dbcmd.Parameters.Add(p_fireRate);
            dbcmd.Parameters.Add(p_playerSpeed);
            dbcmd.Parameters.Add(p_killedEnemies);
            dbcmd.Parameters.Add(p_level);
            dbcmd.Parameters.Add(p_score);
            dbcmd.Parameters.Add(p_saved);

            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            dbconn = null;
        }
    }

    // Insert player
    private void InsertPlayer(string nickName){

        using (dbconn = new SqliteConnection(conn)){

            dbconn.Open(); 
            dbcmd = dbconn.CreateCommand();
            sqlQuery = "INSERT INTO Players(NickName, Score) "+ 
                       "VALUES (\"" + nickName + "\", 0);";
            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            dbconn = null;
        }
    }

    // Update player
    private void UpdatePlayer(int idPl, int scr){

        using (dbconn = new SqliteConnection(conn)){

            dbconn.Open(); 
            dbcmd = dbconn.CreateCommand();
            sqlQuery = "UPDATE Players "+ 
                       "SET Score = @score " + 
                       "WHERE Id = @id";

            SqliteParameter p_id =  new SqliteParameter("@id", idPl);
            SqliteParameter p_score =  new SqliteParameter("@score", scr);

            dbcmd.Parameters.Add(p_id);
            dbcmd.Parameters.Add(p_score);

            dbcmd.CommandText = sqlQuery;
            dbcmd.ExecuteScalar();
            dbcmd.Dispose();
            dbcmd = null;
            dbconn.Close();
            dbconn = null;
        }
    }

    // Select top 5
    private void SelectTop5(){

        using(dbconn = new SqliteConnection(conn)){
            int count = 0;
            dbconn.Open();
            dbcmd = dbconn.CreateCommand();
            string sqlQuery = "SELECT NickName, Score " +
                              "FROM Players " +
                              "ORDER BY Score DESC LIMIT 5;";
            dbcmd.CommandText = sqlQuery;
            reader = dbcmd.ExecuteReader();

            while(reader.Read()){
                results[count] = reader.GetString(0) + " " + reader.GetInt32(1);
                count++;
                
            }

        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
        }
    }

    // 
    public void NewPlayer(string nkName){
        InsertPlayer(nkName);
    }

    // 
    public void NewGame(){
        InsertGame(idPlayer);
    }

    // Check if the player exists
    public void CheckPlayer(string nickName){

        SelectIdPlayer(nickName);

        if(idPlayer != 0){
            // El usuari ja existeix, crea nova partida
            NewGame();

        } else {
            // El usuari no existeix, el crea i crea nova partida
            NewPlayer(nickName);
            SelectIdPlayer(nickName);
            NewGame();
        }   
    }

    public void CheckSavedGame(string nickName){
        
        SelectIdPlayer(nickName);

        if(idPlayer != 0){
            // El usuari ja existeix
            SelectIdAndSavedGame(idPlayer);

            if(gameSaved == 1){
                // Cargar partida
                LoadSavedGame(nickName);

            }else {
                // Partida no guardada
                // Missatge d'error
            }
        }
    }

    // Load saved game
    public void LoadSavedGame(string nickName){

        SelectIdPlayer(nickName);
        SelectIdAndSavedGame(idPlayer);

        SelectGame(GameController.Health,
                   PlayerController.countHealth,
                   PlayerController.countFireRate,
                   PlayerController.countPlayerSpeed,
                   GameController.countKilledEnemies,
                   GameController.level,
                   GameController.score,
                   GameController.savedGame
                   );
    }

    // Update game
    public void UpdateRecord(string nickName, int scr){

        SelectIdPlayer(nickName);
        SelectScore(idPlayer);
        SelectIdAndSavedGame(idPlayer);

        if(GameController.Health <= 0){
            UpdateGameRecord(idGame, 
                             GameController.Health,
                             PlayerController.countHealth,
                             PlayerController.countFireRate,
                             PlayerController.countPlayerSpeed,
                             GameController.countKilledEnemies,
                             GameController.level,
                             scr,
                             GameController.savedGame);
        }
        SelectScore(idPlayer);
        UpdatePlayer(idPlayer, score);
    }

    public string [] Top5(){
        SelectTop5();
        return results;
    }
}

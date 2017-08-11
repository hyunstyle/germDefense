using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.IO;
using Mono.Data.Sqlite;
using System.Data;

public class DBfunction : MonoBehaviour {

    public string dbname;
    

  public void CopyDB()
    {
        string filepath = string.Empty;
        if (Application.platform == RuntimePlatform.Android)
        {
            filepath = Application.persistentDataPath +"/"+ dbname;
            if (!File.Exists(filepath))
            {
                WWW loadDB = new WWW("jar:file://" + Application.dataPath + "!/assets/" + dbname);
                loadDB.bytesDownloaded.ToString();
                while (!loadDB.isDone) { }
                File.WriteAllBytes(filepath, loadDB.bytes);
                Debug.Log("Android Platform");
            }
        }
        else
        {
            filepath = Application.dataPath + dbname;
            if (!File.Exists(filepath))
            {
                File.Copy(Application.streamingAssetsPath + dbname, filepath);
                Debug.Log("Not Android Platform");
            }
        }
    }



    public string GetConStr()
    {
        string strCon = "";
        if (Application.platform == RuntimePlatform.Android)
        {
            strCon = "URI=file:" + Application.persistentDataPath + dbname;
        }
        else
        {
            strCon = "URI=file:" + Application.dataPath + dbname;
        }

        return strCon;
    }

    public string TestConnection()
    {
        string s;
        try
        {
            SqliteConnection con = new SqliteConnection(GetConStr());
            SqliteCommand cmd = new SqliteCommand();
            cmd.Connection = con;
            con.Open();

            if (con.State == ConnectionState.Open)
            {
                s = "OK \n";
                s += con.Database + "\n";
                s += con.DataSource + "\n";
                //s += cmd.ExecuteReader().Read()+ "\n";
                s +=cmd.ExecuteReader().ToString();
            }
            else
            {
                s = "ERR";
            }

            con.Close();
        }
        catch (Exception ex)
        {
            s = ex.ToString();
        }
        return s;
    }


    public DataSet ExecuteDataSet(string query)
    {
        SqliteConnection con = new SqliteConnection(GetConStr());
        SqliteCommand cmd = new SqliteCommand();
        cmd.Connection = con;
        cmd.CommandText = query;
        con.Open();
        SqliteDataReader rd = cmd.ExecuteReader();
        DataSet ds = new DataSet();

        while (rd.HasRows)
        {
            DataTable dt = new DataTable();
            int len = rd.FieldCount;
            for (int i = 0; i < len; i++)
            {
                dt.Columns.Add(rd.GetName(i), rd.GetFieldType(i));
            }
            dt.BeginLoadData();

            var values = new object[len];
            while (rd.Read())
            {
                for (int i = 0; i < len; i++)
                {
                    values[i] = rd[i];
                }

                dt.Rows.Add(values);
            }
            dt.EndLoadData();
            ds.Tables.Add(dt);
            rd.NextResult();
        }

        rd.Close();
        rd.Dispose();
        con.Close();
        return ds;
    }


}

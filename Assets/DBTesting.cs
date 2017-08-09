using UnityEngine;

public class DBTesting : MonoBehaviour {

    public DBfunction db;

    public string message = "";

	// Use this for initialization
	void Start () {

        db.CopyDB();
        message += db.GetConStr() + "\n";
        Debug.Log(message);
        message += db.TestConnection() + "\n";
        Debug.Log(message);

	}
	
	// Update is called once per frame
	void Update () {
        
    }
}

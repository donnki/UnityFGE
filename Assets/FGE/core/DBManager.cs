using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;

public class DBManager
{
	private volatile static DBManager _instance = null;
	private static readonly object lockHelper = new object();

	public static DBManager sharedInstance()
	{
		if(_instance == null)
		{
			lock(lockHelper)
			{
				if(_instance == null)
					_instance = new DBManager();
			}
		}
		return _instance;
	}


	private SqliteConnection db;
	private new Dictionary<string, Dictionary<object, Dictionary<string, object>>> cache;
	public void initDBConnection(string connectionString){
		cache = new Dictionary<string, Dictionary<object, Dictionary<string, object>>>();
		try
		{
			db = new SqliteConnection (connectionString);
			db.Open ();
			Debug.Log ("Connected to db");

		}
		catch(Exception e)
		{
			string temp1 = e.ToString();
			Debug.Log(temp1);
		}
	}


	public void loadTable(string tableName){
		if(cache.ContainsKey(tableName)){
			cache.Remove(tableName);
		}
		Dictionary<object, Dictionary<string, object>> tableData = new Dictionary<object, Dictionary<string, object>>();
		SqliteCommand dbCommand = db.CreateCommand ();

		dbCommand.CommandText = "select * from " + tableName;

		SqliteDataReader reader = dbCommand.ExecuteReader ();
//		Debug.Log("reader.FieldCount:"+ reader.FieldCount
//			+", reader.VisibleFieldCount:" + reader.VisibleFieldCount
//			+", reader.Depth:" + reader.Depth);
		
		while (reader.Read())  
		{ 	
//			string s = "";
			Dictionary<string, object> rowData = new Dictionary<string, object>();
			for(int i=0; i< reader.FieldCount; i++){
//				s = s + (reader.GetName(i) + ": " + reader[i] + ", ");
				rowData.Add(reader.GetName(i), reader[i]);
			}
//			Debug.Log(s);
			tableData.Add(reader[0], rowData);
		} 
		cache.Add(tableName, tableData);
		reader.Close();
	}

	public void dumpCache(){
		
		foreach(var table in cache){
			string s = "表名: " + table.Key + ",共" + table.Value.Count + "条数据: \n";
			foreach(var row in table.Value){
				s = s + "id:" + row.Key + ", { ";
				foreach(var column in row.Value){
					s = s + column.Key + "= " + column.Value + ",";
				}
				s = s + "}, \n";
			}
			Debug.Log(s);
		}
	}

}


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
	private Dictionary<string, Dictionary<object, Dictionary<string, object>>> cache;
	public void initConnection(string connectionString){
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

	public void closeConnection ()

	{
		if (db != null) {
			db.Close ();
		}
		db = null;
		Debug.Log ("Disconnected from db.");

	}

	///加载整张表至缓存中并返回表数据
	public Dictionary<object, Dictionary<string, object>> loadTable(string tableName){
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
		return tableData;
	}

	private Dictionary<string, object> readRowFrom(SqliteDataReader reader){
		Dictionary<string, object> rowData = new Dictionary<string, object>();
		for(int i=0; i< reader.FieldCount; i++){
			rowData.Add(reader.GetName(i), reader[i]);
		}
		return rowData;
	}

	//根据ID从数据库指定表中查询
	public Dictionary<string, object> findByID(string tableName, object id){
		if(cache.ContainsKey(tableName) && cache[tableName].ContainsKey(id)){
			return cache[tableName][id];
		}else{
			Debug.Log("~~~dosearch");	
			SqliteCommand dbCommand = db.CreateCommand ();
			dbCommand.CommandText = "select * from " + tableName + " where id='" + id + "'";
			SqliteDataReader reader = dbCommand.ExecuteReader ();
			Dictionary<string, object> rowData = null;
			if(reader.HasRows){
				while (reader.Read()){
					rowData = readRowFrom(reader);
				}
				if(!cache.ContainsKey(tableName)){
					cache[tableName] = new Dictionary<object, Dictionary<string, object>>();
				}
				cache[tableName].Add(id, rowData);
			}else{
				Debug.Log("ID:"+id+"在数据库表"+tableName+"中不存在！");
			}
			return rowData;
		}
	}

	//打印缓存数据
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


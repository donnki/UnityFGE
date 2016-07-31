using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventManager {
	private volatile static EventManager _instance = null;
	private static readonly object lockHelper = new object();

	public static EventManager sharedInstance()
	{
		if(_instance == null)
		{
			lock(lockHelper)
			{
				if(_instance == null)
					_instance = new EventManager();
			}
		}
		return _instance;
	}

	public delegate void EventHandler(object data);
	private Dictionary<string, EventHandler> m_dicEvents;

	private EventManager(){
		m_dicEvents = new Dictionary<string, EventHandler>();
	}
	public void on(string eventName, EventHandler handler){
		if(!m_dicEvents.ContainsKey(eventName)){
			m_dicEvents.Add(eventName, handler);
		}else{
			m_dicEvents[eventName] += handler;
		}
	}

	public void fire(string eventName){
		fire(eventName, null);
	}

	public void fire(string eventName, object data){
		if(m_dicEvents.ContainsKey(eventName)){
			if(m_dicEvents[eventName] != null){
				m_dicEvents[eventName](data);
				return;
			}
		}
		Debug.LogWarning("触发失败, 事件"+eventName+"未注册！忽略");


	}

	public void clear(string eventName){
		clear(eventName, null);
	}

	public void clear(string eventName, EventHandler handler){
		if(handler == null){
			if(m_dicEvents.ContainsKey(eventName)){
				m_dicEvents.Remove(eventName);
			}else{
				Debug.LogWarning("清除失败, 事件"+eventName+"未注册！忽略");
			}
		}else{
			if(m_dicEvents.ContainsKey(eventName)){
				m_dicEvents[eventName] -= handler;	
			}else{
				Debug.LogWarning("清除失败, 事件"+eventName+"未注册！忽略");
			}
		}
	}
}

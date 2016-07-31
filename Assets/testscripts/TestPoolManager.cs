using UnityEngine;
using System.Collections;

public class TestPoolManager : MonoBehaviour {
	public Transform cubePrefab;

	SpawnPool cubePool;

	///
	//可以直接在编辑器里操作SpawnPool，为一个对象绑上SpawnPool，直接在编辑器里设置好相
	//关的属性，就可以直接调用pool.Spawn("Cube Prefab")来生成对象了。
	//也可以通过纯代码来使用PoolManager， 以下通过程序代码操作：
	void Start () {
		//创建一个名为Cube的对象缓冲区
		cubePool = PoolManager.Pools.Create("Cube");

		cubePool.group.name = "CreatorGroup";//默认名称为CreatorPool

		//指定这个组的父节点（此时的父节点被指定为当前脚本悬挂的对象，即Creator对象）
		cubePool.group.parent = this.transform;
		//那么从缓冲池获取对象，对象的层次关系为：
		//  Creator
		//  -------CreatorGroup
		//  ---------------------Sphere（Clone）001
		//  ---------------------Sphere（Clone）002
		//  ---------------------Sphere（Clone）003
		//指定这个组（即CreatorGroup）的位置与旋转
		cubePool.group.localPosition = new Vector3(1.5f, 0, 0);
		cubePool.group.localRotation = Quaternion.identity;

		//创建预设
		PrefabPool prefabPool = new PrefabPool(cubePrefab);
		//缓存池这个Prefab的最大保存数量
		prefabPool.preloadAmount = 1;
		//是否开启缓存池智能自动清理模式
		prefabPool.cullDespawned = true;
		//缓存池自动清理，但是始终保留几个对象不清理
		prefabPool.cullAbove = 3;
		//每过多久执行一遍自动清理(销毁)，单位是秒
		prefabPool.cullDelay = 6;
		//每次自动清理2个游戏对象
		prefabPool.cullMaxPerPass = 2;
		//是否开启实例的限制功能
		prefabPool.limitInstances = true;
		//限制缓存池里最大的Prefab的数量，它和上面的preloadAmount是有冲突的，如果同时开启则以limitAmout为准
		prefabPool.limitAmount = 10;
		//如果我们限制了缓存池里面只能有10个Prefab，如果不勾选它，那么你拿第11个的时候就会返回null。如果勾选它在取第11个的时候他会返回给你前10个里最不常用的那个
		prefabPool.limitFIFO = true;
		//加入此预设
		cubePool.CreatePrefabPool(prefabPool);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	int count = 0;
	void OnGUI(){
		if(GUI.Button(new Rect(100,100,200,50), "触发事件")){
			Transform ins = cubePool.Spawn("Cube Prefab");
			ins.localPosition = new Vector3((10 - count) * 2, 0, 0);
			count = count + 1;
		}
			
	}
}

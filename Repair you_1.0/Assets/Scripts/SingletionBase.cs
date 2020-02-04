using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

///单例模版
///</summary>
public abstract class SingletionBase<T> : MonoBehaviour where T : SingletionBase<T>
{
    private static T instance;//注意：抽象函数里可以包含静态变量，所以说静态变量不一定都在静态类中

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = CreateT("Singletion of " + typeof(T).ToString());
                instance.Init();
            }
            return instance;
        }
    }
    public abstract void Init();
    private void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            instance.Init();
        }
    }
    private static T CreateT(string name)
    {
        var t = GameObject.FindObjectOfType(typeof(T));
        //按类型在场景中查找对象
        if (t == null)
        {
            return new GameObject(name, typeof(T)).GetComponent<T>();
            //创建一个对象并添加T脚本
        }
        return t as T;
    }
    //当应用程序 退出做 清理工作！单例模式对象=null
    private void OnApplicationQuit()
    {
        instance = null;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
public class WindowManager : MonoBehaviour
{
    public static List<Window> windows = new List<Window>(); 

    public static string startID = "LOGIN";
    async void Start()
    {
        var windowClasses = Assembly.GetAssembly(typeof(Window)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Window)));

        Debug.Log(windowClasses.Count());


        foreach(var c in windowClasses)
        {
            var objects = FindObjectsOfType(c);
            foreach(var o in objects)
            {
                Debug.Log(o);
                windows.Add((Window)o);
            }
        }
        foreach(Window w in windows)
            await w.Hide();

        await windows.First(w => w.name == startID).InitAndOpen();
                
    }

   public static async Task Navigate(string id)
   {
        windows.Where(w => w.open && w.name != id).Select(async (w) => {await w.Close(); });
        await windows.First( w => w.name == id).InitAndOpen();
   }

}


[System.Serializable]
public abstract class Window : MonoBehaviour
{
    public abstract string GetID();

    public bool open;

    public virtual async Task Open() 
    {
        gameObject.SetActive(true);
        open = true;
        await Task.CompletedTask;
    }
    public virtual async Task Close() 
    {
        gameObject.SetActive(false);
        open = false;
        await Task.CompletedTask;
    }    
    public virtual Task Init() {return Task.CompletedTask;}
    public virtual async Task Hide() 
    {
        await Close();
    }

    public async virtual Task InitAndOpen()
    {
        await Init();
        await Open();
    }
}
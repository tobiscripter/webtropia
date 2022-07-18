using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
public class WindowManager : MonoBehaviour
{
    public static Dictionary<string,Window> windows = new Dictionary<string, Window>(); 

    private static int loading = 0;
    public static string startID = "LOGIN";
    public static string errorWindowID = "ERROR";
    public static string loadingWindowID = "LOADING";

    void Awake()
    {
        UserInformation.Load();
        //UserInformation.Show();

        var windowClasses = Assembly.GetAssembly(typeof(Window)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Window)));

        foreach(var c in windowClasses)
        {
            var objects = FindObjectsOfType(c);
            foreach(var o in objects)
            {
                Window w = (Window) o;
                windows[w.GetID()] = w;
            }
        }
    }
    async void Start()
    {
        
        foreach(Window w in windows.Values.Where(w => w.open))
            await w.Hide();

        await windows[startID].InitAndOpen();                
    }

    public static async Task StartLoading()
    {
        loading++;
        if(loading == 1)
            await windows[loadingWindowID].Open();
    }

    public static async Task StopLoading()
    {
        loading--;
        if(loading == 0)
            await windows[loadingWindowID].Close();
    }
   public static async Task Navigate(string id)
   {

        windows.Values.Where(w => w.open && w.GetID() != id).ToList().ForEach(async w => await w.Hide());

        await windows[id].InitAndOpen();
   }

   public static async Task Error(string error)
   {
        await windows[errorWindowID].InitAndOpen(error);
   }

}


[System.Serializable]
public abstract class Window : MonoBehaviour
{
    public abstract string GetID();

    public bool open, init = false;

    public virtual async Task Open() 
    {
        Animation anim = gameObject.GetComponent<Animation>();
        if(anim.isActiveAndEnabled)
        {
            anim.Play("Open-LR");
            while(anim.isPlaying) await Task.Yield();
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        open = true;
    }
    public virtual async Task Close() 
    {
        Animation anim = gameObject.GetComponent<Animation>();
        if(anim.isActiveAndEnabled)
        {
            anim.Play("Close-LR");
            while(anim.isPlaying) await Task.Yield();
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        open = false;
    }    
    public virtual Task Init() {return Task.CompletedTask;}
    public virtual Task Init(string s) {return Task.CompletedTask;}
    public virtual async Task Hide() 
    {
        await Close();
    }

    public async virtual Task InitAndOpen()
    {
        await Init();
        await Open();
    }

    public async virtual Task InitAndOpen(string s)
    {
        await Init(s);
        await Open();
    }
}
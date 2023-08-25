using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEditor.PackageManager;

public class QuickStartProject : UnityEditor.EditorWindow
{
    private string versionNumber = "1.1";
    private List<Pack> packs;
    static AddAndRemoveRequest Request;

    public QuickStartProject(){
        packs = new List<Pack>(){

            new Pack("Generic", new List<PackageModule>(){
                new PackageModule("com.unity.addressables", "Addressables"),
                new PackageModule("com.unity.ai.navigation", "AI Navigation"),
                new PackageModule("https://github.com/febucci/unitypackage-custom-hierarchy.git", "Febucci Custom Hierarchy"),
                new PackageModule("com.unity.ide.visualstudio", "Visual Studio Editor"),
                new PackageModule("com.unity.feature.gameplay-storytelling", "Gameplay and Storytelling"),
                new PackageModule("com.unity.feature.mobile", "Mobile"),
                new PackageModule("com.unity.inputsystem", "New Input System"),
                new PackageModule("com.unity.postprocessing", "Post Processing"),
                new PackageModule("com.unity.purchasing", "In App Purchasing"),
                new PackageModule("com.unity.services.economy", "Unity Economy"),

            }),

            new Pack("2D Basics", new List<PackageModule>(){
                new PackageModule("com.unity.2d.animation", "2D Animation"),
                new PackageModule("com.unity.2d.pixel-perfect", "2D Pixel Perfect"),
                new PackageModule("com.unity.2d.psdimporter", "2D PSD Importer"),
                new PackageModule("com.unity.2d.sprite", "2D Sprite"),
                new PackageModule("com.unity.2d.spriteshape", "2D SpriteShape"),
                new PackageModule("com.unity.2d.tilemap", "2D Tilemap Editor"),
                new PackageModule("com.unity.2d.tilemap.extras", "2D Tilemap Extras"),
            }),

            new Pack("Custom", new List<PackageModule>(){
                new PackageModule("https://github.com/JohnyLoots/2DUtility.git", "Custom 2D Utility")
            }),
        };
    }


    [MenuItem("Tools/Quick start project")]
    static void Initilize()
    {
        //Window
        QuickStartProject window = (QuickStartProject)EditorWindow.GetWindow(typeof(QuickStartProject), true, "My Empty Window");
        var popup_Width = 180;
        var popup_Height = 480;
        window.position = new Rect(Screen.width, Screen.height - (popup_Height / 2), popup_Width, popup_Height);
        window.Show();        
    }


    void OnGUI()
    {
        for (int i = 0; i < packs.Count; i++)
        {
            var module = packs[i];
            module.importState = EditorGUILayout.BeginToggleGroup(module.pack_Name, module.importState);

            for (int x = 0; x < module.packageModules.Count; x++)
            {
                module.packageModules[x].importState = EditorGUILayout.Toggle(module.packageModules[x].package_Name, module.packageModules[x].importState);
            }
            
            EditorGUILayout.EndToggleGroup();
        }

        var buttonHeight = 25;

        if (GUI.Button(new Rect(0, position.height - (buttonHeight * 1), position.width, buttonHeight), "Close Window!")){
            this.Close();
        }

        if (GUI.Button(new Rect(0, position.height - (buttonHeight * 2), position.width, buttonHeight), "Import All")){
            importAll();
        }

        if (GUI.Button(new Rect(0, position.height - (buttonHeight * 3), position.width, buttonHeight), "Clear All")){
            this.Close();
        }
    }


    void importAll(){
        List<string> packages = new List<string>();
        for (int i = 0; i < packs.Count; i++){
            var module = packs[i];
            for (int x = 0; x < module.packageModules.Count; x++)
            {
                packages.Add(module.packageModules[x].packageURL);
                
            }
        }

        Request = Client.AddAndRemove(packages.ToArray());
    }
}

public class Pack{
    public string pack_Name;
    public List<PackageModule> packageModules;
    public bool importState = false;
    public Pack(string package_Name, List<PackageModule> modules){
        this.pack_Name = package_Name;
        this.packageModules = modules;
    }
}

public class PackageModule{

    public PackageModule(string URL, string name){
        this.packageURL = URL;
        this.package_Name = name;
    }
    public bool importState = false;
     public string packageURL;
    public string package_Name;
}





    

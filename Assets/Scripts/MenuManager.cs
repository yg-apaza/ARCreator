using System.Collections;
using System.Collections.Generic;
using System.IO;
using Citesoft.ARAuthoringTool.Core.Template;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    string path;
    string framework;
    public ARAppStructure arAppStructure;
	public ARAppList arAppList = null;

	public InputField filenameTxt;
    public Text messageLbl;
    public Text validationLbl;
    public Button nextBtn;
	public Dropdown listApps;

	public Text textTest;

    void Start()
    {
        framework = "unnamed";
		listApps.ClearOptions();
	}

    public void OpenExplorer()
    {
        //string home = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

//#if UNITY_ANDROID && !UNITY_EDITOR
//		    home="file:///mnt/sdcard/Documents/";
//#endif
	
		string path = "http://aqueous-mountain-38515.herokuapp.com/arapp/summary";
//        path = Path.Combine(home, filenameTxt.text);
//        validationLbl.text = "Path: " + path;
        StartCoroutine(readFile(path));
    }

    IEnumerator readFile(string path)
    {
        WWW data = new WWW(path);
        yield return data;
		
		if (string.IsNullOrEmpty(data.error))
		{
			arAppList = new ARAppList();
			string content = data.text;
			JsonUtility.FromJsonOverwrite(content, arAppList);
			//framework = arAppStructure.framework;
			// TODO: Check validity of JSON structure

			List<string> titles = new List<string>();

			foreach (ARAppSummary a in arAppList.arApps)
				titles.Add(a.title);

			listApps.AddOptions( titles );
			//messageLbl.text = "Opening project " + "\"" + arAppStructure.title + "\" ...";
			// TODO: Check validity of interfaces
			// TODO: Check validity of URL markers and resources
			// TODO: Send the valid JSON object to the next Scene

			// TODO: If everything is OK, enable Next button
			nextBtn.interactable = true;
		}
        else
        {
			textTest.text = "Error al leer";
			// TODO: Handle if file not exits
		}
    }

	IEnumerator ExtractSelection(string path)
	{
		WWW data = new WWW(path);
		yield return data;
		if (string.IsNullOrEmpty(data.error))
		{
			string content = data.text;
			JsonUtility.FromJsonOverwrite(content, arAppStructure);
			framework = arAppStructure.framework;
			messageLbl.text = "Opening project " + "\"" + arAppStructure.title + "\" ...";
			//nextBtn.interactable = true;
		}
		else {
			Debug.Log("Error not data !!!!!!");
		}
	}

	public void NextScene()
    {
		//if (arAppList == null) return;

		string pathARApp = Path.Combine("http://aqueous-mountain-38515.herokuapp.com/arapp/",arAppList.arApps[listApps.value]._id);
		// TODO: Use dictionary to store framework id and its associated scene
		StartCoroutine( ExtractSelection(pathARApp) );

		if (framework.Equals("artoolkit"))
        {
            SceneManager.LoadScene("ARToolKitScene");
        }
        else if (framework.Equals("vuforia"))
        {
            Debug.Log("Coming soon !!");
        }
        else
        {
            Debug.Log("Unsupported AR framework");
        }
    }
}

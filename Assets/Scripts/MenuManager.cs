using System.Collections;
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
    public InputField filenameTxt;
    public Text messageLbl;
    public Text validationLbl;
    public Button nextBtn;

    void Start()
    {
        framework = "unnamed";
        filenameTxt.text = "examplePoly.json";
    }

    public void OpenExplorer()
    {
        string home = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);

#if UNITY_ANDROID && !UNITY_EDITOR
		    home="file:///mnt/sdcard/Documents/";
#endif

        path = Path.Combine(home, filenameTxt.text);
        validationLbl.text = "Path: " + path;

        StartCoroutine(readFile(path));
    }

    IEnumerator readFile(string path)
    {
        WWW data = new WWW(path);
        yield return data;

        if (string.IsNullOrEmpty(data.error))
        {
            string content = data.text;

            JsonUtility.FromJsonOverwrite(content, arAppStructure);
            framework = arAppStructure.framework;

            // TODO: Check validity of JSON structure

            messageLbl.text = "Opening project " + "\"" + arAppStructure.title + "\" ...";
            // TODO: Check validity of interfaces
            // TODO: Check validity of URL markers and resources
            // TODO: Send the valid JSON object to the next Scene

            // TODO: If everything is OK, enable Next button
            nextBtn.interactable = true;
        }
        else
        {
            // TODO: Handle if file not exits
        }
    }

    public void NextScene()
    {
        // TODO: Use dictionary to store framework id and its associated scene
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

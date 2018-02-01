using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    string path;
    public Text filenameTxt;
    public Text messageTxt;
    public Button nextBtn;


    public void OpenExplorer()
    {
        path = Path.GetFileName(EditorUtility.OpenFilePanel("Choose a JSON file", "", "json"));
        if (path != null)
        {

            StreamReader reader = new StreamReader(path);
            string content = reader.ReadToEnd();
            reader.Close();
            ARAppStructure arAppStructure = JsonUtility.FromJson<ARAppStructure>(content);

            // TODO: Check validity of JSON structure
            filenameTxt.text = "Opening project " + "\"" + arAppStructure.title + "\" ...";
            // TODO: Check validity of interfaces
            // TODO: Check validity of URL markers and resources
            // TODO: Send the valid JSON object to the next Scene

            // TODO: If everything is OK, enable Next button
            nextBtn.interactable = true;
        }
    }

    public void NextScene(string sceneName)
    {
        SceneManager.LoadScene(2);
        Debug.Log("Done");
    }
}

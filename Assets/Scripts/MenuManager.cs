using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    string path;
    public ARAppStructure arAppStructure;
    public InputField filenameTxt;
    public Text messageLbl;
    public Text validationLbl;
    public Button nextBtn;


    public void OpenExplorer()
    {
        bool soyNuevo = false;
        path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), filenameTxt.text);
        Debug.Log("Path: " + path);

        // TODO: Check if file exists
        StreamReader reader = new StreamReader(path);
        string content = reader.ReadToEnd();
        reader.Close();

        if (arAppStructure.title.Equals(""))
            soyNuevo = true;

        JsonUtility.FromJsonOverwrite(content, arAppStructure);

        // TODO: Check validity of JSON structure
        Debug.Log("Titulo: <" + arAppStructure.title + ">");
        if(soyNuevo)
            messageLbl.text = "Soy nuevo: Opening project " + "\"" + arAppStructure.title + "\" ...";
        else
            messageLbl.text = "Ya tenia data anterior: Opening project " + "\"" + arAppStructure.title + "\" ...";
        // TODO: Check validity of interfaces
        // TODO: Check validity of URL markers and resources
        // TODO: Send the valid JSON object to the next Scene

        // TODO: If everything is OK, enable Next button
        nextBtn.interactable = true;
    }

    public void NextScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log("Done");
    }
}

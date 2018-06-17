using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameEndMenu : MonoBehaviour {
    
    public Button ReplayButton;

    public Button MenuButton;

    void Start()
    {
        Button btn = ReplayButton.GetComponent<Button>();
        //Calls the TaskOnClick method when you click the Button
        btn.onClick.AddListener(TaskOnClick);

        Button btn2 = MenuButton.GetComponent<Button>();
        //Calls the TaskOnClick method when you click the Button
        btn2.onClick.AddListener(TaskOnClick2);
    }

    void TaskOnClick()
    {
        //to game
        SceneManager.LoadScene("SampleScene");
    }

    void TaskOnClick2()
    {
        //to menu
        SceneManager.LoadScene("ContrinueScreen");
    }
}

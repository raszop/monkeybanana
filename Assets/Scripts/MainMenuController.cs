using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private Text highScoreText;
    [SerializeField]
    private Toggle autoFireToggle;
    [SerializeField]
    private GameObject cutsceneObject;

    private int gameplaySceneIndex = 1;

    // Start is called before the first frame update
    void Start()
    {
        highScoreText.text = "High Score:\n" + Utilities.GetHighScore().ToString();

        autoFireToggle.isOn = Utilities.CheckAutoFireSetting();

        //Add listener for when the state of the Toggle changes, to take action
        autoFireToggle.onValueChanged.AddListener(delegate
        {
            ToggleAutoFire();
        });
    }

    public void StartNewGame()
    {
        cutsceneObject.SetActive(true);
        Invoke(nameof(LoadGameplayScene), 3.0F);
    }

    private void LoadGameplayScene()
    {
        SceneManager.LoadScene(gameplaySceneIndex);
    }

    public void ToggleAutoFire()
    {
        Utilities.ChangeAutoFireSetting(autoFireToggle.isOn);
    }
}

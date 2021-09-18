using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MenuAndSettings : MonoBehaviour
{
    public GameObject menu, hud,end, playerDied;
    public GameObject options;
    public GameObject controls;
    public GameObject play;
    public GameObject resume;
    public TextMeshProUGUI mouseSensitifity;
    public Image  cursor;
    public bool blockESC;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI ammo;
    public static MenuAndSettings instance;
    

    //settings
    public float mouseSpeedModifier;

    private void Awake() {
        if(instance != null){
            Destroy(gameObject);
        }
        else
        {
            instance= this;
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start() {
        options.SetActive(false);
        controls.SetActive(false);
        play.SetActive(true);
        resume.SetActive(false);
        if(SceneManager.GetActiveScene().buildIndex==0){
            menu.SetActive(true);
            hud.SetActive(false);
            end.SetActive(false);
        }
    }
    public void PlayGame(){
        blockESC=false;
        Cursor.lockState = CursorLockMode.Locked;
        transform.Find("Background").gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    public void QuitGame(){
        Debug.Log("Quit");
        Application.Quit();
    }

    public void OptionsSelected(){
        Debug.Log("Selected Options");
        controls.SetActive(false);
        options.SetActive(true);
        mouseSensitifity.text = mouseSpeedModifier.ToString("F3");
    }

    public void ControlsSelected(){
        Debug.Log("Selected controls");
        controls.SetActive(true);
        options.SetActive(false);
    }

    //settings
    public void SetMouseSpeed(float speed){
        Debug.Log(speed);
        mouseSpeedModifier = speed;
        mouseSensitifity.text = speed.ToString("F3");
    }


    public void TryToSetMouseSpeed(){
        GameMaster.SetMouseSpeed(mouseSpeedModifier);
    }

    public void Resume(){
        GameMaster.ESC();
    }

    public void DeactivateCursor(){
        cursor.enabled = false;
        Debug.Log("Cursor deactivated");
    }

    public void ActivateCursor(){
        cursor.enabled = true;
    }

    public void EndGame(string whatEnding){
        blockESC=true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        if(whatEnding =="Player"){
            menu.SetActive(false);
            hud.SetActive(false);
            end.SetActive(false);
            playerDied.SetActive(true);
        }else
        {
            menu.SetActive(false);
            hud.SetActive(false);
            end.SetActive(true);
            playerDied.SetActive(false);
        }
    }

    public void BackToMainMenu(){
        transform.Find("Background").gameObject.SetActive(true);
        play.SetActive(true);
        resume.SetActive(false);
        menu.SetActive(true);
        hud.SetActive(false);
        end.SetActive(false);
        playerDied.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }

}

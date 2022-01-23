using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainmenuManager : MonoBehaviour
{
   public GameObject CreditsMenu;
   public GameObject MainMenu;
   public void StartGame()
   {
      SceneManager.LoadScene(1);
   }

   public void OpenCredits()
   {
      CreditsMenu.SetActive(true);
      MainMenu.SetActive(false);
   }

   public void OpenMenu()
   {
      CreditsMenu.SetActive(false);
      MainMenu.SetActive(true);
   }
}

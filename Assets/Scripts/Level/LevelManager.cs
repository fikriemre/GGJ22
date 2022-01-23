using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public CinemachineVirtualCamera TurnCam;
    public Sublevel[] subLevels;
    [SerializeField] private Transform modelRoot;
    [SerializeField] private Transform bookModelRoot;
    private bool _levelControl = false;
    private bool _bookTurning = false;
    private bool bookCanTurn = false;
    private void Start()
    {
        subLevels[0].SetLevelSelection(false);
        subLevels[1].SetLevelSelection(false);
        subLevels[0].gameObject.SetActive(false);
        subLevels[1].gameObject.SetActive(false);
        bookModelRoot.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TurnBook();
        }
    }

    [Button]
    public void StartLevel()
    {
        subLevels[0].SetLevelSelection(true);
        subLevels[1].SetLevelSelection(false);
        subLevels[1].gameObject.SetActive(false);
        bookModelRoot.gameObject.SetActive(false);
        bookCanTurn = true;
        TurnBook();
    }

    [Button]
    public void TurnBook()
    {
        if(!bookCanTurn)
            return;
        if (_bookTurning)
            return;

        StartCoroutine(TurnAnim());
    }

    IEnumerator TurnAnim()
    {
        _bookTurning = true;
        var wait = new WaitForEndOfFrame();
        TurnCam.gameObject.SetActive(true);
        Sublevel oldSelectedLevel = null;
        Sublevel newSelectedLevel = null;
        if (subLevels[0].SELECTED)
        {
            oldSelectedLevel = subLevels[0];
            newSelectedLevel = subLevels[1];
        }
        else
        {
            oldSelectedLevel = subLevels[1];
            newSelectedLevel = subLevels[0];
        }

        subLevels[0].SetLevelSelection(false);
        subLevels[1].SetLevelSelection(false);
        bookModelRoot.gameObject.SetActive(true);
        oldSelectedLevel.transform.SetParent(transform);
        newSelectedLevel.transform.SetParent(transform);
        bookModelRoot.transform.SetParent(transform);


        Vector3 bookmodelPos = oldSelectedLevel.player.transform.position;
        bookmodelPos.z = 0;
        bookModelRoot.transform.position = bookmodelPos;
        modelRoot.position = bookmodelPos;

        Vector3 newModelPos = newSelectedLevel.transform.position + bookmodelPos - newSelectedLevel.player.transform.position;
        newModelPos.z = newSelectedLevel.transform.position.z;
        newSelectedLevel.transform.position = newModelPos;

        TurnCam.transform.position = new Vector3(bookmodelPos.x, bookmodelPos.y, TurnCam.transform.position.z);


        oldSelectedLevel.transform.SetParent(modelRoot);
        newSelectedLevel.transform.SetParent(modelRoot);
        bookModelRoot.transform.SetParent(modelRoot);

        yield return new WaitForSeconds(1);
        float startRotation = modelRoot.rotation.eulerAngles.y;
        float timer = 0;
        bool gameObjectEnabled = false;
        while (true)
        {
            timer += Time.deltaTime;
            modelRoot.rotation = Quaternion.Euler(0, startRotation + timer * 180, 0);
            yield return wait;
            if (timer >= 0.5f)
            {
                if (!gameObjectEnabled)
                {
                    gameObjectEnabled = true;
                    oldSelectedLevel.gameObject.SetActive(false);
                    newSelectedLevel.gameObject.SetActive(true);
                }
            }

            if (timer >= 1)
                break;
        }

        TurnCam.gameObject.SetActive(false);
        subLevels[0].SetLevelSelection(oldSelectedLevel != subLevels[0]);
        subLevels[1].SetLevelSelection(oldSelectedLevel != subLevels[1]);
        yield return new WaitForSeconds(1);
        modelRoot.rotation = Quaternion.Euler(0, startRotation + 180, 0);
        _bookTurning = false;
        bookModelRoot.gameObject.SetActive(false);
    }
}
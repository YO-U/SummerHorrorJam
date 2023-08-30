using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCrimers : MonoBehaviour
{
    private OpenCloseObject window;
    private HumanWalkToWindow monsterChecker;
    private CameraMove camera;
    
    //В лесу
    public GameObject _monsterInForest;
    private bool spawnInForestCheck=false;
    
    
    //перед окном
    public GameObject _monsterInWindow;
    private bool spawnInWindowCheck=false;
    
    //перед окном телефона
    public GameObject _monsterInWindowPhone;
    private bool spawnInWindowPhoneCheck=false;

    private void Start()
    {
        camera = FindObjectOfType<CameraMove>();
        window = FindObjectOfType<OpenCloseObject>();
        monsterChecker = FindObjectOfType<HumanWalkToWindow>();
        _monsterInForest.SetActive(false);
        _monsterInWindow.SetActive(false);
        _monsterInWindowPhone.SetActive(false);
    }

    private void Update()
    {
        if (monsterChecker.didImposterGotIn || monsterChecker.didImposterNahuiPoshel)
        { 
            if (monsterChecker.humansSinceTheImposter == 3 && spawnInForestCheck ==false)
            {
                StartCoroutine(SpawnMonsterInForest());
            }

            if (window.windowOpened == false && monsterChecker.humansSinceTheImposter == 4 &&spawnInWindowCheck==false && camera.mid)
            {
                StartCoroutine(SpawnMonsterInWindow());
            }

            if (monsterChecker.humansSinceTheImposter == 1 && camera.right && spawnInWindowPhoneCheck==false)
            {
                StartCoroutine(SpawnMonsterInWindowPhone());
            }
        }
    }

    public IEnumerator SpawnMonsterInForest()
    {
        _monsterInForest.SetActive(true);
        yield return new WaitForSeconds(7);
        _monsterInForest.SetActive(false);
        spawnInForestCheck = true;
    }
    
    public IEnumerator SpawnMonsterInWindow()
    {
        _monsterInWindow.SetActive(true);
        yield return new WaitForSeconds(1);
        _monsterInWindow.SetActive(false);
        spawnInWindowCheck = true;
    }
    
    public IEnumerator SpawnMonsterInWindowPhone()
    {
        _monsterInWindowPhone.SetActive(true);
        yield return new WaitForSeconds(1);
        _monsterInWindowPhone.transform.position = _monsterInWindowPhone.transform.position + new Vector3(0.0f, 0.0f, 0.1f)*Time.deltaTime;
        yield return new WaitForSeconds(1);
        _monsterInWindowPhone.SetActive(false);
        spawnInWindowPhoneCheck = true;
    }
}

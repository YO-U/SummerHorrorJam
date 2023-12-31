using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCrimers : MonoBehaviour
{
    private OpenCloseObject window;
    private HumanWalkToWindow monsterChecker;
    private CameraMove camera;
    [SerializeField] private AudioSource screamer;
    
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
        if (monsterChecker.currentHuman > 2)
        { 
            if (monsterChecker.currentHuman >= 3 && camera.mid && spawnInForestCheck == false)
            {
                StartCoroutine(SpawnMonsterInForest());
            }

            if (monsterChecker.currentHuman >= 4 &&spawnInWindowCheck == false && camera.mid && !monsterChecker.hCreatedCh)
            {
                StartCoroutine(SpawnMonsterInWindow());
            }

            if (monsterChecker.currentHuman >= 5 && camera.right && spawnInWindowPhoneCheck==false)
            {
                StartCoroutine(SpawnMonsterInWindowPhone());
            }
        }
    }

    public IEnumerator SpawnMonsterInForest()
    {
        _monsterInForest.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		screamer.pitch = 0.8f;
		screamer.volume = 0.2f;
        screamer.Play();
        yield return new WaitForSeconds(7);
        _monsterInForest.SetActive(false);
        spawnInForestCheck = true;
    }
    
    public IEnumerator SpawnMonsterInWindow()
    {
        _monsterInWindow.SetActive(true);
		yield return new WaitForSeconds(0.5f);
		screamer.pitch = 1.2f;
		screamer.volume = 0.4f;
		screamer.Play();
		yield return new WaitForSeconds(1);
        _monsterInWindow.SetActive(false);
        spawnInWindowCheck = true;
    }
    
    public IEnumerator SpawnMonsterInWindowPhone()
    {
		yield return new WaitForSeconds(0.5f);
        screamer.pitch = 1f;
		screamer.volume = 0.4f;
		screamer.Play();
		_monsterInWindowPhone.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        _monsterInWindowPhone.transform.position = _monsterInWindowPhone.transform.position + new Vector3(0.0f, 0.0f, 0.1f)*Time.deltaTime;
        yield return new WaitForSeconds(1);
        _monsterInWindowPhone.SetActive(false);
        spawnInWindowPhoneCheck = true;
    }
}

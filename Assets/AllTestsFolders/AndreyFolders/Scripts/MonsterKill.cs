using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class MonsterKill : MonoBehaviour
{
	private int currentPointIndex = 0;
	private bool MonsterSpawned = false;
	private bool IsdoorBroken = false;
	private bool HasMonsterSequenceStarted = false;
	[SerializeField] private OpenCloseObject openClose;
    [SerializeField] private HumanWalkToWindow human;
    [SerializeField] private GameObject monsterStock;
	[SerializeField] private GameObject monster;
	[SerializeField] private GameObject door;
	[SerializeField] private AudioSource doorHandle;
	[SerializeField] private AudioSource doorBash;
	[SerializeField] private AudioSource doorBroken;
	[SerializeField] private AudioSource Stinger;
	[SerializeField] private AudioSource ghostCHoir;
	[SerializeField] private CameraMove cameraMove;
	[SerializeField] EndingController endingController;
	public Transform[] pointsArray = new Transform[3];
	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator SpawnMonsterRemoveHuman()
    {
		if (!MonsterSpawned && !HasMonsterSequenceStarted)
		{
			DOTweenModuleAudio.DOFade(cameraMove.tvAudioSource, 0.02f, 3);
			ghostCHoir.Play();
			HasMonsterSequenceStarted = true;
			MonsterSpawned = true;
			if(human.human != null)
			{
				human.human.transform.position = new Vector3(4, 4, 4);
			}
			human.hCreatedCh = false;
			yield return new WaitForSeconds(10);
			monster = Instantiate(monsterStock, pointsArray[0].position, Quaternion.identity) as GameObject;
			monster.GetComponent<AudioSource>().Play();
			monster.GetComponent<Animator>().SetBool("IsRunning", true);
			StartCoroutine(MonsterMove());
			yield return new WaitForSeconds(5);
			monster.transform.DOLocalRotate(new Vector3(0, 180, 0), 0.1f);
			doorHandle.Play();
			yield return new WaitForSeconds(4);
			doorBash.Play();
			yield return new WaitForSeconds(2);
			doorBash.Play();
			doorBroken.Play();
			door.transform.DOLocalMove(new Vector3(0.536f, -0.005f, -1.328f), 0.1f);
			door.transform.DOLocalRotate(new Vector3(-90, 0, 13), 0.1f);
			openClose.inputEvailable = false;
			IsdoorBroken = true;
			cameraMove.cameraPos.transform.DORotate(new Vector3(0, 0 ,0), 1f);
			yield return new WaitForSeconds(5);
			monster.GetComponent<Animator>().SetBool("IsRunning", true);
			Stinger.Play();
			yield return new WaitForSeconds(1);
			ghostCHoir.Stop();
			endingController.blackScreen.GetComponent<UnityEngine.UI.Image>().color = Color.black;
			endingController.IsEndingStarting = true;
			Destroy(monster);
			MonsterSpawned = false;
		}
	}

	private IEnumerator MonsterMove()
	{
		for (int i = currentPointIndex; i < pointsArray.Length; i++)
		{
			monster.transform.LookAt(pointsArray[i].position);
			while (Vector3.Distance(monster.transform.position, pointsArray[i].position) > 0.01f)
			{
				//human.transform.position = Vector3.MoveTowards(human.transform.position, pointsArray[i].position, humanSpeed * Time.deltaTime);
				yield return null;
			}
			currentPointIndex = i + 1; // Переходим к следующей точке
		}
		currentPointIndex = 0;
		monster.GetComponent<AudioSource>().Stop();
		monster.GetComponent<Animator>().SetBool("IsRunning", false);
		monster.transform.LookAt(human.cameraPosition);
	}
}

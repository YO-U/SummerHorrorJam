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
		LookAtMonster();
    }

    public IEnumerator SpawnMonsterRemoveHuman()
    {
		if (!MonsterSpawned && !HasMonsterSequenceStarted)
		{
			ghostCHoir.Play();
			HasMonsterSequenceStarted = true;
			MonsterSpawned = true;
			human.human.transform.position = new Vector3(4, 4, 4);
			human.hCreatedCh = false;
			yield return new WaitForSeconds(10);
			monster = Instantiate(monsterStock, pointsArray[0].position, Quaternion.identity) as GameObject;
			monster.GetComponent<AudioSource>().Play();
			monster.GetComponent<Animator>().SetBool("IsRunning", true);
			StartCoroutine(MonsterMove());
			yield return new WaitForSeconds(5);
			doorHandle.Play();
			yield return new WaitForSeconds(4);
			doorBash.Play();
			yield return new WaitForSeconds(2);
			doorBash.Play();
			doorBroken.Play();
			door.transform.localPosition = new Vector3(0.536f, -0.005f, -1.328f);
			door.transform.localRotation = new Quaternion(-90, 0, 13, 1);
			openClose.inputEvailable = false;
			IsdoorBroken = true;
			yield return new WaitForSeconds(5);
			monster.GetComponent<Animator>().SetBool("IsRunning", true);
			Stinger.Play();
			cameraMove.cameraPos.LookAt(monster.transform);
			yield return new WaitForSeconds(1);
			endingController.blackScreen.GetComponent<UnityEngine.UI.Image>().color = Color.black;
			endingController.IsEndingStarting = true;
			Destroy(monster);
			MonsterSpawned = false;
		}
	}

	private void LookAtMonster()
	{
		if (MonsterSpawned && doorBroken)
		{
			cameraMove.cameraPos.LookAt(monster.transform);
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

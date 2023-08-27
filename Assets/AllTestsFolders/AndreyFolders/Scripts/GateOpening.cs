using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class GateOpening : MonoBehaviour
{
    private bool isGateOpened;
    [SerializeField] private HumanWalkToWindow humanWalk;
    [SerializeField] private CameraMove cameraMove;
    [SerializeField] private KeyCode buttonInteract;
    [SerializeField] private GameObject Gate;
    [SerializeField] private GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        isGateOpened = false;
    }

    // Update is called once per frame
    void Update()
    {
        ButtonInteract();
    }

    private void ButtonInteract()
    {
        if (Input.GetKeyDown(buttonInteract) && !isGateOpened && cameraMove.down && humanWalk.hCreatedCh)
        {
            isGateOpened = true;
            button.transform.DOLocalMoveZ(0f, 0.5f);
            Gate.transform.DOLocalMoveZ(-3.5F, 5F);
            StartCoroutine(DelayGate());
            humanWalk.leavingSequence = true;
            StartCoroutine(humanWalk.HumanNahuiPoshel());
		}
    }

    private IEnumerator DelayGate()
    {
		yield return new WaitForSeconds(0.5f);
		button.transform.DOLocalMoveZ(0.00043f, 1f);
		yield return new WaitForSeconds(20);
		Gate.transform.DOLocalMoveZ(-2.3276f, 5f);
        yield return new WaitForSeconds(7);
		isGateOpened = false;
	}
}

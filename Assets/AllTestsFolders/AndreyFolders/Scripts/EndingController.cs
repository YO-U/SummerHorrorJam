using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingController : MonoBehaviour
{
    public int endingNumber = 0;
    public bool IsEndingStarting;
    [SerializeField] private MovingCar movingCar;
    [SerializeField] private HumanWalkToWindow humanWalk;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(StartEnding());
    }

    private void EndingSelector()
    {
        switch (endingNumber)
        {
            case 0:
                break; 
            case 1:
                break; 
            case 2:
                break;
            case 3:
                break;
        }
    }

    private IEnumerator StartEnding()
    {
        if (IsEndingStarting)
        {
            IsEndingStarting= false;
            yield return new WaitForSeconds(10);
            EndingSelector();
        }
    }
}

using System.Collections;
using UnityEngine;

public class ElevatorBox : MonoBehaviour
{
    public GameObject LeftDoor;
    public GameObject RightDoor;
    public Transform startFloor; // 1층 위치
    private bool isMoving = false;
    private Vector3 initialPosition; // 초기 위치 (1층 위치)

    private void Start()
    {
        initialPosition = startFloor.position; // 초기 위치 저장
        StartCoroutine(DoorControlLoop()); // 문 제어 루프 시작
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isMoving)
        {
            Vector3 endPosition = new Vector3(startFloor.position.x, startFloor.position.y + 4, startFloor.position.z);
            StartCoroutine(MoveElevator(endPosition)); // 엘리베이터 이동 시작
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(MoveElevator(initialPosition)); // 엘리베이터 이동 종료 후 초기 위치로 복귀
        }
    }

    IEnumerator MoveElevator(Vector3 targetPosition)
    {
        isMoving = true;
        float elapsedTime = 0;
        float journeyTime = 10f; // 이동 시간
        Vector3 startPosition = transform.position;

        while (elapsedTime < journeyTime)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / journeyTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;
        isMoving = false;
    }

    IEnumerator DoorControlLoop()
    {
        while (true)
        {
            while (!isMoving)
            {
                yield return new WaitForSeconds(5);
                StartCoroutine(OpenDoors());
                yield return new WaitForSeconds(5);
                StartCoroutine(CloseDoors());
            }
            yield return null; // 엘리베이터가 움직이는 동안 대기
        }
    }

    IEnumerator OpenDoors()
    {
        if (isMoving) yield break;

        Vector3 leftDoorTarget = LeftDoor.transform.position + new Vector3(0, 0, -1.0f); // 왼쪽으로 1 유닛 이동 (z축)
        Vector3 rightDoorTarget = RightDoor.transform.position + new Vector3(0, 0, 1.0f); // 오른쪽으로 1 유닛 이동 (z축)
        float elapsedTime = 0;
        float duration = 2f;

        while (elapsedTime < duration)
        {
            LeftDoor.transform.position = Vector3.Lerp(LeftDoor.transform.position, leftDoorTarget, elapsedTime / duration);
            RightDoor.transform.position = Vector3.Lerp(RightDoor.transform.position, rightDoorTarget, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator CloseDoors()
    {
        if (isMoving) yield break;

        Vector3 leftDoorStart = LeftDoor.transform.position + new Vector3(0, 0, 1.0f); // 원래 위치로 이동 (z축)
        Vector3 rightDoorStart = RightDoor.transform.position + new Vector3(0, 0, -1.0f); // 원래 위치로 이동 (z축)
        float elapsedTime = 0;
        float duration = 2f;

        while (elapsedTime < duration)
        {
            LeftDoor.transform.position = Vector3.Lerp(LeftDoor.transform.position, leftDoorStart, elapsedTime / duration);
            RightDoor.transform.position = Vector3.Lerp(RightDoor.transform.position, rightDoorStart, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
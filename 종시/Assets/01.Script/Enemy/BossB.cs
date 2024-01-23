using System.Collections; // 코루틴 쓸 때 필요한 using문
using UnityEngine;

// BossA의 구조 -> 보스 패턴, 보스 움직임을 담고 있는 클래스
public class BossB : BossA
{
    public GameObject Projectile; // 적의 Projectile을 생성함
    public float ProjectileMoveSpeed = 5.0f; // 적의 Projectile이 움직이는 속도
    public float FireRate = 2.0f; // 실제 발사 속도
    public float MoveSpeed = 2.0f; // 실제 이동 속도
    public float MoveDistance = 5.0f; // 실제 이동 거리

    private int _currentPatternIndex = 0; // 현재 패턴값을 담고 있는 변수
    private bool _movingRight = true; // 현재 보스가 오른쪽으로 이동하는지 체크하는 bool
    private bool _bCanMove = false; // 현재 보스가 움직이는지 체크하는 bool
    private Vector3 _originPosition; // 원래(본래)의 위치

    private void Start()
    {
        _originPosition = transform.position; // _originPosition값을 현재의 transform.position값으로 할당함
        StartCoroutine(MoveDownAndStartPattern()); // 시작하면 MoveDownAndStartPattern 코루틴이 시작됨
    }

    private IEnumerator MoveDownAndStartPattern()
    {
        while (transform.position.y > _originPosition.y - 3f) // 만약 transform.position.y값이 _originPosition.y - 3f보다 크면 while반복문이 돌아감
        {
            transform.Translate(Vector3.down * MoveSpeed * Time.deltaTime); // 현재 위치를 Vector3.down코드를 이용하여 아래로 이동시키고, MoveSpeed의 영향을 받아 runtime중일 때 초당 움직임
            yield return null; // 코루틴의 실행을 일시중지
        }

        _bCanMove = true; // 현재 움직일 수 있다는 것을 bool로 알려줌
        InvokeRepeating("NextPattern", 2.0f, FireRate); // 2초후에 FireRate(현재 설정 값 : 2.0f)마다 다음패턴이 실행됨
    }

    private void Update()
    {
        if (_bCanMove)
            MoveSideways(); // MoveSideways를 실행함 (_bCanMove가 True이면)
    }

    private void NextPattern()
    {
        // 패턴 인덱스를 증가시키고, 마지막 패턴일 경우 다시 처음 패턴으로 돌아감
        _currentPatternIndex = (_currentPatternIndex + 1) % 6;

        // 현재 패턴 실행
        switch (_currentPatternIndex)
        {
            case 0:
                Pattern1(); // 만약 인덱스가 0이면 Pattern1 함수를 실행시킴
                break;
            case 1:
                Pattern2(); // 만약 인덱스가 1이면 Pattern2 함수를 실행시킴
                break;
            case 2:
                StartCoroutine(Pattern3()); // 만약 인덱스가 1이면 Pattern3 코루틴을 실행시킴
                break;
            case 3:
                Pattern4();  // 만약 인덱스가 3이면 Pattern4 함수를 실행시킴
                break;
            case 4:
                Pattern5();  // 만약 인덱스가 3이면 Pattern5 함수를 실행시킴
                break;
            case 5:
                Pattern6();  // 만약 인덱스가 3이면 Pattern6 함수를 실행시킴
                break;
        }
    }

    private void MoveSideways()
    {
        if (_movingRight)
        {
            transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime); // 오른쪽으로 runtime일 때 초당 MoveSpeed를 곱한 값으로 이동함
            if (transform.position.x > MoveDistance) 
            {
                _movingRight = false; // _movingRight를 false로 바꿈
            }
        }
        else
        {
            transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);  // 왼쪽으로 runtime일 때 초당 MoveSpeed를 곱한 값으로 이동함
            if (transform.position.x < -MoveDistance) 
            {
                _movingRight = true;  // _movingRight를 true로 바꿈
            }
        }
    }


    private void StartMovingSideways()
    {
        StartCoroutine(MovingSidewaysRoutine()); // 해당 코루틴을 호출함
    }

    private IEnumerator MovingSidewaysRoutine()
    {
        while (true)
        {
            MoveSideways(); // MoveSideways()를 실행
            yield return null; // 코루틴의 실행을 일시중지
        }
    }

    public void ShootProjectile(Vector3 position, Vector3 direction)
    {
        GameObject instance = Instantiate(Projectile, position, Quaternion.identity); // 인스턴스화 시킴 (현재 보스의 총알을)
        Projectile projectile = instance.GetComponent<Projectile>(); // Projectile에 접근함

        if (projectile != null) // 만약 projectile이 null이 아니라면 (null체크)
        {
            projectile.MoveSpeed = ProjectileMoveSpeed; // projectile.MoveSpeed값을 ProjectileMoveSpeed로 할당함
            projectile.SetDirection(direction.normalized); // 방향 설정
        } 
    }

    private void Pattern1()
    {
        // 패턴 1: 원형으로 총알 발사
        int numBullets1 = 10;
        float angleStep1 = 30.0f / numBullets1; // 현재 각도를 numBullets1으로 나눔 (각도에 따라 나눠서 발사되게 하려고)

        for (int i = 0; i < numBullets1; i++) // i는 0부터 시작 & i보다 numBullets1값이 크면 계속 for반복문이 시작됨
        {
            float angle1 = i * angleStep1;
            float radian1 = angle1 * Mathf.Deg2Rad; // π / 180 값을 가진다. 일반 각도(몇 도 몇 도 하는 그 Degree)에 Mathf.Deg2Rad을 곱하면 라디안으로 변환한 값을 구할 수 있다.
            Vector3 direction1 = new Vector3(Mathf.Cos(radian1), Mathf.Sin(radian1), 0);

            ShootProjectile(transform.position, direction1);
        }
    }

    private void Pattern2()
    {
        // 패턴 2: 방사형으로 총알 발사
        int numBullets2 = 30;
        float angleStep2 = 180f / numBullets2; // 현재 각도를 numBullets2으로 나눔 (각도에 따라 나눠서 발사되게 하려고)

        for (int i = 0; i < numBullets2; i++)
        {
            float angle2 = i * angleStep2;
            float radian2 = angle2 * Mathf.Deg2Rad; // π / 180 값을 가진다. 일반 각도(몇 도 몇 도 하는 그 Degree)에 Mathf.Deg2Rad을 곱하면 라디안으로 변환한 값을 구할 수 있다.
            Vector3 direction2 = new Vector3(Mathf.Cos(radian2), Mathf.Sin(radian2), 0);

            ShootProjectile(transform.position, direction2);
        }


    }

    private IEnumerator Pattern3()
    {
        // 패턴 3: 몇 초 간격으로 플레이어에게 하나씩 발사
        int numBullets = 3;
        float interval = 3.0f;

        for (int i = 0; i < numBullets; i++)
        {
            Vector3 playerDirection = (PlayerPosition() - transform.position).normalized; // 현재 플레이어 Position - 보스 Position을 빼서 정규화시킴 (이걸 Vector3 playerDirection에 저장함)
            ShootProjectile(transform.position, playerDirection); // 발사
            yield return new WaitForSeconds(interval);
        }
    }

    private void Pattern4()
    {
        // 패턴 4: 나선형으로 총알 발사
        int numBullets3 = 10;
        float angleStep3 = 360.0f / numBullets3;
        float radius = 2.0f;

        for (int i = 0; i < numBullets3; i++)
        {
            float angle3 = i * angleStep3;
            float radian3 = angle3 * Mathf.Deg2Rad;
            float x = radius * Mathf.Cos(radian3);
            float y = radius * Mathf.Sin(radian3);

            Vector3 direction3 = new Vector3(x, y, 0).normalized;

            ShootProjectile(transform.position, direction3);
        }
    }

    private void Pattern5()
    {
        // 패턴 5: 삼각형 모양으로 총알을 발사하는 메서드

        // 발사할 총알의 개수
        int numBullets5 = 3;

        // 각 총알의 간격 각도 계산
        float angleStep5 = 360.0f / numBullets5;

        // 발사되는 총알의 반경
        float radius5 = 3.0f;

        // 각 총알에 대한 루프
        for (int i = 0; i < numBullets5; i++)
        {
            // 현재 총알의 발사 각도 계산
            float angle5 = i * angleStep5;

            // 발사 각도를 라디안으로 변환
            float radian5 = angle5 * Mathf.Deg2Rad;

            // 총알의 x, y 좌표 계산
            float x5 = radius5 * Mathf.Cos(radian5);
            float y5 = radius5 * Mathf.Sin(radian5);

            // 총알의 이동 방향을 정규화된 벡터로 설정
            Vector3 direction5 = new Vector3(x5, y5, 0).normalized;

            // 총알 발사 메서드 호출
            ShootProjectile(transform.position, direction5);
        }
    }


    private void Pattern6()
    {
        // 패턴 6: 둥근 곡선 형태로 총알을 발사하는 메서드

        // 발사할 총알의 개수
        int numBullets = 20;

        // 각 총알의 간격 각도 계산
        float angleStep = -90.0f / numBullets;

        // 시작 각도
        float startAngle = 360.0f;

        // 둥근 곡선의 높이 조절
        float curveHeight = 1.0f;

        // 각 총알에 대한 루프
        for (int i = 0; i < numBullets; i++)
        {
            // 현재 총알의 발사 각도 계산
            float angle = startAngle + i * angleStep;

            // 발사 각도를 라디안으로 변환
            float radian = angle * Mathf.Deg2Rad;

            // 둥근 곡선 형태로 각도에 따른 높이 계산
            float curve = curveHeight * Mathf.Sin(i * Mathf.PI / (numBullets - 1));

            // 총알의 x, y 좌표 및 둥근 곡선의 높이를 고려한 이동 방향 설정
            Vector3 direction = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian) + curve, 0).normalized;

            // 총알 발사 메서드 호출
            ShootProjectile(transform.position, direction);
        }
    }

    private Vector3 PlayerPosition()
    {
        return GameManager.Instance.GetPlayerCharacter().transform.position; // GameManager에 인스턴스하여 Player의 위치를 가져옴
    }

    private void OnDestroy()
    {
        //GameManager.Instance.StageClear();
    }


}
using System.Collections; // �ڷ�ƾ �� �� �ʿ��� using��
using UnityEngine;

// BossA�� ���� -> ���� ����, ���� �������� ��� �ִ� Ŭ����
public class BossB : BossA
{
    public GameObject Projectile; // ���� Projectile�� ������
    public float ProjectileMoveSpeed = 5.0f; // ���� Projectile�� �����̴� �ӵ�
    public float FireRate = 2.0f; // ���� �߻� �ӵ�
    public float MoveSpeed = 2.0f; // ���� �̵� �ӵ�
    public float MoveDistance = 5.0f; // ���� �̵� �Ÿ�

    private int _currentPatternIndex = 0; // ���� ���ϰ��� ��� �ִ� ����
    private bool _movingRight = true; // ���� ������ ���������� �̵��ϴ��� üũ�ϴ� bool
    private bool _bCanMove = false; // ���� ������ �����̴��� üũ�ϴ� bool
    private Vector3 _originPosition; // ����(����)�� ��ġ

    private void Start()
    {
        _originPosition = transform.position; // _originPosition���� ������ transform.position������ �Ҵ���
        StartCoroutine(MoveDownAndStartPattern()); // �����ϸ� MoveDownAndStartPattern �ڷ�ƾ�� ���۵�
    }

    private IEnumerator MoveDownAndStartPattern()
    {
        while (transform.position.y > _originPosition.y - 3f) // ���� transform.position.y���� _originPosition.y - 3f���� ũ�� while�ݺ����� ���ư�
        {
            transform.Translate(Vector3.down * MoveSpeed * Time.deltaTime); // ���� ��ġ�� Vector3.down�ڵ带 �̿��Ͽ� �Ʒ��� �̵���Ű��, MoveSpeed�� ������ �޾� runtime���� �� �ʴ� ������
            yield return null; // �ڷ�ƾ�� ������ �Ͻ�����
        }

        _bCanMove = true; // ���� ������ �� �ִٴ� ���� bool�� �˷���
        InvokeRepeating("NextPattern", 2.0f, FireRate); // 2���Ŀ� FireRate(���� ���� �� : 2.0f)���� ���������� �����
    }

    private void Update()
    {
        if (_bCanMove)
            MoveSideways(); // MoveSideways�� ������ (_bCanMove�� True�̸�)
    }

    private void NextPattern()
    {
        // ���� �ε����� ������Ű��, ������ ������ ��� �ٽ� ó�� �������� ���ư�
        _currentPatternIndex = (_currentPatternIndex + 1) % 6;

        // ���� ���� ����
        switch (_currentPatternIndex)
        {
            case 0:
                Pattern1(); // ���� �ε����� 0�̸� Pattern1 �Լ��� �����Ŵ
                break;
            case 1:
                Pattern2(); // ���� �ε����� 1�̸� Pattern2 �Լ��� �����Ŵ
                break;
            case 2:
                StartCoroutine(Pattern3()); // ���� �ε����� 1�̸� Pattern3 �ڷ�ƾ�� �����Ŵ
                break;
            case 3:
                Pattern4();  // ���� �ε����� 3�̸� Pattern4 �Լ��� �����Ŵ
                break;
            case 4:
                Pattern5();  // ���� �ε����� 3�̸� Pattern5 �Լ��� �����Ŵ
                break;
            case 5:
                Pattern6();  // ���� �ε����� 3�̸� Pattern6 �Լ��� �����Ŵ
                break;
        }
    }

    private void MoveSideways()
    {
        if (_movingRight)
        {
            transform.Translate(Vector3.right * MoveSpeed * Time.deltaTime); // ���������� runtime�� �� �ʴ� MoveSpeed�� ���� ������ �̵���
            if (transform.position.x > MoveDistance) 
            {
                _movingRight = false; // _movingRight�� false�� �ٲ�
            }
        }
        else
        {
            transform.Translate(Vector3.left * MoveSpeed * Time.deltaTime);  // �������� runtime�� �� �ʴ� MoveSpeed�� ���� ������ �̵���
            if (transform.position.x < -MoveDistance) 
            {
                _movingRight = true;  // _movingRight�� true�� �ٲ�
            }
        }
    }


    private void StartMovingSideways()
    {
        StartCoroutine(MovingSidewaysRoutine()); // �ش� �ڷ�ƾ�� ȣ����
    }

    private IEnumerator MovingSidewaysRoutine()
    {
        while (true)
        {
            MoveSideways(); // MoveSideways()�� ����
            yield return null; // �ڷ�ƾ�� ������ �Ͻ�����
        }
    }

    public void ShootProjectile(Vector3 position, Vector3 direction)
    {
        GameObject instance = Instantiate(Projectile, position, Quaternion.identity); // �ν��Ͻ�ȭ ��Ŵ (���� ������ �Ѿ���)
        Projectile projectile = instance.GetComponent<Projectile>(); // Projectile�� ������

        if (projectile != null) // ���� projectile�� null�� �ƴ϶�� (nullüũ)
        {
            projectile.MoveSpeed = ProjectileMoveSpeed; // projectile.MoveSpeed���� ProjectileMoveSpeed�� �Ҵ���
            projectile.SetDirection(direction.normalized); // ���� ����
        } 
    }

    private void Pattern1()
    {
        // ���� 1: �������� �Ѿ� �߻�
        int numBullets1 = 10;
        float angleStep1 = 30.0f / numBullets1; // ���� ������ numBullets1���� ���� (������ ���� ������ �߻�ǰ� �Ϸ���)

        for (int i = 0; i < numBullets1; i++) // i�� 0���� ���� & i���� numBullets1���� ũ�� ��� for�ݺ����� ���۵�
        {
            float angle1 = i * angleStep1;
            float radian1 = angle1 * Mathf.Deg2Rad; // �� / 180 ���� ������. �Ϲ� ����(�� �� �� �� �ϴ� �� Degree)�� Mathf.Deg2Rad�� ���ϸ� �������� ��ȯ�� ���� ���� �� �ִ�.
            Vector3 direction1 = new Vector3(Mathf.Cos(radian1), Mathf.Sin(radian1), 0);

            ShootProjectile(transform.position, direction1);
        }
    }

    private void Pattern2()
    {
        // ���� 2: ��������� �Ѿ� �߻�
        int numBullets2 = 30;
        float angleStep2 = 180f / numBullets2; // ���� ������ numBullets2���� ���� (������ ���� ������ �߻�ǰ� �Ϸ���)

        for (int i = 0; i < numBullets2; i++)
        {
            float angle2 = i * angleStep2;
            float radian2 = angle2 * Mathf.Deg2Rad; // �� / 180 ���� ������. �Ϲ� ����(�� �� �� �� �ϴ� �� Degree)�� Mathf.Deg2Rad�� ���ϸ� �������� ��ȯ�� ���� ���� �� �ִ�.
            Vector3 direction2 = new Vector3(Mathf.Cos(radian2), Mathf.Sin(radian2), 0);

            ShootProjectile(transform.position, direction2);
        }


    }

    private IEnumerator Pattern3()
    {
        // ���� 3: �� �� �������� �÷��̾�� �ϳ��� �߻�
        int numBullets = 3;
        float interval = 3.0f;

        for (int i = 0; i < numBullets; i++)
        {
            Vector3 playerDirection = (PlayerPosition() - transform.position).normalized; // ���� �÷��̾� Position - ���� Position�� ���� ����ȭ��Ŵ (�̰� Vector3 playerDirection�� ������)
            ShootProjectile(transform.position, playerDirection); // �߻�
            yield return new WaitForSeconds(interval);
        }
    }

    private void Pattern4()
    {
        // ���� 4: ���������� �Ѿ� �߻�
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
        // ���� 5: �ﰢ�� ������� �Ѿ��� �߻��ϴ� �޼���

        // �߻��� �Ѿ��� ����
        int numBullets5 = 3;

        // �� �Ѿ��� ���� ���� ���
        float angleStep5 = 360.0f / numBullets5;

        // �߻�Ǵ� �Ѿ��� �ݰ�
        float radius5 = 3.0f;

        // �� �Ѿ˿� ���� ����
        for (int i = 0; i < numBullets5; i++)
        {
            // ���� �Ѿ��� �߻� ���� ���
            float angle5 = i * angleStep5;

            // �߻� ������ �������� ��ȯ
            float radian5 = angle5 * Mathf.Deg2Rad;

            // �Ѿ��� x, y ��ǥ ���
            float x5 = radius5 * Mathf.Cos(radian5);
            float y5 = radius5 * Mathf.Sin(radian5);

            // �Ѿ��� �̵� ������ ����ȭ�� ���ͷ� ����
            Vector3 direction5 = new Vector3(x5, y5, 0).normalized;

            // �Ѿ� �߻� �޼��� ȣ��
            ShootProjectile(transform.position, direction5);
        }
    }


    private void Pattern6()
    {
        // ���� 6: �ձ� � ���·� �Ѿ��� �߻��ϴ� �޼���

        // �߻��� �Ѿ��� ����
        int numBullets = 20;

        // �� �Ѿ��� ���� ���� ���
        float angleStep = -90.0f / numBullets;

        // ���� ����
        float startAngle = 360.0f;

        // �ձ� ��� ���� ����
        float curveHeight = 1.0f;

        // �� �Ѿ˿� ���� ����
        for (int i = 0; i < numBullets; i++)
        {
            // ���� �Ѿ��� �߻� ���� ���
            float angle = startAngle + i * angleStep;

            // �߻� ������ �������� ��ȯ
            float radian = angle * Mathf.Deg2Rad;

            // �ձ� � ���·� ������ ���� ���� ���
            float curve = curveHeight * Mathf.Sin(i * Mathf.PI / (numBullets - 1));

            // �Ѿ��� x, y ��ǥ �� �ձ� ��� ���̸� ����� �̵� ���� ����
            Vector3 direction = new Vector3(Mathf.Cos(radian), Mathf.Sin(radian) + curve, 0).normalized;

            // �Ѿ� �߻� �޼��� ȣ��
            ShootProjectile(transform.position, direction);
        }
    }

    private Vector3 PlayerPosition()
    {
        return GameManager.Instance.GetPlayerCharacter().transform.position; // GameManager�� �ν��Ͻ��Ͽ� Player�� ��ġ�� ������
    }

    private void OnDestroy()
    {
        //GameManager.Instance.StageClear();
    }


}
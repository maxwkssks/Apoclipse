using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �Ѿ˿� ���� Ŭ���� �ε�
public class Projectile : MonoBehaviour
{
	// �ν����Ϳ����� ���� �� �� ������, public���� �����Ͽ� MoveSpeed�� ������ �� ����.
	[HideInInspector]
	public float MoveSpeed = 2f;

	// �Ѿ��� ���ư��� ����
	private Vector3 _direction;

	// �̰� ����Ʈ�ΰ� ����
	public GameObject ExplodeFX;

	// ������ �Ѿ��� ������ �ڿ� ������ �� �ִ� �ð�
	[SerializeField]
	private float _lifeTime = 3f;

	// ������ �ǰ�, lifeTime�� ������ �����
	void Start()
	{
		Destroy(gameObject, _lifeTime);
	}

	// transform.Translate�� �̿��ؼ� ���ʸ��� MoveSpeed�� ������ �޾Ƽ� �̵���
	void Update()
	{
		transform.Translate(_direction * MoveSpeed * Time.deltaTime);
	}

	// Vector3�� �̿��ؼ� ������ ������
	public void SetDirection(Vector3 direction)
	{
		_direction = direction;
	}

	// ���� Projectile�� ������� �Ͼ�� ��
	private void OnDestroy()
	{
		//Instantiate(ExplodeFX, transform.position, Quaternion.identity);
	}
}

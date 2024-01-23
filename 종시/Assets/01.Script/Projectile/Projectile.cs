using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 총알에 대한 클래스 인데
public class Projectile : MonoBehaviour
{
	// 인스펙터에서는 조절 할 수 없지만, public으로 선언하여 MoveSpeed를 조정할 수 있음.
	[HideInInspector]
	public float MoveSpeed = 2f;

	// 총알이 날아가는 방향
	private Vector3 _direction;

	// 이건 이펙트인거 같음
	public GameObject ExplodeFX;

	// 실제로 총알이 생성된 뒤에 움직일 수 있는 시간
	[SerializeField]
	private float _lifeTime = 3f;

	// 생성이 되고, lifeTime이 지나면 사라짐
	void Start()
	{
		Destroy(gameObject, _lifeTime);
	}

	// transform.Translate를 이용해서 매초마다 MoveSpeed의 영향을 받아서 이동함
	void Update()
	{
		transform.Translate(_direction * MoveSpeed * Time.deltaTime);
	}

	// Vector3를 이용해서 방향을 정의함
	public void SetDirection(Vector3 direction)
	{
		_direction = direction;
	}

	// 만약 Projectile이 사라지면 일어나는 일
	private void OnDestroy()
	{
		//Instantiate(ExplodeFX, transform.position, Quaternion.identity);
	}
}

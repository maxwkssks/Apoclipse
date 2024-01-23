using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BaseSkill에서 상속을 받아서 기본스킬을 표현한 클래스 -> 레벨에 따라 고유 동작이 다름.
public class PrimarySkill : BaseSkill
{
	// 총알(탄환) 이동속도
	public float ProjectileMoveSpeed;

	// 총알(탄환)
	public GameObject Projectile;

	// 무기를 저장할 공간
	private Weapon[] weapons;

	void Start()
	{
		// 기본 스킬 쿨타임
		CooldownTime = 0.2f;

		weapons = new Weapon[6];

		weapons[0] = new Level1Weapon();
		weapons[1] = new Level2Weapon();
		weapons[2] = new Level3Weapon();
		weapons[3] = new Level4Weapon();
		weapons[4] = new Level5Weapon(); // -> 각 레벨을 배열 나타내서 Weapon레벨을 정의함
		weapons[5] = new Level6Weapon();
	}

	// 실제로 현재의 weapon레벨에 따라서 스킬이 달라짐
	public override void Activate()
	{
		base.Activate();
		weapons[_characterManager.Player.GetComponent<PlayerCharacter>().CurrentWeaponLevel].Activate(this, _characterManager);
		//GameManager.Instance.SoundManager.PlaySFX("PrimarySkill");
	}

	public void ShootProjectile(Vector3 position, Vector3 direction)
	{
		GameObject instance = Instantiate(Projectile, position, Quaternion.identity); // 프리팹을 인스턴스화 시켜서 실제로 보이게끔 실체화시킴
		Projectile projectile = instance.GetComponent<Projectile>(); // Projectile에 접근함

		if (projectile != null)
		{
			projectile.MoveSpeed = ProjectileMoveSpeed; // 현재의 MoveSpeed에 ProjectileMoveSpeed 값을 참조시킴
			projectile.SetDirection(direction.normalized); // 방향을 정해서 표준화 시킴
		}
	}
}


public interface Weapon
{
	// 캐릭터 관리자 객체, 무기를 활성화할 대상 캐릭터를 관리하는 데 사용됨
	void Activate(PrimarySkill primarySkill, CharacterManager characterManager);
}

// 레벨 1 무기가 어떻게 동작하는지 구현한 부분
public class Level1Weapon : Weapon
{
	
	public void Activate(PrimarySkill primarySkill, CharacterManager characterManager)
	{
		// 레벨 1 무기의 특정 동작
		Vector3 position = characterManager.Player.transform.position;
		primarySkill.ShootProjectile(position, Vector3.up);
	}
}

// 레벨 2 무기가 어떻게 동작하는지 구현한 부분
public class Level2Weapon : Weapon
{
	
	public void Activate(PrimarySkill primarySkill, CharacterManager characterManager)
	{
		// 레벨 2 무기의 특정 동작
		Vector3 position = characterManager.Player.transform.position;
		position.x -= 0.1f;

		for (int i = 0; i < 2; i++)
		{
			primarySkill.ShootProjectile(position, Vector3.up);
			position.x += 0.2f;
		}
	}
}

// 레벨 3 무기가 어떻게 동작하는지 구현한 부분
public class Level3Weapon : Weapon
{
	public void Activate(PrimarySkill primarySkill, CharacterManager characterManager)
	{
		// 레벨 3 무기의 특정 동작
		Vector3 position = characterManager.Player.transform.position;

		primarySkill.ShootProjectile(position, Vector3.up);
		primarySkill.ShootProjectile(position, new Vector3(0.3f, 1, 0));
		primarySkill.ShootProjectile(position, new Vector3(-0.3f, 1, 0));
	}
}

// 레벨 4 무기가 어떻게 동작하는지 구현한 부분
public class Level4Weapon : Weapon
{
	public void Activate(PrimarySkill primarySkill, CharacterManager characterManager)
	{
		// 레벨 4 무기의 특정 동작
		Vector3 position = characterManager.Player.transform.position;
		position.x -= 0.1f;

		for (int i = 0; i < 2; i++)
		{
			primarySkill.ShootProjectile(position, Vector3.up);
			position.x += 0.2f;
		}

		Vector3 position2 = characterManager.Player.transform.position;
		primarySkill.ShootProjectile(position2, new Vector3(0.3f, 1, 0));
		primarySkill.ShootProjectile(position2, new Vector3(-0.3f, 1, 0));
	}
}

// 레벨 5 무기가 어떻게 동작하는지 구현한 부분
public class Level5Weapon : Weapon
{
	public void Activate(PrimarySkill primarySkill, CharacterManager characterManager)
	{
		// 레벨 5 무기의 특정 동작
		Vector3 position = characterManager.Player.transform.position;

		for (int i = 0; i < 180; i += 10) // 360도를 10도씩 나눠서 총알 발사
		{
			float angle = i * Mathf.Deg2Rad;
			Debug.Log(angle);
			Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0);

			primarySkill.ShootProjectile(position, direction);
		}
	}
}

public class Level6Weapon : Weapon
{
	public void Activate(PrimarySkill primarySkill, CharacterManager characterManager)
	{
		// 플레이어 위치를 저장
		Vector3 position = characterManager.Player.transform.position;

		for (float x = -18; x <= 18; x += 1) // x값을 -18부터 18까지 1씩 증가시키면서 반복하는 반복문
		{
			// +y축 방향으로 발사 
			Vector3 directionPositiveY = new Vector3(0, 1, 0); // 이 방향으로 
			Vector3 offsetPositiveY = new Vector3(x, 0, 0); // 이 위치에서 발사
			primarySkill.ShootProjectile(position + offsetPositiveY, directionPositiveY); //  총알을 발사하는 역할을 수행하는 primarySkill.ShootProjectile을 가져와서
																						  //  현재 플레이어의 위치에서 y축 방향으로 x만큼 떨어진 위치입니다
																						  //  Vector3(0, 1, 0) -> y축 방향으로 1의 크기를 갖는데, 이게 primarySkill.ShootProjectile 내에 존재하는 인스턴스화를 시켜서
																						  // Projectile에 접근하고나서 현재의 MoveSpeed에 ProjectileMoveSpeed 값을 참조시키고 이 방향을 표준화 시키면서
																						  // 총알을 날아가게 한다.
			// -y축 방향으로 발사
			Vector3 directionNegativeY = new Vector3(0, -1, 0); // 이 방향으로 
			Vector3 offsetNegativeY = new Vector3(x, 0, 0); // 이 위치에서 발사
			primarySkill.ShootProjectile(position + offsetNegativeY, directionNegativeY); //  총알을 발사하는 역할을 수행하는 primarySkill.ShootProjectile을 가져와서 
																						  //  현재 플레이어의 위치에서  y축 방향으로 -x만큼 떨어진 위치입니다
																						  //  Vector3(0, -1, 0) -> y축 방향으로 -1의 크기를 갖는데, 이게 primarySkill.ShootProjectile 내에 존재하는 인스턴스화를 시켜서
																						  // Projectile에 접근하고나서 현재의 MoveSpeed에 ProjectileMoveSpeed 값을 참조시키고 이 방향을 표준화 시키면서
																						  // 총알을 날아가게 한다.

			// +x축 방향으로 발사
			Vector3 directionPositiveX = new Vector3(1, 0, 0); // 이 방향으로 
			Vector3 offsetPositiveX = new Vector3(0, x, 0); // 이 위치에서 발사
			primarySkill.ShootProjectile(position + offsetPositiveX, directionPositiveX); //  총알을 발사하는 역할을 수행하는 primarySkill.ShootProjectile을 가져와서 
																						  //  현재 플레이어의 위치에서  x축 방향으로 x만큼 떨어진 위치입니다.
																						  //  Vector3(1, 0, 0); -> x축 방향으로 1의 크기를 갖는데, 이게 primarySkill.ShootProjectile 내에 존재하는 인스턴스화를 시켜서
																						  // Projectile에 접근하고나서 현재의 MoveSpeed에 ProjectileMoveSpeed 값을 참조시키고 이 방향을 표준화 시키면서
																						  // 총알을 날아가게 한다.

			Vector3 directionNegativeX = new Vector3(-1, 0, 0); // 이 방향으로 
			Vector3 offsetNegativeX = new Vector3(0, x, 0); // 이 위치에서 발사
			primarySkill.ShootProjectile(position + offsetNegativeX, directionNegativeX); //  총알을 발사하는 역할을 수행하는 primarySkill.ShootProjectile을 가져와서 
																						  //  현재 플레이어의 위치에서  x축 방향으로 -x만큼 떨어진 위치입니다.
																						  //  Vector3(-1, 0, 0); -> x축 방향으로 -1의 크기를 갖는데, 이게 primarySkill.ShootProjectile 내에 존재하는 인스턴스화를 시켜서
																						  // Projectile에 접근하고나서 현재의 MoveSpeed에 ProjectileMoveSpeed 값을 참조시키고 이 방향을 표준화 시키면서
																						  // 총알을 날아가게 한다.
		}
	}
}






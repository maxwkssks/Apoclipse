using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BaseSkill���� ����� �޾Ƽ� �⺻��ų�� ǥ���� Ŭ���� -> ������ ���� ���� ������ �ٸ�.
public class PrimarySkill : BaseSkill
{
	// �Ѿ�(źȯ) �̵��ӵ�
	public float ProjectileMoveSpeed;

	// �Ѿ�(źȯ)
	public GameObject Projectile;

	// ���⸦ ������ ����
	private Weapon[] weapons;

	void Start()
	{
		// �⺻ ��ų ��Ÿ��
		CooldownTime = 0.2f;

		weapons = new Weapon[6];

		weapons[0] = new Level1Weapon();
		weapons[1] = new Level2Weapon();
		weapons[2] = new Level3Weapon();
		weapons[3] = new Level4Weapon();
		weapons[4] = new Level5Weapon(); // -> �� ������ �迭 ��Ÿ���� Weapon������ ������
		weapons[5] = new Level6Weapon();
	}

	// ������ ������ weapon������ ���� ��ų�� �޶���
	public override void Activate()
	{
		base.Activate();
		weapons[_characterManager.Player.GetComponent<PlayerCharacter>().CurrentWeaponLevel].Activate(this, _characterManager);
		//GameManager.Instance.SoundManager.PlaySFX("PrimarySkill");
	}

	public void ShootProjectile(Vector3 position, Vector3 direction)
	{
		GameObject instance = Instantiate(Projectile, position, Quaternion.identity); // �������� �ν��Ͻ�ȭ ���Ѽ� ������ ���̰Բ� ��üȭ��Ŵ
		Projectile projectile = instance.GetComponent<Projectile>(); // Projectile�� ������

		if (projectile != null)
		{
			projectile.MoveSpeed = ProjectileMoveSpeed; // ������ MoveSpeed�� ProjectileMoveSpeed ���� ������Ŵ
			projectile.SetDirection(direction.normalized); // ������ ���ؼ� ǥ��ȭ ��Ŵ
		}
	}
}


public interface Weapon
{
	// ĳ���� ������ ��ü, ���⸦ Ȱ��ȭ�� ��� ĳ���͸� �����ϴ� �� ����
	void Activate(PrimarySkill primarySkill, CharacterManager characterManager);
}

// ���� 1 ���Ⱑ ��� �����ϴ��� ������ �κ�
public class Level1Weapon : Weapon
{
	
	public void Activate(PrimarySkill primarySkill, CharacterManager characterManager)
	{
		// ���� 1 ������ Ư�� ����
		Vector3 position = characterManager.Player.transform.position;
		primarySkill.ShootProjectile(position, Vector3.up);
	}
}

// ���� 2 ���Ⱑ ��� �����ϴ��� ������ �κ�
public class Level2Weapon : Weapon
{
	
	public void Activate(PrimarySkill primarySkill, CharacterManager characterManager)
	{
		// ���� 2 ������ Ư�� ����
		Vector3 position = characterManager.Player.transform.position;
		position.x -= 0.1f;

		for (int i = 0; i < 2; i++)
		{
			primarySkill.ShootProjectile(position, Vector3.up);
			position.x += 0.2f;
		}
	}
}

// ���� 3 ���Ⱑ ��� �����ϴ��� ������ �κ�
public class Level3Weapon : Weapon
{
	public void Activate(PrimarySkill primarySkill, CharacterManager characterManager)
	{
		// ���� 3 ������ Ư�� ����
		Vector3 position = characterManager.Player.transform.position;

		primarySkill.ShootProjectile(position, Vector3.up);
		primarySkill.ShootProjectile(position, new Vector3(0.3f, 1, 0));
		primarySkill.ShootProjectile(position, new Vector3(-0.3f, 1, 0));
	}
}

// ���� 4 ���Ⱑ ��� �����ϴ��� ������ �κ�
public class Level4Weapon : Weapon
{
	public void Activate(PrimarySkill primarySkill, CharacterManager characterManager)
	{
		// ���� 4 ������ Ư�� ����
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

// ���� 5 ���Ⱑ ��� �����ϴ��� ������ �κ�
public class Level5Weapon : Weapon
{
	public void Activate(PrimarySkill primarySkill, CharacterManager characterManager)
	{
		// ���� 5 ������ Ư�� ����
		Vector3 position = characterManager.Player.transform.position;

		for (int i = 0; i < 180; i += 10) // 360���� 10���� ������ �Ѿ� �߻�
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
		// �÷��̾� ��ġ�� ����
		Vector3 position = characterManager.Player.transform.position;

		for (float x = -18; x <= 18; x += 1) // x���� -18���� 18���� 1�� ������Ű�鼭 �ݺ��ϴ� �ݺ���
		{
			// +y�� �������� �߻� 
			Vector3 directionPositiveY = new Vector3(0, 1, 0); // �� �������� 
			Vector3 offsetPositiveY = new Vector3(x, 0, 0); // �� ��ġ���� �߻�
			primarySkill.ShootProjectile(position + offsetPositiveY, directionPositiveY); //  �Ѿ��� �߻��ϴ� ������ �����ϴ� primarySkill.ShootProjectile�� �����ͼ�
																						  //  ���� �÷��̾��� ��ġ���� y�� �������� x��ŭ ������ ��ġ�Դϴ�
																						  //  Vector3(0, 1, 0) -> y�� �������� 1�� ũ�⸦ ���µ�, �̰� primarySkill.ShootProjectile ���� �����ϴ� �ν��Ͻ�ȭ�� ���Ѽ�
																						  // Projectile�� �����ϰ��� ������ MoveSpeed�� ProjectileMoveSpeed ���� ������Ű�� �� ������ ǥ��ȭ ��Ű�鼭
																						  // �Ѿ��� ���ư��� �Ѵ�.
			// -y�� �������� �߻�
			Vector3 directionNegativeY = new Vector3(0, -1, 0); // �� �������� 
			Vector3 offsetNegativeY = new Vector3(x, 0, 0); // �� ��ġ���� �߻�
			primarySkill.ShootProjectile(position + offsetNegativeY, directionNegativeY); //  �Ѿ��� �߻��ϴ� ������ �����ϴ� primarySkill.ShootProjectile�� �����ͼ� 
																						  //  ���� �÷��̾��� ��ġ����  y�� �������� -x��ŭ ������ ��ġ�Դϴ�
																						  //  Vector3(0, -1, 0) -> y�� �������� -1�� ũ�⸦ ���µ�, �̰� primarySkill.ShootProjectile ���� �����ϴ� �ν��Ͻ�ȭ�� ���Ѽ�
																						  // Projectile�� �����ϰ��� ������ MoveSpeed�� ProjectileMoveSpeed ���� ������Ű�� �� ������ ǥ��ȭ ��Ű�鼭
																						  // �Ѿ��� ���ư��� �Ѵ�.

			// +x�� �������� �߻�
			Vector3 directionPositiveX = new Vector3(1, 0, 0); // �� �������� 
			Vector3 offsetPositiveX = new Vector3(0, x, 0); // �� ��ġ���� �߻�
			primarySkill.ShootProjectile(position + offsetPositiveX, directionPositiveX); //  �Ѿ��� �߻��ϴ� ������ �����ϴ� primarySkill.ShootProjectile�� �����ͼ� 
																						  //  ���� �÷��̾��� ��ġ����  x�� �������� x��ŭ ������ ��ġ�Դϴ�.
																						  //  Vector3(1, 0, 0); -> x�� �������� 1�� ũ�⸦ ���µ�, �̰� primarySkill.ShootProjectile ���� �����ϴ� �ν��Ͻ�ȭ�� ���Ѽ�
																						  // Projectile�� �����ϰ��� ������ MoveSpeed�� ProjectileMoveSpeed ���� ������Ű�� �� ������ ǥ��ȭ ��Ű�鼭
																						  // �Ѿ��� ���ư��� �Ѵ�.

			Vector3 directionNegativeX = new Vector3(-1, 0, 0); // �� �������� 
			Vector3 offsetNegativeX = new Vector3(0, x, 0); // �� ��ġ���� �߻�
			primarySkill.ShootProjectile(position + offsetNegativeX, directionNegativeX); //  �Ѿ��� �߻��ϴ� ������ �����ϴ� primarySkill.ShootProjectile�� �����ͼ� 
																						  //  ���� �÷��̾��� ��ġ����  x�� �������� -x��ŭ ������ ��ġ�Դϴ�.
																						  //  Vector3(-1, 0, 0); -> x�� �������� -1�� ũ�⸦ ���µ�, �̰� primarySkill.ShootProjectile ���� �����ϴ� �ν��Ͻ�ȭ�� ���Ѽ�
																						  // Projectile�� �����ϰ��� ������ MoveSpeed�� ProjectileMoveSpeed ���� ������Ű�� �� ������ ǥ��ȭ ��Ű�鼭
																						  // �Ѿ��� ���ư��� �Ѵ�.
		}
	}
}






using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

// MonoBehaviour���� ��ӹ��� GameInstance Ŭ������ ����
public class GameInstance : MonoBehaviour
{
	// GameInstance Ŭ������ ���� ���� ����, �� �ϳ��� �ν��Ͻ��� �����ϵ��� ��
	public static GameInstance instance;

	public bool BossBSummoned = false;


	// ���� ���� ������ �����ϴ� ������
	public float GameStartTime = 0f;            // ���� ���� �ð�
	public int Score = 0;                       // �÷��̾� ���ھ�
	public int CurrentStageLevel = 1;           // ���� �������� ����

	public int CurrentPlayerWeaponLevel = 0;   // �÷��̾� ���� ����
	public int CurrentPlayerHP = 3;             // �÷��̾� ���� ü��
	public float CurrentPlayerFuel = 100f;      // �÷��̾� ���� ����

	// Awake �޼���� ��ũ��Ʈ �ν��Ͻ��� �ε�� �� ȣ���
	private void Awake()
	{
		// GameInstance�� �ν��Ͻ��� �̹� �����ϴ��� Ȯ��
		if (instance == null)
		{
			// �ν��Ͻ��� ������ ���� �ν��Ͻ��� �����ϰ� �� ��ȯ�ÿ��� �����ǵ��� ��
			instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			// �̹� �ν��Ͻ��� �����ϸ� �ߺ��� �ν��Ͻ��� �ı�
			Destroy(gameObject);
		}

		// Unity�� Time Ŭ������ ����Ͽ� ���� ���� �ð��� ���
		GameStartTime = Time.time;
	}
}

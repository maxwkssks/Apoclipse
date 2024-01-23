using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���͸� �����ϴ� ���� �����ϴ� Ŭ���� 
public class EnemySpawnManager : BaseManager
{
	public GameObject[] Enemys; // ���ӿ� �����ϴ� ����
	public Transform[] EnemySpawnTransform; // ���� �����Ǵ� ��ġ
	public float CoolDownTime; // ���� ��Ÿ��
	public int MaxSpawnEnemyCount; // �ִ� ������ ���� �� 

	private int _spawnCount = 0; // ���� ��
	public int BossSpawnCount = 10; // ������ �����Ǳ� ���� ���� �� 

	private bool _bSpawnBoss = false; // ������ ���� �Ǿ����� ����

	public GameObject BossA; // ��ȯ�Ǵ� BossA

	// �����Լ��� ��� GameManager�� ������ ��.
	public override void Init(GameManager gameManager)
	{
		base.Init(gameManager); // ���� Ŭ�������� Init�� ���
		StartCoroutine(SpawnEnemy()); // ->	 �θ� Ŭ������ �����Լ��� ���� ��ȯ�ϴ� �ڷ�ƾ�� �߰��Ͽ� ���� ��ȯ��
	}

	IEnumerator SpawnEnemy()
	{
		// ������ ���� ��ȯ���� ���� ���� ����ؼ� �����
		while (!_bSpawnBoss)
		{
			// CoolDownTime ��ŭ ���
			yield return new WaitForSeconds(CoolDownTime);

			// �������� ������ 
			int spawnCount = Random.Range(1, EnemySpawnTransform.Length); 

			// EnemySpawnTransform.Length ũ���� ����Ʈ�� �����ϰ� ���� ������ �������
			List<int> availablePositions = new List<int>(EnemySpawnTransform.Length);

			for (int i = 0; i < EnemySpawnTransform.Length; i++)
			{
				availablePositions.Add(i);  
			}

			// ������ ������ŭ�� ���� ����
			for (int i = 0; i < spawnCount; i++)
			{
				
				// ���� �� ��ġ�� ������
				int randomEnemy = Random.Range(0, Enemys.Length);
			
				int randomPositionIndex = Random.Range(0, availablePositions.Count);
				
				int randomPosition = availablePositions[randomPositionIndex];
				

				// ���Ǿ��� ��ġ�� ����Ʈ���� �����ؼ� �ߺ������ �����ϴ� �ڵ�
				availablePositions.RemoveAt(randomPositionIndex);

				// ���� �ش� ��ġ�� ����
				Instantiate(Enemys[randomEnemy], EnemySpawnTransform[randomPosition].position, Quaternion.identity);
			}

			// ������ ��ü ���� ���� ����
			_spawnCount += spawnCount;

			// ������ ���� ��ȯ Ƚ���� �����ϸ� ������ ��ȯ��
			if (_spawnCount >= BossSpawnCount)
			{
				_bSpawnBoss = true;
				Instantiate(BossA, new Vector3(EnemySpawnTransform[1].position.x, EnemySpawnTransform[1].position.y + 1, 0f), Quaternion.identity); // �ƹ��� ȸ���� ������� ���� ����
			}
		}
	}
}
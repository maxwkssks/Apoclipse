using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


// ��ü������ ������ �����ϴ� Ŭ����
public class GameManager : MonoBehaviour
{
	static public GameManager Instance;  // Singleton ������ ����Ͽ� ���� �Ŵ����� ���� �ν��Ͻ��� �����ϴ� ���� ����

	public CharacterManager CharacterManager;  // ĳ���� ������ ����, Inspector���� ���� ����
	public MapManager MapManager;
	public EnemySpawnManager EnemySpawnManager;

	[HideInInspector] public bool bStageCleared = false;  // ���� ���������� Ŭ����Ǿ����� ���θ� ������ ������ ����

	private void Awake()  // ��ü ������ ���� ����Ǵ� Awake �޼���
	{
		if (Instance == null)  // �� �ϳ��� �����ϰԲ� Ȯ��
		{
			Instance = this;  // ��ü ������ instance�� �ڱ� �ڽ��� �־���
		}
		else
			Destroy(this.gameObject);  // �̹� �����ϴ� ��� ���� ��ü�� �ı��Ͽ� �ߺ� ���� ����
	}

	public PlayerCharacter GetPlayerCharacter()  // ���� �÷��̾� ĳ���͸� ��ȯ�ϴ� �޼���
	{
		return CharacterManager.Player.GetComponent<PlayerCharacter>();
	}

	void Start()
	{
		if (CharacterManager == null) { return; }  // ĳ���� �Ŵ����� �������� ���� ��� �ʱ�ȭ�� �������� �ʰ� ����
		CharacterManager.Init(this);  // ĳ���� �Ŵ����� �ʱ�ȭ

		MapManager.Init(this);
		EnemySpawnManager.Init(this);
	}

	public void GameStart()  // ������ �����ϴ� �޼���
	{
		SceneManager.LoadScene("Stage1");  // "Stage1" ���� �ε��Ͽ� ������ ����
	}
}

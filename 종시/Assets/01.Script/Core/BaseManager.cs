
// �پ��� �޴��� ����� �����ϴ� �ڵ�
using UnityEngine;

public class BaseManager : MonoBehaviour
{
	// GameManager Ÿ���� ��ȣ�� ��� ������ _gameManager�� �����մϴ�.
	// �� ������ �Ļ� Ŭ���������� ���� �����ϸ� ���� �Ŵ����� �ν��Ͻ��� �����մϴ�.
	protected GameManager _gameManager;

	// GameManager �Ӽ�(Property)�� �����մϴ�.
	// �� �Ӽ��� ���� _gameManager ������ ������ �� �ֽ��ϴ�.
	public GameManager GameManager { get { return _gameManager; } }

	// �Ļ� Ŭ�������� �����ǵ� ���� �޼����� Init�� �����մϴ�.
	// �� �޼���� ���� �Ŵ����� �ʱ�ȭ�� ����մϴ�.
	// ���� �Ŵ����� �ν��Ͻ��� �޾ƿ� _gameManager ������ �����մϴ�.
	public virtual void Init(GameManager gameManager)
	{
		_gameManager = gameManager;
	}
}

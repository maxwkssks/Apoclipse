
// 다양한 메니저 기능을 관리하는 코드
using UnityEngine;

public class BaseManager : MonoBehaviour
{
	// GameManager 타입의 보호된 멤버 변수인 _gameManager를 선언합니다.
	// 이 변수는 파생 클래스에서도 접근 가능하며 게임 매니저의 인스턴스를 저장합니다.
	protected GameManager _gameManager;

	// GameManager 속성(Property)을 정의합니다.
	// 이 속성을 통해 _gameManager 변수에 접근할 수 있습니다.
	public GameManager GameManager { get { return _gameManager; } }

	// 파생 클래스에서 재정의될 가상 메서드인 Init을 정의합니다.
	// 이 메서드는 게임 매니저의 초기화를 담당합니다.
	// 게임 매니저의 인스턴스를 받아와 _gameManager 변수에 저장합니다.
	public virtual void Init(GameManager gameManager)
	{
		_gameManager = gameManager;
	}
}

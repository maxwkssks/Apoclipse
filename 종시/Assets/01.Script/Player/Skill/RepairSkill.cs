using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BaseSkill에서 상속을 받아서 기본적인 스킬 정보를 얻고, Player를 Repair하는 것을 구현한 클래스
public class RepairSkill : BaseSkill
{
	// virtual로 썼던 BaseSkill을 재정의 하여 실제로 Repair하는 부분
	public override void Activate()
	{
		base.Activate();

		PlayerHPSystem system = _characterManager.Player.GetComponent<PlayerHPSystem>(); // HPSystem을 가져옴
		if (system != null) // null 체크
		{
			// 지금 system.Health를 높임
			system.Health+= 1;

			// 만약 MaxHealth 보다 높으면 지금 Health를 MaxHealth로 바꿈
			if (system.Health >= system.MaxHealth)
			{
				system.Health = system.MaxHealth;
			}
		}
	}
} 
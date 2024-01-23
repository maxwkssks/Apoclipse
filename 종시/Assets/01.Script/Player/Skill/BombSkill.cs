using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 몬스터를 펑 터뜨리는 스킬을 구현한 클래스
public class BombSkill : BaseSkill
{
	// 실제로 구현된 코드
	public override void Activate()
	{
		base.Activate();

		// 모든 Enemy 찾기
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject obj in enemies)
		{
			if (obj != null)
			{
			    if (obj.GetComponent<BossA>())
		      return;

			    Enemy enemy = obj.GetComponent<Enemy>();
			    if (enemy != null)
			    {
			        enemy.Dead();
			    }
			}
		}

	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� ���͸� �� �Ͷ߸��� ��ų�� ������ Ŭ����
public class BombSkill : BaseSkill
{
	// ������ ������ �ڵ�
	public override void Activate()
	{
		base.Activate();

		// ��� Enemy ã��
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
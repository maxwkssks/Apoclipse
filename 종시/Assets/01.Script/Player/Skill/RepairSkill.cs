using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// BaseSkill���� ����� �޾Ƽ� �⺻���� ��ų ������ ���, Player�� Repair�ϴ� ���� ������ Ŭ����
public class RepairSkill : BaseSkill
{
	// virtual�� ��� BaseSkill�� ������ �Ͽ� ������ Repair�ϴ� �κ�
	public override void Activate()
	{
		base.Activate();

		PlayerHPSystem system = _characterManager.Player.GetComponent<PlayerHPSystem>(); // HPSystem�� ������
		if (system != null) // null üũ
		{
			// ���� system.Health�� ����
			system.Health+= 1;

			// ���� MaxHealth ���� ������ ���� Health�� MaxHealth�� �ٲ�
			if (system.Health >= system.MaxHealth)
			{
				system.Health = system.MaxHealth;
			}
		}
	}
} 
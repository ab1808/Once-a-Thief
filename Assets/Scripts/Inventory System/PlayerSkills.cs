using System.Collections.Generic;
using UnityEngine;

namespace Inventory_System
{
	public class PlayerSkills
	{
		public enum SkillType
		{
			Immortality,
			WealthGrabber,
			HealthBoost,
			Shield,
			HeavyAttack
		}

		private List<SkillType> _unlockedSkillTypeList;

		public PlayerSkills()
		{
			_unlockedSkillTypeList = new List<SkillType>();
		}
		
		public void UnlockSkill(SkillType skillType)
		{
			if (!_unlockedSkillTypeList.Contains(skillType))
			{
				_unlockedSkillTypeList.Add(skillType);
			}
		}
		
		public bool IsSkillUnlocked(SkillType skillType)
		{
			return _unlockedSkillTypeList.Contains(skillType);
		}
		
		
	}
}

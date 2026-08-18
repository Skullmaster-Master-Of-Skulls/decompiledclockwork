using System;
using System.Collections.Generic;
using System.Data.Common.Utils;
using System.Data.Mapping.ViewGeneration.Structures;
using System.Text;

namespace System.Data.Mapping.ViewGeneration.Validation
{
	// Token: 0x0200027F RID: 639
	internal class ConditionComparer : IEqualityComparer<Dictionary<MemberPath, Set<Constant>>>
	{
		// Token: 0x06002695 RID: 9877 RVA: 0x00094478 File Offset: 0x00092678
		public bool Equals(Dictionary<MemberPath, Set<Constant>> one, Dictionary<MemberPath, Set<Constant>> two)
		{
			Set<MemberPath> set = new Set<MemberPath>(one.Keys, MemberPath.EqualityComparer);
			Set<MemberPath> equals = new Set<MemberPath>(two.Keys, MemberPath.EqualityComparer);
			if (!set.SetEquals(equals))
			{
				return false;
			}
			foreach (MemberPath key in set)
			{
				Set<Constant> set2 = one[key];
				Set<Constant> equals2 = two[key];
				if (!set2.SetEquals(equals2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002696 RID: 9878 RVA: 0x00094514 File Offset: 0x00092714
		public int GetHashCode(Dictionary<MemberPath, Set<Constant>> obj)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (MemberPath memberPath in obj.Keys)
			{
				stringBuilder.Append(memberPath.ToString());
			}
			return stringBuilder.ToString().GetHashCode();
		}
	}
}

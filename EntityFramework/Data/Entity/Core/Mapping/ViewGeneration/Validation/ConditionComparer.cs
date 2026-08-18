using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Validation
{
	// Token: 0x02000492 RID: 1170
	internal class ConditionComparer : IEqualityComparer<Dictionary<MemberPath, Set<Constant>>>
	{
		// Token: 0x06002B2F RID: 11055 RVA: 0x000D0CD0 File Offset: 0x000CEED0
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

		// Token: 0x06002B30 RID: 11056 RVA: 0x000D0D6C File Offset: 0x000CEF6C
		public int GetHashCode(Dictionary<MemberPath, Set<Constant>> obj)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (MemberPath value in obj.Keys)
			{
				stringBuilder.Append(value);
			}
			return stringBuilder.ToString().GetHashCode();
		}
	}
}

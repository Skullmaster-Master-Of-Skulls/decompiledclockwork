using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutoMapper
{
	// Token: 0x02000042 RID: 66
	public class AllMemberInfo : IGetTypeInfoMembers
	{
		// Token: 0x060002E6 RID: 742 RVA: 0x000075E5 File Offset: 0x000057E5
		public IEnumerable<MemberInfo> GetMemberInfos(TypeDetails typeInfo)
		{
			return (from m in AllMemberInfo.AllMembers(typeInfo)
			where this._predicates.All((Func<MemberInfo, bool> p) => p(m))
			select m).ToList<MemberInfo>();
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00007603 File Offset: 0x00005803
		public IGetTypeInfoMembers AddCondition(Func<MemberInfo, bool> predicate)
		{
			this._predicates.Add(predicate);
			return this;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00007612 File Offset: 0x00005812
		private static IEnumerable<MemberInfo> AllMembers(TypeDetails typeInfo)
		{
			return typeInfo.PublicReadAccessors.Concat(typeInfo.PublicNoArgMethods).Concat(typeInfo.PublicNoArgExtensionMethods).ToList<MemberInfo>();
		}

		// Token: 0x04000092 RID: 146
		private readonly IList<Func<MemberInfo, bool>> _predicates = new List<Func<MemberInfo, bool>>();
	}
}

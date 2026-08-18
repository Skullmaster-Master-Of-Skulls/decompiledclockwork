using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutoMapper
{
	// Token: 0x02000041 RID: 65
	public interface IGetTypeInfoMembers
	{
		// Token: 0x060002E4 RID: 740
		IEnumerable<MemberInfo> GetMemberInfos(TypeDetails typeInfo);

		// Token: 0x060002E5 RID: 741
		IGetTypeInfoMembers AddCondition(Func<MemberInfo, bool> predicate);
	}
}

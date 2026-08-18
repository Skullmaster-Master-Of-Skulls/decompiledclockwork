using System;
using System.Reflection;

namespace AutoMapper
{
	// Token: 0x02000052 RID: 82
	public interface ISourceToDestinationNameMapper
	{
		// Token: 0x0600032E RID: 814
		MemberInfo GetMatchingMemberInfo(IGetTypeInfoMembers getTypeInfoMembers, TypeDetails typeInfo, Type destType, string nameToSearch);
	}
}

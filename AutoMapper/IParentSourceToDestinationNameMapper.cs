using System;
using System.Collections.Generic;
using System.Reflection;

namespace AutoMapper
{
	// Token: 0x02000043 RID: 67
	public interface IParentSourceToDestinationNameMapper
	{
		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002EB RID: 747
		ICollection<ISourceToDestinationNameMapper> NamedMappers { get; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002EC RID: 748
		IGetTypeInfoMembers GetMembers { get; }

		// Token: 0x060002ED RID: 749
		MemberInfo GetMatchingMemberInfo(TypeDetails typeInfo, Type destType, string nameToSearch);
	}
}

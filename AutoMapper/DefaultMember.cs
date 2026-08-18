using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper.Internal;

namespace AutoMapper
{
	// Token: 0x02000051 RID: 81
	public class DefaultMember : IChildMemberConfiguration
	{
		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600032A RID: 810 RVA: 0x00007E73 File Offset: 0x00006073
		// (set) Token: 0x0600032B RID: 811 RVA: 0x00007E7B File Offset: 0x0000607B
		public IParentSourceToDestinationNameMapper NameMapper { get; set; }

		// Token: 0x0600032C RID: 812 RVA: 0x00007E84 File Offset: 0x00006084
		public bool MapDestinationPropertyToSource(IProfileConfiguration options, TypeDetails sourceType, Type destType, string nameToSearch, LinkedList<IValueResolver> resolvers, IMemberConfiguration parent = null)
		{
			if (string.IsNullOrEmpty(nameToSearch))
			{
				return true;
			}
			MemberInfo matchingMemberInfo = this.NameMapper.GetMatchingMemberInfo(sourceType, destType, nameToSearch);
			if (matchingMemberInfo != null)
			{
				resolvers.AddLast(matchingMemberInfo.ToMemberGetter());
			}
			return matchingMemberInfo != null;
		}
	}
}

using System;
using System.Collections.Generic;

namespace AutoMapper
{
	// Token: 0x0200004F RID: 79
	public interface IChildMemberConfiguration
	{
		// Token: 0x0600031F RID: 799
		bool MapDestinationPropertyToSource(IProfileConfiguration options, TypeDetails sourceType, Type destType, string nameToSearch, LinkedList<IValueResolver> resolvers, IMemberConfiguration parent);
	}
}

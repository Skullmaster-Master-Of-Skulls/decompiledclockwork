using System;
using System.Collections.Generic;

namespace AutoMapper
{
	// Token: 0x0200004D RID: 77
	public interface IMemberConfiguration
	{
		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000311 RID: 785
		IList<IChildMemberConfiguration> MemberMappers { get; }

		// Token: 0x06000312 RID: 786
		IMemberConfiguration AddMember<TMemberMapper>(Action<TMemberMapper> setupAction = null) where TMemberMapper : IChildMemberConfiguration, new();

		// Token: 0x06000313 RID: 787
		IMemberConfiguration AddName<TNameMapper>(Action<TNameMapper> setupAction = null) where TNameMapper : ISourceToDestinationNameMapper, new();

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000314 RID: 788
		// (set) Token: 0x06000315 RID: 789
		IParentSourceToDestinationNameMapper NameMapper { get; set; }

		// Token: 0x06000316 RID: 790
		bool MapDestinationPropertyToSource(IProfileConfiguration options, TypeDetails sourceType, Type destType, string nameToSearch, LinkedList<IValueResolver> resolvers);
	}
}

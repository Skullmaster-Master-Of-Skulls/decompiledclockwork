using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace AutoMapper
{
	// Token: 0x02000044 RID: 68
	public class ParentSourceToDestinationNameMapper : IParentSourceToDestinationNameMapper
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00007679 File Offset: 0x00005879
		public IGetTypeInfoMembers GetMembers { get; } = new AllMemberInfo();

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00007681 File Offset: 0x00005881
		public ICollection<ISourceToDestinationNameMapper> NamedMappers { get; } = new Collection<ISourceToDestinationNameMapper>
		{
			new DefaultName(),
			new SourceToDestinationNameMapperAttributesMember()
		};

		// Token: 0x060002F0 RID: 752 RVA: 0x0000768C File Offset: 0x0000588C
		public MemberInfo GetMatchingMemberInfo(TypeDetails typeInfo, Type destType, string nameToSearch)
		{
			MemberInfo memberInfo = null;
			foreach (ISourceToDestinationNameMapper sourceToDestinationNameMapper in this.NamedMappers)
			{
				memberInfo = sourceToDestinationNameMapper.GetMatchingMemberInfo(this.GetMembers, typeInfo, destType, nameToSearch);
				if (memberInfo != null)
				{
					break;
				}
			}
			return memberInfo;
		}
	}
}

using System;
using System.Linq;
using System.Reflection;

namespace AutoMapper
{
	// Token: 0x02000047 RID: 71
	public class CaseInsensitiveName : ISourceToDestinationNameMapper
	{
		// Token: 0x060002F7 RID: 759 RVA: 0x00007784 File Offset: 0x00005984
		public MemberInfo GetMatchingMemberInfo(IGetTypeInfoMembers getTypeInfoMembers, TypeDetails typeInfo, Type destType, string nameToSearch)
		{
			return getTypeInfoMembers.GetMemberInfos(typeInfo).FirstOrDefault((MemberInfo mi) => string.Compare(mi.Name, nameToSearch, StringComparison.OrdinalIgnoreCase) == 0);
		}
	}
}

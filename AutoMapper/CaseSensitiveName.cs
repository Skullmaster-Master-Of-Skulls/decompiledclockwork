using System;
using System.Linq;
using System.Reflection;

namespace AutoMapper
{
	// Token: 0x02000046 RID: 70
	public class CaseSensitiveName : ISourceToDestinationNameMapper
	{
		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000772C File Offset: 0x0000592C
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x00007734 File Offset: 0x00005934
		public bool MethodCaseSensitive { get; set; }

		// Token: 0x060002F5 RID: 757 RVA: 0x00007740 File Offset: 0x00005940
		public MemberInfo GetMatchingMemberInfo(IGetTypeInfoMembers getTypeInfoMembers, TypeDetails typeInfo, Type destType, string nameToSearch)
		{
			return getTypeInfoMembers.GetMemberInfos(typeInfo).FirstOrDefault(delegate(MemberInfo mi)
			{
				if (!typeof(ParameterInfo).IsAssignableFrom(destType) && this.MethodCaseSensitive)
				{
					return string.CompareOrdinal(mi.Name, nameToSearch) == 0;
				}
				return string.Compare(mi.Name, nameToSearch, StringComparison.OrdinalIgnoreCase) == 0;
			});
		}
	}
}

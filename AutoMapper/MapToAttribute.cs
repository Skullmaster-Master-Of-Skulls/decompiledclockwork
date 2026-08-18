using System;
using System.Reflection;

namespace AutoMapper
{
	// Token: 0x0200004C RID: 76
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
	public class MapToAttribute : SourceToDestinationMapperAttribute
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600030E RID: 782 RVA: 0x00007B56 File Offset: 0x00005D56
		public string MatchingName { get; }

		// Token: 0x0600030F RID: 783 RVA: 0x00007B5E File Offset: 0x00005D5E
		public MapToAttribute(string matchingName)
		{
			this.MatchingName = matchingName;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00007B6D File Offset: 0x00005D6D
		public override bool IsMatch(TypeDetails typeInfo, MemberInfo memberInfo, Type destType, string nameToSearch)
		{
			return string.Compare(this.MatchingName, nameToSearch, StringComparison.OrdinalIgnoreCase) == 0;
		}
	}
}

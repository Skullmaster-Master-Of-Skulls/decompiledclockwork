using System;
using System.Reflection;

namespace AutoMapper
{
	// Token: 0x0200004B RID: 75
	public abstract class SourceToDestinationMapperAttribute : Attribute
	{
		// Token: 0x0600030C RID: 780
		public abstract bool IsMatch(TypeDetails typeInfo, MemberInfo memberInfo, Type destType, string nameToSearch);
	}
}

using System;

namespace AutoMapper
{
	// Token: 0x0200002B RID: 43
	public interface ITypeMapFactory
	{
		// Token: 0x06000124 RID: 292
		TypeMap CreateTypeMap(Type sourceType, Type destinationType, IProfileConfiguration mappingOptions, MemberList memberList);
	}
}

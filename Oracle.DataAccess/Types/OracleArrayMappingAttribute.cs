using System;
using Oracle.DataAccess.Client;

namespace Oracle.DataAccess.Types
{
	// Token: 0x02000078 RID: 120
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
	public sealed class OracleArrayMappingAttribute : Attribute
	{
		// Token: 0x0600055F RID: 1375 RVA: 0x0003C3D0 File Offset: 0x0003B3D0
		static OracleArrayMappingAttribute()
		{
			if (!OracleInit.bSetDllDirectoryInvoked)
			{
				OracleInit.Initialize();
			}
		}
	}
}

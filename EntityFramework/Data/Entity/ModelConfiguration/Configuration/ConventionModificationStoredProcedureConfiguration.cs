using System;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020002B3 RID: 691
	public abstract class ConventionModificationStoredProcedureConfiguration
	{
		// Token: 0x0600183F RID: 6207 RVA: 0x00079D45 File Offset: 0x00077F45
		internal ConventionModificationStoredProcedureConfiguration()
		{
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x00079D58 File Offset: 0x00077F58
		internal ModificationStoredProcedureConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x04000879 RID: 2169
		private readonly ModificationStoredProcedureConfiguration _configuration = new ModificationStoredProcedureConfiguration();
	}
}

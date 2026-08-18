using System;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020002B8 RID: 696
	public abstract class ModificationStoredProcedureConfigurationBase
	{
		// Token: 0x0600186D RID: 6253 RVA: 0x0007A239 File Offset: 0x00078439
		internal ModificationStoredProcedureConfigurationBase()
		{
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x0600186E RID: 6254 RVA: 0x0007A24C File Offset: 0x0007844C
		internal ModificationStoredProcedureConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x0400087F RID: 2175
		private readonly ModificationStoredProcedureConfiguration _configuration = new ModificationStoredProcedureConfiguration();
	}
}

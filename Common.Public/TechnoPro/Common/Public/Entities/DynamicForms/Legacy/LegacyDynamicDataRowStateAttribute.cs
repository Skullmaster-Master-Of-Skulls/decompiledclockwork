using System;
using System.Data;

namespace TechnoPro.Common.Public.Entities.DynamicForms.Legacy
{
	// Token: 0x02000373 RID: 883
	public class LegacyDynamicDataRowStateAttribute : Attribute
	{
		// Token: 0x06001B60 RID: 7008 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public LegacyDynamicDataRowStateAttribute()
		{
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x0001F4B6 File Offset: 0x0001D6B6
		public LegacyDynamicDataRowStateAttribute(DataRowState drState)
		{
			this.DataRowState = drState;
		}

		// Token: 0x17000B5D RID: 2909
		// (get) Token: 0x06001B62 RID: 7010 RVA: 0x0001F4C8 File Offset: 0x0001D6C8
		// (set) Token: 0x06001B63 RID: 7011 RVA: 0x0001F4D0 File Offset: 0x0001D6D0
		public DataRowState DataRowState { get; set; }
	}
}

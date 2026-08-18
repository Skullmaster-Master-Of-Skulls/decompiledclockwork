using System;

namespace OracleInternal.ServiceObjects
{
	// Token: 0x020001C7 RID: 455
	internal class SqlData
	{
		// Token: 0x0600116A RID: 4458 RVA: 0x000C02F4 File Offset: 0x000BE4F4
		internal SqlData(string data, uint id, uint tag)
		{
			this.m_data = data;
			this.m_id = id;
			this.m_tag = tag;
		}

		// Token: 0x040013DF RID: 5087
		internal string m_data;

		// Token: 0x040013E0 RID: 5088
		internal uint m_id;

		// Token: 0x040013E1 RID: 5089
		internal uint m_tag;
	}
}

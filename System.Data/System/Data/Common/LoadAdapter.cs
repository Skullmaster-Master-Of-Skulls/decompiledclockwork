using System;

namespace System.Data.Common
{
	// Token: 0x02000118 RID: 280
	internal sealed class LoadAdapter : DataAdapter
	{
		// Token: 0x060011DB RID: 4571 RVA: 0x00235868 File Offset: 0x00234C68
		internal LoadAdapter()
		{
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x00235888 File Offset: 0x00234C88
		internal int FillFromReader(DataTable[] dataTables, IDataReader dataReader, int startRecord, int maxRecords)
		{
			return this.Fill(dataTables, dataReader, startRecord, maxRecords);
		}
	}
}

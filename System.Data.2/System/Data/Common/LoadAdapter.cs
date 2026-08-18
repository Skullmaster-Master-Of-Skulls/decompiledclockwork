using System;

namespace System.Data.Common
{
	// Token: 0x020002D6 RID: 726
	internal sealed class LoadAdapter : DataAdapter
	{
		// Token: 0x06002D1C RID: 11548 RVA: 0x00123090 File Offset: 0x00122490
		internal LoadAdapter()
		{
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x001230A4 File Offset: 0x001224A4
		internal int FillFromReader(DataTable[] dataTables, IDataReader dataReader, int startRecord, int maxRecords)
		{
			return this.Fill(dataTables, dataReader, startRecord, maxRecords);
		}
	}
}

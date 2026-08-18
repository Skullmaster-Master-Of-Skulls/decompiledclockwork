using System;

namespace System.Data.Design
{
	// Token: 0x02000262 RID: 610
	internal class DataSetNameService : SimpleNameService
	{
		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x0600176F RID: 5999 RVA: 0x00081820 File Offset: 0x0007FA20
		internal new static DataSetNameService DefaultInstance
		{
			get
			{
				if (DataSetNameService.defaultInstance == null)
				{
					DataSetNameService.defaultInstance = new DataSetNameService();
				}
				return DataSetNameService.defaultInstance;
			}
		}

		// Token: 0x06001770 RID: 6000 RVA: 0x00003937 File Offset: 0x00001B37
		public override void ValidateName(string name)
		{
		}

		// Token: 0x04000BFD RID: 3069
		private static DataSetNameService defaultInstance;
	}
}

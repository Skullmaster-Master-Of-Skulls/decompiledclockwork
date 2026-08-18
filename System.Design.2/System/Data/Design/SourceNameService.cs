using System;

namespace System.Data.Design
{
	// Token: 0x02000263 RID: 611
	internal class SourceNameService : SimpleNameService
	{
		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001772 RID: 6002 RVA: 0x00081840 File Offset: 0x0007FA40
		internal new static SourceNameService DefaultInstance
		{
			get
			{
				if (SourceNameService.defaultInstance == null)
				{
					SourceNameService.defaultInstance = new SourceNameService();
				}
				return SourceNameService.defaultInstance;
			}
		}

		// Token: 0x04000BFE RID: 3070
		private static SourceNameService defaultInstance;
	}
}

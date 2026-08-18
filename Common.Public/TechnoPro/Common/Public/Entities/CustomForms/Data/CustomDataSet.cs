using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.CustomForms.Data.Context;

namespace TechnoPro.Common.Public.Entities.CustomForms.Data
{
	// Token: 0x02000423 RID: 1059
	public class CustomDataSet
	{
		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06002035 RID: 8245 RVA: 0x000247E4 File Offset: 0x000229E4
		// (set) Token: 0x06002036 RID: 8246 RVA: 0x000247EC File Offset: 0x000229EC
		public IList<CustomDataHolderCollection> Data { get; set; }

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x06002037 RID: 8247 RVA: 0x000247F5 File Offset: 0x000229F5
		// (set) Token: 0x06002038 RID: 8248 RVA: 0x000247FD File Offset: 0x000229FD
		public CustomDataContext Context { get; set; }
	}
}

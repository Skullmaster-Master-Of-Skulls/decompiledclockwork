using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000E02 RID: 3586
	[Serializable]
	internal abstract class PivotGridModelRowBase
	{
		// Token: 0x17002A10 RID: 10768
		// (get) Token: 0x06008507 RID: 34055 RVA: 0x001E6305 File Offset: 0x001E4505
		// (set) Token: 0x06008508 RID: 34056 RVA: 0x001E630D File Offset: 0x001E450D
		public List<PivotGridModelCellBase> Cells { get; set; }
	}
}

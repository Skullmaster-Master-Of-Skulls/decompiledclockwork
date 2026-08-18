using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000E03 RID: 3587
	[Serializable]
	internal class PivotGridModelRow : PivotGridModelRowBase
	{
		// Token: 0x0600850A RID: 34058 RVA: 0x001E631E File Offset: 0x001E451E
		public PivotGridModelRow()
		{
			base.Cells = new List<PivotGridModelCellBase>();
		}
	}
}

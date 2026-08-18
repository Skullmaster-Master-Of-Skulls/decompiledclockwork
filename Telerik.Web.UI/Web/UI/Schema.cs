using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02000BD7 RID: 3031
	public class Schema : List<DataModel>
	{
		// Token: 0x060073B0 RID: 29616 RVA: 0x001B04B7 File Offset: 0x001AE6B7
		public bool HasModels()
		{
			return base.Count > 0;
		}
	}
}

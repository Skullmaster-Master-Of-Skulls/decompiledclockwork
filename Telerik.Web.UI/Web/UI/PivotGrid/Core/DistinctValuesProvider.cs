using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x020006D4 RID: 1748
	internal abstract class DistinctValuesProvider
	{
		// Token: 0x140000AB RID: 171
		// (add) Token: 0x06003EAB RID: 16043 RVA: 0x000C7E58 File Offset: 0x000C6058
		// (remove) Token: 0x06003EAC RID: 16044 RVA: 0x000C7E90 File Offset: 0x000C6090
		public event EventHandler<EventArgs> Updated;

		// Token: 0x1700147A RID: 5242
		// (get) Token: 0x06003EAD RID: 16045
		public abstract IEnumerable<object> DisctinctValues { get; }

		// Token: 0x06003EAE RID: 16046
		public abstract void Refresh();

		// Token: 0x06003EAF RID: 16047 RVA: 0x000C7EC5 File Offset: 0x000C60C5
		protected void OnUpdated()
		{
			if (this.Updated != null)
			{
				this.Updated(this, EventArgs.Empty);
			}
		}
	}
}

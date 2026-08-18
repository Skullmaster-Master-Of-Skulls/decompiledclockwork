using System;
using System.ComponentModel;

namespace Telerik.Web.UI.PivotGrid.Core.Fields
{
	// Token: 0x020006B9 RID: 1721
	public class ContainerNodeEventArgs : CancelEventArgs
	{
		// Token: 0x06003DE7 RID: 15847 RVA: 0x000C7329 File Offset: 0x000C5529
		public ContainerNodeEventArgs(ContainerNode containerNode, IPivotFieldInfo info)
		{
			this.ContainerNode = containerNode;
			this.ContainerInfo = info;
		}

		// Token: 0x17001441 RID: 5185
		// (get) Token: 0x06003DE8 RID: 15848 RVA: 0x000C733F File Offset: 0x000C553F
		// (set) Token: 0x06003DE9 RID: 15849 RVA: 0x000C7347 File Offset: 0x000C5547
		public ContainerNode ContainerNode { get; set; }

		// Token: 0x17001442 RID: 5186
		// (get) Token: 0x06003DEA RID: 15850 RVA: 0x000C7350 File Offset: 0x000C5550
		// (set) Token: 0x06003DEB RID: 15851 RVA: 0x000C7358 File Offset: 0x000C5558
		public IPivotFieldInfo ContainerInfo { get; private set; }
	}
}

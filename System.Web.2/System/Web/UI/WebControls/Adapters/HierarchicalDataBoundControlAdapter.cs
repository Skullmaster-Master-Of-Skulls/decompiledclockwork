using System;

namespace System.Web.UI.WebControls.Adapters
{
	// Token: 0x020005C0 RID: 1472
	public class HierarchicalDataBoundControlAdapter : WebControlAdapter
	{
		// Token: 0x17001602 RID: 5634
		// (get) Token: 0x06004AA2 RID: 19106 RVA: 0x000F8015 File Offset: 0x000F6215
		protected new HierarchicalDataBoundControl Control
		{
			get
			{
				return (HierarchicalDataBoundControl)base.Control;
			}
		}

		// Token: 0x06004AA3 RID: 19107 RVA: 0x000F8022 File Offset: 0x000F6222
		protected internal virtual void PerformDataBinding()
		{
			this.Control.PerformDataBinding();
		}
	}
}

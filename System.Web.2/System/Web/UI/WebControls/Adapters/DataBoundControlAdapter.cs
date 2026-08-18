using System;
using System.Collections;

namespace System.Web.UI.WebControls.Adapters
{
	// Token: 0x020005BE RID: 1470
	public class DataBoundControlAdapter : WebControlAdapter
	{
		// Token: 0x17001601 RID: 5633
		// (get) Token: 0x06004A9D RID: 19101 RVA: 0x000F7FD6 File Offset: 0x000F61D6
		protected new DataBoundControl Control
		{
			get
			{
				return (DataBoundControl)base.Control;
			}
		}

		// Token: 0x06004A9E RID: 19102 RVA: 0x000F7FE3 File Offset: 0x000F61E3
		protected internal virtual void PerformDataBinding(IEnumerable data)
		{
			this.Control.PerformDataBinding(data);
		}
	}
}

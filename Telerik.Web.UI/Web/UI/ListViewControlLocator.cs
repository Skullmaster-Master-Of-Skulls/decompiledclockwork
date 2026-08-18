using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020019B1 RID: 6577
	internal class ListViewControlLocator
	{
		// Token: 0x0600FE49 RID: 65097 RVA: 0x00392089 File Offset: 0x00390289
		public virtual Control RetriveFromContainer(Control container, string controlId)
		{
			if (string.IsNullOrEmpty(controlId))
			{
				return null;
			}
			return container.FindControl(controlId);
		}
	}
}

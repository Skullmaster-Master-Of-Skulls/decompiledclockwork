using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000204 RID: 516
	internal class DataFormControlLocator
	{
		// Token: 0x06001338 RID: 4920 RVA: 0x00044103 File Offset: 0x00042303
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

using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000144 RID: 324
	internal class DataSourceControlHelper
	{
		// Token: 0x06000CF0 RID: 3312 RVA: 0x0002E064 File Offset: 0x0002C264
		internal static Control FindControl(Control control, string controlID)
		{
			Control control2 = control;
			Control control3 = null;
			if (control == control.Page)
			{
				return control.FindControl(controlID);
			}
			while (control3 == null && control2 != control.Page)
			{
				control2 = control2.NamingContainer;
				if (control2 == null)
				{
					string name = control.GetType().Name;
					string id = control.ID;
					throw new Exception("Cannot find DataSourceControl with ID '" + controlID + "'");
				}
				control3 = control2.FindControl(controlID);
			}
			return control3;
		}
	}
}

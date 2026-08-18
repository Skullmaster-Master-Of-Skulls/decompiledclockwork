using System;
using System.Web.UI;

namespace Telerik.Web.UI.MultiSelect
{
	// Token: 0x02000604 RID: 1540
	public class CascadeFromControlHelper
	{
		// Token: 0x06003788 RID: 14216 RVA: 0x000B7848 File Offset: 0x000B5A48
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
					throw new Exception("Cannot find MultiSelect with ID '" + controlID + "'");
				}
				control3 = control2.FindControl(controlID);
			}
			return control3;
		}
	}
}

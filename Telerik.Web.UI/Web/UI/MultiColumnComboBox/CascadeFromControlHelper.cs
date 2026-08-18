using System;
using System.Web.UI;

namespace Telerik.Web.UI.MultiColumnComboBox
{
	// Token: 0x020005E7 RID: 1511
	public class CascadeFromControlHelper
	{
		// Token: 0x060036B9 RID: 14009 RVA: 0x000B5644 File Offset: 0x000B3844
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
					throw new Exception("Cannot find MultiColumnComboBox with ID '" + controlID + "'");
				}
				control3 = control2.FindControl(controlID);
			}
			return control3;
		}
	}
}

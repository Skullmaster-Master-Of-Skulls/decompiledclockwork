using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Telerik.Web
{
	// Token: 0x0200101E RID: 4126
	internal static class ChildControlHelper
	{
		// Token: 0x0600A2F2 RID: 41714 RVA: 0x002443AC File Offset: 0x002425AC
		public static string GetChildElementId(RadWebControl parentControl, string id)
		{
			return ChildControlHelper.FormatId(parentControl.ClientID, id);
		}

		// Token: 0x0600A2F3 RID: 41715 RVA: 0x002443BA File Offset: 0x002425BA
		public static string GetChildControlId(RadWebControl parentControl, string id)
		{
			return ChildControlHelper.FormatId(parentControl.ID, id);
		}

		// Token: 0x0600A2F4 RID: 41716 RVA: 0x002443C8 File Offset: 0x002425C8
		private static string FormatId(string parentId, string id)
		{
			return string.Format("{0}_{1}", parentId, id);
		}

		// Token: 0x0600A2F5 RID: 41717 RVA: 0x002443D8 File Offset: 0x002425D8
		internal static Control FindControlRecursive(Control searcher, string ID, List<string> controlsToEscape = null)
		{
			Control control = null;
			if (searcher.NamingContainer != null)
			{
				control = searcher.NamingContainer.FindControl(ID);
			}
			if (control != null)
			{
				return control;
			}
			Control control2;
			if (searcher.Page.Master != null)
			{
				control2 = searcher.Page.Master;
			}
			else
			{
				control2 = searcher.Page;
			}
			Control control3 = control2.FindControl(ID);
			if (control3 != null)
			{
				return control3;
			}
			control3 = searcher.Page.FindControl(ID);
			if (control3 != null)
			{
				return control3;
			}
			if (searcher.UniqueID == ID || searcher.ClientID == ID)
			{
				return searcher;
			}
			return ChildControlHelper.FindControlRecursive(ID, control2, controlsToEscape);
		}

		// Token: 0x0600A2F6 RID: 41718 RVA: 0x00244468 File Offset: 0x00242668
		private static Control FindControlRecursive(string ID, Control root, List<string> controlsToEscape)
		{
			Control control = null;
			if (root is DataBoundControl && !root.Visible)
			{
				return control;
			}
			foreach (object obj in root.Controls)
			{
				Control control2 = (Control)obj;
				if (controlsToEscape == null || !controlsToEscape.Contains(control2.ID))
				{
					if (control2 is INamingContainer && control2.FindControl(ID) != null)
					{
						control = control2.FindControl(ID);
						break;
					}
					if (control2.HasControls())
					{
						control = ChildControlHelper.FindControlRecursive(ID, control2, controlsToEscape);
						if (control != null && (control.UniqueID == ID || control.ID == ID))
						{
							break;
						}
					}
				}
			}
			return control;
		}

		// Token: 0x0600A2F7 RID: 41719 RVA: 0x0024452C File Offset: 0x0024272C
		internal static List<Control> GetAllControls(List<Control> controls, Type t, Control parent)
		{
			foreach (object obj in parent.Controls)
			{
				Control control = (Control)obj;
				if (control.GetType() == t && control.Visible)
				{
					controls.Add(control);
				}
				if (control.HasControls() && control.Visible)
				{
					controls = ChildControlHelper.GetAllControls(controls, t, control);
				}
			}
			return controls;
		}
	}
}

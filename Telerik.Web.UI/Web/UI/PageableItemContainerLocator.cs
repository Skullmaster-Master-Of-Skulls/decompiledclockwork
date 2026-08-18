using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001965 RID: 6501
	public class PageableItemContainerLocator
	{
		// Token: 0x0600FBA2 RID: 64418 RVA: 0x0038B5AC File Offset: 0x003897AC
		protected void FindControlInContainer(Control control, Predicate<Control> criteria)
		{
			Control control2 = control;
			while (criteria(control2) && control2 != control.Page)
			{
				control2 = control2.NamingContainer;
				if (control2 == null)
				{
					return;
				}
			}
		}

		// Token: 0x0600FBA3 RID: 64419 RVA: 0x0038B600 File Offset: 0x00389800
		protected Control FindControlById(Control control, string controlId)
		{
			Control foundControl = null;
			this.FindControlInContainer(control, delegate(Control current)
			{
				foundControl = current.FindControl(controlId);
				return foundControl == null;
			});
			return foundControl;
		}

		// Token: 0x0600FBA4 RID: 64420 RVA: 0x0038B670 File Offset: 0x00389870
		protected Control FindControlByType(Control control)
		{
			Control foundControl = null;
			this.FindControlInContainer(control, delegate(Control current)
			{
				if (current is IRadPageableItemContainer)
				{
					foundControl = current;
				}
				else if (current is IPageableItemContainer)
				{
					foundControl = current;
				}
				return foundControl == null;
			});
			return foundControl;
		}

		// Token: 0x0600FBA5 RID: 64421 RVA: 0x0038B6A4 File Offset: 0x003898A4
		public virtual IRadPageableItemContainer RetrievePageableItemContainer(Control control, string controlId)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control", "Invalid first function argument.");
			}
			Control control2;
			if (!string.IsNullOrEmpty(controlId))
			{
				control2 = this.FindControlById(control, controlId);
			}
			else
			{
				control2 = this.FindControlByType(control);
			}
			if (control2 == null)
			{
				return null;
			}
			return PageableItemContainerHelper.WrapContainer(control2);
		}
	}
}

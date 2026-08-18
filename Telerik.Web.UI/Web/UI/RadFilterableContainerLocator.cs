using System;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020018C1 RID: 6337
	public class RadFilterableContainerLocator
	{
		// Token: 0x0600F565 RID: 62821 RVA: 0x0037BD20 File Offset: 0x00379F20
		public virtual IRadFilterableContainer RetrieveFilterableContainer(Control control, string controlId, List<string> controlsToEscape)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control", "Invalid first function argument.");
			}
			IRadFilterableContainer result;
			if (!string.IsNullOrEmpty(controlId))
			{
				result = (ChildControlHelper.FindControlRecursive(control, controlId, controlsToEscape) as IRadFilterableContainer);
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600F566 RID: 62822 RVA: 0x0037BD5C File Offset: 0x00379F5C
		public virtual IDataSource RetrieveDataSourceControl(Control control, string controlId, List<string> controlsToEscape)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control", "Invalid first function argument.");
			}
			IDataSource result;
			if (!string.IsNullOrEmpty(controlId))
			{
				result = (ChildControlHelper.FindControlRecursive(control, controlId, controlsToEscape) as IDataSource);
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}

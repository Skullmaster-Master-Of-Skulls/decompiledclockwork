using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit.Design
{
	// Token: 0x0200008F RID: 143
	public class TypedControlIDConverter<T> : ControlIDConverter
	{
		// Token: 0x060004A5 RID: 1189 RVA: 0x0000CCFE File Offset: 0x0000AEFE
		protected override bool FilterControl(Control control)
		{
			return typeof(T).IsInstanceOfType(control);
		}
	}
}

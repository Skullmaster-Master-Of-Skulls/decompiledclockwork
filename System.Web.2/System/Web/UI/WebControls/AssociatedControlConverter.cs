using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000375 RID: 885
	public class AssociatedControlConverter : ControlIDConverter
	{
		// Token: 0x060028DC RID: 10460 RVA: 0x00084396 File Offset: 0x00082596
		protected override bool FilterControl(Control control)
		{
			return control is WebControl;
		}
	}
}

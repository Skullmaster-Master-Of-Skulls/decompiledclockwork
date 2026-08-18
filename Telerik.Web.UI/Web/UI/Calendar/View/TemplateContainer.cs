using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Calendar.View
{
	// Token: 0x0200101B RID: 4123
	[ToolboxItem(false)]
	public class TemplateContainer : Control, INamingContainer
	{
		// Token: 0x0600A2C8 RID: 41672 RVA: 0x00243E11 File Offset: 0x00242011
		public TemplateContainer()
		{
		}

		// Token: 0x0600A2C9 RID: 41673 RVA: 0x00243E19 File Offset: 0x00242019
		public TemplateContainer(RadCalendarDay parent)
		{
			this._parent = parent;
		}

		// Token: 0x04002D4A RID: 11594
		private RadCalendarDay _parent;
	}
}

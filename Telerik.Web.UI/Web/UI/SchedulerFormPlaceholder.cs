using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001311 RID: 4881
	internal class SchedulerFormPlaceholder : Control
	{
		// Token: 0x0600CC4E RID: 52302 RVA: 0x002D8C3D File Offset: 0x002D6E3D
		public SchedulerFormPlaceholder(SchedulerFormContainer formContainer)
		{
			this._formContainer = formContainer;
		}

		// Token: 0x0600CC4F RID: 52303 RVA: 0x002D8C4C File Offset: 0x002D6E4C
		protected override void Render(HtmlTextWriter writer)
		{
			this._formContainer.RenderControl(writer);
			this._formContainer.Visible = false;
		}

		// Token: 0x04003587 RID: 13703
		private SchedulerFormContainer _formContainer;
	}
}

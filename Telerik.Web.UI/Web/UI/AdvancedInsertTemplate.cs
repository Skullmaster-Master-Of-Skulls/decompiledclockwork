using System;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x0200131E RID: 4894
	internal class AdvancedInsertTemplate : AdvancedTemplate
	{
		// Token: 0x0600CC7C RID: 52348 RVA: 0x002D9017 File Offset: 0x002D7217
		public AdvancedInsertTemplate(RadScheduler owner, string runtimeSkin) : base(owner, runtimeSkin)
		{
		}

		// Token: 0x0600CC7D RID: 52349 RVA: 0x002D9021 File Offset: 0x002D7221
		protected override void CreateButtons()
		{
			base.View.CreateInsertButtons();
			base.Renderer.CreateInsertButtons();
		}

		// Token: 0x0600CC7E RID: 52350 RVA: 0x002D9039 File Offset: 0x002D7239
		protected override void CreateChildControls(Control container)
		{
			if (base.View.ResetExceptions != null)
			{
				base.View.ResetExceptions.Visible = false;
			}
			base.Renderer.CreateTitle(base.Owner.Localization.AdvancedNewAppointment);
		}

		// Token: 0x0600CC7F RID: 52351 RVA: 0x002D9074 File Offset: 0x002D7274
		internal override bool IncludeResource(Resource res)
		{
			return res.Available;
		}
	}
}

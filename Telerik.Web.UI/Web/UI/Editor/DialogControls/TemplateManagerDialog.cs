using System;
using System.ComponentModel;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x0200106B RID: 4203
	[ToolboxItem(false)]
	public class TemplateManagerDialog : UserControlFileBrowser
	{
		// Token: 0x17003677 RID: 13943
		// (get) Token: 0x0600A991 RID: 43409 RVA: 0x0024D62D File Offset: 0x0024B82D
		public override string ControlName
		{
			get
			{
				return "Template";
			}
		}

		// Token: 0x17003678 RID: 13944
		// (get) Token: 0x0600A992 RID: 43410 RVA: 0x0024D634 File Offset: 0x0024B834
		protected override string[] DefaultSearchPatterns
		{
			get
			{
				return new string[]
				{
					"*.html",
					"*.htm"
				};
			}
		}

		// Token: 0x0600A993 RID: 43411 RVA: 0x0024D65C File Offset: 0x0024B85C
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			RadToolBar radToolBar = (RadToolBar)base.FindControlRecursive("EmptyToolbar");
			radToolBar.RenderMode = this.Parameters.RenderMode;
			RadTabStrip radTabStrip = (RadTabStrip)base.FindControlRecursive("templateTabStrip");
			radTabStrip.RenderMode = this.Parameters.RenderMode;
			if (radTabStrip != null && radTabStrip.Tabs.Count >= 1)
			{
				radTabStrip.Tabs[0].Text = this.Localization.GetString(radTabStrip.Tabs[0].Value);
				radTabStrip.Tabs[0].ToolTip = this.Localization.GetString(radTabStrip.Tabs[0].Value);
			}
		}
	}
}

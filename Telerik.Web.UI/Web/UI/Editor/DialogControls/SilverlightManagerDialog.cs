using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x020019E6 RID: 6630
	[ToolboxItem(false)]
	public class SilverlightManagerDialog : UserControlFileBrowser
	{
		// Token: 0x17004D76 RID: 19830
		// (get) Token: 0x0601009D RID: 65693 RVA: 0x0039944E File Offset: 0x0039764E
		public override string ControlName
		{
			get
			{
				return "Silverlight";
			}
		}

		// Token: 0x17004D77 RID: 19831
		// (get) Token: 0x0601009E RID: 65694 RVA: 0x00399458 File Offset: 0x00397658
		protected override string[] DefaultSearchPatterns
		{
			get
			{
				return new string[]
				{
					"*.xap"
				};
			}
		}

		// Token: 0x0601009F RID: 65695 RVA: 0x00399478 File Offset: 0x00397678
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			RadToolBar radToolBar = (RadToolBar)base.FindControlRecursive("EmptyToolbar");
			if (radToolBar != null)
			{
				radToolBar.RenderMode = this.Parameters.RenderMode;
			}
			RadToolBar radToolBar2 = (RadToolBar)base.FindControlRecursive("SilverlightPreviewToolBar");
			if (radToolBar2 != null)
			{
				radToolBar2.RenderMode = this.Parameters.RenderMode;
				if (base.DialogParameters["IsSkinTouch"] != null && (bool)base.DialogParameters["IsSkinTouch"])
				{
					radToolBar2.Height = Unit.Parse("44px", CultureInfo.InvariantCulture);
				}
				if (radToolBar2.Items.Count >= 2)
				{
					radToolBar2.Items[0].ToolTip = this.Localization.GetString(radToolBar2.Items[0].Value);
					radToolBar2.Items[0].Text = this.Localization.GetString(radToolBar2.Items[0].Value);
					radToolBar2.Items[1].ToolTip = this.Localization.GetString(radToolBar2.Items[1].Value);
					radToolBar2.Items[1].Text = this.Localization.GetString(radToolBar2.Items[1].Value);
				}
			}
			RadTabStrip radTabStrip = (RadTabStrip)base.FindControlRecursive("silverlightTabStrip");
			if (radTabStrip != null)
			{
				radTabStrip.RenderMode = this.Parameters.RenderMode;
				if (radTabStrip.Tabs.Count >= 2)
				{
					radTabStrip.Tabs[0].Text = this.Localization.GetString(radTabStrip.Tabs[0].Value);
					radTabStrip.Tabs[0].ToolTip = this.Localization.GetString(radTabStrip.Tabs[0].Value);
					radTabStrip.Tabs[1].Text = this.Localization.GetString(radTabStrip.Tabs[1].Value);
					radTabStrip.Tabs[1].ToolTip = this.Localization.GetString(radTabStrip.Tabs[1].Value);
				}
			}
		}
	}
}

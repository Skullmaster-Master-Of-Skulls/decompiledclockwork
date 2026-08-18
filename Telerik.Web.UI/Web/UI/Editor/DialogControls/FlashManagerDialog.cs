using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x02001066 RID: 4198
	[ToolboxItem(false)]
	public class FlashManagerDialog : UserControlFileBrowser
	{
		// Token: 0x17003665 RID: 13925
		// (get) Token: 0x0600A969 RID: 43369 RVA: 0x0024C881 File Offset: 0x0024AA81
		public override string ControlName
		{
			get
			{
				return "Flash";
			}
		}

		// Token: 0x17003666 RID: 13926
		// (get) Token: 0x0600A96A RID: 43370 RVA: 0x0024C888 File Offset: 0x0024AA88
		protected override string[] DefaultSearchPatterns
		{
			get
			{
				return new string[]
				{
					"*.swf"
				};
			}
		}

		// Token: 0x0600A96B RID: 43371 RVA: 0x0024C8A8 File Offset: 0x0024AAA8
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			RadToolBar radToolBar = (RadToolBar)base.FindControlRecursive("EmptyToolbar");
			radToolBar.RenderMode = this.Parameters.RenderMode;
			if (radToolBar != null)
			{
				if (base.DialogParameters["IsSkinTouch"] != null && (bool)base.DialogParameters["IsSkinTouch"])
				{
					radToolBar.Height = Unit.Parse("44px", CultureInfo.InvariantCulture);
				}
				if (radToolBar.Items.Count >= 2)
				{
					radToolBar.Items[0].ToolTip = this.Localization.GetString(radToolBar.Items[0].Value);
					radToolBar.Items[0].Text = this.Localization.GetString(radToolBar.Items[0].Value);
					radToolBar.Items[1].ToolTip = this.Localization.GetString(radToolBar.Items[1].Value);
					radToolBar.Items[1].Text = this.Localization.GetString(radToolBar.Items[1].Value);
				}
			}
			RadTabStrip radTabStrip = (RadTabStrip)base.FindControlRecursive("flashTabStrip");
			radTabStrip.RenderMode = this.Parameters.RenderMode;
			if (radTabStrip != null && radTabStrip.Tabs.Count >= 2)
			{
				radTabStrip.Tabs[0].Text = this.Localization.GetString(radTabStrip.Tabs[0].Value);
				radTabStrip.Tabs[0].ToolTip = this.Localization.GetString(radTabStrip.Tabs[0].Value);
				radTabStrip.Tabs[1].Text = this.Localization.GetString(radTabStrip.Tabs[1].Value);
				radTabStrip.Tabs[1].ToolTip = this.Localization.GetString(radTabStrip.Tabs[1].Value);
			}
		}
	}
}

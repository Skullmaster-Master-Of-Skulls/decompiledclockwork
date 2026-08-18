using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x0200106A RID: 4202
	[ToolboxItem(false)]
	public class MediaManagerDialog : UserControlFileBrowser
	{
		// Token: 0x17003675 RID: 13941
		// (get) Token: 0x0600A98D RID: 43405 RVA: 0x0024D348 File Offset: 0x0024B548
		public override string ControlName
		{
			get
			{
				return "Media";
			}
		}

		// Token: 0x17003676 RID: 13942
		// (get) Token: 0x0600A98E RID: 43406 RVA: 0x0024D350 File Offset: 0x0024B550
		protected override string[] DefaultSearchPatterns
		{
			get
			{
				return new string[]
				{
					"*.wma",
					"*.wmv",
					"*.avi",
					"*.wav",
					"*.mpeg",
					"*.mpg",
					"*.mpe",
					"*.mp3",
					"*.m3u",
					"*.mid",
					"*.midi",
					"*.snd",
					"*.mkv"
				};
			}
		}

		// Token: 0x0600A98F RID: 43407 RVA: 0x0024D3D4 File Offset: 0x0024B5D4
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			RadToolBar radToolBar = (RadToolBar)base.FindControlRecursive("EmptyToolbar");
			if (radToolBar != null)
			{
				radToolBar.RenderMode = this.Parameters.RenderMode;
			}
			RadToolBar radToolBar2 = (RadToolBar)base.FindControlRecursive("MediaPreviewToolBar");
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
			RadTabStrip radTabStrip = (RadTabStrip)base.FindControlRecursive("mediaTabStrip");
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

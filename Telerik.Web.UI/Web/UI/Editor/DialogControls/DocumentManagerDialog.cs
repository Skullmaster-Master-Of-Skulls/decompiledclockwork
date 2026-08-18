using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x02001064 RID: 4196
	[ToolboxItem(false)]
	public class DocumentManagerDialog : UserControlFileBrowser
	{
		// Token: 0x1700365A RID: 13914
		// (get) Token: 0x0600A94E RID: 43342 RVA: 0x0024C590 File Offset: 0x0024A790
		protected override string[] DefaultSearchPatterns
		{
			get
			{
				return new string[]
				{
					"*.doc",
					"*.txt",
					"*.docx",
					"*.xls",
					"*.xlsx",
					"*.pdf"
				};
			}
		}

		// Token: 0x1700365B RID: 13915
		// (get) Token: 0x0600A94F RID: 43343 RVA: 0x0024C5D5 File Offset: 0x0024A7D5
		public override string ControlName
		{
			get
			{
				return "Document";
			}
		}

		// Token: 0x0600A950 RID: 43344 RVA: 0x0024C5DC File Offset: 0x0024A7DC
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			RadToolBar radToolBar = (RadToolBar)base.FindControlRecursive("EmptyToolbar");
			radToolBar.RenderMode = this.Parameters.RenderMode;
			if (radToolBar != null && base.DialogParameters["IsSkinTouch"] != null && (bool)base.DialogParameters["IsSkinTouch"])
			{
				radToolBar.Height = Unit.Parse("44px", CultureInfo.InvariantCulture);
			}
			RadTabStrip radTabStrip = (RadTabStrip)base.FindControlRecursive("LinkManagerTab");
			if (radTabStrip != null)
			{
				radTabStrip.RenderMode = this.Parameters.RenderMode;
			}
		}
	}
}

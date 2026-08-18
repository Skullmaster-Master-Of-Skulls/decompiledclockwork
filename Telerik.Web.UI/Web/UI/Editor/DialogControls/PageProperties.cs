using System;
using System.ComponentModel;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x02001054 RID: 4180
	[ClientScriptResource("Telerik.Web.UI.Widgets.PageProperties", "Telerik.Web.UI.Common.Core.js")]
	[ToolboxItem(false)]
	public class PageProperties : UserControlBase, IClientParameterConsumer
	{
		// Token: 0x17003637 RID: 13879
		// (get) Token: 0x0600A8F2 RID: 43250 RVA: 0x0024B59D File Offset: 0x0024979D
		public override string DialogName
		{
			get
			{
				return "PageProperties";
			}
		}

		// Token: 0x0600A8F3 RID: 43251 RVA: 0x0024B5A4 File Offset: 0x002497A4
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			ImageDialogCaller imageDialogCaller = (ImageDialogCaller)base.FindControlRecursive("ImageCaller");
			if (imageDialogCaller != null)
			{
				imageDialogCaller.Text = base.ToolsLocalization.ImageManager;
			}
		}
	}
}

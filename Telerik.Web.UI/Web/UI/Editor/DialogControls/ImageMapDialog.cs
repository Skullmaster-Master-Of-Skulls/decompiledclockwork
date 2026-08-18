using System;
using System.ComponentModel;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x02001053 RID: 4179
	[ToolboxItem(false)]
	[ClientScriptResource("Telerik.Web.UI.Editor.DialogControls.ImageMapDialog", "Telerik.Web.UI.Common.Core.js")]
	public class ImageMapDialog : UserControlBase, IClientParameterConsumer
	{
		// Token: 0x17003636 RID: 13878
		// (get) Token: 0x0600A8EF RID: 43247 RVA: 0x0024B553 File Offset: 0x00249753
		public override string DialogName
		{
			get
			{
				return "ImageMap";
			}
		}

		// Token: 0x0600A8F0 RID: 43248 RVA: 0x0024B55C File Offset: 0x0024975C
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

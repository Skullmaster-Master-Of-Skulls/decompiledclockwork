using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.Dialogs;

namespace Telerik.Web.UI.Editor.DialogControls
{
	// Token: 0x02001051 RID: 4177
	[ToolboxItem(false)]
	[ClientScriptResource("Telerik.Web.UI.Widgets.TableProperties", "Telerik.Web.UI.Common.Core.js")]
	public class TableProperties : UserControlBase
	{
		// Token: 0x17003634 RID: 13876
		// (get) Token: 0x0600A8E8 RID: 43240 RVA: 0x0024B4A5 File Offset: 0x002496A5
		public override string DialogName
		{
			get
			{
				return "TableProperties";
			}
		}

		// Token: 0x0600A8E9 RID: 43241 RVA: 0x0024B4AC File Offset: 0x002496AC
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			this.LocalizeControls();
		}

		// Token: 0x0600A8EA RID: 43242 RVA: 0x0024B4BC File Offset: 0x002496BC
		private void LocalizeControls()
		{
			ImageDialogCaller imageDialogCaller = (ImageDialogCaller)base.FindControlRecursive("ImageCaller");
			if (imageDialogCaller != null)
			{
				imageDialogCaller.Text = base.ToolsLocalization.ImageManager;
			}
			ColorPicker colorPicker = this.FindControlRecursive<ColorPicker>("ColorPicker1");
			if (colorPicker != null)
			{
				colorPicker.Title = base.ToolsLocalization.ColorPicker;
			}
			AlignmentSelector alignmentSelector = this.FindControlRecursive<AlignmentSelector>("TableAlignment");
			if (alignmentSelector != null)
			{
				alignmentSelector.Title = base.ToolsLocalization.AlignmentSelector;
			}
		}

		// Token: 0x0600A8EB RID: 43243 RVA: 0x0024B52E File Offset: 0x0024972E
		private T FindControlRecursive<T>(string id) where T : Control
		{
			return (T)((object)base.FindControlRecursive(id));
		}
	}
}

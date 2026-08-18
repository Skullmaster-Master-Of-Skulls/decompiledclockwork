using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000E4D RID: 3661
	[ToolboxItem(false)]
	public class Print : ImageEditorDialog
	{
		// Token: 0x06008ADD RID: 35549 RVA: 0x001F9FFF File Offset: 0x001F81FF
		public Print(string skin, RadImageEditor parentImageEditor) : base(skin, parentImageEditor)
		{
		}

		// Token: 0x17002BDA RID: 11226
		// (get) Token: 0x06008ADE RID: 35550 RVA: 0x001FA009 File Offset: 0x001F8209
		public override string DialogName
		{
			get
			{
				return "Print";
			}
		}

		// Token: 0x17002BDB RID: 11227
		// (get) Token: 0x06008ADF RID: 35551 RVA: 0x001FA010 File Offset: 0x001F8210
		public override string ScriptUrl
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17002BDC RID: 11228
		// (get) Token: 0x06008AE0 RID: 35552 RVA: 0x001FA017 File Offset: 0x001F8217
		public override string Title
		{
			get
			{
				return base.ParentImageEditor.Localization.Dialogs.Print_Title;
			}
		}

		// Token: 0x06008AE1 RID: 35553 RVA: 0x001FA030 File Offset: 0x001F8230
		protected override void SetChildrensProperties()
		{
			base.SetChildrensProperties();
			DialogsStrings dialogs = base.ParentImageEditor.Localization.Dialogs;
			bool flag = base.ParentImageEditor.RuntimeSkin == "MetroTouch";
			this._printImageOverview = (Image)base.FindControlRecursive("printImageOverview");
			if (this._printImageOverview != null)
			{
				this._printImageOverview.ImageUrl = base.ParentImageEditor.CurrentImageUrl;
				if (flag)
				{
					this._printImageOverview.Width = Unit.Pixel(330);
				}
			}
			this._btnPrint = (RadButton)base.FindControlRecursive("btnPrint");
			if (this._btnPrint != null)
			{
				this._btnPrint.Text = (this._btnPrint.ToolTip = dialogs.Print_Button);
				if (flag)
				{
					this._btnPrint.Icon.PrimaryIconTop = Unit.Pixel(9);
					this._btnPrint.Icon.PrimaryIconLeft = Unit.Pixel(9);
				}
				base.SetChildControlRenderMode(this._btnPrint);
			}
			this._btnCancel = (RadButton)base.FindControlRecursive("btnCancel");
			if (this._btnCancel != null)
			{
				this._btnCancel.Text = (this._btnCancel.ToolTip = dialogs.Common_Cancel);
				if (flag)
				{
					this._btnCancel.Icon.PrimaryIconTop = Unit.Pixel(10);
					this._btnCancel.Icon.PrimaryIconLeft = Unit.Pixel(10);
				}
				base.SetChildControlRenderMode(this._btnCancel);
			}
		}

		// Token: 0x040026D6 RID: 9942
		private Image _printImageOverview;

		// Token: 0x040026D7 RID: 9943
		private RadButton _btnPrint;

		// Token: 0x040026D8 RID: 9944
		private RadButton _btnCancel;
	}
}

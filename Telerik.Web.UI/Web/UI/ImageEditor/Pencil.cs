using System;
using System.ComponentModel;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000BB3 RID: 2995
	[ToolboxItem(false)]
	public class Pencil : ImageEditorDialog
	{
		// Token: 0x060071A2 RID: 29090 RVA: 0x001A98AD File Offset: 0x001A7AAD
		public Pencil(string skin, RadImageEditor parentImageEditor) : base(skin, parentImageEditor)
		{
		}

		// Token: 0x17002512 RID: 9490
		// (get) Token: 0x060071A3 RID: 29091 RVA: 0x001A98B7 File Offset: 0x001A7AB7
		public override string DialogName
		{
			get
			{
				return "Pencil";
			}
		}

		// Token: 0x17002513 RID: 9491
		// (get) Token: 0x060071A4 RID: 29092 RVA: 0x001A98BE File Offset: 0x001A7ABE
		public override string ScriptUrl
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17002514 RID: 9492
		// (get) Token: 0x060071A5 RID: 29093 RVA: 0x001A98C5 File Offset: 0x001A7AC5
		public override string Title
		{
			get
			{
				return base.ParentImageEditor.Localization.Dialogs.Pencil_Title;
			}
		}

		// Token: 0x060071A6 RID: 29094 RVA: 0x001A98DC File Offset: 0x001A7ADC
		protected override void SetChildrensProperties()
		{
			base.SetChildrensProperties();
			bool flag = base.IsTouchSkin();
			DialogsStrings dialogs = base.ParentImageEditor.Localization.Dialogs;
			this.LocalizeHtmlControl("lblColor", dialogs.Pencil_Color);
			this.LocalizeHtmlControl("lblLineSize", dialogs.Pencil_LineSize);
			RadColorPicker radColorPicker = (RadColorPicker)base.FindControlRecursive("DrawColorPicker");
			if (radColorPicker != null)
			{
				radColorPicker.Localization.PickColorText = dialogs.Pencil_PickColor;
				radColorPicker.Localization.CurrentColorText = dialogs.Pencil_CurrentColorText;
				base.SetChildControlRenderMode(radColorPicker);
			}
			RadComboBox radComboBox = (RadComboBox)base.FindControlRecursive("SizeCombo");
			if (radComboBox != null)
			{
				radComboBox.ToolTip = dialogs.Pencil_LineSizeToolTip;
				if (flag)
				{
					radComboBox.Width = Unit.Pixel(90);
				}
				base.SetChildControlRenderMode(radComboBox);
			}
		}

		// Token: 0x060071A7 RID: 29095 RVA: 0x001A99A0 File Offset: 0x001A7BA0
		private void LocalizeHtmlControl(string lblID, string localizedValue)
		{
			HtmlGenericControl htmlGenericControl = (HtmlGenericControl)base.FindControlRecursive(lblID);
			if (htmlGenericControl != null)
			{
				htmlGenericControl.InnerText = localizedValue;
			}
		}
	}
}

using System;
using System.ComponentModel;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000524 RID: 1316
	[ToolboxItem(false)]
	public class Line : ImageEditorDialog
	{
		// Token: 0x06002EFA RID: 12026 RVA: 0x000998D5 File Offset: 0x00097AD5
		public Line(string skin, RadImageEditor parentImageEditor) : base(skin, parentImageEditor)
		{
		}

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06002EFB RID: 12027 RVA: 0x000998DF File Offset: 0x00097ADF
		public override string DialogName
		{
			get
			{
				return "Line";
			}
		}

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x06002EFC RID: 12028 RVA: 0x000998E6 File Offset: 0x00097AE6
		public override string ScriptUrl
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x06002EFD RID: 12029 RVA: 0x000998ED File Offset: 0x00097AED
		public override string Title
		{
			get
			{
				return base.ParentImageEditor.Localization.Dialogs.Line_Title;
			}
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x00099904 File Offset: 0x00097B04
		protected override void SetChildrensProperties()
		{
			base.SetChildrensProperties();
			bool flag = base.IsTouchSkin();
			DialogsStrings dialogs = base.ParentImageEditor.Localization.Dialogs;
			this.LocalizeHtmlControl("lblColor", dialogs.Line_Color);
			this.LocalizeHtmlControl("lblLineSize", dialogs.Line_LineSize);
			this.LocalizeHtmlControl("lblTransform", dialogs.Common_Transform);
			this.LocalizeRadButton("TransformButton", dialogs.Common_Apply, dialogs.Common_AppyDrawToolTip);
			base.SetChildControlRenderMode("TransformButton");
			RadColorPicker radColorPicker = (RadColorPicker)base.FindControlRecursive("DrawColorPicker");
			if (radColorPicker != null)
			{
				radColorPicker.Localization.PickColorText = dialogs.Line_PickColor;
				radColorPicker.Localization.CurrentColorText = dialogs.Line_CurrentColorText;
				base.SetChildControlRenderMode(radColorPicker);
			}
			RadComboBox radComboBox = (RadComboBox)base.FindControlRecursive("SizeCombo");
			if (radComboBox != null)
			{
				radComboBox.ToolTip = dialogs.Line_LineSizeToolTip;
				if (flag)
				{
					radComboBox.Width = Unit.Pixel(90);
				}
				base.SetChildControlRenderMode(radComboBox);
			}
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x000999F8 File Offset: 0x00097BF8
		private void LocalizeRadButton(string buttonId, string text, string tooltip)
		{
			RadButton radButton = (RadButton)base.FindControlRecursive(buttonId);
			if (radButton != null)
			{
				radButton.Text = text;
				radButton.ToolTip = tooltip;
			}
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x00099A24 File Offset: 0x00097C24
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

using System;
using System.ComponentModel;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000522 RID: 1314
	[ToolboxItem(false)]
	public class DrawRectangle : ImageEditorDialog
	{
		// Token: 0x06002EE7 RID: 12007 RVA: 0x0009951C File Offset: 0x0009771C
		public DrawRectangle(string skin, RadImageEditor parentImageEditor) : base(skin, parentImageEditor)
		{
		}

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x06002EE8 RID: 12008 RVA: 0x00099526 File Offset: 0x00097726
		public override string DialogName
		{
			get
			{
				return "DrawRectangle";
			}
		}

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x06002EE9 RID: 12009 RVA: 0x0009952D File Offset: 0x0009772D
		public override string ScriptUrl
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x06002EEA RID: 12010 RVA: 0x00099534 File Offset: 0x00097734
		public override string Title
		{
			get
			{
				return base.ParentImageEditor.Localization.Dialogs.DrawRectangle_Title;
			}
		}

		// Token: 0x06002EEB RID: 12011 RVA: 0x0009954C File Offset: 0x0009774C
		protected override void SetChildrensProperties()
		{
			base.SetChildrensProperties();
			bool flag = base.IsTouchSkin();
			DialogsStrings dialogs = base.ParentImageEditor.Localization.Dialogs;
			this.LocalizeHtmlControl("lblColor", dialogs.DrawRectangle_Color);
			this.LocalizeHtmlControl("lblFillColor", dialogs.DrawRectangle_FillColor);
			this.LocalizeHtmlControl("lblLineSize", dialogs.DrawRectangle_LineSize);
			this.LocalizeHtmlControl("lblTransform", dialogs.Common_Transform);
			this.LocalizeRadButton("TransformButton", dialogs.Common_Apply, dialogs.Common_AppyDrawToolTip);
			this.LocalizeColorPicker("FillColorPicker", dialogs.DrawRectangle_PickFillColor, dialogs.DrawRectangle_CurrentColorText);
			this.LocalizeColorPicker("DrawColorPicker", dialogs.DrawRectangle_PickColor, dialogs.DrawRectangle_CurrentColorText);
			RadComboBox radComboBox = (RadComboBox)base.FindControlRecursive("SizeCombo");
			if (radComboBox != null)
			{
				radComboBox.ToolTip = dialogs.DrawRectangle_LineSizeToolTip;
				if (flag)
				{
					radComboBox.Width = Unit.Pixel(90);
				}
				base.SetChildControlRenderMode(radComboBox);
			}
			base.SetChildControlRenderMode("TransformButton");
			base.SetChildControlRenderMode("FillColorPicker");
			base.SetChildControlRenderMode("DrawColorPicker");
		}

		// Token: 0x06002EEC RID: 12012 RVA: 0x00099658 File Offset: 0x00097858
		private void LocalizeRadButton(string buttonId, string text, string tooltip)
		{
			RadButton radButton = (RadButton)base.FindControlRecursive(buttonId);
			if (radButton != null)
			{
				radButton.Text = text;
				radButton.ToolTip = tooltip;
			}
		}

		// Token: 0x06002EED RID: 12013 RVA: 0x00099684 File Offset: 0x00097884
		private void LocalizeColorPicker(string pickerId, string pickColorText, string currentColorText)
		{
			RadColorPicker radColorPicker = (RadColorPicker)base.FindControlRecursive(pickerId);
			if (radColorPicker != null)
			{
				radColorPicker.Localization.PickColorText = pickColorText;
				radColorPicker.Localization.CurrentColorText = currentColorText;
			}
		}

		// Token: 0x06002EEE RID: 12014 RVA: 0x000996BC File Offset: 0x000978BC
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

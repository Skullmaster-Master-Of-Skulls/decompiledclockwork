using System;
using System.ComponentModel;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000521 RID: 1313
	[ToolboxItem(false)]
	public class DrawCircle : ImageEditorDialog
	{
		// Token: 0x06002EDF RID: 11999 RVA: 0x00099359 File Offset: 0x00097559
		public DrawCircle(string skin, RadImageEditor parentImageEditor) : base(skin, parentImageEditor)
		{
		}

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x06002EE0 RID: 12000 RVA: 0x00099363 File Offset: 0x00097563
		public override string DialogName
		{
			get
			{
				return "DrawCircle";
			}
		}

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x06002EE1 RID: 12001 RVA: 0x0009936A File Offset: 0x0009756A
		public override string ScriptUrl
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x06002EE2 RID: 12002 RVA: 0x00099371 File Offset: 0x00097571
		public override string Title
		{
			get
			{
				return base.ParentImageEditor.Localization.Dialogs.DrawCircle_Title;
			}
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x00099388 File Offset: 0x00097588
		protected override void SetChildrensProperties()
		{
			base.SetChildrensProperties();
			bool flag = base.IsTouchSkin();
			DialogsStrings dialogs = base.ParentImageEditor.Localization.Dialogs;
			this.LocalizeHtmlControl("lblColor", dialogs.DrawCircle_Color);
			this.LocalizeHtmlControl("lblFillColor", dialogs.DrawCircle_FillColor);
			this.LocalizeHtmlControl("lblLineSize", dialogs.DrawCircle_LineSize);
			this.LocalizeHtmlControl("lblTransform", dialogs.Common_Transform);
			this.LocalizeRadButton("TransformButton", dialogs.Common_Apply, dialogs.Common_AppyDrawToolTip);
			this.LocalizeColorPicker("FillColorPicker", dialogs.DrawCircle_PickFillColor, dialogs.DrawCircle_CurrentColorText);
			this.LocalizeColorPicker("DrawColorPicker", dialogs.DrawCircle_PickColor, dialogs.DrawCircle_CurrentColorText);
			RadComboBox radComboBox = (RadComboBox)base.FindControlRecursive("SizeCombo");
			if (radComboBox != null)
			{
				radComboBox.ToolTip = dialogs.DrawCircle_LineSizeToolTip;
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

		// Token: 0x06002EE4 RID: 12004 RVA: 0x00099494 File Offset: 0x00097694
		private void LocalizeRadButton(string buttonId, string text, string tooltip)
		{
			RadButton radButton = (RadButton)base.FindControlRecursive(buttonId);
			if (radButton != null)
			{
				radButton.Text = text;
				radButton.ToolTip = tooltip;
			}
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x000994C0 File Offset: 0x000976C0
		private void LocalizeColorPicker(string pickerId, string pickColorText, string currentColorText)
		{
			RadColorPicker radColorPicker = (RadColorPicker)base.FindControlRecursive(pickerId);
			if (radColorPicker != null)
			{
				radColorPicker.Localization.PickColorText = pickColorText;
				radColorPicker.Localization.CurrentColorText = currentColorText;
			}
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x000994F8 File Offset: 0x000976F8
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

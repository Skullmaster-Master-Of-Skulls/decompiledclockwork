using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.ImageEditor
{
	// Token: 0x02000EB0 RID: 3760
	[ToolboxItem(false)]
	public class AddText : ImageEditorDialog
	{
		// Token: 0x06008F43 RID: 36675 RVA: 0x002040EA File Offset: 0x002022EA
		public AddText(string skin, RadImageEditor parentImageEditor) : base(skin, parentImageEditor)
		{
		}

		// Token: 0x17002D4A RID: 11594
		// (get) Token: 0x06008F44 RID: 36676 RVA: 0x002040F4 File Offset: 0x002022F4
		public override string DialogName
		{
			get
			{
				return "AddText";
			}
		}

		// Token: 0x17002D4B RID: 11595
		// (get) Token: 0x06008F45 RID: 36677 RVA: 0x002040FB File Offset: 0x002022FB
		public override string ScriptUrl
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17002D4C RID: 11596
		// (get) Token: 0x06008F46 RID: 36678 RVA: 0x00204102 File Offset: 0x00202302
		public override string Title
		{
			get
			{
				return base.ParentImageEditor.Localization.Dialogs.AddText_Title;
			}
		}

		// Token: 0x06008F47 RID: 36679 RVA: 0x0020411C File Offset: 0x0020231C
		protected override void SetChildrensProperties()
		{
			base.SetChildrensProperties();
			DialogsStrings dialogs = base.ParentImageEditor.Localization.Dialogs;
			bool flag = base.IsTouchSkin();
			this._textContent = (HtmlTextArea)base.FindControlRecursive("textContent");
			if (this._textContent != null)
			{
				this._textContent.Value = dialogs.AddText_SampleText;
			}
			this._lblTextArea = (Label)base.FindControlRecursive("lblTextArea");
			if (this._lblTextArea != null)
			{
				this._lblTextArea.Text = dialogs.AddText_SampleText;
			}
			this._lblFontFamily = (HtmlGenericControl)base.FindControlRecursive("lblFontFamily");
			if (this._lblFontFamily != null)
			{
				this._lblFontFamily.InnerText = dialogs.AddText_FontFamily + ":";
			}
			this._fontFamily = (RadComboBox)base.FindControlRecursive("fontFamily");
			if (this._fontFamily != null)
			{
				this._fontFamily.ToolTip = dialogs.AddText_FontFamilyToolTip;
				if (flag)
				{
					this._fontFamily.Width = Unit.Pixel(170);
				}
				base.SetChildControlRenderMode(this._fontFamily);
			}
			this._lblFontSize = (Label)base.FindControlRecursive("lblFontSize");
			if (this._lblFontSize != null)
			{
				this._lblFontSize.Text = dialogs.AddText_FontSize + ":";
			}
			this._size = (TextBox)base.FindControlRecursive("size");
			if (this._size != null)
			{
				this._size.ToolTip = dialogs.AddText_FontFamilyToolTip;
				if (flag)
				{
					this._size.Width = Unit.Pixel(70);
				}
			}
			this._lblColor = (Label)base.FindControlRecursive("lblColor");
			if (this._lblColor != null)
			{
				this._lblColor.Text = dialogs.AddText_Color + ":";
			}
			this._color = (TextBox)base.FindControlRecursive("color");
			if (this._color != null)
			{
				this._color.ToolTip = dialogs.AddText_ColorToolTip;
				if (flag)
				{
					this._color.Width = Unit.Pixel(70);
				}
			}
			this._colorPicker = (RadColorPicker)base.FindControlRecursive("colorPicker");
			if (this._colorPicker != null)
			{
				this._colorPicker.Localization.PickColorText = dialogs.AddText_PickColorToolTip;
				this._colorPicker.Localization.CurrentColorText = dialogs.AddText_CurrentColorText;
				base.SetChildControlRenderMode(this._colorPicker);
			}
			this._lPosition = (Literal)base.FindControlRecursive("lPosition");
			if (this._lPosition != null)
			{
				this._lPosition.Text = dialogs.Common_Position + ":";
			}
			this._lblX = (Label)base.FindControlRecursive("lblX");
			if (this._lblX != null)
			{
				this._lblX.Text = dialogs.Common_X + " ";
			}
			this._txtX = (TextBox)base.FindControlRecursive("txtX");
			if (this._txtX != null)
			{
				this._txtX.ToolTip = dialogs.Common_Left;
			}
			this._lblY = (Label)base.FindControlRecursive("lblY");
			if (this._lblY != null)
			{
				this._lblY.Text = dialogs.Common_Y + " ";
			}
			this._txtY = (TextBox)base.FindControlRecursive("txtY");
			if (this._txtY != null)
			{
				this._txtY.ToolTip = dialogs.Common_Top;
			}
			this._btnApply = (RadButton)base.FindControlRecursive("btnApply");
			if (this._btnApply != null)
			{
				this._btnApply.Text = (this._btnApply.ToolTip = dialogs.Common_Insert);
				base.SetChildControlRenderMode(this._btnApply);
			}
			this._btnCancel = (RadButton)base.FindControlRecursive("btnCancel");
			if (this._btnCancel != null)
			{
				this._btnCancel.Text = (this._btnCancel.ToolTip = dialogs.Common_Cancel);
				base.SetChildControlRenderMode(this._btnCancel);
			}
		}

		// Token: 0x06008F48 RID: 36680 RVA: 0x0020451C File Offset: 0x0020271C
		protected override void Render(HtmlTextWriter writer)
		{
			if (this._lblFontFamily != null && this._fontFamily != null)
			{
				this._lblFontFamily.Attributes.Add("for", this._fontFamily.ClientID + "_Input");
			}
			base.Render(writer);
		}

		// Token: 0x040027C8 RID: 10184
		private HtmlTextArea _textContent;

		// Token: 0x040027C9 RID: 10185
		private Label _lblTextArea;

		// Token: 0x040027CA RID: 10186
		private RadComboBox _fontFamily;

		// Token: 0x040027CB RID: 10187
		private HtmlGenericControl _lblFontFamily;

		// Token: 0x040027CC RID: 10188
		private TextBox _size;

		// Token: 0x040027CD RID: 10189
		private Label _lblFontSize;

		// Token: 0x040027CE RID: 10190
		private TextBox _color;

		// Token: 0x040027CF RID: 10191
		private Label _lblColor;

		// Token: 0x040027D0 RID: 10192
		private RadColorPicker _colorPicker;

		// Token: 0x040027D1 RID: 10193
		private Literal _lPosition;

		// Token: 0x040027D2 RID: 10194
		private TextBox _txtX;

		// Token: 0x040027D3 RID: 10195
		private Label _lblX;

		// Token: 0x040027D4 RID: 10196
		private TextBox _txtY;

		// Token: 0x040027D5 RID: 10197
		private Label _lblY;

		// Token: 0x040027D6 RID: 10198
		private RadButton _btnApply;

		// Token: 0x040027D7 RID: 10199
		private RadButton _btnCancel;
	}
}

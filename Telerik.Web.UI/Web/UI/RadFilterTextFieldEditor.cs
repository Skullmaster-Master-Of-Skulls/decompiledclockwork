using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200189F RID: 6303
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	public class RadFilterTextFieldEditor : RadFilterDataFieldEditor
	{
		// Token: 0x0600F3D0 RID: 62416 RVA: 0x0037745C File Offset: 0x0037565C
		public override void InitializeEditor(Control container)
		{
			this._textBoxControl = new TextBox();
			this.PrepareTextBoxControl(this._textBoxControl);
			container.Controls.Add(this._textBoxControl);
			if (!base.IsSingleValue || base.Owner.IsClientOperationMode)
			{
				base.AddBetweenDelimeterControl(container);
				this._secondTextBoxControl = new TextBox();
				this.PrepareTextBoxControl(this._secondTextBoxControl);
				container.Controls.Add(this._secondTextBoxControl);
			}
		}

		// Token: 0x0600F3D1 RID: 62417 RVA: 0x003774D8 File Offset: 0x003756D8
		protected virtual void PrepareTextBoxControl(TextBox textBox)
		{
			textBox.ToolTip = this.ToolTip;
			textBox.CssClass = "rfText";
			textBox.Width = Unit.Pixel(this.TextBoxWidth);
			bool flag = RadFilterTypeHelper.GetNonNullableType(this.DataType) == typeof(char);
			if (flag)
			{
				textBox.MaxLength = 1;
			}
			if (base.Owner.AllowFilterOnBlur)
			{
				textBox.Attributes["onchange"] = this.FilterOnBlurClientScript;
				textBox.Attributes["onkeypress"] = this.FilterOnBlurClientScript;
			}
		}

		// Token: 0x17004978 RID: 18808
		// (get) Token: 0x0600F3D2 RID: 62418 RVA: 0x0037756C File Offset: 0x0037576C
		// (set) Token: 0x0600F3D3 RID: 62419 RVA: 0x00377596 File Offset: 0x00375796
		[DefaultValue("120")]
		[NotifyParentProperty(true)]
		public int TextBoxWidth
		{
			get
			{
				object obj = base.ViewState["TextBoxWidth"];
				if (obj == null)
				{
					return 120;
				}
				return (int)obj;
			}
			set
			{
				base.ViewState["TextBoxWidth"] = value;
			}
		}

		// Token: 0x0600F3D4 RID: 62420 RVA: 0x003775B0 File Offset: 0x003757B0
		protected override void CopySettings(RadFilterDataFieldEditor baseEditor)
		{
			base.CopySettings(baseEditor);
			RadFilterTextFieldEditor radFilterTextFieldEditor = baseEditor as RadFilterTextFieldEditor;
			if (radFilterTextFieldEditor != null)
			{
				this.TextBoxWidth = radFilterTextFieldEditor.TextBoxWidth;
			}
		}

		// Token: 0x0600F3D5 RID: 62421 RVA: 0x003775DC File Offset: 0x003757DC
		public override ArrayList ExtractValues()
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(this._textBoxControl.Text);
			if (!base.IsSingleValue || this._secondTextBoxControl != null)
			{
				arrayList.Add(this._secondTextBoxControl.Text);
			}
			return arrayList;
		}

		// Token: 0x0600F3D6 RID: 62422 RVA: 0x00377624 File Offset: 0x00375824
		public override void SetEditorValues(ArrayList values)
		{
			if (values != null)
			{
				if (values[0] != null)
				{
					this._textBoxControl.Text = values[0].ToString();
				}
				if (!base.IsSingleValue && this._secondTextBoxControl != null && values.Count > 1 && values[1] != null)
				{
					this._secondTextBoxControl.Text = values[1].ToString();
				}
			}
		}

		// Token: 0x0600F3D7 RID: 62423 RVA: 0x0037768D File Offset: 0x0037588D
		internal override WebControl GetFirstInputControl(Control container)
		{
			return this._textBoxControl;
		}

		// Token: 0x0600F3D8 RID: 62424 RVA: 0x00377695 File Offset: 0x00375895
		internal override WebControl GetSecondInputControl(Control container)
		{
			return this._secondTextBoxControl;
		}

		// Token: 0x040045E6 RID: 17894
		private TextBox _textBoxControl;

		// Token: 0x040045E7 RID: 17895
		private TextBox _secondTextBoxControl;
	}
}

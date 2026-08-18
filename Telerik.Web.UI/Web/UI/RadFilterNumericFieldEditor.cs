using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Threading;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200189E RID: 6302
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	public class RadFilterNumericFieldEditor : RadFilterDataFieldEditor
	{
		// Token: 0x17004974 RID: 18804
		// (get) Token: 0x0600F3BE RID: 62398 RVA: 0x00376F10 File Offset: 0x00375110
		// (set) Token: 0x0600F3BF RID: 62399 RVA: 0x00376F3E File Offset: 0x0037513E
		[DefaultValue(typeof(NumericType), "Number")]
		[Description("Gets or sets the NumericType property of the RadNumericTextBox control")]
		[NotifyParentProperty(true)]
		public NumericType NumericType
		{
			get
			{
				object obj = base.ViewState["NumericType"] ?? NumericType.Number;
				return (NumericType)obj;
			}
			set
			{
				base.ViewState["NumericType"] = value;
			}
		}

		// Token: 0x17004975 RID: 18805
		// (get) Token: 0x0600F3C0 RID: 62400 RVA: 0x00376F58 File Offset: 0x00375158
		// (set) Token: 0x0600F3C1 RID: 62401 RVA: 0x00376F86 File Offset: 0x00375186
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the NumberFormat.AllowRounding property of the RadNumericTextBox control")]
		public bool AllowRounding
		{
			get
			{
				object obj = base.ViewState["AllowRounding"] ?? true;
				return (bool)obj;
			}
			set
			{
				base.ViewState["AllowRounding"] = value;
			}
		}

		// Token: 0x17004976 RID: 18806
		// (get) Token: 0x0600F3C2 RID: 62402 RVA: 0x00376FA0 File Offset: 0x003751A0
		// (set) Token: 0x0600F3C3 RID: 62403 RVA: 0x00376FCE File Offset: 0x003751CE
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the NumberFormat.KeepNotRoundedValue property of the RadNumericTextBox control")]
		public bool KeepNotRoundedValue
		{
			get
			{
				object obj = base.ViewState["KeepNotRoundedValue"] ?? false;
				return (bool)obj;
			}
			set
			{
				base.ViewState["KeepNotRoundedValue"] = value;
			}
		}

		// Token: 0x17004977 RID: 18807
		// (get) Token: 0x0600F3C4 RID: 62404 RVA: 0x00376FE8 File Offset: 0x003751E8
		// (set) Token: 0x0600F3C5 RID: 62405 RVA: 0x00377057 File Offset: 0x00375257
		[Description("Gets or sets the NumberFormat.DecimalDigits property of the RadNumericTextBox control")]
		[NotifyParentProperty(true)]
		public int DecimalDigits
		{
			get
			{
				object obj = base.ViewState["DecimalDigits"];
				if (obj != null)
				{
					return (int)obj;
				}
				CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
				switch (this.NumericType)
				{
				case NumericType.Currency:
					return currentCulture.NumberFormat.CurrencyDecimalDigits;
				case NumericType.Percent:
					return currentCulture.NumberFormat.PercentDecimalDigits;
				default:
					return currentCulture.NumberFormat.NumberDecimalDigits;
				}
			}
			set
			{
				if (value < 0 || value > 99)
				{
					throw new ArgumentOutOfRangeException("DecimalDigits", "Valid values are between 0 and 99, inclusive.");
				}
				base.ViewState["DecimalDigits"] = value;
			}
		}

		// Token: 0x0600F3C6 RID: 62406 RVA: 0x003770D8 File Offset: 0x003752D8
		public override void InitializeEditor(Control container)
		{
			this._numericTextBoxControl = new RadNumericTextBox();
			this._numericTextBoxControl.DataType = this.DataType;
			this._numericTextBoxControl.ToolTip = this.ToolTip;
			this._numericTextBoxControl.PreRender += delegate(object sender, EventArgs args)
			{
				this._numericTextBoxControl.Skin = ((RadFilterExpressionItem)this._numericTextBoxControl.NamingContainer).OwnerFilter.RuntimeSkin;
			};
			this.PrepareProperties(this._numericTextBoxControl);
			container.Controls.Add(this._numericTextBoxControl);
			if (!base.IsSingleValue || base.Owner.IsClientOperationMode)
			{
				base.AddBetweenDelimeterControl(container);
				this._secondNumericTextBoxControl = new RadNumericTextBox();
				this._secondNumericTextBoxControl.DataType = this.DataType;
				this._secondNumericTextBoxControl.PreRender += delegate(object sender, EventArgs args)
				{
					this._secondNumericTextBoxControl.Skin = ((RadFilterExpressionItem)this._secondNumericTextBoxControl.NamingContainer).OwnerFilter.RuntimeSkin;
				};
				this.PrepareProperties(this._secondNumericTextBoxControl);
				container.Controls.Add(this._secondNumericTextBoxControl);
			}
			if (RadFilterTypeHelper.GetNumericTypeKind(this.DataType) > 1)
			{
				this._numericTextBoxControl.NumberFormat.DecimalDigits = 0;
				if (this._secondNumericTextBoxControl != null)
				{
					this._secondNumericTextBoxControl.NumberFormat.DecimalDigits = 0;
				}
			}
			if (this.DataType.FullName == "System.Int32")
			{
				this._numericTextBoxControl.MinValue = -2147483648.0;
				this._numericTextBoxControl.MaxValue = 2147483647.0;
				if (this._secondNumericTextBoxControl != null)
				{
					this._secondNumericTextBoxControl.MinValue = -2147483648.0;
					this._secondNumericTextBoxControl.MaxValue = 2147483647.0;
				}
			}
		}

		// Token: 0x0600F3C7 RID: 62407 RVA: 0x00377260 File Offset: 0x00375460
		protected void PrepareProperties(RadNumericTextBox textBox)
		{
			textBox.EnableEmbeddedSkins = base.Owner.EnableEmbeddedSkins;
			textBox.EnableEmbeddedScripts = base.Owner.EnableEmbeddedScripts;
			textBox.EnableEmbeddedBaseStylesheet = base.Owner.EnableEmbeddedBaseStylesheet;
			textBox.RegisterWithScriptManager = base.Owner.RegisterWithScriptManager;
			textBox.EnableAriaSupport = base.Owner.EnableAriaSupport;
			textBox.RenderMode = base.Owner.ResolvedRenderMode;
			textBox.Type = this.NumericType;
			textBox.NumberFormat.AllowRounding = this.AllowRounding;
			textBox.NumberFormat.KeepNotRoundedValue = this.KeepNotRoundedValue;
			textBox.NumberFormat.DecimalDigits = this.DecimalDigits;
			if (base.Owner.AllowFilterOnBlur)
			{
				textBox.Attributes["onchange"] = this.FilterOnBlurClientScript;
				textBox.Attributes["onkeypress"] = this.FilterOnBlurClientScript;
			}
		}

		// Token: 0x0600F3C8 RID: 62408 RVA: 0x0037734C File Offset: 0x0037554C
		protected override void CopySettings(RadFilterDataFieldEditor baseEditor)
		{
			base.CopySettings(baseEditor);
			RadFilterNumericFieldEditor radFilterNumericFieldEditor = baseEditor as RadFilterNumericFieldEditor;
			if (radFilterNumericFieldEditor != null)
			{
				this.NumericType = radFilterNumericFieldEditor.NumericType;
				this.AllowRounding = radFilterNumericFieldEditor.AllowRounding;
				this.KeepNotRoundedValue = radFilterNumericFieldEditor.KeepNotRoundedValue;
				this.DecimalDigits = radFilterNumericFieldEditor.DecimalDigits;
			}
		}

		// Token: 0x0600F3C9 RID: 62409 RVA: 0x0037739C File Offset: 0x0037559C
		public override ArrayList ExtractValues()
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(this._numericTextBoxControl.DbValue);
			if (!base.IsSingleValue || this._secondNumericTextBoxControl != null)
			{
				arrayList.Add(this._secondNumericTextBoxControl.DbValue);
			}
			return arrayList;
		}

		// Token: 0x0600F3CA RID: 62410 RVA: 0x003773E4 File Offset: 0x003755E4
		public override void SetEditorValues(ArrayList values)
		{
			if (values != null)
			{
				if (values[0] != null)
				{
					this._numericTextBoxControl.DbValue = values[0];
				}
				if (!base.IsSingleValue && this._secondNumericTextBoxControl != null && values.Count > 1 && values[1] != null)
				{
					this._secondNumericTextBoxControl.DbValue = values[1];
				}
			}
		}

		// Token: 0x0600F3CB RID: 62411 RVA: 0x00377443 File Offset: 0x00375643
		internal override WebControl GetFirstInputControl(Control container)
		{
			return this._numericTextBoxControl;
		}

		// Token: 0x0600F3CC RID: 62412 RVA: 0x0037744B File Offset: 0x0037564B
		internal override WebControl GetSecondInputControl(Control container)
		{
			return this._secondNumericTextBoxControl;
		}

		// Token: 0x040045E4 RID: 17892
		private RadNumericTextBox _numericTextBoxControl;

		// Token: 0x040045E5 RID: 17893
		private RadNumericTextBox _secondNumericTextBoxControl;
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Security.Permissions;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Licensing;
using Telerik.Web.UI.Design.DatePickerAttributes;

namespace Telerik.Web.UI
{
	// Token: 0x020012BE RID: 4798
	[LightweightRendering]
	[ToolboxBitmap(typeof(RadMaskedTextBox), "Telerik.Web.UI.MaskedTextbox.png")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[Description("Telerik RadInput")]
	[ClientScriptResource("Telerik.Web.UI.RadMaskedTextBox", "Telerik.Web.UI.Input.MaskedTextBox.RadMaskedInputScript.js")]
	[RequiredScript(typeof(RadTextBox))]
	[Designer("Telerik.Web.Design.MaskedTextBoxDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxData("<{0}:RadMaskedTextBox Runat=server></{0}:RadMaskedTextBox>")]
	[ValidationProperty("ValidationText")]
	[SupportsEventValidation]
	[ControlBuilder(typeof(TextBoxControlBuilder))]
	[ControlValueProperty("Text")]
	[DefaultEvent("TextChanged")]
	[DataBindingHandler("System.Web.UI.Design.TextDataBindingHandler, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("Text")]
	[TelerikToolboxCategory("Data Editing")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class RadMaskedTextBox : RadInputControl
	{
		// Token: 0x170040E6 RID: 16614
		// (get) Token: 0x0600C8C9 RID: 51401 RVA: 0x002CC59E File Offset: 0x002CA79E
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Editor("Telerik.Web.Design.MaskPartCollectionEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public MaskPartCollection DisplayMaskParts
		{
			get
			{
				return this._displayMaskParts;
			}
		}

		// Token: 0x170040E7 RID: 16615
		// (get) Token: 0x0600C8CA RID: 51402 RVA: 0x002CC5A6 File Offset: 0x002CA7A6
		// (set) Token: 0x0600C8CB RID: 51403 RVA: 0x002CC5B4 File Offset: 0x002CA7B4
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Editor("Telerik.Web.Design.MaskPropertyEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[Category("Behavior")]
		public string Mask
		{
			get
			{
				return this.MaskParts.Mask;
			}
			set
			{
				bool flag = !string.IsNullOrEmpty(this.Mask);
				string text = this.Text;
				bool flag2 = false;
				if (!string.IsNullOrEmpty(this.MaskParts.Mask) && !string.IsNullOrEmpty(text))
				{
					flag2 = true;
				}
				this.MaskParts.Mask = value;
				if (!string.IsNullOrEmpty(this._textValueSetBeforeMask))
				{
					this.Text = this._textValueSetBeforeMask;
				}
				else if (flag2)
				{
					this.Text = text;
				}
				if (flag)
				{
					this.RefreshViewState();
				}
			}
		}

		// Token: 0x170040E8 RID: 16616
		// (get) Token: 0x0600C8CC RID: 51404 RVA: 0x002CC62F File Offset: 0x002CA82F
		// (set) Token: 0x0600C8CD RID: 51405 RVA: 0x002CC63C File Offset: 0x002CA83C
		[NotifyParentProperty(true)]
		[Editor("Telerik.Web.Design.DisplayMaskPropertyEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Category("Behavior")]
		public string DisplayMask
		{
			get
			{
				return this.DisplayMaskParts.Mask;
			}
			set
			{
				this.DisplayMaskParts.Mask = value;
			}
		}

		// Token: 0x170040E9 RID: 16617
		// (get) Token: 0x0600C8CE RID: 51406 RVA: 0x002CC64A File Offset: 0x002CA84A
		// (set) Token: 0x0600C8CF RID: 51407 RVA: 0x002CC675 File Offset: 0x002CA875
		[ClientControlProperty]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[DefaultValue(DisplayFormatPosition.Left)]
		public DisplayFormatPosition DisplayFormatPosition
		{
			get
			{
				if (this.ViewState["DisplayFormatPosition"] == null)
				{
					return DisplayFormatPosition.Left;
				}
				return (DisplayFormatPosition)this.ViewState["DisplayFormatPosition"];
			}
			set
			{
				this.ViewState["DisplayFormatPosition"] = value;
			}
		}

		// Token: 0x170040EA RID: 16618
		// (get) Token: 0x0600C8D0 RID: 51408 RVA: 0x002CC68D File Offset: 0x002CA88D
		// (set) Token: 0x0600C8D1 RID: 51409 RVA: 0x002CC6B8 File Offset: 0x002CA8B8
		[DefaultValue(InputMode.SingleLine)]
		[NotifyParentProperty(true)]
		[Description("Single-line or multiline mode.")]
		[Category("Behavior")]
		public InputMode TextMode
		{
			get
			{
				if (this.ViewState["TextMode"] == null)
				{
					return InputMode.SingleLine;
				}
				return (InputMode)this.ViewState["TextMode"];
			}
			set
			{
				this.ViewState["TextMode"] = value;
			}
		}

		// Token: 0x170040EB RID: 16619
		// (get) Token: 0x0600C8D2 RID: 51410 RVA: 0x002CC6D0 File Offset: 0x002CA8D0
		// (set) Token: 0x0600C8D3 RID: 51411 RVA: 0x002CC6FB File Offset: 0x002CA8FB
		[ClientControlProperty]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Description("Hide prompt on blur.")]
		public bool HideOnBlur
		{
			get
			{
				return this.ViewState["HideOnBlur"] != null && (bool)this.ViewState["HideOnBlur"];
			}
			set
			{
				this.ViewState["HideOnBlur"] = value;
			}
		}

		// Token: 0x170040EC RID: 16620
		// (get) Token: 0x0600C8D4 RID: 51412 RVA: 0x002CC713 File Offset: 0x002CA913
		// (set) Token: 0x0600C8D5 RID: 51413 RVA: 0x002CC73E File Offset: 0x002CA93E
		[Description("Require complete text to be entered in the RadMaskedTextBox. By default is 'false'. Set to 'true' if you want full text to be required.")]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool RequireCompleteText
		{
			get
			{
				return this.ViewState["RequireCompleteText"] != null && (bool)this.ViewState["RequireCompleteText"];
			}
			set
			{
				this.ViewState["RequireCompleteText"] = value;
			}
		}

		// Token: 0x170040ED RID: 16621
		// (get) Token: 0x0600C8D6 RID: 51414 RVA: 0x002CC756 File Offset: 0x002CA956
		// (set) Token: 0x0600C8D7 RID: 51415 RVA: 0x002CC781 File Offset: 0x002CA981
		[DefaultValue(false)]
		[Description("Move the caret to the begining when the textbox is focused.")]
		[NotifyParentProperty(true)]
		[ClientControlProperty]
		[Category("Behavior")]
		public bool ResetCaretOnFocus
		{
			get
			{
				return this.ViewState["ResetCaretOnFocus"] != null && (bool)this.ViewState["ResetCaretOnFocus"];
			}
			set
			{
				this.ViewState["ResetCaretOnFocus"] = value;
			}
		}

		// Token: 0x170040EE RID: 16622
		// (get) Token: 0x0600C8D8 RID: 51416 RVA: 0x002CC799 File Offset: 0x002CA999
		// (set) Token: 0x0600C8D9 RID: 51417 RVA: 0x002CC7C8 File Offset: 0x002CA9C8
		[Description("Gets or sets the prompt char.")]
		[ClientControlProperty]
		[Category("Behavior")]
		[DefaultValue("_")]
		[NotifyParentProperty(true)]
		public virtual string PromptChar
		{
			get
			{
				if (this.ViewState["PromptChar"] == null)
				{
					return "_";
				}
				return (string)this.ViewState["PromptChar"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = " ";
				}
				this.ViewState["PromptChar"] = value;
			}
		}

		// Token: 0x170040EF RID: 16623
		// (get) Token: 0x0600C8DA RID: 51418 RVA: 0x002CC7EA File Offset: 0x002CA9EA
		// (set) Token: 0x0600C8DB RID: 51419 RVA: 0x002CC819 File Offset: 0x002CAA19
		[DefaultValue("_")]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[ClientControlProperty]
		[Description("Gets or sets the prompt character used in the display mask.")]
		public virtual string DisplayPromptChar
		{
			get
			{
				if (this.ViewState["DisplayPromptChar"] == null)
				{
					return "_";
				}
				return (string)this.ViewState["DisplayPromptChar"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = " ";
				}
				this.ViewState["DisplayPromptChar"] = value;
			}
		}

		// Token: 0x170040F0 RID: 16624
		// (get) Token: 0x0600C8DC RID: 51420 RVA: 0x002CC83B File Offset: 0x002CAA3B
		// (set) Token: 0x0600C8DD RID: 51421 RVA: 0x002CC866 File Offset: 0x002CAA66
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[Category("Behavior")]
		public bool ZeroPadNumericRanges
		{
			get
			{
				return this.ViewState["ZeroPadNumericRanges"] == null || (bool)this.ViewState["ZeroPadNumericRanges"];
			}
			set
			{
				this.ViewState["ZeroPadNumericRanges"] = value;
			}
		}

		// Token: 0x170040F1 RID: 16625
		// (get) Token: 0x0600C8DE RID: 51422 RVA: 0x002CC87E File Offset: 0x002CAA7E
		// (set) Token: 0x0600C8DF RID: 51423 RVA: 0x002CC8A9 File Offset: 0x002CAAA9
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[ClientControlProperty]
		[Category("Behavior")]
		[Description("Determines if the numberic ranges will be rounded.")]
		public bool RoundNumericRanges
		{
			get
			{
				return this.ViewState["RoundNumericRanges"] == null || (bool)this.ViewState["RoundNumericRanges"];
			}
			set
			{
				this.ViewState["RoundNumericRanges"] = value;
			}
		}

		// Token: 0x170040F2 RID: 16626
		// (get) Token: 0x0600C8E0 RID: 51424 RVA: 0x002CC8C1 File Offset: 0x002CAAC1
		// (set) Token: 0x0600C8E1 RID: 51425 RVA: 0x002CC8EC File Offset: 0x002CAAEC
		[DefaultValue(false)]
		[ClientControlProperty]
		[Category("Behavior")]
		[Description("Enables empty mask parts.")]
		[NotifyParentProperty(true)]
		public bool AllowEmptyEnumerations
		{
			get
			{
				return this.ViewState["AllowEmptyEnumerations"] != null && (bool)this.ViewState["AllowEmptyEnumerations"];
			}
			set
			{
				this.ViewState["AllowEmptyEnumerations"] = value;
			}
		}

		// Token: 0x170040F3 RID: 16627
		// (get) Token: 0x0600C8E2 RID: 51426 RVA: 0x002CC904 File Offset: 0x002CAB04
		// (set) Token: 0x0600C8E3 RID: 51427 RVA: 0x002CC92F File Offset: 0x002CAB2F
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[DefaultValue(NumericRangeAlign.Right)]
		[Description("Alignment of numeric ranges.")]
		public NumericRangeAlign NumericRangeAlign
		{
			get
			{
				if (this.ViewState["NumericRangeAlign"] == null)
				{
					return NumericRangeAlign.Right;
				}
				return (NumericRangeAlign)this.ViewState["NumericRangeAlign"];
			}
			set
			{
				this.ViewState["NumericRangeAlign"] = value;
			}
		}

		// Token: 0x170040F4 RID: 16628
		// (get) Token: 0x0600C8E4 RID: 51428 RVA: 0x002CC947 File Offset: 0x002CAB47
		// (set) Token: 0x0600C8E5 RID: 51429 RVA: 0x002CC972 File Offset: 0x002CAB72
		[NotifyParentProperty(true)]
		[Description("Number of rows.")]
		[Category("Behavior")]
		[DefaultValue(2)]
		public virtual int Rows
		{
			get
			{
				if (this.ViewState["Rows"] == null)
				{
					return 2;
				}
				return (int)this.ViewState["Rows"];
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["Rows"] = value;
			}
		}

		// Token: 0x170040F5 RID: 16629
		// (get) Token: 0x0600C8E6 RID: 51430 RVA: 0x002CC999 File Offset: 0x002CAB99
		// (set) Token: 0x0600C8E7 RID: 51431 RVA: 0x002CC9C5 File Offset: 0x002CABC5
		[DefaultValue(20)]
		[NotifyParentProperty(true)]
		[Description("Display width in characters.")]
		[Category("Behavior")]
		public virtual int Columns
		{
			get
			{
				if (this.ViewState["Columns"] == null)
				{
					return 20;
				}
				return (int)this.ViewState["Columns"];
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["Columns"] = value;
			}
		}

		// Token: 0x170040F6 RID: 16630
		// (get) Token: 0x0600C8E8 RID: 51432 RVA: 0x002CC9EC File Offset: 0x002CABEC
		// (set) Token: 0x0600C8E9 RID: 51433 RVA: 0x002CCA17 File Offset: 0x002CAC17
		[Category("Layout")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public virtual bool Wrap
		{
			get
			{
				return this.ViewState["Wrap"] == null || (bool)this.ViewState["Wrap"];
			}
			set
			{
				this.ViewState["Wrap"] = value;
			}
		}

		// Token: 0x170040F7 RID: 16631
		// (get) Token: 0x0600C8EA RID: 51434 RVA: 0x002CCA2F File Offset: 0x002CAC2F
		// (set) Token: 0x0600C8EB RID: 51435 RVA: 0x002CCA5A File Offset: 0x002CAC5A
		[ClientControlProperty]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(SelectionOnFocus), "SelectAll")]
		[Themeable(true)]
		[Category("Behavior")]
		public override SelectionOnFocus SelectionOnFocus
		{
			get
			{
				if (this.ViewState["SelectionOnFocus"] == null)
				{
					return SelectionOnFocus.SelectAll;
				}
				return (SelectionOnFocus)this.ViewState["SelectionOnFocus"];
			}
			set
			{
				this.ViewState["SelectionOnFocus"] = value;
			}
		}

		// Token: 0x170040F8 RID: 16632
		// (get) Token: 0x0600C8EC RID: 51436 RVA: 0x002CCA74 File Offset: 0x002CAC74
		// (set) Token: 0x0600C8ED RID: 51437 RVA: 0x002CCB28 File Offset: 0x002CAD28
		[Description("Raw text of the control.")]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[DefaultValue("")]
		public override string Text
		{
			get
			{
				if (this.ViewState["Text"] != null)
				{
					return (string)this.ViewState["Text"];
				}
				if (string.IsNullOrEmpty(this.Mask) && !string.IsNullOrEmpty(this._textValueSetBeforeMask))
				{
					return this._textValueSetBeforeMask;
				}
				StringBuilder stringBuilder = new StringBuilder();
				foreach (object obj in this._maskParts)
				{
					MaskPart maskPart = (MaskPart)obj;
					stringBuilder.Append(maskPart.Value);
				}
				return stringBuilder.ToString();
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (value.Length == this.Prompt.Length && this.ViewState["OldMask"] != null && this.Mask == (string)this.ViewState["OldMask"])
				{
					this.ParseExactValue(value);
				}
				else
				{
					this.ParseValue(value);
				}
				if (string.IsNullOrEmpty(this.Mask))
				{
					this._textValueSetBeforeMask = value;
				}
				this.RefreshViewState();
			}
		}

		// Token: 0x170040F9 RID: 16633
		// (get) Token: 0x0600C8EE RID: 51438 RVA: 0x002CCBB0 File Offset: 0x002CADB0
		// (set) Token: 0x0600C8EF RID: 51439 RVA: 0x002CCBB8 File Offset: 0x002CADB8
		[DefaultValue(false)]
		[DatePickerBrowsable(false)]
		[ClientControlProperty]
		[Description("Read-only mode")]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		public override bool ReadOnly
		{
			get
			{
				return base.ReadOnly;
			}
			set
			{
				base.ReadOnly = value;
			}
		}

		// Token: 0x170040FA RID: 16634
		// (get) Token: 0x0600C8F0 RID: 51440 RVA: 0x002CCBC1 File Offset: 0x002CADC1
		// (set) Token: 0x0600C8F1 RID: 51441 RVA: 0x002CCBC9 File Offset: 0x002CADC9
		[Browsable(false)]
		public override int MaxLength
		{
			get
			{
				return base.MaxLength;
			}
			set
			{
				base.MaxLength = value;
			}
		}

		// Token: 0x170040FB RID: 16635
		// (get) Token: 0x0600C8F2 RID: 51442 RVA: 0x002CCBD4 File Offset: 0x002CADD4
		[DefaultValue("")]
		[Category("Behavior")]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[Description("Text including the prompt.")]
		public string TextWithPrompt
		{
			get
			{
				if (this.ViewState["TextWithPrompt"] == null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (object obj in this._maskParts)
					{
						MaskPart maskPart = (MaskPart)obj;
						if (!(maskPart is LiteralMaskPart))
						{
							stringBuilder.Append(maskPart.Prompt);
						}
					}
					return stringBuilder.ToString();
				}
				return (string)this.ViewState["TextWithPrompt"];
			}
		}

		// Token: 0x170040FC RID: 16636
		// (get) Token: 0x0600C8F3 RID: 51443 RVA: 0x002CCC70 File Offset: 0x002CAE70
		// (set) Token: 0x0600C8F4 RID: 51444 RVA: 0x002CCD28 File Offset: 0x002CAF28
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("Text including literals.")]
		[Browsable(true)]
		[Category("Behavior")]
		public string TextWithLiterals
		{
			get
			{
				if (this.ViewState["TextWithLiterals"] != null)
				{
					return (string)this.ViewState["TextWithLiterals"];
				}
				if (base.DesignMode)
				{
					return "";
				}
				StringBuilder stringBuilder = new StringBuilder();
				foreach (object obj in this._maskParts)
				{
					MaskPart maskPart = (MaskPart)obj;
					if (maskPart is LiteralMaskPart)
					{
						stringBuilder.Append(maskPart.Prompt);
					}
					stringBuilder.Append(maskPart.Value);
				}
				return stringBuilder.ToString();
			}
			set
			{
				this.Text = value;
			}
		}

		// Token: 0x170040FD RID: 16637
		// (get) Token: 0x0600C8F5 RID: 51445 RVA: 0x002CCD34 File Offset: 0x002CAF34
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Browsable(true)]
		[Description("Text with prompt and literals")]
		[Category("Behavior")]
		public string TextWithPromptAndLiterals
		{
			get
			{
				if (this.ViewState["TextWithPromptAndLiterals"] == null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (object obj in this._maskParts)
					{
						MaskPart maskPart = (MaskPart)obj;
						stringBuilder.Append(maskPart.Prompt);
					}
					return stringBuilder.ToString();
				}
				return (string)this.ViewState["TextWithPromptAndLiterals"];
			}
		}

		// Token: 0x170040FE RID: 16638
		// (get) Token: 0x0600C8F6 RID: 51446 RVA: 0x002CCDC8 File Offset: 0x002CAFC8
		// (set) Token: 0x0600C8F7 RID: 51447 RVA: 0x002CCDD0 File Offset: 0x002CAFD0
		[Description("The skin used by RadInput  components.")]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		public override string Skin
		{
			get
			{
				return base.Skin;
			}
			set
			{
				base.Skin = value;
			}
		}

		// Token: 0x170040FF RID: 16639
		// (get) Token: 0x0600C8F8 RID: 51448 RVA: 0x002CCDD9 File Offset: 0x002CAFD9
		protected virtual string InitialHiddenFieldValue
		{
			get
			{
				return this.TextWithLiterals;
			}
		}

		// Token: 0x17004100 RID: 16640
		// (get) Token: 0x0600C8F9 RID: 51449 RVA: 0x002CCDE1 File Offset: 0x002CAFE1
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				if (this.TextMode == InputMode.MultiLine)
				{
					return HtmlTextWriterTag.Textarea;
				}
				return HtmlTextWriterTag.Input;
			}
		}

		// Token: 0x17004101 RID: 16641
		// (get) Token: 0x0600C8FA RID: 51450 RVA: 0x002CCDF1 File Offset: 0x002CAFF1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ValidationText
		{
			get
			{
				if (string.IsNullOrEmpty(this.Text) || (this.RequireCompleteText && this.TextWithPromptAndLiterals.Length != this.TextWithLiterals.Length))
				{
					return string.Empty;
				}
				return this.TextWithLiterals;
			}
		}

		// Token: 0x17004102 RID: 16642
		// (get) Token: 0x0600C8FB RID: 51451 RVA: 0x002CCE2C File Offset: 0x002CB02C
		[Editor("Telerik.Web.Design.MaskPartCollectionEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Category("Behavior")]
		public MaskPartCollection MaskParts
		{
			get
			{
				return this._maskParts;
			}
		}

		// Token: 0x17004103 RID: 16643
		// (get) Token: 0x0600C8FC RID: 51452 RVA: 0x002CCE34 File Offset: 0x002CB034
		internal string Prompt
		{
			get
			{
				return this._maskParts.Prompt;
			}
		}

		// Token: 0x17004104 RID: 16644
		// (get) Token: 0x0600C8FD RID: 51453 RVA: 0x002CCE41 File Offset: 0x002CB041
		protected internal virtual MaskPartCollection InitialMasks
		{
			get
			{
				return this._maskParts;
			}
		}

		// Token: 0x17004105 RID: 16645
		// (get) Token: 0x0600C8FE RID: 51454 RVA: 0x002CCE49 File Offset: 0x002CB049
		protected internal virtual MaskPartCollection InitialDisplayMasks
		{
			get
			{
				if (this._displayMaskParts.Count > 0)
				{
					return this._displayMaskParts;
				}
				return null;
			}
		}

		// Token: 0x0600C8FF RID: 51455 RVA: 0x002CCE64 File Offset: 0x002CB064
		public RadMaskedTextBox()
		{
			this._maskParts.Owner = this;
			this._displayMaskParts.Owner = this;
		}

		// Token: 0x0600C900 RID: 51456 RVA: 0x002CCEB0 File Offset: 0x002CB0B0
		protected override bool IsMultiLine()
		{
			return this.TextMode == InputMode.MultiLine;
		}

		// Token: 0x0600C901 RID: 51457 RVA: 0x002CCEBC File Offset: 0x002CB0BC
		protected override void SetDefaultSize()
		{
			if (this.isOnlyInputRendered())
			{
				this.defaultWidth = ((this.Columns != 20) ? Unit.Empty : Unit.Pixel(160));
			}
			else if (this.Columns == 20)
			{
				this.defaultWidth = Unit.Pixel(160);
			}
			else
			{
				this.defaultWidth = Unit.Parse(((int)((double)this.Columns * 8.25) + 22).ToString());
			}
			base.SetDefaultSize();
		}

		// Token: 0x0600C902 RID: 51458 RVA: 0x002CCF40 File Offset: 0x002CB140
		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			switch (this.TextMode)
			{
			case InputMode.SingleLine:
				writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
				if (this.MaxLength > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Maxlength, this.MaxLength.ToString(NumberFormatInfo.InvariantInfo));
				}
				if (this.Columns > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Size, this.Columns.ToString(NumberFormatInfo.InvariantInfo));
				}
				break;
			case InputMode.MultiLine:
				writer.AddAttribute(HtmlTextWriterAttribute.Rows, this.Rows.ToString(NumberFormatInfo.InvariantInfo));
				writer.AddAttribute(HtmlTextWriterAttribute.Cols, this.Columns.ToString(NumberFormatInfo.InvariantInfo));
				if (!this.Wrap)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Wrap, "off");
				}
				break;
			case InputMode.Password:
				base.Attributes[HtmlTextWriterAttribute.Type.ToString().ToLower()] = "password";
				if (this.MaxLength > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Maxlength, this.MaxLength.ToString(NumberFormatInfo.InvariantInfo));
				}
				if (this.Columns > 0)
				{
					writer.AddAttribute(HtmlTextWriterAttribute.Size, this.Columns.ToString(NumberFormatInfo.InvariantInfo));
				}
				break;
			}
			if (!this.IsMultiLine() && !base.DesignMode)
			{
				base.Attributes[HtmlTextWriterAttribute.Value.ToString().ToLower()] = this.TextWithPromptAndLiterals;
			}
			base.AddAttributesToRender(writer);
		}

		// Token: 0x0600C903 RID: 51459 RVA: 0x002CD0C0 File Offset: 0x002CB2C0
		protected override void RenderContentsSingleInputFields(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
			writer.AddAttribute(HtmlTextWriterAttribute.Name, this.UniqueID);
			this.AddAttributesToRender(writer);
			writer.RenderBeginTag(this.TagKey.ToString().ToLower(CultureInfo.InvariantCulture));
			if (this.IsMultiLine() && !string.IsNullOrEmpty(this.TextWithPromptAndLiterals))
			{
				HttpUtility.HtmlEncode(this.TextWithPromptAndLiterals, writer);
			}
			writer.RenderEndTag();
		}

		// Token: 0x0600C904 RID: 51460 RVA: 0x002CD137 File Offset: 0x002CB337
		protected override void RenderTextAreaContents(HtmlTextWriter writer)
		{
			HttpUtility.HtmlEncode(this.TextWithPromptAndLiterals, writer);
		}

		// Token: 0x0600C905 RID: 51461 RVA: 0x002CD145 File Offset: 0x002CB345
		public void RaisePostBackEvent(string eventArgument)
		{
		}

		// Token: 0x0600C906 RID: 51462 RVA: 0x002CD148 File Offset: 0x002CB348
		private void ParseValue(string value)
		{
			value = value.Trim();
			int num = 0;
			foreach (object obj in this._maskParts)
			{
				MaskPart maskPart = (MaskPart)obj;
				maskPart.SetValue("");
			}
			foreach (object obj2 in this._maskParts)
			{
				MaskPart maskPart2 = (MaskPart)obj2;
				string value2 = value.Substring(num, Math.Min(value.Length - num, maskPart2.PromptLength));
				num += maskPart2.SetValue(value2);
				if (num >= value.Length)
				{
					break;
				}
			}
		}

		// Token: 0x0600C907 RID: 51463 RVA: 0x002CD230 File Offset: 0x002CB430
		private void ParseExactValue(string value)
		{
			int num = 0;
			foreach (object obj in this._maskParts)
			{
				MaskPart maskPart = (MaskPart)obj;
				maskPart.SetValue("");
			}
			foreach (object obj2 in this._maskParts)
			{
				MaskPart maskPart2 = (MaskPart)obj2;
				maskPart2.SetValue(value.Substring(num, maskPart2.PromptLength));
				num += maskPart2.PromptLength;
			}
		}

		// Token: 0x0600C908 RID: 51464 RVA: 0x002CD2F8 File Offset: 0x002CB4F8
		protected virtual void RefreshViewState()
		{
			this.ViewState["Text"] = null;
			this.ViewState["TextWithLiterals"] = null;
			this.ViewState["TextWithPromptAndLiterals"] = null;
			this.ViewState["TextWithPrompt"] = null;
			this.ViewState["Text"] = this.Text;
			this.ViewState["TextWithLiterals"] = this.TextWithLiterals;
			this.ViewState["TextWithPrompt"] = this.TextWithPrompt;
			this.ViewState["TextWithPromptAndLiterals"] = this.TextWithPromptAndLiterals;
			this.ViewState["OriginalValue"] = null;
		}

		// Token: 0x0600C909 RID: 51465 RVA: 0x002CD3B4 File Offset: 0x002CB5B4
		protected override void OnPreRender(EventArgs e)
		{
			if (this.ViewState["OriginalValue"] == null)
			{
				this.ViewState["OriginalValue"] = this.TextWithPromptAndLiterals;
			}
			base.OnPreRender(e);
			this.ViewState["OldMask"] = this.Mask;
		}

		// Token: 0x0600C90A RID: 51466 RVA: 0x002CD408 File Offset: 0x002CB608
		protected override void AddParsedSubObject(object obj)
		{
			LiteralControl literalControl = obj as LiteralControl;
			if (literalControl == null)
			{
				throw new HttpException("Cannot have children of type " + obj.GetType().ToString());
			}
			this.Text = literalControl.Text;
		}

		// Token: 0x0600C90B RID: 51467 RVA: 0x002CD448 File Offset: 0x002CB648
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddScriptProperty("_initialMasks", this.DescribeMasks(this.InitialMasks));
			if (this.InitialDisplayMasks != null)
			{
				descriptor.AddScriptProperty("_initialDisplayMasks", this.DescribeMasks(this.InitialDisplayMasks));
			}
			if (this.RequireCompleteText)
			{
				descriptor.AddScriptProperty("_requireCompleteText", this.RequireCompleteText.ToString().ToLowerInvariant());
			}
		}

		// Token: 0x0600C90C RID: 51468 RVA: 0x002CD4B8 File Offset: 0x002CB6B8
		private string DescribeMasks(MaskPartCollection masks)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("[");
			for (int i = 0; i < masks.Count; i++)
			{
				stringBuilder.Append(masks[i].InitScript);
				if (i < masks.Count - 1)
				{
					stringBuilder.Append(",");
				}
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x0600C90D RID: 51469 RVA: 0x002CD524 File Offset: 0x002CB724
		protected override void LoadClientState(Dictionary<string, object> clientState)
		{
			base.LoadClientState(clientState);
			if (clientState.ContainsKey("valueWithPromptAndLiterals") && this.IsMultiLine())
			{
				this.Text = ((string)clientState["valueWithPromptAndLiterals"]).Replace("\r\n", "\n").Replace("\n", "\r\n");
			}
		}

		// Token: 0x0600C90E RID: 51470 RVA: 0x002CD584 File Offset: 0x002CB784
		protected override void LoadViewState(object savedState)
		{
			if (savedState == null)
			{
				base.LoadViewState(null);
				return;
			}
			object[] array = (object[])savedState;
			base.LoadViewState(array[0]);
			((IStateManager)this.MaskParts).LoadViewState(array[1]);
			((IStateManager)this.DisplayMaskParts).LoadViewState(array[2]);
		}

		// Token: 0x0600C90F RID: 51471 RVA: 0x002CD5C8 File Offset: 0x002CB7C8
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.MaskParts).SaveViewState(),
				((IStateManager)this.DisplayMaskParts).SaveViewState()
			};
		}

		// Token: 0x0600C910 RID: 51472 RVA: 0x002CD602 File Offset: 0x002CB802
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.MaskParts).TrackViewState();
			((IStateManager)this.DisplayMaskParts).TrackViewState();
		}

		// Token: 0x0600C911 RID: 51473 RVA: 0x002CD620 File Offset: 0x002CB820
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeProperty<bool>(descriptor, "allowEmptyEnumerations", this.AllowEmptyEnumerations, false);
			base.DescribeProperty<DisplayFormatPosition>(descriptor, "displayFormatPosition", this.DisplayFormatPosition, DisplayFormatPosition.Left);
			base.DescribeProperty<string>(descriptor, "displayPromptChar", this.DisplayPromptChar, "_");
			base.DescribeProperty<bool>(descriptor, "hideOnBlur", this.HideOnBlur, false);
			base.DescribeProperty<string>(descriptor, "promptChar", this.PromptChar, "_");
			base.DescribeProperty<bool>(descriptor, "readOnly", this.ReadOnly, false);
			base.DescribeProperty<bool>(descriptor, "resetCaretOnFocus", this.ResetCaretOnFocus, false);
			base.DescribeProperty<bool>(descriptor, "roundNumericRanges", this.RoundNumericRanges, true);
			base.DescribeProperty<object>(descriptor, "selectionOnFocus", this.SelectionOnFocus, Enum.Parse(typeof(SelectionOnFocus), "SelectAll"));
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x0600C912 RID: 51474 RVA: 0x002CD6FF File Offset: 0x002CB8FF
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x040034D7 RID: 13527
		private const int _defaultColumnsValue = 20;

		// Token: 0x040034D8 RID: 13528
		private const int _defaultRowsValue = 2;

		// Token: 0x040034D9 RID: 13529
		internal MaskPartCollection _maskParts = new MaskPartCollection();

		// Token: 0x040034DA RID: 13530
		internal MaskPartCollection _displayMaskParts = new MaskPartCollection();

		// Token: 0x040034DB RID: 13531
		private string _textValueSetBeforeMask = "";
	}
}

using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000870 RID: 2160
	public class MaskedTextBoxSetting : InputSetting
	{
		// Token: 0x06004FB2 RID: 20402 RVA: 0x000F9CB0 File Offset: 0x000F7EB0
		public MaskedTextBoxSetting()
		{
			this.MaskParts.OwnerMaskedTextBoxSetting = this;
			this.DisplayMaskParts.OwnerMaskedTextBoxSetting = this;
		}

		// Token: 0x06004FB3 RID: 20403 RVA: 0x000F9CE6 File Offset: 0x000F7EE6
		public override void Validate(TextBox input)
		{
			this.Validate(input, null);
		}

		// Token: 0x06004FB4 RID: 20404 RVA: 0x000F9CF0 File Offset: 0x000F7EF0
		public override void Validate(TextBox input, object context)
		{
			base.Validate(input, context);
		}

		// Token: 0x06004FB5 RID: 20405 RVA: 0x000F9CFC File Offset: 0x000F7EFC
		internal override void UpdateValue(TextBox input, bool shouldFormat)
		{
			if (shouldFormat)
			{
				if (string.IsNullOrEmpty(input.Text) && !string.IsNullOrEmpty(this.EmptyMessage))
				{
					input.Text = this.EmptyMessage;
					return;
				}
				this.ParseValue(input.Text);
				StringBuilder stringBuilder = new StringBuilder();
				foreach (object obj in this.MaskParts)
				{
					MaskPart maskPart = (MaskPart)obj;
					stringBuilder.Append(maskPart.Prompt);
				}
				input.Text = stringBuilder.ToString();
			}
		}

		// Token: 0x06004FB6 RID: 20406 RVA: 0x000F9DA8 File Offset: 0x000F7FA8
		private void ParseValue(string value)
		{
			value = value.Trim();
			int num = 0;
			foreach (object obj in this.MaskParts)
			{
				MaskPart maskPart = (MaskPart)obj;
				maskPart.SetValue("");
			}
			foreach (object obj2 in this.MaskParts)
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

		// Token: 0x06004FB7 RID: 20407 RVA: 0x000F9E90 File Offset: 0x000F8090
		internal override void Describe(IScriptDescriptor descriptor)
		{
			base.Describe(descriptor);
			descriptor.AddScriptProperty("initialMaskParts", this.DescribeMasks(this.MaskParts));
			if (this.DisplayMaskParts.Count > 0)
			{
				descriptor.AddScriptProperty("initialDisplayMaskParts", this.DescribeMasks(this.DisplayMaskParts));
			}
			descriptor.AddProperty("_promptChar", this.PromptChar);
			descriptor.AddProperty("_displayPromptChar", this.DisplayPromptChar);
			descriptor.AddProperty("_allowEmptyEnumerations", this.AllowEmptyEnumerations);
			descriptor.AddProperty("_roundNumericRanges", this.RoundNumericRanges);
			descriptor.AddProperty("_hideOnBlur", this.HideOnBlur);
			if (this.RequireCompleteText)
			{
				descriptor.AddProperty("_requireCompleteText", this.RequireCompleteText);
			}
			if (this.InvalidStyleDuration != 100)
			{
				descriptor.AddProperty("invalidStyleDuration", this.InvalidStyleDuration);
			}
		}

		// Token: 0x06004FB8 RID: 20408 RVA: 0x000F9F84 File Offset: 0x000F8184
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

		// Token: 0x17001A0C RID: 6668
		// (get) Token: 0x06004FB9 RID: 20409 RVA: 0x000F9FF0 File Offset: 0x000F81F0
		// (set) Token: 0x06004FBA RID: 20410 RVA: 0x000FA01B File Offset: 0x000F821B
		[DefaultValue(false)]
		[Description("Require complete text to be entered in the input. By default is 'false'. Set to 'true' if you want full text to be required.")]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		public bool RequireCompleteText
		{
			get
			{
				return base.ViewState["RequireCompleteText"] != null && (bool)base.ViewState["RequireCompleteText"];
			}
			set
			{
				base.ViewState["RequireCompleteText"] = value;
			}
		}

		// Token: 0x17001A0D RID: 6669
		// (get) Token: 0x06004FBB RID: 20411 RVA: 0x000FA033 File Offset: 0x000F8233
		// (set) Token: 0x06004FBC RID: 20412 RVA: 0x000FA040 File Offset: 0x000F8240
		[Category("Behavior")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Editor("Telerik.Web.Design.MaskPropertyEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
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

		// Token: 0x17001A0E RID: 6670
		// (get) Token: 0x06004FBD RID: 20413 RVA: 0x000FA04E File Offset: 0x000F824E
		// (set) Token: 0x06004FBE RID: 20414 RVA: 0x000FA05B File Offset: 0x000F825B
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Editor("Telerik.Web.Design.MaskPropertyEditor, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4", typeof(UITypeEditor))]
		public string Mask
		{
			get
			{
				return this.MaskParts.Mask;
			}
			set
			{
				this.MaskParts.Mask = value;
			}
		}

		// Token: 0x17001A0F RID: 6671
		// (get) Token: 0x06004FBF RID: 20415 RVA: 0x000FA069 File Offset: 0x000F8269
		// (set) Token: 0x06004FC0 RID: 20416 RVA: 0x000FA098 File Offset: 0x000F8298
		[DefaultValue("_")]
		[ClientControlProperty]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the prompt char.")]
		[Category("Behavior")]
		public virtual string PromptChar
		{
			get
			{
				if (base.ViewState["PromptChar"] == null)
				{
					return "_";
				}
				return (string)base.ViewState["PromptChar"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = " ";
				}
				base.ViewState["PromptChar"] = value;
			}
		}

		// Token: 0x17001A10 RID: 6672
		// (get) Token: 0x06004FC1 RID: 20417 RVA: 0x000FA0BA File Offset: 0x000F82BA
		// (set) Token: 0x06004FC2 RID: 20418 RVA: 0x000FA0E9 File Offset: 0x000F82E9
		[NotifyParentProperty(true)]
		[ClientControlProperty]
		[Category("Behavior")]
		[DefaultValue("_")]
		[Description("Gets or sets the prompt character used in the display mask.")]
		public virtual string DisplayPromptChar
		{
			get
			{
				if (base.ViewState["DisplayPromptChar"] == null)
				{
					return "_";
				}
				return (string)base.ViewState["DisplayPromptChar"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					value = " ";
				}
				base.ViewState["DisplayPromptChar"] = value;
			}
		}

		// Token: 0x17001A11 RID: 6673
		// (get) Token: 0x06004FC3 RID: 20419 RVA: 0x000FA10B File Offset: 0x000F830B
		// (set) Token: 0x06004FC4 RID: 20420 RVA: 0x000FA136 File Offset: 0x000F8336
		[ClientControlProperty]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Enables empty mask parts.")]
		public bool AllowEmptyEnumerations
		{
			get
			{
				return base.ViewState["AllowEmptyEnumerations"] != null && (bool)base.ViewState["AllowEmptyEnumerations"];
			}
			set
			{
				base.ViewState["AllowEmptyEnumerations"] = value;
			}
		}

		// Token: 0x17001A12 RID: 6674
		// (get) Token: 0x06004FC5 RID: 20421 RVA: 0x000FA14E File Offset: 0x000F834E
		// (set) Token: 0x06004FC6 RID: 20422 RVA: 0x000FA179 File Offset: 0x000F8379
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public bool ZeroPadNumericRanges
		{
			get
			{
				return base.ViewState["ZeroPadNumericRanges"] == null || (bool)base.ViewState["ZeroPadNumericRanges"];
			}
			set
			{
				base.ViewState["ZeroPadNumericRanges"] = value;
			}
		}

		// Token: 0x17001A13 RID: 6675
		// (get) Token: 0x06004FC7 RID: 20423 RVA: 0x000FA191 File Offset: 0x000F8391
		// (set) Token: 0x06004FC8 RID: 20424 RVA: 0x000FA1BC File Offset: 0x000F83BC
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[DefaultValue(NumericRangeAlign.Right)]
		[Description("Alignment of numeric ranges.")]
		public NumericRangeAlign NumericRangeAlign
		{
			get
			{
				if (base.ViewState["NumericRangeAlign"] == null)
				{
					return NumericRangeAlign.Right;
				}
				return (NumericRangeAlign)base.ViewState["NumericRangeAlign"];
			}
			set
			{
				base.ViewState["NumericRangeAlign"] = value;
			}
		}

		// Token: 0x17001A14 RID: 6676
		// (get) Token: 0x06004FC9 RID: 20425 RVA: 0x000FA1D4 File Offset: 0x000F83D4
		// (set) Token: 0x06004FCA RID: 20426 RVA: 0x000FA1FF File Offset: 0x000F83FF
		[ClientControlProperty]
		[Category("Behavior")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Description("Determines if the numberic ranges will be rounded.")]
		public bool RoundNumericRanges
		{
			get
			{
				return base.ViewState["RoundNumericRanges"] == null || (bool)base.ViewState["RoundNumericRanges"];
			}
			set
			{
				base.ViewState["RoundNumericRanges"] = value;
			}
		}

		// Token: 0x17001A15 RID: 6677
		// (get) Token: 0x06004FCB RID: 20427 RVA: 0x000FA217 File Offset: 0x000F8417
		// (set) Token: 0x06004FCC RID: 20428 RVA: 0x000FA242 File Offset: 0x000F8442
		[Category("Behavior")]
		[ClientControlProperty]
		[DefaultValue(false)]
		[Description("Hide prompt on blur.")]
		[NotifyParentProperty(true)]
		public bool HideOnBlur
		{
			get
			{
				return base.ViewState["HideOnBlur"] != null && (bool)base.ViewState["HideOnBlur"];
			}
			set
			{
				base.ViewState["HideOnBlur"] = value;
			}
		}

		// Token: 0x17001A16 RID: 6678
		// (get) Token: 0x06004FCD RID: 20429 RVA: 0x000FA25A File Offset: 0x000F845A
		// (set) Token: 0x06004FCE RID: 20430 RVA: 0x000FA286 File Offset: 0x000F8486
		[NotifyParentProperty(true)]
		[Description("Time, in milliseconds, the InvalidStyle should be displayd. Must be a positive integer.")]
		[Category("Behavior")]
		[DefaultValue(100)]
		public virtual int InvalidStyleDuration
		{
			get
			{
				if (base.ViewState["InvalidStyleDuration"] == null)
				{
					return 100;
				}
				return (int)base.ViewState["InvalidStyleDuration"];
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("InvalidStyleDuration", "Must be a positive integer.");
				}
				base.ViewState["InvalidStyleDuration"] = value;
			}
		}

		// Token: 0x040013D5 RID: 5077
		protected MaskPartCollection MaskParts = new MaskPartCollection();

		// Token: 0x040013D6 RID: 5078
		protected MaskPartCollection DisplayMaskParts = new MaskPartCollection();
	}
}

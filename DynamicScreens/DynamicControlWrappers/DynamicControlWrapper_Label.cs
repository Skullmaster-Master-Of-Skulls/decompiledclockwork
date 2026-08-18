using System;
using System.ComponentModel;
using System.Drawing.Design;
using DynamicScreens.DynamicControlWrappers.TypeConverters;

namespace DynamicScreens.DynamicControlWrappers
{
	// Token: 0x02000016 RID: 22
	public class DynamicControlWrapper_Label : DynamicControlWrapper_Base
	{
		// Token: 0x0600017B RID: 379 RVA: 0x00015E44 File Offset: 0x00014E44
		public DynamicControlWrapper_Label(DynamicControl dynamicControl) : base(dynamicControl)
		{
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00015E50 File Offset: 0x00014E50
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00015E73 File Offset: 0x00014E73
		[Category("Display")]
		[Description("AutoSize")]
		public bool AutoSize
		{
			get
			{
				return this.dynamicControl.Setting2 != 0;
			}
			set
			{
				this.dynamicControl.Setting2 = (value ? 1 : 0);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00015E8C File Offset: 0x00014E8C
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00015EAF File Offset: 0x00014EAF
		[Description("Make this label show a summary of all data currently filled into the rest of the form.  Note that the 'indent' parameter will indicate the height of this label if the summary mode is activated.")]
		[Category("Display")]
		public bool ActAsFormSummary
		{
			get
			{
				return this.dynamicControl.Setting3 != 0;
			}
			set
			{
				this.dynamicControl.Setting3 = (value ? 1 : 0);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00015EC8 File Offset: 0x00014EC8
		// (set) Token: 0x06000181 RID: 385 RVA: 0x00015EEA File Offset: 0x00014EEA
		[Description("Indicates whether the label will be underlined.")]
		[Category("Display")]
		public bool TextStyleUnderline
		{
			get
			{
				return (this.dynamicControl.Setting1 & 4) == 4;
			}
			set
			{
				this.dynamicControl.Setting1 = this.AddRemoveFontStyle(value, this.dynamicControl.Setting1, 4);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00015F0C File Offset: 0x00014F0C
		// (set) Token: 0x06000183 RID: 387 RVA: 0x00015F29 File Offset: 0x00014F29
		[Category("Display")]
		[Description("Indent in pixels")]
		public int Indent
		{
			get
			{
				return this.dynamicControl.Setting4;
			}
			set
			{
				this.dynamicControl.Setting4 = value;
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00015F3C File Offset: 0x00014F3C
		private int AddRemoveFontStyle(bool addFontStyle, int oldFontStyle, int fontStyleToAddRemove)
		{
			if (addFontStyle)
			{
				if ((oldFontStyle & fontStyleToAddRemove) != fontStyleToAddRemove)
				{
					return oldFontStyle + fontStyleToAddRemove;
				}
			}
			else if ((oldFontStyle & fontStyleToAddRemove) == fontStyleToAddRemove)
			{
				return oldFontStyle - fontStyleToAddRemove;
			}
			return oldFontStyle;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00015F80 File Offset: 0x00014F80
		// (set) Token: 0x06000186 RID: 390 RVA: 0x00015FA2 File Offset: 0x00014FA2
		[Description("Indicates whether the label will be bolded.")]
		[Category("Display")]
		public bool TextStyleBold
		{
			get
			{
				return (this.dynamicControl.Setting1 & 1) == 1;
			}
			set
			{
				this.dynamicControl.Setting1 = this.AddRemoveFontStyle(value, this.dynamicControl.Setting1, 1);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00015FC4 File Offset: 0x00014FC4
		// (set) Token: 0x06000188 RID: 392 RVA: 0x00015FE6 File Offset: 0x00014FE6
		[Category("Display")]
		[Description("Indicates whether the label will be italicized.")]
		public bool TextStyleItalic
		{
			get
			{
				return (this.dynamicControl.Setting1 & 2) == 2;
			}
			set
			{
				this.dynamicControl.Setting1 = this.AddRemoveFontStyle(value, this.dynamicControl.Setting1, 2);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00016008 File Offset: 0x00015008
		// (set) Token: 0x0600018A RID: 394 RVA: 0x0001602A File Offset: 0x0001502A
		[Description("Indicates whether the label will be strikeout.")]
		[Category("Display")]
		public bool TextStyleStrikeout
		{
			get
			{
				return (this.dynamicControl.Setting1 & 8) == 8;
			}
			set
			{
				this.dynamicControl.Setting1 = this.AddRemoveFontStyle(value, this.dynamicControl.Setting1, 8);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0001604C File Offset: 0x0001504C
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00016069 File Offset: 0x00015069
		[Description("Indicates the percentage size of the text.  The percentage is applied to the base screen font size.")]
		[Category("Display")]
		public int FontSizePercent
		{
			get
			{
				return this.dynamicControl.DefaultValue;
			}
			set
			{
				this.dynamicControl.DefaultValue = value;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0001607C File Offset: 0x0001507C
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00016099 File Offset: 0x00015099
		[Category("Display")]
		[Description("Some additional help text to describe what this field is for or how it should be used.  The message will appear as a pop-up if the user holds their mouse over this control.")]
		[Browsable(false)]
		public override string HelpText
		{
			get
			{
				return this.dynamicControl.HelpText;
			}
			set
			{
				this.dynamicControl.HelpText = value;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000160AC File Offset: 0x000150AC
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00016104 File Offset: 0x00015104
		[Description("Horizontal alignment of label text.")]
		[Category("Display")]
		public DynamicControlWrapper_Label.LabelAlign HorizontalAlign
		{
			get
			{
				string specialInstructionStringValue = DynamicControl.GetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "align");
				DynamicControlWrapper_Label.LabelAlign result;
				if (specialInstructionStringValue.Equals("right"))
				{
					result = DynamicControlWrapper_Label.LabelAlign.right;
				}
				else if (specialInstructionStringValue.Equals("center"))
				{
					result = DynamicControlWrapper_Label.LabelAlign.center;
				}
				else
				{
					result = DynamicControlWrapper_Label.LabelAlign.left;
				}
				return result;
			}
			set
			{
				string value2;
				if (value == DynamicControlWrapper_Label.LabelAlign.right)
				{
					value2 = "right";
				}
				else if (value == DynamicControlWrapper_Label.LabelAlign.center)
				{
					value2 = "center";
				}
				else
				{
					value2 = "";
				}
				this.dynamicControl.ControlGroup = DynamicControl.SetSpecialInstructionStringValue(this.dynamicControl.ControlGroup, "align", value2);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00016160 File Offset: 0x00015160
		// (set) Token: 0x06000192 RID: 402 RVA: 0x0001617D File Offset: 0x0001517D
		[Editor(typeof(RichTextPropertyEditor), typeof(UITypeEditor))]
		[Description("Template text - a button will be create with the label 'Insert Template' - the text in this property will be inserted into the following textbox when the user clicks the button.")]
		[Category("Display")]
		public virtual string TemplateText
		{
			get
			{
				return this.dynamicControl.HelpText;
			}
			set
			{
				this.dynamicControl.HelpText = value;
			}
		}

		// Token: 0x02000017 RID: 23
		public enum LabelAlign
		{
			// Token: 0x04000129 RID: 297
			left,
			// Token: 0x0400012A RID: 298
			right,
			// Token: 0x0400012B RID: 299
			center
		}
	}
}

using System;
using System.ComponentModel;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020012B1 RID: 4785
	public class InputStyle : Style
	{
		// Token: 0x0600C839 RID: 51257 RVA: 0x002C9B7D File Offset: 0x002C7D7D
		public InputStyle()
		{
		}

		// Token: 0x0600C83A RID: 51258 RVA: 0x002C9B85 File Offset: 0x002C7D85
		public InputStyle(StateBag bag) : base(bag)
		{
		}

		// Token: 0x0600C83B RID: 51259 RVA: 0x002C9B90 File Offset: 0x002C7D90
		public override void AddAttributesToRender(HtmlTextWriter writer, WebControl owner)
		{
			base.AddAttributesToRender(writer, owner);
			HorizontalAlign horizontalAlign = this.HorizontalAlign;
			if (horizontalAlign != HorizontalAlign.NotSet)
			{
				TypeConverter converter = TypeDescriptor.GetConverter(typeof(HorizontalAlign));
				writer.AddStyleAttribute("text-align", converter.ConvertToString(horizontalAlign).ToLower(CultureInfo.InvariantCulture));
			}
			Unit paddingRight = this.PaddingRight;
			if (paddingRight != Unit.Empty)
			{
				TypeConverter converter2 = TypeDescriptor.GetConverter(typeof(Unit));
				writer.AddStyleAttribute("padding-right", converter2.ConvertToString(paddingRight).ToLower(CultureInfo.InvariantCulture));
			}
			Unit paddingLeft = this.PaddingLeft;
			if (paddingLeft != Unit.Empty)
			{
				TypeConverter converter3 = TypeDescriptor.GetConverter(typeof(Unit));
				writer.AddStyleAttribute("padding-left", converter3.ConvertToString(paddingLeft).ToLower(CultureInfo.InvariantCulture));
			}
			Unit paddingTop = this.PaddingTop;
			if (paddingTop != Unit.Empty)
			{
				TypeConverter converter4 = TypeDescriptor.GetConverter(typeof(Unit));
				writer.AddStyleAttribute("padding-top", converter4.ConvertToString(paddingTop).ToLower(CultureInfo.InvariantCulture));
			}
			Unit paddingBottom = this.PaddingBottom;
			if (paddingBottom != Unit.Empty)
			{
				TypeConverter converter5 = TypeDescriptor.GetConverter(typeof(Unit));
				writer.AddStyleAttribute("padding-bottom", converter5.ConvertToString(paddingBottom).ToLower(CultureInfo.InvariantCulture));
			}
			Unit letterSpacing = this.LetterSpacing;
			if (letterSpacing != Unit.Empty)
			{
				TypeConverter converter6 = TypeDescriptor.GetConverter(typeof(Unit));
				writer.AddStyleAttribute("letter-spacing", converter6.ConvertToString(letterSpacing).ToLower(CultureInfo.InvariantCulture));
			}
			if (this.Resize != ResizeMode.None)
			{
				writer.AddStyleAttribute("resize", this.Resize.ToString().ToLowerInvariant());
			}
		}

		// Token: 0x0600C83C RID: 51260 RVA: 0x002C9D78 File Offset: 0x002C7F78
		public override void CopyFrom(Style s)
		{
			if (s != null)
			{
				base.CopyFrom(s);
				InputStyle inputStyle = s as InputStyle;
				if (inputStyle != null)
				{
					if (this.HorizontalAlign != inputStyle.HorizontalAlign)
					{
						this.HorizontalAlign = inputStyle.HorizontalAlign;
					}
					if (this.PaddingRight != inputStyle.PaddingRight)
					{
						this.PaddingRight = inputStyle.PaddingRight;
					}
					if (this.PaddingLeft != inputStyle.PaddingLeft)
					{
						this.PaddingLeft = inputStyle.PaddingLeft;
					}
					if (this.PaddingTop != inputStyle.PaddingTop)
					{
						this.PaddingTop = inputStyle.PaddingTop;
					}
					if (this.PaddingBottom != inputStyle.PaddingBottom)
					{
						this.PaddingBottom = inputStyle.PaddingBottom;
					}
					if (this.LetterSpacing != inputStyle.LetterSpacing)
					{
						this.LetterSpacing = inputStyle.LetterSpacing;
					}
				}
			}
		}

		// Token: 0x0600C83D RID: 51261 RVA: 0x002C9E54 File Offset: 0x002C8054
		public override void MergeWith(Style s)
		{
			if (s != null)
			{
				base.MergeWith(s);
				InputStyle inputStyle = s as InputStyle;
				if (inputStyle != null)
				{
					if (inputStyle.HorizontalAlign != HorizontalAlign.NotSet && this.HorizontalAlign == HorizontalAlign.NotSet)
					{
						this.HorizontalAlign = inputStyle.HorizontalAlign;
					}
					if (!inputStyle.PaddingRight.IsEmpty && this.PaddingRight.IsEmpty)
					{
						this.PaddingRight = inputStyle.PaddingRight;
					}
					if (!inputStyle.PaddingLeft.IsEmpty && this.PaddingLeft.IsEmpty)
					{
						this.PaddingLeft = inputStyle.PaddingLeft;
					}
					if (!inputStyle.PaddingTop.IsEmpty && this.PaddingTop.IsEmpty)
					{
						this.PaddingTop = inputStyle.PaddingTop;
					}
					if (!inputStyle.PaddingBottom.IsEmpty && this.PaddingBottom.IsEmpty)
					{
						this.PaddingBottom = inputStyle.PaddingBottom;
					}
					if (!inputStyle.LetterSpacing.IsEmpty && this.LetterSpacing.IsEmpty)
					{
						this.LetterSpacing = inputStyle.LetterSpacing;
					}
					this.Resize = inputStyle.Resize;
				}
			}
		}

		// Token: 0x170040B3 RID: 16563
		// (get) Token: 0x0600C83E RID: 51262 RVA: 0x002C9F88 File Offset: 0x002C8188
		public override bool IsEmpty
		{
			get
			{
				return base.BackColor.IsEmpty && base.BorderColor.IsEmpty && base.BorderStyle == BorderStyle.NotSet && base.BorderWidth.IsEmpty && string.IsNullOrEmpty(base.CssClass) && base.ForeColor.IsEmpty && base.Height.IsEmpty && this.HorizontalAlign == HorizontalAlign.NotSet && this.LetterSpacing.IsEmpty && this.PaddingBottom.IsEmpty && this.PaddingLeft.IsEmpty && this.PaddingRight.IsEmpty && this.PaddingTop.IsEmpty && base.Width.IsEmpty;
			}
		}

		// Token: 0x0600C83F RID: 51263 RVA: 0x002CA080 File Offset: 0x002C8280
		public override void Reset()
		{
			if (this.HorizontalAlign != HorizontalAlign.NotSet)
			{
				base.ViewState.Remove("HorizontalAlign");
			}
			if (!this.PaddingRight.IsEmpty)
			{
				base.ViewState.Remove("PaddingRight");
			}
			if (!this.PaddingLeft.IsEmpty)
			{
				base.ViewState.Remove("PaddingLeft");
			}
			if (!this.PaddingTop.IsEmpty)
			{
				base.ViewState.Remove("PaddingTop");
			}
			if (!this.PaddingBottom.IsEmpty)
			{
				base.ViewState.Remove("PaddingBottom");
			}
			if (!this.LetterSpacing.IsEmpty)
			{
				base.ViewState.Remove("LetterSpacing");
			}
			base.Reset();
		}

		// Token: 0x170040B4 RID: 16564
		// (get) Token: 0x0600C840 RID: 51264 RVA: 0x002CA14C File Offset: 0x002C834C
		// (set) Token: 0x0600C841 RID: 51265 RVA: 0x002CA177 File Offset: 0x002C8377
		[Category("Layout")]
		[Description("The horizontal alignment applied to the HTML input element.")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(HorizontalAlign), "NotSet")]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				if (base.ViewState["HorizontalAlign"] != null)
				{
					return (HorizontalAlign)base.ViewState["HorizontalAlign"];
				}
				return HorizontalAlign.NotSet;
			}
			set
			{
				if (value < HorizontalAlign.NotSet || value > HorizontalAlign.Justify)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["HorizontalAlign"] = value;
			}
		}

		// Token: 0x170040B5 RID: 16565
		// (get) Token: 0x0600C842 RID: 51266 RVA: 0x002CA1A2 File Offset: 0x002C83A2
		// (set) Token: 0x0600C843 RID: 51267 RVA: 0x002CA1D1 File Offset: 0x002C83D1
		[NotifyParentProperty(true)]
		[Description("The right padding applied to the html input element.")]
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		public virtual Unit PaddingRight
		{
			get
			{
				if (base.ViewState["PaddingRight"] != null)
				{
					return (Unit)base.ViewState["PaddingRight"];
				}
				return Unit.Empty;
			}
			set
			{
				base.ViewState["PaddingRight"] = value;
			}
		}

		// Token: 0x170040B6 RID: 16566
		// (get) Token: 0x0600C844 RID: 51268 RVA: 0x002CA1E9 File Offset: 0x002C83E9
		// (set) Token: 0x0600C845 RID: 51269 RVA: 0x002CA218 File Offset: 0x002C8418
		[DefaultValue(typeof(Unit), "")]
		[Description("The left padding applied to the html input element.")]
		[NotifyParentProperty(true)]
		[Category("Layout")]
		public virtual Unit PaddingLeft
		{
			get
			{
				if (base.ViewState["PaddingLeft"] != null)
				{
					return (Unit)base.ViewState["PaddingLeft"];
				}
				return Unit.Empty;
			}
			set
			{
				base.ViewState["PaddingLeft"] = value;
			}
		}

		// Token: 0x170040B7 RID: 16567
		// (get) Token: 0x0600C846 RID: 51270 RVA: 0x002CA230 File Offset: 0x002C8430
		// (set) Token: 0x0600C847 RID: 51271 RVA: 0x002CA25F File Offset: 0x002C845F
		[Category("Layout")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "")]
		[Description("The top padding applied to the html input element.")]
		public virtual Unit PaddingTop
		{
			get
			{
				if (base.ViewState["PaddingTop"] != null)
				{
					return (Unit)base.ViewState["PaddingTop"];
				}
				return Unit.Empty;
			}
			set
			{
				base.ViewState["PaddingTop"] = value;
			}
		}

		// Token: 0x170040B8 RID: 16568
		// (get) Token: 0x0600C848 RID: 51272 RVA: 0x002CA277 File Offset: 0x002C8477
		// (set) Token: 0x0600C849 RID: 51273 RVA: 0x002CA2A6 File Offset: 0x002C84A6
		[Description("The top padding applied to the html input element.")]
		[Category("Layout")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "")]
		public virtual Unit PaddingBottom
		{
			get
			{
				if (base.ViewState["PaddingBottom"] != null)
				{
					return (Unit)base.ViewState["PaddingBottom"];
				}
				return Unit.Empty;
			}
			set
			{
				base.ViewState["PaddingBottom"] = value;
			}
		}

		// Token: 0x170040B9 RID: 16569
		// (get) Token: 0x0600C84A RID: 51274 RVA: 0x002CA2BE File Offset: 0x002C84BE
		// (set) Token: 0x0600C84B RID: 51275 RVA: 0x002CA2ED File Offset: 0x002C84ED
		[NotifyParentProperty(true)]
		[Description("The spacing between letters in the input control")]
		[DefaultValue(typeof(Unit), "")]
		[Category("Layout")]
		public virtual Unit LetterSpacing
		{
			get
			{
				if (base.ViewState["LetterSpacing"] != null)
				{
					return (Unit)base.ViewState["LetterSpacing"];
				}
				return Unit.Empty;
			}
			set
			{
				base.ViewState["LetterSpacing"] = value;
			}
		}

		// Token: 0x170040BA RID: 16570
		// (get) Token: 0x0600C84C RID: 51276 RVA: 0x002CA305 File Offset: 0x002C8505
		// (set) Token: 0x0600C84D RID: 51277 RVA: 0x002CA330 File Offset: 0x002C8530
		public virtual ResizeMode Resize
		{
			get
			{
				if (base.ViewState["ResizeMode"] != null)
				{
					return (ResizeMode)base.ViewState["ResizeMode"];
				}
				return ResizeMode.None;
			}
			set
			{
				base.ViewState["ResizeMode"] = value;
			}
		}
	}
}

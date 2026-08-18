using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x02000254 RID: 596
	[TypeConverter(typeof(FlatButtonAppearanceConverter))]
	public class FlatButtonAppearance
	{
		// Token: 0x060025AE RID: 9646 RVA: 0x000AF98C File Offset: 0x000ADB8C
		internal FlatButtonAppearance(ButtonBase owner)
		{
			this.owner = owner;
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x060025AF RID: 9647 RVA: 0x000AF9D9 File Offset: 0x000ADBD9
		// (set) Token: 0x060025B0 RID: 9648 RVA: 0x000AF9E4 File Offset: 0x000ADBE4
		[Browsable(true)]
		[ApplicableToButton]
		[NotifyParentProperty(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ButtonBorderSizeDescr")]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(1)]
		public int BorderSize
		{
			get
			{
				return this.borderSize;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("BorderSize", value, SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"BorderSize",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.borderSize != value)
				{
					this.borderSize = value;
					if (this.owner != null && this.owner.ParentInternal != null)
					{
						LayoutTransaction.DoLayoutIf(this.owner.AutoSize, this.owner.ParentInternal, this.owner, PropertyNames.FlatAppearanceBorderSize);
					}
					this.owner.Invalidate();
				}
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x060025B1 RID: 9649 RVA: 0x000AFA94 File Offset: 0x000ADC94
		// (set) Token: 0x060025B2 RID: 9650 RVA: 0x000AFA9C File Offset: 0x000ADC9C
		[Browsable(true)]
		[ApplicableToButton]
		[NotifyParentProperty(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ButtonBorderColorDescr")]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(typeof(Color), "")]
		public Color BorderColor
		{
			get
			{
				return this.borderColor;
			}
			set
			{
				if (value.Equals(Color.Transparent))
				{
					throw new NotSupportedException(SR.GetString("ButtonFlatAppearanceInvalidBorderColor"));
				}
				if (this.borderColor != value)
				{
					this.borderColor = value;
					this.owner.Invalidate();
				}
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x060025B3 RID: 9651 RVA: 0x000AFAF2 File Offset: 0x000ADCF2
		// (set) Token: 0x060025B4 RID: 9652 RVA: 0x000AFAFA File Offset: 0x000ADCFA
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ButtonCheckedBackColorDescr")]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(typeof(Color), "")]
		public Color CheckedBackColor
		{
			get
			{
				return this.checkedBackColor;
			}
			set
			{
				if (this.checkedBackColor != value)
				{
					this.checkedBackColor = value;
					this.owner.Invalidate();
				}
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x060025B5 RID: 9653 RVA: 0x000AFB1C File Offset: 0x000ADD1C
		// (set) Token: 0x060025B6 RID: 9654 RVA: 0x000AFB24 File Offset: 0x000ADD24
		[Browsable(true)]
		[ApplicableToButton]
		[NotifyParentProperty(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ButtonMouseDownBackColorDescr")]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(typeof(Color), "")]
		public Color MouseDownBackColor
		{
			get
			{
				return this.mouseDownBackColor;
			}
			set
			{
				if (this.mouseDownBackColor != value)
				{
					this.mouseDownBackColor = value;
					this.owner.Invalidate();
				}
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x060025B7 RID: 9655 RVA: 0x000AFB46 File Offset: 0x000ADD46
		// (set) Token: 0x060025B8 RID: 9656 RVA: 0x000AFB4E File Offset: 0x000ADD4E
		[Browsable(true)]
		[ApplicableToButton]
		[NotifyParentProperty(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ButtonMouseOverBackColorDescr")]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DefaultValue(typeof(Color), "")]
		public Color MouseOverBackColor
		{
			get
			{
				return this.mouseOverBackColor;
			}
			set
			{
				if (this.mouseOverBackColor != value)
				{
					this.mouseOverBackColor = value;
					this.owner.Invalidate();
				}
			}
		}

		// Token: 0x04000FAA RID: 4010
		private ButtonBase owner;

		// Token: 0x04000FAB RID: 4011
		private int borderSize = 1;

		// Token: 0x04000FAC RID: 4012
		private Color borderColor = Color.Empty;

		// Token: 0x04000FAD RID: 4013
		private Color checkedBackColor = Color.Empty;

		// Token: 0x04000FAE RID: 4014
		private Color mouseDownBackColor = Color.Empty;

		// Token: 0x04000FAF RID: 4015
		private Color mouseOverBackColor = Color.Empty;
	}
}

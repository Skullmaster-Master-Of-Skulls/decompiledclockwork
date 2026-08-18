using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x0200177A RID: 6010
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	public class DimensionsSeriesPointMark : Dimensions
	{
		// Token: 0x0600EA7D RID: 60029 RVA: 0x003571C6 File Offset: 0x003553C6
		public DimensionsSeriesPointMark(object containerObject) : base(containerObject)
		{
		}

		// Token: 0x0600EA7E RID: 60030 RVA: 0x003571CF File Offset: 0x003553CF
		public DimensionsSeriesPointMark()
		{
		}

		// Token: 0x1700470C RID: 18188
		// (get) Token: 0x0600EA7F RID: 60031 RVA: 0x003571D7 File Offset: 0x003553D7
		// (set) Token: 0x0600EA80 RID: 60032 RVA: 0x003571F7 File Offset: 0x003553F7
		public override Unit Height
		{
			get
			{
				return (Unit)(base.ViewState["Height"] ?? DefaultValues.DEFAULT_POINTMARK_PIXEL_VALUE);
			}
			set
			{
				if (value.PixelValue >= 0f)
				{
					base.Height = value;
				}
			}
		}

		// Token: 0x1700470D RID: 18189
		// (get) Token: 0x0600EA81 RID: 60033 RVA: 0x0035720D File Offset: 0x0035540D
		// (set) Token: 0x0600EA82 RID: 60034 RVA: 0x0035722D File Offset: 0x0035542D
		public override Unit Width
		{
			get
			{
				return (Unit)(base.ViewState["Width"] ?? DefaultValues.DEFAULT_POINTMARK_PIXEL_VALUE);
			}
			set
			{
				if (value.PixelValue >= 0f)
				{
					base.Width = value;
				}
			}
		}

		// Token: 0x1700470E RID: 18190
		// (get) Token: 0x0600EA83 RID: 60035 RVA: 0x00357243 File Offset: 0x00355443
		// (set) Token: 0x0600EA84 RID: 60036 RVA: 0x0035724B File Offset: 0x0035544B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[SkinnableProperty]
		public override ChartMargins Margins
		{
			get
			{
				return base.Margins;
			}
			set
			{
				base.Margins = value;
			}
		}

		// Token: 0x1700470F RID: 18191
		// (get) Token: 0x0600EA85 RID: 60037 RVA: 0x00357254 File Offset: 0x00355454
		// (set) Token: 0x0600EA86 RID: 60038 RVA: 0x0035725C File Offset: 0x0035545C
		[SkinnableProperty]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ChartPaddings Paddings
		{
			get
			{
				return base.Paddings;
			}
			set
			{
				base.Paddings = value;
			}
		}

		// Token: 0x0600EA87 RID: 60039 RVA: 0x00357265 File Offset: 0x00355465
		protected override void ResetHeight()
		{
			this.Height = DefaultValues.DEFAULT_POINTMARK_PIXEL_VALUE;
		}

		// Token: 0x0600EA88 RID: 60040 RVA: 0x00357272 File Offset: 0x00355472
		protected override void ResetWidth()
		{
			this.Width = DefaultValues.DEFAULT_POINTMARK_PIXEL_VALUE;
		}

		// Token: 0x0600EA89 RID: 60041 RVA: 0x00357280 File Offset: 0x00355480
		internal override void Reset()
		{
			base.Reset();
			this.Height = (this.Width = DefaultValues.DEFAULT_POINTMARK_PIXEL_VALUE);
		}

		// Token: 0x0600EA8A RID: 60042 RVA: 0x003572A8 File Offset: 0x003554A8
		public override object Clone()
		{
			DimensionsSeriesPointMark result = (DimensionsSeriesPointMark)base.Clone();
			if (this.AutoSize)
			{
				this.ResetHeight();
				this.ResetWidth();
			}
			return result;
		}
	}
}

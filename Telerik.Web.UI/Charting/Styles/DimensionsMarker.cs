using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x0200177F RID: 6015
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	public class DimensionsMarker : Dimensions
	{
		// Token: 0x0600EAA4 RID: 60068 RVA: 0x0035758F File Offset: 0x0035578F
		public DimensionsMarker(object containerObject) : base(containerObject)
		{
		}

		// Token: 0x0600EAA5 RID: 60069 RVA: 0x00357598 File Offset: 0x00355798
		public DimensionsMarker()
		{
		}

		// Token: 0x0600EAA6 RID: 60070 RVA: 0x003575A0 File Offset: 0x003557A0
		public DimensionsMarker(float width, float height) : base(width, height)
		{
		}

		// Token: 0x17004717 RID: 18199
		// (get) Token: 0x0600EAA7 RID: 60071 RVA: 0x003575AA File Offset: 0x003557AA
		// (set) Token: 0x0600EAA8 RID: 60072 RVA: 0x003575B2 File Offset: 0x003557B2
		[Browsable(false)]
		[SkinnableProperty]
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

		// Token: 0x17004718 RID: 18200
		// (get) Token: 0x0600EAA9 RID: 60073 RVA: 0x003575BB File Offset: 0x003557BB
		// (set) Token: 0x0600EAAA RID: 60074 RVA: 0x003575C3 File Offset: 0x003557C3
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
				if (value)
				{
					this.ResetHeight();
					this.ResetWidth();
				}
			}
		}

		// Token: 0x17004719 RID: 18201
		// (get) Token: 0x0600EAAB RID: 60075 RVA: 0x003575DB File Offset: 0x003557DB
		// (set) Token: 0x0600EAAC RID: 60076 RVA: 0x003575FB File Offset: 0x003557FB
		public override Unit Height
		{
			get
			{
				return (Unit)(base.ViewState["Height"] ?? DefaultValues.DEFAULT_MARKER_PIXEL_VALUE);
			}
			set
			{
				if (value.PixelValue >= 0f)
				{
					base.Height = value;
				}
			}
		}

		// Token: 0x1700471A RID: 18202
		// (get) Token: 0x0600EAAD RID: 60077 RVA: 0x00357611 File Offset: 0x00355811
		// (set) Token: 0x0600EAAE RID: 60078 RVA: 0x00357631 File Offset: 0x00355831
		public override Unit Width
		{
			get
			{
				return (Unit)(base.ViewState["Width"] ?? DefaultValues.DEFAULT_MARKER_PIXEL_VALUE);
			}
			set
			{
				if (value.PixelValue >= 0f)
				{
					base.Width = value;
				}
			}
		}

		// Token: 0x0600EAAF RID: 60079 RVA: 0x00357647 File Offset: 0x00355847
		protected override void ResetHeight()
		{
			this.Height = DefaultValues.DEFAULT_MARKER_PIXEL_VALUE;
		}

		// Token: 0x0600EAB0 RID: 60080 RVA: 0x00357654 File Offset: 0x00355854
		protected override void ResetWidth()
		{
			this.Width = DefaultValues.DEFAULT_MARKER_PIXEL_VALUE;
		}

		// Token: 0x0600EAB1 RID: 60081 RVA: 0x00357664 File Offset: 0x00355864
		internal override void Reset()
		{
			base.Reset();
			this.Width = (this.Height = DefaultValues.DEFAULT_MARKER_PIXEL_VALUE);
		}
	}
}

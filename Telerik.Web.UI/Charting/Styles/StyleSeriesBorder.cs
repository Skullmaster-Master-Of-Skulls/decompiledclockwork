using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x0200179E RID: 6046
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	public class StyleSeriesBorder : StyleBorder
	{
		// Token: 0x0600EBB0 RID: 60336 RVA: 0x0035A568 File Offset: 0x00358768
		public StyleSeriesBorder(ChartSeries series) : base(series)
		{
		}

		// Token: 0x0600EBB1 RID: 60337 RVA: 0x0035A571 File Offset: 0x00358771
		public StyleSeriesBorder()
		{
		}

		// Token: 0x1700475B RID: 18267
		// (get) Token: 0x0600EBB2 RID: 60338 RVA: 0x0035A579 File Offset: 0x00358779
		// (set) Token: 0x0600EBB3 RID: 60339 RVA: 0x0035A59A File Offset: 0x0035879A
		[DefaultValue(typeof(DashStyle), "Solid")]
		[SkinnableProperty]
		public override DashStyle PenStyle
		{
			get
			{
				return (DashStyle)(base.ViewState["PenStyle"] ?? DashStyle.Solid);
			}
			set
			{
				base.PenStyle = value;
			}
		}

		// Token: 0x1700475C RID: 18268
		// (get) Token: 0x0600EBB4 RID: 60340 RVA: 0x0035A5A3 File Offset: 0x003587A3
		// (set) Token: 0x0600EBB5 RID: 60341 RVA: 0x0035A5C8 File Offset: 0x003587C8
		[Description("Border color")]
		[DefaultValue(typeof(Color), "153, 209, 248")]
		[TypeConverter(typeof(ColorConverter))]
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		public override Color Color
		{
			get
			{
				return (Color)(base.ViewState["Color"] ?? DefaultValues.DEFAULT_SERIESBORDER_COLOR);
			}
			set
			{
				base.Color = value;
			}
		}

		// Token: 0x0600EBB6 RID: 60342 RVA: 0x0035A5D1 File Offset: 0x003587D1
		internal override void Reset()
		{
			base.Reset();
			this.Color = DefaultValues.DEFAULT_SERIESBORDER_COLOR;
		}
	}
}

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x0200179C RID: 6044
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	public class StyleLineSeries : LineStyle
	{
		// Token: 0x17004754 RID: 18260
		// (get) Token: 0x0600EB9F RID: 60319 RVA: 0x0035A3B6 File Offset: 0x003585B6
		// (set) Token: 0x0600EBA0 RID: 60320 RVA: 0x0035A3DB File Offset: 0x003585DB
		[DefaultValue(2f)]
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[Description("Specifies the width of the line in a line series.")]
		public override float Width
		{
			get
			{
				return (float)(base.ViewState["Width"] ?? 2f);
			}
			set
			{
				base.Width = value;
			}
		}

		// Token: 0x17004755 RID: 18261
		// (get) Token: 0x0600EBA1 RID: 60321 RVA: 0x0035A3E4 File Offset: 0x003585E4
		// (set) Token: 0x0600EBA2 RID: 60322 RVA: 0x0035A405 File Offset: 0x00358605
		[SkinnableProperty]
		[DefaultValue(typeof(DashStyle), "Solid")]
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

		// Token: 0x17004756 RID: 18262
		// (get) Token: 0x0600EBA3 RID: 60323 RVA: 0x0035A40E File Offset: 0x0035860E
		protected bool IsEmptyLine
		{
			get
			{
				return this is StyleEmptyLineSeries;
			}
		}

		// Token: 0x17004757 RID: 18263
		// (get) Token: 0x0600EBA4 RID: 60324 RVA: 0x0035A419 File Offset: 0x00358619
		// (set) Token: 0x0600EBA5 RID: 60325 RVA: 0x0035A451 File Offset: 0x00358651
		[Description("Line color")]
		[SkinnableProperty]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(typeof(Color), "")]
		[TypeConverter(typeof(ColorConverter))]
		[NotifyParentProperty(true)]
		public override Color Color
		{
			get
			{
				if (this.lineStyleContainerObject is ChartSeries && !this.IsEmptyLine)
				{
					return ((ChartSeries)this.lineStyleContainerObject).Appearance.FillStyle.MainColor;
				}
				return this.tmpStyleLineSeriesColor;
			}
			set
			{
				if (this.lineStyleContainerObject is ChartSeries && !this.IsEmptyLine)
				{
					((ChartSeries)this.lineStyleContainerObject).Appearance.FillStyle.MainColor = value;
					return;
				}
				this.tmpStyleLineSeriesColor = value;
			}
		}

		// Token: 0x17004758 RID: 18264
		// (get) Token: 0x0600EBA6 RID: 60326 RVA: 0x0035A48B File Offset: 0x0035868B
		// (set) Token: 0x0600EBA7 RID: 60327 RVA: 0x0035A4B9 File Offset: 0x003586B9
		[DefaultValue(true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[NotifyParentProperty(true)]
		public override bool Visible
		{
			get
			{
				if (this.lineStyleContainerObject is ChartSeries && !this.IsEmptyLine)
				{
					return ((ChartSeries)this.lineStyleContainerObject).Visible;
				}
				return base.Visible;
			}
			set
			{
				if (this.lineStyleContainerObject is ChartSeries && !this.IsEmptyLine)
				{
					((ChartSeries)this.lineStyleContainerObject).Visible = value;
					return;
				}
				base.Visible = value;
			}
		}

		// Token: 0x0600EBA8 RID: 60328 RVA: 0x0035A4E9 File Offset: 0x003586E9
		internal override void Reset()
		{
			base.Reset();
			this.Color = Color.Empty;
			this.Width = 2f;
			this.PenStyle = DashStyle.Solid;
		}

		// Token: 0x0400441A RID: 17434
		private Color tmpStyleLineSeriesColor;
	}
}

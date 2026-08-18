using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017D0 RID: 6096
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	[ParseChildren(true)]
	public class StyleGridLine : LineStyle
	{
		// Token: 0x170047D2 RID: 18386
		// (get) Token: 0x0600ED3E RID: 60734 RVA: 0x00361DEE File Offset: 0x0035FFEE
		// (set) Token: 0x0600ED3F RID: 60735 RVA: 0x00361E0F File Offset: 0x0036000F
		[DefaultValue(true)]
		[SkinnableProperty]
		public bool HideWithAxis
		{
			get
			{
				return (bool)(base.ViewState["HideWithAxis"] ?? true);
			}
			set
			{
				base.ViewState["HideWithAxis"] = value;
			}
		}

		// Token: 0x170047D3 RID: 18387
		// (get) Token: 0x0600ED40 RID: 60736 RVA: 0x00361E27 File Offset: 0x00360027
		// (set) Token: 0x0600ED41 RID: 60737 RVA: 0x00361E4C File Offset: 0x0036004C
		[DefaultValue(1f)]
		[Description("Specifies the width of the grid line.")]
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		public override float Width
		{
			get
			{
				return (float)(base.ViewState["Width"] ?? 1f);
			}
			set
			{
				base.Width = value;
			}
		}

		// Token: 0x170047D4 RID: 18388
		// (get) Token: 0x0600ED42 RID: 60738 RVA: 0x00361E55 File Offset: 0x00360055
		// (set) Token: 0x0600ED43 RID: 60739 RVA: 0x00361E76 File Offset: 0x00360076
		[Browsable(true)]
		[Description("Specifies the pen style with which the grid lines are drawn.")]
		[DefaultValue(typeof(DashStyle), "Dot")]
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		public override DashStyle PenStyle
		{
			get
			{
				return (DashStyle)(base.ViewState["PenStyle"] ?? DashStyle.Dot);
			}
			set
			{
				base.PenStyle = value;
			}
		}

		// Token: 0x170047D5 RID: 18389
		// (get) Token: 0x0600ED44 RID: 60740 RVA: 0x00361E7F File Offset: 0x0036007F
		// (set) Token: 0x0600ED45 RID: 60741 RVA: 0x00361EA4 File Offset: 0x003600A4
		[TypeConverter(typeof(ColorConverter))]
		[SkinnableProperty]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Color), "38, 215, 215, 215")]
		public override Color Color
		{
			get
			{
				return (Color)(base.ViewState["Color"] ?? DefaultValues.DEFAULT_GRIDLINE_COLOR);
			}
			set
			{
				base.Color = value;
			}
		}

		// Token: 0x0600ED46 RID: 60742 RVA: 0x00361EAD File Offset: 0x003600AD
		internal bool ShouldRender(bool axisVisible)
		{
			if (axisVisible)
			{
				return this.Visible;
			}
			return !this.HideWithAxis && this.Visible;
		}

		// Token: 0x0600ED47 RID: 60743 RVA: 0x00361EC9 File Offset: 0x003600C9
		internal override void Reset()
		{
			base.Reset();
			this.HideWithAxis = true;
			this.Color = DefaultValues.DEFAULT_GRIDLINE_COLOR;
			this.Width = 1f;
			this.PenStyle = DashStyle.Dot;
		}
	}
}

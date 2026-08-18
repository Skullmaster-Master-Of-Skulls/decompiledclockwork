using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017E2 RID: 6114
	[ParseChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	public class StyleMarkerLegend : StyleMarker
	{
		// Token: 0x0600EDDD RID: 60893 RVA: 0x003635E0 File Offset: 0x003617E0
		public StyleMarkerLegend()
		{
			this.styleBorder = new StyleLegendBorder();
		}

		// Token: 0x170047FC RID: 18428
		// (get) Token: 0x0600EDDE RID: 60894 RVA: 0x003635F3 File Offset: 0x003617F3
		// (set) Token: 0x0600EDDF RID: 60895 RVA: 0x00363613 File Offset: 0x00361813
		[SkinnableProperty]
		[Description("Specifies the shape of the marker")]
		[Category("Figure")]
		[Editor(typeof(FiguresEditor), typeof(UITypeEditor))]
		[DefaultValue(typeof(string), "Ellipse")]
		[NotifyParentProperty(true)]
		public override string Figure
		{
			get
			{
				return (string)(base.ViewState["Figure"] ?? "Ellipse");
			}
			set
			{
				base.Figure = value;
			}
		}

		// Token: 0x170047FD RID: 18429
		// (get) Token: 0x0600EDE0 RID: 60896 RVA: 0x0036361C File Offset: 0x0036181C
		// (set) Token: 0x0600EDE1 RID: 60897 RVA: 0x0036363D File Offset: 0x0036183D
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		public override bool Visible
		{
			get
			{
				return (bool)(base.ViewState["Visible"] ?? true);
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x0600EDE2 RID: 60898 RVA: 0x00363646 File Offset: 0x00361846
		internal override void Reset()
		{
			base.Reset();
			this.Figure = "Ellipse";
			this.Visible = true;
		}
	}
}

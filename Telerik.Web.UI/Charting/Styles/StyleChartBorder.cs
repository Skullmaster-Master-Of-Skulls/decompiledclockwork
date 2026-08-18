using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017A1 RID: 6049
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	public class StyleChartBorder : StyleBorder
	{
		// Token: 0x1700475E RID: 18270
		// (get) Token: 0x0600EBBC RID: 60348 RVA: 0x0035A635 File Offset: 0x00358835
		// (set) Token: 0x0600EBBD RID: 60349 RVA: 0x0035A65A File Offset: 0x0035885A
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[DefaultValue(typeof(Color), "56, 56, 56")]
		[TypeConverter(typeof(ColorConverter))]
		[Description("Border color")]
		public override Color Color
		{
			get
			{
				return (Color)(base.ViewState["Color"] ?? DefaultValues.DEFAULT_CHART_BORDER_COLOR);
			}
			set
			{
				base.Color = value;
			}
		}

		// Token: 0x0600EBBE RID: 60350 RVA: 0x0035A663 File Offset: 0x00358863
		internal override void Reset()
		{
			base.Reset();
			this.Color = DefaultValues.DEFAULT_CHART_BORDER_COLOR;
		}
	}
}

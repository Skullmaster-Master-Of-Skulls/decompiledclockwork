using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017A0 RID: 6048
	[ParseChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	public class StyleLegendBorder : StyleBorder
	{
		// Token: 0x1700475D RID: 18269
		// (get) Token: 0x0600EBB8 RID: 60344 RVA: 0x0035A5EC File Offset: 0x003587EC
		// (set) Token: 0x0600EBB9 RID: 60345 RVA: 0x0035A611 File Offset: 0x00358811
		[SkinnableProperty]
		[DefaultValue(typeof(Color), "156, 156, 156")]
		[TypeConverter(typeof(ColorConverter))]
		[Description("Border color")]
		[NotifyParentProperty(true)]
		public override Color Color
		{
			get
			{
				return (Color)(base.ViewState["Color"] ?? DefaultValues.DEFAULT_LEGEND_BORDER_COLOR);
			}
			set
			{
				base.Color = value;
			}
		}

		// Token: 0x0600EBBA RID: 60346 RVA: 0x0035A61A File Offset: 0x0035881A
		internal override void Reset()
		{
			base.Reset();
			this.Color = DefaultValues.DEFAULT_LEGEND_BORDER_COLOR;
		}
	}
}

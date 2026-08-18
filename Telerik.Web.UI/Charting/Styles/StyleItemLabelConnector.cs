using System;
using System.ComponentModel;
using System.Drawing;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017A3 RID: 6051
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	public class StyleItemLabelConnector : LineStyle
	{
		// Token: 0x17004760 RID: 18272
		// (get) Token: 0x0600EBC5 RID: 60357 RVA: 0x0035A6CF File Offset: 0x003588CF
		// (set) Token: 0x0600EBC6 RID: 60358 RVA: 0x0035A6F4 File Offset: 0x003588F4
		[DefaultValue(typeof(Color), "Black")]
		[SkinnableProperty]
		[TypeConverter(typeof(ColorConverter))]
		[Description("Line color")]
		[NotifyParentProperty(true)]
		public override Color Color
		{
			get
			{
				return (Color)(base.ViewState["Color"] ?? DefaultValues.DEFAULT_SERIES_ITEM_LABEL_CONNECTOR_COLOR);
			}
			set
			{
				base.Color = value;
			}
		}

		// Token: 0x0600EBC7 RID: 60359 RVA: 0x0035A6FD File Offset: 0x003588FD
		internal override void Reset()
		{
			base.Reset();
			this.Color = DefaultValues.DEFAULT_SERIES_ITEM_LABEL_CONNECTOR_COLOR;
		}
	}
}

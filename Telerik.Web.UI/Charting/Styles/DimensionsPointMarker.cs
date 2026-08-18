using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x02001780 RID: 6016
	[ParseChildren(true)]
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class DimensionsPointMarker : DimensionsMarker
	{
		// Token: 0x1700471B RID: 18203
		// (get) Token: 0x0600EAB2 RID: 60082 RVA: 0x0035768B File Offset: 0x0035588B
		// (set) Token: 0x0600EAB3 RID: 60083 RVA: 0x00357693 File Offset: 0x00355893
		[SkinnableProperty]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x0600EAB4 RID: 60084 RVA: 0x0035769C File Offset: 0x0035589C
		public override object Clone()
		{
			Dimensions result = (DimensionsPointMarker)base.Clone();
			if (this.AutoSize)
			{
				this.ResetHeight();
				this.ResetWidth();
			}
			return result;
		}
	}
}

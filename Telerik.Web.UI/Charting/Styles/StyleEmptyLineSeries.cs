using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x0200179D RID: 6045
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	public class StyleEmptyLineSeries : StyleLineSeries
	{
		// Token: 0x17004759 RID: 18265
		// (get) Token: 0x0600EBAA RID: 60330 RVA: 0x0035A516 File Offset: 0x00358716
		// (set) Token: 0x0600EBAB RID: 60331 RVA: 0x0035A51E File Offset: 0x0035871E
		[SkinnableProperty]
		[TypeConverter(typeof(ColorConverter))]
		[DefaultValue(typeof(Color), "")]
		[Description("Empty line color")]
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override Color Color
		{
			get
			{
				return base.Color;
			}
			set
			{
				base.Color = value;
			}
		}

		// Token: 0x1700475A RID: 18266
		// (get) Token: 0x0600EBAC RID: 60332 RVA: 0x0035A527 File Offset: 0x00358727
		// (set) Token: 0x0600EBAD RID: 60333 RVA: 0x0035A548 File Offset: 0x00358748
		[SkinnableProperty]
		[DefaultValue(typeof(DashStyle), "Dash")]
		public override DashStyle PenStyle
		{
			get
			{
				return (DashStyle)(base.ViewState["PenStyle"] ?? DashStyle.Dash);
			}
			set
			{
				base.PenStyle = value;
			}
		}

		// Token: 0x0600EBAE RID: 60334 RVA: 0x0035A551 File Offset: 0x00358751
		internal override void Reset()
		{
			base.Reset();
			this.PenStyle = DashStyle.Dash;
		}
	}
}

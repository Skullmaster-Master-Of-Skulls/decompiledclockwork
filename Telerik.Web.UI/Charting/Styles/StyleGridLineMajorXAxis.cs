using System;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017D3 RID: 6099
	[PersistChildren(false)]
	[ParseChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class StyleGridLineMajorXAxis : StyleGridLineMajor
	{
		// Token: 0x170047D7 RID: 18391
		// (get) Token: 0x0600ED4F RID: 60751 RVA: 0x00361F4E File Offset: 0x0036014E
		// (set) Token: 0x0600ED50 RID: 60752 RVA: 0x00361F6F File Offset: 0x0036016F
		[Description("Specifies the pen style with which the grid lines are drawn.")]
		[SkinnableProperty]
		[Browsable(true)]
		[NotifyParentProperty(true)]
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

		// Token: 0x0600ED51 RID: 60753 RVA: 0x00361F78 File Offset: 0x00360178
		internal override void Reset()
		{
			base.Reset();
			this.PenStyle = DashStyle.Dash;
		}
	}
}

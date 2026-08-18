using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Gauge
{
	// Token: 0x02000B6E RID: 2926
	[ToolboxItem(false)]
	public class RadialScale : ScaleBase
	{
		// Token: 0x17002433 RID: 9267
		// (get) Token: 0x06006E55 RID: 28245 RVA: 0x00199246 File Offset: 0x00197446
		// (set) Token: 0x06006E56 RID: 28246 RVA: 0x00199268 File Offset: 0x00197468
		[Category("Behavior")]
		[DefaultValue(-30)]
		[Description("Gets or sets the start angle of the RadialGauge. The gauge is rendered clockwise(0 degrees are the 180 degrees in the polar coordinate system).")]
		public virtual int StartAngle
		{
			get
			{
				return (int)(base.ViewState["StartAngle"] ?? -30);
			}
			set
			{
				base.ViewState["StartAngle"] = value;
			}
		}

		// Token: 0x17002434 RID: 9268
		// (get) Token: 0x06006E57 RID: 28247 RVA: 0x00199280 File Offset: 0x00197480
		// (set) Token: 0x06006E58 RID: 28248 RVA: 0x001992A5 File Offset: 0x001974A5
		[Description("Gets or sets the end angle of the RadialGauge. The gauge is rendered clockwise(0 degrees are the 180 degrees in the polar coordinate system).")]
		[Category("Behavior")]
		[DefaultValue(210)]
		public virtual int EndAngle
		{
			get
			{
				return (int)(base.ViewState["EndAngle"] ?? 210);
			}
			set
			{
				base.ViewState["EndAngle"] = value;
			}
		}
	}
}

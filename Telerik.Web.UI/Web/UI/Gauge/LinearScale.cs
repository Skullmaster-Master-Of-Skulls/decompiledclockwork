using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Gauge
{
	// Token: 0x02000B6C RID: 2924
	[ToolboxItem(false)]
	public class LinearScale : ScaleBase
	{
		// Token: 0x17002431 RID: 9265
		// (get) Token: 0x06006E50 RID: 28240 RVA: 0x001991CC File Offset: 0x001973CC
		// (set) Token: 0x06006E51 RID: 28241 RVA: 0x001991ED File Offset: 0x001973ED
		[Description("Gets or sets a bool value indicating whether the LinearGauge will be vertically or horizontally positioned.")]
		[DefaultValue(true)]
		[Category("Behavior")]
		public virtual bool Vertical
		{
			get
			{
				return (bool)(base.ViewState["Vertical"] ?? true);
			}
			set
			{
				base.ViewState["Vertical"] = value;
			}
		}

		// Token: 0x17002432 RID: 9266
		// (get) Token: 0x06006E52 RID: 28242 RVA: 0x00199205 File Offset: 0x00197405
		// (set) Token: 0x06006E53 RID: 28243 RVA: 0x00199226 File Offset: 0x00197426
		[Category("Behavior")]
		[Description("Gets or sets a bool value that indicates whether the scale labels and ticks will be mirrored.")]
		[DefaultValue(false)]
		public virtual bool Mirror
		{
			get
			{
				return (bool)(base.ViewState["Mirror"] ?? false);
			}
			set
			{
				base.ViewState["Mirror"] = value;
			}
		}
	}
}

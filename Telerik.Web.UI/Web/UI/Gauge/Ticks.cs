using System;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.Web.UI.Gauge
{
	// Token: 0x02000B70 RID: 2928
	[ToolboxItem(false)]
	public class Ticks : StateManager
	{
		// Token: 0x1700243C RID: 9276
		// (get) Token: 0x06006E69 RID: 28265 RVA: 0x00199452 File Offset: 0x00197652
		// (set) Token: 0x06006E6A RID: 28266 RVA: 0x00199477 File Offset: 0x00197677
		[Category("Behavior")]
		[Description("Gets or sets the color of the ticks.")]
		[DefaultValue(typeof(Color), "")]
		public Color Color
		{
			get
			{
				return (Color)(base.ViewState["Color"] ?? Color.Empty);
			}
			set
			{
				base.ViewState["Color"] = value;
			}
		}

		// Token: 0x1700243D RID: 9277
		// (get) Token: 0x06006E6B RID: 28267 RVA: 0x0019948F File Offset: 0x0019768F
		// (set) Token: 0x06006E6C RID: 28268 RVA: 0x001994AB File Offset: 0x001976AB
		[Category("Behavior")]
		[Description("Gets or sets the size of the ticks.")]
		[DefaultValue(null)]
		public float? Size
		{
			get
			{
				return (float?)(base.ViewState["Size"] ?? null);
			}
			set
			{
				base.ViewState["Size"] = value;
			}
		}

		// Token: 0x1700243E RID: 9278
		// (get) Token: 0x06006E6D RID: 28269 RVA: 0x001994C3 File Offset: 0x001976C3
		// (set) Token: 0x06006E6E RID: 28270 RVA: 0x001994EC File Offset: 0x001976EC
		[Description("Gets or sets the width of the ticks.")]
		[Category("Behavior")]
		[DefaultValue(typeof(double), "0.5")]
		public double Width
		{
			get
			{
				return (double)(base.ViewState["Width"] ?? 0.5);
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x1700243F RID: 9279
		// (get) Token: 0x06006E6F RID: 28271 RVA: 0x00199504 File Offset: 0x00197704
		// (set) Token: 0x06006E70 RID: 28272 RVA: 0x00199525 File Offset: 0x00197725
		[Category("Behavior")]
		[Description("Gets or sets a bool value indicating whether the ticks of the Gauge's scale will be visible.")]
		[DefaultValue(true)]
		public bool Visible
		{
			get
			{
				return (bool)(base.ViewState["Visible"] ?? true);
			}
			set
			{
				base.ViewState["Visible"] = value;
			}
		}
	}
}

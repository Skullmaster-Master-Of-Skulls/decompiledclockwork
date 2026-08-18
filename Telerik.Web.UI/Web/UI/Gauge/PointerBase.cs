using System;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.Web.UI.Gauge
{
	// Token: 0x02000B62 RID: 2914
	[ToolboxItem(false)]
	public abstract class PointerBase : StateManager
	{
		// Token: 0x17002414 RID: 9236
		// (get) Token: 0x06006E0A RID: 28170 RVA: 0x00198908 File Offset: 0x00196B08
		// (set) Token: 0x06006E0B RID: 28171 RVA: 0x0019892D File Offset: 0x00196B2D
		[Description("Gets or sets the color of the pointer.")]
		[DefaultValue(typeof(Color), "")]
		[Category("Behavior")]
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

		// Token: 0x17002415 RID: 9237
		// (get) Token: 0x06006E0C RID: 28172 RVA: 0x00198945 File Offset: 0x00196B45
		// (set) Token: 0x06006E0D RID: 28173 RVA: 0x00198961 File Offset: 0x00196B61
		[DefaultValue(null)]
		[Description("Gets or sets the value at which the pointer is pointing.")]
		[Bindable(true)]
		[Category("Behavior")]
		public decimal? Value
		{
			get
			{
				return (decimal?)(base.ViewState["Value"] ?? null);
			}
			set
			{
				base.ViewState["Value"] = value;
			}
		}
	}
}

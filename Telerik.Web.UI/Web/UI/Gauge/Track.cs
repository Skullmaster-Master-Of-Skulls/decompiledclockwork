using System;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.Web.UI.Gauge
{
	// Token: 0x02000B64 RID: 2916
	[ToolboxItem(false)]
	public class Track : StateManager
	{
		// Token: 0x17002418 RID: 9240
		// (get) Token: 0x06006E14 RID: 28180 RVA: 0x001989FA File Offset: 0x00196BFA
		// (set) Token: 0x06006E15 RID: 28181 RVA: 0x00198A1F File Offset: 0x00196C1F
		[Category("Behavior")]
		[Description("Gets or sets the color of the track.")]
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

		// Token: 0x17002419 RID: 9241
		// (get) Token: 0x06006E16 RID: 28182 RVA: 0x00198A37 File Offset: 0x00196C37
		// (set) Token: 0x06006E17 RID: 28183 RVA: 0x00198A53 File Offset: 0x00196C53
		[Category("Behavior")]
		[Description("Gets or sets the size of the track.")]
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

		// Token: 0x1700241A RID: 9242
		// (get) Token: 0x06006E18 RID: 28184 RVA: 0x00198A6B File Offset: 0x00196C6B
		// (set) Token: 0x06006E19 RID: 28185 RVA: 0x00198A9A File Offset: 0x00196C9A
		[DefaultValue(1f)]
		[Description("Gets or sets the transparency of the track.")]
		[Category("Behavior")]
		public float Opacity
		{
			get
			{
				if (base.ViewState["Opacity"] == null)
				{
					return 1f;
				}
				return (float)base.ViewState["Opacity"];
			}
			set
			{
				base.ViewState["Opacity"] = value;
			}
		}

		// Token: 0x1700241B RID: 9243
		// (get) Token: 0x06006E1A RID: 28186 RVA: 0x00198AB2 File Offset: 0x00196CB2
		// (set) Token: 0x06006E1B RID: 28187 RVA: 0x00198AD3 File Offset: 0x00196CD3
		[Description("Gets or sets a bool value indicating whether the track of the LinearGauge pointer will be visible.")]
		[Category("Behavior")]
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

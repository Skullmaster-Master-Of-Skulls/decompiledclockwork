using System;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.Web.UI.Gauge
{
	// Token: 0x02000B63 RID: 2915
	[ToolboxItem(false)]
	public class Cap : StateManager
	{
		// Token: 0x17002416 RID: 9238
		// (get) Token: 0x06006E0F RID: 28175 RVA: 0x00198981 File Offset: 0x00196B81
		// (set) Token: 0x06006E10 RID: 28176 RVA: 0x001989A6 File Offset: 0x00196BA6
		[Description("Gets or sets the color of the cap.")]
		[Category("Behavior")]
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

		// Token: 0x17002417 RID: 9239
		// (get) Token: 0x06006E11 RID: 28177 RVA: 0x001989BE File Offset: 0x00196BBE
		// (set) Token: 0x06006E12 RID: 28178 RVA: 0x001989DA File Offset: 0x00196BDA
		[DefaultValue(null)]
		[Description("Gets or sets the size of the cap in percents. (from 0 to 1)")]
		[Category("Behavior")]
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
	}
}

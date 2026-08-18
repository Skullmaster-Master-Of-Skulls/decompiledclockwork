using System;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.Web.UI.Gauge
{
	// Token: 0x02000B5E RID: 2910
	public class Appearance : StateManager
	{
		// Token: 0x17002404 RID: 9220
		// (get) Token: 0x06006DD9 RID: 28121 RVA: 0x00197AF2 File Offset: 0x00195CF2
		// (set) Token: 0x06006DDA RID: 28122 RVA: 0x00197B17 File Offset: 0x00195D17
		[DefaultValue(typeof(Color), "White")]
		[Category("Behavior")]
		[Description("Gets or sets the color of the cap.")]
		public Color BackgroundColor
		{
			get
			{
				return (Color)(base.ViewState["BackgroundColor"] ?? Color.White);
			}
			set
			{
				base.ViewState["BackgroundColor"] = value;
			}
		}
	}
}

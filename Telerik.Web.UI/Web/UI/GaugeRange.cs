using System;
using System.ComponentModel;
using System.Drawing;

namespace Telerik.Web.UI
{
	// Token: 0x02000B69 RID: 2921
	[ToolboxItem(false)]
	public class GaugeRange : StateManager
	{
		// Token: 0x17002425 RID: 9253
		// (get) Token: 0x06006E34 RID: 28212 RVA: 0x00198DF4 File Offset: 0x00196FF4
		// (set) Token: 0x06006E35 RID: 28213 RVA: 0x00198E19 File Offset: 0x00197019
		[Description("Gets or sets the color of the range.")]
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

		// Token: 0x17002426 RID: 9254
		// (get) Token: 0x06006E36 RID: 28214 RVA: 0x00198E34 File Offset: 0x00197034
		// (set) Token: 0x06006E37 RID: 28215 RVA: 0x00198E6E File Offset: 0x0019706E
		[Description("Gets or sets the lower bound of the range.")]
		[Category("Behavior")]
		[DefaultValue(typeof(decimal), "0")]
		public decimal From
		{
			get
			{
				decimal? num = (decimal?)base.ViewState["From"];
				if (num == null)
				{
					return 0m;
				}
				return num.GetValueOrDefault();
			}
			set
			{
				base.ViewState["From"] = value;
			}
		}

		// Token: 0x17002427 RID: 9255
		// (get) Token: 0x06006E38 RID: 28216 RVA: 0x00198E88 File Offset: 0x00197088
		// (set) Token: 0x06006E39 RID: 28217 RVA: 0x00198EC2 File Offset: 0x001970C2
		[DefaultValue(typeof(decimal), "0")]
		[Category("Behavior")]
		[Description("Gets or sets the upper bound of the range.")]
		public decimal To
		{
			get
			{
				decimal? num = (decimal?)base.ViewState["To"];
				if (num == null)
				{
					return 0m;
				}
				return num.GetValueOrDefault();
			}
			set
			{
				base.ViewState["To"] = value;
			}
		}
	}
}

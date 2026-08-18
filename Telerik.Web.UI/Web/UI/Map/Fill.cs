using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Map
{
	// Token: 0x02000598 RID: 1432
	public class Fill : StateManager, IDefaultCheck
	{
		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x06003363 RID: 13155 RVA: 0x000AB242 File Offset: 0x000A9442
		// (set) Token: 0x06003364 RID: 13156 RVA: 0x000AB262 File Offset: 0x000A9462
		[DefaultValue("")]
		public string Color
		{
			get
			{
				return (string)(base.ViewState["Color"] ?? "");
			}
			set
			{
				base.ViewState["Color"] = value;
			}
		}

		// Token: 0x170010AF RID: 4271
		// (get) Token: 0x06003365 RID: 13157 RVA: 0x000AB275 File Offset: 0x000A9475
		// (set) Token: 0x06003366 RID: 13158 RVA: 0x000AB29E File Offset: 0x000A949E
		[DefaultValue(0.0)]
		public double Opacity
		{
			get
			{
				return (double)(base.ViewState["Opacity"] ?? 0.0);
			}
			set
			{
				base.ViewState["Opacity"] = value;
			}
		}

		// Token: 0x170010B0 RID: 4272
		// (get) Token: 0x06003367 RID: 13159 RVA: 0x000AB2B6 File Offset: 0x000A94B6
		public bool IsDefault
		{
			get
			{
				return this.Color == "" && this.Opacity == 0.0;
			}
		}
	}
}

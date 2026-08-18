using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Map
{
	// Token: 0x02000592 RID: 1426
	public class Close : StateManager, IDefaultCheck
	{
		// Token: 0x1700109F RID: 4255
		// (get) Token: 0x06003341 RID: 13121 RVA: 0x000AAD52 File Offset: 0x000A8F52
		// (set) Token: 0x06003342 RID: 13122 RVA: 0x000AAD72 File Offset: 0x000A8F72
		[DefaultValue("")]
		public string Effects
		{
			get
			{
				return (string)(base.ViewState["Effects"] ?? "");
			}
			set
			{
				base.ViewState["Effects"] = value;
			}
		}

		// Token: 0x170010A0 RID: 4256
		// (get) Token: 0x06003343 RID: 13123 RVA: 0x000AAD85 File Offset: 0x000A8F85
		// (set) Token: 0x06003344 RID: 13124 RVA: 0x000AADAE File Offset: 0x000A8FAE
		[DefaultValue(0.0)]
		public double Duration
		{
			get
			{
				return (double)(base.ViewState["Duration"] ?? 0.0);
			}
			set
			{
				base.ViewState["Duration"] = value;
			}
		}

		// Token: 0x170010A1 RID: 4257
		// (get) Token: 0x06003345 RID: 13125 RVA: 0x000AADC6 File Offset: 0x000A8FC6
		public bool IsDefault
		{
			get
			{
				return this.Effects == "" && this.Duration == 0.0;
			}
		}
	}
}

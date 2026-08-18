using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Map
{
	// Token: 0x02000594 RID: 1428
	public class Content : StateManager, IDefaultCheck
	{
		// Token: 0x170010A3 RID: 4259
		// (get) Token: 0x0600334A RID: 13130 RVA: 0x000AAE72 File Offset: 0x000A9072
		// (set) Token: 0x0600334B RID: 13131 RVA: 0x000AAE92 File Offset: 0x000A9092
		[DefaultValue("")]
		public string Url
		{
			get
			{
				return (string)(base.ViewState["Url"] ?? "");
			}
			set
			{
				base.ViewState["Url"] = value;
			}
		}

		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x0600334C RID: 13132 RVA: 0x000AAEA5 File Offset: 0x000A90A5
		public bool IsDefault
		{
			get
			{
				return this.Url == "";
			}
		}
	}
}

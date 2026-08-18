using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Breadcrumb
{
	// Token: 0x02000011 RID: 17
	public class Messages : StateManager, IDefaultCheck
	{
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000392D File Offset: 0x00001B2D
		// (set) Token: 0x0600010C RID: 268 RVA: 0x0000394D File Offset: 0x00001B4D
		[DefaultValue("Go to root")]
		public string RootTitle
		{
			get
			{
				return (string)(base.ViewState["RootTitle"] ?? "Go to root");
			}
			set
			{
				base.ViewState["RootTitle"] = value;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00003960 File Offset: 0x00001B60
		public bool IsDefault
		{
			get
			{
				return this.RootTitle == "Go to root";
			}
		}
	}
}

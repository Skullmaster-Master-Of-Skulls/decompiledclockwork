using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Chat
{
	// Token: 0x0200008C RID: 140
	public class User : StateManager, IDefaultCheck
	{
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x0000D936 File Offset: 0x0000BB36
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x0000D956 File Offset: 0x0000BB56
		[DefaultValue("")]
		public string IconUrl
		{
			get
			{
				return (string)(base.ViewState["IconUrl"] ?? "");
			}
			set
			{
				base.ViewState["IconUrl"] = value;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x0000D969 File Offset: 0x0000BB69
		// (set) Token: 0x06000573 RID: 1395 RVA: 0x0000D989 File Offset: 0x0000BB89
		[DefaultValue("User")]
		public string Name
		{
			get
			{
				return (string)(base.ViewState["Name"] ?? "User");
			}
			set
			{
				base.ViewState["Name"] = value;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0000D99C File Offset: 0x0000BB9C
		public bool IsDefault
		{
			get
			{
				return this.IconUrl == "" && this.Name == "User";
			}
		}
	}
}

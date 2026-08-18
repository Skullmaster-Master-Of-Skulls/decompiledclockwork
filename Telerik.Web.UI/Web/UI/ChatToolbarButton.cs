using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000080 RID: 128
	public class ChatToolbarButton : StateManager
	{
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0000D02A File Offset: 0x0000B22A
		// (set) Token: 0x06000537 RID: 1335 RVA: 0x0000D04A File Offset: 0x0000B24A
		[DefaultValue("")]
		public string Name
		{
			get
			{
				return (string)(base.ViewState["Name"] ?? "");
			}
			set
			{
				base.ViewState["Name"] = value;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x0000D05D File Offset: 0x0000B25D
		// (set) Token: 0x06000539 RID: 1337 RVA: 0x0000D07D File Offset: 0x0000B27D
		[DefaultValue("")]
		public string Text
		{
			get
			{
				return (string)(base.ViewState["Text"] ?? "");
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0000D090 File Offset: 0x0000B290
		// (set) Token: 0x0600053B RID: 1339 RVA: 0x0000D0B0 File Offset: 0x0000B2B0
		[DefaultValue("")]
		public string IconClass
		{
			get
			{
				return (string)(base.ViewState["IconClass"] ?? "");
			}
			set
			{
				base.ViewState["IconClass"] = value;
			}
		}
	}
}

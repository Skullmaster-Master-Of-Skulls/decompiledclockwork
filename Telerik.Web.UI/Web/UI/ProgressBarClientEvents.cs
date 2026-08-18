using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000773 RID: 1907
	public class ProgressBarClientEvents : StateManager
	{
		// Token: 0x170015EA RID: 5610
		// (get) Token: 0x06004350 RID: 17232 RVA: 0x000D2B39 File Offset: 0x000D0D39
		// (set) Token: 0x06004351 RID: 17233 RVA: 0x000D2B59 File Offset: 0x000D0D59
		[ClientControlEvent]
		[Description("Specifies the client-side script that executes when a RadProgressBar ClientInitialize event is raised.")]
		[Category("Client-side events")]
		[ClientPropertyName("initialize")]
		[DefaultValue("")]
		public string OnInitialize
		{
			get
			{
				return (string)(base.ViewState["OnInitialize"] ?? "");
			}
			set
			{
				base.ViewState["OnInitialize"] = value;
			}
		}

		// Token: 0x170015EB RID: 5611
		// (get) Token: 0x06004352 RID: 17234 RVA: 0x000D2B6C File Offset: 0x000D0D6C
		// (set) Token: 0x06004353 RID: 17235 RVA: 0x000D2B8C File Offset: 0x000D0D8C
		[Description("Specifies the client-side script that executes when a RadProgressBar ClientLoad event is raised.")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("load")]
		public string OnLoad
		{
			get
			{
				return (string)(base.ViewState["OnLoad"] ?? "");
			}
			set
			{
				base.ViewState["OnLoad"] = value;
			}
		}

		// Token: 0x170015EC RID: 5612
		// (get) Token: 0x06004354 RID: 17236 RVA: 0x000D2B9F File Offset: 0x000D0D9F
		// (set) Token: 0x06004355 RID: 17237 RVA: 0x000D2BBF File Offset: 0x000D0DBF
		[ClientControlEvent]
		[Category("Client-side events")]
		[Description("Gets or sets the client-side script that executes before the progress bar value property is changed.")]
		[DefaultValue("")]
		[ClientPropertyName("valueChanging")]
		public string OnValueChanging
		{
			get
			{
				return (string)(base.ViewState["OnValueChanging"] ?? "");
			}
			set
			{
				base.ViewState["OnValueChanging"] = value;
			}
		}

		// Token: 0x170015ED RID: 5613
		// (get) Token: 0x06004356 RID: 17238 RVA: 0x000D2BD2 File Offset: 0x000D0DD2
		// (set) Token: 0x06004357 RID: 17239 RVA: 0x000D2BF2 File Offset: 0x000D0DF2
		[Category("Client-side events")]
		[Description("Gets or sets the client-side script that executes after the progress bar value property is changed.")]
		[ClientControlEvent]
		[ClientPropertyName("valueChanged")]
		[DefaultValue("")]
		public string OnValueChanged
		{
			get
			{
				return (string)(base.ViewState["OnValueChanged"] ?? "");
			}
			set
			{
				base.ViewState["OnValueChanged"] = value;
			}
		}

		// Token: 0x170015EE RID: 5614
		// (get) Token: 0x06004358 RID: 17240 RVA: 0x000D2C05 File Offset: 0x000D0E05
		// (set) Token: 0x06004359 RID: 17241 RVA: 0x000D2C25 File Offset: 0x000D0E25
		[ClientPropertyName("completed")]
		[ClientControlEvent]
		[Description("Gets or sets the client-side script that executes after the progress bar value reaches its max value.")]
		[DefaultValue("")]
		[Category("Client-side events")]
		public string OnCompleted
		{
			get
			{
				return (string)(base.ViewState["OnCompleted"] ?? "");
			}
			set
			{
				base.ViewState["OnCompleted"] = value;
			}
		}
	}
}

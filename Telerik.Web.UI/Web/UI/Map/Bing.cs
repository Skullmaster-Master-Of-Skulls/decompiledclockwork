using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Map
{
	// Token: 0x02000590 RID: 1424
	public class Bing : StateManager, IDefaultCheck
	{
		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x06003332 RID: 13106 RVA: 0x000AAB1A File Offset: 0x000A8D1A
		// (set) Token: 0x06003333 RID: 13107 RVA: 0x000AAB3A File Offset: 0x000A8D3A
		[DefaultValue("")]
		public string Attribution
		{
			get
			{
				return (string)(base.ViewState["Attribution"] ?? "");
			}
			set
			{
				base.ViewState["Attribution"] = value;
			}
		}

		// Token: 0x17001099 RID: 4249
		// (get) Token: 0x06003334 RID: 13108 RVA: 0x000AAB4D File Offset: 0x000A8D4D
		// (set) Token: 0x06003335 RID: 13109 RVA: 0x000AAB76 File Offset: 0x000A8D76
		[DefaultValue(1.0)]
		public double Opacity
		{
			get
			{
				return (double)(base.ViewState["Opacity"] ?? 1.0);
			}
			set
			{
				base.ViewState["Opacity"] = value;
			}
		}

		// Token: 0x1700109A RID: 4250
		// (get) Token: 0x06003336 RID: 13110 RVA: 0x000AAB8E File Offset: 0x000A8D8E
		// (set) Token: 0x06003337 RID: 13111 RVA: 0x000AABAE File Offset: 0x000A8DAE
		[DefaultValue("")]
		public string Key
		{
			get
			{
				return (string)(base.ViewState["Key"] ?? "");
			}
			set
			{
				base.ViewState["Key"] = value;
			}
		}

		// Token: 0x1700109B RID: 4251
		// (get) Token: 0x06003338 RID: 13112 RVA: 0x000AABC1 File Offset: 0x000A8DC1
		// (set) Token: 0x06003339 RID: 13113 RVA: 0x000AABE1 File Offset: 0x000A8DE1
		[DefaultValue("road")]
		public string ImagerySet
		{
			get
			{
				return (string)(base.ViewState["ImagerySet"] ?? "road");
			}
			set
			{
				base.ViewState["ImagerySet"] = value;
			}
		}

		// Token: 0x1700109C RID: 4252
		// (get) Token: 0x0600333A RID: 13114 RVA: 0x000AABF4 File Offset: 0x000A8DF4
		// (set) Token: 0x0600333B RID: 13115 RVA: 0x000AAC14 File Offset: 0x000A8E14
		[DefaultValue("en-US")]
		public string Culture
		{
			get
			{
				return (string)(base.ViewState["Culture"] ?? "en-US");
			}
			set
			{
				base.ViewState["Culture"] = value;
			}
		}

		// Token: 0x1700109D RID: 4253
		// (get) Token: 0x0600333C RID: 13116 RVA: 0x000AAC28 File Offset: 0x000A8E28
		public bool IsDefault
		{
			get
			{
				return this.Attribution == "" && this.Opacity == 1.0 && this.Key == "" && this.ImagerySet == "road" && this.Culture == "en-US";
			}
		}
	}
}

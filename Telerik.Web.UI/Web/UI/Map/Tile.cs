using System;
using System.ComponentModel;
using Telerik.Web.Design;

namespace Telerik.Web.UI.Map
{
	// Token: 0x020005B5 RID: 1461
	public class Tile : StateManager, IDefaultCheck
	{
		// Token: 0x170010FF RID: 4351
		// (get) Token: 0x06003422 RID: 13346 RVA: 0x000AD046 File Offset: 0x000AB246
		// (set) Token: 0x06003423 RID: 13347 RVA: 0x000AD066 File Offset: 0x000AB266
		[DefaultValue("")]
		public string UrlTemplate
		{
			get
			{
				return (string)(base.ViewState["UrlTemplate"] ?? "");
			}
			set
			{
				base.ViewState["UrlTemplate"] = value;
			}
		}

		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x06003424 RID: 13348 RVA: 0x000AD079 File Offset: 0x000AB279
		// (set) Token: 0x06003425 RID: 13349 RVA: 0x000AD099 File Offset: 0x000AB299
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

		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x06003426 RID: 13350 RVA: 0x000AD0AC File Offset: 0x000AB2AC
		// (set) Token: 0x06003427 RID: 13351 RVA: 0x000AD0C8 File Offset: 0x000AB2C8
		[DefaultValue(null)]
		[TypeConverter(typeof(ListConverter))]
		public string[] Subdomains
		{
			get
			{
				return (string[])(base.ViewState["Subdomains"] ?? null);
			}
			set
			{
				base.ViewState["Subdomains"] = value;
			}
		}

		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x06003428 RID: 13352 RVA: 0x000AD0DB File Offset: 0x000AB2DB
		// (set) Token: 0x06003429 RID: 13353 RVA: 0x000AD104 File Offset: 0x000AB304
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

		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x0600342A RID: 13354 RVA: 0x000AD11C File Offset: 0x000AB31C
		public bool IsDefault
		{
			get
			{
				return this.UrlTemplate == "" && this.Attribution == "" && this.Subdomains == null && this.Opacity == 1.0;
			}
		}
	}
}

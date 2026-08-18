using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.DataSourceSettings
{
	// Token: 0x02000107 RID: 263
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class WebServiceBaseSettings : StateManager
	{
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000B0C RID: 2828 RVA: 0x00027353 File Offset: 0x00025553
		internal bool ShouldSerializeDataType
		{
			get
			{
				return this.DataType != ClientDataSourceDataType.JSON;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000B0D RID: 2829 RVA: 0x00027361 File Offset: 0x00025561
		internal bool ShouldSerializeContentType
		{
			get
			{
				return this.ContentType.ToLower() != "json";
			}
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000B0E RID: 2830 RVA: 0x00027378 File Offset: 0x00025578
		internal bool ShouldSerializeUrl
		{
			get
			{
				return !string.IsNullOrEmpty(this.Url);
			}
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x00027388 File Offset: 0x00025588
		internal bool ShouldSerializeHttpMethod
		{
			get
			{
				return this.RequestType != ClientDataSourceHttpMethod.Get;
			}
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x00027398 File Offset: 0x00025598
		// (set) Token: 0x06000B11 RID: 2833 RVA: 0x000273C1 File Offset: 0x000255C1
		[DefaultValue(ClientDataSourceHttpMethod.Get)]
		[Category("Client")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the type of the request that will be used to send the data to the service, Default is Get")]
		public virtual ClientDataSourceHttpMethod RequestType
		{
			get
			{
				object obj = base.ViewState["RequestType"];
				if (obj != null)
				{
					return (ClientDataSourceHttpMethod)obj;
				}
				return ClientDataSourceHttpMethod.Get;
			}
			set
			{
				base.ViewState["RequestType"] = value;
			}
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x000273DC File Offset: 0x000255DC
		// (set) Token: 0x06000B13 RID: 2835 RVA: 0x00027405 File Offset: 0x00025605
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(ClientDataSourceDataType), "JSON")]
		[Category("Client")]
		[Description("Gets or sets the data type that is expected from the data service.")]
		public virtual ClientDataSourceDataType DataType
		{
			get
			{
				object obj = base.ViewState["DataType"];
				if (obj != null)
				{
					return (ClientDataSourceDataType)obj;
				}
				return ClientDataSourceDataType.JSON;
			}
			set
			{
				base.ViewState["DataType"] = value;
			}
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000B14 RID: 2836 RVA: 0x00027420 File Offset: 0x00025620
		// (set) Token: 0x06000B15 RID: 2837 RVA: 0x0002744D File Offset: 0x0002564D
		[NotifyParentProperty(true)]
		[Description("Gets or sets the content type that is expected from the data service.")]
		[Category("Client")]
		[DefaultValue("json")]
		public virtual string ContentType
		{
			get
			{
				object obj = base.ViewState["ContentType"];
				if (obj != null)
				{
					return obj.ToString();
				}
				return "json";
			}
			set
			{
				base.ViewState["ContentType"] = value;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x00027460 File Offset: 0x00025660
		// (set) Token: 0x06000B17 RID: 2839 RVA: 0x00027489 File Offset: 0x00025689
		[Description("Enables or disables client-side data caching in RadClientDataSource. Caching is disabled by default.")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Category("Client")]
		public virtual bool EnableCaching
		{
			get
			{
				object obj = base.ViewState["EnableCaching"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["EnableCaching"] = value;
			}
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x000274A1 File Offset: 0x000256A1
		// (set) Token: 0x06000B19 RID: 2841 RVA: 0x000274C1 File Offset: 0x000256C1
		[Category("Client")]
		[NotifyParentProperty(true)]
		[UrlProperty]
		[DefaultValue("")]
		[Description("Gets or sets the base URL of the web service.")]
		public virtual string Url
		{
			get
			{
				return (base.ViewState["CDSUrl"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["CDSUrl"] = value;
			}
		}
	}
}

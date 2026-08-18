using System;
using System.ComponentModel;

namespace Telerik.Web.UI.ODataSource.Transport
{
	// Token: 0x02000BD8 RID: 3032
	public abstract class CrudBase
	{
		// Token: 0x060073B1 RID: 29617 RVA: 0x001B04C2 File Offset: 0x001AE6C2
		public CrudBase()
		{
			this._url = null;
			this._dataType = ODataSourceResponseType.JSONP;
		}

		// Token: 0x170025AD RID: 9645
		// (get) Token: 0x060073B2 RID: 29618 RVA: 0x001B04D8 File Offset: 0x001AE6D8
		// (set) Token: 0x060073B3 RID: 29619 RVA: 0x001B04E0 File Offset: 0x001AE6E0
		[DefaultValue(null)]
		[Description("Gets or sets the data service url for CRUD operation")]
		[Category("Behavior")]
		public Uri Url
		{
			get
			{
				return this._url;
			}
			set
			{
				this._url = value;
			}
		}

		// Token: 0x170025AE RID: 9646
		// (get) Token: 0x060073B4 RID: 29620 RVA: 0x001B04E9 File Offset: 0x001AE6E9
		// (set) Token: 0x060073B5 RID: 29621 RVA: 0x001B04F1 File Offset: 0x001AE6F1
		[Category("Behavior")]
		[DefaultValue(ODataSourceResponseType.JSONP)]
		[Description("Gets or sets the type of the response. It could be JSON or JSONP, should the request is cross domain.")]
		public ODataSourceResponseType DataType
		{
			get
			{
				return this._dataType;
			}
			set
			{
				this._dataType = value;
			}
		}

		// Token: 0x04001F74 RID: 8052
		private Uri _url;

		// Token: 0x04001F75 RID: 8053
		private ODataSourceResponseType _dataType;
	}
}

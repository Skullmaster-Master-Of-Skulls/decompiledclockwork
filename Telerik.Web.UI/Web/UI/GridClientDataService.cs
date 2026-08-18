using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001900 RID: 6400
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridClientDataService : ObjectWithState
	{
		// Token: 0x0600F710 RID: 63248 RVA: 0x0038101C File Offset: 0x0037F21C
		public GridClientDataService(StateBag OwnerStateBag) : base("cs_dbds_", OwnerStateBag)
		{
		}

		// Token: 0x17004A63 RID: 19043
		// (get) Token: 0x0600F711 RID: 63249 RVA: 0x0038102A File Offset: 0x0037F22A
		// (set) Token: 0x0600F712 RID: 63250 RVA: 0x0038104A File Offset: 0x0037F24A
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Category("Client")]
		[Description("Gets or set table name for the specified ADO.NET DataService. Default is empty string!")]
		public virtual string TableName
		{
			get
			{
				return ((string)base.ViewState["TableName"]) ?? "";
			}
			set
			{
				base.ViewState["TableName"] = value;
			}
		}

		// Token: 0x17004A64 RID: 19044
		// (get) Token: 0x0600F713 RID: 63251 RVA: 0x00381060 File Offset: 0x0037F260
		// (set) Token: 0x0600F714 RID: 63252 RVA: 0x00381089 File Offset: 0x0037F289
		[NotifyParentProperty(true)]
		[DefaultValue(GridClientDataServiceType.ADONet)]
		[Category("Client")]
		[Description("Gets or sets the client data service type RadGrid binds to.")]
		public GridClientDataServiceType Type
		{
			get
			{
				object obj = base.ViewState["GridClientDataServiceType"];
				if (obj == null)
				{
					return GridClientDataServiceType.ADONet;
				}
				return (GridClientDataServiceType)obj;
			}
			set
			{
				base.ViewState["GridClientDataServiceType"] = value;
			}
		}

		// Token: 0x17004A65 RID: 19045
		// (get) Token: 0x0600F715 RID: 63253 RVA: 0x003810A1 File Offset: 0x0037F2A1
		// (set) Token: 0x0600F716 RID: 63254 RVA: 0x003810C1 File Offset: 0x0037F2C1
		[Category("Client")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("Gets or set a filter query option for the specified ADO.NET DataService. Default is empty string!")]
		public virtual string FilterQueryOption
		{
			get
			{
				return ((string)base.ViewState["FilterQueryOption"]) ?? "";
			}
			set
			{
				base.ViewState["FilterQueryOption"] = value;
			}
		}

		// Token: 0x17004A66 RID: 19046
		// (get) Token: 0x0600F717 RID: 63255 RVA: 0x003810D4 File Offset: 0x0037F2D4
		// (set) Token: 0x0600F718 RID: 63256 RVA: 0x003810F4 File Offset: 0x0037F2F4
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Category("Client")]
		[Description("Gets or set an orderby query option for the specified ADO.NET DataService. Default is empty string!")]
		public virtual string SortQueryOption
		{
			get
			{
				return ((string)base.ViewState["SortQueryOption"]) ?? "";
			}
			set
			{
				base.ViewState["SortQueryOption"] = value;
			}
		}
	}
}

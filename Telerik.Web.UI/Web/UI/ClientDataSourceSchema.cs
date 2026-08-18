using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000113 RID: 275
	public class ClientDataSourceSchema : StateManager
	{
		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000B44 RID: 2884 RVA: 0x00027A15 File Offset: 0x00025C15
		internal bool ShouldSerializeDataName
		{
			get
			{
				return !string.IsNullOrEmpty(this.DataName);
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x00027A25 File Offset: 0x00025C25
		internal bool ShouldSerializeAggregateResultsName
		{
			get
			{
				return !string.IsNullOrEmpty(this.AggregateResultsName);
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000B46 RID: 2886 RVA: 0x00027A35 File Offset: 0x00025C35
		internal bool ShouldSerializeGroupsName
		{
			get
			{
				return !string.IsNullOrEmpty(this.GroupsName);
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000B47 RID: 2887 RVA: 0x00027A45 File Offset: 0x00025C45
		internal bool ShouldSerializeErrorsName
		{
			get
			{
				return !string.IsNullOrEmpty(this.ErrorsName);
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000B48 RID: 2888 RVA: 0x00027A55 File Offset: 0x00025C55
		internal bool ShouldSerializeTotalName
		{
			get
			{
				return !string.IsNullOrEmpty(this.TotalName);
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000B49 RID: 2889 RVA: 0x00027A65 File Offset: 0x00025C65
		internal bool ShouldSerializeResponseType
		{
			get
			{
				return this.ResponseType != ClientDataSourceDataType.JSON;
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x00027A73 File Offset: 0x00025C73
		// (set) Token: 0x06000B4B RID: 2891 RVA: 0x00027A8F File Offset: 0x00025C8F
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the model of the Schema")]
		public virtual ClientDataSourceModel Model
		{
			get
			{
				return (base.ViewState["Model"] as ClientDataSourceModel) ?? null;
			}
			set
			{
				base.ViewState["Model"] = value;
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x00027AA4 File Offset: 0x00025CA4
		// (set) Token: 0x06000B4D RID: 2893 RVA: 0x00027ACD File Offset: 0x00025CCD
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(ClientDataSourceDataType), "JSON")]
		[Description("Gets or sets the data type of the server response. Only JSON and XML are supported")]
		public virtual ClientDataSourceDataType ResponseType
		{
			get
			{
				object obj = base.ViewState["ResponseType"];
				if (obj != null)
				{
					return (ClientDataSourceDataType)obj;
				}
				return ClientDataSourceDataType.JSON;
			}
			set
			{
				base.ViewState["ResponseType"] = value;
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x00027AE5 File Offset: 0x00025CE5
		// (set) Token: 0x06000B4F RID: 2895 RVA: 0x00027B05 File Offset: 0x00025D05
		[DefaultValue("")]
		[Description("Gets or sets the name of the collection that holds the data items")]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		public virtual string DataName
		{
			get
			{
				return (base.ViewState["DataName"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["DataName"] = value;
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x00027B18 File Offset: 0x00025D18
		// (set) Token: 0x06000B51 RID: 2897 RVA: 0x00027B38 File Offset: 0x00025D38
		[NotifyParentProperty(true)]
		[Description("Gets or sets the name of the field from the response which contains the aggregate results")]
		[Category("Behavior")]
		[DefaultValue("")]
		public virtual string AggregateResultsName
		{
			get
			{
				return (base.ViewState["AggregateResultsName"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["AggregateResultsName"] = value;
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00027B4B File Offset: 0x00025D4B
		// (set) Token: 0x06000B53 RID: 2899 RVA: 0x00027B6B File Offset: 0x00025D6B
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the field from the server response which contains the groups")]
		public virtual string GroupsName
		{
			get
			{
				return (base.ViewState["GroupsName"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["GroupsName"] = value;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000B54 RID: 2900 RVA: 0x00027B7E File Offset: 0x00025D7E
		// (set) Token: 0x06000B55 RID: 2901 RVA: 0x00027B9E File Offset: 0x00025D9E
		[Description("Gets or sets the name of the field from the server response which contains server-side errors")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Category("Behavior")]
		public virtual string ErrorsName
		{
			get
			{
				return (base.ViewState["ErrorsName"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["ErrorsName"] = value;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000B56 RID: 2902 RVA: 0x00027BB1 File Offset: 0x00025DB1
		// (set) Token: 0x06000B57 RID: 2903 RVA: 0x00027BD1 File Offset: 0x00025DD1
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("Gets or sets the name of the field from the server response which contains the total number of data items")]
		public virtual string TotalName
		{
			get
			{
				return (base.ViewState["TotalName"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["TotalName"] = value;
			}
		}
	}
}

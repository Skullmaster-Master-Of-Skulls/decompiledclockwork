using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020018FD RID: 6397
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridClientDataBinding : ObjectWithState
	{
		// Token: 0x0600F6F1 RID: 63217 RVA: 0x00380CBF File Offset: 0x0037EEBF
		public GridClientDataBinding(StateBag OwnerStateBag) : base("cs_db_", OwnerStateBag)
		{
		}

		// Token: 0x17004A53 RID: 19027
		// (get) Token: 0x0600F6F2 RID: 63218 RVA: 0x00380CCD File Offset: 0x0037EECD
		[Category("Client")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public GridClientDataService DataService
		{
			get
			{
				if (this._dataService == null)
				{
					this._dataService = new GridClientDataService(base.OwnerViewState);
				}
				return this._dataService;
			}
		}

		// Token: 0x17004A54 RID: 19028
		// (get) Token: 0x0600F6F3 RID: 63219 RVA: 0x00380CEE File Offset: 0x0037EEEE
		// (set) Token: 0x0600F6F4 RID: 63220 RVA: 0x00380D0E File Offset: 0x0037EF0E
		[UrlProperty]
		[NotifyParentProperty(true)]
		[Category("Client")]
		[Description("Gets or sets url for the WebService or Page which will be requested to get data.")]
		[DefaultValue("")]
		public virtual string Location
		{
			get
			{
				return ((string)base.ViewState["Location"]) ?? "";
			}
			set
			{
				base.ViewState["Location"] = value;
			}
		}

		// Token: 0x17004A55 RID: 19029
		// (get) Token: 0x0600F6F5 RID: 63221 RVA: 0x00380D21 File Offset: 0x0037EF21
		// (set) Token: 0x0600F6F6 RID: 63222 RVA: 0x00380D41 File Offset: 0x0037EF41
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Category("Client")]
		[Description("Gets or sets method name in the WebService or Page which will be requested to get data.")]
		public virtual string SelectMethod
		{
			get
			{
				return ((string)base.ViewState["SelectMethod"]) ?? "";
			}
			set
			{
				base.ViewState["SelectMethod"] = value;
			}
		}

		// Token: 0x17004A56 RID: 19030
		// (get) Token: 0x0600F6F7 RID: 63223 RVA: 0x00380D54 File Offset: 0x0037EF54
		// (set) Token: 0x0600F6F8 RID: 63224 RVA: 0x00380D74 File Offset: 0x0037EF74
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Category("Client")]
		[Description("Gets or sets method name in the WebService or Page which will be requested to get total records count.")]
		public virtual string SelectCountMethod
		{
			get
			{
				return ((string)base.ViewState["SelectCountMethod"]) ?? "";
			}
			set
			{
				base.ViewState["SelectCountMethod"] = value;
			}
		}

		// Token: 0x17004A57 RID: 19031
		// (get) Token: 0x0600F6F9 RID: 63225 RVA: 0x00380D87 File Offset: 0x0037EF87
		// (set) Token: 0x0600F6FA RID: 63226 RVA: 0x00380DA7 File Offset: 0x0037EFA7
		[Description("Gets or sets maximum rows parameter name for the SelectMethod in the WebService or Page which will be requested to get data.")]
		[DefaultValue("maximumRows")]
		[NotifyParentProperty(true)]
		[Category("Client")]
		public virtual string MaximumRowsParameterName
		{
			get
			{
				return ((string)base.ViewState["MaximumRowsParameterName"]) ?? "maximumRows";
			}
			set
			{
				base.ViewState["MaximumRowsParameterName"] = value;
			}
		}

		// Token: 0x17004A58 RID: 19032
		// (get) Token: 0x0600F6FB RID: 63227 RVA: 0x00380DBA File Offset: 0x0037EFBA
		// (set) Token: 0x0600F6FC RID: 63228 RVA: 0x00380DDA File Offset: 0x0037EFDA
		[DefaultValue("startRowIndex")]
		[NotifyParentProperty(true)]
		[Category("Client")]
		[Description("Gets or start row index parameter name for the SelectMethod in the WebService or Page which will be requested to get data.")]
		public virtual string StartRowIndexParameterName
		{
			get
			{
				return ((string)base.ViewState["StartRowIndexParameterName"]) ?? "startRowIndex";
			}
			set
			{
				base.ViewState["StartRowIndexParameterName"] = value;
			}
		}

		// Token: 0x17004A59 RID: 19033
		// (get) Token: 0x0600F6FD RID: 63229 RVA: 0x00380DED File Offset: 0x0037EFED
		// (set) Token: 0x0600F6FE RID: 63230 RVA: 0x00380E0D File Offset: 0x0037F00D
		[Category("Client")]
		[NotifyParentProperty(true)]
		[DefaultValue("sortExpression")]
		[Description("Gets or set sort parameter name for the SelectMethod in the WebService or Page which will be requested to get data.")]
		public virtual string SortParameterName
		{
			get
			{
				return ((string)base.ViewState["SortParameterName"]) ?? "sortExpression";
			}
			set
			{
				base.ViewState["SortParameterName"] = value;
			}
		}

		// Token: 0x17004A5A RID: 19034
		// (get) Token: 0x0600F6FF RID: 63231 RVA: 0x00380E20 File Offset: 0x0037F020
		// (set) Token: 0x0600F700 RID: 63232 RVA: 0x00380E40 File Offset: 0x0037F040
		[Category("Client")]
		[NotifyParentProperty(true)]
		[DefaultValue("filterExpression")]
		[Description("Gets or set filter parameter name for the SelectMethod in the WebService or Page which will be requested to get data.")]
		public virtual string FilterParameterName
		{
			get
			{
				return ((string)base.ViewState["FilterParameterName"]) ?? "filterExpression";
			}
			set
			{
				base.ViewState["FilterParameterName"] = value;
			}
		}

		// Token: 0x17004A5B RID: 19035
		// (get) Token: 0x0600F701 RID: 63233 RVA: 0x00380E53 File Offset: 0x0037F053
		// (set) Token: 0x0600F702 RID: 63234 RVA: 0x00380E74 File Offset: 0x0037F074
		[DefaultValue(typeof(GridClientDataBindingParameterType), "List")]
		[NotifyParentProperty(true)]
		[Category("Client")]
		[Description("Gets or set filter parameter type for the SelectMethod in the WebService or Page which will be requested to get data. Default value is List.")]
		public virtual GridClientDataBindingParameterType FilterParameterType
		{
			get
			{
				return (GridClientDataBindingParameterType)(base.ViewState["FilterParameterType"] ?? GridClientDataBindingParameterType.List);
			}
			set
			{
				base.ViewState["FilterParameterType"] = value;
			}
		}

		// Token: 0x17004A5C RID: 19036
		// (get) Token: 0x0600F703 RID: 63235 RVA: 0x00380E8C File Offset: 0x0037F08C
		// (set) Token: 0x0600F704 RID: 63236 RVA: 0x00380EAD File Offset: 0x0037F0AD
		[DefaultValue(typeof(GridClientDataBindingParameterType), "List")]
		[NotifyParentProperty(true)]
		[Category("Client")]
		[Description("Gets or set sort parameter type for the SelectMethod in the WebService or Page which will be requested to get data. Default value is List.")]
		public virtual GridClientDataBindingParameterType SortParameterType
		{
			get
			{
				return (GridClientDataBindingParameterType)(base.ViewState["SortParameterType"] ?? GridClientDataBindingParameterType.List);
			}
			set
			{
				base.ViewState["SortParameterType"] = value;
			}
		}

		// Token: 0x17004A5D RID: 19037
		// (get) Token: 0x0600F705 RID: 63237 RVA: 0x00380EC5 File Offset: 0x0037F0C5
		// (set) Token: 0x0600F706 RID: 63238 RVA: 0x00380EE6 File Offset: 0x0037F0E6
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Client")]
		[Description("Gets or set a value indicating whether the client-side caching should be enabled or not.")]
		public virtual bool EnableCaching
		{
			get
			{
				return (bool)(base.ViewState["EnableCaching"] ?? false);
			}
			set
			{
				base.ViewState["EnableCaching"] = value;
			}
		}

		// Token: 0x17004A5E RID: 19038
		// (get) Token: 0x0600F707 RID: 63239 RVA: 0x00380EFE File Offset: 0x0037F0FE
		// (set) Token: 0x0600F708 RID: 63240 RVA: 0x00380F1E File Offset: 0x0037F11E
		[Category("Client")]
		[NotifyParentProperty(true)]
		[DefaultValue("Data")]
		[Description("Gets or set data property name for the SelectMethod in the WebService or Page which will be requested to get data and count. Default is \"Data\"!")]
		public virtual string DataPropertyName
		{
			get
			{
				return ((string)base.ViewState["DataPropertyName"]) ?? "Data";
			}
			set
			{
				base.ViewState["DataPropertyName"] = value;
			}
		}

		// Token: 0x17004A5F RID: 19039
		// (get) Token: 0x0600F709 RID: 63241 RVA: 0x00380F31 File Offset: 0x0037F131
		// (set) Token: 0x0600F70A RID: 63242 RVA: 0x00380F51 File Offset: 0x0037F151
		[Category("Client")]
		[NotifyParentProperty(true)]
		[DefaultValue("Count")]
		[Description("Gets or set total records count property name for the SelectMethod in the WebService or Page which will be requested to get data and count. Default is \"Count\"!")]
		public virtual string CountPropertyName
		{
			get
			{
				return ((string)base.ViewState["CountPropertyName"]) ?? "Count";
			}
			set
			{
				base.ViewState["CountPropertyName"] = value;
			}
		}

		// Token: 0x17004A60 RID: 19040
		// (get) Token: 0x0600F70B RID: 63243 RVA: 0x00380F64 File Offset: 0x0037F164
		// (set) Token: 0x0600F70C RID: 63244 RVA: 0x00380F8D File Offset: 0x0037F18D
		[DefaultValue(GridClientDataResponseType.JSON)]
		[NotifyParentProperty(true)]
		[Category("Client")]
		[Description("Gets or sets the type of the data requested from a data service.")]
		public GridClientDataResponseType ResponseType
		{
			get
			{
				object obj = base.ViewState["ResponseType"];
				if (obj != null)
				{
					return (GridClientDataResponseType)obj;
				}
				return GridClientDataResponseType.JSON;
			}
			set
			{
				base.ViewState["ResponseType"] = value;
			}
		}

		// Token: 0x17004A61 RID: 19041
		// (get) Token: 0x0600F70D RID: 63245 RVA: 0x00380FA8 File Offset: 0x0037F1A8
		// (set) Token: 0x0600F70E RID: 63246 RVA: 0x00380FD1 File Offset: 0x0037F1D1
		[Category("Client")]
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Description("Gets or sets a value indicating whether empty data rows are shown when client-side databinding is setup.")]
		public bool ShowEmptyRowsOnLoad
		{
			get
			{
				object obj = base.ViewState["ShowEmptyRowsOnLoad"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["ShowEmptyRowsOnLoad"] = value;
			}
		}

		// Token: 0x17004A62 RID: 19042
		// (get) Token: 0x0600F70F RID: 63247 RVA: 0x00380FE9 File Offset: 0x0037F1E9
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public bool IsSet
		{
			get
			{
				return !string.IsNullOrEmpty(this.Location) && (!string.IsNullOrEmpty(this.SelectMethod) || !string.IsNullOrEmpty(this.DataService.TableName));
			}
		}

		// Token: 0x04004683 RID: 18051
		private GridClientDataService _dataService;
	}
}

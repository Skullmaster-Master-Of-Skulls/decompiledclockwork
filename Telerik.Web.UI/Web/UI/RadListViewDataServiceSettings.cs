using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000BBC RID: 3004
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class RadListViewDataServiceSettings : StateManager
	{
		// Token: 0x1700257A RID: 9594
		// (get) Token: 0x06007329 RID: 29481 RVA: 0x001AF7BD File Offset: 0x001AD9BD
		// (set) Token: 0x0600732A RID: 29482 RVA: 0x001AF7DD File Offset: 0x001AD9DD
		[Description("Gets or sets the base URL of the web service.")]
		[NotifyParentProperty(true)]
		[UrlProperty]
		[DefaultValue("")]
		[Category("Client")]
		public virtual string Location
		{
			get
			{
				return (base.ViewState["Location"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["Location"] = value;
			}
		}

		// Token: 0x1700257B RID: 9595
		// (get) Token: 0x0600732B RID: 29483 RVA: 0x001AF7F0 File Offset: 0x001AD9F0
		// (set) Token: 0x0600732C RID: 29484 RVA: 0x001AF810 File Offset: 0x001ADA10
		[Description("Gets or sets the table, method or entity path that gets appended to the base location of the web service. The result URL is used for requesting the data.")]
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Category("Client")]
		public virtual string DataPath
		{
			get
			{
				return (base.ViewState["DataPath"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["DataPath"] = value;
			}
		}

		// Token: 0x1700257C RID: 9596
		// (get) Token: 0x0600732D RID: 29485 RVA: 0x001AF823 File Offset: 0x001ADA23
		// (set) Token: 0x0600732E RID: 29486 RVA: 0x001AF843 File Offset: 0x001ADA43
		[NotifyParentProperty(true)]
		[Category("Client")]
		[DefaultValue("")]
		[Description("Gets or sets the count method name that gets appended to the base location of the web service. The result URL is used for requesting the total item count.")]
		public virtual string CountPath
		{
			get
			{
				return (base.ViewState["CountPath"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["CountPath"] = value;
			}
		}

		// Token: 0x1700257D RID: 9597
		// (get) Token: 0x0600732F RID: 29487 RVA: 0x001AF858 File Offset: 0x001ADA58
		// (set) Token: 0x06007330 RID: 29488 RVA: 0x001AF881 File Offset: 0x001ADA81
		[DefaultValue(typeof(RadListViewDataServiceHttpMethod), "Post")]
		[Category("Client")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the HTTP method that RadListView will use to access the data service URL. Default is POST.")]
		public virtual RadListViewDataServiceHttpMethod HttpMethod
		{
			get
			{
				object obj = base.ViewState["HttpMethod"];
				if (obj != null)
				{
					return (RadListViewDataServiceHttpMethod)obj;
				}
				return RadListViewDataServiceHttpMethod.Post;
			}
			set
			{
				base.ViewState["HttpMethod"] = value;
			}
		}

		// Token: 0x1700257E RID: 9598
		// (get) Token: 0x06007331 RID: 29489 RVA: 0x001AF89C File Offset: 0x001ADA9C
		// (set) Token: 0x06007332 RID: 29490 RVA: 0x001AF8C5 File Offset: 0x001ADAC5
		[Category("Client")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Description("Enables or disables client-side data caching in RadListView. Caching is disabled by default.")]
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

		// Token: 0x1700257F RID: 9599
		// (get) Token: 0x06007333 RID: 29491 RVA: 0x001AF8DD File Offset: 0x001ADADD
		// (set) Token: 0x06007334 RID: 29492 RVA: 0x001AF8FD File Offset: 0x001ADAFD
		[Description("Gets or sets the name of the property in the result object returned by the data service that contains the data objects RadListView will bind to.")]
		[NotifyParentProperty(true)]
		[Category("Client")]
		[DefaultValue("")]
		public virtual string DataPropertyName
		{
			get
			{
				return (base.ViewState["DataPropertyName"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["DataPropertyName"] = value;
			}
		}

		// Token: 0x17002580 RID: 9600
		// (get) Token: 0x06007335 RID: 29493 RVA: 0x001AF910 File Offset: 0x001ADB10
		// (set) Token: 0x06007336 RID: 29494 RVA: 0x001AF930 File Offset: 0x001ADB30
		[NotifyParentProperty(true)]
		[Category("Client")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the property in the result object returned by the data service that contains the total item count in the data source.")]
		public virtual string CountPropertyName
		{
			get
			{
				return (base.ViewState["CountPropertyName"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["CountPropertyName"] = value;
			}
		}

		// Token: 0x17002581 RID: 9601
		// (get) Token: 0x06007337 RID: 29495 RVA: 0x001AF943 File Offset: 0x001ADB43
		// (set) Token: 0x06007338 RID: 29496 RVA: 0x001AF963 File Offset: 0x001ADB63
		[DefaultValue("")]
		[Category("Client")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the name of the filter parameter that will be used with the data service.")]
		public virtual string FilterParameterName
		{
			get
			{
				return (base.ViewState["FilterParameterName"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["FilterParameterName"] = value;
			}
		}

		// Token: 0x17002582 RID: 9602
		// (get) Token: 0x06007339 RID: 29497 RVA: 0x001AF978 File Offset: 0x001ADB78
		// (set) Token: 0x0600733A RID: 29498 RVA: 0x001AF9A1 File Offset: 0x001ADBA1
		[Category("Client")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(RadListViewClientDataBindingParameterType), "List")]
		[Description("Gets or sets the format in which the filter expressions will be sent to the web service. Default is List.")]
		public virtual RadListViewClientDataBindingParameterType FilterParameterType
		{
			get
			{
				object obj = base.ViewState["FilterParameterType"];
				if (obj != null)
				{
					return (RadListViewClientDataBindingParameterType)obj;
				}
				return RadListViewClientDataBindingParameterType.List;
			}
			set
			{
				base.ViewState["FilterParameterType"] = value;
			}
		}

		// Token: 0x17002583 RID: 9603
		// (get) Token: 0x0600733B RID: 29499 RVA: 0x001AF9B9 File Offset: 0x001ADBB9
		// (set) Token: 0x0600733C RID: 29500 RVA: 0x001AF9D9 File Offset: 0x001ADBD9
		[NotifyParentProperty(true)]
		[Description("Gets or sets the name of the sort parameter that will be used with the data service.")]
		[DefaultValue("")]
		[Category("Client")]
		public virtual string SortParameterName
		{
			get
			{
				return (base.ViewState["SortParameterName"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["SortParameterName"] = value;
			}
		}

		// Token: 0x17002584 RID: 9604
		// (get) Token: 0x0600733D RID: 29501 RVA: 0x001AF9EC File Offset: 0x001ADBEC
		// (set) Token: 0x0600733E RID: 29502 RVA: 0x001AFA15 File Offset: 0x001ADC15
		[Category("Client")]
		[Description("Gets or sets the format in which the sort expressions will be sent to the web service. Default is List.")]
		[DefaultValue(typeof(RadListViewClientDataBindingParameterType), "List")]
		[NotifyParentProperty(true)]
		public virtual RadListViewClientDataBindingParameterType SortParameterType
		{
			get
			{
				object obj = base.ViewState["SortParameterType"];
				if (obj != null)
				{
					return (RadListViewClientDataBindingParameterType)obj;
				}
				return RadListViewClientDataBindingParameterType.List;
			}
			set
			{
				base.ViewState["SortParameterType"] = value;
			}
		}

		// Token: 0x17002585 RID: 9605
		// (get) Token: 0x0600733F RID: 29503 RVA: 0x001AFA2D File Offset: 0x001ADC2D
		// (set) Token: 0x06007340 RID: 29504 RVA: 0x001AFA4D File Offset: 0x001ADC4D
		[Category("Client")]
		[DefaultValue("")]
		[Description("Gets or sets the name of the start row index parameter that will be used with the data service.")]
		[NotifyParentProperty(true)]
		public virtual string StartRowIndexParameterName
		{
			get
			{
				return (base.ViewState["StartRowIndexParameterName"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["StartRowIndexParameterName"] = value;
			}
		}

		// Token: 0x17002586 RID: 9606
		// (get) Token: 0x06007341 RID: 29505 RVA: 0x001AFA60 File Offset: 0x001ADC60
		// (set) Token: 0x06007342 RID: 29506 RVA: 0x001AFA80 File Offset: 0x001ADC80
		[Description("Gets or sets the name of the maximum rows parameter that will be used with the data service.")]
		[DefaultValue("")]
		[Category("Client")]
		[NotifyParentProperty(true)]
		public virtual string MaximumRowsParameterName
		{
			get
			{
				return (base.ViewState["MaximumRowsParameterName"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["MaximumRowsParameterName"] = value;
			}
		}

		// Token: 0x17002587 RID: 9607
		// (get) Token: 0x06007343 RID: 29507 RVA: 0x001AFA94 File Offset: 0x001ADC94
		// (set) Token: 0x06007344 RID: 29508 RVA: 0x001AFABD File Offset: 0x001ADCBD
		[Description("Gets or sets the response type that is expected from the data service.")]
		[DefaultValue(typeof(RadListViewDataServiceResponseType), "JSON")]
		[Category("Client")]
		[NotifyParentProperty(true)]
		public virtual RadListViewDataServiceResponseType ResponseType
		{
			get
			{
				object obj = base.ViewState["ResponseType"];
				if (obj != null)
				{
					return (RadListViewDataServiceResponseType)obj;
				}
				return RadListViewDataServiceResponseType.JSON;
			}
			set
			{
				base.ViewState["ResponseType"] = value;
			}
		}
	}
}

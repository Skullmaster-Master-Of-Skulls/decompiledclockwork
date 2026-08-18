using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Licensing;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x02000064 RID: 100
	[ClientScriptResource("Telerik.Web.UI.RadClientDataSource", "Telerik.Web.UI.ClientDataSource.RadClientDataSourceScripts.js")]
	[Designer("Telerik.Web.Design.RadClientDataSourceDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[Description("Telerik RadClientDataSource")]
	[RequiredScript(typeof(jQueryPlugins))]
	[RequiredScript(typeof(Html5Data))]
	[ToolboxData("<{0}:RadClientDataSource runat=\"server\"></{0}:RadClientDataSource>")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[PersistChildren(false)]
	[ParseChildren(true)]
	[ToolboxBitmap(typeof(RadClientDataSource), "Telerik.Web.UI.ClientDataSource.png")]
	[TelerikToolboxCategory("Data")]
	public class RadClientDataSource : Control, IScriptControl, IControl, IControlResolver, IPostBackDataHandler, ICallbackEventHandler
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0000A14A File Offset: 0x0000834A
		internal JavaScriptSerializer Serializer
		{
			get
			{
				if (this._serializer == null)
				{
					this._serializer = new JavaScriptSerializer();
				}
				return this._serializer;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000A165 File Offset: 0x00008365
		protected ScriptManager ScriptManager
		{
			get
			{
				if (this._scriptManager == null)
				{
					this._scriptManager = ScriptRegistrar.GetScriptManager(this);
				}
				return this._scriptManager;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000A181 File Offset: 0x00008381
		internal new EventHandlerList Events
		{
			get
			{
				return base.Events;
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000A189 File Offset: 0x00008389
		public RadClientDataSource()
		{
			this.EnsureLicensing();
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000A198 File Offset: 0x00008398
		protected virtual void RenderDescriptorsNoScriptManager(HtmlTextWriter writer)
		{
			string controlDescriptors = ControlRenderer.GetControlDescriptors(this);
			writer.WriteLine(controlDescriptors);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000A1B3 File Offset: 0x000083B3
		protected override void OnInit(EventArgs e)
		{
			this.ParseModelFieldsDefaultValues();
			base.OnInit(e);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000A1C4 File Offset: 0x000083C4
		private void ParseModelFieldsDefaultValues()
		{
			if (this.Schema != null && this.Schema.Model != null && this.Schema.Model.Fields != null)
			{
				foreach (object obj in this.Schema.Model.Fields)
				{
					ClientDataSourceModelField clientDataSourceModelField = (ClientDataSourceModelField)obj;
					if (clientDataSourceModelField.DefaultValue != null && clientDataSourceModelField.DefaultValue.ToString() != string.Empty)
					{
						object defaultValue = clientDataSourceModelField.DefaultValue;
						switch (clientDataSourceModelField.DataType)
						{
						case ClientDataSourceModelFieldType.Number:
							clientDataSourceModelField.DefaultValue = Convert.ToDecimal(defaultValue);
							continue;
						case ClientDataSourceModelFieldType.Boolean:
							clientDataSourceModelField.DefaultValue = Convert.ToBoolean(defaultValue);
							continue;
						case ClientDataSourceModelFieldType.Date:
							clientDataSourceModelField.DefaultValue = Convert.ToDateTime(defaultValue);
							continue;
						}
						clientDataSourceModelField.DefaultValue = defaultValue;
					}
				}
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000A2E0 File Offset: 0x000084E0
		protected virtual void RegisterScriptDescriptors()
		{
			if (this.RegisterWithScriptManager)
			{
				this.ScriptManager.RegisterScriptDescriptors(this);
			}
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000A2F6 File Offset: 0x000084F6
		protected virtual void RegisterScriptControl()
		{
			if (this.RegisterWithScriptManager)
			{
				this.ScriptManager.RegisterScriptControl<RadClientDataSource>(this);
			}
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000A30C File Offset: 0x0000850C
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.RegisterScriptControl();
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000A31B File Offset: 0x0000851B
		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
			if (!base.DesignMode)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Id, this.ClientID);
				writer.AddStyleAttribute(HtmlTextWriterStyle.Display, "none");
				writer.RenderBeginTag(HtmlTextWriterTag.Span);
				writer.RenderEndTag();
				this.RegisterScriptDescriptors();
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000A35C File Offset: 0x0000855C
		void IControl.DescribeComponent(IScriptDescriptor descriptor)
		{
			this.Serializer.RegisterConverters(new JavaScriptConverter[]
			{
				new ClientDataSourceJavaScriptConverter()
			});
			this.DescribeProperties(descriptor);
			this.DescribeEvents(descriptor);
			this.Page.ClientScript.GetCallbackEventReference(this, null, null, null);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000A3A8 File Offset: 0x000085A8
		private void DescribeProperties(IScriptDescriptor descriptor)
		{
			descriptor.AddProperty("_id", this.ClientID);
			descriptor.AddProperty("_uniqueID", this.UniqueID);
			if (this.EnableServerFiltering)
			{
				descriptor.AddProperty("_enableServerFiltering", this.EnableServerFiltering);
			}
			if (this.EnableServerSorting)
			{
				descriptor.AddProperty("_enableServerSorting", this.EnableServerSorting);
			}
			if (this.EnableServerPaging)
			{
				descriptor.AddProperty("_enableServerPaging", this.EnableServerPaging);
			}
			if (this.EnableServerGrouping)
			{
				descriptor.AddProperty("_enableServerGrouping", this.EnableServerGrouping);
			}
			if (this.EnableServerAggregates)
			{
				descriptor.AddProperty("_enableServerAggregates", this.EnableServerAggregates);
			}
			if (this.PageSize != 10)
			{
				descriptor.AddProperty("_pageSize", this.PageSize);
			}
			if (this.CurrentPageIndex > 0)
			{
				descriptor.AddProperty("_currentPageIndex", this.CurrentPageIndex);
			}
			if (this.AllowBatchOperations)
			{
				descriptor.AddProperty("_allowBatchOperations", this.AllowBatchOperations);
			}
			if (this.AllowPaging)
			{
				descriptor.AddProperty("_allowPaging", this.AllowPaging);
			}
			if (this.AutoSync)
			{
				descriptor.AddProperty("_autoSync", this.AutoSync);
			}
			descriptor.AddScriptProperty("schema", this.Serializer.Serialize(this.Schema));
			if (this.FilterExpression != null && this.FilterExpression.Filters.Count > 0)
			{
				descriptor.AddScriptProperty("_filterExpressions", this.Serializer.Serialize(this.FilterExpression));
			}
			if (this.SortExpressions.Count > 0)
			{
				descriptor.AddScriptProperty("_sortExpressions", this.Serializer.Serialize(this.SortExpressions));
			}
			if (this.GroupExpressions.Count > 0)
			{
				descriptor.AddScriptProperty("_groupExpressions", this.Serializer.Serialize(this.GroupExpressions));
			}
			if (this.Aggregates.Count > 0)
			{
				descriptor.AddScriptProperty("_aggregates", this.Serializer.Serialize(this.Aggregates));
			}
			descriptor.AddScriptProperty("transport", this.Serializer.Serialize(this.DataSource.WebServiceDataSourceSettings));
			descriptor.AddScriptProperty("dataSourceSettings", this.Serializer.Serialize(this.DataSource.DataSourceControlSettings));
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000A61C File Offset: 0x0000881C
		private void DescribeEvents(IScriptDescriptor descriptor)
		{
			this.DescribeEvent(descriptor, "requestStart", this.ClientEvents.OnRequestStart);
			this.DescribeEvent(descriptor, "requestEnd", this.ClientEvents.OnRequestEnd);
			this.DescribeEvent(descriptor, "requestFailed", this.ClientEvents.OnRequestFailed);
			this.DescribeEvent(descriptor, "command", this.ClientEvents.OnCommand);
			this.DescribeEvent(descriptor, "customParameter", this.ClientEvents.OnCustomParameter);
			this.DescribeEvent(descriptor, "change", this.ClientEvents.OnChange);
			this.DescribeEvent(descriptor, "sync", this.ClientEvents.OnSync);
			this.DescribeEvent(descriptor, "dataRequested", this.ClientEvents.OnDataRequested);
			this.DescribeEvent(descriptor, "countRequested", this.ClientEvents.OnCountRequested);
			this.DescribeEvent(descriptor, "dataParse", this.ClientEvents.OnDataParse);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000A70F File Offset: 0x0000890F
		protected void DescribeEvent(IScriptDescriptor descriptor, string eventName, string handlerName)
		{
			if (!string.IsNullOrEmpty(handlerName))
			{
				descriptor.AddEvent(eventName, handlerName);
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000A721 File Offset: 0x00008921
		void IControl.EnsureChildControlsCreated()
		{
			this.EnsureChildControls();
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000A729 File Offset: 0x00008929
		public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			return ScriptRegistrar.GetScriptDescriptors(this);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000A734 File Offset: 0x00008934
		public IEnumerable<ScriptReference> GetScriptReferences()
		{
			List<ScriptReference> list = new List<ScriptReference>();
			list.AddRange(ScriptRegistrar.GetScriptReferences(this));
			return list;
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000A754 File Offset: 0x00008954
		[ClientControlProperty]
		[Browsable(false)]
		public string ClientStateFieldID
		{
			get
			{
				return this.ClientID + "_ClientState";
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000A766 File Offset: 0x00008966
		protected virtual bool LoadClientState(Dictionary<string, object> clientState)
		{
			return false;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000A76C File Offset: 0x0000896C
		private void EnsureLicensing()
		{
			if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
			{
				try
				{
					LicenseManager.Validate(base.GetType());
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x0000A7A4 File Offset: 0x000089A4
		protected virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			string text = postCollection[this.ClientStateFieldID];
			if (!string.IsNullOrEmpty(text))
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				Dictionary<string, object> dictionary = javaScriptSerializer.DeserializeObject(text) as Dictionary<string, object>;
				if (dictionary != null)
				{
					return this.LoadClientState(dictionary);
				}
			}
			return false;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x0000A7E5 File Offset: 0x000089E5
		protected virtual void RaisePostDataChangedEvent()
		{
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0000A7E7 File Offset: 0x000089E7
		bool IPostBackDataHandler.LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			return this.LoadPostData(postDataKey, postCollection);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000A7F1 File Offset: 0x000089F1
		void IPostBackDataHandler.RaisePostDataChangedEvent()
		{
			this.RaisePostDataChangedEvent();
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000A7F9 File Offset: 0x000089F9
		Control IControlResolver.ResolveControl(string controlId)
		{
			return this.FindControl(controlId);
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000A804 File Offset: 0x00008A04
		protected override object SaveViewState()
		{
			ArrayList arrayList = new ArrayList();
			object value = base.SaveViewState();
			arrayList.Add(value);
			arrayList.Add(((IStateManager)this.DataSource).SaveViewState());
			arrayList.Add(((IStateManager)this.FilterExpression).SaveViewState());
			arrayList.Add(((IStateManager)this.SortExpressions).SaveViewState());
			arrayList.Add(((IStateManager)this.GroupExpressions).SaveViewState());
			arrayList.Add(((IStateManager)this.Aggregates).SaveViewState());
			arrayList.Add(((IStateManager)this.Schema).SaveViewState());
			arrayList.Add(((IStateManager)this.ClientEvents).SaveViewState());
			return arrayList.ToArray(typeof(object));
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000A8B4 File Offset: 0x00008AB4
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				int num = 0;
				base.LoadViewState(array[num++]);
				((IStateManager)this.DataSource).LoadViewState(array[num++]);
				((IStateManager)this.FilterExpression).LoadViewState(array[num++]);
				((IStateManager)this.SortExpressions).LoadViewState(array[num++]);
				((IStateManager)this.GroupExpressions).LoadViewState(array[num++]);
				((IStateManager)this.Aggregates).LoadViewState(array[num++]);
				((IStateManager)this.Schema).LoadViewState(array[num++]);
				((IStateManager)this.ClientEvents).LoadViewState(array[num++]);
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000A95C File Offset: 0x00008B5C
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (base.IsTrackingViewState)
			{
				return;
			}
			((IStateManager)this.DataSource).TrackViewState();
			((IStateManager)this.FilterExpression).TrackViewState();
			((IStateManager)this.SortExpressions).TrackViewState();
			((IStateManager)this.GroupExpressions).TrackViewState();
			((IStateManager)this.Aggregates).TrackViewState();
			((IStateManager)this.Schema).TrackViewState();
			((IStateManager)this.ClientEvents).TrackViewState();
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000A9C5 File Offset: 0x00008BC5
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x0000A9CD File Offset: 0x00008BCD
		internal RadProxyBoundControl ProxyBoundControl { get; set; }

		// Token: 0x0600042A RID: 1066 RVA: 0x0000A9D6 File Offset: 0x00008BD6
		public string GetCallbackResult()
		{
			return this.ProxyBoundControl.GetJson();
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0000A9E4 File Offset: 0x00008BE4
		public void RaiseCallbackEvent(string eventArgument)
		{
			this.Controls.Clear();
			this.ProxyBoundControl = new RadProxyBoundControl(this);
			this.ProxyBoundControl.AllowPaging = (this.AllowPaging && this.EnableServerPaging);
			this.Controls.Add(this.ProxyBoundControl);
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new ClientDataSourceJavaScriptConverter()
			});
			ClientDataSourceContext clientDataSourceContext = javaScriptSerializer.Deserialize<ClientDataSourceContext>(eventArgument);
			this.ProxyBoundControl.DataSourceID = this.DataSource.DataSourceControlSettings.DataSourceID;
			if (this.DataSource.DataSourceControlSettings.AllowAutomaticUpdates && !string.IsNullOrEmpty(clientDataSourceContext.CommandName))
			{
				new Hashtable();
				bool flag = this.Schema != null && this.Schema.Model != null && !string.IsNullOrEmpty(this.Schema.Model.ID) && this.Schema.Model.Fields != null && this.Schema.Model.Fields.Count > 0;
				bool flag2 = clientDataSourceContext.NewValues != null && clientDataSourceContext.OldValues != null && clientDataSourceContext.OldValues.Count > 0 && clientDataSourceContext.NewValues.Count > 0 && clientDataSourceContext.CommandName == "update";
				if (flag && flag2)
				{
					this.ProxyBoundControl.PerformUpdate(clientDataSourceContext.IDKeys, clientDataSourceContext.NewValues, clientDataSourceContext.OldValues);
				}
			}
			if (this.DataSource.DataSourceControlSettings.AllowAutomaticInserts && !string.IsNullOrEmpty(clientDataSourceContext.CommandName))
			{
				new Hashtable();
				bool flag3 = this.Schema != null && this.Schema.Model != null && !string.IsNullOrEmpty(this.Schema.Model.ID) && this.Schema.Model.Fields != null && this.Schema.Model.Fields.Count > 0;
				if (flag3 && clientDataSourceContext.NewValues != null && clientDataSourceContext.NewValues.Count > 0 && clientDataSourceContext.CommandName == "insert")
				{
					this.ProxyBoundControl.PerformInsert(clientDataSourceContext.NewValues);
				}
			}
			if (this.DataSource.DataSourceControlSettings.AllowAutomaticDeletes && !string.IsNullOrEmpty(clientDataSourceContext.CommandName))
			{
				new Hashtable();
				bool flag4 = this.Schema != null && this.Schema.Model != null && !string.IsNullOrEmpty(this.Schema.Model.ID) && this.Schema.Model.Fields != null && this.Schema.Model.Fields.Count > 0;
				if (flag4 && clientDataSourceContext.OldValues != null && clientDataSourceContext.OldValues.Count > 0 && clientDataSourceContext.CommandName == "delete")
				{
					this.ProxyBoundControl.PerformDelete(clientDataSourceContext.IDKeys, clientDataSourceContext.OldValues);
				}
			}
			if (this.EnableServerSorting)
			{
				this.ProxyBoundControl.SortExpressions.Clear();
				RadListViewSortExpressionCollection radListViewSortExpressionCollection = clientDataSourceContext.SortExpressions.ToListViewFilterExpression();
				foreach (object value in radListViewSortExpressionCollection)
				{
					this.ProxyBoundControl.SortExpressions.Add(value);
				}
			}
			if (this.EnableServerFiltering)
			{
				ClientDataSourceFilterExpression filterExpression = clientDataSourceContext.FilterExpression;
				bool flag5 = this.Schema != null && this.Schema.Model != null && this.Schema.Model.Fields != null;
				Dictionary<string, ClientDataSourceModelFieldType> dictionary = new Dictionary<string, ClientDataSourceModelFieldType>();
				this.ProxyBoundControl.FilterExpressions.Clear();
				if (flag5)
				{
					foreach (object obj in this.Schema.Model.Fields)
					{
						ClientDataSourceModelField clientDataSourceModelField = (ClientDataSourceModelField)obj;
						dictionary.Add(clientDataSourceModelField.FieldName, clientDataSourceModelField.DataType);
					}
					foreach (object obj2 in filterExpression.Filters)
					{
						ClientDataSourceFilterExpression clientDataSourceFilterExpression = obj2 as ClientDataSourceFilterExpression;
						if (clientDataSourceFilterExpression != null)
						{
							foreach (object obj3 in clientDataSourceFilterExpression.Filters)
							{
								ClientDataSourceFilterEntry clientDataSourceFilterEntry = obj3 as ClientDataSourceFilterEntry;
								if (clientDataSourceFilterEntry != null && dictionary.ContainsKey(clientDataSourceFilterEntry.FieldName))
								{
									this.ProxyBoundControl.FilterExpressions.Add(clientDataSourceFilterEntry.ToListViewExpression(clientDataSourceFilterEntry, dictionary[clientDataSourceFilterEntry.FieldName]));
								}
							}
						}
					}
				}
			}
			if (this.EnableServerPaging)
			{
				this.ProxyBoundControl.CurrentPageIndex = clientDataSourceContext.CurrentPageIndex;
				this.ProxyBoundControl.PageSize = clientDataSourceContext.PageSize;
			}
			this.ProxyBoundControl.DataBind();
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0000AF48 File Offset: 0x00009148
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Category("Client")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ClientDataSourceClientEvents ClientEvents
		{
			get
			{
				if (this._clientEvents == null)
				{
					this._clientEvents = new ClientDataSourceClientEvents();
				}
				if (base.IsTrackingViewState)
				{
					((IStateManager)this._clientEvents).TrackViewState();
				}
				return this._clientEvents;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0000AF76 File Offset: 0x00009176
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x0000AF97 File Offset: 0x00009197
		[Category("Behavior")]
		[Description("Whether to register with the ScriptManager control on the page")]
		[DefaultValue(true)]
		public virtual bool RegisterWithScriptManager
		{
			get
			{
				return (bool)(this.ViewState["RegisterWithScriptManager"] ?? true);
			}
			set
			{
				this.ViewState["RegisterWithScriptManager"] = value;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0000AFB0 File Offset: 0x000091B0
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x0000AFD9 File Offset: 0x000091D9
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets value indicating whether server-side paging is enabled")]
		[DefaultValue(false)]
		public virtual bool EnableServerPaging
		{
			get
			{
				object obj = this.ViewState["EnableServerPaging"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["EnableServerPaging"] = value;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000AFF4 File Offset: 0x000091F4
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x0000B01D File Offset: 0x0000921D
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Description("Gets or sets value indicating whether server-side filtering is enabled")]
		public virtual bool EnableServerFiltering
		{
			get
			{
				object obj = this.ViewState["EnableServerFiltering"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["EnableServerFiltering"] = value;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000B038 File Offset: 0x00009238
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x0000B061 File Offset: 0x00009261
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Description("Gets or sets value indicating whether server-side sorting is enabled")]
		[Category("Behavior")]
		public virtual bool EnableServerSorting
		{
			get
			{
				object obj = this.ViewState["EnableServerSorting"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["EnableServerSorting"] = value;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0000B07C File Offset: 0x0000927C
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x0000B0A5 File Offset: 0x000092A5
		[Category("Behavior")]
		[Description("Gets or sets value indicating whether server-side grouping is enabled")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public virtual bool EnableServerGrouping
		{
			get
			{
				object obj = this.ViewState["EnableServerGrouping"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["EnableServerGrouping"] = value;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000B0C0 File Offset: 0x000092C0
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x0000B0E9 File Offset: 0x000092E9
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Description("Gets or sets value indicating whether server-side aggregates are enabled")]
		[Category("Behavior")]
		public virtual bool EnableServerAggregates
		{
			get
			{
				object obj = this.ViewState["EnableServerAggregates"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["EnableServerAggregates"] = value;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000B104 File Offset: 0x00009304
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x0000B12E File Offset: 0x0000932E
		[Description("Gets or sets the maximum number of items that would appear in a page")]
		[Category("Behavior")]
		[DefaultValue(10)]
		[SimplePersistenceSetting]
		[NotifyParentProperty(true)]
		public virtual int PageSize
		{
			get
			{
				object obj = this.ViewState["PageSize"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 10;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["PageSize"] = value;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0000B158 File Offset: 0x00009358
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x0000B181 File Offset: 0x00009381
		[DefaultValue(0)]
		[Bindable(true)]
		[Browsable(false)]
		[Description("Gets or sets a value indicating the index of the current page")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int CurrentPageIndex
		{
			get
			{
				object obj = this.ViewState["CurrentPageIndex"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["CurrentPageIndex"] = value;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0000B1A8 File Offset: 0x000093A8
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x0000B1D1 File Offset: 0x000093D1
		[Description("Gets or sets a value indicating whether the paging in RadClientDataSource is enabled")]
		[Category("Behavior")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public virtual bool AllowPaging
		{
			get
			{
				object obj = this.ViewState["AllowPaging"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AllowPaging"] = value;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0000B1EC File Offset: 0x000093EC
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x0000B215 File Offset: 0x00009415
		[Description("Gets or sets a value indicating whether batch operations in the RadClientDataSource are enabled")]
		[Category("Behavior")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public virtual bool AllowBatchOperations
		{
			get
			{
				object obj = this.ViewState["AllowBatchOperations"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AllowBatchOperations"] = value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0000B230 File Offset: 0x00009430
		// (set) Token: 0x06000442 RID: 1090 RVA: 0x0000B259 File Offset: 0x00009459
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[Description("Gets or sets a value indicating whether the RadClientDataSource would automatically save any changed data items by calling the sync method")]
		[DefaultValue(false)]
		public virtual bool AutoSync
		{
			get
			{
				object obj = this.ViewState["AutoSync"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["AutoSync"] = value;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0000B271 File Offset: 0x00009471
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public virtual ClientDataSourceFilterExpression FilterExpression
		{
			get
			{
				if (this._filterExpression == null)
				{
					this._filterExpression = new ClientDataSourceFilterExpression();
				}
				if (base.IsTrackingViewState)
				{
					((IStateManager)this._filterExpression).TrackViewState();
				}
				return this._filterExpression;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0000B29F File Offset: 0x0000949F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ClientDataSourceSortExpressionCollection SortExpressions
		{
			get
			{
				if (this._sortExpressions == null)
				{
					this._sortExpressions = new ClientDataSourceSortExpressionCollection();
				}
				if (base.IsTrackingViewState)
				{
					((IStateManager)this._sortExpressions).TrackViewState();
				}
				return this._sortExpressions;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0000B2CD File Offset: 0x000094CD
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ClientDataSourceGroupExpressionCollection GroupExpressions
		{
			get
			{
				if (this._groupExpressions == null)
				{
					this._groupExpressions = new ClientDataSourceGroupExpressionCollection();
				}
				if (base.IsTrackingViewState)
				{
					((IStateManager)this._groupExpressions).TrackViewState();
				}
				return this._groupExpressions;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0000B2FB File Offset: 0x000094FB
		[NotifyParentProperty(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public virtual ClientDataSourceAggregatesCollection Aggregates
		{
			get
			{
				if (this._aggregates == null)
				{
					this._aggregates = new ClientDataSourceAggregatesCollection();
				}
				if (base.IsTrackingViewState)
				{
					((IStateManager)this._aggregates).TrackViewState();
				}
				return this._aggregates;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0000B329 File Offset: 0x00009529
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Client")]
		[NotifyParentProperty(true)]
		[Description("Contains settings about the different types of data sources used in RadClientDataSource.")]
		public virtual ClientDataSourceSettings DataSource
		{
			get
			{
				if (this._dataSource == null)
				{
					this._dataSource = new ClientDataSourceSettings(this);
				}
				if (base.IsTrackingViewState)
				{
					((IStateManager)this._dataSource).TrackViewState();
				}
				return this._dataSource;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0000B358 File Offset: 0x00009558
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Client")]
		[NotifyParentProperty(true)]
		[Description("Contains settings about the schema and model of the data used in RadClientDataSource.")]
		public virtual ClientDataSourceSchema Schema
		{
			get
			{
				if (this._schema == null)
				{
					this._schema = new ClientDataSourceSchema();
				}
				if (base.IsTrackingViewState)
				{
					((IStateManager)this._schema).TrackViewState();
				}
				return this._schema;
			}
		}

		// Token: 0x04000078 RID: 120
		private ScriptManager _scriptManager;

		// Token: 0x04000079 RID: 121
		private ClientDataSourceClientEvents _clientEvents;

		// Token: 0x0400007A RID: 122
		private ClientDataSourceFilterExpression _filterExpression;

		// Token: 0x0400007B RID: 123
		private ClientDataSourceSortExpressionCollection _sortExpressions;

		// Token: 0x0400007C RID: 124
		private ClientDataSourceGroupExpressionCollection _groupExpressions;

		// Token: 0x0400007D RID: 125
		private ClientDataSourceAggregatesCollection _aggregates;

		// Token: 0x0400007E RID: 126
		private ClientDataSourceSchema _schema;

		// Token: 0x0400007F RID: 127
		private JavaScriptSerializer _serializer;

		// Token: 0x04000080 RID: 128
		private ClientDataSourceSettings _dataSource;
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Licensing;
using Telerik.Web.UI.Common;
using Telerik.Web.UI.ODataSource;

namespace Telerik.Web.UI
{
	// Token: 0x02000BD4 RID: 3028
	[PersistChildren(false)]
	[RequiredScript(typeof(jQuery))]
	[ClientScriptResource("Telerik.Web.UI.RadODataDataSource", "Telerik.Web.UI.ODataDataSource.RadODataDataSource.js", LoadOrder = 1)]
	[ClientScriptResource("Telerik.Web.UI.RadODataDataSource", "Telerik.Web.UI.ODataDataSource.Binders.js", LoadOrder = 2)]
	[ParseChildren(true)]
	[RequiredScript(typeof(Core))]
	[ToolboxData("<{0}:RadODataDataSource runat=\"server\"></{0}:RadODataDataSource>")]
	[ToolboxBitmap(typeof(RadODataDataSource), "Telerik.Web.UI.ODataDataSource.png")]
	[TelerikToolboxCategory("Data")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[RequiredScript(typeof(jQueryPlugins))]
	[Designer("Telerik.Web.Design.RadODataSourceDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	public class RadODataDataSource : Control, IScriptControl, IControl, IControlResolver
	{
		// Token: 0x1700259C RID: 9628
		// (get) Token: 0x06007382 RID: 29570 RVA: 0x001B003A File Offset: 0x001AE23A
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[Category("Behavior")]
		[Description("The web service to be used for populating items with ExpandMode set to WebService.")]
		public Transport Transport
		{
			get
			{
				if (this._transport == null)
				{
					this._transport = new Transport();
				}
				return this._transport;
			}
		}

		// Token: 0x1700259D RID: 9629
		// (get) Token: 0x06007383 RID: 29571 RVA: 0x001B0055 File Offset: 0x001AE255
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Client-side events.")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[Category("Behavior")]
		public ClientEvents ClientEvents
		{
			get
			{
				if (this._clientEvents == null)
				{
					this._clientEvents = new ClientEvents();
				}
				return this._clientEvents;
			}
		}

		// Token: 0x1700259E RID: 9630
		// (get) Token: 0x06007384 RID: 29572 RVA: 0x001B0070 File Offset: 0x001AE270
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Schema Schema
		{
			get
			{
				if (this._schema == null)
				{
					this._schema = new Schema();
				}
				return this._schema;
			}
		}

		// Token: 0x1700259F RID: 9631
		// (get) Token: 0x06007385 RID: 29573 RVA: 0x001B008B File Offset: 0x001AE28B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public FilterExpressionCollection FilterExpressions
		{
			get
			{
				if (this._filters == null)
				{
					this._filters = new FilterExpressionCollection();
				}
				return this._filters;
			}
		}

		// Token: 0x170025A0 RID: 9632
		// (get) Token: 0x06007386 RID: 29574 RVA: 0x001B00A6 File Offset: 0x001AE2A6
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public SortExpressionCollection SortExpressions
		{
			get
			{
				if (this._sorts == null)
				{
					this._sorts = new SortExpressionCollection();
				}
				return this._sorts;
			}
		}

		// Token: 0x170025A1 RID: 9633
		// (get) Token: 0x06007387 RID: 29575 RVA: 0x001B00C1 File Offset: 0x001AE2C1
		// (set) Token: 0x06007388 RID: 29576 RVA: 0x001B00C9 File Offset: 0x001AE2C9
		[DefaultValue(true)]
		[ClientControlProperty]
		[ClientPropertyName("_sorting")]
		[Category("Behavior")]
		[Description("Gets or sets whether server-side sorting is enabled.")]
		public bool EnableSorting
		{
			get
			{
				return this._serverSorting;
			}
			set
			{
				this._serverSorting = value;
			}
		}

		// Token: 0x170025A2 RID: 9634
		// (get) Token: 0x06007389 RID: 29577 RVA: 0x001B00D2 File Offset: 0x001AE2D2
		// (set) Token: 0x0600738A RID: 29578 RVA: 0x001B00DA File Offset: 0x001AE2DA
		[ClientPropertyName("_filtering")]
		[Category("Behavior")]
		[ClientControlProperty]
		[DefaultValue(true)]
		[Description("Gets or sets whether server-side filtering is enabled.")]
		public bool EnableFiltering
		{
			get
			{
				return this._serverFiltering;
			}
			set
			{
				this._serverFiltering = value;
			}
		}

		// Token: 0x170025A3 RID: 9635
		// (get) Token: 0x0600738B RID: 29579 RVA: 0x001B00E3 File Offset: 0x001AE2E3
		// (set) Token: 0x0600738C RID: 29580 RVA: 0x001B00EB File Offset: 0x001AE2EB
		[ClientPropertyName("_paging")]
		[ClientControlProperty]
		[DefaultValue(true)]
		[Category("Behavior")]
		[Description("Gets or sets whether server-side paging is enabled.")]
		public bool EnablePaging
		{
			get
			{
				return this._serverPaging;
			}
			set
			{
				this._serverPaging = value;
			}
		}

		// Token: 0x170025A4 RID: 9636
		// (get) Token: 0x0600738D RID: 29581 RVA: 0x001B00F4 File Offset: 0x001AE2F4
		// (set) Token: 0x0600738E RID: 29582 RVA: 0x001B00FC File Offset: 0x001AE2FC
		[DefaultValue(false)]
		[ClientControlProperty]
		[ClientPropertyName("_enableDataCaching")]
		[Category("Behavior")]
		[Description("Gets or sets whether data caching is enabled.")]
		public bool EnableDataCaching
		{
			get
			{
				return this._enableDataCaching;
			}
			set
			{
				this._enableDataCaching = value;
			}
		}

		// Token: 0x170025A5 RID: 9637
		// (get) Token: 0x0600738F RID: 29583 RVA: 0x001B0105 File Offset: 0x001AE305
		// (set) Token: 0x06007390 RID: 29584 RVA: 0x001B0126 File Offset: 0x001AE326
		[Category("Behavior")]
		[DefaultValue(true)]
		[Description("Whether to register with the ScriptManager control on the page")]
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

		// Token: 0x170025A6 RID: 9638
		// (get) Token: 0x06007391 RID: 29585 RVA: 0x001B013E File Offset: 0x001AE33E
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

		// Token: 0x06007392 RID: 29586 RVA: 0x001B0159 File Offset: 0x001AE359
		public RadODataDataSource()
		{
			this.InitFields();
			this.EnsureLicensing();
		}

		// Token: 0x06007393 RID: 29587 RVA: 0x001B016D File Offset: 0x001AE36D
		private void InitFields()
		{
			this._serverPaging = true;
			this._serverFiltering = true;
			this._serverSorting = true;
			this._filters = null;
			this._sorts = null;
		}

		// Token: 0x06007394 RID: 29588 RVA: 0x001B0194 File Offset: 0x001AE394
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

		// Token: 0x170025A7 RID: 9639
		// (get) Token: 0x06007395 RID: 29589 RVA: 0x001B01CC File Offset: 0x001AE3CC
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

		// Token: 0x06007396 RID: 29590 RVA: 0x001B01E8 File Offset: 0x001AE3E8
		protected virtual void RenderDescriptorsNoScriptManager(HtmlTextWriter writer)
		{
			string controlDescriptors = ControlRenderer.GetControlDescriptors(this);
			writer.WriteLine(controlDescriptors);
		}

		// Token: 0x06007397 RID: 29591 RVA: 0x001B0203 File Offset: 0x001AE403
		protected virtual void RegisterScriptDescriptors()
		{
			if (this.RegisterWithScriptManager)
			{
				this.ScriptManager.RegisterScriptDescriptors(this);
			}
		}

		// Token: 0x06007398 RID: 29592 RVA: 0x001B0219 File Offset: 0x001AE419
		protected virtual void RegisterScriptControl()
		{
			if (this.RegisterWithScriptManager)
			{
				this.ScriptManager.RegisterScriptControl<RadODataDataSource>(this);
			}
		}

		// Token: 0x06007399 RID: 29593 RVA: 0x001B022F File Offset: 0x001AE42F
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.RegisterScriptControl();
		}

		// Token: 0x0600739A RID: 29594 RVA: 0x001B023E File Offset: 0x001AE43E
		protected override void Render(HtmlTextWriter writer)
		{
			base.Render(writer);
			if (!base.DesignMode)
			{
				this.RegisterScriptDescriptors();
			}
		}

		// Token: 0x0600739B RID: 29595 RVA: 0x001B0258 File Offset: 0x001AE458
		void IControl.DescribeComponent(IScriptDescriptor descriptor)
		{
			this.RegisterConverters(this.Serializer);
			descriptor.AddProperty("_id", this.ClientID);
			descriptor.AddProperty("_filtering", this.EnableFiltering);
			descriptor.AddProperty("_sorting", this.EnableSorting);
			descriptor.AddProperty("_paging", this.EnablePaging);
			descriptor.AddProperty("_enableDataCaching", this.EnableDataCaching);
			descriptor.AddScriptProperty("schema", this.Serializer.Serialize(this.Schema));
			descriptor.AddScriptProperty("transport", this.Serializer.Serialize(this.Transport));
			descriptor.AddScriptProperty("filters", this.Serializer.Serialize(this.FilterExpressions));
			descriptor.AddScriptProperty("sorts", this.Serializer.Serialize(this.SortExpressions));
			this.DescribeEvent(descriptor, "requesting", this.ClientEvents.Requesting);
			this.DescribeEvent(descriptor, "requestSucceeded", this.ClientEvents.RequestSucceeded);
			this.DescribeEvent(descriptor, "requestFailed", this.ClientEvents.RequestFailed);
		}

		// Token: 0x0600739C RID: 29596 RVA: 0x001B0390 File Offset: 0x001AE590
		protected virtual void RegisterConverters(JavaScriptSerializer serializer)
		{
			serializer.RegisterConverters(new JavaScriptConverter[]
			{
				new FiltersConverter(),
				new SortsConverter()
			});
		}

		// Token: 0x0600739D RID: 29597 RVA: 0x001B03BB File Offset: 0x001AE5BB
		protected void DescribeEvent(IScriptDescriptor descriptor, string eventName, string handlerHame)
		{
			if (!string.IsNullOrEmpty(handlerHame))
			{
				descriptor.AddEvent(eventName, handlerHame);
			}
		}

		// Token: 0x0600739E RID: 29598 RVA: 0x001B03CD File Offset: 0x001AE5CD
		void IControl.EnsureChildControlsCreated()
		{
			this.EnsureChildControls();
		}

		// Token: 0x0600739F RID: 29599 RVA: 0x001B03D5 File Offset: 0x001AE5D5
		public IEnumerable<ScriptDescriptor> GetScriptDescriptors()
		{
			return ScriptRegistrar.GetScriptDescriptors(this);
		}

		// Token: 0x060073A0 RID: 29600 RVA: 0x001B03E0 File Offset: 0x001AE5E0
		public IEnumerable<ScriptReference> GetScriptReferences()
		{
			List<ScriptReference> list = new List<ScriptReference>();
			list.AddRange(ScriptRegistrar.GetScriptReferences(this));
			return list;
		}

		// Token: 0x060073A1 RID: 29601 RVA: 0x001B0400 File Offset: 0x001AE600
		Control IControlResolver.ResolveControl(string controlId)
		{
			return this.FindControl(controlId);
		}

		// Token: 0x04001F64 RID: 8036
		private ScriptManager _scriptManager;

		// Token: 0x04001F65 RID: 8037
		private Transport _transport;

		// Token: 0x04001F66 RID: 8038
		private ClientEvents _clientEvents;

		// Token: 0x04001F67 RID: 8039
		private Schema _schema;

		// Token: 0x04001F68 RID: 8040
		private bool _serverPaging;

		// Token: 0x04001F69 RID: 8041
		private bool _serverFiltering;

		// Token: 0x04001F6A RID: 8042
		private bool _serverSorting;

		// Token: 0x04001F6B RID: 8043
		private bool _enableDataCaching;

		// Token: 0x04001F6C RID: 8044
		private FilterExpressionCollection _filters;

		// Token: 0x04001F6D RID: 8045
		private SortExpressionCollection _sorts;

		// Token: 0x04001F6E RID: 8046
		private JavaScriptSerializer _serializer;
	}
}

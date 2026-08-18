using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Licensing;
using Telerik.Web.UI.Map;

namespace Telerik.Web.UI
{
	// Token: 0x02000443 RID: 1091
	[RequiredScript(typeof(Html5DataVizMap))]
	[LightweightRendering]
	[Designer("Telerik.Web.Design.RadMapDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[ToolboxBitmap(typeof(RadMap), "Telerik.Web.UI.Map.png")]
	[TelerikToolboxCategory("Visualization")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[RequiredScript(typeof(MaterialRipple))]
	[ClientScriptResource("Telerik.Web.UI.RadMap", "Telerik.Web.UI.Map.Scripts.RadMap.js")]
	[EmbeddedSkin("Map", typeof(RadMap))]
	[EmbeddedSkin("Map", "Default", typeof(RadMap))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Classic, typeof(RadMap))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.fonticons.css", RenderMode.Lightweight, typeof(RadMap))]
	[RequiredCss("Telerik.Web.UI.Skins.HTML5UI.dataviz.css", RenderMode.Classic, typeof(RadMap))]
	[RequiredCss("Telerik.Web.UI.Skins.HTML5UI.dataviz.css", RenderMode.Lightweight, typeof(RadMap))]
	[RequiredCss("Telerik.Web.UI.Skins.Common.MaterialRipple.css", RenderMode.Lightweight, typeof(RadButton))]
	[ParseChildren(ChildrenAsProperties = true)]
	public class RadMap : RadDataBoundControl
	{
		// Token: 0x17000C98 RID: 3224
		// (get) Token: 0x06002717 RID: 10007 RVA: 0x0007F0E0 File Offset: 0x0007D2E0
		// (set) Token: 0x06002718 RID: 10008 RVA: 0x0007F0FB File Offset: 0x0007D2FB
		[Category("Behavior")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		public MapBinding DataBindings
		{
			get
			{
				if (this._dataBindings == null)
				{
					this._dataBindings = new MapBinding();
				}
				return this._dataBindings;
			}
			set
			{
				this._dataBindings = value;
			}
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x06002719 RID: 10009 RVA: 0x0007F104 File Offset: 0x0007D304
		// (set) Token: 0x0600271A RID: 10010 RVA: 0x0007F10C File Offset: 0x0007D30C
		public object LayersDataSource
		{
			get
			{
				return this._layersDataSource;
			}
			set
			{
				if (value != null)
				{
					this.ValidateDataSource(value);
				}
				this._layersDataSource = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17000C9A RID: 3226
		// (get) Token: 0x0600271B RID: 10011 RVA: 0x0007F125 File Offset: 0x0007D325
		// (set) Token: 0x0600271C RID: 10012 RVA: 0x0007F145 File Offset: 0x0007D345
		public string LayersDataSourceID
		{
			get
			{
				return (string)(this.ViewState["LayersDataSourceID"] ?? "");
			}
			set
			{
				this.ViewState["LayersDataSourceID"] = value;
				this.OnDataPropertyChanged();
			}
		}

		// Token: 0x17000C9B RID: 3227
		// (get) Token: 0x0600271D RID: 10013 RVA: 0x0007F15E File Offset: 0x0007D35E
		// (set) Token: 0x0600271E RID: 10014 RVA: 0x0007F17F File Offset: 0x0007D37F
		[Description("Gets or sets a bool value that indicates whether the markers are cleared before data binding.")]
		[Category("Behavior")]
		[DefaultValue(false)]
		public bool AppendDataBoundMarkers
		{
			get
			{
				return (bool)(this.ViewState["AppendDataBoundMarkers"] ?? false);
			}
			set
			{
				this.ViewState["AppendDataBoundMarkers"] = value;
			}
		}

		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x0600271F RID: 10015 RVA: 0x0007F197 File Offset: 0x0007D397
		// (set) Token: 0x06002720 RID: 10016 RVA: 0x0007F1B8 File Offset: 0x0007D3B8
		[Category("Behavior")]
		[Description("Gets or sets a bool value that indicates whether the layers are cleared before data binding.")]
		[DefaultValue(false)]
		public bool AppendDataBoundLayers
		{
			get
			{
				return (bool)(this.ViewState["AppendDataBoundLayers"] ?? false);
			}
			set
			{
				this.ViewState["AppendDataBoundLayers"] = value;
			}
		}

		// Token: 0x14000087 RID: 135
		// (add) Token: 0x06002721 RID: 10017 RVA: 0x0007F1D0 File Offset: 0x0007D3D0
		// (remove) Token: 0x06002722 RID: 10018 RVA: 0x0007F1E3 File Offset: 0x0007D3E3
		public event RadMap.MapItemDataBoundEventHandler ItemDataBound
		{
			add
			{
				base.Events.AddHandler(RadMap.itemDataBoundEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadMap.itemDataBoundEvent, value);
			}
		}

		// Token: 0x06002723 RID: 10019 RVA: 0x0007F1F8 File Offset: 0x0007D3F8
		protected virtual void OnItemDataBound(MapItemDataBoundEventArgs e)
		{
			RadMap.MapItemDataBoundEventHandler mapItemDataBoundEventHandler = (RadMap.MapItemDataBoundEventHandler)base.Events[RadMap.itemDataBoundEvent];
			if (mapItemDataBoundEventHandler != null)
			{
				mapItemDataBoundEventHandler(this, e);
			}
		}

		// Token: 0x14000088 RID: 136
		// (add) Token: 0x06002724 RID: 10020 RVA: 0x0007F226 File Offset: 0x0007D426
		// (remove) Token: 0x06002725 RID: 10021 RVA: 0x0007F239 File Offset: 0x0007D439
		public event RadMap.MapItemCreatedEventHandler ItemCreated
		{
			add
			{
				base.Events.AddHandler(RadMap.itemCreatedEvent, value);
			}
			remove
			{
				base.Events.RemoveHandler(RadMap.itemCreatedEvent, value);
			}
		}

		// Token: 0x06002726 RID: 10022 RVA: 0x0007F24C File Offset: 0x0007D44C
		protected virtual void OnItemCreated(MapItemEventArgs e)
		{
			RadMap.MapItemCreatedEventHandler mapItemCreatedEventHandler = (RadMap.MapItemCreatedEventHandler)base.Events[RadMap.itemCreatedEvent];
			if (mapItemCreatedEventHandler != null)
			{
				mapItemCreatedEventHandler(this, e);
			}
		}

		// Token: 0x06002727 RID: 10023 RVA: 0x0007F27A File Offset: 0x0007D47A
		protected override void PerformDataBinding(IEnumerable data)
		{
			base.PerformDataBinding(data);
			this.BindMarkers(data);
			this.BindLayers();
		}

		// Token: 0x06002728 RID: 10024 RVA: 0x0007F290 File Offset: 0x0007D490
		private void BindMarkers(IEnumerable data)
		{
			if (data != null)
			{
				if (!this.AppendDataBoundMarkers)
				{
					this.MarkersCollection.Clear();
				}
				foreach (object dataItem in data)
				{
					MapMarker item = this.CreateMarker(dataItem, this.DataBindings.MarkerBinding);
					this.MarkersCollection.Add(item);
				}
			}
		}

		// Token: 0x06002729 RID: 10025 RVA: 0x0007F310 File Offset: 0x0007D510
		private void BindLayers()
		{
			if (this.LayersDataSource == null)
			{
				if (!string.IsNullOrEmpty(this.LayersDataSourceID))
				{
					this.BindLayers(this.LayersDataSourceID);
				}
				return;
			}
			if (this.LayersDataSource is IListSource)
			{
				this.BindLayers(((IListSource)this.LayersDataSource).GetList());
				return;
			}
			if (this.LayersDataSource is IDataSource)
			{
				this.BindLayers((IDataSource)this.LayersDataSource);
				return;
			}
			this.BindLayers(this.LayersDataSource as IEnumerable);
		}

		// Token: 0x0600272A RID: 10026 RVA: 0x0007F394 File Offset: 0x0007D594
		private void BindLayers(IEnumerable data)
		{
			if (data != null)
			{
				if (!this.AppendDataBoundLayers)
				{
					this.LayersCollection.Clear();
				}
				foreach (object dataItem in data)
				{
					MapLayer item = this.CreateLayer(dataItem, this.DataBindings.LayerBinding);
					this.LayersCollection.Add(item);
				}
			}
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x0007F414 File Offset: 0x0007D614
		public void BindLayers(IDataSource dataSource)
		{
			DataSourceView view = dataSource.GetView("");
			view.Select(DataSourceSelectArguments.Empty, new DataSourceViewSelectCallback(this.BindLayers));
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x0007F444 File Offset: 0x0007D644
		private void BindLayers(string dataSourceID)
		{
			IDataSource dataSource = this.FindControl(dataSourceID) as IDataSource;
			if (dataSource == null)
			{
				throw new Exception(string.Format("The LayersDataSourceID of '{0}' must be the ID of a control of type IDataSource.  A control with ID '{1}' could not be found.", this.ID, dataSourceID));
			}
			this.BindLayers(dataSource);
		}

		// Token: 0x0600272D RID: 10029 RVA: 0x0007F480 File Offset: 0x0007D680
		private MapLayer CreateLayer(object dataItem, LayerBinding layerBinding)
		{
			MapLayer mapLayer = new MapLayer();
			this.OnItemCreated(new MapItemEventArgs(mapLayer));
			this.SetPropertyValue("Type", mapLayer, dataItem, layerBinding.DataTypeField);
			this.SetPropertyValue("UrlTemplate", mapLayer, dataItem, layerBinding.DataUrlTemplateField);
			this.SetPropertyValue("MinZoom", mapLayer, dataItem, layerBinding.DataMinZoomField);
			this.SetPropertyValue("MaxZoom", mapLayer, dataItem, layerBinding.DataMaxZoomField);
			this.SetPropertyValue("Opacity", mapLayer, dataItem, layerBinding.DataOpacityField);
			this.SetPropertyValue("Attribution", mapLayer, dataItem, layerBinding.DataAttributionField);
			this.SetPropertyValue("Key", mapLayer, dataItem, layerBinding.DataKeyField);
			if (!string.IsNullOrEmpty(layerBinding.DataSubdomainsField))
			{
				string propertyValue = DataBinder.GetPropertyValue(dataItem, layerBinding.DataSubdomainsField, null);
				if (!string.IsNullOrEmpty(propertyValue))
				{
					mapLayer.Subdomains = propertyValue.Split(new char[]
					{
						','
					});
				}
			}
			this.OnItemDataBound(new MapItemDataBoundEventArgs(mapLayer, dataItem));
			return mapLayer;
		}

		// Token: 0x0600272E RID: 10030 RVA: 0x0007F570 File Offset: 0x0007D770
		private MapMarker CreateMarker(object dataItem, MarkerBinding markerDataBindingSettings)
		{
			MapMarker mapMarker = new MapMarker();
			this.OnItemCreated(new MapItemEventArgs(mapMarker));
			if (!string.IsNullOrEmpty(markerDataBindingSettings.DataLocationLatitudeField))
			{
				string propertyValue = DataBinder.GetPropertyValue(dataItem, markerDataBindingSettings.DataLocationLatitudeField, null);
				if (!string.IsNullOrEmpty(propertyValue))
				{
					mapMarker.LocationSettings.Latitude = double.Parse(propertyValue);
				}
			}
			if (!string.IsNullOrEmpty(markerDataBindingSettings.DataLocationLongitudeField))
			{
				string propertyValue2 = DataBinder.GetPropertyValue(dataItem, markerDataBindingSettings.DataLocationLongitudeField, null);
				if (!string.IsNullOrEmpty(propertyValue2))
				{
					mapMarker.LocationSettings.Longitude = double.Parse(propertyValue2);
				}
			}
			if (!string.IsNullOrEmpty(markerDataBindingSettings.DataTitleField))
			{
				string propertyValue3 = DataBinder.GetPropertyValue(dataItem, markerDataBindingSettings.DataTitleField, null);
				if (!string.IsNullOrEmpty(propertyValue3))
				{
					mapMarker.Title = propertyValue3;
				}
			}
			if (!string.IsNullOrEmpty(markerDataBindingSettings.DataShapeField))
			{
				string propertyValue4 = DataBinder.GetPropertyValue(dataItem, markerDataBindingSettings.DataShapeField, null);
				if (!string.IsNullOrEmpty(propertyValue4))
				{
					mapMarker.Shape = propertyValue4;
				}
			}
			if (!string.IsNullOrEmpty(markerDataBindingSettings.DataTooltipTemplateField))
			{
				string propertyValue5 = DataBinder.GetPropertyValue(dataItem, markerDataBindingSettings.DataTooltipTemplateField, null);
				if (!string.IsNullOrEmpty(propertyValue5))
				{
					mapMarker.TooltipSettings.Template = propertyValue5;
				}
			}
			if (!string.IsNullOrEmpty(markerDataBindingSettings.DataTooltipContentField))
			{
				string propertyValue6 = DataBinder.GetPropertyValue(dataItem, markerDataBindingSettings.DataTooltipContentField, null);
				if (!string.IsNullOrEmpty(propertyValue6))
				{
					mapMarker.TooltipSettings.Content = propertyValue6;
				}
			}
			this.OnItemDataBound(new MapItemDataBoundEventArgs(mapMarker, dataItem));
			return mapMarker;
		}

		// Token: 0x0600272F RID: 10031 RVA: 0x0007F6C0 File Offset: 0x0007D8C0
		private void SetPropertyValue(string propName, object recipient, object dataItem, string dataField)
		{
			if (string.IsNullOrEmpty(dataField))
			{
				return;
			}
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(recipient)[propName];
			string propertyValue = DataBinder.GetPropertyValue(dataItem, dataField, null);
			if (!string.IsNullOrEmpty(propertyValue))
			{
				if (propertyDescriptor.PropertyType == typeof(double))
				{
					propertyDescriptor.SetValue(recipient, double.Parse(propertyValue));
					return;
				}
				if (propertyDescriptor.PropertyType.IsEnum)
				{
					propertyDescriptor.SetValue(recipient, Enum.Parse(propertyDescriptor.PropertyType, propertyValue, true));
					return;
				}
				propertyDescriptor.SetValue(recipient, propertyValue);
			}
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x06002730 RID: 10032 RVA: 0x0007F74A File Offset: 0x0007D94A
		protected internal override bool SupportsRenderingMode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002731 RID: 10033 RVA: 0x0007F74D File Offset: 0x0007D94D
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06002732 RID: 10034 RVA: 0x0007F756 File Offset: 0x0007D956
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadDataBoundControl.DescribeEvent(descriptor, "initialize", this.ClientEvents.OnInitialize);
			RadDataBoundControl.DescribeEvent(descriptor, "load", this.ClientEvents.OnLoad);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x06002733 RID: 10035 RVA: 0x0007F78B File Offset: 0x0007D98B
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}

		// Token: 0x06002734 RID: 10036 RVA: 0x0007F79A File Offset: 0x0007D99A
		private void InitializeComponent()
		{
			base.PreRender += this.Map_PreRender;
		}

		// Token: 0x06002735 RID: 10037 RVA: 0x0007F7B0 File Offset: 0x0007D9B0
		private void Map_PreRender(object sender, EventArgs e)
		{
			RadMap radMap = sender as RadMap;
			if (radMap != null)
			{
				foreach (object obj in radMap.LayersCollection)
				{
					MapLayer mapLayer = (MapLayer)obj;
					if (!string.IsNullOrEmpty(mapLayer.ClientDataSourceID))
					{
						RadClientDataSource radClientDataSource = this.FindControl(mapLayer.ClientDataSourceID) as RadClientDataSource;
						if (radClientDataSource != null)
						{
							mapLayer.ClientDataSourceID = radClientDataSource.ClientID;
						}
					}
				}
			}
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x0007F840 File Offset: 0x0007DA40
		public RadMap()
		{
			this.RegisterJSConverters();
		}

		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x06002737 RID: 10039 RVA: 0x0007F871 File Offset: 0x0007DA71
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Location CenterSettings
		{
			get
			{
				if (this._center == null)
				{
					this._center = new Location();
				}
				return this._center;
			}
		}

		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x06002738 RID: 10040 RVA: 0x0007F88C File Offset: 0x0007DA8C
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Controls ControlsSettings
		{
			get
			{
				if (this._controls == null)
				{
					this._controls = new Controls();
				}
				return this._controls;
			}
		}

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x06002739 RID: 10041 RVA: 0x0007F8A7 File Offset: 0x0007DAA7
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public LayerDefaults LayerDefaultsSettings
		{
			get
			{
				if (this._layerDefaults == null)
				{
					this._layerDefaults = new LayerDefaults();
				}
				return this._layerDefaults;
			}
		}

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x0600273A RID: 10042 RVA: 0x0007F8C2 File Offset: 0x0007DAC2
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public MapLayersCollection LayersCollection
		{
			get
			{
				if (this._layers == null)
				{
					this._layers = new MapLayersCollection();
				}
				return this._layers;
			}
		}

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x0600273B RID: 10043 RVA: 0x0007F8DD File Offset: 0x0007DADD
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public MarkerDefaults MarkerDefaultsSettings
		{
			get
			{
				if (this._markerDefaults == null)
				{
					this._markerDefaults = new MarkerDefaults();
				}
				return this._markerDefaults;
			}
		}

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x0600273C RID: 10044 RVA: 0x0007F8F8 File Offset: 0x0007DAF8
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public MapMarkersCollection MarkersCollection
		{
			get
			{
				if (this._markers == null)
				{
					this._markers = new MapMarkersCollection();
				}
				return this._markers;
			}
		}

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x0600273D RID: 10045 RVA: 0x0007F913 File Offset: 0x0007DB13
		// (set) Token: 0x0600273E RID: 10046 RVA: 0x0007F93C File Offset: 0x0007DB3C
		[DefaultValue(1.0)]
		public double MinZoom
		{
			get
			{
				return (double)(this.ViewState["MinZoom"] ?? 1.0);
			}
			set
			{
				this.ViewState["MinZoom"] = value;
			}
		}

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x0600273F RID: 10047 RVA: 0x0007F954 File Offset: 0x0007DB54
		// (set) Token: 0x06002740 RID: 10048 RVA: 0x0007F97D File Offset: 0x0007DB7D
		[DefaultValue(19.0)]
		public double MaxZoom
		{
			get
			{
				return (double)(this.ViewState["MaxZoom"] ?? 19.0);
			}
			set
			{
				this.ViewState["MaxZoom"] = value;
			}
		}

		// Token: 0x17000CA6 RID: 3238
		// (get) Token: 0x06002741 RID: 10049 RVA: 0x0007F995 File Offset: 0x0007DB95
		// (set) Token: 0x06002742 RID: 10050 RVA: 0x0007F9BE File Offset: 0x0007DBBE
		[DefaultValue(256.0)]
		public double MinSize
		{
			get
			{
				return (double)(this.ViewState["MinSize"] ?? 256.0);
			}
			set
			{
				this.ViewState["MinSize"] = value;
			}
		}

		// Token: 0x17000CA7 RID: 3239
		// (get) Token: 0x06002743 RID: 10051 RVA: 0x0007F9D6 File Offset: 0x0007DBD6
		// (set) Token: 0x06002744 RID: 10052 RVA: 0x0007F9F7 File Offset: 0x0007DBF7
		[DefaultValue(true)]
		public bool Pannable
		{
			get
			{
				return (bool)(this.ViewState["Pannable"] ?? true);
			}
			set
			{
				this.ViewState["Pannable"] = value;
			}
		}

		// Token: 0x17000CA8 RID: 3240
		// (get) Token: 0x06002745 RID: 10053 RVA: 0x0007FA0F File Offset: 0x0007DC0F
		// (set) Token: 0x06002746 RID: 10054 RVA: 0x0007FA30 File Offset: 0x0007DC30
		[DefaultValue(true)]
		public bool Wraparound
		{
			get
			{
				return (bool)(this.ViewState["Wraparound"] ?? true);
			}
			set
			{
				this.ViewState["Wraparound"] = value;
			}
		}

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x06002747 RID: 10055 RVA: 0x0007FA48 File Offset: 0x0007DC48
		// (set) Token: 0x06002748 RID: 10056 RVA: 0x0007FA71 File Offset: 0x0007DC71
		[DefaultValue(3.0)]
		public double Zoom
		{
			get
			{
				return (double)(this.ViewState["Zoom"] ?? 3.0);
			}
			set
			{
				this.ViewState["Zoom"] = value;
			}
		}

		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x06002749 RID: 10057 RVA: 0x0007FA89 File Offset: 0x0007DC89
		// (set) Token: 0x0600274A RID: 10058 RVA: 0x0007FAAA File Offset: 0x0007DCAA
		[DefaultValue(true)]
		public bool Zoomable
		{
			get
			{
				return (bool)(this.ViewState["Zoomable"] ?? true);
			}
			set
			{
				this.ViewState["Zoomable"] = value;
			}
		}

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x0600274B RID: 10059 RVA: 0x0007FAC2 File Offset: 0x0007DCC2
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public MapClientEvents ClientEvents
		{
			get
			{
				if (this._clientEvents == null)
				{
					this._clientEvents = new MapClientEvents();
				}
				return this._clientEvents;
			}
		}

		// Token: 0x0600274C RID: 10060 RVA: 0x0007FADD File Offset: 0x0007DCDD
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddScriptProperty("_options", this.serializer.Serialize(this));
			descriptor.AddProperty("skin", base.RuntimeSkin);
		}

		// Token: 0x0600274D RID: 10061 RVA: 0x0007FB10 File Offset: 0x0007DD10
		protected override void OnPreRender(EventArgs e)
		{
			foreach (object obj in this.LayersCollection)
			{
				MapLayer mapLayer = (MapLayer)obj;
				if (!string.IsNullOrEmpty(mapLayer.ClientDataSourceID))
				{
					RadClientDataSource radClientDataSource = this.FindControl(mapLayer.ClientDataSourceID) as RadClientDataSource;
					if (radClientDataSource != null)
					{
						mapLayer.ClientDataSourceID = radClientDataSource.ClientID;
					}
				}
			}
			base.OnPreRender(e);
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x0007FB98 File Offset: 0x0007DD98
		private void RegisterJSConverters()
		{
			List<JavaScriptConverter> converters = new List<JavaScriptConverter>
			{
				new RadMapConverter(),
				new AttributionConverter(),
				new NavigatorConverter(),
				new ZoomConverter(),
				new ControlsConverter(),
				new CloseConverter(),
				new OpenConverter(),
				new AnimationConverter(),
				new ContentConverter(),
				new TooltipConverter(),
				new MarkerConverter(),
				new FillConverter(),
				new StrokeConverter(),
				new StyleConverter(),
				new ShapeConverter(),
				new BubbleConverter(),
				new TileConverter(),
				new BingConverter(),
				new LayerDefaultsConverter(),
				new MapLayerConverter(),
				new MarkerDefaultsConverter(),
				new MapMarkerConverter()
			};
			this.serializer.RegisterConverters(converters);
		}

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x0600274F RID: 10063 RVA: 0x0007FCAB File Offset: 0x0007DEAB
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06002750 RID: 10064 RVA: 0x0007FCAF File Offset: 0x0007DEAF
		protected override string CssClassFormatString
		{
			get
			{
				return "RadMap RadMap_{0}";
			}
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x0007FCB8 File Offset: 0x0007DEB8
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.CenterSettings).LoadViewState(array[num++]);
			((IStateManager)this.ClientEvents).LoadViewState(array[num++]);
			((IStateManager)this.ControlsSettings).LoadViewState(array[num++]);
			((IStateManager)this.LayerDefaultsSettings).LoadViewState(array[num++]);
			((IStateManager)this.LayersCollection).LoadViewState(array[num++]);
			((IStateManager)this.MarkerDefaultsSettings).LoadViewState(array[num++]);
			((IStateManager)this.MarkersCollection).LoadViewState(array[num++]);
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x0007FD5C File Offset: 0x0007DF5C
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.CenterSettings).SaveViewState(),
				((IStateManager)this.ClientEvents).SaveViewState(),
				((IStateManager)this.ControlsSettings).SaveViewState(),
				((IStateManager)this.LayerDefaultsSettings).SaveViewState(),
				((IStateManager)this.LayersCollection).SaveViewState(),
				((IStateManager)this.MarkerDefaultsSettings).SaveViewState(),
				((IStateManager)this.MarkersCollection).SaveViewState()
			};
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x0007FDE0 File Offset: 0x0007DFE0
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.CenterSettings).TrackViewState();
			((IStateManager)this.ClientEvents).TrackViewState();
			((IStateManager)this.ControlsSettings).TrackViewState();
			((IStateManager)this.LayerDefaultsSettings).TrackViewState();
			((IStateManager)this.LayersCollection).TrackViewState();
			((IStateManager)this.MarkerDefaultsSettings).TrackViewState();
			((IStateManager)this.MarkersCollection).TrackViewState();
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x0007FE40 File Offset: 0x0007E040
		protected override bool LoadPostData(string postDataKey, NameValueCollection postCollection)
		{
			bool result = false;
			string text = postCollection[base.ClientStateFieldID];
			if (string.IsNullOrEmpty(text))
			{
				return false;
			}
			RadMapClientState radMapClientState = null;
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			try
			{
				radMapClientState = javaScriptSerializer.Deserialize<RadMapClientState>(text);
			}
			catch (InvalidOperationException)
			{
			}
			catch (ArgumentException)
			{
			}
			if (radMapClientState == null)
			{
				return false;
			}
			if (this.Zoom != radMapClientState.Zoom)
			{
				this.Zoom = radMapClientState.Zoom;
				result = true;
			}
			if (this.CenterSettings.Latitude != radMapClientState.CenterLatitude)
			{
				this.CenterSettings.Latitude = radMapClientState.CenterLatitude;
				result = true;
			}
			if (this.CenterSettings.Longitude != radMapClientState.CenterLongitude)
			{
				this.CenterSettings.Longitude = radMapClientState.CenterLongitude;
				result = true;
			}
			return result;
		}

		// Token: 0x04000A06 RID: 2566
		private MapBinding _dataBindings;

		// Token: 0x04000A07 RID: 2567
		private object _layersDataSource;

		// Token: 0x04000A08 RID: 2568
		private static readonly object itemDataBoundEvent = new object();

		// Token: 0x04000A09 RID: 2569
		private static readonly object itemCreatedEvent = new object();

		// Token: 0x04000A0A RID: 2570
		private Location _center;

		// Token: 0x04000A0B RID: 2571
		private Controls _controls;

		// Token: 0x04000A0C RID: 2572
		private LayerDefaults _layerDefaults;

		// Token: 0x04000A0D RID: 2573
		private MapLayersCollection _layers;

		// Token: 0x04000A0E RID: 2574
		private MarkerDefaults _markerDefaults;

		// Token: 0x04000A0F RID: 2575
		private MapMarkersCollection _markers;

		// Token: 0x04000A10 RID: 2576
		private MapClientEvents _clientEvents;

		// Token: 0x04000A11 RID: 2577
		private AdvancedJavaScriptSerializer serializer = new AdvancedJavaScriptSerializer
		{
			MaxJsonLength = int.MaxValue
		};

		// Token: 0x02000444 RID: 1092
		// (Invoke) Token: 0x06002757 RID: 10071
		public delegate void MapItemDataBoundEventHandler(object sender, MapItemDataBoundEventArgs e);

		// Token: 0x02000445 RID: 1093
		// (Invoke) Token: 0x0600275B RID: 10075
		public delegate void MapItemCreatedEventHandler(object sender, MapItemEventArgs e);
	}
}

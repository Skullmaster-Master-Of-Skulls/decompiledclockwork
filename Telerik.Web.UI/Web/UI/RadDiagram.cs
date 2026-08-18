using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Licensing;
using Telerik.Web.UI.Diagram;
using Telerik.Web.UI.Diagram.DataBinding;

namespace Telerik.Web.UI
{
	// Token: 0x02000263 RID: 611
	[Designer("Telerik.Web.Design.RadDiagramDesigner, Telerik.Web.Design, Version=2021.2.616.45, Culture=neutral, PublicKeyToken=121fae78165ba3d4")]
	[LicenseProvider(typeof(TelerikLicenseProvider))]
	[RequiredScript(typeof(Html5DataVizDiagram))]
	[ClientScriptResource("Telerik.Web.UI.RadDiagram", "Telerik.Web.UI.Diagram.Scripts.RadDiagram.js")]
	[ClientScriptResource("Telerik.Web.UI.RadDiagram", "Telerik.Web.UI.Diagram.Scripts.RadDiagramSkins.js")]
	[EmbeddedSkin("Diagram")]
	[EmbeddedSkin("Diagram", "Default")]
	[RequiredCss("Telerik.Web.UI.Skins.HTML5UI.dataviz.css", RenderMode.Classic, typeof(RadDiagram))]
	[RequiredCss("Telerik.Web.UI.Skins.HTML5UI.dataviz.css", RenderMode.Lightweight, typeof(RadDiagram))]
	[ParseChildren(ChildrenAsProperties = true)]
	[ToolboxBitmap(typeof(RadDiagram), "Telerik.Web.UI.Diagram.png")]
	[TelerikToolboxCategory("Visualization")]
	public class RadDiagram : RadDataBoundControl
	{
		// Token: 0x060015FF RID: 5631 RVA: 0x0004ADE6 File Offset: 0x00048FE6
		public RadDiagram()
		{
			this.RegisterJSConverters();
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x06001600 RID: 5632 RVA: 0x0004ADFF File Offset: 0x00048FFF
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ConnectionDefaults ConnectionDefaultsSettings
		{
			get
			{
				if (this._connectionDefaults == null)
				{
					this._connectionDefaults = new ConnectionDefaults();
				}
				return this._connectionDefaults;
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x0004AE1A File Offset: 0x0004901A
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public DiagramConnectionsCollection ConnectionsCollection
		{
			get
			{
				if (this._connections == null)
				{
					this._connections = new DiagramConnectionsCollection();
				}
				return this._connections;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06001602 RID: 5634 RVA: 0x0004AE35 File Offset: 0x00049035
		// (set) Token: 0x06001603 RID: 5635 RVA: 0x0004AE56 File Offset: 0x00049056
		[DefaultValue(true)]
		public bool Editable
		{
			get
			{
				return (bool)(this.ViewState["Editable"] ?? true);
			}
			set
			{
				this.ViewState["Editable"] = value;
			}
		}

		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06001604 RID: 5636 RVA: 0x0004AE6E File Offset: 0x0004906E
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public DiagramEditable EditableSettings
		{
			get
			{
				if (this._editable == null)
				{
					this._editable = new DiagramEditable();
				}
				return this._editable;
			}
		}

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06001605 RID: 5637 RVA: 0x0004AE89 File Offset: 0x00049089
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public DiagramLayout LayoutSettings
		{
			get
			{
				if (this._layout == null)
				{
					this._layout = new DiagramLayout();
				}
				return this._layout;
			}
		}

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06001606 RID: 5638 RVA: 0x0004AEA4 File Offset: 0x000490A4
		// (set) Token: 0x06001607 RID: 5639 RVA: 0x0004AEC5 File Offset: 0x000490C5
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

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06001608 RID: 5640 RVA: 0x0004AEDD File Offset: 0x000490DD
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Pannable PannableSettings
		{
			get
			{
				if (this._pannable == null)
				{
					this._pannable = new Pannable();
				}
				return this._pannable;
			}
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x0004AEF8 File Offset: 0x000490F8
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public DiagramPdf PdfSettings
		{
			get
			{
				if (this._pdf == null)
				{
					this._pdf = new DiagramPdf();
				}
				return this._pdf;
			}
		}

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x0600160A RID: 5642 RVA: 0x0004AF13 File Offset: 0x00049113
		// (set) Token: 0x0600160B RID: 5643 RVA: 0x0004AF34 File Offset: 0x00049134
		[DefaultValue(true)]
		public bool Selectable
		{
			get
			{
				return (bool)(this.ViewState["Selectable"] ?? true);
			}
			set
			{
				this.ViewState["Selectable"] = value;
			}
		}

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x0600160C RID: 5644 RVA: 0x0004AF4C File Offset: 0x0004914C
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Selectable SelectableSettings
		{
			get
			{
				if (this._selectable == null)
				{
					this._selectable = new Selectable();
				}
				return this._selectable;
			}
		}

		// Token: 0x1700078E RID: 1934
		// (get) Token: 0x0600160D RID: 5645 RVA: 0x0004AF67 File Offset: 0x00049167
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ShapeDefaults ShapeDefaultsSettings
		{
			get
			{
				if (this._shapeDefaults == null)
				{
					this._shapeDefaults = new ShapeDefaults();
				}
				return this._shapeDefaults;
			}
		}

		// Token: 0x1700078F RID: 1935
		// (get) Token: 0x0600160E RID: 5646 RVA: 0x0004AF82 File Offset: 0x00049182
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public DiagramShapesCollection ShapesCollection
		{
			get
			{
				if (this._shapes == null)
				{
					this._shapes = new DiagramShapesCollection();
				}
				return this._shapes;
			}
		}

		// Token: 0x17000790 RID: 1936
		// (get) Token: 0x0600160F RID: 5647 RVA: 0x0004AF9D File Offset: 0x0004919D
		// (set) Token: 0x06001610 RID: 5648 RVA: 0x0004AFBD File Offset: 0x000491BD
		[DefaultValue("")]
		public string Template
		{
			get
			{
				return (string)(this.ViewState["Template"] ?? "");
			}
			set
			{
				this.ViewState["Template"] = value;
			}
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x06001611 RID: 5649 RVA: 0x0004AFD0 File Offset: 0x000491D0
		// (set) Token: 0x06001612 RID: 5650 RVA: 0x0004AFF9 File Offset: 0x000491F9
		[DefaultValue(1.0)]
		public double Zoom
		{
			get
			{
				return (double)(this.ViewState["Zoom"] ?? 1.0);
			}
			set
			{
				this.ViewState["Zoom"] = value;
			}
		}

		// Token: 0x17000792 RID: 1938
		// (get) Token: 0x06001613 RID: 5651 RVA: 0x0004B011 File Offset: 0x00049211
		// (set) Token: 0x06001614 RID: 5652 RVA: 0x0004B03A File Offset: 0x0004923A
		[DefaultValue(2.0)]
		public double ZoomMax
		{
			get
			{
				return (double)(this.ViewState["ZoomMax"] ?? 2.0);
			}
			set
			{
				this.ViewState["ZoomMax"] = value;
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001615 RID: 5653 RVA: 0x0004B052 File Offset: 0x00049252
		// (set) Token: 0x06001616 RID: 5654 RVA: 0x0004B07B File Offset: 0x0004927B
		[DefaultValue(0.1)]
		public double ZoomMin
		{
			get
			{
				return (double)(this.ViewState["ZoomMin"] ?? 0.1);
			}
			set
			{
				this.ViewState["ZoomMin"] = value;
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001617 RID: 5655 RVA: 0x0004B093 File Offset: 0x00049293
		// (set) Token: 0x06001618 RID: 5656 RVA: 0x0004B0BC File Offset: 0x000492BC
		[DefaultValue(0.1)]
		public double ZoomRate
		{
			get
			{
				return (double)(this.ViewState["ZoomRate"] ?? 0.1);
			}
			set
			{
				this.ViewState["ZoomRate"] = value;
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001619 RID: 5657 RVA: 0x0004B0D4 File Offset: 0x000492D4
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public DiagramClientEvents ClientEvents
		{
			get
			{
				if (this._clientEvents == null)
				{
					this._clientEvents = new DiagramClientEvents();
				}
				return this._clientEvents;
			}
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x0004B0EF File Offset: 0x000492EF
		protected override void DescribeComponent(IScriptDescriptor descriptor)
		{
			base.DescribeComponent(descriptor);
			descriptor.AddScriptProperty("_options", this.serializer.Serialize(this));
		}

		// Token: 0x0600161B RID: 5659 RVA: 0x0004B110 File Offset: 0x00049310
		private void RegisterJSConverters()
		{
			List<JavaScriptConverter> converters = new List<JavaScriptConverter>
			{
				new RadDiagramConverter(),
				new ContentConverter(),
				new DiagramConnectionEditableToolConverter(),
				new ConnectionEditableConverter(),
				new FillConverter(),
				new StrokeConverter(),
				new EndCapConverter(),
				new ConnectionStrokeConverter(),
				new ConnectionHoverConverter(),
				new HandlesConverter(),
				new SelectionConverter(),
				new StartCapConverter(),
				new ConnectionDefaultsConverter(),
				new DiagramConnectionPointConverter(),
				new DiagramConnectionConverter(),
				new SnapConverter(),
				new DragConverter(),
				new HoverConverter(),
				new ResizeConverter(),
				new RotateConverter(),
				new DiagramEditableToolConverter(),
				new DiagramEditableConverter(),
				new DiagramGridConverter(),
				new DiagramLayoutConverter(),
				new PannableConverter(),
				new MarginConverter(),
				new DiagramPdfConverter(),
				new SelectableConverter(),
				new DiagramShapeConnectorConverter(),
				new ConnectorDefaultsConverter(),
				new DiagramShapeEditableToolConverter(),
				new ShapeEditableConverter(),
				new DiagramGradientStopConverter(),
				new GradientConverter(),
				new ShapeFillConverter(),
				new ShapeHoverConverter(),
				new ShapeRotationConverter(),
				new ShapeStrokeConverter(),
				new ShapeDefaultsConverter(),
				new DiagramShapeConverter(),
				new ConnectionContentConverter(),
				new ConnectionEndPointConverter(),
				new ShapeContentConverter()
			};
			this.serializer.RegisterConverters(converters);
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x0600161C RID: 5660 RVA: 0x0004B30A File Offset: 0x0004950A
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x0600161D RID: 5661 RVA: 0x0004B30E File Offset: 0x0004950E
		protected override string CssClassFormatString
		{
			get
			{
				return "RadDiagram RadDiagram_{0}";
			}
		}

		// Token: 0x0600161E RID: 5662 RVA: 0x0004B318 File Offset: 0x00049518
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.ClientEvents).LoadViewState(array[num++]);
			((IStateManager)this.ConnectionDefaultsSettings).LoadViewState(array[num++]);
			((IStateManager)this.ConnectionsCollection).LoadViewState(array[num++]);
			((IStateManager)this.EditableSettings).LoadViewState(array[num++]);
			((IStateManager)this.LayoutSettings).LoadViewState(array[num++]);
			((IStateManager)this.PannableSettings).LoadViewState(array[num++]);
			((IStateManager)this.PdfSettings).LoadViewState(array[num++]);
			((IStateManager)this.SelectableSettings).LoadViewState(array[num++]);
			((IStateManager)this.ShapeDefaultsSettings).LoadViewState(array[num++]);
			((IStateManager)this.ShapesCollection).LoadViewState(array[num++]);
		}

		// Token: 0x0600161F RID: 5663 RVA: 0x0004B3F0 File Offset: 0x000495F0
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.ClientEvents).SaveViewState(),
				((IStateManager)this.ConnectionDefaultsSettings).SaveViewState(),
				((IStateManager)this.ConnectionsCollection).SaveViewState(),
				((IStateManager)this.EditableSettings).SaveViewState(),
				((IStateManager)this.LayoutSettings).SaveViewState(),
				((IStateManager)this.PannableSettings).SaveViewState(),
				((IStateManager)this.PdfSettings).SaveViewState(),
				((IStateManager)this.SelectableSettings).SaveViewState(),
				((IStateManager)this.ShapeDefaultsSettings).SaveViewState(),
				((IStateManager)this.ShapesCollection).SaveViewState()
			};
		}

		// Token: 0x06001620 RID: 5664 RVA: 0x0004B4A0 File Offset: 0x000496A0
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.ClientEvents).TrackViewState();
			((IStateManager)this.ConnectionDefaultsSettings).TrackViewState();
			((IStateManager)this.ConnectionsCollection).TrackViewState();
			((IStateManager)this.EditableSettings).TrackViewState();
			((IStateManager)this.LayoutSettings).TrackViewState();
			((IStateManager)this.PannableSettings).TrackViewState();
			((IStateManager)this.PdfSettings).TrackViewState();
			((IStateManager)this.SelectableSettings).TrackViewState();
			((IStateManager)this.ShapeDefaultsSettings).TrackViewState();
			((IStateManager)this.ShapesCollection).TrackViewState();
		}

		// Token: 0x17000798 RID: 1944
		// (get) Token: 0x06001621 RID: 5665 RVA: 0x0004B521 File Offset: 0x00049721
		public BindingSettings BindingSettings
		{
			get
			{
				if (this._bindingSettings == null)
				{
					this._bindingSettings = new BindingSettings();
				}
				return this._bindingSettings;
			}
		}

		// Token: 0x17000799 RID: 1945
		// (get) Token: 0x06001622 RID: 5666 RVA: 0x0004B53C File Offset: 0x0004973C
		// (set) Token: 0x06001623 RID: 5667 RVA: 0x0004B544 File Offset: 0x00049744
		public object ConnectionDataSource { get; set; }

		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x06001624 RID: 5668 RVA: 0x0004B54D File Offset: 0x0004974D
		// (set) Token: 0x06001625 RID: 5669 RVA: 0x0004B555 File Offset: 0x00049755
		public string ConnectionDataSourceId { get; set; }

		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001626 RID: 5670 RVA: 0x0004B55E File Offset: 0x0004975E
		// (set) Token: 0x06001627 RID: 5671 RVA: 0x0004B57E File Offset: 0x0004977E
		[Category("Data")]
		[DefaultValue("")]
		public string ConnectionsClientDataSourceID
		{
			get
			{
				return (string)(this.ViewState["ConnectionsClientDataSourceID"] ?? "");
			}
			set
			{
				this.ViewState["ConnectionsClientDataSourceID"] = value;
			}
		}

		// Token: 0x14000032 RID: 50
		// (add) Token: 0x06001628 RID: 5672 RVA: 0x0004B594 File Offset: 0x00049794
		// (remove) Token: 0x06001629 RID: 5673 RVA: 0x0004B5CC File Offset: 0x000497CC
		public event RadDiagram.ItemDataBoundEventHandler ItemDataBound;

		// Token: 0x0600162A RID: 5674 RVA: 0x0004B601 File Offset: 0x00049801
		protected virtual void OnItemDataBound(object item, object dataItem)
		{
			if (this.ItemDataBound != null)
			{
				this.ItemDataBound(this, new DiagramItemDataBoundEventArgs(item, dataItem));
			}
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x0004B620 File Offset: 0x00049820
		protected override void PerformDataBinding(IEnumerable data)
		{
			base.PerformDataBinding(data);
			if (data != null)
			{
				foreach (object dataItem in data)
				{
					DiagramShape item = this.CreateShape(dataItem, this.BindingSettings.ShapeSettings);
					this.ShapesCollection.Add(item);
				}
			}
			if (this.ConnectionDataSource != null || !string.IsNullOrEmpty(this.ConnectionDataSourceId))
			{
				if (this.ConnectionDataSource != null)
				{
					this.BindConnectionsToDataSource(this.ConnectionDataSource);
					return;
				}
				this.BindConnections(this.ConnectionDataSourceId);
			}
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x0004B6C8 File Offset: 0x000498C8
		private void BindConnectionsToDataSource(object dataSource)
		{
			IDataSource dataSource2 = dataSource as IDataSource;
			if (dataSource2 != null)
			{
				this.BindConnections(dataSource2);
				return;
			}
			this.BindConnections(dataSource as IEnumerable);
		}

		// Token: 0x0600162D RID: 5677 RVA: 0x0004B6F4 File Offset: 0x000498F4
		private DiagramShape CreateShape(object dataItem, ShapeSettings shapeSettings)
		{
			DiagramShape diagramShape = new DiagramShape();
			this.SetPropertyValue("X", diagramShape, dataItem, shapeSettings.DataXField);
			this.SetPropertyValue("Y", diagramShape, dataItem, shapeSettings.DataYField);
			this.SetPropertyValue("Width", diagramShape, dataItem, shapeSettings.DataWidthField);
			this.SetPropertyValue("Height", diagramShape, dataItem, shapeSettings.DataHeightField);
			this.SetPropertyValue("MinWidth", diagramShape, dataItem, shapeSettings.DataMinWidthField);
			this.SetPropertyValue("MinHeight", diagramShape, dataItem, shapeSettings.DataMinHeightField);
			this.SetPropertyValue("Color", diagramShape.FillSettings, dataItem, shapeSettings.DataFillColorField);
			this.SetPropertyValue("Id", diagramShape, dataItem, shapeSettings.DataIdField);
			this.SetPropertyValue("Align", diagramShape.ContentSettings, dataItem, shapeSettings.DataContentAlignField);
			this.SetPropertyValue("Text", diagramShape.ContentSettings, dataItem, shapeSettings.DataContentTextField);
			this.SetPropertyValue("Path", diagramShape, dataItem, shapeSettings.DataPathField);
			this.SetPropertyValue("Type", diagramShape, dataItem, shapeSettings.DataTypeField);
			this.SetPropertyValue("Color", diagramShape.HoverSettings.FillSettings, dataItem, shapeSettings.DataHoverFillColorField);
			this.SetPropertyValue("Color", diagramShape.StrokeSettings, dataItem, shapeSettings.DataStrokeColorField);
			this.SetPropertyValue("DashType", diagramShape.StrokeSettings, dataItem, shapeSettings.DataStrokeDashTypeField);
			this.SetPropertyValue("Width", diagramShape.StrokeSettings, dataItem, shapeSettings.DataStrokeWidthField);
			this.SetPropertyValue("Angle", diagramShape.RotationSettings, dataItem, shapeSettings.DataRotationAngleField);
			this.OnItemDataBound(diagramShape, dataItem);
			return diagramShape;
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x0004B880 File Offset: 0x00049A80
		private DiagramConnection CreateConnection(object dataItem, ConnectionSettings connSettings)
		{
			DiagramConnection diagramConnection = new DiagramConnection();
			this.SetPropertyValue("StartCap", diagramConnection, dataItem, connSettings.DataStartCapField);
			this.SetPropertyValue("EndCap", diagramConnection, dataItem, connSettings.DataEndCapField);
			this.SetPropertyValue("Connector", diagramConnection.FromSettings, dataItem, connSettings.DataFromConnectorField);
			this.SetPropertyValue("ShapeId", diagramConnection.FromSettings, dataItem, connSettings.DataFromShapeIdField);
			this.SetPropertyValue("Connector", diagramConnection.ToSettings, dataItem, connSettings.DataToConnectorField);
			this.SetPropertyValue("ShapeId", diagramConnection.ToSettings, dataItem, connSettings.DataToShapeIdField);
			this.SetPropertyValue("Color", diagramConnection.StrokeSettings, dataItem, connSettings.DataStrokeColorField);
			this.SetPropertyValue("Color", diagramConnection.HoverSettings.StrokeSettings, dataItem, connSettings.DataHoverStrokeColorField);
			this.OnItemDataBound(diagramConnection, dataItem);
			return diagramConnection;
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x0004B958 File Offset: 0x00049B58
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

		// Token: 0x06001630 RID: 5680 RVA: 0x0004B9E4 File Offset: 0x00049BE4
		private void BindConnections(IEnumerable data)
		{
			foreach (object dataItem in data)
			{
				DiagramConnection item = this.CreateConnection(dataItem, this.BindingSettings.ConnectionSettings);
				this.ConnectionsCollection.Add(item);
			}
		}

		// Token: 0x06001631 RID: 5681 RVA: 0x0004BA4C File Offset: 0x00049C4C
		private void BindConnections(string dataSourceId)
		{
			IDataSource dataSource = ChildControlHelper.FindControlRecursive(this, dataSourceId, null) as IDataSource;
			if (dataSource == null)
			{
				throw new Exception(string.Format("The ConnectionDataSourceID of '{0}' must be the ID of a control of type IDataSource.  A control with ID '{1}' could not be found.", this.ID, dataSourceId));
			}
			this.BindConnections(dataSource);
		}

		// Token: 0x06001632 RID: 5682 RVA: 0x0004BA88 File Offset: 0x00049C88
		private void BindConnections(IDataSource dataSource)
		{
			DataSourceView view = dataSource.GetView("");
			view.Select(DataSourceSelectArguments.Empty, new DataSourceViewSelectCallback(this.BindConnections));
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x0004BAB8 File Offset: 0x00049CB8
		public string ResolveClientDataSourceID(string dataSourceID)
		{
			if (string.IsNullOrEmpty(dataSourceID))
			{
				return dataSourceID;
			}
			string result;
			try
			{
				Control control = DataSourceControlHelper.FindControl(this, dataSourceID);
				result = control.ClientID;
			}
			catch (Exception)
			{
				result = dataSourceID;
			}
			return result;
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x0004BAF8 File Offset: 0x00049CF8
		protected internal override void DescribeClientProperties(IScriptDescriptor descriptor)
		{
			base.DescribeClientProperties(descriptor);
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x0004BB01 File Offset: 0x00049D01
		protected internal override void DescribeClientEvents(IScriptDescriptor descriptor)
		{
			RadDataBoundControl.DescribeEvent(descriptor, "load", this.ClientEvents.OnLoad);
			base.DescribeClientEvents(descriptor);
		}

		// Token: 0x040005DA RID: 1498
		private ConnectionDefaults _connectionDefaults;

		// Token: 0x040005DB RID: 1499
		private DiagramConnectionsCollection _connections;

		// Token: 0x040005DC RID: 1500
		private DiagramEditable _editable;

		// Token: 0x040005DD RID: 1501
		private DiagramLayout _layout;

		// Token: 0x040005DE RID: 1502
		private Pannable _pannable;

		// Token: 0x040005DF RID: 1503
		private DiagramPdf _pdf;

		// Token: 0x040005E0 RID: 1504
		private Selectable _selectable;

		// Token: 0x040005E1 RID: 1505
		private ShapeDefaults _shapeDefaults;

		// Token: 0x040005E2 RID: 1506
		private DiagramShapesCollection _shapes;

		// Token: 0x040005E3 RID: 1507
		private DiagramClientEvents _clientEvents;

		// Token: 0x040005E4 RID: 1508
		private AdvancedJavaScriptSerializer serializer = new AdvancedJavaScriptSerializer();

		// Token: 0x040005E5 RID: 1509
		private BindingSettings _bindingSettings;

		// Token: 0x02000264 RID: 612
		// (Invoke) Token: 0x06001637 RID: 5687
		[SuppressMessage("Microsoft.Design", "CA1034:NestedTypesShouldNotBeVisible")]
		public delegate void ItemDataBoundEventHandler(object sender, DiagramItemDataBoundEventArgs e);
	}
}

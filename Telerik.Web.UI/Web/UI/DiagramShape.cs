using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.Diagram;

namespace Telerik.Web.UI
{
	// Token: 0x02000243 RID: 579
	public class DiagramShape : StateManager
	{
		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x0600153C RID: 5436 RVA: 0x0004902A File Offset: 0x0004722A
		// (set) Token: 0x0600153D RID: 5437 RVA: 0x0004904A File Offset: 0x0004724A
		[DefaultValue("")]
		public string Id
		{
			get
			{
				return (string)(base.ViewState["Id"] ?? "");
			}
			set
			{
				base.ViewState["Id"] = value;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x0004905D File Offset: 0x0004725D
		// (set) Token: 0x0600153F RID: 5439 RVA: 0x0004907E File Offset: 0x0004727E
		[DefaultValue(true)]
		public bool Editable
		{
			get
			{
				return (bool)(base.ViewState["Editable"] ?? true);
			}
			set
			{
				base.ViewState["Editable"] = value;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x00049096 File Offset: 0x00047296
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ShapeEditable EditableSettings
		{
			get
			{
				if (this._editable == null)
				{
					this._editable = new ShapeEditable();
				}
				return this._editable;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001541 RID: 5441 RVA: 0x000490B1 File Offset: 0x000472B1
		// (set) Token: 0x06001542 RID: 5442 RVA: 0x000490D1 File Offset: 0x000472D1
		[DefaultValue("")]
		public string Path
		{
			get
			{
				return (string)(base.ViewState["Path"] ?? "");
			}
			set
			{
				base.ViewState["Path"] = value;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001543 RID: 5443 RVA: 0x000490E4 File Offset: 0x000472E4
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ShapeStroke StrokeSettings
		{
			get
			{
				if (this._stroke == null)
				{
					this._stroke = new ShapeStroke();
				}
				return this._stroke;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06001544 RID: 5444 RVA: 0x000490FF File Offset: 0x000472FF
		// (set) Token: 0x06001545 RID: 5445 RVA: 0x0004911F File Offset: 0x0004731F
		[DefaultValue("rectangle")]
		public string Type
		{
			get
			{
				return (string)(base.ViewState["Type"] ?? "rectangle");
			}
			set
			{
				base.ViewState["Type"] = value;
			}
		}

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x00049132 File Offset: 0x00047332
		// (set) Token: 0x06001547 RID: 5447 RVA: 0x0004915B File Offset: 0x0004735B
		[DefaultValue(0.0)]
		public double X
		{
			get
			{
				return (double)(base.ViewState["X"] ?? 0.0);
			}
			set
			{
				base.ViewState["X"] = value;
			}
		}

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001548 RID: 5448 RVA: 0x00049173 File Offset: 0x00047373
		// (set) Token: 0x06001549 RID: 5449 RVA: 0x0004919C File Offset: 0x0004739C
		[DefaultValue(0.0)]
		public double Y
		{
			get
			{
				return (double)(base.ViewState["Y"] ?? 0.0);
			}
			set
			{
				base.ViewState["Y"] = value;
			}
		}

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x000491B4 File Offset: 0x000473B4
		// (set) Token: 0x0600154B RID: 5451 RVA: 0x000491DD File Offset: 0x000473DD
		[DefaultValue(20.0)]
		public double MinWidth
		{
			get
			{
				return (double)(base.ViewState["MinWidth"] ?? 20.0);
			}
			set
			{
				base.ViewState["MinWidth"] = value;
			}
		}

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x0600154C RID: 5452 RVA: 0x000491F5 File Offset: 0x000473F5
		// (set) Token: 0x0600154D RID: 5453 RVA: 0x0004921E File Offset: 0x0004741E
		[DefaultValue(20.0)]
		public double MinHeight
		{
			get
			{
				return (double)(base.ViewState["MinHeight"] ?? 20.0);
			}
			set
			{
				base.ViewState["MinHeight"] = value;
			}
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x0600154E RID: 5454 RVA: 0x00049236 File Offset: 0x00047436
		// (set) Token: 0x0600154F RID: 5455 RVA: 0x0004925F File Offset: 0x0004745F
		[DefaultValue(100.0)]
		public double Width
		{
			get
			{
				return (double)(base.ViewState["Width"] ?? 100.0);
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06001550 RID: 5456 RVA: 0x00049277 File Offset: 0x00047477
		// (set) Token: 0x06001551 RID: 5457 RVA: 0x000492A0 File Offset: 0x000474A0
		[DefaultValue(100.0)]
		public double Height
		{
			get
			{
				return (double)(base.ViewState["Height"] ?? 100.0);
			}
			set
			{
				base.ViewState["Height"] = value;
			}
		}

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001552 RID: 5458 RVA: 0x000492B8 File Offset: 0x000474B8
		// (set) Token: 0x06001553 RID: 5459 RVA: 0x000492D8 File Offset: 0x000474D8
		[DefaultValue("")]
		public string Fill
		{
			get
			{
				return (string)(base.ViewState["Fill"] ?? "");
			}
			set
			{
				base.ViewState["Fill"] = value;
			}
		}

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001554 RID: 5460 RVA: 0x000492EB File Offset: 0x000474EB
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ShapeFill FillSettings
		{
			get
			{
				if (this._fill == null)
				{
					this._fill = new ShapeFill();
				}
				return this._fill;
			}
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06001555 RID: 5461 RVA: 0x00049306 File Offset: 0x00047506
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ShapeHover HoverSettings
		{
			get
			{
				if (this._hover == null)
				{
					this._hover = new ShapeHover();
				}
				return this._hover;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001556 RID: 5462 RVA: 0x00049321 File Offset: 0x00047521
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public DiagramShapeConnectorsCollection ConnectorsCollection
		{
			get
			{
				if (this._connectors == null)
				{
					this._connectors = new DiagramShapeConnectorsCollection();
				}
				return this._connectors;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x0004933C File Offset: 0x0004753C
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ShapeRotation RotationSettings
		{
			get
			{
				if (this._rotation == null)
				{
					this._rotation = new ShapeRotation();
				}
				return this._rotation;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001558 RID: 5464 RVA: 0x00049357 File Offset: 0x00047557
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ShapeContent ContentSettings
		{
			get
			{
				if (this._content == null)
				{
					this._content = new ShapeContent();
				}
				return this._content;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x00049372 File Offset: 0x00047572
		// (set) Token: 0x0600155A RID: 5466 RVA: 0x00049393 File Offset: 0x00047593
		[DefaultValue(true)]
		public bool Selectable
		{
			get
			{
				return (bool)(base.ViewState["Selectable"] ?? true);
			}
			set
			{
				base.ViewState["Selectable"] = value;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x000493AB File Offset: 0x000475AB
		// (set) Token: 0x0600155C RID: 5468 RVA: 0x000493CB File Offset: 0x000475CB
		[DefaultValue("")]
		public string Visual
		{
			get
			{
				return (string)(base.ViewState["Visual"] ?? "");
			}
			set
			{
				base.ViewState["Visual"] = value;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x000493DE File Offset: 0x000475DE
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ConnectorDefaults ConnectorDefaultsSettings
		{
			get
			{
				if (this._connectorDefaults == null)
				{
					this._connectorDefaults = new ConnectorDefaults();
				}
				return this._connectorDefaults;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x000493F9 File Offset: 0x000475F9
		// (set) Token: 0x0600155F RID: 5471 RVA: 0x00049419 File Offset: 0x00047619
		[DefaultValue("")]
		public string Source
		{
			get
			{
				return (string)(base.ViewState["Source"] ?? "");
			}
			set
			{
				base.ViewState["Source"] = value;
			}
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x0004942C File Offset: 0x0004762C
		internal override void SetDirty()
		{
			base.SetDirty();
			this.ConnectorDefaultsSettings.SetDirty();
			this.ConnectorsCollection.SetDirty();
			this.ContentSettings.SetDirty();
			this.EditableSettings.SetDirty();
			this.FillSettings.SetDirty();
			this.HoverSettings.SetDirty();
			this.RotationSettings.SetDirty();
			this.StrokeSettings.SetDirty();
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x00049498 File Offset: 0x00047698
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.ConnectorDefaultsSettings).LoadViewState(array[num++]);
			((IStateManager)this.ConnectorsCollection).LoadViewState(array[num++]);
			((IStateManager)this.ContentSettings).LoadViewState(array[num++]);
			((IStateManager)this.EditableSettings).LoadViewState(array[num++]);
			((IStateManager)this.FillSettings).LoadViewState(array[num++]);
			((IStateManager)this.HoverSettings).LoadViewState(array[num++]);
			((IStateManager)this.RotationSettings).LoadViewState(array[num++]);
			((IStateManager)this.StrokeSettings).LoadViewState(array[num++]);
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x0004954C File Offset: 0x0004774C
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.ConnectorDefaultsSettings).SaveViewState(),
				((IStateManager)this.ConnectorsCollection).SaveViewState(),
				((IStateManager)this.ContentSettings).SaveViewState(),
				((IStateManager)this.EditableSettings).SaveViewState(),
				((IStateManager)this.FillSettings).SaveViewState(),
				((IStateManager)this.HoverSettings).SaveViewState(),
				((IStateManager)this.RotationSettings).SaveViewState(),
				((IStateManager)this.StrokeSettings).SaveViewState()
			};
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x000495E0 File Offset: 0x000477E0
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.ConnectorDefaultsSettings).TrackViewState();
			((IStateManager)this.ConnectorsCollection).TrackViewState();
			((IStateManager)this.ContentSettings).TrackViewState();
			((IStateManager)this.EditableSettings).TrackViewState();
			((IStateManager)this.FillSettings).TrackViewState();
			((IStateManager)this.HoverSettings).TrackViewState();
			((IStateManager)this.RotationSettings).TrackViewState();
			((IStateManager)this.StrokeSettings).TrackViewState();
		}

		// Token: 0x040005B0 RID: 1456
		private ShapeEditable _editable;

		// Token: 0x040005B1 RID: 1457
		private ShapeStroke _stroke;

		// Token: 0x040005B2 RID: 1458
		private ShapeFill _fill;

		// Token: 0x040005B3 RID: 1459
		private ShapeHover _hover;

		// Token: 0x040005B4 RID: 1460
		private DiagramShapeConnectorsCollection _connectors;

		// Token: 0x040005B5 RID: 1461
		private ShapeRotation _rotation;

		// Token: 0x040005B6 RID: 1462
		private ShapeContent _content;

		// Token: 0x040005B7 RID: 1463
		private ConnectorDefaults _connectorDefaults;
	}
}

using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x0200044D RID: 1101
	public class ShapeDefaults : StateManager, IDefaultCheck
	{
		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x060027AA RID: 10154 RVA: 0x00080D5E File Offset: 0x0007EF5E
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

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x060027AB RID: 10155 RVA: 0x00080D79 File Offset: 0x0007EF79
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

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x060027AC RID: 10156 RVA: 0x00080D94 File Offset: 0x0007EF94
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Content ContentSettings
		{
			get
			{
				if (this._content == null)
				{
					this._content = new Content();
				}
				return this._content;
			}
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x060027AD RID: 10157 RVA: 0x00080DAF File Offset: 0x0007EFAF
		// (set) Token: 0x060027AE RID: 10158 RVA: 0x00080DD0 File Offset: 0x0007EFD0
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

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x060027AF RID: 10159 RVA: 0x00080DE8 File Offset: 0x0007EFE8
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

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x060027B0 RID: 10160 RVA: 0x00080E03 File Offset: 0x0007F003
		// (set) Token: 0x060027B1 RID: 10161 RVA: 0x00080E23 File Offset: 0x0007F023
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

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x060027B2 RID: 10162 RVA: 0x00080E36 File Offset: 0x0007F036
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

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x060027B3 RID: 10163 RVA: 0x00080E51 File Offset: 0x0007F051
		// (set) Token: 0x060027B4 RID: 10164 RVA: 0x00080E7A File Offset: 0x0007F07A
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

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x060027B5 RID: 10165 RVA: 0x00080E92 File Offset: 0x0007F092
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

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x060027B6 RID: 10166 RVA: 0x00080EAD File Offset: 0x0007F0AD
		// (set) Token: 0x060027B7 RID: 10167 RVA: 0x00080ED6 File Offset: 0x0007F0D6
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

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x060027B8 RID: 10168 RVA: 0x00080EEE File Offset: 0x0007F0EE
		// (set) Token: 0x060027B9 RID: 10169 RVA: 0x00080F17 File Offset: 0x0007F117
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

		// Token: 0x17000CDB RID: 3291
		// (get) Token: 0x060027BA RID: 10170 RVA: 0x00080F2F File Offset: 0x0007F12F
		// (set) Token: 0x060027BB RID: 10171 RVA: 0x00080F4F File Offset: 0x0007F14F
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

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x060027BC RID: 10172 RVA: 0x00080F62 File Offset: 0x0007F162
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

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x060027BD RID: 10173 RVA: 0x00080F7D File Offset: 0x0007F17D
		// (set) Token: 0x060027BE RID: 10174 RVA: 0x00080F9E File Offset: 0x0007F19E
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

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x060027BF RID: 10175 RVA: 0x00080FB6 File Offset: 0x0007F1B6
		// (set) Token: 0x060027C0 RID: 10176 RVA: 0x00080FD6 File Offset: 0x0007F1D6
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

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x060027C1 RID: 10177 RVA: 0x00080FE9 File Offset: 0x0007F1E9
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

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x060027C2 RID: 10178 RVA: 0x00081004 File Offset: 0x0007F204
		// (set) Token: 0x060027C3 RID: 10179 RVA: 0x00081024 File Offset: 0x0007F224
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

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x060027C4 RID: 10180 RVA: 0x00081037 File Offset: 0x0007F237
		// (set) Token: 0x060027C5 RID: 10181 RVA: 0x00081057 File Offset: 0x0007F257
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

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x060027C6 RID: 10182 RVA: 0x0008106A File Offset: 0x0007F26A
		// (set) Token: 0x060027C7 RID: 10183 RVA: 0x00081093 File Offset: 0x0007F293
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

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x060027C8 RID: 10184 RVA: 0x000810AB File Offset: 0x0007F2AB
		// (set) Token: 0x060027C9 RID: 10185 RVA: 0x000810D4 File Offset: 0x0007F2D4
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

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x060027CA RID: 10186 RVA: 0x000810EC File Offset: 0x0007F2EC
		// (set) Token: 0x060027CB RID: 10187 RVA: 0x00081115 File Offset: 0x0007F315
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

		// Token: 0x060027CC RID: 10188 RVA: 0x00081130 File Offset: 0x0007F330
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

		// Token: 0x060027CD RID: 10189 RVA: 0x0008119C File Offset: 0x0007F39C
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

		// Token: 0x060027CE RID: 10190 RVA: 0x00081250 File Offset: 0x0007F450
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

		// Token: 0x060027CF RID: 10191 RVA: 0x000812E4 File Offset: 0x0007F4E4
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

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x060027D0 RID: 10192 RVA: 0x00081350 File Offset: 0x0007F550
		public bool IsDefault
		{
			get
			{
				return this.ConnectorsCollection.ItemsList.Count == 0 && this.ConnectorDefaultsSettings.IsDefault && this.ContentSettings.IsDefault && this.Editable && this.EditableSettings.IsDefault && this.Fill == "" && this.FillSettings.IsDefault && this.Height == 100.0 && this.HoverSettings.IsDefault && this.MinHeight == 20.0 && this.MinWidth == 20.0 && this.Path == "" && this.RotationSettings.IsDefault && this.Selectable && this.Source == "" && this.StrokeSettings.IsDefault && this.Type == "rectangle" && this.Visual == "" && this.Width == 100.0 && this.X == 0.0 && this.Y == 0.0;
			}
		}

		// Token: 0x04000A17 RID: 2583
		private DiagramShapeConnectorsCollection _connectors;

		// Token: 0x04000A18 RID: 2584
		private ConnectorDefaults _connectorDefaults;

		// Token: 0x04000A19 RID: 2585
		private Content _content;

		// Token: 0x04000A1A RID: 2586
		private ShapeEditable _editable;

		// Token: 0x04000A1B RID: 2587
		private ShapeFill _fill;

		// Token: 0x04000A1C RID: 2588
		private ShapeHover _hover;

		// Token: 0x04000A1D RID: 2589
		private ShapeRotation _rotation;

		// Token: 0x04000A1E RID: 2590
		private ShapeStroke _stroke;
	}
}

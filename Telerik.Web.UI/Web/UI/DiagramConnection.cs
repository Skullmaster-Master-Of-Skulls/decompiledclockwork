using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.Diagram;

namespace Telerik.Web.UI
{
	// Token: 0x0200022C RID: 556
	public class DiagramConnection : StateManager
	{
		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001475 RID: 5237 RVA: 0x00046DE7 File Offset: 0x00044FE7
		// (set) Token: 0x06001476 RID: 5238 RVA: 0x00046E07 File Offset: 0x00045007
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

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001477 RID: 5239 RVA: 0x00046E1A File Offset: 0x0004501A
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ConnectionContent ContentSettings
		{
			get
			{
				if (this._content == null)
				{
					this._content = new ConnectionContent();
				}
				return this._content;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x00046E35 File Offset: 0x00045035
		// (set) Token: 0x06001479 RID: 5241 RVA: 0x00046E56 File Offset: 0x00045056
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

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x00046E6E File Offset: 0x0004506E
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ConnectionEditable EditableSettings
		{
			get
			{
				if (this._editable == null)
				{
					this._editable = new ConnectionEditable();
				}
				return this._editable;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x00046E89 File Offset: 0x00045089
		// (set) Token: 0x0600147C RID: 5244 RVA: 0x00046EA9 File Offset: 0x000450A9
		[DefaultValue("Auto")]
		public string FromConnector
		{
			get
			{
				return (string)(base.ViewState["FromConnector"] ?? "Auto");
			}
			set
			{
				base.ViewState["FromConnector"] = value;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x00046EBC File Offset: 0x000450BC
		// (set) Token: 0x0600147E RID: 5246 RVA: 0x00046EE5 File Offset: 0x000450E5
		[DefaultValue(0.0)]
		public double FromX
		{
			get
			{
				return (double)(base.ViewState["FromX"] ?? 0.0);
			}
			set
			{
				base.ViewState["FromX"] = value;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x0600147F RID: 5247 RVA: 0x00046EFD File Offset: 0x000450FD
		// (set) Token: 0x06001480 RID: 5248 RVA: 0x00046F26 File Offset: 0x00045126
		[DefaultValue(0.0)]
		public double FromY
		{
			get
			{
				return (double)(base.ViewState["FromY"] ?? 0.0);
			}
			set
			{
				base.ViewState["FromY"] = value;
			}
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x00046F3E File Offset: 0x0004513E
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ConnectionStroke StrokeSettings
		{
			get
			{
				if (this._stroke == null)
				{
					this._stroke = new ConnectionStroke();
				}
				return this._stroke;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001482 RID: 5250 RVA: 0x00046F59 File Offset: 0x00045159
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ConnectionHover HoverSettings
		{
			get
			{
				if (this._hover == null)
				{
					this._hover = new ConnectionHover();
				}
				return this._hover;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x00046F74 File Offset: 0x00045174
		// (set) Token: 0x06001484 RID: 5252 RVA: 0x00046F95 File Offset: 0x00045195
		[DefaultValue(ConnectionStartCap.None)]
		public ConnectionStartCap StartCap
		{
			get
			{
				return (ConnectionStartCap)(base.ViewState["StartCap"] ?? ConnectionStartCap.None);
			}
			set
			{
				base.ViewState["StartCap"] = value;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x00046FAD File Offset: 0x000451AD
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public StartCap StartCapSettings
		{
			get
			{
				if (this._startCap == null)
				{
					this._startCap = new StartCap();
				}
				return this._startCap;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x00046FC8 File Offset: 0x000451C8
		// (set) Token: 0x06001487 RID: 5255 RVA: 0x00046FE9 File Offset: 0x000451E9
		[DefaultValue(ConnectionEndCap.None)]
		public ConnectionEndCap EndCap
		{
			get
			{
				return (ConnectionEndCap)(base.ViewState["EndCap"] ?? ConnectionEndCap.None);
			}
			set
			{
				base.ViewState["EndCap"] = value;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001488 RID: 5256 RVA: 0x00047001 File Offset: 0x00045201
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public EndCap EndCapSettings
		{
			get
			{
				if (this._endCap == null)
				{
					this._endCap = new EndCap();
				}
				return this._endCap;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x06001489 RID: 5257 RVA: 0x0004701C File Offset: 0x0004521C
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public DiagramConnectionPointsCollection PointsCollection
		{
			get
			{
				if (this._points == null)
				{
					this._points = new DiagramConnectionPointsCollection();
				}
				return this._points;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x0600148A RID: 5258 RVA: 0x00047037 File Offset: 0x00045237
		// (set) Token: 0x0600148B RID: 5259 RVA: 0x00047058 File Offset: 0x00045258
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

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x0600148C RID: 5260 RVA: 0x00047070 File Offset: 0x00045270
		// (set) Token: 0x0600148D RID: 5261 RVA: 0x00047090 File Offset: 0x00045290
		[DefaultValue("Auto")]
		public string ToConnector
		{
			get
			{
				return (string)(base.ViewState["ToConnector"] ?? "Auto");
			}
			set
			{
				base.ViewState["ToConnector"] = value;
			}
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x0600148E RID: 5262 RVA: 0x000470A3 File Offset: 0x000452A3
		// (set) Token: 0x0600148F RID: 5263 RVA: 0x000470CC File Offset: 0x000452CC
		[DefaultValue(0.0)]
		public double ToX
		{
			get
			{
				return (double)(base.ViewState["ToX"] ?? 0.0);
			}
			set
			{
				base.ViewState["ToX"] = value;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x000470E4 File Offset: 0x000452E4
		// (set) Token: 0x06001491 RID: 5265 RVA: 0x0004710D File Offset: 0x0004530D
		[DefaultValue(0.0)]
		public double ToY
		{
			get
			{
				return (double)(base.ViewState["ToY"] ?? 0.0);
			}
			set
			{
				base.ViewState["ToY"] = value;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06001492 RID: 5266 RVA: 0x00047125 File Offset: 0x00045325
		// (set) Token: 0x06001493 RID: 5267 RVA: 0x00047146 File Offset: 0x00045346
		[DefaultValue(ConnectionType.Cascading)]
		public ConnectionType Type
		{
			get
			{
				return (ConnectionType)(base.ViewState["Type"] ?? ConnectionType.Cascading);
			}
			set
			{
				base.ViewState["Type"] = value;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x0004715E File Offset: 0x0004535E
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ConnectionEndPoint FromSettings
		{
			get
			{
				if (this._from == null)
				{
					this._from = new ConnectionEndPoint();
				}
				return this._from;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x00047179 File Offset: 0x00045379
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ConnectionEndPoint ToSettings
		{
			get
			{
				if (this._to == null)
				{
					this._to = new ConnectionEndPoint();
				}
				return this._to;
			}
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x00047194 File Offset: 0x00045394
		internal override void SetDirty()
		{
			base.SetDirty();
			this.ContentSettings.SetDirty();
			this.EditableSettings.SetDirty();
			this.EndCapSettings.SetDirty();
			this.FromSettings.SetDirty();
			this.HoverSettings.SetDirty();
			this.PointsCollection.SetDirty();
			this.StartCapSettings.SetDirty();
			this.StrokeSettings.SetDirty();
			this.ToSettings.SetDirty();
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x0004720C File Offset: 0x0004540C
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.ContentSettings).LoadViewState(array[num++]);
			((IStateManager)this.EditableSettings).LoadViewState(array[num++]);
			((IStateManager)this.EndCapSettings).LoadViewState(array[num++]);
			((IStateManager)this.FromSettings).LoadViewState(array[num++]);
			((IStateManager)this.HoverSettings).LoadViewState(array[num++]);
			((IStateManager)this.PointsCollection).LoadViewState(array[num++]);
			((IStateManager)this.StartCapSettings).LoadViewState(array[num++]);
			((IStateManager)this.StrokeSettings).LoadViewState(array[num++]);
			((IStateManager)this.ToSettings).LoadViewState(array[num++]);
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x000472D4 File Offset: 0x000454D4
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.ContentSettings).SaveViewState(),
				((IStateManager)this.EditableSettings).SaveViewState(),
				((IStateManager)this.EndCapSettings).SaveViewState(),
				((IStateManager)this.FromSettings).SaveViewState(),
				((IStateManager)this.HoverSettings).SaveViewState(),
				((IStateManager)this.PointsCollection).SaveViewState(),
				((IStateManager)this.StartCapSettings).SaveViewState(),
				((IStateManager)this.StrokeSettings).SaveViewState(),
				((IStateManager)this.ToSettings).SaveViewState()
			};
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x00047374 File Offset: 0x00045574
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.ContentSettings).TrackViewState();
			((IStateManager)this.EditableSettings).TrackViewState();
			((IStateManager)this.EndCapSettings).TrackViewState();
			((IStateManager)this.FromSettings).TrackViewState();
			((IStateManager)this.HoverSettings).TrackViewState();
			((IStateManager)this.PointsCollection).TrackViewState();
			((IStateManager)this.StartCapSettings).TrackViewState();
			((IStateManager)this.StrokeSettings).TrackViewState();
			((IStateManager)this.ToSettings).TrackViewState();
		}

		// Token: 0x040005A1 RID: 1441
		private ConnectionContent _content;

		// Token: 0x040005A2 RID: 1442
		private ConnectionEditable _editable;

		// Token: 0x040005A3 RID: 1443
		private ConnectionStroke _stroke;

		// Token: 0x040005A4 RID: 1444
		private ConnectionHover _hover;

		// Token: 0x040005A5 RID: 1445
		private StartCap _startCap;

		// Token: 0x040005A6 RID: 1446
		private EndCap _endCap;

		// Token: 0x040005A7 RID: 1447
		private DiagramConnectionPointsCollection _points;

		// Token: 0x040005A8 RID: 1448
		private ConnectionEndPoint _from;

		// Token: 0x040005A9 RID: 1449
		private ConnectionEndPoint _to;
	}
}

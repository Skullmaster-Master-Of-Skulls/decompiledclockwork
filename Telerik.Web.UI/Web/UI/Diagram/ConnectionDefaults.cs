using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000216 RID: 534
	public class ConnectionDefaults : StateManager, IDefaultCheck
	{
		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x060013A3 RID: 5027 RVA: 0x00045062 File Offset: 0x00043262
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

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x060013A4 RID: 5028 RVA: 0x0004507D File Offset: 0x0004327D
		// (set) Token: 0x060013A5 RID: 5029 RVA: 0x0004509E File Offset: 0x0004329E
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

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x060013A6 RID: 5030 RVA: 0x000450B6 File Offset: 0x000432B6
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

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x060013A7 RID: 5031 RVA: 0x000450D1 File Offset: 0x000432D1
		// (set) Token: 0x060013A8 RID: 5032 RVA: 0x000450F2 File Offset: 0x000432F2
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

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x060013A9 RID: 5033 RVA: 0x0004510A File Offset: 0x0004330A
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

		// Token: 0x17000677 RID: 1655
		// (get) Token: 0x060013AA RID: 5034 RVA: 0x00045125 File Offset: 0x00043325
		// (set) Token: 0x060013AB RID: 5035 RVA: 0x00045145 File Offset: 0x00043345
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

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x00045158 File Offset: 0x00043358
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

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x060013AD RID: 5037 RVA: 0x00045173 File Offset: 0x00043373
		// (set) Token: 0x060013AE RID: 5038 RVA: 0x00045194 File Offset: 0x00043394
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

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x000451AC File Offset: 0x000433AC
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Selection SelectionSettings
		{
			get
			{
				if (this._selection == null)
				{
					this._selection = new Selection();
				}
				return this._selection;
			}
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x060013B0 RID: 5040 RVA: 0x000451C7 File Offset: 0x000433C7
		// (set) Token: 0x060013B1 RID: 5041 RVA: 0x000451E8 File Offset: 0x000433E8
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

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x060013B2 RID: 5042 RVA: 0x00045200 File Offset: 0x00043400
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

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x0004521B File Offset: 0x0004341B
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

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x00045236 File Offset: 0x00043436
		// (set) Token: 0x060013B5 RID: 5045 RVA: 0x00045256 File Offset: 0x00043456
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

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x00045269 File Offset: 0x00043469
		// (set) Token: 0x060013B7 RID: 5047 RVA: 0x0004528A File Offset: 0x0004348A
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

		// Token: 0x060013B8 RID: 5048 RVA: 0x000452A4 File Offset: 0x000434A4
		internal override void SetDirty()
		{
			base.SetDirty();
			this.ContentSettings.SetDirty();
			this.EditableSettings.SetDirty();
			this.EndCapSettings.SetDirty();
			this.HoverSettings.SetDirty();
			this.SelectionSettings.SetDirty();
			this.StartCapSettings.SetDirty();
			this.StrokeSettings.SetDirty();
		}

		// Token: 0x060013B9 RID: 5049 RVA: 0x00045304 File Offset: 0x00043504
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.ContentSettings).LoadViewState(array[num++]);
			((IStateManager)this.EditableSettings).LoadViewState(array[num++]);
			((IStateManager)this.EndCapSettings).LoadViewState(array[num++]);
			((IStateManager)this.HoverSettings).LoadViewState(array[num++]);
			((IStateManager)this.SelectionSettings).LoadViewState(array[num++]);
			((IStateManager)this.StartCapSettings).LoadViewState(array[num++]);
			((IStateManager)this.StrokeSettings).LoadViewState(array[num++]);
		}

		// Token: 0x060013BA RID: 5050 RVA: 0x000453A8 File Offset: 0x000435A8
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.ContentSettings).SaveViewState(),
				((IStateManager)this.EditableSettings).SaveViewState(),
				((IStateManager)this.EndCapSettings).SaveViewState(),
				((IStateManager)this.HoverSettings).SaveViewState(),
				((IStateManager)this.SelectionSettings).SaveViewState(),
				((IStateManager)this.StartCapSettings).SaveViewState(),
				((IStateManager)this.StrokeSettings).SaveViewState()
			};
		}

		// Token: 0x060013BB RID: 5051 RVA: 0x0004542C File Offset: 0x0004362C
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.ContentSettings).TrackViewState();
			((IStateManager)this.EditableSettings).TrackViewState();
			((IStateManager)this.EndCapSettings).TrackViewState();
			((IStateManager)this.HoverSettings).TrackViewState();
			((IStateManager)this.SelectionSettings).TrackViewState();
			((IStateManager)this.StartCapSettings).TrackViewState();
			((IStateManager)this.StrokeSettings).TrackViewState();
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x0004548C File Offset: 0x0004368C
		public bool IsDefault
		{
			get
			{
				return this.ContentSettings.IsDefault && this.Editable && this.EditableSettings.IsDefault && this.EndCap == ConnectionEndCap.None && this.EndCapSettings.IsDefault && this.FromConnector == "Auto" && this.HoverSettings.IsDefault && this.Selectable && this.SelectionSettings.IsDefault && this.StartCap == ConnectionStartCap.None && this.StartCapSettings.IsDefault && this.StrokeSettings.IsDefault && this.ToConnector == "Auto" && this.Type == ConnectionType.Cascading;
			}
		}

		// Token: 0x0400057C RID: 1404
		private ConnectionContent _content;

		// Token: 0x0400057D RID: 1405
		private ConnectionEditable _editable;

		// Token: 0x0400057E RID: 1406
		private EndCap _endCap;

		// Token: 0x0400057F RID: 1407
		private ConnectionHover _hover;

		// Token: 0x04000580 RID: 1408
		private Selection _selection;

		// Token: 0x04000581 RID: 1409
		private StartCap _startCap;

		// Token: 0x04000582 RID: 1410
		private ConnectionStroke _stroke;
	}
}

using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.Diagram;

namespace Telerik.Web.UI
{
	// Token: 0x02000235 RID: 565
	public class DiagramEditable : StateManager, IDefaultCheck
	{
		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x060014B2 RID: 5298 RVA: 0x0004784A File Offset: 0x00045A4A
		// (set) Token: 0x060014B3 RID: 5299 RVA: 0x0004786A File Offset: 0x00045A6A
		[DefaultValue("")]
		public string ConnectionTemplate
		{
			get
			{
				return (string)(base.ViewState["ConnectionTemplate"] ?? "");
			}
			set
			{
				base.ViewState["ConnectionTemplate"] = value;
			}
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x0004787D File Offset: 0x00045A7D
		// (set) Token: 0x060014B5 RID: 5301 RVA: 0x0004789E File Offset: 0x00045A9E
		[DefaultValue(true)]
		public bool Drag
		{
			get
			{
				return (bool)(base.ViewState["Drag"] ?? true);
			}
			set
			{
				base.ViewState["Drag"] = value;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x000478B6 File Offset: 0x00045AB6
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Drag DragSettings
		{
			get
			{
				if (this._drag == null)
				{
					this._drag = new Drag();
				}
				return this._drag;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x000478D1 File Offset: 0x00045AD1
		// (set) Token: 0x060014B8 RID: 5304 RVA: 0x000478F2 File Offset: 0x00045AF2
		[DefaultValue(true)]
		public bool Remove
		{
			get
			{
				return (bool)(base.ViewState["Remove"] ?? true);
			}
			set
			{
				base.ViewState["Remove"] = value;
			}
		}

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x060014B9 RID: 5305 RVA: 0x0004790A File Offset: 0x00045B0A
		// (set) Token: 0x060014BA RID: 5306 RVA: 0x0004792B File Offset: 0x00045B2B
		[DefaultValue(true)]
		public bool Resize
		{
			get
			{
				return (bool)(base.ViewState["Resize"] ?? true);
			}
			set
			{
				base.ViewState["Resize"] = value;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x060014BB RID: 5307 RVA: 0x00047943 File Offset: 0x00045B43
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Resize ResizeSettings
		{
			get
			{
				if (this._resize == null)
				{
					this._resize = new Resize();
				}
				return this._resize;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x060014BC RID: 5308 RVA: 0x0004795E File Offset: 0x00045B5E
		// (set) Token: 0x060014BD RID: 5309 RVA: 0x0004797F File Offset: 0x00045B7F
		[DefaultValue(true)]
		public bool Rotate
		{
			get
			{
				return (bool)(base.ViewState["Rotate"] ?? true);
			}
			set
			{
				base.ViewState["Rotate"] = value;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x00047997 File Offset: 0x00045B97
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Rotate RotateSettings
		{
			get
			{
				if (this._rotate == null)
				{
					this._rotate = new Rotate();
				}
				return this._rotate;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x000479B2 File Offset: 0x00045BB2
		// (set) Token: 0x060014C0 RID: 5312 RVA: 0x000479D2 File Offset: 0x00045BD2
		[DefaultValue("")]
		public string ShapeTemplate
		{
			get
			{
				return (string)(base.ViewState["ShapeTemplate"] ?? "");
			}
			set
			{
				base.ViewState["ShapeTemplate"] = value;
			}
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x060014C1 RID: 5313 RVA: 0x000479E5 File Offset: 0x00045BE5
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public DiagramEditableToolsCollection ToolsCollection
		{
			get
			{
				if (this._tools == null)
				{
					this._tools = new DiagramEditableToolsCollection();
				}
				return this._tools;
			}
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x00047A00 File Offset: 0x00045C00
		internal override void SetDirty()
		{
			base.SetDirty();
			this.DragSettings.SetDirty();
			this.ResizeSettings.SetDirty();
			this.RotateSettings.SetDirty();
			this.ToolsCollection.SetDirty();
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x00047A34 File Offset: 0x00045C34
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.DragSettings).LoadViewState(array[num++]);
			((IStateManager)this.ResizeSettings).LoadViewState(array[num++]);
			((IStateManager)this.RotateSettings).LoadViewState(array[num++]);
			((IStateManager)this.ToolsCollection).LoadViewState(array[num++]);
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x00047AA0 File Offset: 0x00045CA0
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.DragSettings).SaveViewState(),
				((IStateManager)this.ResizeSettings).SaveViewState(),
				((IStateManager)this.RotateSettings).SaveViewState(),
				((IStateManager)this.ToolsCollection).SaveViewState()
			};
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x00047AF8 File Offset: 0x00045CF8
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.DragSettings).TrackViewState();
			((IStateManager)this.ResizeSettings).TrackViewState();
			((IStateManager)this.RotateSettings).TrackViewState();
			((IStateManager)this.ToolsCollection).TrackViewState();
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x00047B2C File Offset: 0x00045D2C
		public bool IsDefault
		{
			get
			{
				return this.ConnectionTemplate == "" && this.Drag && this.DragSettings.IsDefault && this.Remove && this.Resize && this.ResizeSettings.IsDefault && this.Rotate && this.RotateSettings.IsDefault && this.ShapeTemplate == "" && this.ToolsCollection.ItemsList.Count == 0;
			}
		}

		// Token: 0x040005AA RID: 1450
		private Drag _drag;

		// Token: 0x040005AB RID: 1451
		private Resize _resize;

		// Token: 0x040005AC RID: 1452
		private Rotate _rotate;

		// Token: 0x040005AD RID: 1453
		private DiagramEditableToolsCollection _tools;
	}
}

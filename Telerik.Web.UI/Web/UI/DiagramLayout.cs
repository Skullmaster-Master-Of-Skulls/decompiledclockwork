using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.Diagram;

namespace Telerik.Web.UI
{
	// Token: 0x0200023F RID: 575
	public class DiagramLayout : StateManager, IDefaultCheck
	{
		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x060014F0 RID: 5360 RVA: 0x0004829A File Offset: 0x0004649A
		// (set) Token: 0x060014F1 RID: 5361 RVA: 0x000482C3 File Offset: 0x000464C3
		[DefaultValue(360.0)]
		public double EndRadialAngle
		{
			get
			{
				return (double)(base.ViewState["EndRadialAngle"] ?? 360.0);
			}
			set
			{
				base.ViewState["EndRadialAngle"] = value;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x060014F2 RID: 5362 RVA: 0x000482DB File Offset: 0x000464DB
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public DiagramGrid GridSettings
		{
			get
			{
				if (this._grid == null)
				{
					this._grid = new DiagramGrid();
				}
				return this._grid;
			}
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x000482F6 File Offset: 0x000464F6
		// (set) Token: 0x060014F4 RID: 5364 RVA: 0x0004831F File Offset: 0x0004651F
		[DefaultValue(90.0)]
		public double HorizontalSeparation
		{
			get
			{
				return (double)(base.ViewState["HorizontalSeparation"] ?? 90.0);
			}
			set
			{
				base.ViewState["HorizontalSeparation"] = value;
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x00048337 File Offset: 0x00046537
		// (set) Token: 0x060014F6 RID: 5366 RVA: 0x00048360 File Offset: 0x00046560
		[DefaultValue(300.0)]
		public double Iterations
		{
			get
			{
				return (double)(base.ViewState["Iterations"] ?? 300.0);
			}
			set
			{
				base.ViewState["Iterations"] = value;
			}
		}

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x00048378 File Offset: 0x00046578
		// (set) Token: 0x060014F8 RID: 5368 RVA: 0x000483A1 File Offset: 0x000465A1
		[DefaultValue(50.0)]
		public double LayerSeparation
		{
			get
			{
				return (double)(base.ViewState["LayerSeparation"] ?? 50.0);
			}
			set
			{
				base.ViewState["LayerSeparation"] = value;
			}
		}

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x000483B9 File Offset: 0x000465B9
		// (set) Token: 0x060014FA RID: 5370 RVA: 0x000483E2 File Offset: 0x000465E2
		[DefaultValue(50.0)]
		public double NodeDistance
		{
			get
			{
				return (double)(base.ViewState["NodeDistance"] ?? 50.0);
			}
			set
			{
				base.ViewState["NodeDistance"] = value;
			}
		}

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x060014FB RID: 5371 RVA: 0x000483FA File Offset: 0x000465FA
		// (set) Token: 0x060014FC RID: 5372 RVA: 0x00048423 File Offset: 0x00046623
		[DefaultValue(200.0)]
		public double RadialFirstLevelSeparation
		{
			get
			{
				return (double)(base.ViewState["RadialFirstLevelSeparation"] ?? 200.0);
			}
			set
			{
				base.ViewState["RadialFirstLevelSeparation"] = value;
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x0004843B File Offset: 0x0004663B
		// (set) Token: 0x060014FE RID: 5374 RVA: 0x00048464 File Offset: 0x00046664
		[DefaultValue(150.0)]
		public double RadialSeparation
		{
			get
			{
				return (double)(base.ViewState["RadialSeparation"] ?? 150.0);
			}
			set
			{
				base.ViewState["RadialSeparation"] = value;
			}
		}

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x0004847C File Offset: 0x0004667C
		// (set) Token: 0x06001500 RID: 5376 RVA: 0x000484A5 File Offset: 0x000466A5
		[DefaultValue(0.0)]
		public double StartRadialAngle
		{
			get
			{
				return (double)(base.ViewState["StartRadialAngle"] ?? 0.0);
			}
			set
			{
				base.ViewState["StartRadialAngle"] = value;
			}
		}

		// Token: 0x17000719 RID: 1817
		// (get) Token: 0x06001501 RID: 5377 RVA: 0x000484BD File Offset: 0x000466BD
		// (set) Token: 0x06001502 RID: 5378 RVA: 0x000484DE File Offset: 0x000466DE
		[DefaultValue(LayoutSubtype.Down)]
		public LayoutSubtype Subtype
		{
			get
			{
				return (LayoutSubtype)(base.ViewState["Subtype"] ?? LayoutSubtype.Down);
			}
			set
			{
				base.ViewState["Subtype"] = value;
			}
		}

		// Token: 0x1700071A RID: 1818
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x000484F6 File Offset: 0x000466F6
		// (set) Token: 0x06001504 RID: 5380 RVA: 0x0004851F File Offset: 0x0004671F
		[DefaultValue(0.0)]
		public double TipOverTreeStartLevel
		{
			get
			{
				return (double)(base.ViewState["TipOverTreeStartLevel"] ?? 0.0);
			}
			set
			{
				base.ViewState["TipOverTreeStartLevel"] = value;
			}
		}

		// Token: 0x1700071B RID: 1819
		// (get) Token: 0x06001505 RID: 5381 RVA: 0x00048537 File Offset: 0x00046737
		// (set) Token: 0x06001506 RID: 5382 RVA: 0x00048558 File Offset: 0x00046758
		[DefaultValue(LayoutType.Tree)]
		public LayoutType Type
		{
			get
			{
				return (LayoutType)(base.ViewState["Type"] ?? LayoutType.Tree);
			}
			set
			{
				base.ViewState["Type"] = value;
			}
		}

		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001507 RID: 5383 RVA: 0x00048570 File Offset: 0x00046770
		// (set) Token: 0x06001508 RID: 5384 RVA: 0x00048599 File Offset: 0x00046799
		[DefaultValue(15.0)]
		public double UnderneathHorizontalOffset
		{
			get
			{
				return (double)(base.ViewState["UnderneathHorizontalOffset"] ?? 15.0);
			}
			set
			{
				base.ViewState["UnderneathHorizontalOffset"] = value;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x000485B1 File Offset: 0x000467B1
		// (set) Token: 0x0600150A RID: 5386 RVA: 0x000485DA File Offset: 0x000467DA
		[DefaultValue(15.0)]
		public double UnderneathVerticalSeparation
		{
			get
			{
				return (double)(base.ViewState["UnderneathVerticalSeparation"] ?? 15.0);
			}
			set
			{
				base.ViewState["UnderneathVerticalSeparation"] = value;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x000485F2 File Offset: 0x000467F2
		// (set) Token: 0x0600150C RID: 5388 RVA: 0x0004861B File Offset: 0x0004681B
		[DefaultValue(15.0)]
		public double UnderneathVerticalTopOffset
		{
			get
			{
				return (double)(base.ViewState["UnderneathVerticalTopOffset"] ?? 15.0);
			}
			set
			{
				base.ViewState["UnderneathVerticalTopOffset"] = value;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x0600150D RID: 5389 RVA: 0x00048633 File Offset: 0x00046833
		// (set) Token: 0x0600150E RID: 5390 RVA: 0x0004865C File Offset: 0x0004685C
		[DefaultValue(50.0)]
		public double VerticalSeparation
		{
			get
			{
				return (double)(base.ViewState["VerticalSeparation"] ?? 50.0);
			}
			set
			{
				base.ViewState["VerticalSeparation"] = value;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x00048674 File Offset: 0x00046874
		// (set) Token: 0x06001510 RID: 5392 RVA: 0x00048695 File Offset: 0x00046895
		[DefaultValue(false)]
		public bool Enabled
		{
			get
			{
				return (bool)(base.ViewState["Enabled"] ?? false);
			}
			set
			{
				base.ViewState["Enabled"] = value;
			}
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x000486AD File Offset: 0x000468AD
		internal override void SetDirty()
		{
			base.SetDirty();
			this.GridSettings.SetDirty();
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x000486C0 File Offset: 0x000468C0
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.GridSettings).LoadViewState(array[num++]);
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x000486F8 File Offset: 0x000468F8
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.GridSettings).SaveViewState()
			};
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x00048726 File Offset: 0x00046926
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.GridSettings).TrackViewState();
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001515 RID: 5397 RVA: 0x0004873C File Offset: 0x0004693C
		public bool IsDefault
		{
			get
			{
				return this.EndRadialAngle == 360.0 && this.GridSettings.IsDefault && this.HorizontalSeparation == 90.0 && this.Iterations == 300.0 && this.LayerSeparation == 50.0 && this.NodeDistance == 50.0 && this.RadialFirstLevelSeparation == 200.0 && this.RadialSeparation == 150.0 && this.StartRadialAngle == 0.0 && this.Subtype == LayoutSubtype.Down && this.TipOverTreeStartLevel == 0.0 && this.Type == LayoutType.Tree && this.UnderneathHorizontalOffset == 15.0 && this.UnderneathVerticalSeparation == 15.0 && this.UnderneathVerticalTopOffset == 15.0 && this.VerticalSeparation == 50.0 && !this.Enabled;
			}
		}

		// Token: 0x040005AE RID: 1454
		private DiagramGrid _grid;
	}
}

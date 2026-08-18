using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000254 RID: 596
	public class Gradient : StateManager, IDefaultCheck
	{
		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x060015B0 RID: 5552 RVA: 0x0004A18A File Offset: 0x0004838A
		// (set) Token: 0x060015B1 RID: 5553 RVA: 0x0004A1A6 File Offset: 0x000483A6
		[DefaultValue(null)]
		[TypeConverter(typeof(DoubleArrayConverter))]
		public object[] Center
		{
			get
			{
				return (object[])(base.ViewState["Center"] ?? null);
			}
			set
			{
				base.ViewState["Center"] = value;
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x060015B2 RID: 5554 RVA: 0x0004A1B9 File Offset: 0x000483B9
		// (set) Token: 0x060015B3 RID: 5555 RVA: 0x0004A1E2 File Offset: 0x000483E2
		[DefaultValue(1.0)]
		public double Radius
		{
			get
			{
				return (double)(base.ViewState["Radius"] ?? 1.0);
			}
			set
			{
				base.ViewState["Radius"] = value;
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x060015B4 RID: 5556 RVA: 0x0004A1FA File Offset: 0x000483FA
		// (set) Token: 0x060015B5 RID: 5557 RVA: 0x0004A216 File Offset: 0x00048416
		[TypeConverter(typeof(DoubleArrayConverter))]
		[DefaultValue(null)]
		public object[] Start
		{
			get
			{
				return (object[])(base.ViewState["Start"] ?? null);
			}
			set
			{
				base.ViewState["Start"] = value;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x060015B6 RID: 5558 RVA: 0x0004A229 File Offset: 0x00048429
		// (set) Token: 0x060015B7 RID: 5559 RVA: 0x0004A245 File Offset: 0x00048445
		[TypeConverter(typeof(DoubleArrayConverter))]
		[DefaultValue(null)]
		public object[] End
		{
			get
			{
				return (object[])(base.ViewState["End"] ?? null);
			}
			set
			{
				base.ViewState["End"] = value;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x060015B8 RID: 5560 RVA: 0x0004A258 File Offset: 0x00048458
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public DiagramGradientStopsCollection StopsCollection
		{
			get
			{
				if (this._stops == null)
				{
					this._stops = new DiagramGradientStopsCollection();
				}
				return this._stops;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x060015B9 RID: 5561 RVA: 0x0004A273 File Offset: 0x00048473
		// (set) Token: 0x060015BA RID: 5562 RVA: 0x0004A294 File Offset: 0x00048494
		[DefaultValue(GradientType.Linear)]
		public GradientType Type
		{
			get
			{
				return (GradientType)(base.ViewState["Type"] ?? GradientType.Linear);
			}
			set
			{
				base.ViewState["Type"] = value;
			}
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x0004A2AC File Offset: 0x000484AC
		internal override void SetDirty()
		{
			base.SetDirty();
			this.StopsCollection.SetDirty();
		}

		// Token: 0x060015BC RID: 5564 RVA: 0x0004A2C0 File Offset: 0x000484C0
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.StopsCollection).LoadViewState(array[num++]);
		}

		// Token: 0x060015BD RID: 5565 RVA: 0x0004A2F8 File Offset: 0x000484F8
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.StopsCollection).SaveViewState()
			};
		}

		// Token: 0x060015BE RID: 5566 RVA: 0x0004A326 File Offset: 0x00048526
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.StopsCollection).TrackViewState();
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x060015BF RID: 5567 RVA: 0x0004A33C File Offset: 0x0004853C
		public bool IsDefault
		{
			get
			{
				return this.Center == null && this.Radius == 1.0 && this.Start == null && this.End == null && this.StopsCollection.ItemsList.Count == 0 && this.Type == GradientType.Linear;
			}
		}

		// Token: 0x040005BC RID: 1468
		private DiagramGradientStopsCollection _stops;
	}
}

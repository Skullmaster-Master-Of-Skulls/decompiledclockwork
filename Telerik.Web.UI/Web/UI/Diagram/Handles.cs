using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000257 RID: 599
	public class Handles : StateManager, IDefaultCheck
	{
		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x060015C4 RID: 5572 RVA: 0x0004A486 File Offset: 0x00048686
		// (set) Token: 0x060015C5 RID: 5573 RVA: 0x0004A4A6 File Offset: 0x000486A6
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

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x060015C6 RID: 5574 RVA: 0x0004A4B9 File Offset: 0x000486B9
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Fill FillSettings
		{
			get
			{
				if (this._fill == null)
				{
					this._fill = new Fill();
				}
				return this._fill;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x060015C7 RID: 5575 RVA: 0x0004A4D4 File Offset: 0x000486D4
		// (set) Token: 0x060015C8 RID: 5576 RVA: 0x0004A4FD File Offset: 0x000486FD
		[DefaultValue(0.0)]
		public double Height
		{
			get
			{
				return (double)(base.ViewState["Height"] ?? 0.0);
			}
			set
			{
				base.ViewState["Height"] = value;
			}
		}

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x060015C9 RID: 5577 RVA: 0x0004A515 File Offset: 0x00048715
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Hover HoverSettings
		{
			get
			{
				if (this._hover == null)
				{
					this._hover = new Hover();
				}
				return this._hover;
			}
		}

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x060015CA RID: 5578 RVA: 0x0004A530 File Offset: 0x00048730
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Stroke StrokeSettings
		{
			get
			{
				if (this._stroke == null)
				{
					this._stroke = new Stroke();
				}
				return this._stroke;
			}
		}

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x060015CB RID: 5579 RVA: 0x0004A54B File Offset: 0x0004874B
		// (set) Token: 0x060015CC RID: 5580 RVA: 0x0004A574 File Offset: 0x00048774
		[DefaultValue(0.0)]
		public double Width
		{
			get
			{
				return (double)(base.ViewState["Width"] ?? 0.0);
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x0004A58C File Offset: 0x0004878C
		internal override void SetDirty()
		{
			base.SetDirty();
			this.FillSettings.SetDirty();
			this.HoverSettings.SetDirty();
			this.StrokeSettings.SetDirty();
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x0004A5B8 File Offset: 0x000487B8
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.FillSettings).LoadViewState(array[num++]);
			((IStateManager)this.HoverSettings).LoadViewState(array[num++]);
			((IStateManager)this.StrokeSettings).LoadViewState(array[num++]);
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x0004A614 File Offset: 0x00048814
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.FillSettings).SaveViewState(),
				((IStateManager)this.HoverSettings).SaveViewState(),
				((IStateManager)this.StrokeSettings).SaveViewState()
			};
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x0004A65E File Offset: 0x0004885E
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.FillSettings).TrackViewState();
			((IStateManager)this.HoverSettings).TrackViewState();
			((IStateManager)this.StrokeSettings).TrackViewState();
		}

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x060015D1 RID: 5585 RVA: 0x0004A688 File Offset: 0x00048888
		public bool IsDefault
		{
			get
			{
				return this.Fill == "" && this.FillSettings.IsDefault && this.Height == 0.0 && this.HoverSettings.IsDefault && this.StrokeSettings.IsDefault && this.Width == 0.0;
			}
		}

		// Token: 0x040005C0 RID: 1472
		private Fill _fill;

		// Token: 0x040005C1 RID: 1473
		private Hover _hover;

		// Token: 0x040005C2 RID: 1474
		private Stroke _stroke;
	}
}

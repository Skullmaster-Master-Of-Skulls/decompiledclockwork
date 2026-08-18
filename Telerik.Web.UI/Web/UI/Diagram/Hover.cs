using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x0200025A RID: 602
	public class Hover : StateManager, IDefaultCheck
	{
		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x060015DC RID: 5596 RVA: 0x0004A8C6 File Offset: 0x00048AC6
		// (set) Token: 0x060015DD RID: 5597 RVA: 0x0004A8E6 File Offset: 0x00048AE6
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

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x060015DE RID: 5598 RVA: 0x0004A8F9 File Offset: 0x00048AF9
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

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x060015DF RID: 5599 RVA: 0x0004A914 File Offset: 0x00048B14
		// (set) Token: 0x060015E0 RID: 5600 RVA: 0x0004A934 File Offset: 0x00048B34
		[DefaultValue("")]
		public string Stroke
		{
			get
			{
				return (string)(base.ViewState["Stroke"] ?? "");
			}
			set
			{
				base.ViewState["Stroke"] = value;
			}
		}

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x060015E1 RID: 5601 RVA: 0x0004A947 File Offset: 0x00048B47
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

		// Token: 0x060015E2 RID: 5602 RVA: 0x0004A962 File Offset: 0x00048B62
		internal override void SetDirty()
		{
			base.SetDirty();
			this.FillSettings.SetDirty();
			this.StrokeSettings.SetDirty();
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x0004A980 File Offset: 0x00048B80
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.FillSettings).LoadViewState(array[num++]);
			((IStateManager)this.StrokeSettings).LoadViewState(array[num++]);
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x0004A9C8 File Offset: 0x00048BC8
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.FillSettings).SaveViewState(),
				((IStateManager)this.StrokeSettings).SaveViewState()
			};
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x0004AA04 File Offset: 0x00048C04
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.FillSettings).TrackViewState();
			((IStateManager)this.StrokeSettings).TrackViewState();
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x060015E6 RID: 5606 RVA: 0x0004AA22 File Offset: 0x00048C22
		public bool IsDefault
		{
			get
			{
				return this.Fill == "" && this.FillSettings.IsDefault && this.Stroke == "" && this.StrokeSettings.IsDefault;
			}
		}

		// Token: 0x040005C4 RID: 1476
		private Fill _fill;

		// Token: 0x040005C5 RID: 1477
		private Stroke _stroke;
	}
}

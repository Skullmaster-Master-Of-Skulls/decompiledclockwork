using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Diagram
{
	// Token: 0x02000221 RID: 545
	public class ConnectorDefaults : StateManager, IDefaultCheck
	{
		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x00045A9A File Offset: 0x00043C9A
		// (set) Token: 0x060013E0 RID: 5088 RVA: 0x00045AC3 File Offset: 0x00043CC3
		[DefaultValue(8.0)]
		public double Width
		{
			get
			{
				return (double)(base.ViewState["Width"] ?? 8.0);
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x060013E1 RID: 5089 RVA: 0x00045ADB File Offset: 0x00043CDB
		// (set) Token: 0x060013E2 RID: 5090 RVA: 0x00045B04 File Offset: 0x00043D04
		[DefaultValue(8.0)]
		public double Height
		{
			get
			{
				return (double)(base.ViewState["Height"] ?? 8.0);
			}
			set
			{
				base.ViewState["Height"] = value;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x060013E3 RID: 5091 RVA: 0x00045B1C File Offset: 0x00043D1C
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

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x060013E4 RID: 5092 RVA: 0x00045B37 File Offset: 0x00043D37
		// (set) Token: 0x060013E5 RID: 5093 RVA: 0x00045B57 File Offset: 0x00043D57
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

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x060013E6 RID: 5094 RVA: 0x00045B6A File Offset: 0x00043D6A
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

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x060013E7 RID: 5095 RVA: 0x00045B85 File Offset: 0x00043D85
		// (set) Token: 0x060013E8 RID: 5096 RVA: 0x00045BA5 File Offset: 0x00043DA5
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

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x060013E9 RID: 5097 RVA: 0x00045BB8 File Offset: 0x00043DB8
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

		// Token: 0x060013EA RID: 5098 RVA: 0x00045BD3 File Offset: 0x00043DD3
		internal override void SetDirty()
		{
			base.SetDirty();
			this.FillSettings.SetDirty();
			this.HoverSettings.SetDirty();
			this.StrokeSettings.SetDirty();
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x00045BFC File Offset: 0x00043DFC
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.FillSettings).LoadViewState(array[num++]);
			((IStateManager)this.HoverSettings).LoadViewState(array[num++]);
			((IStateManager)this.StrokeSettings).LoadViewState(array[num++]);
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x00045C58 File Offset: 0x00043E58
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

		// Token: 0x060013ED RID: 5101 RVA: 0x00045CA2 File Offset: 0x00043EA2
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.FillSettings).TrackViewState();
			((IStateManager)this.HoverSettings).TrackViewState();
			((IStateManager)this.StrokeSettings).TrackViewState();
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x00045CCC File Offset: 0x00043ECC
		public bool IsDefault
		{
			get
			{
				return this.Width == 8.0 && this.Height == 8.0 && this.HoverSettings.IsDefault && this.Fill == "" && this.FillSettings.IsDefault && this.Stroke == "" && this.StrokeSettings.IsDefault;
			}
		}

		// Token: 0x0400058F RID: 1423
		private Hover _hover;

		// Token: 0x04000590 RID: 1424
		private Fill _fill;

		// Token: 0x04000591 RID: 1425
		private Stroke _stroke;
	}
}

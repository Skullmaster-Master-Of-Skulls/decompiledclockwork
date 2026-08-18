using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.Diagram;

namespace Telerik.Web.UI
{
	// Token: 0x02000241 RID: 577
	public class DiagramPdf : StateManager, IDefaultCheck
	{
		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x00048B12 File Offset: 0x00046D12
		// (set) Token: 0x0600151B RID: 5403 RVA: 0x00048B2E File Offset: 0x00046D2E
		[DefaultValue(null)]
		public string Author
		{
			get
			{
				return (string)(base.ViewState["Author"] ?? null);
			}
			set
			{
				base.ViewState["Author"] = value;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x0600151C RID: 5404 RVA: 0x00048B41 File Offset: 0x00046D41
		// (set) Token: 0x0600151D RID: 5405 RVA: 0x00048B61 File Offset: 0x00046D61
		[DefaultValue("Kendo UI PDF Generator")]
		public string Creator
		{
			get
			{
				return (string)(base.ViewState["Creator"] ?? "Kendo UI PDF Generator");
			}
			set
			{
				base.ViewState["Creator"] = value;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x0600151E RID: 5406 RVA: 0x00048B74 File Offset: 0x00046D74
		// (set) Token: 0x0600151F RID: 5407 RVA: 0x00048B90 File Offset: 0x00046D90
		[DefaultValue(null)]
		public DateTime? Date
		{
			get
			{
				return (DateTime?)(base.ViewState["Date"] ?? null);
			}
			set
			{
				base.ViewState["Date"] = value;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x06001520 RID: 5408 RVA: 0x00048BA8 File Offset: 0x00046DA8
		// (set) Token: 0x06001521 RID: 5409 RVA: 0x00048BC8 File Offset: 0x00046DC8
		[DefaultValue("Export.pdf")]
		public string FileName
		{
			get
			{
				return (string)(base.ViewState["FileName"] ?? "Export.pdf");
			}
			set
			{
				base.ViewState["FileName"] = value;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x06001522 RID: 5410 RVA: 0x00048BDB File Offset: 0x00046DDB
		// (set) Token: 0x06001523 RID: 5411 RVA: 0x00048BFC File Offset: 0x00046DFC
		[DefaultValue(false)]
		public bool ForceProxy
		{
			get
			{
				return (bool)(base.ViewState["ForceProxy"] ?? false);
			}
			set
			{
				base.ViewState["ForceProxy"] = value;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x06001524 RID: 5412 RVA: 0x00048C14 File Offset: 0x00046E14
		// (set) Token: 0x06001525 RID: 5413 RVA: 0x00048C30 File Offset: 0x00046E30
		[DefaultValue(null)]
		public string Keywords
		{
			get
			{
				return (string)(base.ViewState["Keywords"] ?? null);
			}
			set
			{
				base.ViewState["Keywords"] = value;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x00048C43 File Offset: 0x00046E43
		// (set) Token: 0x06001527 RID: 5415 RVA: 0x00048C64 File Offset: 0x00046E64
		[DefaultValue(false)]
		public bool Landscape
		{
			get
			{
				return (bool)(base.ViewState["Landscape"] ?? false);
			}
			set
			{
				base.ViewState["Landscape"] = value;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06001528 RID: 5416 RVA: 0x00048C7C File Offset: 0x00046E7C
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Margin MarginSettings
		{
			get
			{
				if (this._margin == null)
				{
					this._margin = new Margin();
				}
				return this._margin;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x00048C97 File Offset: 0x00046E97
		// (set) Token: 0x0600152A RID: 5418 RVA: 0x00048CB7 File Offset: 0x00046EB7
		[DefaultValue("auto")]
		public string PaperSize
		{
			get
			{
				return (string)(base.ViewState["PaperSize"] ?? "auto");
			}
			set
			{
				base.ViewState["PaperSize"] = value;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x00048CCA File Offset: 0x00046ECA
		// (set) Token: 0x0600152C RID: 5420 RVA: 0x00048CE6 File Offset: 0x00046EE6
		[DefaultValue(null)]
		public string ProxyURL
		{
			get
			{
				return (string)(base.ViewState["ProxyURL"] ?? null);
			}
			set
			{
				base.ViewState["ProxyURL"] = value;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x0600152D RID: 5421 RVA: 0x00048CF9 File Offset: 0x00046EF9
		// (set) Token: 0x0600152E RID: 5422 RVA: 0x00048D19 File Offset: 0x00046F19
		[DefaultValue("_self")]
		public string ProxyTarget
		{
			get
			{
				return (string)(base.ViewState["ProxyTarget"] ?? "_self");
			}
			set
			{
				base.ViewState["ProxyTarget"] = value;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x00048D2C File Offset: 0x00046F2C
		// (set) Token: 0x06001530 RID: 5424 RVA: 0x00048D48 File Offset: 0x00046F48
		[DefaultValue(null)]
		public string Subject
		{
			get
			{
				return (string)(base.ViewState["Subject"] ?? null);
			}
			set
			{
				base.ViewState["Subject"] = value;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x00048D5B File Offset: 0x00046F5B
		// (set) Token: 0x06001532 RID: 5426 RVA: 0x00048D77 File Offset: 0x00046F77
		[DefaultValue(null)]
		public string Title
		{
			get
			{
				return (string)(base.ViewState["Title"] ?? null);
			}
			set
			{
				base.ViewState["Title"] = value;
			}
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x00048D8A File Offset: 0x00046F8A
		internal override void SetDirty()
		{
			base.SetDirty();
			this.MarginSettings.SetDirty();
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x00048DA0 File Offset: 0x00046FA0
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			int num = 0;
			base.LoadViewState(array[num++]);
			((IStateManager)this.MarginSettings).LoadViewState(array[num++]);
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x00048DD8 File Offset: 0x00046FD8
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.MarginSettings).SaveViewState()
			};
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x00048E06 File Offset: 0x00047006
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.MarginSettings).TrackViewState();
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x00048E1C File Offset: 0x0004701C
		public bool IsDefault
		{
			get
			{
				return this.Author == null && this.Creator == "Kendo UI PDF Generator" && this.Date == null && this.FileName == "Export.pdf" && !this.ForceProxy && this.Keywords == null && !this.Landscape && this.MarginSettings.IsDefault && this.PaperSize == "auto" && this.ProxyURL == null && this.ProxyTarget == "_self" && this.Subject == null && this.Title == null;
			}
		}

		// Token: 0x040005AF RID: 1455
		private Margin _margin;
	}
}

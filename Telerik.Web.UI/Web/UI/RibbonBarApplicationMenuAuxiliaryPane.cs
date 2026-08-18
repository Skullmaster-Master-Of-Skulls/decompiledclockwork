using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020007BD RID: 1981
	public class RibbonBarApplicationMenuAuxiliaryPane : StateManager, IDisposable
	{
		// Token: 0x1700163E RID: 5694
		// (get) Token: 0x06004517 RID: 17687 RVA: 0x000DABDB File Offset: 0x000D8DDB
		// (set) Token: 0x06004518 RID: 17688 RVA: 0x000DABFB File Offset: 0x000D8DFB
		[DefaultValue("")]
		public string Header
		{
			get
			{
				return (string)(base.ViewState["Header"] ?? string.Empty);
			}
			set
			{
				base.ViewState["Header"] = value;
			}
		}

		// Token: 0x1700163F RID: 5695
		// (get) Token: 0x06004519 RID: 17689 RVA: 0x000DAC0E File Offset: 0x000D8E0E
		// (set) Token: 0x0600451A RID: 17690 RVA: 0x000DAC16 File Offset: 0x000D8E16
		[TemplateContainer(typeof(RibbonBarApplicationMenu))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		[Bindable(false)]
		public ITemplate ContentTemplate { get; set; }

		// Token: 0x17001640 RID: 5696
		// (get) Token: 0x0600451B RID: 17691 RVA: 0x000DAC1F File Offset: 0x000D8E1F
		public WebControl ContentWrapper
		{
			get
			{
				if (this._contentWrapper == null)
				{
					this._contentWrapper = new WebControl(HtmlTextWriterTag.Div);
				}
				return this._contentWrapper;
			}
		}

		// Token: 0x0600451C RID: 17692 RVA: 0x000DAC3C File Offset: 0x000D8E3C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600451D RID: 17693 RVA: 0x000DAC4B File Offset: 0x000D8E4B
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this._disposed)
			{
				if (this._contentWrapper != null)
				{
					this._contentWrapper.Dispose();
					this._contentWrapper = null;
				}
				GC.SuppressFinalize(this);
				this._disposed = true;
			}
		}

		// Token: 0x040011FC RID: 4604
		private WebControl _contentWrapper;

		// Token: 0x040011FD RID: 4605
		private bool _disposed;
	}
}

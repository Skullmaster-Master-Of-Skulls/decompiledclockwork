using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020007BE RID: 1982
	public class RibbonBarApplicationMenuFooterPane : IDisposable
	{
		// Token: 0x17001641 RID: 5697
		// (get) Token: 0x0600451F RID: 17695 RVA: 0x000DAC87 File Offset: 0x000D8E87
		// (set) Token: 0x06004520 RID: 17696 RVA: 0x000DAC8F File Offset: 0x000D8E8F
		[Bindable(false)]
		[Browsable(false)]
		[TemplateContainer(typeof(RibbonBarApplicationMenu))]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public ITemplate ContentTemplate { get; set; }

		// Token: 0x17001642 RID: 5698
		// (get) Token: 0x06004521 RID: 17697 RVA: 0x000DAC98 File Offset: 0x000D8E98
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

		// Token: 0x06004522 RID: 17698 RVA: 0x000DACB5 File Offset: 0x000D8EB5
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004523 RID: 17699 RVA: 0x000DACC4 File Offset: 0x000D8EC4
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

		// Token: 0x040011FF RID: 4607
		private WebControl _contentWrapper;

		// Token: 0x04001200 RID: 4608
		private bool _disposed;
	}
}

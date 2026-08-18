using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200196A RID: 6506
	internal class PageableItemContainerWrapper : IRadPageableItemContainer
	{
		// Token: 0x0600FC16 RID: 64534 RVA: 0x0038CE98 File Offset: 0x0038B098
		public PageableItemContainerWrapper(IPageableItemContainer container)
		{
			this._container = container;
			this._container.TotalRowCountAvailable += this.Container_TotalRowCountAvailable;
		}

		// Token: 0x0600FC17 RID: 64535 RVA: 0x0038CEC0 File Offset: 0x0038B0C0
		protected void Container_TotalRowCountAvailable(object sender, PageEventArgs e)
		{
			RadDataPagerPageEventArgs e2 = new RadDataPagerPageEventArgs(e);
			this.Events(this, e2);
		}

		// Token: 0x140001DA RID: 474
		// (add) Token: 0x0600FC18 RID: 64536 RVA: 0x0038CEE1 File Offset: 0x0038B0E1
		// (remove) Token: 0x0600FC19 RID: 64537 RVA: 0x0038CEFA File Offset: 0x0038B0FA
		event EventHandler<RadDataPagerPageEventArgs> IRadPageableItemContainer.TotalRowCountAvailable
		{
			add
			{
				this.Events = (EventHandler<RadDataPagerPageEventArgs>)Delegate.Combine(this.Events, value);
			}
			remove
			{
				this.Events = (EventHandler<RadDataPagerPageEventArgs>)Delegate.Remove(this.Events, value);
			}
		}

		// Token: 0x0600FC1A RID: 64538 RVA: 0x0038CF13 File Offset: 0x0038B113
		void IRadPageableItemContainer.SetPageProperties(int startRowIndex, int maximumRows, bool databind)
		{
			this._container.SetPageProperties(startRowIndex, maximumRows, databind);
		}

		// Token: 0x17004C27 RID: 19495
		// (get) Token: 0x0600FC1B RID: 64539 RVA: 0x0038CF23 File Offset: 0x0038B123
		int IRadPageableItemContainer.MaximumRows
		{
			get
			{
				return this._container.MaximumRows;
			}
		}

		// Token: 0x17004C28 RID: 19496
		// (get) Token: 0x0600FC1C RID: 64540 RVA: 0x0038CF30 File Offset: 0x0038B130
		int IRadPageableItemContainer.StartRowIndex
		{
			get
			{
				return this._container.StartRowIndex;
			}
		}

		// Token: 0x040047B7 RID: 18359
		private IPageableItemContainer _container;

		// Token: 0x040047B8 RID: 18360
		private EventHandler<RadDataPagerPageEventArgs> Events;
	}
}

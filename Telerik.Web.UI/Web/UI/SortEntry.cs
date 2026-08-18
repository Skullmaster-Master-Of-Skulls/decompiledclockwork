using System;
using System.ComponentModel;
using Telerik.Web.UI.ODataSource.Filters;

namespace Telerik.Web.UI
{
	// Token: 0x02000BD3 RID: 3027
	public class SortEntry : EntryBase
	{
		// Token: 0x0600737F RID: 29567 RVA: 0x001B001A File Offset: 0x001AE21A
		public SortEntry()
		{
			this._order = ODataSourceOrder.Asc;
		}

		// Token: 0x1700259B RID: 9627
		// (get) Token: 0x06007380 RID: 29568 RVA: 0x001B0029 File Offset: 0x001AE229
		// (set) Token: 0x06007381 RID: 29569 RVA: 0x001B0031 File Offset: 0x001AE231
		[Description("Gets or sets the sorting direction.")]
		[Category("Behavior")]
		[DefaultValue("")]
		public ODataSourceOrder Order
		{
			get
			{
				return this._order;
			}
			set
			{
				this._order = value;
			}
		}

		// Token: 0x04001F63 RID: 8035
		private ODataSourceOrder _order;
	}
}

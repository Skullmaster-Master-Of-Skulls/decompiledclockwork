using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.Scheduler.Views
{
	// Token: 0x02000828 RID: 2088
	internal abstract class ViewBase : ISchedulerView
	{
		// Token: 0x1700193D RID: 6461
		// (get) Token: 0x06004D3A RID: 19770
		// (set) Token: 0x06004D3B RID: 19771
		public abstract ISchedulerModel Model { get; protected set; }

		// Token: 0x1700193E RID: 6462
		// (get) Token: 0x06004D3C RID: 19772 RVA: 0x000F2E9E File Offset: 0x000F109E
		// (set) Token: 0x06004D3D RID: 19773 RVA: 0x000F2EBF File Offset: 0x000F10BF
		public IList<ViewHeader> RowHeaders
		{
			get
			{
				if (this._rowHeaders == null)
				{
					this._rowHeaders = new List<ViewHeader>();
					this.InitializeRowHeaders();
				}
				return this._rowHeaders;
			}
			set
			{
				this._rowHeaders = value;
			}
		}

		// Token: 0x1700193F RID: 6463
		// (get) Token: 0x06004D3E RID: 19774 RVA: 0x000F2EC8 File Offset: 0x000F10C8
		// (set) Token: 0x06004D3F RID: 19775 RVA: 0x000F2EE9 File Offset: 0x000F10E9
		public IList<ViewHeader> ColumnHeaders
		{
			get
			{
				if (this._columnHeaders == null)
				{
					this._columnHeaders = new List<ViewHeader>();
					this.InitializeColumnHeaders();
				}
				return this._columnHeaders;
			}
			set
			{
				this._columnHeaders = value;
			}
		}

		// Token: 0x17001940 RID: 6464
		// (get) Token: 0x06004D40 RID: 19776 RVA: 0x000F2EF2 File Offset: 0x000F10F2
		public int ColumnHeadersDepth
		{
			get
			{
				return ViewBase.GetHeadersDepth(this.ColumnHeaders);
			}
		}

		// Token: 0x17001941 RID: 6465
		// (get) Token: 0x06004D41 RID: 19777 RVA: 0x000F2EFF File Offset: 0x000F10FF
		public int RowHeadersDepth
		{
			get
			{
				return ViewBase.GetHeadersDepth(this.RowHeaders);
			}
		}

		// Token: 0x17001942 RID: 6466
		// (get) Token: 0x06004D42 RID: 19778
		public abstract RadScheduler Owner { get; }

		// Token: 0x06004D43 RID: 19779
		protected abstract void InitializeColumnHeaders();

		// Token: 0x06004D44 RID: 19780
		protected abstract void InitializeRowHeaders();

		// Token: 0x06004D45 RID: 19781 RVA: 0x000F2F0C File Offset: 0x000F110C
		protected static int GetHeadersDepth(IEnumerable<ViewHeader> headerList)
		{
			int num = 0;
			foreach (ViewHeader viewHeader in headerList)
			{
				num = Math.Max(num, viewHeader.Depth);
			}
			return num;
		}

		// Token: 0x04001358 RID: 4952
		private IList<ViewHeader> _columnHeaders;

		// Token: 0x04001359 RID: 4953
		private IList<ViewHeader> _rowHeaders;
	}
}

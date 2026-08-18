using System;
using System.Collections;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.DataSouceView
{
	// Token: 0x02000C9D RID: 3229
	internal class EnumerableDataSourceView : IDataSourceView, IReadOnlyList<object>, IReadOnlyCollection<object>, IEnumerable<object>, IEnumerable
	{
		// Token: 0x0600795E RID: 31070 RVA: 0x001BE6DC File Offset: 0x001BC8DC
		public EnumerableDataSourceView(IEnumerable enumerableSource)
		{
			this.internalList = new List<object>();
			foreach (object item in enumerableSource)
			{
				this.internalList.Add(item);
			}
		}

		// Token: 0x17002723 RID: 10019
		// (get) Token: 0x0600795F RID: 31071 RVA: 0x001BE744 File Offset: 0x001BC944
		public int Count
		{
			get
			{
				return this.internalList.Count;
			}
		}

		// Token: 0x17002724 RID: 10020
		public object this[int index]
		{
			get
			{
				return this.internalList[index];
			}
		}

		// Token: 0x06007961 RID: 31073 RVA: 0x001BE75F File Offset: 0x001BC95F
		public IEnumerator<object> GetEnumerator()
		{
			return this.internalList.GetEnumerator();
		}

		// Token: 0x06007962 RID: 31074 RVA: 0x001BE76C File Offset: 0x001BC96C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.internalList.GetEnumerator();
		}

		// Token: 0x0400212B RID: 8491
		private IList<object> internalList;
	}
}

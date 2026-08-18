using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x02000584 RID: 1412
	public class ListViewCustomAggregateEventArgs : EventArgs
	{
		// Token: 0x060032F2 RID: 13042 RVA: 0x000A89A6 File Offset: 0x000A6BA6
		public ListViewCustomAggregateEventArgs(ListViewDataGroup dataGroup, IEnumerable dataItems, string dataField)
		{
			this._dataGroup = dataGroup;
			this._dataItems = dataItems;
			this._dataField = dataField;
		}

		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x060032F3 RID: 13043 RVA: 0x000A89D9 File Offset: 0x000A6BD9
		public ListViewDataGroup DataGroup
		{
			get
			{
				return this._dataGroup;
			}
		}

		// Token: 0x17001086 RID: 4230
		// (get) Token: 0x060032F4 RID: 13044 RVA: 0x000A89E1 File Offset: 0x000A6BE1
		public IEnumerable DataItems
		{
			get
			{
				return this._dataItems;
			}
		}

		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x060032F5 RID: 13045 RVA: 0x000A89E9 File Offset: 0x000A6BE9
		public IEnumerable DataField
		{
			get
			{
				return this._dataField;
			}
		}

		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x060032F6 RID: 13046 RVA: 0x000A89F1 File Offset: 0x000A6BF1
		// (set) Token: 0x060032F7 RID: 13047 RVA: 0x000A89F9 File Offset: 0x000A6BF9
		public object Result
		{
			get
			{
				return this._result;
			}
			set
			{
				this._result = value;
			}
		}

		// Token: 0x04000DF3 RID: 3571
		private object _result = string.Empty;

		// Token: 0x04000DF4 RID: 3572
		private ListViewDataGroup _dataGroup;

		// Token: 0x04000DF5 RID: 3573
		private IEnumerable _dataItems;

		// Token: 0x04000DF6 RID: 3574
		private string _dataField = string.Empty;
	}
}

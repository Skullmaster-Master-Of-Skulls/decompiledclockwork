using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020019A5 RID: 6565
	internal class ListViewEnumerableFromViewState : ListViewEnumerableBase
	{
		// Token: 0x0600FDF4 RID: 65012 RVA: 0x0039028E File Offset: 0x0038E48E
		public ListViewEnumerableFromViewState(ListViewControlStateManager viewState)
		{
			this._viewState = viewState;
		}

		// Token: 0x17004CB3 RID: 19635
		// (get) Token: 0x0600FDF5 RID: 65013 RVA: 0x0039029D File Offset: 0x0038E49D
		public override bool SupportsPaging
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600FDF6 RID: 65014 RVA: 0x003902A0 File Offset: 0x0038E4A0
		public override IEnumerable RawEnumerable()
		{
			return new ListViewEnumerableFromViewState.ListViewDummyDataSource(this.Count);
		}

		// Token: 0x0600FDF7 RID: 65015 RVA: 0x003902AD File Offset: 0x0038E4AD
		protected override void TransformEnumerable()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17004CB4 RID: 19636
		// (get) Token: 0x0600FDF8 RID: 65016 RVA: 0x003902B4 File Offset: 0x0038E4B4
		public override int DataSourceCount
		{
			get
			{
				object obj = this._viewState["_!DSIC"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
		}

		// Token: 0x17004CB5 RID: 19637
		// (get) Token: 0x0600FDF9 RID: 65017 RVA: 0x003902E0 File Offset: 0x0038E4E0
		public override int Count
		{
			get
			{
				object obj = this._viewState["_!ItemCount"];
				if (obj == null)
				{
					return 0;
				}
				return (int)obj;
			}
		}

		// Token: 0x04004818 RID: 18456
		private readonly ListViewControlStateManager _viewState;

		// Token: 0x020019A6 RID: 6566
		internal class ListViewDummyDataSource : ICollection, IEnumerable
		{
			// Token: 0x0600FDFA RID: 65018 RVA: 0x00390309 File Offset: 0x0038E509
			public ListViewDummyDataSource(int itemsCount)
			{
				this._itemsCount = itemsCount;
			}

			// Token: 0x0600FDFB RID: 65019 RVA: 0x003903B0 File Offset: 0x0038E5B0
			public IEnumerator GetEnumerator()
			{
				for (int i = 0; i < this._itemsCount; i++)
				{
					yield return null;
				}
				yield break;
			}

			// Token: 0x0600FDFC RID: 65020 RVA: 0x003903CC File Offset: 0x0038E5CC
			public void CopyTo(Array array, int index)
			{
				foreach (object value in this)
				{
					array.SetValue(value, index++);
				}
			}

			// Token: 0x17004CB6 RID: 19638
			// (get) Token: 0x0600FDFD RID: 65021 RVA: 0x00390424 File Offset: 0x0038E624
			public int Count
			{
				get
				{
					return this._itemsCount;
				}
			}

			// Token: 0x17004CB7 RID: 19639
			// (get) Token: 0x0600FDFE RID: 65022 RVA: 0x0039042C File Offset: 0x0038E62C
			public bool IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17004CB8 RID: 19640
			// (get) Token: 0x0600FDFF RID: 65023 RVA: 0x0039042F File Offset: 0x0038E62F
			public object SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x04004819 RID: 18457
			private readonly int _itemsCount;
		}
	}
}

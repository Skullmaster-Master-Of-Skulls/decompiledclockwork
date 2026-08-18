using System;
using System.Collections;
using System.ComponentModel;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000494 RID: 1172
	public sealed class PagedDataSource : ICollection, IEnumerable, ITypedList
	{
		// Token: 0x06003A3F RID: 14911 RVA: 0x000BD2F9 File Offset: 0x000BB4F9
		public PagedDataSource()
		{
			this.pageSize = 10;
			this.allowPaging = false;
			this.currentPageIndex = 0;
			this.allowCustomPaging = false;
			this.allowServerPaging = false;
			this.virtualCount = 0;
		}

		// Token: 0x170010F3 RID: 4339
		// (get) Token: 0x06003A40 RID: 14912 RVA: 0x000BD32C File Offset: 0x000BB52C
		// (set) Token: 0x06003A41 RID: 14913 RVA: 0x000BD334 File Offset: 0x000BB534
		public bool AllowCustomPaging
		{
			get
			{
				return this.allowCustomPaging;
			}
			set
			{
				this.allowCustomPaging = value;
			}
		}

		// Token: 0x170010F4 RID: 4340
		// (get) Token: 0x06003A42 RID: 14914 RVA: 0x000BD33D File Offset: 0x000BB53D
		// (set) Token: 0x06003A43 RID: 14915 RVA: 0x000BD345 File Offset: 0x000BB545
		public bool AllowPaging
		{
			get
			{
				return this.allowPaging;
			}
			set
			{
				this.allowPaging = value;
			}
		}

		// Token: 0x170010F5 RID: 4341
		// (get) Token: 0x06003A44 RID: 14916 RVA: 0x000BD34E File Offset: 0x000BB54E
		// (set) Token: 0x06003A45 RID: 14917 RVA: 0x000BD356 File Offset: 0x000BB556
		public bool AllowServerPaging
		{
			get
			{
				return this.allowServerPaging;
			}
			set
			{
				this.allowServerPaging = value;
			}
		}

		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x06003A46 RID: 14918 RVA: 0x000BD35F File Offset: 0x000BB55F
		public int Count
		{
			get
			{
				if (this.dataSource == null)
				{
					return 0;
				}
				if (!this.IsPagingEnabled)
				{
					return this.DataSourceCount;
				}
				if (this.IsCustomPagingEnabled || !this.IsLastPage)
				{
					return this.pageSize;
				}
				return this.DataSourceCount - this.FirstIndexInPage;
			}
		}

		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x06003A47 RID: 14919 RVA: 0x000BD39E File Offset: 0x000BB59E
		// (set) Token: 0x06003A48 RID: 14920 RVA: 0x000BD3A6 File Offset: 0x000BB5A6
		public int CurrentPageIndex
		{
			get
			{
				return this.currentPageIndex;
			}
			set
			{
				this.currentPageIndex = value;
			}
		}

		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x06003A49 RID: 14921 RVA: 0x000BD3AF File Offset: 0x000BB5AF
		// (set) Token: 0x06003A4A RID: 14922 RVA: 0x000BD3B7 File Offset: 0x000BB5B7
		public IEnumerable DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				this.dataSource = value;
			}
		}

		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x06003A4B RID: 14923 RVA: 0x000BD3C0 File Offset: 0x000BB5C0
		public int DataSourceCount
		{
			get
			{
				if (this.dataSource == null)
				{
					return 0;
				}
				if (this.IsCustomPagingEnabled || this.IsServerPagingEnabled)
				{
					return this.virtualCount;
				}
				if (this.dataSource is ICollection)
				{
					return ((ICollection)this.dataSource).Count;
				}
				throw new HttpException(SR.GetString("PagedDataSource_Cannot_Get_Count"));
			}
		}

		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x06003A4C RID: 14924 RVA: 0x000BD41B File Offset: 0x000BB61B
		public int FirstIndexInPage
		{
			get
			{
				if (this.dataSource == null || !this.IsPagingEnabled)
				{
					return 0;
				}
				if (this.IsCustomPagingEnabled || this.IsServerPagingEnabled)
				{
					return 0;
				}
				return this.currentPageIndex * this.pageSize;
			}
		}

		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x06003A4D RID: 14925 RVA: 0x000BD44E File Offset: 0x000BB64E
		public bool IsCustomPagingEnabled
		{
			get
			{
				return this.IsPagingEnabled && this.allowCustomPaging;
			}
		}

		// Token: 0x170010FC RID: 4348
		// (get) Token: 0x06003A4E RID: 14926 RVA: 0x000BD460 File Offset: 0x000BB660
		public bool IsFirstPage
		{
			get
			{
				return !this.IsPagingEnabled || this.CurrentPageIndex == 0;
			}
		}

		// Token: 0x170010FD RID: 4349
		// (get) Token: 0x06003A4F RID: 14927 RVA: 0x000BD475 File Offset: 0x000BB675
		public bool IsLastPage
		{
			get
			{
				return !this.IsPagingEnabled || this.CurrentPageIndex == this.PageCount - 1;
			}
		}

		// Token: 0x170010FE RID: 4350
		// (get) Token: 0x06003A50 RID: 14928 RVA: 0x000BD491 File Offset: 0x000BB691
		public bool IsPagingEnabled
		{
			get
			{
				return this.allowPaging && this.pageSize != 0;
			}
		}

		// Token: 0x170010FF RID: 4351
		// (get) Token: 0x06003A51 RID: 14929 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x06003A52 RID: 14930 RVA: 0x000BD4A6 File Offset: 0x000BB6A6
		public bool IsServerPagingEnabled
		{
			get
			{
				return this.IsPagingEnabled && this.allowServerPaging;
			}
		}

		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x06003A53 RID: 14931 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x06003A54 RID: 14932 RVA: 0x000BD4B8 File Offset: 0x000BB6B8
		public int PageCount
		{
			get
			{
				if (this.dataSource == null)
				{
					return 0;
				}
				int dataSourceCount = this.DataSourceCount;
				if (!this.IsPagingEnabled || dataSourceCount <= 0)
				{
					return 1;
				}
				int num = dataSourceCount + this.pageSize - 1;
				if (num < 0)
				{
					return 1;
				}
				return num / this.pageSize;
			}
		}

		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x06003A55 RID: 14933 RVA: 0x000BD4FD File Offset: 0x000BB6FD
		// (set) Token: 0x06003A56 RID: 14934 RVA: 0x000BD505 File Offset: 0x000BB705
		public int PageSize
		{
			get
			{
				return this.pageSize;
			}
			set
			{
				this.pageSize = value;
			}
		}

		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x06003A57 RID: 14935 RVA: 0x00004335 File Offset: 0x00002535
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001105 RID: 4357
		// (get) Token: 0x06003A58 RID: 14936 RVA: 0x000BD50E File Offset: 0x000BB70E
		// (set) Token: 0x06003A59 RID: 14937 RVA: 0x000BD516 File Offset: 0x000BB716
		public int VirtualCount
		{
			get
			{
				return this.virtualCount;
			}
			set
			{
				this.virtualCount = value;
			}
		}

		// Token: 0x06003A5A RID: 14938 RVA: 0x000BD520 File Offset: 0x000BB720
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x06003A5B RID: 14939 RVA: 0x000BD550 File Offset: 0x000BB750
		public IEnumerator GetEnumerator()
		{
			int firstIndexInPage = this.FirstIndexInPage;
			int count = -1;
			if (this.dataSource is ICollection)
			{
				count = this.Count;
			}
			if (this.dataSource is IList)
			{
				return new PagedDataSource.EnumeratorOnIList((IList)this.dataSource, firstIndexInPage, count);
			}
			if (this.dataSource is Array)
			{
				return new PagedDataSource.EnumeratorOnArray((object[])this.dataSource, firstIndexInPage, count);
			}
			if (this.dataSource is ICollection)
			{
				return new PagedDataSource.EnumeratorOnICollection((ICollection)this.dataSource, firstIndexInPage, count);
			}
			if (this.allowCustomPaging || this.allowServerPaging)
			{
				return new PagedDataSource.EnumeratorOnIEnumerator(this.dataSource.GetEnumerator(), this.Count);
			}
			return this.dataSource.GetEnumerator();
		}

		// Token: 0x06003A5C RID: 14940 RVA: 0x000BD60C File Offset: 0x000BB80C
		public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			if (this.dataSource == null)
			{
				return null;
			}
			if (this.dataSource is ITypedList)
			{
				return ((ITypedList)this.dataSource).GetItemProperties(listAccessors);
			}
			return null;
		}

		// Token: 0x06003A5D RID: 14941 RVA: 0x00028752 File Offset: 0x00026952
		public string GetListName(PropertyDescriptor[] listAccessors)
		{
			return string.Empty;
		}

		// Token: 0x040022EC RID: 8940
		private IEnumerable dataSource;

		// Token: 0x040022ED RID: 8941
		private int currentPageIndex;

		// Token: 0x040022EE RID: 8942
		private int pageSize;

		// Token: 0x040022EF RID: 8943
		private bool allowPaging;

		// Token: 0x040022F0 RID: 8944
		private bool allowCustomPaging;

		// Token: 0x040022F1 RID: 8945
		private bool allowServerPaging;

		// Token: 0x040022F2 RID: 8946
		private int virtualCount;

		// Token: 0x020009BB RID: 2491
		private sealed class EnumeratorOnIEnumerator : IEnumerator
		{
			// Token: 0x06006C02 RID: 27650 RVA: 0x00182534 File Offset: 0x00180734
			public EnumeratorOnIEnumerator(IEnumerator realEnum, int count)
			{
				this.realEnum = realEnum;
				this.index = -1;
				this.indexBounds = count;
			}

			// Token: 0x17001DC3 RID: 7619
			// (get) Token: 0x06006C03 RID: 27651 RVA: 0x00182551 File Offset: 0x00180751
			public object Current
			{
				get
				{
					return this.realEnum.Current;
				}
			}

			// Token: 0x06006C04 RID: 27652 RVA: 0x00182560 File Offset: 0x00180760
			public bool MoveNext()
			{
				bool flag = this.realEnum.MoveNext();
				this.index++;
				return flag && this.index < this.indexBounds;
			}

			// Token: 0x06006C05 RID: 27653 RVA: 0x0018259A File Offset: 0x0018079A
			public void Reset()
			{
				this.realEnum.Reset();
				this.index = -1;
			}

			// Token: 0x0400397F RID: 14719
			private IEnumerator realEnum;

			// Token: 0x04003980 RID: 14720
			private int index;

			// Token: 0x04003981 RID: 14721
			private int indexBounds;
		}

		// Token: 0x020009BC RID: 2492
		private sealed class EnumeratorOnICollection : IEnumerator
		{
			// Token: 0x06006C06 RID: 27654 RVA: 0x001825AE File Offset: 0x001807AE
			public EnumeratorOnICollection(ICollection collection, int startIndex, int count)
			{
				this.collection = collection;
				this.startIndex = startIndex;
				this.index = -1;
				this.indexBounds = startIndex + count;
				if (this.indexBounds > collection.Count)
				{
					this.indexBounds = collection.Count;
				}
			}

			// Token: 0x17001DC4 RID: 7620
			// (get) Token: 0x06006C07 RID: 27655 RVA: 0x001825EE File Offset: 0x001807EE
			public object Current
			{
				get
				{
					return this.collectionEnum.Current;
				}
			}

			// Token: 0x06006C08 RID: 27656 RVA: 0x001825FC File Offset: 0x001807FC
			public bool MoveNext()
			{
				if (this.collectionEnum == null)
				{
					this.collectionEnum = this.collection.GetEnumerator();
					for (int i = 0; i < this.startIndex; i++)
					{
						this.collectionEnum.MoveNext();
					}
				}
				this.collectionEnum.MoveNext();
				this.index++;
				return this.startIndex + this.index < this.indexBounds;
			}

			// Token: 0x06006C09 RID: 27657 RVA: 0x0018266E File Offset: 0x0018086E
			public void Reset()
			{
				this.collectionEnum = null;
				this.index = -1;
			}

			// Token: 0x04003982 RID: 14722
			private ICollection collection;

			// Token: 0x04003983 RID: 14723
			private IEnumerator collectionEnum;

			// Token: 0x04003984 RID: 14724
			private int startIndex;

			// Token: 0x04003985 RID: 14725
			private int index;

			// Token: 0x04003986 RID: 14726
			private int indexBounds;
		}

		// Token: 0x020009BD RID: 2493
		private sealed class EnumeratorOnIList : IEnumerator
		{
			// Token: 0x06006C0A RID: 27658 RVA: 0x0018267E File Offset: 0x0018087E
			public EnumeratorOnIList(IList collection, int startIndex, int count)
			{
				this.collection = collection;
				this.startIndex = startIndex;
				this.index = -1;
				this.indexBounds = startIndex + count;
				if (this.indexBounds > collection.Count)
				{
					this.indexBounds = collection.Count;
				}
			}

			// Token: 0x17001DC5 RID: 7621
			// (get) Token: 0x06006C0B RID: 27659 RVA: 0x001826BE File Offset: 0x001808BE
			public object Current
			{
				get
				{
					if (this.index < 0)
					{
						throw new InvalidOperationException(SR.GetString("Enumerator_MoveNext_Not_Called"));
					}
					return this.collection[this.startIndex + this.index];
				}
			}

			// Token: 0x06006C0C RID: 27660 RVA: 0x001826F1 File Offset: 0x001808F1
			public bool MoveNext()
			{
				this.index++;
				return this.startIndex + this.index < this.indexBounds;
			}

			// Token: 0x06006C0D RID: 27661 RVA: 0x00182716 File Offset: 0x00180916
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x04003987 RID: 14727
			private IList collection;

			// Token: 0x04003988 RID: 14728
			private int startIndex;

			// Token: 0x04003989 RID: 14729
			private int index;

			// Token: 0x0400398A RID: 14730
			private int indexBounds;
		}

		// Token: 0x020009BE RID: 2494
		private sealed class EnumeratorOnArray : IEnumerator
		{
			// Token: 0x06006C0E RID: 27662 RVA: 0x0018271F File Offset: 0x0018091F
			public EnumeratorOnArray(object[] array, int startIndex, int count)
			{
				this.array = array;
				this.startIndex = startIndex;
				this.index = -1;
				this.indexBounds = startIndex + count;
				if (this.indexBounds > array.Length)
				{
					this.indexBounds = array.Length;
				}
			}

			// Token: 0x17001DC6 RID: 7622
			// (get) Token: 0x06006C0F RID: 27663 RVA: 0x00182759 File Offset: 0x00180959
			public object Current
			{
				get
				{
					if (this.index < 0)
					{
						throw new InvalidOperationException(SR.GetString("Enumerator_MoveNext_Not_Called"));
					}
					return this.array[this.startIndex + this.index];
				}
			}

			// Token: 0x06006C10 RID: 27664 RVA: 0x00182788 File Offset: 0x00180988
			public bool MoveNext()
			{
				this.index++;
				return this.startIndex + this.index < this.indexBounds;
			}

			// Token: 0x06006C11 RID: 27665 RVA: 0x001827AD File Offset: 0x001809AD
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x0400398B RID: 14731
			private object[] array;

			// Token: 0x0400398C RID: 14732
			private int startIndex;

			// Token: 0x0400398D RID: 14733
			private int index;

			// Token: 0x0400398E RID: 14734
			private int indexBounds;
		}
	}
}

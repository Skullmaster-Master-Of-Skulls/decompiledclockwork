using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000603 RID: 1539
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class PagedDataSource : ICollection, IEnumerable, ITypedList
	{
		// Token: 0x06004C32 RID: 19506 RVA: 0x00135D56 File Offset: 0x00134D56
		public PagedDataSource()
		{
			this.pageSize = 10;
			this.allowPaging = false;
			this.currentPageIndex = 0;
			this.allowCustomPaging = false;
			this.allowServerPaging = false;
			this.virtualCount = 0;
		}

		// Token: 0x17001315 RID: 4885
		// (get) Token: 0x06004C33 RID: 19507 RVA: 0x00135D89 File Offset: 0x00134D89
		// (set) Token: 0x06004C34 RID: 19508 RVA: 0x00135D91 File Offset: 0x00134D91
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

		// Token: 0x17001316 RID: 4886
		// (get) Token: 0x06004C35 RID: 19509 RVA: 0x00135D9A File Offset: 0x00134D9A
		// (set) Token: 0x06004C36 RID: 19510 RVA: 0x00135DA2 File Offset: 0x00134DA2
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

		// Token: 0x17001317 RID: 4887
		// (get) Token: 0x06004C37 RID: 19511 RVA: 0x00135DAB File Offset: 0x00134DAB
		// (set) Token: 0x06004C38 RID: 19512 RVA: 0x00135DB3 File Offset: 0x00134DB3
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

		// Token: 0x17001318 RID: 4888
		// (get) Token: 0x06004C39 RID: 19513 RVA: 0x00135DBC File Offset: 0x00134DBC
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

		// Token: 0x17001319 RID: 4889
		// (get) Token: 0x06004C3A RID: 19514 RVA: 0x00135DFB File Offset: 0x00134DFB
		// (set) Token: 0x06004C3B RID: 19515 RVA: 0x00135E03 File Offset: 0x00134E03
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

		// Token: 0x1700131A RID: 4890
		// (get) Token: 0x06004C3C RID: 19516 RVA: 0x00135E0C File Offset: 0x00134E0C
		// (set) Token: 0x06004C3D RID: 19517 RVA: 0x00135E14 File Offset: 0x00134E14
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

		// Token: 0x1700131B RID: 4891
		// (get) Token: 0x06004C3E RID: 19518 RVA: 0x00135E20 File Offset: 0x00134E20
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

		// Token: 0x1700131C RID: 4892
		// (get) Token: 0x06004C3F RID: 19519 RVA: 0x00135E7B File Offset: 0x00134E7B
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

		// Token: 0x1700131D RID: 4893
		// (get) Token: 0x06004C40 RID: 19520 RVA: 0x00135EAE File Offset: 0x00134EAE
		public bool IsCustomPagingEnabled
		{
			get
			{
				return this.IsPagingEnabled && this.allowCustomPaging;
			}
		}

		// Token: 0x1700131E RID: 4894
		// (get) Token: 0x06004C41 RID: 19521 RVA: 0x00135EC0 File Offset: 0x00134EC0
		public bool IsFirstPage
		{
			get
			{
				return !this.IsPagingEnabled || this.CurrentPageIndex == 0;
			}
		}

		// Token: 0x1700131F RID: 4895
		// (get) Token: 0x06004C42 RID: 19522 RVA: 0x00135ED5 File Offset: 0x00134ED5
		public bool IsLastPage
		{
			get
			{
				return !this.IsPagingEnabled || this.CurrentPageIndex == this.PageCount - 1;
			}
		}

		// Token: 0x17001320 RID: 4896
		// (get) Token: 0x06004C43 RID: 19523 RVA: 0x00135EF1 File Offset: 0x00134EF1
		public bool IsPagingEnabled
		{
			get
			{
				return this.allowPaging && this.pageSize != 0;
			}
		}

		// Token: 0x17001321 RID: 4897
		// (get) Token: 0x06004C44 RID: 19524 RVA: 0x00135F09 File Offset: 0x00134F09
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001322 RID: 4898
		// (get) Token: 0x06004C45 RID: 19525 RVA: 0x00135F0C File Offset: 0x00134F0C
		public bool IsServerPagingEnabled
		{
			get
			{
				return this.IsPagingEnabled && this.allowServerPaging;
			}
		}

		// Token: 0x17001323 RID: 4899
		// (get) Token: 0x06004C46 RID: 19526 RVA: 0x00135F1E File Offset: 0x00134F1E
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001324 RID: 4900
		// (get) Token: 0x06004C47 RID: 19527 RVA: 0x00135F24 File Offset: 0x00134F24
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

		// Token: 0x17001325 RID: 4901
		// (get) Token: 0x06004C48 RID: 19528 RVA: 0x00135F69 File Offset: 0x00134F69
		// (set) Token: 0x06004C49 RID: 19529 RVA: 0x00135F71 File Offset: 0x00134F71
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

		// Token: 0x17001326 RID: 4902
		// (get) Token: 0x06004C4A RID: 19530 RVA: 0x00135F7A File Offset: 0x00134F7A
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17001327 RID: 4903
		// (get) Token: 0x06004C4B RID: 19531 RVA: 0x00135F7D File Offset: 0x00134F7D
		// (set) Token: 0x06004C4C RID: 19532 RVA: 0x00135F85 File Offset: 0x00134F85
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

		// Token: 0x06004C4D RID: 19533 RVA: 0x00135F90 File Offset: 0x00134F90
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x06004C4E RID: 19534 RVA: 0x00135FC0 File Offset: 0x00134FC0
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

		// Token: 0x06004C4F RID: 19535 RVA: 0x0013607C File Offset: 0x0013507C
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

		// Token: 0x06004C50 RID: 19536 RVA: 0x001360A8 File Offset: 0x001350A8
		public string GetListName(PropertyDescriptor[] listAccessors)
		{
			return string.Empty;
		}

		// Token: 0x04002BE2 RID: 11234
		private IEnumerable dataSource;

		// Token: 0x04002BE3 RID: 11235
		private int currentPageIndex;

		// Token: 0x04002BE4 RID: 11236
		private int pageSize;

		// Token: 0x04002BE5 RID: 11237
		private bool allowPaging;

		// Token: 0x04002BE6 RID: 11238
		private bool allowCustomPaging;

		// Token: 0x04002BE7 RID: 11239
		private bool allowServerPaging;

		// Token: 0x04002BE8 RID: 11240
		private int virtualCount;

		// Token: 0x02000604 RID: 1540
		private sealed class EnumeratorOnIEnumerator : IEnumerator
		{
			// Token: 0x06004C51 RID: 19537 RVA: 0x001360AF File Offset: 0x001350AF
			public EnumeratorOnIEnumerator(IEnumerator realEnum, int count)
			{
				this.realEnum = realEnum;
				this.index = -1;
				this.indexBounds = count;
			}

			// Token: 0x17001328 RID: 4904
			// (get) Token: 0x06004C52 RID: 19538 RVA: 0x001360CC File Offset: 0x001350CC
			public object Current
			{
				get
				{
					return this.realEnum.Current;
				}
			}

			// Token: 0x06004C53 RID: 19539 RVA: 0x001360DC File Offset: 0x001350DC
			public bool MoveNext()
			{
				bool flag = this.realEnum.MoveNext();
				this.index++;
				return flag && this.index < this.indexBounds;
			}

			// Token: 0x06004C54 RID: 19540 RVA: 0x00136116 File Offset: 0x00135116
			public void Reset()
			{
				this.realEnum.Reset();
				this.index = -1;
			}

			// Token: 0x04002BE9 RID: 11241
			private IEnumerator realEnum;

			// Token: 0x04002BEA RID: 11242
			private int index;

			// Token: 0x04002BEB RID: 11243
			private int indexBounds;
		}

		// Token: 0x02000605 RID: 1541
		private sealed class EnumeratorOnICollection : IEnumerator
		{
			// Token: 0x06004C55 RID: 19541 RVA: 0x0013612A File Offset: 0x0013512A
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

			// Token: 0x17001329 RID: 4905
			// (get) Token: 0x06004C56 RID: 19542 RVA: 0x0013616A File Offset: 0x0013516A
			public object Current
			{
				get
				{
					return this.collectionEnum.Current;
				}
			}

			// Token: 0x06004C57 RID: 19543 RVA: 0x00136178 File Offset: 0x00135178
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

			// Token: 0x06004C58 RID: 19544 RVA: 0x001361EA File Offset: 0x001351EA
			public void Reset()
			{
				this.collectionEnum = null;
				this.index = -1;
			}

			// Token: 0x04002BEC RID: 11244
			private ICollection collection;

			// Token: 0x04002BED RID: 11245
			private IEnumerator collectionEnum;

			// Token: 0x04002BEE RID: 11246
			private int startIndex;

			// Token: 0x04002BEF RID: 11247
			private int index;

			// Token: 0x04002BF0 RID: 11248
			private int indexBounds;
		}

		// Token: 0x02000606 RID: 1542
		private sealed class EnumeratorOnIList : IEnumerator
		{
			// Token: 0x06004C59 RID: 19545 RVA: 0x001361FA File Offset: 0x001351FA
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

			// Token: 0x1700132A RID: 4906
			// (get) Token: 0x06004C5A RID: 19546 RVA: 0x0013623A File Offset: 0x0013523A
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

			// Token: 0x06004C5B RID: 19547 RVA: 0x0013626D File Offset: 0x0013526D
			public bool MoveNext()
			{
				this.index++;
				return this.startIndex + this.index < this.indexBounds;
			}

			// Token: 0x06004C5C RID: 19548 RVA: 0x00136292 File Offset: 0x00135292
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x04002BF1 RID: 11249
			private IList collection;

			// Token: 0x04002BF2 RID: 11250
			private int startIndex;

			// Token: 0x04002BF3 RID: 11251
			private int index;

			// Token: 0x04002BF4 RID: 11252
			private int indexBounds;
		}

		// Token: 0x02000607 RID: 1543
		private sealed class EnumeratorOnArray : IEnumerator
		{
			// Token: 0x06004C5D RID: 19549 RVA: 0x0013629B File Offset: 0x0013529B
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

			// Token: 0x1700132B RID: 4907
			// (get) Token: 0x06004C5E RID: 19550 RVA: 0x001362D5 File Offset: 0x001352D5
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

			// Token: 0x06004C5F RID: 19551 RVA: 0x00136304 File Offset: 0x00135304
			public bool MoveNext()
			{
				this.index++;
				return this.startIndex + this.index < this.indexBounds;
			}

			// Token: 0x06004C60 RID: 19552 RVA: 0x00136329 File Offset: 0x00135329
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x04002BF5 RID: 11253
			private object[] array;

			// Token: 0x04002BF6 RID: 11254
			private int startIndex;

			// Token: 0x04002BF7 RID: 11255
			private int index;

			// Token: 0x04002BF8 RID: 11256
			private int indexBounds;
		}
	}
}

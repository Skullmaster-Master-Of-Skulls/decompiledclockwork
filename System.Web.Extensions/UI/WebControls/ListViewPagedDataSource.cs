using System;
using System.Collections;
using System.ComponentModel;
using System.Web.Resources;

namespace System.Web.UI.WebControls
{
	// Token: 0x020000B6 RID: 182
	public class ListViewPagedDataSource : ICollection, IEnumerable, ITypedList
	{
		// Token: 0x060008D8 RID: 2264 RVA: 0x00022485 File Offset: 0x00020685
		public ListViewPagedDataSource()
		{
			this._allowServerPaging = false;
			this._totalRowCount = 0;
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060008D9 RID: 2265 RVA: 0x0002249B File Offset: 0x0002069B
		// (set) Token: 0x060008DA RID: 2266 RVA: 0x000224A3 File Offset: 0x000206A3
		public bool AllowServerPaging
		{
			get
			{
				return this._allowServerPaging;
			}
			set
			{
				this._allowServerPaging = value;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x000224AC File Offset: 0x000206AC
		public int Count
		{
			get
			{
				if (this._dataSource == null)
				{
					return 0;
				}
				if (this.IsLastPage)
				{
					return this.DataSourceCount - this.StartRowIndex;
				}
				if (this.MaximumRows >= 0)
				{
					return this.MaximumRows;
				}
				return this.DataSourceCount - this.StartRowIndex;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x000224EB File Offset: 0x000206EB
		// (set) Token: 0x060008DD RID: 2269 RVA: 0x000224F3 File Offset: 0x000206F3
		public IEnumerable DataSource
		{
			get
			{
				return this._dataSource;
			}
			set
			{
				this._dataSource = value;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x000224FC File Offset: 0x000206FC
		public int DataSourceCount
		{
			get
			{
				if (this._dataSource == null)
				{
					return 0;
				}
				if (this.IsServerPagingEnabled)
				{
					return this._totalRowCount;
				}
				if (this._dataSource is ICollection)
				{
					return ((ICollection)this._dataSource).Count;
				}
				throw new InvalidOperationException(AtlasWeb.ListViewPagedDataSource_CannotGetCount);
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x0002254A File Offset: 0x0002074A
		private bool IsLastPage
		{
			get
			{
				return this.StartRowIndex + this.MaximumRows >= this.DataSourceCount;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x0001359B File Offset: 0x0001179B
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x0002249B File Offset: 0x0002069B
		public bool IsServerPagingEnabled
		{
			get
			{
				return this._allowServerPaging;
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x0001359B File Offset: 0x0001179B
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x00022564 File Offset: 0x00020764
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x0002256C File Offset: 0x0002076C
		public int MaximumRows
		{
			get
			{
				return this._maximumRows;
			}
			set
			{
				this._maximumRows = value;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x00022575 File Offset: 0x00020775
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x0002257D File Offset: 0x0002077D
		public int StartRowIndex
		{
			get
			{
				return this._startRowIndex;
			}
			set
			{
				this._startRowIndex = value;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x00022586 File Offset: 0x00020786
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x00022589 File Offset: 0x00020789
		// (set) Token: 0x060008E9 RID: 2281 RVA: 0x00022591 File Offset: 0x00020791
		public int TotalRowCount
		{
			get
			{
				return this._totalRowCount;
			}
			set
			{
				this._totalRowCount = value;
			}
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x0002259C File Offset: 0x0002079C
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x000225CC File Offset: 0x000207CC
		public IEnumerator GetEnumerator()
		{
			int startRowIndex = 0;
			int count = -1;
			if (!this.IsServerPagingEnabled)
			{
				startRowIndex = this.StartRowIndex;
			}
			if (this._dataSource is ICollection)
			{
				count = this.Count;
			}
			if (this._dataSource is IList)
			{
				return new ListViewPagedDataSource.EnumeratorOnIList((IList)this._dataSource, startRowIndex, count);
			}
			if (this._dataSource is Array)
			{
				return new ListViewPagedDataSource.EnumeratorOnArray((object[])this._dataSource, startRowIndex, count);
			}
			if (this._dataSource is ICollection)
			{
				return new ListViewPagedDataSource.EnumeratorOnICollection((ICollection)this._dataSource, startRowIndex, count);
			}
			if (this._allowServerPaging)
			{
				return new ListViewPagedDataSource.EnumeratorOnIEnumerator(this._dataSource.GetEnumerator(), this.Count);
			}
			return this._dataSource.GetEnumerator();
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x0002268A File Offset: 0x0002088A
		public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			if (this._dataSource == null)
			{
				return null;
			}
			if (this._dataSource is ITypedList)
			{
				return ((ITypedList)this._dataSource).GetItemProperties(listAccessors);
			}
			return null;
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x000226B6 File Offset: 0x000208B6
		public string GetListName(PropertyDescriptor[] listAccessors)
		{
			return string.Empty;
		}

		// Token: 0x040002F2 RID: 754
		private IEnumerable _dataSource;

		// Token: 0x040002F3 RID: 755
		private bool _allowServerPaging;

		// Token: 0x040002F4 RID: 756
		private int _startRowIndex;

		// Token: 0x040002F5 RID: 757
		private int _maximumRows;

		// Token: 0x040002F6 RID: 758
		private int _totalRowCount;

		// Token: 0x0200016F RID: 367
		private sealed class EnumeratorOnIEnumerator : IEnumerator
		{
			// Token: 0x0600104F RID: 4175 RVA: 0x00038009 File Offset: 0x00036209
			public EnumeratorOnIEnumerator(IEnumerator realEnum, int count)
			{
				this.realEnum = realEnum;
				this.index = -1;
				this.indexBounds = count;
			}

			// Token: 0x170005A2 RID: 1442
			// (get) Token: 0x06001050 RID: 4176 RVA: 0x00038026 File Offset: 0x00036226
			public object Current
			{
				get
				{
					return this.realEnum.Current;
				}
			}

			// Token: 0x06001051 RID: 4177 RVA: 0x00038034 File Offset: 0x00036234
			public bool MoveNext()
			{
				bool flag = this.realEnum.MoveNext();
				this.index++;
				return flag && this.index < this.indexBounds;
			}

			// Token: 0x06001052 RID: 4178 RVA: 0x0003806E File Offset: 0x0003626E
			public void Reset()
			{
				this.realEnum.Reset();
				this.index = -1;
			}

			// Token: 0x040004FD RID: 1277
			private IEnumerator realEnum;

			// Token: 0x040004FE RID: 1278
			private int index;

			// Token: 0x040004FF RID: 1279
			private int indexBounds;
		}

		// Token: 0x02000170 RID: 368
		private sealed class EnumeratorOnICollection : IEnumerator
		{
			// Token: 0x06001053 RID: 4179 RVA: 0x00038082 File Offset: 0x00036282
			public EnumeratorOnICollection(ICollection collection, int startRowIndex, int count)
			{
				this.collection = collection;
				this.startRowIndex = startRowIndex;
				this.index = -1;
				this.indexBounds = startRowIndex + count;
				if (this.indexBounds > collection.Count)
				{
					this.indexBounds = collection.Count;
				}
			}

			// Token: 0x170005A3 RID: 1443
			// (get) Token: 0x06001054 RID: 4180 RVA: 0x000380C2 File Offset: 0x000362C2
			public object Current
			{
				get
				{
					return this.collectionEnum.Current;
				}
			}

			// Token: 0x06001055 RID: 4181 RVA: 0x000380D0 File Offset: 0x000362D0
			public bool MoveNext()
			{
				if (this.collectionEnum == null)
				{
					this.collectionEnum = this.collection.GetEnumerator();
					for (int i = 0; i < this.startRowIndex; i++)
					{
						this.collectionEnum.MoveNext();
					}
				}
				this.collectionEnum.MoveNext();
				this.index++;
				return this.startRowIndex + this.index < this.indexBounds;
			}

			// Token: 0x06001056 RID: 4182 RVA: 0x00038142 File Offset: 0x00036342
			public void Reset()
			{
				this.collectionEnum = null;
				this.index = -1;
			}

			// Token: 0x04000500 RID: 1280
			private ICollection collection;

			// Token: 0x04000501 RID: 1281
			private IEnumerator collectionEnum;

			// Token: 0x04000502 RID: 1282
			private int startRowIndex;

			// Token: 0x04000503 RID: 1283
			private int index;

			// Token: 0x04000504 RID: 1284
			private int indexBounds;
		}

		// Token: 0x02000171 RID: 369
		private sealed class EnumeratorOnIList : IEnumerator
		{
			// Token: 0x06001057 RID: 4183 RVA: 0x00038152 File Offset: 0x00036352
			public EnumeratorOnIList(IList collection, int startRowIndex, int count)
			{
				this.collection = collection;
				this.startRowIndex = startRowIndex;
				this.index = -1;
				this.indexBounds = startRowIndex + count;
				if (this.indexBounds > collection.Count)
				{
					this.indexBounds = collection.Count;
				}
			}

			// Token: 0x170005A4 RID: 1444
			// (get) Token: 0x06001058 RID: 4184 RVA: 0x00038192 File Offset: 0x00036392
			public object Current
			{
				get
				{
					if (this.index < 0)
					{
						throw new InvalidOperationException(AtlasWeb.ListViewPagedDataSource_EnumeratorMoveNextNotCalled);
					}
					return this.collection[this.startRowIndex + this.index];
				}
			}

			// Token: 0x06001059 RID: 4185 RVA: 0x000381C0 File Offset: 0x000363C0
			public bool MoveNext()
			{
				this.index++;
				return this.startRowIndex + this.index < this.indexBounds;
			}

			// Token: 0x0600105A RID: 4186 RVA: 0x000381E5 File Offset: 0x000363E5
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x04000505 RID: 1285
			private IList collection;

			// Token: 0x04000506 RID: 1286
			private int startRowIndex;

			// Token: 0x04000507 RID: 1287
			private int index;

			// Token: 0x04000508 RID: 1288
			private int indexBounds;
		}

		// Token: 0x02000172 RID: 370
		private sealed class EnumeratorOnArray : IEnumerator
		{
			// Token: 0x0600105B RID: 4187 RVA: 0x000381EE File Offset: 0x000363EE
			public EnumeratorOnArray(object[] array, int startRowIndex, int count)
			{
				this.array = array;
				this.startRowIndex = startRowIndex;
				this.index = -1;
				this.indexBounds = startRowIndex + count;
				if (this.indexBounds > array.Length)
				{
					this.indexBounds = array.Length;
				}
			}

			// Token: 0x170005A5 RID: 1445
			// (get) Token: 0x0600105C RID: 4188 RVA: 0x00038228 File Offset: 0x00036428
			public object Current
			{
				get
				{
					if (this.index < 0)
					{
						throw new InvalidOperationException(AtlasWeb.ListViewPagedDataSource_EnumeratorMoveNextNotCalled);
					}
					return this.array[this.startRowIndex + this.index];
				}
			}

			// Token: 0x0600105D RID: 4189 RVA: 0x00038252 File Offset: 0x00036452
			public bool MoveNext()
			{
				this.index++;
				return this.startRowIndex + this.index < this.indexBounds;
			}

			// Token: 0x0600105E RID: 4190 RVA: 0x00038277 File Offset: 0x00036477
			public void Reset()
			{
				this.index = -1;
			}

			// Token: 0x04000509 RID: 1289
			private object[] array;

			// Token: 0x0400050A RID: 1290
			private int startRowIndex;

			// Token: 0x0400050B RID: 1291
			private int index;

			// Token: 0x0400050C RID: 1292
			private int indexBounds;
		}
	}
}

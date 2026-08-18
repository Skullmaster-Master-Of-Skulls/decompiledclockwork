using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000765 RID: 1893
	public class PivotGridFiltersCollection : IList, ICollection, IList<PivotGridFilter>, ICollection<PivotGridFilter>, IEnumerable<PivotGridFilter>, IEnumerable, IStateManager
	{
		// Token: 0x060042AE RID: 17070 RVA: 0x000D077A File Offset: 0x000CE97A
		public PivotGridFiltersCollection()
		{
			this.filters = new List<PivotGridFilter>();
		}

		// Token: 0x060042AF RID: 17071 RVA: 0x000D078D File Offset: 0x000CE98D
		public int Add(object value)
		{
			this.InsertInternal(-1, (PivotGridFilter)value);
			return this.Count - 1;
		}

		// Token: 0x060042B0 RID: 17072 RVA: 0x000D07A4 File Offset: 0x000CE9A4
		public void Clear()
		{
			this.filters.Clear();
		}

		// Token: 0x060042B1 RID: 17073 RVA: 0x000D07B1 File Offset: 0x000CE9B1
		public bool Contains(object value)
		{
			return this.Contains(value as PivotGridFilter);
		}

		// Token: 0x060042B2 RID: 17074 RVA: 0x000D07BF File Offset: 0x000CE9BF
		public int IndexOf(object value)
		{
			return this.IndexOf(value as PivotGridFilter);
		}

		// Token: 0x060042B3 RID: 17075 RVA: 0x000D07CD File Offset: 0x000CE9CD
		public void Insert(int index, object value)
		{
			this.Insert(index, value as PivotGridFilter);
		}

		// Token: 0x170015C0 RID: 5568
		// (get) Token: 0x060042B4 RID: 17076 RVA: 0x000D07DC File Offset: 0x000CE9DC
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170015C1 RID: 5569
		// (get) Token: 0x060042B5 RID: 17077 RVA: 0x000D07DF File Offset: 0x000CE9DF
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060042B6 RID: 17078 RVA: 0x000D07E2 File Offset: 0x000CE9E2
		public void Remove(object value)
		{
			this.Remove(value as PivotGridFilter);
		}

		// Token: 0x060042B7 RID: 17079 RVA: 0x000D07F1 File Offset: 0x000CE9F1
		public void RemoveAt(int index)
		{
			this.RemoveInternal(index, null);
		}

		// Token: 0x170015C2 RID: 5570
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				((IList<PivotGridFilter>)this)[index] = (PivotGridFilter)value;
			}
		}

		// Token: 0x060042BA RID: 17082 RVA: 0x000D0814 File Offset: 0x000CEA14
		public void CopyTo(Array array, int index)
		{
			foreach (object value in this)
			{
				array.SetValue(value, index++);
			}
		}

		// Token: 0x170015C3 RID: 5571
		// (get) Token: 0x060042BB RID: 17083 RVA: 0x000D0844 File Offset: 0x000CEA44
		public int Count
		{
			get
			{
				return this.filters.Count;
			}
		}

		// Token: 0x170015C4 RID: 5572
		// (get) Token: 0x060042BC RID: 17084 RVA: 0x000D0851 File Offset: 0x000CEA51
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170015C5 RID: 5573
		// (get) Token: 0x060042BD RID: 17085 RVA: 0x000D0854 File Offset: 0x000CEA54
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060042BE RID: 17086 RVA: 0x000D0857 File Offset: 0x000CEA57
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170015C6 RID: 5574
		// (get) Token: 0x060042BF RID: 17087 RVA: 0x000D085F File Offset: 0x000CEA5F
		public bool IsTrackingViewState
		{
			get
			{
				return this.isTrackingViewState;
			}
		}

		// Token: 0x060042C0 RID: 17088 RVA: 0x000D0867 File Offset: 0x000CEA67
		public void LoadViewState(object state)
		{
			this.filters = (state as List<PivotGridFilter>);
		}

		// Token: 0x060042C1 RID: 17089 RVA: 0x000D0875 File Offset: 0x000CEA75
		public object SaveViewState()
		{
			return this.filters;
		}

		// Token: 0x060042C2 RID: 17090 RVA: 0x000D087D File Offset: 0x000CEA7D
		public void TrackViewState()
		{
			this.isTrackingViewState = true;
		}

		// Token: 0x060042C3 RID: 17091 RVA: 0x000D0886 File Offset: 0x000CEA86
		public void Add(PivotGridFilter item)
		{
			this.filters.Add(item);
		}

		// Token: 0x060042C4 RID: 17092 RVA: 0x000D0894 File Offset: 0x000CEA94
		public bool Contains(PivotGridFilter item)
		{
			return this.filters.Contains(item);
		}

		// Token: 0x060042C5 RID: 17093 RVA: 0x000D08A2 File Offset: 0x000CEAA2
		public void CopyTo(PivotGridFilter[] array, int arrayIndex)
		{
			this.filters.CopyTo(array, arrayIndex);
		}

		// Token: 0x060042C6 RID: 17094 RVA: 0x000D08B1 File Offset: 0x000CEAB1
		public bool Remove(PivotGridFilter item)
		{
			return this.filters.Remove(item);
		}

		// Token: 0x060042C7 RID: 17095 RVA: 0x000D08BF File Offset: 0x000CEABF
		public IEnumerator<PivotGridFilter> GetEnumerator()
		{
			return this.filters.GetEnumerator();
		}

		// Token: 0x060042C8 RID: 17096 RVA: 0x000D08D1 File Offset: 0x000CEAD1
		public int IndexOf(PivotGridFilter item)
		{
			return this.filters.IndexOf(item);
		}

		// Token: 0x060042C9 RID: 17097 RVA: 0x000D08DF File Offset: 0x000CEADF
		public void Insert(int index, PivotGridFilter item)
		{
			this.filters.Insert(index, item);
		}

		// Token: 0x170015C7 RID: 5575
		public PivotGridFilter this[int index]
		{
			get
			{
				return this.filters[index];
			}
			set
			{
				this.filters[index] = value;
			}
		}

		// Token: 0x060042CC RID: 17100 RVA: 0x000D090B File Offset: 0x000CEB0B
		public int RemoveAll(Predicate<PivotGridFilter> match)
		{
			return this.filters.RemoveAll(match);
		}

		// Token: 0x060042CD RID: 17101 RVA: 0x000D0919 File Offset: 0x000CEB19
		private void InsertInternal(int index, PivotGridFilter filter)
		{
			if (filter == null)
			{
				throw new ArgumentNullException("filter expression");
			}
			if (index < 0)
			{
				this.filters.Add(filter);
				return;
			}
			this.filters.Insert(index, filter);
		}

		// Token: 0x060042CE RID: 17102 RVA: 0x000D0948 File Offset: 0x000CEB48
		private bool RemoveInternal(int index, PivotGridFilter filter)
		{
			bool result;
			if (index < 0)
			{
				if (filter == null)
				{
					throw new ArgumentNullException("filter expression", "Value cannot be null.");
				}
				result = this.filters.Remove(filter);
			}
			else
			{
				this.filters.RemoveAt(index);
				result = true;
			}
			return result;
		}

		// Token: 0x040011BA RID: 4538
		private bool isTrackingViewState;

		// Token: 0x040011BB RID: 4539
		private List<PivotGridFilter> filters;
	}
}

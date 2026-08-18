using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Xml;

namespace Telerik.Charting
{
	// Token: 0x02001708 RID: 5896
	public abstract class ChartingStateManagedCollection<T> : IDeserializableCollection, IList<T>, ICollection<!0>, IEnumerable<!0>, IChartingStateManager, IList, ICollection, IEnumerable where T : class, IChartingStateManagedItem, new()
	{
		// Token: 0x170045D2 RID: 17874
		// (get) Token: 0x0600E507 RID: 58631 RVA: 0x0032E732 File Offset: 0x0032C932
		protected IList<T> List
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x170045D3 RID: 17875
		// (get) Token: 0x0600E508 RID: 58632 RVA: 0x0032E73C File Offset: 0x0032C93C
		internal T First
		{
			get
			{
				if (this.Count > 0)
				{
					return this.items[0];
				}
				return default(T);
			}
		}

		// Token: 0x170045D4 RID: 17876
		// (get) Token: 0x0600E509 RID: 58633 RVA: 0x0032E768 File Offset: 0x0032C968
		internal T Last
		{
			get
			{
				if (this.Count > 0)
				{
					return this.items[this.Count - 1];
				}
				return default(T);
			}
		}

		// Token: 0x0600E50A RID: 58634 RVA: 0x0032E79B File Offset: 0x0032C99B
		public int IndexOf(T item)
		{
			return this.items.IndexOf(item);
		}

		// Token: 0x0600E50B RID: 58635 RVA: 0x0032E7AC File Offset: 0x0032C9AC
		public virtual void Insert(int index, T item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this.OnInsert(index, item);
			if (index == -1)
			{
				this.items.Add(item);
			}
			else
			{
				this.items.Insert(index, item);
			}
			this.OnInsertComplete(index, item);
		}

		// Token: 0x0600E50C RID: 58636 RVA: 0x0032E808 File Offset: 0x0032CA08
		public virtual void RemoveAt(int index)
		{
			T t = this.items[index];
			this.OnRemove(index, t);
			this.items.RemoveAt(index);
			try
			{
				this.OnRemoveComplete(index, t);
			}
			catch
			{
				this.items.Insert(index, t);
				throw;
			}
		}

		// Token: 0x170045D5 RID: 17877
		public virtual T this[int index]
		{
			get
			{
				return this.items[index];
			}
			set
			{
				this.items[index] = value;
			}
		}

		// Token: 0x0600E50F RID: 58639 RVA: 0x0032E889 File Offset: 0x0032CA89
		public virtual void Add(T item)
		{
			if (this.items.IndexOf(item) == -1)
			{
				this.Insert(-1, item);
			}
		}

		// Token: 0x0600E510 RID: 58640 RVA: 0x0032E8A4 File Offset: 0x0032CAA4
		public virtual void AddRange(T[] itemsToAdd)
		{
			foreach (T item in itemsToAdd)
			{
				this.Add(item);
			}
		}

		// Token: 0x0600E511 RID: 58641 RVA: 0x0032E8D0 File Offset: 0x0032CAD0
		public void Clear()
		{
			this.items.Clear();
			this.OnClearComplete();
		}

		// Token: 0x0600E512 RID: 58642 RVA: 0x0032E8E3 File Offset: 0x0032CAE3
		public virtual bool Contains(T item)
		{
			return this.items.Contains(item);
		}

		// Token: 0x0600E513 RID: 58643 RVA: 0x0032E8F1 File Offset: 0x0032CAF1
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.items.CopyTo(array, arrayIndex);
		}

		// Token: 0x170045D6 RID: 17878
		// (get) Token: 0x0600E514 RID: 58644 RVA: 0x0032E900 File Offset: 0x0032CB00
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x170045D7 RID: 17879
		// (get) Token: 0x0600E515 RID: 58645 RVA: 0x0032E90D File Offset: 0x0032CB0D
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600E516 RID: 58646 RVA: 0x0032E910 File Offset: 0x0032CB10
		public virtual bool Remove(T item)
		{
			return this.items.Remove(item);
		}

		// Token: 0x0600E517 RID: 58647 RVA: 0x0032E91E File Offset: 0x0032CB1E
		public IEnumerator<T> GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x0600E518 RID: 58648 RVA: 0x0032E930 File Offset: 0x0032CB30
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable)this.items).GetEnumerator();
		}

		// Token: 0x170045D8 RID: 17880
		// (get) Token: 0x0600E519 RID: 58649 RVA: 0x0032E93D File Offset: 0x0032CB3D
		bool IChartingStateManager.IsTrackingViewState
		{
			get
			{
				return this.tracking;
			}
		}

		// Token: 0x0600E51A RID: 58650 RVA: 0x0032E945 File Offset: 0x0032CB45
		void IChartingStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		// Token: 0x0600E51B RID: 58651 RVA: 0x0032E950 File Offset: 0x0032CB50
		protected virtual void LoadViewState(object state)
		{
			if (state != null)
			{
				Pair pair = state as Pair;
				if (pair != null)
				{
					int num = (int)pair.First;
					object[] array = (object[])pair.Second;
					this.Clear();
					foreach (object state2 in array)
					{
						T item = Activator.CreateInstance<T>();
						item.TrackViewState();
						item.LoadViewState(state2);
						this.items.Add(item);
					}
				}
			}
		}

		// Token: 0x0600E51C RID: 58652 RVA: 0x0032E9D4 File Offset: 0x0032CBD4
		protected virtual object SaveViewState()
		{
			object[] array = new object[this.items.Count];
			for (int i = 0; i < this.items.Count; i++)
			{
				T itemDirty = this.items[i];
				this.SetItemDirty(itemDirty);
				array[i] = itemDirty.SaveViewState();
			}
			return new Pair(this.items.Count, array);
		}

		// Token: 0x0600E51D RID: 58653 RVA: 0x0032EA42 File Offset: 0x0032CC42
		object IChartingStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x0600E51E RID: 58654 RVA: 0x0032EA4C File Offset: 0x0032CC4C
		void IChartingStateManager.TrackViewState()
		{
			this.tracking = true;
			foreach (T t in this.items)
			{
				IChartingStateManager chartingStateManager = t;
				chartingStateManager.TrackViewState();
			}
		}

		// Token: 0x0600E51F RID: 58655 RVA: 0x0032EAAC File Offset: 0x0032CCAC
		internal void SetDirty()
		{
			foreach (T itemDirty in this.items)
			{
				this.SetItemDirty(itemDirty);
			}
		}

		// Token: 0x0600E520 RID: 58656 RVA: 0x0032EB00 File Offset: 0x0032CD00
		protected virtual void SetItemDirty(T item)
		{
			item.SetDirty();
		}

		// Token: 0x0600E521 RID: 58657 RVA: 0x0032EB0F File Offset: 0x0032CD0F
		int IList.Add(object value)
		{
			this.Add(value as T);
			return this.Count - 1;
		}

		// Token: 0x0600E522 RID: 58658 RVA: 0x0032EB2A File Offset: 0x0032CD2A
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x0600E523 RID: 58659 RVA: 0x0032EB32 File Offset: 0x0032CD32
		bool IList.Contains(object value)
		{
			return this.Contains(value as T);
		}

		// Token: 0x0600E524 RID: 58660 RVA: 0x0032EB45 File Offset: 0x0032CD45
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as T);
		}

		// Token: 0x0600E525 RID: 58661 RVA: 0x0032EB58 File Offset: 0x0032CD58
		void IList.Insert(int index, object value)
		{
			this.Insert(index, value as T);
		}

		// Token: 0x170045D9 RID: 17881
		// (get) Token: 0x0600E526 RID: 58662 RVA: 0x0032EB6C File Offset: 0x0032CD6C
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170045DA RID: 17882
		// (get) Token: 0x0600E527 RID: 58663 RVA: 0x0032EB6F File Offset: 0x0032CD6F
		bool IList.IsReadOnly
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		// Token: 0x0600E528 RID: 58664 RVA: 0x0032EB77 File Offset: 0x0032CD77
		void IList.Remove(object value)
		{
			this.Remove(value as T);
		}

		// Token: 0x0600E529 RID: 58665 RVA: 0x0032EB8B File Offset: 0x0032CD8B
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x170045DB RID: 17883
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (value as T);
			}
		}

		// Token: 0x0600E52C RID: 58668 RVA: 0x0032EBB6 File Offset: 0x0032CDB6
		void ICollection.CopyTo(Array array, int index)
		{
		}

		// Token: 0x170045DC RID: 17884
		// (get) Token: 0x0600E52D RID: 58669 RVA: 0x0032EBB8 File Offset: 0x0032CDB8
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x170045DD RID: 17885
		// (get) Token: 0x0600E52E RID: 58670 RVA: 0x0032EBC0 File Offset: 0x0032CDC0
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170045DE RID: 17886
		// (get) Token: 0x0600E52F RID: 58671 RVA: 0x0032EBC3 File Offset: 0x0032CDC3
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600E530 RID: 58672 RVA: 0x0032EBC6 File Offset: 0x0032CDC6
		protected virtual void OnInsert(int index, object value)
		{
		}

		// Token: 0x0600E531 RID: 58673 RVA: 0x0032EBC8 File Offset: 0x0032CDC8
		protected virtual void OnInsertComplete(int index, object value)
		{
		}

		// Token: 0x0600E532 RID: 58674 RVA: 0x0032EBCA File Offset: 0x0032CDCA
		protected virtual void OnRemove(int index, object value)
		{
		}

		// Token: 0x0600E533 RID: 58675 RVA: 0x0032EBCC File Offset: 0x0032CDCC
		protected virtual void OnRemoveComplete(int index, object value)
		{
		}

		// Token: 0x0600E534 RID: 58676 RVA: 0x0032EBCE File Offset: 0x0032CDCE
		protected virtual void OnClear()
		{
		}

		// Token: 0x0600E535 RID: 58677 RVA: 0x0032EBD0 File Offset: 0x0032CDD0
		protected virtual void OnClearComplete()
		{
		}

		// Token: 0x0600E536 RID: 58678 RVA: 0x0032EBD4 File Offset: 0x0032CDD4
		void IDeserializableCollection.PopulateFromXml(XmlElement rootElement)
		{
			this.Clear();
			foreach (object obj in rootElement.ChildNodes)
			{
				XmlElement xmlElement = (XmlElement)obj;
				T item = (T)((object)Activator.CreateInstance(typeof(T)));
				this.Add(item);
			}
		}

		// Token: 0x0600E537 RID: 58679 RVA: 0x0032EC48 File Offset: 0x0032CE48
		internal void PopulateFromXml(XmlElement rootElement)
		{
			this.PopulateFromXml(rootElement);
		}

		// Token: 0x0600E538 RID: 58680 RVA: 0x0032EC51 File Offset: 0x0032CE51
		public override string ToString()
		{
			return string.Empty;
		}

		// Token: 0x040041FF RID: 16895
		private List<T> items = new List<T>();

		// Token: 0x04004200 RID: 16896
		private bool tracking;
	}
}

using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Web.UI
{
	// Token: 0x02000302 RID: 770
	public abstract class StateManagedCollection : IList, ICollection, IEnumerable, IStateManager
	{
		// Token: 0x0600238F RID: 9103 RVA: 0x00073C80 File Offset: 0x00071E80
		protected StateManagedCollection()
		{
			this._collectionItems = new ArrayList();
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06002390 RID: 9104 RVA: 0x00073C93 File Offset: 0x00071E93
		public int Count
		{
			get
			{
				return this._collectionItems.Count;
			}
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x00073CA0 File Offset: 0x00071EA0
		public void Clear()
		{
			this.OnClear();
			this._collectionItems.Clear();
			this.OnClearComplete();
			if (this._tracking)
			{
				this._saveAll = true;
			}
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x00073CC8 File Offset: 0x00071EC8
		public void CopyTo(Array array, int index)
		{
			this._collectionItems.CopyTo(array, index);
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x00073CD7 File Offset: 0x00071ED7
		protected virtual object CreateKnownType(int index)
		{
			throw new InvalidOperationException(SR.GetString("StateManagedCollection_NoKnownTypes"));
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x00073CE8 File Offset: 0x00071EE8
		public IEnumerator GetEnumerator()
		{
			return this._collectionItems.GetEnumerator();
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x0000298D File Offset: 0x00000B8D
		protected virtual Type[] GetKnownTypes()
		{
			return null;
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x00073CF8 File Offset: 0x00071EF8
		private int GetKnownTypeCount()
		{
			Type[] knownTypes = this.GetKnownTypes();
			if (knownTypes == null)
			{
				return 0;
			}
			return knownTypes.Length;
		}

		// Token: 0x06002397 RID: 9111 RVA: 0x00073D14 File Offset: 0x00071F14
		private void InsertInternal(int index, object o)
		{
			if (o == null)
			{
				throw new ArgumentNullException("o");
			}
			if (((IStateManager)this).IsTrackingViewState)
			{
				((IStateManager)o).TrackViewState();
				this.SetDirtyObject(o);
			}
			this.OnInsert(index, o);
			int index2;
			if (index == -1)
			{
				index2 = this._collectionItems.Add(o);
			}
			else
			{
				index2 = index;
				this._collectionItems.Insert(index, o);
			}
			try
			{
				this.OnInsertComplete(index, o);
			}
			catch
			{
				this._collectionItems.RemoveAt(index2);
				throw;
			}
		}

		// Token: 0x06002398 RID: 9112 RVA: 0x00073DA0 File Offset: 0x00071FA0
		private void LoadAllItemsFromViewState(object savedState)
		{
			Pair pair = (Pair)savedState;
			if (pair.Second is Pair)
			{
				Pair pair2 = (Pair)pair.Second;
				object[] array = (object[])pair.First;
				int[] array2 = (int[])pair2.First;
				ArrayList arrayList = (ArrayList)pair2.Second;
				this.Clear();
				for (int i = 0; i < array.Length; i++)
				{
					object obj;
					if (array2 == null)
					{
						obj = this.CreateKnownType(0);
					}
					else
					{
						int num = array2[i];
						if (num < this.GetKnownTypeCount())
						{
							obj = this.CreateKnownType(num);
						}
						else
						{
							string typeName = (string)arrayList[num - this.GetKnownTypeCount()];
							Type type = Type.GetType(typeName);
							obj = Activator.CreateInstance(type);
						}
					}
					((IStateManager)obj).TrackViewState();
					((IStateManager)obj).LoadViewState(array[i]);
					((IList)this).Add(obj);
				}
				return;
			}
			object[] array3 = (object[])pair.First;
			int[] array4 = (int[])pair.Second;
			this.Clear();
			for (int j = 0; j < array3.Length; j++)
			{
				int index = 0;
				if (array4 != null)
				{
					index = array4[j];
				}
				object obj2 = this.CreateKnownType(index);
				((IStateManager)obj2).TrackViewState();
				((IStateManager)obj2).LoadViewState(array3[j]);
				((IList)this).Add(obj2);
			}
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x00073EFC File Offset: 0x000720FC
		private void LoadChangedItemsFromViewState(object savedState)
		{
			Triplet triplet = (Triplet)savedState;
			if (triplet.Third is Pair)
			{
				Pair pair = (Pair)triplet.Third;
				ArrayList arrayList = (ArrayList)triplet.First;
				ArrayList arrayList2 = (ArrayList)triplet.Second;
				ArrayList arrayList3 = (ArrayList)pair.First;
				ArrayList arrayList4 = (ArrayList)pair.Second;
				for (int i = 0; i < arrayList.Count; i++)
				{
					int num = (int)arrayList[i];
					if (num < this.Count)
					{
						((IStateManager)((IList)this)[num]).LoadViewState(arrayList2[i]);
					}
					else
					{
						object obj;
						if (arrayList3 == null)
						{
							obj = this.CreateKnownType(0);
						}
						else
						{
							int num2 = (int)arrayList3[i];
							if (num2 < this.GetKnownTypeCount())
							{
								obj = this.CreateKnownType(num2);
							}
							else
							{
								string typeName = (string)arrayList4[num2 - this.GetKnownTypeCount()];
								Type type = Type.GetType(typeName);
								obj = Activator.CreateInstance(type);
							}
						}
						((IStateManager)obj).TrackViewState();
						((IStateManager)obj).LoadViewState(arrayList2[i]);
						((IList)this).Add(obj);
					}
				}
				return;
			}
			ArrayList arrayList5 = (ArrayList)triplet.First;
			ArrayList arrayList6 = (ArrayList)triplet.Second;
			ArrayList arrayList7 = (ArrayList)triplet.Third;
			for (int j = 0; j < arrayList5.Count; j++)
			{
				int num3 = (int)arrayList5[j];
				if (num3 < this.Count)
				{
					((IStateManager)((IList)this)[num3]).LoadViewState(arrayList6[j]);
				}
				else
				{
					int index = 0;
					if (arrayList7 != null)
					{
						index = (int)arrayList7[j];
					}
					object obj2 = this.CreateKnownType(index);
					((IStateManager)obj2).TrackViewState();
					((IStateManager)obj2).LoadViewState(arrayList6[j]);
					((IList)this).Add(obj2);
				}
			}
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void OnClear()
		{
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void OnClearComplete()
		{
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x000740FE File Offset: 0x000722FE
		protected virtual void OnValidate(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void OnInsert(int index, object value)
		{
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void OnInsertComplete(int index, object value)
		{
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void OnRemove(int index, object value)
		{
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void OnRemoveComplete(int index, object value)
		{
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x00074110 File Offset: 0x00072310
		private object SaveAllItemsToViewState()
		{
			bool flag = false;
			int count = this._collectionItems.Count;
			int[] array = new int[count];
			object[] array2 = new object[count];
			ArrayList arrayList = null;
			IDictionary dictionary = null;
			int knownTypeCount = this.GetKnownTypeCount();
			for (int i = 0; i < count; i++)
			{
				object obj = this._collectionItems[i];
				this.SetDirtyObject(obj);
				array2[i] = ((IStateManager)obj).SaveViewState();
				if (array2[i] != null)
				{
					flag = true;
				}
				Type type = obj.GetType();
				int num = -1;
				if (knownTypeCount != 0)
				{
					num = ((IList)this.GetKnownTypes()).IndexOf(type);
				}
				if (num != -1)
				{
					array[i] = num;
				}
				else
				{
					if (arrayList == null)
					{
						arrayList = new ArrayList();
						dictionary = new HybridDictionary();
					}
					object obj2 = dictionary[type];
					if (obj2 == null)
					{
						arrayList.Add(type.AssemblyQualifiedName);
						obj2 = arrayList.Count + knownTypeCount - 1;
						dictionary[type] = obj2;
					}
					array[i] = (int)obj2;
				}
			}
			if (!this._hadItems && !flag)
			{
				return null;
			}
			if (arrayList == null)
			{
				if (knownTypeCount == 1)
				{
					array = null;
				}
				return new Pair(array2, array);
			}
			return new Pair(array2, new Pair(array, arrayList));
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x00074240 File Offset: 0x00072440
		private object SaveChangedItemsToViewState()
		{
			bool flag = false;
			int count = this._collectionItems.Count;
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			ArrayList arrayList3 = new ArrayList();
			ArrayList arrayList4 = null;
			IDictionary dictionary = null;
			int knownTypeCount = this.GetKnownTypeCount();
			for (int i = 0; i < count; i++)
			{
				object obj = this._collectionItems[i];
				object obj2 = ((IStateManager)obj).SaveViewState();
				if (obj2 != null)
				{
					flag = true;
					arrayList.Add(i);
					arrayList2.Add(obj2);
					Type type = obj.GetType();
					int num = -1;
					if (knownTypeCount != 0)
					{
						num = ((IList)this.GetKnownTypes()).IndexOf(type);
					}
					if (num != -1)
					{
						arrayList3.Add(num);
					}
					else
					{
						if (arrayList4 == null)
						{
							arrayList4 = new ArrayList();
							dictionary = new HybridDictionary();
						}
						object obj3 = dictionary[type];
						if (obj3 == null)
						{
							arrayList4.Add(type.AssemblyQualifiedName);
							obj3 = arrayList4.Count + knownTypeCount - 1;
							dictionary[type] = obj3;
						}
						arrayList3.Add(obj3);
					}
				}
			}
			if (!this._hadItems && !flag)
			{
				return null;
			}
			if (arrayList4 == null)
			{
				if (knownTypeCount == 1)
				{
					arrayList3 = null;
				}
				return new Triplet(arrayList, arrayList2, arrayList3);
			}
			return new Triplet(arrayList, arrayList2, new Pair(arrayList3, arrayList4));
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x0007438F File Offset: 0x0007258F
		public void SetDirty()
		{
			this._saveAll = true;
		}

		// Token: 0x060023A4 RID: 9124
		protected abstract void SetDirtyObject(object o);

		// Token: 0x060023A5 RID: 9125 RVA: 0x00074398 File Offset: 0x00072598
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x060023A6 RID: 9126 RVA: 0x000743A0 File Offset: 0x000725A0
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x060023A7 RID: 9127 RVA: 0x00007722 File Offset: 0x00005922
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x060023A8 RID: 9128 RVA: 0x0000298D File Offset: 0x00000B8D
		object ICollection.SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x060023A9 RID: 9129 RVA: 0x00007722 File Offset: 0x00005922
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x060023AA RID: 9130 RVA: 0x000743A8 File Offset: 0x000725A8
		bool IList.IsReadOnly
		{
			get
			{
				return this._collectionItems.IsReadOnly;
			}
		}

		// Token: 0x170009FC RID: 2556
		object IList.this[int index]
		{
			get
			{
				return this._collectionItems[index];
			}
			set
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("StateManagedCollection_InvalidIndex"));
				}
				((IList)this).RemoveAt(index);
				((IList)this).Insert(index, value);
			}
		}

		// Token: 0x060023AD RID: 9133 RVA: 0x000743F6 File Offset: 0x000725F6
		int IList.Add(object value)
		{
			this.OnValidate(value);
			this.InsertInternal(-1, value);
			return this._collectionItems.Count - 1;
		}

		// Token: 0x060023AE RID: 9134 RVA: 0x00074414 File Offset: 0x00072614
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x060023AF RID: 9135 RVA: 0x0007441C File Offset: 0x0007261C
		bool IList.Contains(object value)
		{
			if (value == null)
			{
				return false;
			}
			this.OnValidate(value);
			return this._collectionItems.Contains(value);
		}

		// Token: 0x060023B0 RID: 9136 RVA: 0x00074436 File Offset: 0x00072636
		int IList.IndexOf(object value)
		{
			if (value == null)
			{
				return -1;
			}
			this.OnValidate(value);
			return this._collectionItems.IndexOf(value);
		}

		// Token: 0x060023B1 RID: 9137 RVA: 0x00074450 File Offset: 0x00072650
		void IList.Insert(int index, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (index < 0 || index > this.Count)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("StateManagedCollection_InvalidIndex"));
			}
			this.OnValidate(value);
			this.InsertInternal(index, value);
			if (this._tracking)
			{
				this._saveAll = true;
			}
		}

		// Token: 0x060023B2 RID: 9138 RVA: 0x000744AB File Offset: 0x000726AB
		void IList.Remove(object value)
		{
			if (value == null)
			{
				return;
			}
			this.OnValidate(value);
			((IList)this).RemoveAt(((IList)this).IndexOf(value));
		}

		// Token: 0x060023B3 RID: 9139 RVA: 0x000744C8 File Offset: 0x000726C8
		void IList.RemoveAt(int index)
		{
			object value = this._collectionItems[index];
			this.OnRemove(index, value);
			this._collectionItems.RemoveAt(index);
			try
			{
				this.OnRemoveComplete(index, value);
			}
			catch
			{
				this._collectionItems.Insert(index, value);
				throw;
			}
			if (this._tracking)
			{
				this._saveAll = true;
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x060023B4 RID: 9140 RVA: 0x00074530 File Offset: 0x00072730
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._tracking;
			}
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x00074538 File Offset: 0x00072738
		void IStateManager.LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				if (savedState is Triplet)
				{
					this.LoadChangedItemsFromViewState(savedState);
					return;
				}
				this.LoadAllItemsFromViewState(savedState);
			}
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x00074554 File Offset: 0x00072754
		object IStateManager.SaveViewState()
		{
			if (this._saveAll)
			{
				return this.SaveAllItemsToViewState();
			}
			return this.SaveChangedItemsToViewState();
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x0007456C File Offset: 0x0007276C
		void IStateManager.TrackViewState()
		{
			if (!((IStateManager)this).IsTrackingViewState)
			{
				this._hadItems = (this.Count > 0);
				this._tracking = true;
				foreach (object obj in this._collectionItems)
				{
					IStateManager stateManager = (IStateManager)obj;
					stateManager.TrackViewState();
				}
			}
		}

		// Token: 0x04001CC5 RID: 7365
		private ArrayList _collectionItems;

		// Token: 0x04001CC6 RID: 7366
		private bool _tracking;

		// Token: 0x04001CC7 RID: 7367
		private bool _saveAll;

		// Token: 0x04001CC8 RID: 7368
		private bool _hadItems;
	}
}

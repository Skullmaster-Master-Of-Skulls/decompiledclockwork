using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000300 RID: 768
	public sealed class StateBag : IStateManager, IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06002368 RID: 9064 RVA: 0x000738D0 File Offset: 0x00071AD0
		public StateBag() : this(false)
		{
		}

		// Token: 0x06002369 RID: 9065 RVA: 0x000738D9 File Offset: 0x00071AD9
		public StateBag(bool ignoreCase)
		{
			this.marked = false;
			this.ignoreCase = ignoreCase;
			this.bag = this.CreateBag();
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x0600236A RID: 9066 RVA: 0x000738FB File Offset: 0x00071AFB
		public int Count
		{
			get
			{
				return this.bag.Count;
			}
		}

		// Token: 0x170009EA RID: 2538
		// (get) Token: 0x0600236B RID: 9067 RVA: 0x00073908 File Offset: 0x00071B08
		public ICollection Keys
		{
			get
			{
				return this.bag.Keys;
			}
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x0600236C RID: 9068 RVA: 0x00073915 File Offset: 0x00071B15
		public ICollection Values
		{
			get
			{
				return this.bag.Values;
			}
		}

		// Token: 0x170009EC RID: 2540
		public object this[string key]
		{
			get
			{
				if (string.IsNullOrEmpty(key))
				{
					throw ExceptionUtil.ParameterNullOrEmpty("key");
				}
				StateItem stateItem = this.bag[key] as StateItem;
				if (stateItem != null)
				{
					return stateItem.Value;
				}
				return null;
			}
			set
			{
				this.Add(key, value);
			}
		}

		// Token: 0x170009ED RID: 2541
		object IDictionary.this[object key]
		{
			get
			{
				return this[(string)key];
			}
			set
			{
				this[(string)key] = value;
			}
		}

		// Token: 0x06002371 RID: 9073 RVA: 0x00073989 File Offset: 0x00071B89
		private IDictionary CreateBag()
		{
			return new HybridDictionary(this.ignoreCase);
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x00073998 File Offset: 0x00071B98
		public StateItem Add(string key, object value)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw ExceptionUtil.ParameterNullOrEmpty("key");
			}
			StateItem stateItem = this.bag[key] as StateItem;
			if (stateItem == null)
			{
				if (value != null || this.marked)
				{
					stateItem = new StateItem(value);
					this.bag.Add(key, stateItem);
				}
			}
			else if (value == null && !this.marked)
			{
				this.bag.Remove(key);
			}
			else
			{
				stateItem.Value = value;
			}
			if (stateItem != null && this.marked)
			{
				stateItem.IsDirty = true;
			}
			return stateItem;
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x00073A21 File Offset: 0x00071C21
		void IDictionary.Add(object key, object value)
		{
			this.Add((string)key, value);
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x00073A31 File Offset: 0x00071C31
		public void Clear()
		{
			this.bag.Clear();
		}

		// Token: 0x06002375 RID: 9077 RVA: 0x00073A3E File Offset: 0x00071C3E
		public IDictionaryEnumerator GetEnumerator()
		{
			return this.bag.GetEnumerator();
		}

		// Token: 0x06002376 RID: 9078 RVA: 0x00073A4C File Offset: 0x00071C4C
		public bool IsItemDirty(string key)
		{
			StateItem stateItem = this.bag[key] as StateItem;
			return stateItem != null && stateItem.IsDirty;
		}

		// Token: 0x170009EE RID: 2542
		// (get) Token: 0x06002377 RID: 9079 RVA: 0x00073A76 File Offset: 0x00071C76
		internal bool IsTrackingViewState
		{
			get
			{
				return this.marked;
			}
		}

		// Token: 0x06002378 RID: 9080 RVA: 0x00073A80 File Offset: 0x00071C80
		internal void LoadViewState(object state)
		{
			if (state != null)
			{
				ArrayList arrayList = (ArrayList)state;
				for (int i = 0; i < arrayList.Count; i += 2)
				{
					string value = ((IndexedString)arrayList[i]).Value;
					object value2 = arrayList[i + 1];
					this.Add(value, value2);
				}
			}
		}

		// Token: 0x06002379 RID: 9081 RVA: 0x00073ACD File Offset: 0x00071CCD
		internal void TrackViewState()
		{
			this.marked = true;
		}

		// Token: 0x0600237A RID: 9082 RVA: 0x00073AD6 File Offset: 0x00071CD6
		public void Remove(string key)
		{
			this.bag.Remove(key);
		}

		// Token: 0x0600237B RID: 9083 RVA: 0x00073AE4 File Offset: 0x00071CE4
		void IDictionary.Remove(object key)
		{
			this.Remove((string)key);
		}

		// Token: 0x0600237C RID: 9084 RVA: 0x00073AF4 File Offset: 0x00071CF4
		internal object SaveViewState()
		{
			ArrayList arrayList = null;
			if (this.bag.Count != 0)
			{
				IDictionaryEnumerator enumerator = this.bag.GetEnumerator();
				while (enumerator.MoveNext())
				{
					StateItem stateItem = (StateItem)enumerator.Value;
					if (stateItem.IsDirty)
					{
						if (arrayList == null)
						{
							arrayList = new ArrayList();
						}
						arrayList.Add(new IndexedString((string)enumerator.Key));
						arrayList.Add(stateItem.Value);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x0600237D RID: 9085 RVA: 0x00073B68 File Offset: 0x00071D68
		public void SetDirty(bool dirty)
		{
			if (this.bag.Count != 0)
			{
				foreach (object obj in this.bag.Values)
				{
					StateItem stateItem = (StateItem)obj;
					stateItem.IsDirty = dirty;
				}
			}
		}

		// Token: 0x0600237E RID: 9086 RVA: 0x00073BD4 File Offset: 0x00071DD4
		public void SetItemDirty(string key, bool dirty)
		{
			StateItem stateItem = this.bag[key] as StateItem;
			if (stateItem != null)
			{
				stateItem.IsDirty = dirty;
			}
		}

		// Token: 0x170009EF RID: 2543
		// (get) Token: 0x0600237F RID: 9087 RVA: 0x00007722 File Offset: 0x00005922
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009F0 RID: 2544
		// (get) Token: 0x06002380 RID: 9088 RVA: 0x00007722 File Offset: 0x00005922
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x06002381 RID: 9089 RVA: 0x00007722 File Offset: 0x00005922
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170009F2 RID: 2546
		// (get) Token: 0x06002382 RID: 9090 RVA: 0x00004335 File Offset: 0x00002535
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06002383 RID: 9091 RVA: 0x00073BFD File Offset: 0x00071DFD
		bool IDictionary.Contains(object key)
		{
			return this.bag.Contains((string)key);
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x00073C10 File Offset: 0x00071E10
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IDictionary)this).GetEnumerator();
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x00073C18 File Offset: 0x00071E18
		void ICollection.CopyTo(Array array, int index)
		{
			this.Values.CopyTo(array, index);
		}

		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06002386 RID: 9094 RVA: 0x00073C27 File Offset: 0x00071E27
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x00073C2F File Offset: 0x00071E2F
		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x00073C38 File Offset: 0x00071E38
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x06002389 RID: 9097 RVA: 0x00073C40 File Offset: 0x00071E40
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x04001CC0 RID: 7360
		private IDictionary bag;

		// Token: 0x04001CC1 RID: 7361
		private bool marked;

		// Token: 0x04001CC2 RID: 7362
		private bool ignoreCase;
	}
}

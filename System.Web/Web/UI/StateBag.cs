using System;
using System.Collections;
using System.Collections.Specialized;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000466 RID: 1126
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class StateBag : IStateManager, IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06003536 RID: 13622 RVA: 0x000E660E File Offset: 0x000E560E
		public StateBag() : this(false)
		{
		}

		// Token: 0x06003537 RID: 13623 RVA: 0x000E6617 File Offset: 0x000E5617
		public StateBag(bool ignoreCase)
		{
			this.marked = false;
			this.ignoreCase = ignoreCase;
			this.bag = this.CreateBag();
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06003538 RID: 13624 RVA: 0x000E6639 File Offset: 0x000E5639
		public int Count
		{
			get
			{
				return this.bag.Count;
			}
		}

		// Token: 0x17000BE8 RID: 3048
		// (get) Token: 0x06003539 RID: 13625 RVA: 0x000E6646 File Offset: 0x000E5646
		public ICollection Keys
		{
			get
			{
				return this.bag.Keys;
			}
		}

		// Token: 0x17000BE9 RID: 3049
		// (get) Token: 0x0600353A RID: 13626 RVA: 0x000E6653 File Offset: 0x000E5653
		public ICollection Values
		{
			get
			{
				return this.bag.Values;
			}
		}

		// Token: 0x17000BEA RID: 3050
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

		// Token: 0x17000BEB RID: 3051
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

		// Token: 0x0600353F RID: 13631 RVA: 0x000E66C5 File Offset: 0x000E56C5
		private IDictionary CreateBag()
		{
			return new HybridDictionary(this.ignoreCase);
		}

		// Token: 0x06003540 RID: 13632 RVA: 0x000E66D4 File Offset: 0x000E56D4
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

		// Token: 0x06003541 RID: 13633 RVA: 0x000E675D File Offset: 0x000E575D
		void IDictionary.Add(object key, object value)
		{
			this.Add((string)key, value);
		}

		// Token: 0x06003542 RID: 13634 RVA: 0x000E676D File Offset: 0x000E576D
		public void Clear()
		{
			this.bag.Clear();
		}

		// Token: 0x06003543 RID: 13635 RVA: 0x000E677A File Offset: 0x000E577A
		public IDictionaryEnumerator GetEnumerator()
		{
			return this.bag.GetEnumerator();
		}

		// Token: 0x06003544 RID: 13636 RVA: 0x000E6788 File Offset: 0x000E5788
		public bool IsItemDirty(string key)
		{
			StateItem stateItem = this.bag[key] as StateItem;
			return stateItem != null && stateItem.IsDirty;
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06003545 RID: 13637 RVA: 0x000E67B2 File Offset: 0x000E57B2
		internal bool IsTrackingViewState
		{
			get
			{
				return this.marked;
			}
		}

		// Token: 0x06003546 RID: 13638 RVA: 0x000E67BC File Offset: 0x000E57BC
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

		// Token: 0x06003547 RID: 13639 RVA: 0x000E6809 File Offset: 0x000E5809
		internal void TrackViewState()
		{
			this.marked = true;
		}

		// Token: 0x06003548 RID: 13640 RVA: 0x000E6812 File Offset: 0x000E5812
		public void Remove(string key)
		{
			this.bag.Remove(key);
		}

		// Token: 0x06003549 RID: 13641 RVA: 0x000E6820 File Offset: 0x000E5820
		void IDictionary.Remove(object key)
		{
			this.Remove((string)key);
		}

		// Token: 0x0600354A RID: 13642 RVA: 0x000E6830 File Offset: 0x000E5830
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

		// Token: 0x0600354B RID: 13643 RVA: 0x000E68A4 File Offset: 0x000E58A4
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

		// Token: 0x0600354C RID: 13644 RVA: 0x000E6910 File Offset: 0x000E5910
		public void SetItemDirty(string key, bool dirty)
		{
			StateItem stateItem = this.bag[key] as StateItem;
			if (stateItem != null)
			{
				stateItem.IsDirty = dirty;
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x0600354D RID: 13645 RVA: 0x000E6939 File Offset: 0x000E5939
		bool IDictionary.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x0600354E RID: 13646 RVA: 0x000E693C File Offset: 0x000E593C
		bool IDictionary.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x0600354F RID: 13647 RVA: 0x000E693F File Offset: 0x000E593F
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x06003550 RID: 13648 RVA: 0x000E6942 File Offset: 0x000E5942
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06003551 RID: 13649 RVA: 0x000E6945 File Offset: 0x000E5945
		bool IDictionary.Contains(object key)
		{
			return this.bag.Contains((string)key);
		}

		// Token: 0x06003552 RID: 13650 RVA: 0x000E6958 File Offset: 0x000E5958
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IDictionary)this).GetEnumerator();
		}

		// Token: 0x06003553 RID: 13651 RVA: 0x000E6960 File Offset: 0x000E5960
		void ICollection.CopyTo(Array array, int index)
		{
			this.Values.CopyTo(array, index);
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06003554 RID: 13652 RVA: 0x000E696F File Offset: 0x000E596F
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.IsTrackingViewState;
			}
		}

		// Token: 0x06003555 RID: 13653 RVA: 0x000E6977 File Offset: 0x000E5977
		void IStateManager.LoadViewState(object state)
		{
			this.LoadViewState(state);
		}

		// Token: 0x06003556 RID: 13654 RVA: 0x000E6980 File Offset: 0x000E5980
		void IStateManager.TrackViewState()
		{
			this.TrackViewState();
		}

		// Token: 0x06003557 RID: 13655 RVA: 0x000E6988 File Offset: 0x000E5988
		object IStateManager.SaveViewState()
		{
			return this.SaveViewState();
		}

		// Token: 0x0400252A RID: 9514
		private IDictionary bag;

		// Token: 0x0400252B RID: 9515
		private bool marked;

		// Token: 0x0400252C RID: 9516
		private bool ignoreCase;
	}
}

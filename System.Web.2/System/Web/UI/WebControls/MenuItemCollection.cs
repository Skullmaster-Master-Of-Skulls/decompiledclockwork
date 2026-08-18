using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000472 RID: 1138
	public sealed class MenuItemCollection : ICollection, IEnumerable, IStateManager
	{
		// Token: 0x0600382F RID: 14383 RVA: 0x000B6FB2 File Offset: 0x000B51B2
		public MenuItemCollection() : this(null)
		{
		}

		// Token: 0x06003830 RID: 14384 RVA: 0x000B6FBB File Offset: 0x000B51BB
		public MenuItemCollection(MenuItem owner)
		{
			this._owner = owner;
			this._list = new List<MenuItem>();
		}

		// Token: 0x1700107C RID: 4220
		// (get) Token: 0x06003831 RID: 14385 RVA: 0x000B6FD5 File Offset: 0x000B51D5
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x06003832 RID: 14386 RVA: 0x000B6FE2 File Offset: 0x000B51E2
		public bool IsSynchronized
		{
			get
			{
				return ((ICollection)this._list).IsSynchronized;
			}
		}

		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x06003833 RID: 14387 RVA: 0x000B6FEF File Offset: 0x000B51EF
		private List<MenuItemCollection.LogItem> Log
		{
			get
			{
				if (this._log == null)
				{
					this._log = new List<MenuItemCollection.LogItem>();
				}
				return this._log;
			}
		}

		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x06003834 RID: 14388 RVA: 0x000B700A File Offset: 0x000B520A
		public object SyncRoot
		{
			get
			{
				return ((ICollection)this._list).SyncRoot;
			}
		}

		// Token: 0x17001080 RID: 4224
		public MenuItem this[int index]
		{
			get
			{
				return this._list[index];
			}
		}

		// Token: 0x06003836 RID: 14390 RVA: 0x000B7025 File Offset: 0x000B5225
		public void Add(MenuItem child)
		{
			this.AddAt(this._list.Count, child);
		}

		// Token: 0x06003837 RID: 14391 RVA: 0x000B703C File Offset: 0x000B523C
		public void AddAt(int index, MenuItem child)
		{
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			if (child.Owner != null && child.Parent == null)
			{
				child.Owner.Items.Remove(child);
			}
			if (child.Parent != null)
			{
				child.Parent.ChildItems.Remove(child);
			}
			if (this._owner != null)
			{
				child.SetParent(this._owner);
				child.SetOwner(this._owner.Owner);
			}
			this._list.Insert(index, child);
			this._version++;
			if (this._isTrackingViewState)
			{
				((IStateManager)child).TrackViewState();
				child.SetDirty();
			}
			this.Log.Add(new MenuItemCollection.LogItem(MenuItemCollection.LogItemType.Insert, index, this._isTrackingViewState));
		}

		// Token: 0x06003838 RID: 14392 RVA: 0x000B7100 File Offset: 0x000B5300
		public void Clear()
		{
			if (this.Count == 0)
			{
				return;
			}
			if (this._owner != null)
			{
				Menu owner = this._owner.Owner;
				if (owner != null)
				{
					for (MenuItem menuItem = owner.SelectedItem; menuItem != null; menuItem = menuItem.Parent)
					{
						if (this.Contains(menuItem))
						{
							owner.SetSelectedItem(null);
							break;
						}
					}
				}
			}
			foreach (MenuItem menuItem2 in this._list)
			{
				menuItem2.SetParent(null);
			}
			this._list.Clear();
			this._version++;
			if (this._isTrackingViewState)
			{
				this.Log.Clear();
			}
			this.Log.Add(new MenuItemCollection.LogItem(MenuItemCollection.LogItemType.Clear, 0, this._isTrackingViewState));
		}

		// Token: 0x06003839 RID: 14393 RVA: 0x000B71DC File Offset: 0x000B53DC
		public void CopyTo(Array array, int index)
		{
			if (!(array is MenuItem[]))
			{
				throw new ArgumentException(SR.GetString("MenuItemCollection_InvalidArrayType"), "array");
			}
			this._list.CopyTo((MenuItem[])array, index);
		}

		// Token: 0x0600383A RID: 14394 RVA: 0x000B720D File Offset: 0x000B540D
		public void CopyTo(MenuItem[] array, int index)
		{
			this._list.CopyTo(array, index);
		}

		// Token: 0x0600383B RID: 14395 RVA: 0x000B721C File Offset: 0x000B541C
		public bool Contains(MenuItem c)
		{
			return this._list.Contains(c);
		}

		// Token: 0x0600383C RID: 14396 RVA: 0x000B722C File Offset: 0x000B542C
		internal MenuItem FindItem(string[] path, int pos)
		{
			if (pos == path.Length)
			{
				return this._owner;
			}
			string b = TreeView.UnEscape(path[pos]);
			for (int i = 0; i < this.Count; i++)
			{
				MenuItem menuItem = this._list[i];
				if (menuItem.Value == b)
				{
					return menuItem.ChildItems.FindItem(path, pos + 1);
				}
			}
			return null;
		}

		// Token: 0x0600383D RID: 14397 RVA: 0x000B728C File Offset: 0x000B548C
		public IEnumerator GetEnumerator()
		{
			return new MenuItemCollection.MenuItemCollectionEnumerator(this);
		}

		// Token: 0x0600383E RID: 14398 RVA: 0x000B7294 File Offset: 0x000B5494
		public int IndexOf(MenuItem value)
		{
			return this._list.IndexOf(value);
		}

		// Token: 0x0600383F RID: 14399 RVA: 0x000B72A4 File Offset: 0x000B54A4
		public void Remove(MenuItem value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			int num = this._list.IndexOf(value);
			if (num != -1)
			{
				this.RemoveAt(num);
			}
		}

		// Token: 0x06003840 RID: 14400 RVA: 0x000B72D8 File Offset: 0x000B54D8
		public void RemoveAt(int index)
		{
			MenuItem menuItem = this._list[index];
			Menu owner = menuItem.Owner;
			if (owner != null)
			{
				for (MenuItem menuItem2 = owner.SelectedItem; menuItem2 != null; menuItem2 = menuItem2.Parent)
				{
					if (menuItem2 == menuItem)
					{
						owner.SetSelectedItem(null);
						break;
					}
				}
			}
			menuItem.SetParent(null);
			this._list.RemoveAt(index);
			this._version++;
			this.Log.Add(new MenuItemCollection.LogItem(MenuItemCollection.LogItemType.Remove, index, this._isTrackingViewState));
		}

		// Token: 0x06003841 RID: 14401 RVA: 0x000B7358 File Offset: 0x000B5558
		internal void SetDirty()
		{
			foreach (MenuItemCollection.LogItem logItem in this.Log)
			{
				logItem.Tracked = true;
			}
			for (int i = 0; i < this.Count; i++)
			{
				this[i].SetDirty();
			}
		}

		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x06003842 RID: 14402 RVA: 0x000B73C8 File Offset: 0x000B55C8
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x06003843 RID: 14403 RVA: 0x000B73D0 File Offset: 0x000B55D0
		void IStateManager.LoadViewState(object state)
		{
			object[] array = (object[])state;
			if (array != null)
			{
				if (array[0] != null)
				{
					string text = (string)array[0];
					string[] array2 = text.Split(new char[]
					{
						','
					});
					for (int i = 0; i < array2.Length; i++)
					{
						string[] array3 = array2[i].Split(new char[]
						{
							':'
						});
						MenuItemCollection.LogItemType logItemType = (MenuItemCollection.LogItemType)int.Parse(array3[0], CultureInfo.InvariantCulture);
						int index = int.Parse(array3[1], CultureInfo.InvariantCulture);
						if (logItemType == MenuItemCollection.LogItemType.Insert)
						{
							this.AddAt(index, new MenuItem());
						}
						else if (logItemType == MenuItemCollection.LogItemType.Remove)
						{
							this.RemoveAt(index);
						}
						else if (logItemType == MenuItemCollection.LogItemType.Clear)
						{
							this.Clear();
						}
					}
				}
				for (int j = 0; j < array.Length - 1; j++)
				{
					if (array[j + 1] != null && this[j] != null)
					{
						((IStateManager)this[j]).LoadViewState(array[j + 1]);
					}
				}
			}
		}

		// Token: 0x06003844 RID: 14404 RVA: 0x000B74B8 File Offset: 0x000B56B8
		object IStateManager.SaveViewState()
		{
			object[] array = new object[this.Count + 1];
			bool flag = false;
			if (this._log != null && this._log.Count > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				int num = 0;
				for (int i = 0; i < this._log.Count; i++)
				{
					MenuItemCollection.LogItem logItem = this._log[i];
					if (logItem.Tracked)
					{
						stringBuilder.Append((int)logItem.Type);
						stringBuilder.Append(":");
						stringBuilder.Append(logItem.Index);
						if (i < this._log.Count - 1)
						{
							stringBuilder.Append(",");
						}
						num++;
					}
				}
				if (num > 0)
				{
					array[0] = stringBuilder.ToString();
					flag = true;
				}
			}
			for (int j = 0; j < this.Count; j++)
			{
				array[j + 1] = ((IStateManager)this[j]).SaveViewState();
				if (array[j + 1] != null)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return null;
			}
			return array;
		}

		// Token: 0x06003845 RID: 14405 RVA: 0x000B75BC File Offset: 0x000B57BC
		void IStateManager.TrackViewState()
		{
			this._isTrackingViewState = true;
			for (int i = 0; i < this.Count; i++)
			{
				((IStateManager)this[i]).TrackViewState();
			}
		}

		// Token: 0x04002276 RID: 8822
		private List<MenuItem> _list;

		// Token: 0x04002277 RID: 8823
		private MenuItem _owner;

		// Token: 0x04002278 RID: 8824
		private int _version;

		// Token: 0x04002279 RID: 8825
		private bool _isTrackingViewState;

		// Token: 0x0400227A RID: 8826
		private List<MenuItemCollection.LogItem> _log;

		// Token: 0x020009AD RID: 2477
		private class LogItem
		{
			// Token: 0x06006BE0 RID: 27616 RVA: 0x00182251 File Offset: 0x00180451
			public LogItem(MenuItemCollection.LogItemType type, int index, bool tracked)
			{
				this._type = type;
				this._index = index;
				this._tracked = tracked;
			}

			// Token: 0x17001DBE RID: 7614
			// (get) Token: 0x06006BE1 RID: 27617 RVA: 0x0018226E File Offset: 0x0018046E
			public int Index
			{
				get
				{
					return this._index;
				}
			}

			// Token: 0x17001DBF RID: 7615
			// (get) Token: 0x06006BE2 RID: 27618 RVA: 0x00182276 File Offset: 0x00180476
			// (set) Token: 0x06006BE3 RID: 27619 RVA: 0x0018227E File Offset: 0x0018047E
			public bool Tracked
			{
				get
				{
					return this._tracked;
				}
				set
				{
					this._tracked = value;
				}
			}

			// Token: 0x17001DC0 RID: 7616
			// (get) Token: 0x06006BE4 RID: 27620 RVA: 0x00182287 File Offset: 0x00180487
			public MenuItemCollection.LogItemType Type
			{
				get
				{
					return this._type;
				}
			}

			// Token: 0x04003956 RID: 14678
			private MenuItemCollection.LogItemType _type;

			// Token: 0x04003957 RID: 14679
			private int _index;

			// Token: 0x04003958 RID: 14680
			private bool _tracked;
		}

		// Token: 0x020009AE RID: 2478
		private enum LogItemType
		{
			// Token: 0x0400395A RID: 14682
			Insert,
			// Token: 0x0400395B RID: 14683
			Remove,
			// Token: 0x0400395C RID: 14684
			Clear
		}

		// Token: 0x020009AF RID: 2479
		private class MenuItemCollectionEnumerator : IEnumerator
		{
			// Token: 0x06006BE5 RID: 27621 RVA: 0x0018228F File Offset: 0x0018048F
			internal MenuItemCollectionEnumerator(MenuItemCollection list)
			{
				this.list = list;
				this.index = -1;
				this.version = list._version;
			}

			// Token: 0x06006BE6 RID: 27622 RVA: 0x001822B4 File Offset: 0x001804B4
			public bool MoveNext()
			{
				if (this.version != this.list._version)
				{
					throw new InvalidOperationException(SR.GetString("ListEnumVersionMismatch"));
				}
				if (this.index < this.list.Count - 1)
				{
					this.index++;
					this.currentElement = this.list[this.index];
					return true;
				}
				this.index = this.list.Count;
				return false;
			}

			// Token: 0x17001DC1 RID: 7617
			// (get) Token: 0x06006BE7 RID: 27623 RVA: 0x00182332 File Offset: 0x00180532
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x17001DC2 RID: 7618
			// (get) Token: 0x06006BE8 RID: 27624 RVA: 0x0018233C File Offset: 0x0018053C
			public MenuItem Current
			{
				get
				{
					if (this.index == -1)
					{
						throw new InvalidOperationException(SR.GetString("ListEnumCurrentOutOfRange"));
					}
					if (this.index >= this.list.Count)
					{
						throw new InvalidOperationException(SR.GetString("ListEnumCurrentOutOfRange"));
					}
					return this.currentElement;
				}
			}

			// Token: 0x06006BE9 RID: 27625 RVA: 0x0018238B File Offset: 0x0018058B
			public void Reset()
			{
				if (this.version != this.list._version)
				{
					throw new InvalidOperationException(SR.GetString("ListEnumVersionMismatch"));
				}
				this.currentElement = null;
				this.index = -1;
			}

			// Token: 0x0400395D RID: 14685
			private MenuItemCollection list;

			// Token: 0x0400395E RID: 14686
			private int index;

			// Token: 0x0400395F RID: 14687
			private int version;

			// Token: 0x04003960 RID: 14688
			private MenuItem currentElement;
		}
	}
}

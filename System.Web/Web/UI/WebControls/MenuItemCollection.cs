using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Permissions;
using System.Text;

namespace System.Web.UI.WebControls
{
	// Token: 0x020005E6 RID: 1510
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class MenuItemCollection : ICollection, IEnumerable, IStateManager
	{
		// Token: 0x06004AD2 RID: 19154 RVA: 0x00131933 File Offset: 0x00130933
		public MenuItemCollection() : this(null)
		{
		}

		// Token: 0x06004AD3 RID: 19155 RVA: 0x0013193C File Offset: 0x0013093C
		public MenuItemCollection(MenuItem owner)
		{
			this._owner = owner;
			this._list = new List<MenuItem>();
		}

		// Token: 0x170012BD RID: 4797
		// (get) Token: 0x06004AD4 RID: 19156 RVA: 0x00131956 File Offset: 0x00130956
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x170012BE RID: 4798
		// (get) Token: 0x06004AD5 RID: 19157 RVA: 0x00131963 File Offset: 0x00130963
		public bool IsSynchronized
		{
			get
			{
				return ((ICollection)this._list).IsSynchronized;
			}
		}

		// Token: 0x170012BF RID: 4799
		// (get) Token: 0x06004AD6 RID: 19158 RVA: 0x00131970 File Offset: 0x00130970
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

		// Token: 0x170012C0 RID: 4800
		// (get) Token: 0x06004AD7 RID: 19159 RVA: 0x0013198B File Offset: 0x0013098B
		public object SyncRoot
		{
			get
			{
				return ((ICollection)this._list).SyncRoot;
			}
		}

		// Token: 0x170012C1 RID: 4801
		public MenuItem this[int index]
		{
			get
			{
				return this._list[index];
			}
		}

		// Token: 0x06004AD9 RID: 19161 RVA: 0x001319A6 File Offset: 0x001309A6
		public void Add(MenuItem child)
		{
			this.AddAt(this._list.Count, child);
		}

		// Token: 0x06004ADA RID: 19162 RVA: 0x001319BC File Offset: 0x001309BC
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

		// Token: 0x06004ADB RID: 19163 RVA: 0x00131A80 File Offset: 0x00130A80
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

		// Token: 0x06004ADC RID: 19164 RVA: 0x00131B5C File Offset: 0x00130B5C
		public void CopyTo(Array array, int index)
		{
			if (!(array is MenuItem[]))
			{
				throw new ArgumentException(SR.GetString("MenuItemCollection_InvalidArrayType"), "array");
			}
			this._list.CopyTo((MenuItem[])array, index);
		}

		// Token: 0x06004ADD RID: 19165 RVA: 0x00131B8D File Offset: 0x00130B8D
		public void CopyTo(MenuItem[] array, int index)
		{
			this._list.CopyTo(array, index);
		}

		// Token: 0x06004ADE RID: 19166 RVA: 0x00131B9C File Offset: 0x00130B9C
		public bool Contains(MenuItem c)
		{
			return this._list.Contains(c);
		}

		// Token: 0x06004ADF RID: 19167 RVA: 0x00131BAC File Offset: 0x00130BAC
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

		// Token: 0x06004AE0 RID: 19168 RVA: 0x00131C0C File Offset: 0x00130C0C
		public IEnumerator GetEnumerator()
		{
			return new MenuItemCollection.MenuItemCollectionEnumerator(this);
		}

		// Token: 0x06004AE1 RID: 19169 RVA: 0x00131C14 File Offset: 0x00130C14
		public int IndexOf(MenuItem value)
		{
			return this._list.IndexOf(value);
		}

		// Token: 0x06004AE2 RID: 19170 RVA: 0x00131C24 File Offset: 0x00130C24
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

		// Token: 0x06004AE3 RID: 19171 RVA: 0x00131C58 File Offset: 0x00130C58
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

		// Token: 0x06004AE4 RID: 19172 RVA: 0x00131CD8 File Offset: 0x00130CD8
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

		// Token: 0x170012C2 RID: 4802
		// (get) Token: 0x06004AE5 RID: 19173 RVA: 0x00131D48 File Offset: 0x00130D48
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x06004AE6 RID: 19174 RVA: 0x00131D50 File Offset: 0x00130D50
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

		// Token: 0x06004AE7 RID: 19175 RVA: 0x00131E40 File Offset: 0x00130E40
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

		// Token: 0x06004AE8 RID: 19176 RVA: 0x00131F44 File Offset: 0x00130F44
		void IStateManager.TrackViewState()
		{
			this._isTrackingViewState = true;
			for (int i = 0; i < this.Count; i++)
			{
				((IStateManager)this[i]).TrackViewState();
			}
		}

		// Token: 0x04002B83 RID: 11139
		private List<MenuItem> _list;

		// Token: 0x04002B84 RID: 11140
		private MenuItem _owner;

		// Token: 0x04002B85 RID: 11141
		private int _version;

		// Token: 0x04002B86 RID: 11142
		private bool _isTrackingViewState;

		// Token: 0x04002B87 RID: 11143
		private List<MenuItemCollection.LogItem> _log;

		// Token: 0x020005E7 RID: 1511
		private class LogItem
		{
			// Token: 0x06004AE9 RID: 19177 RVA: 0x00131F75 File Offset: 0x00130F75
			public LogItem(MenuItemCollection.LogItemType type, int index, bool tracked)
			{
				this._type = type;
				this._index = index;
				this._tracked = tracked;
			}

			// Token: 0x170012C3 RID: 4803
			// (get) Token: 0x06004AEA RID: 19178 RVA: 0x00131F92 File Offset: 0x00130F92
			public int Index
			{
				get
				{
					return this._index;
				}
			}

			// Token: 0x170012C4 RID: 4804
			// (get) Token: 0x06004AEB RID: 19179 RVA: 0x00131F9A File Offset: 0x00130F9A
			// (set) Token: 0x06004AEC RID: 19180 RVA: 0x00131FA2 File Offset: 0x00130FA2
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

			// Token: 0x170012C5 RID: 4805
			// (get) Token: 0x06004AED RID: 19181 RVA: 0x00131FAB File Offset: 0x00130FAB
			public MenuItemCollection.LogItemType Type
			{
				get
				{
					return this._type;
				}
			}

			// Token: 0x04002B88 RID: 11144
			private MenuItemCollection.LogItemType _type;

			// Token: 0x04002B89 RID: 11145
			private int _index;

			// Token: 0x04002B8A RID: 11146
			private bool _tracked;
		}

		// Token: 0x020005E8 RID: 1512
		private enum LogItemType
		{
			// Token: 0x04002B8C RID: 11148
			Insert,
			// Token: 0x04002B8D RID: 11149
			Remove,
			// Token: 0x04002B8E RID: 11150
			Clear
		}

		// Token: 0x020005E9 RID: 1513
		private class MenuItemCollectionEnumerator : IEnumerator
		{
			// Token: 0x06004AEE RID: 19182 RVA: 0x00131FB3 File Offset: 0x00130FB3
			internal MenuItemCollectionEnumerator(MenuItemCollection list)
			{
				this.list = list;
				this.index = -1;
				this.version = list._version;
			}

			// Token: 0x06004AEF RID: 19183 RVA: 0x00131FD8 File Offset: 0x00130FD8
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

			// Token: 0x170012C6 RID: 4806
			// (get) Token: 0x06004AF0 RID: 19184 RVA: 0x00132056 File Offset: 0x00131056
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x170012C7 RID: 4807
			// (get) Token: 0x06004AF1 RID: 19185 RVA: 0x00132060 File Offset: 0x00131060
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

			// Token: 0x06004AF2 RID: 19186 RVA: 0x001320AF File Offset: 0x001310AF
			public void Reset()
			{
				if (this.version != this.list._version)
				{
					throw new InvalidOperationException(SR.GetString("ListEnumVersionMismatch"));
				}
				this.currentElement = null;
				this.index = -1;
			}

			// Token: 0x04002B8F RID: 11151
			private MenuItemCollection list;

			// Token: 0x04002B90 RID: 11152
			private int index;

			// Token: 0x04002B91 RID: 11153
			private int version;

			// Token: 0x04002B92 RID: 11154
			private MenuItem currentElement;
		}
	}
}

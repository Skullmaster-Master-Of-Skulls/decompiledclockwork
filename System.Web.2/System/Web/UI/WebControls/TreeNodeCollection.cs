using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004FD RID: 1277
	public sealed class TreeNodeCollection : ICollection, IEnumerable, IStateManager
	{
		// Token: 0x06004000 RID: 16384 RVA: 0x000CEA46 File Offset: 0x000CCC46
		public TreeNodeCollection() : this(null, true)
		{
		}

		// Token: 0x06004001 RID: 16385 RVA: 0x000CEA50 File Offset: 0x000CCC50
		public TreeNodeCollection(TreeNode owner) : this(owner, true)
		{
		}

		// Token: 0x06004002 RID: 16386 RVA: 0x000CEA5A File Offset: 0x000CCC5A
		internal TreeNodeCollection(TreeNode owner, bool updateParent)
		{
			this._owner = owner;
			this._list = new List<TreeNode>();
			this._updateParent = updateParent;
		}

		// Token: 0x170012B7 RID: 4791
		// (get) Token: 0x06004003 RID: 16387 RVA: 0x000CEA7B File Offset: 0x000CCC7B
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x170012B8 RID: 4792
		// (get) Token: 0x06004004 RID: 16388 RVA: 0x000CEA88 File Offset: 0x000CCC88
		public bool IsSynchronized
		{
			get
			{
				return ((ICollection)this._list).IsSynchronized;
			}
		}

		// Token: 0x170012B9 RID: 4793
		// (get) Token: 0x06004005 RID: 16389 RVA: 0x000CEA95 File Offset: 0x000CCC95
		private List<TreeNodeCollection.LogItem> Log
		{
			get
			{
				if (this._log == null)
				{
					this._log = new List<TreeNodeCollection.LogItem>();
				}
				return this._log;
			}
		}

		// Token: 0x170012BA RID: 4794
		// (get) Token: 0x06004006 RID: 16390 RVA: 0x000CEAB0 File Offset: 0x000CCCB0
		public object SyncRoot
		{
			get
			{
				return ((ICollection)this._list).SyncRoot;
			}
		}

		// Token: 0x170012BB RID: 4795
		public TreeNode this[int index]
		{
			get
			{
				return this._list[index];
			}
		}

		// Token: 0x06004008 RID: 16392 RVA: 0x000CEACB File Offset: 0x000CCCCB
		public void Add(TreeNode child)
		{
			this.AddAt(this.Count, child);
		}

		// Token: 0x06004009 RID: 16393 RVA: 0x000CEADC File Offset: 0x000CCCDC
		public void AddAt(int index, TreeNode child)
		{
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			if (this._updateParent)
			{
				if (child.Owner != null && child.Parent == null)
				{
					child.Owner.Nodes.Remove(child);
				}
				if (child.Parent != null)
				{
					child.Parent.ChildNodes.Remove(child);
				}
				if (this._owner != null)
				{
					child.SetParent(this._owner);
					child.SetOwner(this._owner.Owner);
				}
			}
			this._list.Insert(index, child);
			this._version++;
			if (this._isTrackingViewState)
			{
				((IStateManager)child).TrackViewState();
				child.SetDirty();
			}
			this.Log.Add(new TreeNodeCollection.LogItem(TreeNodeCollection.LogItemType.Insert, index, this._isTrackingViewState));
		}

		// Token: 0x0600400A RID: 16394 RVA: 0x000CEBA8 File Offset: 0x000CCDA8
		public void Clear()
		{
			if (this.Count == 0)
			{
				return;
			}
			if (this._owner != null)
			{
				TreeView owner = this._owner.Owner;
				if (owner != null)
				{
					if (owner.CheckedNodes.Count != 0)
					{
						owner.CheckedNodes.Clear();
					}
					for (TreeNode treeNode = owner.SelectedNode; treeNode != null; treeNode = treeNode.Parent)
					{
						if (this.Contains(treeNode))
						{
							owner.SetSelectedNode(null);
							break;
						}
					}
				}
			}
			foreach (TreeNode treeNode2 in this._list)
			{
				treeNode2.SetParent(null);
			}
			this._list.Clear();
			this._version++;
			if (this._isTrackingViewState)
			{
				this.Log.Clear();
			}
			this.Log.Add(new TreeNodeCollection.LogItem(TreeNodeCollection.LogItemType.Clear, 0, this._isTrackingViewState));
		}

		// Token: 0x0600400B RID: 16395 RVA: 0x00095DD9 File Offset: 0x00093FD9
		public void CopyTo(TreeNode[] nodeArray, int index)
		{
			((ICollection)this).CopyTo(nodeArray, index);
		}

		// Token: 0x0600400C RID: 16396 RVA: 0x000CEC9C File Offset: 0x000CCE9C
		public bool Contains(TreeNode c)
		{
			return this._list.Contains(c);
		}

		// Token: 0x0600400D RID: 16397 RVA: 0x000CECAC File Offset: 0x000CCEAC
		internal TreeNode FindNode(string[] path, int pos)
		{
			if (pos == path.Length)
			{
				return this._owner;
			}
			string b = TreeView.UnEscape(path[pos]);
			for (int i = 0; i < this.Count; i++)
			{
				TreeNode treeNode = this[i];
				if (treeNode.Value == b)
				{
					return treeNode.ChildNodes.FindNode(path, pos + 1);
				}
			}
			return null;
		}

		// Token: 0x0600400E RID: 16398 RVA: 0x000CED07 File Offset: 0x000CCF07
		public IEnumerator GetEnumerator()
		{
			return new TreeNodeCollection.TreeNodeCollectionEnumerator(this);
		}

		// Token: 0x0600400F RID: 16399 RVA: 0x000CED0F File Offset: 0x000CCF0F
		public int IndexOf(TreeNode value)
		{
			return this._list.IndexOf(value);
		}

		// Token: 0x06004010 RID: 16400 RVA: 0x000CED20 File Offset: 0x000CCF20
		public void Remove(TreeNode value)
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

		// Token: 0x06004011 RID: 16401 RVA: 0x000CED54 File Offset: 0x000CCF54
		public void RemoveAt(int index)
		{
			TreeNode treeNode = this._list[index];
			if (this._updateParent)
			{
				TreeView owner = treeNode.Owner;
				if (owner != null)
				{
					if (owner.CheckedNodes.Count != 0)
					{
						TreeNodeCollection.UnCheckUnSelectRecursive(treeNode);
					}
					else
					{
						for (TreeNode treeNode2 = owner.SelectedNode; treeNode2 != null; treeNode2 = treeNode2.Parent)
						{
							if (treeNode2 == treeNode)
							{
								owner.SetSelectedNode(null);
								break;
							}
						}
					}
				}
				treeNode.SetParent(null);
			}
			this._list.RemoveAt(index);
			this._version++;
			this.Log.Add(new TreeNodeCollection.LogItem(TreeNodeCollection.LogItemType.Remove, index, this._isTrackingViewState));
		}

		// Token: 0x06004012 RID: 16402 RVA: 0x000CEDF0 File Offset: 0x000CCFF0
		internal void SetDirty()
		{
			foreach (TreeNodeCollection.LogItem logItem in this.Log)
			{
				logItem.Tracked = true;
			}
			for (int i = 0; i < this.Count; i++)
			{
				this[i].SetDirty();
			}
		}

		// Token: 0x06004013 RID: 16403 RVA: 0x000CEE60 File Offset: 0x000CD060
		private static void UnCheckUnSelectRecursive(TreeNode node)
		{
			TreeNodeCollection checkedNodes = node.Owner.CheckedNodes;
			if (node.Checked)
			{
				checkedNodes.Remove(node);
			}
			TreeNode treeNode = node.Owner.SelectedNode;
			if (node == treeNode)
			{
				node.Owner.SetSelectedNode(null);
				treeNode = null;
			}
			if (treeNode != null || checkedNodes.Count != 0)
			{
				foreach (object obj in node.ChildNodes)
				{
					TreeNode node2 = (TreeNode)obj;
					TreeNodeCollection.UnCheckUnSelectRecursive(node2);
				}
			}
		}

		// Token: 0x06004014 RID: 16404 RVA: 0x000CEF00 File Offset: 0x000CD100
		void ICollection.CopyTo(Array array, int index)
		{
			if (!(array is TreeNode[]))
			{
				throw new ArgumentException(SR.GetString("TreeNodeCollection_InvalidArrayType"), "array");
			}
			this._list.CopyTo((TreeNode[])array, index);
		}

		// Token: 0x170012BC RID: 4796
		// (get) Token: 0x06004015 RID: 16405 RVA: 0x000CEF31 File Offset: 0x000CD131
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x06004016 RID: 16406 RVA: 0x000CEF3C File Offset: 0x000CD13C
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
						TreeNodeCollection.LogItemType logItemType = (TreeNodeCollection.LogItemType)int.Parse(array3[0], CultureInfo.InvariantCulture);
						int index = int.Parse(array3[1], CultureInfo.InvariantCulture);
						if (logItemType == TreeNodeCollection.LogItemType.Insert)
						{
							if (this._owner != null && this._owner.Owner != null)
							{
								this.AddAt(index, this._owner.Owner.CreateNode());
							}
							else
							{
								this.AddAt(index, new TreeNode());
							}
						}
						else if (logItemType == TreeNodeCollection.LogItemType.Remove)
						{
							this.RemoveAt(index);
						}
						else if (logItemType == TreeNodeCollection.LogItemType.Clear)
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

		// Token: 0x06004017 RID: 16407 RVA: 0x000CF058 File Offset: 0x000CD258
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
					TreeNodeCollection.LogItem logItem = this._log[i];
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

		// Token: 0x06004018 RID: 16408 RVA: 0x000CF15C File Offset: 0x000CD35C
		void IStateManager.TrackViewState()
		{
			this._isTrackingViewState = true;
			for (int i = 0; i < this.Count; i++)
			{
				((IStateManager)this[i]).TrackViewState();
			}
		}

		// Token: 0x04002466 RID: 9318
		private List<TreeNode> _list;

		// Token: 0x04002467 RID: 9319
		private TreeNode _owner;

		// Token: 0x04002468 RID: 9320
		private bool _updateParent;

		// Token: 0x04002469 RID: 9321
		private int _version;

		// Token: 0x0400246A RID: 9322
		private bool _isTrackingViewState;

		// Token: 0x0400246B RID: 9323
		private List<TreeNodeCollection.LogItem> _log;

		// Token: 0x020009C9 RID: 2505
		private class LogItem
		{
			// Token: 0x06006C66 RID: 27750 RVA: 0x00183C9B File Offset: 0x00181E9B
			public LogItem(TreeNodeCollection.LogItemType type, int index, bool tracked)
			{
				this._type = type;
				this._index = index;
				this._tracked = tracked;
			}

			// Token: 0x17001DE5 RID: 7653
			// (get) Token: 0x06006C67 RID: 27751 RVA: 0x00183CB8 File Offset: 0x00181EB8
			public int Index
			{
				get
				{
					return this._index;
				}
			}

			// Token: 0x17001DE6 RID: 7654
			// (get) Token: 0x06006C68 RID: 27752 RVA: 0x00183CC0 File Offset: 0x00181EC0
			// (set) Token: 0x06006C69 RID: 27753 RVA: 0x00183CC8 File Offset: 0x00181EC8
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

			// Token: 0x17001DE7 RID: 7655
			// (get) Token: 0x06006C6A RID: 27754 RVA: 0x00183CD1 File Offset: 0x00181ED1
			public TreeNodeCollection.LogItemType Type
			{
				get
				{
					return this._type;
				}
			}

			// Token: 0x040039B2 RID: 14770
			private TreeNodeCollection.LogItemType _type;

			// Token: 0x040039B3 RID: 14771
			private int _index;

			// Token: 0x040039B4 RID: 14772
			private bool _tracked;
		}

		// Token: 0x020009CA RID: 2506
		private enum LogItemType
		{
			// Token: 0x040039B6 RID: 14774
			Insert,
			// Token: 0x040039B7 RID: 14775
			Remove,
			// Token: 0x040039B8 RID: 14776
			Clear
		}

		// Token: 0x020009CB RID: 2507
		private class TreeNodeCollectionEnumerator : IEnumerator
		{
			// Token: 0x06006C6B RID: 27755 RVA: 0x00183CD9 File Offset: 0x00181ED9
			internal TreeNodeCollectionEnumerator(TreeNodeCollection list)
			{
				this.list = list;
				this.index = -1;
				this.version = list._version;
			}

			// Token: 0x06006C6C RID: 27756 RVA: 0x00183CFC File Offset: 0x00181EFC
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

			// Token: 0x17001DE8 RID: 7656
			// (get) Token: 0x06006C6D RID: 27757 RVA: 0x00183D7A File Offset: 0x00181F7A
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x17001DE9 RID: 7657
			// (get) Token: 0x06006C6E RID: 27758 RVA: 0x00183D84 File Offset: 0x00181F84
			public TreeNode Current
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

			// Token: 0x06006C6F RID: 27759 RVA: 0x00183DD3 File Offset: 0x00181FD3
			public void Reset()
			{
				if (this.version != this.list._version)
				{
					throw new InvalidOperationException(SR.GetString("ListEnumVersionMismatch"));
				}
				this.currentElement = null;
				this.index = -1;
			}

			// Token: 0x040039B9 RID: 14777
			private TreeNodeCollection list;

			// Token: 0x040039BA RID: 14778
			private int index;

			// Token: 0x040039BB RID: 14779
			private int version;

			// Token: 0x040039BC RID: 14780
			private TreeNode currentElement;
		}
	}
}

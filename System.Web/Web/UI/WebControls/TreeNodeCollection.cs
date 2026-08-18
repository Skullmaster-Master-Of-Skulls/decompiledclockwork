using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Permissions;
using System.Text;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200066B RID: 1643
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class TreeNodeCollection : ICollection, IEnumerable, IStateManager
	{
		// Token: 0x060050B0 RID: 20656 RVA: 0x00144307 File Offset: 0x00143307
		public TreeNodeCollection() : this(null, true)
		{
		}

		// Token: 0x060050B1 RID: 20657 RVA: 0x00144311 File Offset: 0x00143311
		public TreeNodeCollection(TreeNode owner) : this(owner, true)
		{
		}

		// Token: 0x060050B2 RID: 20658 RVA: 0x0014431B File Offset: 0x0014331B
		internal TreeNodeCollection(TreeNode owner, bool updateParent)
		{
			this._owner = owner;
			this._list = new List<TreeNode>();
			this._updateParent = updateParent;
		}

		// Token: 0x1700147B RID: 5243
		// (get) Token: 0x060050B3 RID: 20659 RVA: 0x0014433C File Offset: 0x0014333C
		public int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x1700147C RID: 5244
		// (get) Token: 0x060050B4 RID: 20660 RVA: 0x00144349 File Offset: 0x00143349
		public bool IsSynchronized
		{
			get
			{
				return ((ICollection)this._list).IsSynchronized;
			}
		}

		// Token: 0x1700147D RID: 5245
		// (get) Token: 0x060050B5 RID: 20661 RVA: 0x00144356 File Offset: 0x00143356
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

		// Token: 0x1700147E RID: 5246
		// (get) Token: 0x060050B6 RID: 20662 RVA: 0x00144371 File Offset: 0x00143371
		public object SyncRoot
		{
			get
			{
				return ((ICollection)this._list).SyncRoot;
			}
		}

		// Token: 0x1700147F RID: 5247
		public TreeNode this[int index]
		{
			get
			{
				return this._list[index];
			}
		}

		// Token: 0x060050B8 RID: 20664 RVA: 0x0014438C File Offset: 0x0014338C
		public void Add(TreeNode child)
		{
			this.AddAt(this.Count, child);
		}

		// Token: 0x060050B9 RID: 20665 RVA: 0x0014439C File Offset: 0x0014339C
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

		// Token: 0x060050BA RID: 20666 RVA: 0x00144468 File Offset: 0x00143468
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

		// Token: 0x060050BB RID: 20667 RVA: 0x0014455C File Offset: 0x0014355C
		public void CopyTo(TreeNode[] nodeArray, int index)
		{
			((ICollection)this).CopyTo(nodeArray, index);
		}

		// Token: 0x060050BC RID: 20668 RVA: 0x00144566 File Offset: 0x00143566
		public bool Contains(TreeNode c)
		{
			return this._list.Contains(c);
		}

		// Token: 0x060050BD RID: 20669 RVA: 0x00144574 File Offset: 0x00143574
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

		// Token: 0x060050BE RID: 20670 RVA: 0x001445CF File Offset: 0x001435CF
		public IEnumerator GetEnumerator()
		{
			return new TreeNodeCollection.TreeNodeCollectionEnumerator(this);
		}

		// Token: 0x060050BF RID: 20671 RVA: 0x001445D7 File Offset: 0x001435D7
		public int IndexOf(TreeNode value)
		{
			return this._list.IndexOf(value);
		}

		// Token: 0x060050C0 RID: 20672 RVA: 0x001445E8 File Offset: 0x001435E8
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

		// Token: 0x060050C1 RID: 20673 RVA: 0x0014461C File Offset: 0x0014361C
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

		// Token: 0x060050C2 RID: 20674 RVA: 0x001446B8 File Offset: 0x001436B8
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

		// Token: 0x060050C3 RID: 20675 RVA: 0x00144728 File Offset: 0x00143728
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

		// Token: 0x060050C4 RID: 20676 RVA: 0x001447C8 File Offset: 0x001437C8
		void ICollection.CopyTo(Array array, int index)
		{
			if (!(array is TreeNode[]))
			{
				throw new ArgumentException(SR.GetString("TreeNodeCollection_InvalidArrayType"), "array");
			}
			this._list.CopyTo((TreeNode[])array, index);
		}

		// Token: 0x17001480 RID: 5248
		// (get) Token: 0x060050C5 RID: 20677 RVA: 0x001447F9 File Offset: 0x001437F9
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this._isTrackingViewState;
			}
		}

		// Token: 0x060050C6 RID: 20678 RVA: 0x00144804 File Offset: 0x00143804
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

		// Token: 0x060050C7 RID: 20679 RVA: 0x0014492C File Offset: 0x0014392C
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

		// Token: 0x060050C8 RID: 20680 RVA: 0x00144A30 File Offset: 0x00143A30
		void IStateManager.TrackViewState()
		{
			this._isTrackingViewState = true;
			for (int i = 0; i < this.Count; i++)
			{
				((IStateManager)this[i]).TrackViewState();
			}
		}

		// Token: 0x04002D26 RID: 11558
		private List<TreeNode> _list;

		// Token: 0x04002D27 RID: 11559
		private TreeNode _owner;

		// Token: 0x04002D28 RID: 11560
		private bool _updateParent;

		// Token: 0x04002D29 RID: 11561
		private int _version;

		// Token: 0x04002D2A RID: 11562
		private bool _isTrackingViewState;

		// Token: 0x04002D2B RID: 11563
		private List<TreeNodeCollection.LogItem> _log;

		// Token: 0x0200066C RID: 1644
		private class LogItem
		{
			// Token: 0x060050C9 RID: 20681 RVA: 0x00144A61 File Offset: 0x00143A61
			public LogItem(TreeNodeCollection.LogItemType type, int index, bool tracked)
			{
				this._type = type;
				this._index = index;
				this._tracked = tracked;
			}

			// Token: 0x17001481 RID: 5249
			// (get) Token: 0x060050CA RID: 20682 RVA: 0x00144A7E File Offset: 0x00143A7E
			public int Index
			{
				get
				{
					return this._index;
				}
			}

			// Token: 0x17001482 RID: 5250
			// (get) Token: 0x060050CB RID: 20683 RVA: 0x00144A86 File Offset: 0x00143A86
			// (set) Token: 0x060050CC RID: 20684 RVA: 0x00144A8E File Offset: 0x00143A8E
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

			// Token: 0x17001483 RID: 5251
			// (get) Token: 0x060050CD RID: 20685 RVA: 0x00144A97 File Offset: 0x00143A97
			public TreeNodeCollection.LogItemType Type
			{
				get
				{
					return this._type;
				}
			}

			// Token: 0x04002D2C RID: 11564
			private TreeNodeCollection.LogItemType _type;

			// Token: 0x04002D2D RID: 11565
			private int _index;

			// Token: 0x04002D2E RID: 11566
			private bool _tracked;
		}

		// Token: 0x0200066D RID: 1645
		private enum LogItemType
		{
			// Token: 0x04002D30 RID: 11568
			Insert,
			// Token: 0x04002D31 RID: 11569
			Remove,
			// Token: 0x04002D32 RID: 11570
			Clear
		}

		// Token: 0x0200066E RID: 1646
		private class TreeNodeCollectionEnumerator : IEnumerator
		{
			// Token: 0x060050CE RID: 20686 RVA: 0x00144A9F File Offset: 0x00143A9F
			internal TreeNodeCollectionEnumerator(TreeNodeCollection list)
			{
				this.list = list;
				this.index = -1;
				this.version = list._version;
			}

			// Token: 0x060050CF RID: 20687 RVA: 0x00144AC4 File Offset: 0x00143AC4
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

			// Token: 0x17001484 RID: 5252
			// (get) Token: 0x060050D0 RID: 20688 RVA: 0x00144B42 File Offset: 0x00143B42
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x17001485 RID: 5253
			// (get) Token: 0x060050D1 RID: 20689 RVA: 0x00144B4C File Offset: 0x00143B4C
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

			// Token: 0x060050D2 RID: 20690 RVA: 0x00144B9B File Offset: 0x00143B9B
			public void Reset()
			{
				if (this.version != this.list._version)
				{
					throw new InvalidOperationException(SR.GetString("ListEnumVersionMismatch"));
				}
				this.currentElement = null;
				this.index = -1;
			}

			// Token: 0x04002D33 RID: 11571
			private TreeNodeCollection list;

			// Token: 0x04002D34 RID: 11572
			private int index;

			// Token: 0x04002D35 RID: 11573
			private int version;

			// Token: 0x04002D36 RID: 11574
			private TreeNode currentElement;
		}
	}
}

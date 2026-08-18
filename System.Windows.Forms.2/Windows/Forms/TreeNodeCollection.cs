using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;

namespace System.Windows.Forms
{
	// Token: 0x02000413 RID: 1043
	[Editor("System.Windows.Forms.Design.TreeNodeCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	public class TreeNodeCollection : IList, ICollection, IEnumerable
	{
		// Token: 0x060048C5 RID: 18629 RVA: 0x00132927 File Offset: 0x00130B27
		internal TreeNodeCollection(TreeNode owner)
		{
			this.owner = owner;
		}

		// Token: 0x170011DE RID: 4574
		// (get) Token: 0x060048C6 RID: 18630 RVA: 0x00132944 File Offset: 0x00130B44
		// (set) Token: 0x060048C7 RID: 18631 RVA: 0x0013294C File Offset: 0x00130B4C
		internal int FixedIndex
		{
			get
			{
				return this.fixedIndex;
			}
			set
			{
				this.fixedIndex = value;
			}
		}

		// Token: 0x170011DF RID: 4575
		public virtual TreeNode this[int index]
		{
			get
			{
				if (index < 0 || index >= this.owner.childCount)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this.owner.children[index];
			}
			set
			{
				if (index < 0 || index >= this.owner.childCount)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				value.parent = this.owner;
				value.index = index;
				this.owner.children[index] = value;
				value.Realize(false);
			}
		}

		// Token: 0x170011E0 RID: 4576
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (value is TreeNode)
				{
					this[index] = (TreeNode)value;
					return;
				}
				throw new ArgumentException(SR.GetString("TreeNodeCollectionBadTreeNode"), "value");
			}
		}

		// Token: 0x170011E1 RID: 4577
		public virtual TreeNode this[string key]
		{
			get
			{
				if (string.IsNullOrEmpty(key))
				{
					return null;
				}
				int index = this.IndexOfKey(key);
				if (this.IsValidIndex(index))
				{
					return this[index];
				}
				return null;
			}
		}

		// Token: 0x170011E2 RID: 4578
		// (get) Token: 0x060048CD RID: 18637 RVA: 0x00132A65 File Offset: 0x00130C65
		[Browsable(false)]
		public int Count
		{
			get
			{
				return this.owner.childCount;
			}
		}

		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x060048CE RID: 18638 RVA: 0x00006C59 File Offset: 0x00004E59
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170011E4 RID: 4580
		// (get) Token: 0x060048CF RID: 18639 RVA: 0x00011A20 File Offset: 0x0000FC20
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170011E5 RID: 4581
		// (get) Token: 0x060048D0 RID: 18640 RVA: 0x00011A20 File Offset: 0x0000FC20
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170011E6 RID: 4582
		// (get) Token: 0x060048D1 RID: 18641 RVA: 0x00011A20 File Offset: 0x0000FC20
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060048D2 RID: 18642 RVA: 0x00132A74 File Offset: 0x00130C74
		public virtual TreeNode Add(string text)
		{
			TreeNode treeNode = new TreeNode(text);
			this.Add(treeNode);
			return treeNode;
		}

		// Token: 0x060048D3 RID: 18643 RVA: 0x00132A94 File Offset: 0x00130C94
		public virtual TreeNode Add(string key, string text)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			this.Add(treeNode);
			return treeNode;
		}

		// Token: 0x060048D4 RID: 18644 RVA: 0x00132AB8 File Offset: 0x00130CB8
		public virtual TreeNode Add(string key, string text, int imageIndex)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			treeNode.ImageIndex = imageIndex;
			this.Add(treeNode);
			return treeNode;
		}

		// Token: 0x060048D5 RID: 18645 RVA: 0x00132AE4 File Offset: 0x00130CE4
		public virtual TreeNode Add(string key, string text, string imageKey)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			treeNode.ImageKey = imageKey;
			this.Add(treeNode);
			return treeNode;
		}

		// Token: 0x060048D6 RID: 18646 RVA: 0x00132B10 File Offset: 0x00130D10
		public virtual TreeNode Add(string key, string text, int imageIndex, int selectedImageIndex)
		{
			TreeNode treeNode = new TreeNode(text, imageIndex, selectedImageIndex);
			treeNode.Name = key;
			this.Add(treeNode);
			return treeNode;
		}

		// Token: 0x060048D7 RID: 18647 RVA: 0x00132B38 File Offset: 0x00130D38
		public virtual TreeNode Add(string key, string text, string imageKey, string selectedImageKey)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			treeNode.ImageKey = imageKey;
			treeNode.SelectedImageKey = selectedImageKey;
			this.Add(treeNode);
			return treeNode;
		}

		// Token: 0x060048D8 RID: 18648 RVA: 0x00132B6C File Offset: 0x00130D6C
		public virtual void AddRange(TreeNode[] nodes)
		{
			if (nodes == null)
			{
				throw new ArgumentNullException("nodes");
			}
			if (nodes.Length == 0)
			{
				return;
			}
			TreeView treeView = this.owner.TreeView;
			if (treeView != null && nodes.Length > 200)
			{
				treeView.BeginUpdate();
			}
			this.owner.Nodes.FixedIndex = this.owner.childCount;
			this.owner.EnsureCapacity(nodes.Length);
			for (int i = nodes.Length - 1; i >= 0; i--)
			{
				this.AddInternal(nodes[i], i);
			}
			this.owner.Nodes.FixedIndex = -1;
			if (treeView != null && nodes.Length > 200)
			{
				treeView.EndUpdate();
			}
		}

		// Token: 0x060048D9 RID: 18649 RVA: 0x00132C14 File Offset: 0x00130E14
		public TreeNode[] Find(string key, bool searchAllChildren)
		{
			ArrayList arrayList = this.FindInternal(key, searchAllChildren, this, new ArrayList());
			TreeNode[] array = new TreeNode[arrayList.Count];
			arrayList.CopyTo(array, 0);
			return array;
		}

		// Token: 0x060048DA RID: 18650 RVA: 0x00132C48 File Offset: 0x00130E48
		private ArrayList FindInternal(string key, bool searchAllChildren, TreeNodeCollection treeNodeCollectionToLookIn, ArrayList foundTreeNodes)
		{
			if (treeNodeCollectionToLookIn == null || foundTreeNodes == null)
			{
				return null;
			}
			for (int i = 0; i < treeNodeCollectionToLookIn.Count; i++)
			{
				if (treeNodeCollectionToLookIn[i] != null && WindowsFormsUtils.SafeCompareStrings(treeNodeCollectionToLookIn[i].Name, key, true))
				{
					foundTreeNodes.Add(treeNodeCollectionToLookIn[i]);
				}
			}
			if (searchAllChildren)
			{
				for (int j = 0; j < treeNodeCollectionToLookIn.Count; j++)
				{
					if (treeNodeCollectionToLookIn[j] != null && treeNodeCollectionToLookIn[j].Nodes != null && treeNodeCollectionToLookIn[j].Nodes.Count > 0)
					{
						foundTreeNodes = this.FindInternal(key, searchAllChildren, treeNodeCollectionToLookIn[j].Nodes, foundTreeNodes);
					}
				}
			}
			return foundTreeNodes;
		}

		// Token: 0x060048DB RID: 18651 RVA: 0x00132CF5 File Offset: 0x00130EF5
		public virtual int Add(TreeNode node)
		{
			return this.AddInternal(node, 0);
		}

		// Token: 0x060048DC RID: 18652 RVA: 0x00132D00 File Offset: 0x00130F00
		private int AddInternal(TreeNode node, int delta)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (node.handle != IntPtr.Zero)
			{
				throw new ArgumentException(SR.GetString("OnlyOneControl", new object[]
				{
					node.Text
				}), "node");
			}
			TreeView treeView = this.owner.TreeView;
			if (treeView != null && treeView.Sorted)
			{
				return this.owner.AddSorted(node);
			}
			node.parent = this.owner;
			int num = this.owner.Nodes.FixedIndex;
			if (num != -1)
			{
				node.index = num + delta;
			}
			else
			{
				this.owner.EnsureCapacity(1);
				node.index = this.owner.childCount;
			}
			this.owner.children[node.index] = node;
			this.owner.childCount++;
			node.Realize(false);
			if (treeView != null && node == treeView.selectedNode)
			{
				treeView.SelectedNode = node;
			}
			if (treeView != null && treeView.TreeViewNodeSorter != null)
			{
				treeView.Sort();
			}
			return node.index;
		}

		// Token: 0x060048DD RID: 18653 RVA: 0x00132E15 File Offset: 0x00131015
		int IList.Add(object node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (node is TreeNode)
			{
				return this.Add((TreeNode)node);
			}
			return this.Add(node.ToString()).index;
		}

		// Token: 0x060048DE RID: 18654 RVA: 0x00132E4B File Offset: 0x0013104B
		public bool Contains(TreeNode node)
		{
			return this.IndexOf(node) != -1;
		}

		// Token: 0x060048DF RID: 18655 RVA: 0x00132E5A File Offset: 0x0013105A
		public virtual bool ContainsKey(string key)
		{
			return this.IsValidIndex(this.IndexOfKey(key));
		}

		// Token: 0x060048E0 RID: 18656 RVA: 0x00132E69 File Offset: 0x00131069
		bool IList.Contains(object node)
		{
			return node is TreeNode && this.Contains((TreeNode)node);
		}

		// Token: 0x060048E1 RID: 18657 RVA: 0x00132E84 File Offset: 0x00131084
		public int IndexOf(TreeNode node)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i] == node)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060048E2 RID: 18658 RVA: 0x00132EAF File Offset: 0x001310AF
		int IList.IndexOf(object node)
		{
			if (node is TreeNode)
			{
				return this.IndexOf((TreeNode)node);
			}
			return -1;
		}

		// Token: 0x060048E3 RID: 18659 RVA: 0x00132EC8 File Offset: 0x001310C8
		public virtual int IndexOfKey(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				return -1;
			}
			if (this.IsValidIndex(this.lastAccessedIndex) && WindowsFormsUtils.SafeCompareStrings(this[this.lastAccessedIndex].Name, key, true))
			{
				return this.lastAccessedIndex;
			}
			for (int i = 0; i < this.Count; i++)
			{
				if (WindowsFormsUtils.SafeCompareStrings(this[i].Name, key, true))
				{
					this.lastAccessedIndex = i;
					return i;
				}
			}
			this.lastAccessedIndex = -1;
			return -1;
		}

		// Token: 0x060048E4 RID: 18660 RVA: 0x00132F48 File Offset: 0x00131148
		public virtual void Insert(int index, TreeNode node)
		{
			if (node.handle != IntPtr.Zero)
			{
				throw new ArgumentException(SR.GetString("OnlyOneControl", new object[]
				{
					node.Text
				}), "node");
			}
			TreeView treeView = this.owner.TreeView;
			if (treeView != null && treeView.Sorted)
			{
				this.owner.AddSorted(node);
				return;
			}
			if (index < 0)
			{
				index = 0;
			}
			if (index > this.owner.childCount)
			{
				index = this.owner.childCount;
			}
			this.owner.InsertNodeAt(index, node);
		}

		// Token: 0x060048E5 RID: 18661 RVA: 0x00132FDF File Offset: 0x001311DF
		void IList.Insert(int index, object node)
		{
			if (node is TreeNode)
			{
				this.Insert(index, (TreeNode)node);
				return;
			}
			throw new ArgumentException(SR.GetString("TreeNodeCollectionBadTreeNode"), "node");
		}

		// Token: 0x060048E6 RID: 18662 RVA: 0x0013300C File Offset: 0x0013120C
		public virtual TreeNode Insert(int index, string text)
		{
			TreeNode treeNode = new TreeNode(text);
			this.Insert(index, treeNode);
			return treeNode;
		}

		// Token: 0x060048E7 RID: 18663 RVA: 0x0013302C File Offset: 0x0013122C
		public virtual TreeNode Insert(int index, string key, string text)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			this.Insert(index, treeNode);
			return treeNode;
		}

		// Token: 0x060048E8 RID: 18664 RVA: 0x00133050 File Offset: 0x00131250
		public virtual TreeNode Insert(int index, string key, string text, int imageIndex)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			treeNode.ImageIndex = imageIndex;
			this.Insert(index, treeNode);
			return treeNode;
		}

		// Token: 0x060048E9 RID: 18665 RVA: 0x0013307C File Offset: 0x0013127C
		public virtual TreeNode Insert(int index, string key, string text, string imageKey)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			treeNode.ImageKey = imageKey;
			this.Insert(index, treeNode);
			return treeNode;
		}

		// Token: 0x060048EA RID: 18666 RVA: 0x001330A8 File Offset: 0x001312A8
		public virtual TreeNode Insert(int index, string key, string text, int imageIndex, int selectedImageIndex)
		{
			TreeNode treeNode = new TreeNode(text, imageIndex, selectedImageIndex);
			treeNode.Name = key;
			this.Insert(index, treeNode);
			return treeNode;
		}

		// Token: 0x060048EB RID: 18667 RVA: 0x001330D0 File Offset: 0x001312D0
		public virtual TreeNode Insert(int index, string key, string text, string imageKey, string selectedImageKey)
		{
			TreeNode treeNode = new TreeNode(text);
			treeNode.Name = key;
			treeNode.ImageKey = imageKey;
			treeNode.SelectedImageKey = selectedImageKey;
			this.Insert(index, treeNode);
			return treeNode;
		}

		// Token: 0x060048EC RID: 18668 RVA: 0x00133104 File Offset: 0x00131304
		private bool IsValidIndex(int index)
		{
			return index >= 0 && index < this.Count;
		}

		// Token: 0x060048ED RID: 18669 RVA: 0x00133115 File Offset: 0x00131315
		public virtual void Clear()
		{
			this.owner.Clear();
		}

		// Token: 0x060048EE RID: 18670 RVA: 0x00133122 File Offset: 0x00131322
		public void CopyTo(Array dest, int index)
		{
			if (this.owner.childCount > 0)
			{
				Array.Copy(this.owner.children, 0, dest, index, this.owner.childCount);
			}
		}

		// Token: 0x060048EF RID: 18671 RVA: 0x00133150 File Offset: 0x00131350
		public void Remove(TreeNode node)
		{
			node.Remove();
		}

		// Token: 0x060048F0 RID: 18672 RVA: 0x00133158 File Offset: 0x00131358
		void IList.Remove(object node)
		{
			if (node is TreeNode)
			{
				this.Remove((TreeNode)node);
			}
		}

		// Token: 0x060048F1 RID: 18673 RVA: 0x0013316E File Offset: 0x0013136E
		public virtual void RemoveAt(int index)
		{
			this[index].Remove();
		}

		// Token: 0x060048F2 RID: 18674 RVA: 0x0013317C File Offset: 0x0013137C
		public virtual void RemoveByKey(string key)
		{
			int index = this.IndexOfKey(key);
			if (this.IsValidIndex(index))
			{
				this.RemoveAt(index);
			}
		}

		// Token: 0x060048F3 RID: 18675 RVA: 0x001331A4 File Offset: 0x001313A4
		public IEnumerator GetEnumerator()
		{
			object[] children = this.owner.children;
			return new WindowsFormsUtils.ArraySubsetEnumerator(children, this.owner.childCount);
		}

		// Token: 0x04002748 RID: 10056
		private TreeNode owner;

		// Token: 0x04002749 RID: 10057
		private int lastAccessedIndex = -1;

		// Token: 0x0400274A RID: 10058
		private int fixedIndex = -1;
	}
}

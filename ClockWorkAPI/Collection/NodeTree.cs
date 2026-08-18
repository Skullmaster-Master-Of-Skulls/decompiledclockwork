using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml.Serialization;

namespace ClockWorkAPI.Collection
{
	// Token: 0x02000040 RID: 64
	[Serializable]
	public class NodeTree<T> : INode<T>, ITree<T>, IEnumerableCollectionPair<T>, IDisposable, ISerializable
	{
		// Token: 0x06000361 RID: 865 RVA: 0x00011474 File Offset: 0x00010474
		protected NodeTree()
		{
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000114A8 File Offset: 0x000104A8
		~NodeTree()
		{
			this.Dispose(false);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x000114DC File Offset: 0x000104DC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000114F0 File Offset: 0x000104F0
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._EventHandlerList != null)
				{
					this._EventHandlerList.Dispose();
				}
			}
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00011524 File Offset: 0x00010524
		public static ITree<T> NewTree()
		{
			return new NodeTree<T>.RootObject();
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0001153C File Offset: 0x0001053C
		public static ITree<T> NewTree(IEqualityComparer<T> dataComparer)
		{
			return new NodeTree<T>.RootObject(dataComparer);
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00011554 File Offset: 0x00010554
		protected static INode<T> NewNode()
		{
			return new NodeTree<T>();
		}

		// Token: 0x06000368 RID: 872 RVA: 0x0001156C File Offset: 0x0001056C
		protected virtual NodeTree<T> CreateTree()
		{
			return new NodeTree<T>.RootObject();
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00011584 File Offset: 0x00010584
		protected virtual NodeTree<T> CreateNode()
		{
			return new NodeTree<T>();
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0001159C File Offset: 0x0001059C
		public override string ToString()
		{
			T data = this.Data;
			string result;
			if (data == null)
			{
				result = string.Empty;
			}
			else
			{
				result = data.ToString();
			}
			return result;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x000115DC File Offset: 0x000105DC
		public virtual string ToStringRecursive()
		{
			string text = string.Empty;
			foreach (INode<T> node in this.All.Nodes)
			{
				NodeTree<T> nodeTree = (NodeTree<T>)node;
				object obj = text;
				text = string.Concat(new object[]
				{
					obj,
					new string('\t', nodeTree.Depth),
					nodeTree,
					Environment.NewLine
				});
			}
			return text;
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600036C RID: 876 RVA: 0x00011680 File Offset: 0x00010680
		public virtual int Depth
		{
			get
			{
				int num = -1;
				INode<T> node = this;
				while (!node.IsRoot)
				{
					num++;
					node = node.Parent;
				}
				return num;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600036D RID: 877 RVA: 0x000116B4 File Offset: 0x000106B4
		public virtual int BranchIndex
		{
			get
			{
				int num = -1;
				for (INode<T> node = this; node != null; node = node.Previous)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600036E RID: 878 RVA: 0x000116E4 File Offset: 0x000106E4
		public virtual int BranchCount
		{
			get
			{
				int num = 0;
				for (INode<T> node = this.First; node != null; node = node.Next)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00011718 File Offset: 0x00010718
		[ReflectionPermission(SecurityAction.Demand, Unrestricted = true)]
		protected virtual T DeepCopyData(T data)
		{
			T result;
			if (data == null)
			{
				result = default(T);
			}
			else
			{
				IDeepCopy deepCopy = data as IDeepCopy;
				if (deepCopy != null)
				{
					result = (T)((object)deepCopy.CreateDeepCopy());
				}
				else
				{
					ICloneable cloneable = data as ICloneable;
					if (cloneable != null)
					{
						result = (T)((object)cloneable.Clone());
					}
					else
					{
						ConstructorInfo constructor = data.GetType().GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, new Type[]
						{
							typeof(T)
						}, null);
						if (constructor != null)
						{
							result = (T)((object)constructor.Invoke(new object[]
							{
								data
							}));
						}
						else
						{
							result = data;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000370 RID: 880 RVA: 0x000117F4 File Offset: 0x000107F4
		protected virtual NodeTree<T>.RootObject GetRootObject
		{
			get
			{
				return (NodeTree<T>.RootObject)this.Root;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000371 RID: 881 RVA: 0x00011814 File Offset: 0x00010814
		// (set) Token: 0x06000372 RID: 882 RVA: 0x0001184C File Offset: 0x0001084C
		public virtual IEqualityComparer<T> DataComparer
		{
			get
			{
				if (!this.Root.IsTree)
				{
					throw new InvalidOperationException("This is not a Tree");
				}
				return this.GetRootObject.DataComparer;
			}
			set
			{
				if (!this.Root.IsTree)
				{
					throw new InvalidOperationException("This is not a Tree");
				}
				this.GetRootObject.DataComparer = value;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00011884 File Offset: 0x00010884
		// (set) Token: 0x06000374 RID: 884 RVA: 0x000118C0 File Offset: 0x000108C0
		protected virtual int Version
		{
			get
			{
				INode<T> root = this.Root;
				if (!root.IsTree)
				{
					throw new InvalidOperationException("This is not a Tree");
				}
				return NodeTree<T>.GetNodeTree(root).Version;
			}
			set
			{
				INode<T> root = this.Root;
				if (!root.IsTree)
				{
					throw new InvalidOperationException("This is not a Tree");
				}
				NodeTree<T>.GetNodeTree(root).Version = value;
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x000118F8 File Offset: 0x000108F8
		protected bool HasChanged(int version)
		{
			return this.Version != version;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00011918 File Offset: 0x00010918
		protected void IncrementVersion()
		{
			INode<T> root = this.Root;
			if (!root.IsTree)
			{
				throw new InvalidOperationException("This is not a Tree");
			}
			NodeTree<T>.GetNodeTree(root).Version++;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00011958 File Offset: 0x00010958
		[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("NodeTreeVersion", 1);
			info.AddValue("Data", this._Data);
			info.AddValue("Parent", this._Parent);
			info.AddValue("Previous", this._Previous);
			info.AddValue("Next", this._Next);
			info.AddValue("Child", this._Child);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x000119D4 File Offset: 0x000109D4
		protected NodeTree(SerializationInfo info, StreamingContext context)
		{
			int @int = info.GetInt32("NodeTreeVersion");
			if (@int != 1)
			{
				throw new SerializationException("Unknown version");
			}
			this._Data = (T)((object)info.GetValue("Data", typeof(T)));
			this._Parent = (NodeTree<T>)info.GetValue("Parent", typeof(NodeTree<T>));
			this._Previous = (NodeTree<T>)info.GetValue("Previous", typeof(NodeTree<T>));
			this._Next = (NodeTree<T>)info.GetValue("Next", typeof(NodeTree<T>));
			this._Child = (NodeTree<T>)info.GetValue("Child", typeof(NodeTree<T>));
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00011AD4 File Offset: 0x00010AD4
		public virtual void XmlSerialize(Stream stream)
		{
			XmlSerializer xmlSerializer;
			try
			{
				xmlSerializer = new XmlSerializer(typeof(NodeTree<T>.TreeXmlSerializationAdapter));
			}
			catch (Exception ex)
			{
				throw;
			}
			try
			{
				xmlSerializer.Serialize(stream, new NodeTree<T>.TreeXmlSerializationAdapter(NodeTree<T>.XmlAdapterTag, this));
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00011B34 File Offset: 0x00010B34
		public static ITree<T> XmlDeserialize(Stream stream)
		{
			XmlSerializer xmlSerializer;
			try
			{
				xmlSerializer = new XmlSerializer(typeof(NodeTree<T>.TreeXmlSerializationAdapter));
			}
			catch (Exception ex)
			{
				throw;
			}
			object obj;
			try
			{
				obj = xmlSerializer.Deserialize(stream);
			}
			catch (Exception ex)
			{
				throw;
			}
			NodeTree<T>.TreeXmlSerializationAdapter treeXmlSerializationAdapter = (NodeTree<T>.TreeXmlSerializationAdapter)obj;
			return treeXmlSerializationAdapter.Tree;
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00011BA0 File Offset: 0x00010BA0
		// (set) Token: 0x0600037C RID: 892 RVA: 0x00011BB8 File Offset: 0x00010BB8
		public T Data
		{
			get
			{
				return this._Data;
			}
			set
			{
				if (this.IsRoot)
				{
					throw new InvalidOperationException("This is a Root");
				}
				this.OnSetting(this, value);
				this._Data = value;
				this.OnSetDone(this, value);
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x0600037D RID: 893 RVA: 0x00011BF8 File Offset: 0x00010BF8
		public INode<T> Parent
		{
			get
			{
				return this._Parent;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x0600037E RID: 894 RVA: 0x00011C10 File Offset: 0x00010C10
		public INode<T> Previous
		{
			get
			{
				return this._Previous;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x0600037F RID: 895 RVA: 0x00011C28 File Offset: 0x00010C28
		public INode<T> Next
		{
			get
			{
				return this._Next;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00011C40 File Offset: 0x00010C40
		public INode<T> Child
		{
			get
			{
				return this._Child;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000381 RID: 897 RVA: 0x00011C58 File Offset: 0x00010C58
		public ITree<T> Tree
		{
			get
			{
				return (ITree<T>)this.Root;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000382 RID: 898 RVA: 0x00011C78 File Offset: 0x00010C78
		public INode<T> Root
		{
			get
			{
				INode<T> node = this;
				while (node.Parent != null)
				{
					node = node.Parent;
				}
				return node;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000383 RID: 899 RVA: 0x00011CA8 File Offset: 0x00010CA8
		public INode<T> Top
		{
			get
			{
				if (!this.Root.IsTree)
				{
					throw new InvalidOperationException("This is not a tree");
				}
				INode<T> result;
				if (this.IsRoot)
				{
					result = null;
				}
				else
				{
					INode<T> node = this;
					while (node.Parent.Parent != null)
					{
						node = node.Parent;
					}
					result = node;
				}
				return result;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00011D08 File Offset: 0x00010D08
		public INode<T> First
		{
			get
			{
				INode<T> node = this;
				while (node.Previous != null)
				{
					node = node.Previous;
				}
				return node;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000385 RID: 901 RVA: 0x00011D38 File Offset: 0x00010D38
		public INode<T> Last
		{
			get
			{
				INode<T> node = this;
				while (node.Next != null)
				{
					node = node.Next;
				}
				return node;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00011D68 File Offset: 0x00010D68
		public INode<T> LastChild
		{
			get
			{
				INode<T> result;
				if (this.Child == null)
				{
					result = null;
				}
				else
				{
					result = this.Child.Last;
				}
				return result;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000387 RID: 903 RVA: 0x00011D9C File Offset: 0x00010D9C
		public bool HasPrevious
		{
			get
			{
				return this.Previous != null;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000388 RID: 904 RVA: 0x00011DBC File Offset: 0x00010DBC
		public bool HasNext
		{
			get
			{
				return this.Next != null;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00011DDC File Offset: 0x00010DDC
		public bool HasChild
		{
			get
			{
				return this.Child != null;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600038A RID: 906 RVA: 0x00011DFC File Offset: 0x00010DFC
		public bool IsFirst
		{
			get
			{
				return this.Previous == null;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00011E18 File Offset: 0x00010E18
		public bool IsLast
		{
			get
			{
				return this.Next == null;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00011E34 File Offset: 0x00010E34
		public bool IsTree
		{
			get
			{
				return this.IsRoot && this is NodeTree<T>.RootObject;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00011E60 File Offset: 0x00010E60
		public bool IsRoot
		{
			get
			{
				bool flag = this.Parent == null;
				if (flag)
				{
				}
				return flag;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600038E RID: 910 RVA: 0x00011E88 File Offset: 0x00010E88
		public bool HasParent
		{
			get
			{
				return !this.IsRoot && this.Parent.Parent != null;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600038F RID: 911 RVA: 0x00011EBC File Offset: 0x00010EBC
		public bool IsTop
		{
			get
			{
				return !this.IsRoot && this.Parent.Parent == null;
			}
		}

		// Token: 0x17000175 RID: 373
		public virtual INode<T> this[T item]
		{
			get
			{
				if (!this.Root.IsTree)
				{
					throw new InvalidOperationException("This is not a tree");
				}
				IEqualityComparer<T> dataComparer = this.DataComparer;
				foreach (INode<T> node in this.All.Nodes)
				{
					if (dataComparer.Equals(node.Data, item))
					{
						return node;
					}
				}
				return null;
			}
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00011F8C File Offset: 0x00010F8C
		public virtual bool Contains(INode<T> item)
		{
			if (!this.Root.IsTree)
			{
				throw new InvalidOperationException("This is not a tree");
			}
			return this.All.Nodes.Contains(item);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00011FCC File Offset: 0x00010FCC
		public virtual bool Contains(T item)
		{
			if (!this.Root.IsTree)
			{
				throw new InvalidOperationException("This is not a tree");
			}
			return this.All.Values.Contains(item);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x0001200C File Offset: 0x0001100C
		public INode<T> InsertPrevious(T o)
		{
			if (this.IsRoot)
			{
				throw new InvalidOperationException("This is a Root");
			}
			if (!this.Root.IsTree)
			{
				throw new InvalidOperationException("This is not a tree");
			}
			NodeTree<T> nodeTree = this.CreateNode();
			nodeTree._Data = o;
			this.InsertPreviousCore(nodeTree);
			return nodeTree;
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00012068 File Offset: 0x00011068
		public INode<T> InsertNext(T o)
		{
			if (this.IsRoot)
			{
				throw new InvalidOperationException("This is a Root");
			}
			if (!this.Root.IsTree)
			{
				throw new InvalidOperationException("This is not a tree");
			}
			NodeTree<T> nodeTree = this.CreateNode();
			nodeTree._Data = o;
			this.InsertNextCore(nodeTree);
			return nodeTree;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x000120C4 File Offset: 0x000110C4
		public INode<T> InsertChild(T o)
		{
			if (!this.Root.IsTree)
			{
				throw new InvalidOperationException("This is not a tree");
			}
			NodeTree<T> nodeTree = this.CreateNode();
			nodeTree._Data = o;
			this.InsertChildCore(nodeTree);
			return nodeTree;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00012108 File Offset: 0x00011108
		public INode<T> Add(T o)
		{
			if (this.IsRoot)
			{
				throw new InvalidOperationException("This is a Root");
			}
			if (!this.Root.IsTree)
			{
				throw new InvalidOperationException("This is not a tree");
			}
			return this.Last.InsertNext(o);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00012158 File Offset: 0x00011158
		public INode<T> AddChild(T o)
		{
			if (!this.Root.IsTree)
			{
				throw new InvalidOperationException("This is not a tree");
			}
			INode<T> result;
			if (this.Child == null)
			{
				result = this.InsertChild(o);
			}
			else
			{
				result = this.Child.Add(o);
			}
			return result;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x000121AC File Offset: 0x000111AC
		public void InsertPrevious(ITree<T> tree)
		{
			if (this.IsRoot)
			{
				throw new InvalidOperationException("This is a Root");
			}
			if (!this.Root.IsTree)
			{
				throw new InvalidOperationException("This is not a tree");
			}
			NodeTree<T> nodeTree = NodeTree<T>.GetNodeTree(tree);
			if (!nodeTree.IsRoot)
			{
				throw new ArgumentException("Tree is not a Root");
			}
			if (!nodeTree.IsTree)
			{
				throw new ArgumentException("Tree is not a tree");
			}
			for (INode<T> node = nodeTree.Child; node != null; node = node.Next)
			{
				NodeTree<T> nodeTree2 = NodeTree<T>.GetNodeTree(node);
				NodeTree<T> newINode = nodeTree2.CopyCore();
				this.InsertPreviousCore(newINode);
			}
		}

		// Token: 0x06000399 RID: 921 RVA: 0x0001225C File Offset: 0x0001125C
		public void InsertNext(ITree<T> tree)
		{
			if (this.IsRoot)
			{
				throw new InvalidOperationException("This is a Root");
			}
			if (!this.Root.IsTree)
			{
				throw new InvalidOperationException("This is not a tree");
			}
			NodeTree<T> nodeTree = NodeTree<T>.GetNodeTree(tree);
			if (!nodeTree.IsRoot)
			{
				throw new ArgumentException("Tree is not a Root");
			}
			if (!nodeTree.IsTree)
			{
				throw new ArgumentException("Tree is not a tree");
			}
			for (INode<T> node = nodeTree.LastChild; node != null; node = node.Previous)
			{
				NodeTree<T> nodeTree2 = NodeTree<T>.GetNodeTree(node);
				NodeTree<T> newINode = nodeTree2.CopyCore();
				this.InsertNextCore(newINode);
			}
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0001230C File Offset: 0x0001130C
		public void InsertChild(ITree<T> tree)
		{
			if (!this.Root.IsTree)
			{
				throw new InvalidOperationException("This is not a tree");
			}
			NodeTree<T> nodeTree = NodeTree<T>.GetNodeTree(tree);
			if (!nodeTree.IsRoot)
			{
				throw new ArgumentException("Tree is not a Root");
			}
			if (!nodeTree.IsTree)
			{
				throw new ArgumentException("Tree is not a tree");
			}
			for (INode<T> node = nodeTree.LastChild; node != null; node = node.Previous)
			{
				NodeTree<T> nodeTree2 = NodeTree<T>.GetNodeTree(node);
				NodeTree<T> newINode = nodeTree2.CopyCore();
				this.InsertChildCore(newINode);
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x000123A0 File Offset: 0x000113A0
		public void Add(ITree<T> tree)
		{
			if (this.IsRoot)
			{
				throw new InvalidOperationException("This is a Root");
			}
			if (!this.Root.IsTree)
			{
				throw new InvalidOperationException("This is not a tree");
			}
			this.Last.InsertNext(tree);
		}

		// Token: 0x0600039C RID: 924 RVA: 0x000123F0 File Offset: 0x000113F0
		public void AddChild(ITree<T> tree)
		{
			if (!this.Root.IsTree)
			{
				throw new InvalidOperationException("This is not a tree");
			}
			if (this.Child == null)
			{
				this.InsertChild(tree);
			}
			else
			{
				this.Child.Add(tree);
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00012440 File Offset: 0x00011440
		protected virtual void InsertPreviousCore(INode<T> newINode)
		{
			if (this.IsRoot)
			{
				throw new InvalidOperationException("This is a Root");
			}
			if (!newINode.IsRoot)
			{
				throw new ArgumentException("Node is not a Root");
			}
			if (newINode.IsTree)
			{
				throw new ArgumentException("Node is a tree");
			}
			this.IncrementVersion();
			this.OnInserting(this, NodeTreeInsertOperation.Previous, newINode);
			NodeTree<T> nodeTree = NodeTree<T>.GetNodeTree(newINode);
			nodeTree._Parent = this._Parent;
			nodeTree._Previous = this._Previous;
			nodeTree._Next = this;
			this._Previous = nodeTree;
			if (nodeTree.Previous != null)
			{
				NodeTree<T> nodeTree2 = NodeTree<T>.GetNodeTree(nodeTree.Previous);
				nodeTree2._Next = nodeTree;
			}
			else
			{
				NodeTree<T> nodeTree3 = NodeTree<T>.GetNodeTree(nodeTree.Parent);
				nodeTree3._Child = nodeTree;
			}
			this.OnInserted(this, NodeTreeInsertOperation.Previous, newINode);
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00012514 File Offset: 0x00011514
		protected virtual void InsertNextCore(INode<T> newINode)
		{
			if (this.IsRoot)
			{
				throw new InvalidOperationException("This is a Root");
			}
			if (!newINode.IsRoot)
			{
				throw new ArgumentException("Node is not a Root");
			}
			if (newINode.IsTree)
			{
				throw new ArgumentException("Node is a tree");
			}
			this.IncrementVersion();
			this.OnInserting(this, NodeTreeInsertOperation.Next, newINode);
			NodeTree<T> nodeTree = NodeTree<T>.GetNodeTree(newINode);
			nodeTree._Parent = this._Parent;
			nodeTree._Previous = this;
			nodeTree._Next = this._Next;
			this._Next = nodeTree;
			if (nodeTree.Next != null)
			{
				NodeTree<T> nodeTree2 = NodeTree<T>.GetNodeTree(nodeTree.Next);
				nodeTree2._Previous = nodeTree;
			}
			this.OnInserted(this, NodeTreeInsertOperation.Next, newINode);
		}

		// Token: 0x0600039F RID: 927 RVA: 0x000125D4 File Offset: 0x000115D4
		protected virtual void InsertChildCore(INode<T> newINode)
		{
			if (!newINode.IsRoot)
			{
				throw new ArgumentException("Node is not a Root");
			}
			if (newINode.IsTree)
			{
				throw new ArgumentException("Node is a tree");
			}
			this.IncrementVersion();
			this.OnInserting(this, NodeTreeInsertOperation.Child, newINode);
			NodeTree<T> nodeTree = NodeTree<T>.GetNodeTree(newINode);
			nodeTree._Parent = this;
			nodeTree._Next = this._Child;
			this._Child = nodeTree;
			if (nodeTree.Next != null)
			{
				NodeTree<T> nodeTree2 = NodeTree<T>.GetNodeTree(nodeTree.Next);
				nodeTree2._Previous = nodeTree;
			}
			this.OnInserted(this, NodeTreeInsertOperation.Child, newINode);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00012670 File Offset: 0x00011670
		protected virtual void AddCore(INode<T> newINode)
		{
			if (this.IsRoot)
			{
				throw new InvalidOperationException("This is a Root");
			}
			NodeTree<T> nodeTree = NodeTree<T>.GetNodeTree(this.Last);
			nodeTree.InsertNextCore(newINode);
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x000126AC File Offset: 0x000116AC
		protected virtual void AddChildCore(INode<T> newINode)
		{
			if (this.Child == null)
			{
				this.InsertChildCore(newINode);
			}
			else
			{
				NodeTree<T> nodeTree = NodeTree<T>.GetNodeTree(this.Child);
				nodeTree.AddCore(newINode);
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x000126EC File Offset: 0x000116EC
		public ITree<T> Cut(T o)
		{
			if (!this.Root.IsTree)
			{
				throw new InvalidOperationException("This is not a tree");
			}
			INode<T> node = this[o];
			ITree<T> result;
			if (node == null)
			{
				result = null;
			}
			else
			{
				result = node.Cut();
			}
			return result;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00012738 File Offset: 0x00011738
		public ITree<T> Copy(T o)
		{
			if (!this.Root.IsTree)
			{
				throw new InvalidOperationException("This is not a tree");
			}
			INode<T> node = this[o];
			ITree<T> result;
			if (node == null)
			{
				result = null;
			}
			else
			{
				result = node.Copy();
			}
			return result;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00012784 File Offset: 0x00011784
		public ITree<T> DeepCopy(T o)
		{
			if (!this.Root.IsTree)
			{
				throw new InvalidOperationException("This is not a tree");
			}
			INode<T> node = this[o];
			ITree<T> result;
			if (node == null)
			{
				result = null;
			}
			else
			{
				result = node.DeepCopy();
			}
			return result;
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x000127D0 File Offset: 0x000117D0
		public bool Remove(T o)
		{
			if (!this.Root.IsTree)
			{
				throw new InvalidOperationException("This is not a tree");
			}
			INode<T> node = this[o];
			bool result;
			if (node == null)
			{
				result = false;
			}
			else
			{
				node.Remove();
				result = true;
			}
			return result;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0001281C File Offset: 0x0001181C
		private NodeTree<T> BoxInTree(NodeTree<T> node)
		{
			if (!node.IsRoot)
			{
				throw new ArgumentException("Node is not a Root");
			}
			if (node.IsTree)
			{
				throw new ArgumentException("Node is a tree");
			}
			NodeTree<T> nodeTree = this.CreateTree();
			nodeTree.AddChildCore(node);
			return nodeTree;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x0001286C File Offset: 0x0001186C
		public ITree<T> Cut()
		{
			if (this.IsRoot)
			{
				throw new InvalidOperationException("This is a Root");
			}
			if (!this.Root.IsTree)
			{
				throw new InvalidOperationException("This is not a tree");
			}
			NodeTree<T> node = this.CutCore();
			return this.BoxInTree(node);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x000128C0 File Offset: 0x000118C0
		public ITree<T> Copy()
		{
			if (!this.Root.IsTree)
			{
				throw new InvalidOperationException("This is not a tree");
			}
			ITree<T> result;
			if (this.IsTree)
			{
				NodeTree<T> nodeTree = this.CopyCore();
				result = nodeTree;
			}
			else
			{
				NodeTree<T> node = this.CopyCore();
				result = this.BoxInTree(node);
			}
			return result;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00012914 File Offset: 0x00011914
		public ITree<T> DeepCopy()
		{
			if (!this.Root.IsTree)
			{
				throw new InvalidOperationException("This is not a tree");
			}
			ITree<T> result;
			if (this.IsTree)
			{
				NodeTree<T> nodeTree = this.DeepCopyCore();
				result = nodeTree;
			}
			else
			{
				NodeTree<T> node = this.DeepCopyCore();
				result = this.BoxInTree(node);
			}
			return result;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00012968 File Offset: 0x00011968
		public void Remove()
		{
			if (this.IsRoot)
			{
				throw new InvalidOperationException("This is a Root");
			}
			if (!this.Root.IsTree)
			{
				throw new InvalidOperationException("This is not a tree");
			}
			this.RemoveCore();
		}

		// Token: 0x060003AB RID: 939 RVA: 0x000129B0 File Offset: 0x000119B0
		protected virtual NodeTree<T> CutCore()
		{
			if (this.IsRoot)
			{
				throw new InvalidOperationException("This is a Root");
			}
			this.IncrementVersion();
			this.OnCutting(this);
			INode<T> root = this.Root;
			if (this._Next != null)
			{
				this._Next._Previous = this._Previous;
			}
			if (this.Previous != null)
			{
				this._Previous._Next = this._Next;
			}
			else
			{
				this._Parent._Child = this._Next;
			}
			this._Parent = null;
			this._Previous = null;
			this._Next = null;
			this.OnCutDone(root, this);
			return this;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00012A60 File Offset: 0x00011A60
		protected virtual NodeTree<T> CopyCore()
		{
			if (!this.Root.IsTree)
			{
				throw new InvalidOperationException("This is not a tree");
			}
			if (this.IsRoot && !this.IsTree)
			{
				throw new InvalidOperationException("This is a Root");
			}
			NodeTree<T> result;
			if (this.IsTree)
			{
				NodeTree<T> nodeTree = this.CreateTree();
				this.OnCopying(this, nodeTree);
				this.CopyChildNodes(this, nodeTree, false);
				this.OnCopied(this, nodeTree);
				result = nodeTree;
			}
			else
			{
				NodeTree<T> nodeTree2 = this.CreateNode();
				nodeTree2._Data = this.Data;
				this.OnCopying(this, nodeTree2);
				this.CopyChildNodes(this, nodeTree2, false);
				this.OnCopied(this, nodeTree2);
				result = nodeTree2;
			}
			return result;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00012B14 File Offset: 0x00011B14
		protected virtual NodeTree<T> DeepCopyCore()
		{
			if (!this.Root.IsTree)
			{
				throw new InvalidOperationException("This is not a tree");
			}
			if (this.IsRoot && !this.IsTree)
			{
				throw new InvalidOperationException("This is a Root");
			}
			NodeTree<T> result;
			if (this.IsTree)
			{
				NodeTree<T> nodeTree = this.CreateTree();
				this.OnCopying(this, nodeTree);
				this.CopyChildNodes(this, nodeTree, true);
				this.OnCopied(this, nodeTree);
				result = nodeTree;
			}
			else
			{
				NodeTree<T> nodeTree2 = this.CreateNode();
				nodeTree2._Data = this.DeepCopyData(this.Data);
				this.OnDeepCopying(this, nodeTree2);
				this.CopyChildNodes(this, nodeTree2, true);
				this.OnDeepCopied(this, nodeTree2);
				result = nodeTree2;
			}
			return result;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00012BCC File Offset: 0x00011BCC
		private void CopyChildNodes(INode<T> oldNode, NodeTree<T> newNode, bool bDeepCopy)
		{
			NodeTree<T> nodeTree = null;
			for (INode<T> node = oldNode.Child; node != null; node = node.Next)
			{
				NodeTree<T> nodeTree2 = this.CreateNode();
				if (!bDeepCopy)
				{
					nodeTree2._Data = node.Data;
				}
				else
				{
					nodeTree2._Data = this.DeepCopyData(node.Data);
				}
				if (node.Previous == null)
				{
					newNode._Child = nodeTree2;
				}
				nodeTree2._Parent = newNode;
				nodeTree2._Previous = nodeTree;
				if (nodeTree != null)
				{
					nodeTree._Next = nodeTree2;
				}
				this.CopyChildNodes(node, nodeTree2, bDeepCopy);
				nodeTree = nodeTree2;
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00012C68 File Offset: 0x00011C68
		protected virtual void RemoveCore()
		{
			if (this.IsRoot)
			{
				throw new InvalidOperationException("This is a Root");
			}
			this.CutCore();
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00012C98 File Offset: 0x00011C98
		public bool CanMoveToParent
		{
			get
			{
				return !this.IsRoot && !this.IsTop;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x00012CD0 File Offset: 0x00011CD0
		public bool CanMoveToPrevious
		{
			get
			{
				return !this.IsRoot && !this.IsFirst;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x00012D08 File Offset: 0x00011D08
		public bool CanMoveToNext
		{
			get
			{
				return !this.IsRoot && !this.IsLast;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x00012D40 File Offset: 0x00011D40
		public bool CanMoveToChild
		{
			get
			{
				return !this.IsRoot && !this.IsFirst;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x00012D78 File Offset: 0x00011D78
		public bool CanMoveToFirst
		{
			get
			{
				return !this.IsRoot && !this.IsFirst;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x00012DB0 File Offset: 0x00011DB0
		public bool CanMoveToLast
		{
			get
			{
				return !this.IsRoot && !this.IsLast;
			}
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00012DE8 File Offset: 0x00011DE8
		public void MoveToParent()
		{
			if (!this.CanMoveToParent)
			{
				throw new InvalidOperationException("Cannot move to Parent");
			}
			NodeTree<T> nodeTree = NodeTree<T>.GetNodeTree(this.Parent);
			NodeTree<T> newINode = this.CutCore();
			nodeTree.InsertNextCore(newINode);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00012E28 File Offset: 0x00011E28
		public void MoveToPrevious()
		{
			if (!this.CanMoveToPrevious)
			{
				throw new InvalidOperationException("Cannot move to Previous");
			}
			NodeTree<T> nodeTree = NodeTree<T>.GetNodeTree(this.Previous);
			NodeTree<T> newINode = this.CutCore();
			nodeTree.InsertPreviousCore(newINode);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00012E68 File Offset: 0x00011E68
		public void MoveToNext()
		{
			if (!this.CanMoveToNext)
			{
				throw new InvalidOperationException("Cannot move to Next");
			}
			NodeTree<T> nodeTree = NodeTree<T>.GetNodeTree(this.Next);
			NodeTree<T> newINode = this.CutCore();
			nodeTree.InsertNextCore(newINode);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00012EA8 File Offset: 0x00011EA8
		public void MoveToChild()
		{
			if (!this.CanMoveToChild)
			{
				throw new InvalidOperationException("Cannot move to Child");
			}
			NodeTree<T> nodeTree = NodeTree<T>.GetNodeTree(this.Previous);
			NodeTree<T> newINode = this.CutCore();
			nodeTree.AddChildCore(newINode);
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00012EE8 File Offset: 0x00011EE8
		public void MoveToFirst()
		{
			if (!this.CanMoveToFirst)
			{
				throw new InvalidOperationException("Cannot move to first");
			}
			NodeTree<T> nodeTree = NodeTree<T>.GetNodeTree(this.First);
			NodeTree<T> newINode = this.CutCore();
			nodeTree.InsertPreviousCore(newINode);
		}

		// Token: 0x060003BB RID: 955 RVA: 0x00012F28 File Offset: 0x00011F28
		public void MoveToLast()
		{
			if (!this.CanMoveToLast)
			{
				throw new InvalidOperationException("Cannot move to last");
			}
			NodeTree<T> nodeTree = NodeTree<T>.GetNodeTree(this.Last);
			NodeTree<T> newINode = this.CutCore();
			nodeTree.InsertNextCore(newINode);
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00012F68 File Offset: 0x00011F68
		public virtual IEnumerableCollection<INode<T>> Nodes
		{
			get
			{
				return this.All.Nodes;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060003BD RID: 957 RVA: 0x00012F88 File Offset: 0x00011F88
		public virtual IEnumerableCollection<T> Values
		{
			get
			{
				return this.All.Values;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00012FA8 File Offset: 0x00011FA8
		public IEnumerableCollectionPair<T> All
		{
			get
			{
				return new NodeTree<T>.AllEnumerator(this);
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060003BF RID: 959 RVA: 0x00012FC0 File Offset: 0x00011FC0
		public IEnumerableCollectionPair<T> AllChildren
		{
			get
			{
				return new NodeTree<T>.AllChildrenEnumerator(this);
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00012FD8 File Offset: 0x00011FD8
		public IEnumerableCollectionPair<T> DirectChildren
		{
			get
			{
				return new NodeTree<T>.DirectChildrenEnumerator(this);
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x00012FF0 File Offset: 0x00011FF0
		public IEnumerableCollectionPair<T> DirectChildrenInReverse
		{
			get
			{
				return new NodeTree<T>.DirectChildrenInReverseEnumerator(this);
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00013008 File Offset: 0x00012008
		public int DirectChildCount
		{
			get
			{
				int num = 0;
				for (INode<T> node = this.Child; node != null; node = node.Next)
				{
					num++;
				}
				return num;
			}
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0001303C File Offset: 0x0001203C
		public virtual Type DataType
		{
			get
			{
				return typeof(T);
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00013058 File Offset: 0x00012058
		public void Clear()
		{
			if (!this.IsRoot)
			{
				throw new InvalidOperationException("This is not a Root");
			}
			if (!this.IsTree)
			{
				throw new InvalidOperationException("This is not a tree");
			}
			this.OnClearing(this);
			this._Child = null;
			this.OnCleared(this);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x000130A8 File Offset: 0x000120A8
		protected static NodeTree<T> GetNodeTree(ITree<T> tree)
		{
			if (tree == null)
			{
				throw new ArgumentNullException("Tree is null");
			}
			return (NodeTree<T>)tree;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000130D8 File Offset: 0x000120D8
		protected static NodeTree<T> GetNodeTree(INode<T> node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("Node is null");
			}
			return (NodeTree<T>)node;
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x00013108 File Offset: 0x00012108
		public virtual int Count
		{
			get
			{
				int num = this.IsRoot ? 0 : 1;
				for (INode<T> node = this.Child; node != null; node = node.Next)
				{
					num += node.Count;
				}
				return num;
			}
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x0001314C File Offset: 0x0001214C
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x00013160 File Offset: 0x00012160
		protected EventHandlerList EventHandlerList
		{
			get
			{
				return this._EventHandlerList;
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00013178 File Offset: 0x00012178
		protected EventHandlerList GetCreateEventHandlerList()
		{
			if (this._EventHandlerList == null)
			{
				this._EventHandlerList = new EventHandlerList();
			}
			return this._EventHandlerList;
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060003CB RID: 971 RVA: 0x000131AC File Offset: 0x000121AC
		protected static object ValidateEventKey
		{
			get
			{
				return NodeTree<T>._ValidateEventKey;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060003CC RID: 972 RVA: 0x000131C4 File Offset: 0x000121C4
		protected static object ClearingEventKey
		{
			get
			{
				return NodeTree<T>._ClearingEventKey;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060003CD RID: 973 RVA: 0x000131DC File Offset: 0x000121DC
		protected static object ClearedEventKey
		{
			get
			{
				return NodeTree<T>._ClearedEventKey;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060003CE RID: 974 RVA: 0x000131F4 File Offset: 0x000121F4
		protected static object SettingEventKey
		{
			get
			{
				return NodeTree<T>._SettingEventKey;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0001320C File Offset: 0x0001220C
		protected static object SetDoneEventKey
		{
			get
			{
				return NodeTree<T>._SetDoneEventKey;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00013224 File Offset: 0x00012224
		protected static object InsertingEventKey
		{
			get
			{
				return NodeTree<T>._InsertingEventKey;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0001323C File Offset: 0x0001223C
		protected static object InsertedEventKey
		{
			get
			{
				return NodeTree<T>._InsertedEventKey;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00013254 File Offset: 0x00012254
		protected static object CuttingEventKey
		{
			get
			{
				return NodeTree<T>._CuttingEventKey;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0001326C File Offset: 0x0001226C
		protected static object CutDoneEventKey
		{
			get
			{
				return NodeTree<T>._CutDoneEventKey;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x00013284 File Offset: 0x00012284
		protected static object CopyingEventKey
		{
			get
			{
				return NodeTree<T>._CopyingEventKey;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0001329C File Offset: 0x0001229C
		protected static object CopiedEventKey
		{
			get
			{
				return NodeTree<T>._CopiedEventKey;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x000132B4 File Offset: 0x000122B4
		protected static object DeepCopyingEventKey
		{
			get
			{
				return NodeTree<T>._DeepCopyingEventKey;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x000132CC File Offset: 0x000122CC
		protected static object DeepCopiedEventKey
		{
			get
			{
				return NodeTree<T>._DeepCopiedEventKey;
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060003D8 RID: 984 RVA: 0x000132E3 File Offset: 0x000122E3
		// (remove) Token: 0x060003D9 RID: 985 RVA: 0x000132F8 File Offset: 0x000122F8
		public event EventHandler<NodeTreeDataEventArgs<T>> Validate
		{
			add
			{
				this.GetCreateEventHandlerList().AddHandler(NodeTree<T>.ValidateEventKey, value);
			}
			remove
			{
				this.GetCreateEventHandlerList().RemoveHandler(NodeTree<T>.ValidateEventKey, value);
			}
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060003DA RID: 986 RVA: 0x0001330D File Offset: 0x0001230D
		// (remove) Token: 0x060003DB RID: 987 RVA: 0x00013322 File Offset: 0x00012322
		public event EventHandler Clearing
		{
			add
			{
				this.GetCreateEventHandlerList().AddHandler(NodeTree<T>.ClearingEventKey, value);
			}
			remove
			{
				this.GetCreateEventHandlerList().RemoveHandler(NodeTree<T>.ClearingEventKey, value);
			}
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x060003DC RID: 988 RVA: 0x00013337 File Offset: 0x00012337
		// (remove) Token: 0x060003DD RID: 989 RVA: 0x0001334C File Offset: 0x0001234C
		public event EventHandler Cleared
		{
			add
			{
				this.GetCreateEventHandlerList().AddHandler(NodeTree<T>.ClearedEventKey, value);
			}
			remove
			{
				this.GetCreateEventHandlerList().RemoveHandler(NodeTree<T>.ClearedEventKey, value);
			}
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x060003DE RID: 990 RVA: 0x00013361 File Offset: 0x00012361
		// (remove) Token: 0x060003DF RID: 991 RVA: 0x00013376 File Offset: 0x00012376
		public event EventHandler<NodeTreeDataEventArgs<T>> Setting
		{
			add
			{
				this.GetCreateEventHandlerList().AddHandler(NodeTree<T>.SettingEventKey, value);
			}
			remove
			{
				this.GetCreateEventHandlerList().RemoveHandler(NodeTree<T>.SettingEventKey, value);
			}
		}

		// Token: 0x1400001E RID: 30
		// (add) Token: 0x060003E0 RID: 992 RVA: 0x0001338B File Offset: 0x0001238B
		// (remove) Token: 0x060003E1 RID: 993 RVA: 0x000133A0 File Offset: 0x000123A0
		public event EventHandler<NodeTreeDataEventArgs<T>> SetDone
		{
			add
			{
				this.GetCreateEventHandlerList().AddHandler(NodeTree<T>.SetDoneEventKey, value);
			}
			remove
			{
				this.GetCreateEventHandlerList().RemoveHandler(NodeTree<T>.SetDoneEventKey, value);
			}
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x060003E2 RID: 994 RVA: 0x000133B5 File Offset: 0x000123B5
		// (remove) Token: 0x060003E3 RID: 995 RVA: 0x000133CA File Offset: 0x000123CA
		public event EventHandler<NodeTreeInsertEventArgs<T>> Inserting
		{
			add
			{
				this.GetCreateEventHandlerList().AddHandler(NodeTree<T>.InsertingEventKey, value);
			}
			remove
			{
				this.GetCreateEventHandlerList().RemoveHandler(NodeTree<T>.InsertingEventKey, value);
			}
		}

		// Token: 0x14000020 RID: 32
		// (add) Token: 0x060003E4 RID: 996 RVA: 0x000133DF File Offset: 0x000123DF
		// (remove) Token: 0x060003E5 RID: 997 RVA: 0x000133F4 File Offset: 0x000123F4
		public event EventHandler<NodeTreeInsertEventArgs<T>> Inserted
		{
			add
			{
				this.GetCreateEventHandlerList().AddHandler(NodeTree<T>.InsertedEventKey, value);
			}
			remove
			{
				this.GetCreateEventHandlerList().RemoveHandler(NodeTree<T>.InsertedEventKey, value);
			}
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x060003E6 RID: 998 RVA: 0x00013409 File Offset: 0x00012409
		// (remove) Token: 0x060003E7 RID: 999 RVA: 0x0001341E File Offset: 0x0001241E
		public event EventHandler Cutting
		{
			add
			{
				this.GetCreateEventHandlerList().AddHandler(NodeTree<T>.CuttingEventKey, value);
			}
			remove
			{
				this.GetCreateEventHandlerList().RemoveHandler(NodeTree<T>.CuttingEventKey, value);
			}
		}

		// Token: 0x14000022 RID: 34
		// (add) Token: 0x060003E8 RID: 1000 RVA: 0x00013433 File Offset: 0x00012433
		// (remove) Token: 0x060003E9 RID: 1001 RVA: 0x00013448 File Offset: 0x00012448
		public event EventHandler CutDone
		{
			add
			{
				this.GetCreateEventHandlerList().AddHandler(NodeTree<T>.CutDoneEventKey, value);
			}
			remove
			{
				this.GetCreateEventHandlerList().RemoveHandler(NodeTree<T>.CutDoneEventKey, value);
			}
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x060003EA RID: 1002 RVA: 0x0001345D File Offset: 0x0001245D
		// (remove) Token: 0x060003EB RID: 1003 RVA: 0x00013472 File Offset: 0x00012472
		public event EventHandler<NodeTreeNodeEventArgs<T>> Copying
		{
			add
			{
				this.GetCreateEventHandlerList().AddHandler(NodeTree<T>.CopyingEventKey, value);
			}
			remove
			{
				this.GetCreateEventHandlerList().RemoveHandler(NodeTree<T>.CopyingEventKey, value);
			}
		}

		// Token: 0x14000024 RID: 36
		// (add) Token: 0x060003EC RID: 1004 RVA: 0x00013487 File Offset: 0x00012487
		// (remove) Token: 0x060003ED RID: 1005 RVA: 0x0001349C File Offset: 0x0001249C
		public event EventHandler<NodeTreeNodeEventArgs<T>> Copied
		{
			add
			{
				this.GetCreateEventHandlerList().AddHandler(NodeTree<T>.CopiedEventKey, value);
			}
			remove
			{
				this.GetCreateEventHandlerList().RemoveHandler(NodeTree<T>.CopiedEventKey, value);
			}
		}

		// Token: 0x14000025 RID: 37
		// (add) Token: 0x060003EE RID: 1006 RVA: 0x000134B1 File Offset: 0x000124B1
		// (remove) Token: 0x060003EF RID: 1007 RVA: 0x000134C6 File Offset: 0x000124C6
		public event EventHandler<NodeTreeNodeEventArgs<T>> DeepCopying
		{
			add
			{
				this.GetCreateEventHandlerList().AddHandler(NodeTree<T>.DeepCopyingEventKey, value);
			}
			remove
			{
				this.GetCreateEventHandlerList().RemoveHandler(NodeTree<T>.DeepCopyingEventKey, value);
			}
		}

		// Token: 0x14000026 RID: 38
		// (add) Token: 0x060003F0 RID: 1008 RVA: 0x000134DB File Offset: 0x000124DB
		// (remove) Token: 0x060003F1 RID: 1009 RVA: 0x000134F0 File Offset: 0x000124F0
		public event EventHandler<NodeTreeNodeEventArgs<T>> DeepCopied
		{
			add
			{
				this.GetCreateEventHandlerList().AddHandler(NodeTree<T>.DeepCopiedEventKey, value);
			}
			remove
			{
				this.GetCreateEventHandlerList().RemoveHandler(NodeTree<T>.DeepCopiedEventKey, value);
			}
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00013508 File Offset: 0x00012508
		protected virtual void OnValidate(INode<T> node, T data)
		{
			if (!this.Root.IsTree)
			{
				throw new InvalidOperationException("This is not a tree");
			}
			if (data is INode<T>)
			{
				throw new ArgumentException("Object is a node");
			}
			if ((!typeof(T).IsClass || data != null) && !this.DataType.IsInstanceOfType(data))
			{
				throw new ArgumentException("Object is not a " + this.DataType.Name);
			}
			if (this._EventHandlerList != null)
			{
				EventHandler<NodeTreeDataEventArgs<T>> eventHandler = (EventHandler<NodeTreeDataEventArgs<T>>)this._EventHandlerList[NodeTree<T>.ValidateEventKey];
				if (eventHandler != null)
				{
					eventHandler(node, new NodeTreeDataEventArgs<T>(data));
				}
			}
			if (!this.IsRoot)
			{
				NodeTree<T>.GetNodeTree(this.Root).OnValidate(node, data);
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x000135FC File Offset: 0x000125FC
		protected virtual void OnClearing(ITree<T> tree)
		{
			if (this._EventHandlerList != null)
			{
				EventHandler eventHandler = (EventHandler)this._EventHandlerList[NodeTree<T>.ClearingEventKey];
				if (eventHandler != null)
				{
					eventHandler(tree, EventArgs.Empty);
				}
			}
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00013644 File Offset: 0x00012644
		protected virtual void OnCleared(ITree<T> tree)
		{
			if (this._EventHandlerList != null)
			{
				EventHandler eventHandler = (EventHandler)this._EventHandlerList[NodeTree<T>.ClearedEventKey];
				if (eventHandler != null)
				{
					eventHandler(tree, EventArgs.Empty);
				}
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x0001368C File Offset: 0x0001268C
		protected virtual void OnSetting(INode<T> node, T data)
		{
			this.OnSettingCore(node, data, true);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x0001369C File Offset: 0x0001269C
		protected virtual void OnSettingCore(INode<T> node, T data, bool raiseValidate)
		{
			if (this._EventHandlerList != null)
			{
				EventHandler<NodeTreeDataEventArgs<T>> eventHandler = (EventHandler<NodeTreeDataEventArgs<T>>)this._EventHandlerList[NodeTree<T>.SettingEventKey];
				if (eventHandler != null)
				{
					eventHandler(node, new NodeTreeDataEventArgs<T>(data));
				}
			}
			if (!this.IsRoot)
			{
				NodeTree<T>.GetNodeTree(this.Root).OnSettingCore(node, data, false);
			}
			if (raiseValidate)
			{
				this.OnValidate(node, data);
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00013714 File Offset: 0x00012714
		protected virtual void OnSetDone(INode<T> node, T data)
		{
			this.OnSetDoneCore(node, data, true);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00013724 File Offset: 0x00012724
		protected virtual void OnSetDoneCore(INode<T> node, T data, bool raiseValidate)
		{
			if (this._EventHandlerList != null)
			{
				EventHandler<NodeTreeDataEventArgs<T>> eventHandler = (EventHandler<NodeTreeDataEventArgs<T>>)this._EventHandlerList[NodeTree<T>.SetDoneEventKey];
				if (eventHandler != null)
				{
					eventHandler(node, new NodeTreeDataEventArgs<T>(data));
				}
			}
			if (!this.IsRoot)
			{
				NodeTree<T>.GetNodeTree(this.Root).OnSetDoneCore(node, data, false);
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x0001378B File Offset: 0x0001278B
		protected virtual void OnInserting(INode<T> oldNode, NodeTreeInsertOperation operation, INode<T> newNode)
		{
			this.OnInsertingCore(oldNode, operation, newNode, true);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001379C File Offset: 0x0001279C
		protected virtual void OnInsertingCore(INode<T> oldNode, NodeTreeInsertOperation operation, INode<T> newNode, bool raiseValidate)
		{
			if (this._EventHandlerList != null)
			{
				EventHandler<NodeTreeInsertEventArgs<T>> eventHandler = (EventHandler<NodeTreeInsertEventArgs<T>>)this._EventHandlerList[NodeTree<T>.InsertingEventKey];
				if (eventHandler != null)
				{
					eventHandler(oldNode, new NodeTreeInsertEventArgs<T>(operation, newNode));
				}
			}
			if (!this.IsRoot)
			{
				NodeTree<T>.GetNodeTree(this.Root).OnInsertingCore(oldNode, operation, newNode, false);
			}
			if (raiseValidate)
			{
				this.OnValidate(oldNode, newNode.Data);
			}
			if (raiseValidate)
			{
				this.OnInsertingTree(newNode);
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00013830 File Offset: 0x00012830
		protected virtual void OnInsertingTree(INode<T> newNode)
		{
			for (INode<T> node = newNode.Child; node != null; node = node.Next)
			{
				this.OnInsertingTree(newNode, node);
				this.OnInsertingTree(node);
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x0001386C File Offset: 0x0001286C
		protected virtual void OnInsertingTree(INode<T> newNode, INode<T> child)
		{
			this.OnInsertingTreeCore(newNode, child, true);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001387C File Offset: 0x0001287C
		protected virtual void OnInsertingTreeCore(INode<T> newNode, INode<T> child, bool raiseValidate)
		{
			if (this._EventHandlerList != null)
			{
				EventHandler<NodeTreeInsertEventArgs<T>> eventHandler = (EventHandler<NodeTreeInsertEventArgs<T>>)this._EventHandlerList[NodeTree<T>.InsertingEventKey];
				if (eventHandler != null)
				{
					eventHandler(newNode, new NodeTreeInsertEventArgs<T>(NodeTreeInsertOperation.Tree, child));
				}
			}
			if (!this.IsRoot)
			{
				NodeTree<T>.GetNodeTree(this.Root).OnInsertingTreeCore(newNode, child, false);
			}
			if (raiseValidate)
			{
				this.OnValidate(newNode, child.Data);
			}
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x000138FA File Offset: 0x000128FA
		protected virtual void OnInserted(INode<T> oldNode, NodeTreeInsertOperation operation, INode<T> newNode)
		{
			this.OnInsertedCore(oldNode, operation, newNode, true);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00013908 File Offset: 0x00012908
		protected virtual void OnInsertedCore(INode<T> oldNode, NodeTreeInsertOperation operation, INode<T> newNode, bool raiseValidate)
		{
			if (this._EventHandlerList != null)
			{
				EventHandler<NodeTreeInsertEventArgs<T>> eventHandler = (EventHandler<NodeTreeInsertEventArgs<T>>)this._EventHandlerList[NodeTree<T>.InsertedEventKey];
				if (eventHandler != null)
				{
					eventHandler(oldNode, new NodeTreeInsertEventArgs<T>(operation, newNode));
				}
			}
			if (!this.IsRoot)
			{
				NodeTree<T>.GetNodeTree(this.Root).OnInsertedCore(oldNode, operation, newNode, false);
			}
			if (raiseValidate)
			{
				this.OnInsertedTree(newNode);
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00013984 File Offset: 0x00012984
		protected virtual void OnInsertedTree(INode<T> newNode)
		{
			for (INode<T> node = newNode.Child; node != null; node = node.Next)
			{
				this.OnInsertedTree(newNode, node);
				this.OnInsertedTree(node);
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000139C0 File Offset: 0x000129C0
		protected virtual void OnInsertedTree(INode<T> newNode, INode<T> child)
		{
			this.OnInsertedTreeCore(newNode, child, true);
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000139D0 File Offset: 0x000129D0
		protected virtual void OnInsertedTreeCore(INode<T> newNode, INode<T> child, bool raiseValidate)
		{
			if (this._EventHandlerList != null)
			{
				EventHandler<NodeTreeInsertEventArgs<T>> eventHandler = (EventHandler<NodeTreeInsertEventArgs<T>>)this._EventHandlerList[NodeTree<T>.InsertedEventKey];
				if (eventHandler != null)
				{
					eventHandler(newNode, new NodeTreeInsertEventArgs<T>(NodeTreeInsertOperation.Tree, child));
				}
			}
			if (!this.IsRoot)
			{
				NodeTree<T>.GetNodeTree(this.Root).OnInsertedTreeCore(newNode, child, false);
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00013A38 File Offset: 0x00012A38
		protected virtual void OnCutting(INode<T> oldNode)
		{
			if (this._EventHandlerList != null)
			{
				EventHandler eventHandler = (EventHandler)this._EventHandlerList[NodeTree<T>.CuttingEventKey];
				if (eventHandler != null)
				{
					eventHandler(oldNode, EventArgs.Empty);
				}
			}
			if (!this.IsRoot)
			{
				NodeTree<T>.GetNodeTree(this.Root).OnCutting(oldNode);
			}
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00013A9C File Offset: 0x00012A9C
		protected virtual void OnCutDone(INode<T> oldRoot, INode<T> oldNode)
		{
			if (this._EventHandlerList != null)
			{
				EventHandler eventHandler = (EventHandler)this._EventHandlerList[NodeTree<T>.CutDoneEventKey];
				if (eventHandler != null)
				{
					eventHandler(oldNode, EventArgs.Empty);
				}
			}
			if (!this.IsTree)
			{
				NodeTree<T>.GetNodeTree(oldRoot).OnCutDone(oldRoot, oldNode);
			}
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00013AFC File Offset: 0x00012AFC
		protected virtual void OnCopying(INode<T> oldNode, INode<T> newNode)
		{
			this.OnCopyingCore(oldNode, newNode, true);
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00013B0C File Offset: 0x00012B0C
		protected virtual void OnCopyingCore(INode<T> oldNode, INode<T> newNode, bool raiseValidate)
		{
			if (this._EventHandlerList != null)
			{
				EventHandler<NodeTreeNodeEventArgs<T>> eventHandler = (EventHandler<NodeTreeNodeEventArgs<T>>)this._EventHandlerList[NodeTree<T>.CopyingEventKey];
				if (eventHandler != null)
				{
					eventHandler(oldNode, new NodeTreeNodeEventArgs<T>(newNode));
				}
			}
			if (!this.IsRoot)
			{
				NodeTree<T>.GetNodeTree(this.Root).OnCopyingCore(oldNode, newNode, false);
			}
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00013B73 File Offset: 0x00012B73
		protected virtual void OnCopied(INode<T> oldNode, INode<T> newNode)
		{
			this.OnCopiedCore(oldNode, newNode, true);
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00013B80 File Offset: 0x00012B80
		protected virtual void OnCopiedCore(INode<T> oldNode, INode<T> newNode, bool raiseValidate)
		{
			if (this._EventHandlerList != null)
			{
				EventHandler<NodeTreeNodeEventArgs<T>> eventHandler = (EventHandler<NodeTreeNodeEventArgs<T>>)this._EventHandlerList[NodeTree<T>.CopiedEventKey];
				if (eventHandler != null)
				{
					eventHandler(oldNode, new NodeTreeNodeEventArgs<T>(newNode));
				}
			}
			if (!this.IsRoot)
			{
				NodeTree<T>.GetNodeTree(this.Root).OnCopiedCore(oldNode, newNode, false);
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00013BE7 File Offset: 0x00012BE7
		protected virtual void OnDeepCopying(INode<T> oldNode, INode<T> newNode)
		{
			this.OnDeepCopyingCore(oldNode, newNode, true);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x00013BF4 File Offset: 0x00012BF4
		protected virtual void OnDeepCopyingCore(INode<T> oldNode, INode<T> newNode, bool raiseValidate)
		{
			if (this._EventHandlerList != null)
			{
				EventHandler<NodeTreeNodeEventArgs<T>> eventHandler = (EventHandler<NodeTreeNodeEventArgs<T>>)this._EventHandlerList[NodeTree<T>.DeepCopyingEventKey];
				if (eventHandler != null)
				{
					eventHandler(oldNode, new NodeTreeNodeEventArgs<T>(newNode));
				}
			}
			if (!this.IsRoot)
			{
				NodeTree<T>.GetNodeTree(this.Root).OnDeepCopyingCore(oldNode, newNode, false);
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00013C5B File Offset: 0x00012C5B
		protected virtual void OnDeepCopied(INode<T> oldNode, INode<T> newNode)
		{
			this.OnDeepCopiedCore(oldNode, newNode, true);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00013C68 File Offset: 0x00012C68
		protected virtual void OnDeepCopiedCore(INode<T> oldNode, INode<T> newNode, bool raiseValidate)
		{
			if (this._EventHandlerList != null)
			{
				EventHandler<NodeTreeNodeEventArgs<T>> eventHandler = (EventHandler<NodeTreeNodeEventArgs<T>>)this._EventHandlerList[NodeTree<T>.DeepCopiedEventKey];
				if (eventHandler != null)
				{
					eventHandler(oldNode, new NodeTreeNodeEventArgs<T>(newNode));
				}
			}
			if (!this.IsRoot)
			{
				NodeTree<T>.GetNodeTree(this.Root).OnDeepCopiedCore(oldNode, newNode, false);
			}
		}

		// Token: 0x04000185 RID: 389
		private T _Data = default(T);

		// Token: 0x04000186 RID: 390
		private NodeTree<T> _Parent = null;

		// Token: 0x04000187 RID: 391
		private NodeTree<T> _Previous = null;

		// Token: 0x04000188 RID: 392
		private NodeTree<T> _Next = null;

		// Token: 0x04000189 RID: 393
		private NodeTree<T> _Child = null;

		// Token: 0x0400018A RID: 394
		protected static readonly object XmlAdapterTag = new object();

		// Token: 0x0400018B RID: 395
		private EventHandlerList _EventHandlerList;

		// Token: 0x0400018C RID: 396
		private static readonly object _ValidateEventKey = new object();

		// Token: 0x0400018D RID: 397
		private static readonly object _ClearingEventKey = new object();

		// Token: 0x0400018E RID: 398
		private static readonly object _ClearedEventKey = new object();

		// Token: 0x0400018F RID: 399
		private static readonly object _SettingEventKey = new object();

		// Token: 0x04000190 RID: 400
		private static readonly object _SetDoneEventKey = new object();

		// Token: 0x04000191 RID: 401
		private static readonly object _InsertingEventKey = new object();

		// Token: 0x04000192 RID: 402
		private static readonly object _InsertedEventKey = new object();

		// Token: 0x04000193 RID: 403
		private static readonly object _CuttingEventKey = new object();

		// Token: 0x04000194 RID: 404
		private static readonly object _CutDoneEventKey = new object();

		// Token: 0x04000195 RID: 405
		private static readonly object _CopyingEventKey = new object();

		// Token: 0x04000196 RID: 406
		private static readonly object _CopiedEventKey = new object();

		// Token: 0x04000197 RID: 407
		private static readonly object _DeepCopyingEventKey = new object();

		// Token: 0x04000198 RID: 408
		private static readonly object _DeepCopiedEventKey = new object();

		// Token: 0x02000041 RID: 65
		[Serializable]
		protected class RootObject : NodeTree<T>
		{
			// Token: 0x17000194 RID: 404
			// (get) Token: 0x0600040E RID: 1038 RVA: 0x00013D6C File Offset: 0x00012D6C
			// (set) Token: 0x0600040F RID: 1039 RVA: 0x00013D84 File Offset: 0x00012D84
			protected override int Version
			{
				get
				{
					return this._Version;
				}
				set
				{
					this._Version = value;
				}
			}

			// Token: 0x17000195 RID: 405
			// (get) Token: 0x06000410 RID: 1040 RVA: 0x00013D90 File Offset: 0x00012D90
			// (set) Token: 0x06000411 RID: 1041 RVA: 0x00013DC3 File Offset: 0x00012DC3
			public override IEqualityComparer<T> DataComparer
			{
				get
				{
					if (this._DataComparer == null)
					{
						this._DataComparer = EqualityComparer<T>.Default;
					}
					return this._DataComparer;
				}
				set
				{
					this._DataComparer = value;
				}
			}

			// Token: 0x06000412 RID: 1042 RVA: 0x00013DCD File Offset: 0x00012DCD
			public RootObject()
			{
			}

			// Token: 0x06000413 RID: 1043 RVA: 0x00013DDF File Offset: 0x00012DDF
			public RootObject(IEqualityComparer<T> dataComparer)
			{
				this._DataComparer = dataComparer;
			}

			// Token: 0x06000414 RID: 1044 RVA: 0x00013DF8 File Offset: 0x00012DF8
			public override string ToString()
			{
				return "ROOT: " + this.DataType.Name;
			}

			// Token: 0x06000415 RID: 1045 RVA: 0x00013E1F File Offset: 0x00012E1F
			[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
			public override void GetObjectData(SerializationInfo info, StreamingContext context)
			{
				base.GetObjectData(info, context);
				info.AddValue("RootObjectVersion", 1);
			}

			// Token: 0x06000416 RID: 1046 RVA: 0x00013E38 File Offset: 0x00012E38
			protected RootObject(SerializationInfo info, StreamingContext context) : base(info, context)
			{
				int @int = info.GetInt32("RootObjectVersion");
				if (@int != 1)
				{
					throw new SerializationException("Unknown version");
				}
			}

			// Token: 0x04000199 RID: 409
			private int _Version = 0;

			// Token: 0x0400019A RID: 410
			private IEqualityComparer<T> _DataComparer;
		}

		// Token: 0x02000042 RID: 66
		[XmlType("Tree")]
		public class TreeXmlSerializationAdapter
		{
			// Token: 0x17000196 RID: 406
			// (get) Token: 0x06000417 RID: 1047 RVA: 0x00013E78 File Offset: 0x00012E78
			// (set) Token: 0x06000418 RID: 1048 RVA: 0x00013E8B File Offset: 0x00012E8B
			[XmlAttribute]
			public int Version
			{
				get
				{
					return 1;
				}
				set
				{
					this._Version = value;
				}
			}

			// Token: 0x17000197 RID: 407
			// (get) Token: 0x06000419 RID: 1049 RVA: 0x00013E98 File Offset: 0x00012E98
			[XmlIgnore]
			public ITree<T> Tree
			{
				get
				{
					return this._Tree;
				}
			}

			// Token: 0x0600041A RID: 1050 RVA: 0x00013EB0 File Offset: 0x00012EB0
			private TreeXmlSerializationAdapter()
			{
			}

			// Token: 0x0600041B RID: 1051 RVA: 0x00013EC4 File Offset: 0x00012EC4
			public TreeXmlSerializationAdapter(object tag, ITree<T> tree)
			{
				if (!object.ReferenceEquals(NodeTree<T>.XmlAdapterTag, tag))
				{
					throw new InvalidOperationException("Don't use this class");
				}
				this._Tree = tree;
			}

			// Token: 0x17000198 RID: 408
			// (get) Token: 0x0600041C RID: 1052 RVA: 0x00013F04 File Offset: 0x00012F04
			// (set) Token: 0x0600041D RID: 1053 RVA: 0x00013F2B File Offset: 0x00012F2B
			public NodeTree<T>.NodeXmlSerializationAdapter Root
			{
				get
				{
					return new NodeTree<T>.NodeXmlSerializationAdapter(NodeTree<T>.XmlAdapterTag, this._Tree.Root);
				}
				set
				{
					this._Tree = NodeTree<T>.NewTree();
					this.ReformTree(this._Tree.Root, value);
				}
			}

			// Token: 0x0600041E RID: 1054 RVA: 0x00013F4C File Offset: 0x00012F4C
			private void ReformTree(INode<T> parent, NodeTree<T>.NodeXmlSerializationAdapter node)
			{
				foreach (object obj in node.Children)
				{
					NodeTree<T>.NodeXmlSerializationAdapter nodeXmlSerializationAdapter = (NodeTree<T>.NodeXmlSerializationAdapter)obj;
					INode<T> parent2 = parent.AddChild(nodeXmlSerializationAdapter.Data);
					this.ReformTree(parent2, nodeXmlSerializationAdapter);
				}
			}

			// Token: 0x0400019B RID: 411
			private int _Version = 0;

			// Token: 0x0400019C RID: 412
			private ITree<T> _Tree;
		}

		// Token: 0x02000043 RID: 67
		[XmlType("Node")]
		public class NodeXmlSerializationAdapter
		{
			// Token: 0x17000199 RID: 409
			// (get) Token: 0x0600041F RID: 1055 RVA: 0x00013FC4 File Offset: 0x00012FC4
			// (set) Token: 0x06000420 RID: 1056 RVA: 0x00013FD7 File Offset: 0x00012FD7
			[XmlAttribute]
			public int Version
			{
				get
				{
					return 1;
				}
				set
				{
					this._Version = value;
				}
			}

			// Token: 0x1700019A RID: 410
			// (get) Token: 0x06000421 RID: 1057 RVA: 0x00013FE4 File Offset: 0x00012FE4
			[XmlIgnore]
			public INode<T> Node
			{
				get
				{
					return this._Node;
				}
			}

			// Token: 0x06000422 RID: 1058 RVA: 0x00013FFC File Offset: 0x00012FFC
			private NodeXmlSerializationAdapter()
			{
				this._Node = NodeTree<T>.NewNode();
			}

			// Token: 0x06000423 RID: 1059 RVA: 0x00014024 File Offset: 0x00013024
			public NodeXmlSerializationAdapter(object tag, INode<T> node)
			{
				if (!object.ReferenceEquals(NodeTree<T>.XmlAdapterTag, tag))
				{
					throw new InvalidOperationException("Don't use this class");
				}
				this._Node = node;
				foreach (INode<T> node2 in node.DirectChildren.Nodes)
				{
					this._Children.Add(new NodeTree<T>.NodeXmlSerializationAdapter(NodeTree<T>.XmlAdapterTag, node2));
				}
			}

			// Token: 0x1700019B RID: 411
			// (get) Token: 0x06000424 RID: 1060 RVA: 0x000140CC File Offset: 0x000130CC
			// (set) Token: 0x06000425 RID: 1061 RVA: 0x000140E9 File Offset: 0x000130E9
			public T Data
			{
				get
				{
					return this._Node.Data;
				}
				set
				{
					NodeTree<T>.GetNodeTree(this._Node)._Data = value;
				}
			}

			// Token: 0x1700019C RID: 412
			// (get) Token: 0x06000426 RID: 1062 RVA: 0x00014100 File Offset: 0x00013100
			// (set) Token: 0x06000427 RID: 1063 RVA: 0x00014118 File Offset: 0x00013118
			public NodeTree<T>.NodeXmlSerializationAdapter.IXmlCollection Children
			{
				get
				{
					return this._Children;
				}
				set
				{
				}
			}

			// Token: 0x0400019D RID: 413
			private int _Version = 0;

			// Token: 0x0400019E RID: 414
			private INode<T> _Node;

			// Token: 0x0400019F RID: 415
			private NodeTree<T>.NodeXmlSerializationAdapter.IXmlCollection _Children = new NodeTree<T>.NodeXmlSerializationAdapter.ChildCollection();

			// Token: 0x02000044 RID: 68
			public interface IXmlCollection : ICollection, IEnumerable
			{
				// Token: 0x1700019D RID: 413
				NodeTree<T>.NodeXmlSerializationAdapter this[int index]
				{
					get;
				}

				// Token: 0x06000429 RID: 1065
				void Add(NodeTree<T>.NodeXmlSerializationAdapter item);
			}

			// Token: 0x02000045 RID: 69
			private class ChildCollection : List<NodeTree<T>.NodeXmlSerializationAdapter>, NodeTree<T>.NodeXmlSerializationAdapter.IXmlCollection, ICollection, IEnumerable
			{
			}
		}

		// Token: 0x02000046 RID: 70
		protected abstract class BaseEnumerableCollectionPair : IEnumerableCollectionPair<T>
		{
			// Token: 0x1700019E RID: 414
			// (get) Token: 0x0600042B RID: 1067 RVA: 0x00014124 File Offset: 0x00013124
			// (set) Token: 0x0600042C RID: 1068 RVA: 0x0001413C File Offset: 0x0001313C
			protected NodeTree<T> Root
			{
				get
				{
					return this._Root;
				}
				set
				{
					this._Root = value;
				}
			}

			// Token: 0x0600042D RID: 1069 RVA: 0x00014146 File Offset: 0x00013146
			protected BaseEnumerableCollectionPair(NodeTree<T> root)
			{
				this._Root = root;
			}

			// Token: 0x1700019F RID: 415
			// (get) Token: 0x0600042E RID: 1070
			public abstract IEnumerableCollection<INode<T>> Nodes { get; }

			// Token: 0x170001A0 RID: 416
			// (get) Token: 0x0600042F RID: 1071 RVA: 0x00014160 File Offset: 0x00013160
			public virtual IEnumerableCollection<T> Values
			{
				get
				{
					return new NodeTree<T>.BaseEnumerableCollectionPair.ValuesEnumerableCollection(this._Root.DataComparer, this.Nodes);
				}
			}

			// Token: 0x040001A0 RID: 416
			private NodeTree<T> _Root = null;

			// Token: 0x02000047 RID: 71
			protected abstract class BaseNodesEnumerableCollection : IEnumerableCollection<INode<T>>, IEnumerable<INode<T>>, ICollection, IEnumerable, IEnumerator<INode<T>>, IDisposable, IEnumerator
			{
				// Token: 0x170001A1 RID: 417
				// (get) Token: 0x06000430 RID: 1072 RVA: 0x00014188 File Offset: 0x00013188
				// (set) Token: 0x06000431 RID: 1073 RVA: 0x000141A0 File Offset: 0x000131A0
				protected NodeTree<T> Root
				{
					get
					{
						return this._Root;
					}
					set
					{
						this._Root = value;
					}
				}

				// Token: 0x170001A2 RID: 418
				// (get) Token: 0x06000432 RID: 1074 RVA: 0x000141AC File Offset: 0x000131AC
				// (set) Token: 0x06000433 RID: 1075 RVA: 0x000141C4 File Offset: 0x000131C4
				protected INode<T> CurrentNode
				{
					get
					{
						return this._CurrentNode;
					}
					set
					{
						this._CurrentNode = value;
					}
				}

				// Token: 0x170001A3 RID: 419
				// (get) Token: 0x06000434 RID: 1076 RVA: 0x000141D0 File Offset: 0x000131D0
				// (set) Token: 0x06000435 RID: 1077 RVA: 0x000141E8 File Offset: 0x000131E8
				protected bool BeforeFirst
				{
					get
					{
						return this._BeforeFirst;
					}
					set
					{
						this._BeforeFirst = value;
					}
				}

				// Token: 0x170001A4 RID: 420
				// (get) Token: 0x06000436 RID: 1078 RVA: 0x000141F4 File Offset: 0x000131F4
				// (set) Token: 0x06000437 RID: 1079 RVA: 0x0001420C File Offset: 0x0001320C
				protected bool AfterLast
				{
					get
					{
						return this._AfterLast;
					}
					set
					{
						this._AfterLast = value;
					}
				}

				// Token: 0x06000438 RID: 1080 RVA: 0x00014218 File Offset: 0x00013218
				protected BaseNodesEnumerableCollection(NodeTree<T> root)
				{
					this._Root = root;
					this._CurrentNode = root;
					this._Version = this._Root.Version;
				}

				// Token: 0x06000439 RID: 1081 RVA: 0x0001427C File Offset: 0x0001327C
				~BaseNodesEnumerableCollection()
				{
					this.Dispose(false);
				}

				// Token: 0x0600043A RID: 1082
				protected abstract NodeTree<T>.BaseEnumerableCollectionPair.BaseNodesEnumerableCollection CreateCopy();

				// Token: 0x170001A5 RID: 421
				// (get) Token: 0x0600043B RID: 1083 RVA: 0x000142B0 File Offset: 0x000132B0
				protected virtual bool HasChanged
				{
					get
					{
						return this._Root.HasChanged(this._Version);
					}
				}

				// Token: 0x0600043C RID: 1084 RVA: 0x000142D3 File Offset: 0x000132D3
				public void Dispose()
				{
					this.Dispose(true);
					GC.SuppressFinalize(this);
				}

				// Token: 0x0600043D RID: 1085 RVA: 0x000142E5 File Offset: 0x000132E5
				protected virtual void Dispose(bool disposing)
				{
				}

				// Token: 0x0600043E RID: 1086 RVA: 0x000142E8 File Offset: 0x000132E8
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x0600043F RID: 1087 RVA: 0x00014300 File Offset: 0x00013300
				public virtual IEnumerator<INode<T>> GetEnumerator()
				{
					return this;
				}

				// Token: 0x170001A6 RID: 422
				// (get) Token: 0x06000440 RID: 1088 RVA: 0x00014314 File Offset: 0x00013314
				public virtual int Count
				{
					get
					{
						NodeTree<T>.BaseEnumerableCollectionPair.BaseNodesEnumerableCollection baseNodesEnumerableCollection = this.CreateCopy();
						int num = 0;
						foreach (INode<T> node in baseNodesEnumerableCollection)
						{
							num++;
						}
						return num;
					}
				}

				// Token: 0x170001A7 RID: 423
				// (get) Token: 0x06000441 RID: 1089 RVA: 0x0001437C File Offset: 0x0001337C
				public virtual bool IsSynchronized
				{
					get
					{
						return false;
					}
				}

				// Token: 0x170001A8 RID: 424
				// (get) Token: 0x06000442 RID: 1090 RVA: 0x00014390 File Offset: 0x00013390
				public virtual object SyncRoot
				{
					get
					{
						return this._SyncRoot;
					}
				}

				// Token: 0x06000443 RID: 1091 RVA: 0x000143A8 File Offset: 0x000133A8
				void ICollection.CopyTo(Array array, int index)
				{
					if (array == null)
					{
						throw new ArgumentNullException("array");
					}
					if (array.Rank > 1)
					{
						throw new ArgumentException("array is multidimensional", "array");
					}
					if (index < 0)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					int count = this.Count;
					if (count > 0 && index >= array.Length)
					{
						throw new ArgumentException("index is out of bounds", "index");
					}
					if (index + count > array.Length)
					{
						throw new ArgumentException("Not enough space in array", "array");
					}
					NodeTree<T>.BaseEnumerableCollectionPair.BaseNodesEnumerableCollection baseNodesEnumerableCollection = this.CreateCopy();
					foreach (INode<T> value in baseNodesEnumerableCollection)
					{
						array.SetValue(value, index++);
					}
				}

				// Token: 0x06000444 RID: 1092 RVA: 0x000144AC File Offset: 0x000134AC
				public virtual void CopyTo(T[] array, int index)
				{
					((ICollection)this).CopyTo(array, index);
				}

				// Token: 0x06000445 RID: 1093 RVA: 0x000144B8 File Offset: 0x000134B8
				public virtual bool Contains(INode<T> item)
				{
					NodeTree<T>.BaseEnumerableCollectionPair.BaseNodesEnumerableCollection baseNodesEnumerableCollection = this.CreateCopy();
					IEqualityComparer<INode<T>> @default = EqualityComparer<INode<T>>.Default;
					foreach (INode<T> x in baseNodesEnumerableCollection)
					{
						if (@default.Equals(x, item))
						{
							return true;
						}
					}
					return false;
				}

				// Token: 0x170001A9 RID: 425
				// (get) Token: 0x06000446 RID: 1094 RVA: 0x00014538 File Offset: 0x00013538
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06000447 RID: 1095 RVA: 0x00014550 File Offset: 0x00013550
				public virtual void Reset()
				{
					if (this.HasChanged)
					{
						throw new InvalidOperationException("Tree has been modified.");
					}
					this._CurrentNode = this._Root;
					this._BeforeFirst = true;
					this._AfterLast = false;
				}

				// Token: 0x06000448 RID: 1096 RVA: 0x00014590 File Offset: 0x00013590
				public virtual bool MoveNext()
				{
					if (this.HasChanged)
					{
						throw new InvalidOperationException("Tree has been modified.");
					}
					this._BeforeFirst = false;
					return true;
				}

				// Token: 0x170001AA RID: 426
				// (get) Token: 0x06000449 RID: 1097 RVA: 0x000145C4 File Offset: 0x000135C4
				public virtual INode<T> Current
				{
					get
					{
						if (this._BeforeFirst)
						{
							throw new InvalidOperationException("Enumeration has not started.");
						}
						if (this._AfterLast)
						{
							throw new InvalidOperationException("Enumeration has finished.");
						}
						return this._CurrentNode;
					}
				}

				// Token: 0x040001A1 RID: 417
				private int _Version = 0;

				// Token: 0x040001A2 RID: 418
				private object _SyncRoot = new object();

				// Token: 0x040001A3 RID: 419
				private NodeTree<T> _Root = null;

				// Token: 0x040001A4 RID: 420
				private INode<T> _CurrentNode = null;

				// Token: 0x040001A5 RID: 421
				private bool _BeforeFirst = true;

				// Token: 0x040001A6 RID: 422
				private bool _AfterLast = false;
			}

			// Token: 0x02000048 RID: 72
			private class ValuesEnumerableCollection : IEnumerableCollection<T>, IEnumerable<T>, ICollection, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
			{
				// Token: 0x0600044A RID: 1098 RVA: 0x0001460C File Offset: 0x0001360C
				public ValuesEnumerableCollection(IEqualityComparer<T> dataComparer, IEnumerableCollection<INode<T>> nodes)
				{
					this._DataComparer = dataComparer;
					this._Nodes = nodes;
					this._Enumerator = this._Nodes.GetEnumerator();
				}

				// Token: 0x0600044B RID: 1099 RVA: 0x00014636 File Offset: 0x00013636
				protected ValuesEnumerableCollection(NodeTree<T>.BaseEnumerableCollectionPair.ValuesEnumerableCollection o)
				{
					this._Nodes = o._Nodes;
					this._Enumerator = this._Nodes.GetEnumerator();
				}

				// Token: 0x0600044C RID: 1100 RVA: 0x00014660 File Offset: 0x00013660
				protected virtual NodeTree<T>.BaseEnumerableCollectionPair.ValuesEnumerableCollection CreateCopy()
				{
					return new NodeTree<T>.BaseEnumerableCollectionPair.ValuesEnumerableCollection(this);
				}

				// Token: 0x0600044D RID: 1101 RVA: 0x00014678 File Offset: 0x00013678
				~ValuesEnumerableCollection()
				{
					this.Dispose(false);
				}

				// Token: 0x0600044E RID: 1102 RVA: 0x000146AC File Offset: 0x000136AC
				public void Dispose()
				{
					this.Dispose(true);
					GC.SuppressFinalize(this);
				}

				// Token: 0x0600044F RID: 1103 RVA: 0x000146BE File Offset: 0x000136BE
				protected virtual void Dispose(bool disposing)
				{
				}

				// Token: 0x06000450 RID: 1104 RVA: 0x000146C4 File Offset: 0x000136C4
				IEnumerator IEnumerable.GetEnumerator()
				{
					return this.GetEnumerator();
				}

				// Token: 0x06000451 RID: 1105 RVA: 0x000146DC File Offset: 0x000136DC
				public virtual IEnumerator<T> GetEnumerator()
				{
					return this;
				}

				// Token: 0x170001AB RID: 427
				// (get) Token: 0x06000452 RID: 1106 RVA: 0x000146F0 File Offset: 0x000136F0
				public virtual int Count
				{
					get
					{
						return this._Nodes.Count;
					}
				}

				// Token: 0x170001AC RID: 428
				// (get) Token: 0x06000453 RID: 1107 RVA: 0x00014710 File Offset: 0x00013710
				public virtual bool IsSynchronized
				{
					get
					{
						return false;
					}
				}

				// Token: 0x170001AD RID: 429
				// (get) Token: 0x06000454 RID: 1108 RVA: 0x00014724 File Offset: 0x00013724
				public virtual object SyncRoot
				{
					get
					{
						return this._Nodes.SyncRoot;
					}
				}

				// Token: 0x06000455 RID: 1109 RVA: 0x00014744 File Offset: 0x00013744
				public virtual void CopyTo(Array array, int index)
				{
					if (array == null)
					{
						throw new ArgumentNullException("array");
					}
					if (array.Rank > 1)
					{
						throw new ArgumentException("array is multidimensional", "array");
					}
					if (index < 0)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					int count = this.Count;
					if (count > 0 && index >= array.Length)
					{
						throw new ArgumentException("index is out of bounds", "index");
					}
					if (index + count > array.Length)
					{
						throw new ArgumentException("Not enough space in array", "array");
					}
					NodeTree<T>.BaseEnumerableCollectionPair.ValuesEnumerableCollection valuesEnumerableCollection = this.CreateCopy();
					foreach (T t in valuesEnumerableCollection)
					{
						array.SetValue(t, index++);
					}
				}

				// Token: 0x06000456 RID: 1110 RVA: 0x0001484C File Offset: 0x0001384C
				public virtual bool Contains(T item)
				{
					NodeTree<T>.BaseEnumerableCollectionPair.ValuesEnumerableCollection valuesEnumerableCollection = this.CreateCopy();
					foreach (T x in valuesEnumerableCollection)
					{
						if (this._DataComparer.Equals(x, item))
						{
							return true;
						}
					}
					return false;
				}

				// Token: 0x170001AE RID: 430
				// (get) Token: 0x06000457 RID: 1111 RVA: 0x000148C4 File Offset: 0x000138C4
				object IEnumerator.Current
				{
					get
					{
						return this.Current;
					}
				}

				// Token: 0x06000458 RID: 1112 RVA: 0x000148E1 File Offset: 0x000138E1
				public virtual void Reset()
				{
					this._Enumerator.Reset();
				}

				// Token: 0x06000459 RID: 1113 RVA: 0x000148F0 File Offset: 0x000138F0
				public virtual bool MoveNext()
				{
					return this._Enumerator.MoveNext();
				}

				// Token: 0x170001AF RID: 431
				// (get) Token: 0x0600045A RID: 1114 RVA: 0x00014910 File Offset: 0x00013910
				public virtual T Current
				{
					get
					{
						T result;
						if (this._Enumerator == null)
						{
							result = default(T);
						}
						else if (this._Enumerator.Current == null)
						{
							result = default(T);
						}
						else
						{
							result = this._Enumerator.Current.Data;
						}
						return result;
					}
				}

				// Token: 0x040001A7 RID: 423
				private IEqualityComparer<T> _DataComparer;

				// Token: 0x040001A8 RID: 424
				private IEnumerableCollection<INode<T>> _Nodes;

				// Token: 0x040001A9 RID: 425
				private IEnumerator<INode<T>> _Enumerator;
			}
		}

		// Token: 0x02000049 RID: 73
		protected class AllEnumerator : NodeTree<T>.BaseEnumerableCollectionPair
		{
			// Token: 0x0600045B RID: 1115 RVA: 0x00014971 File Offset: 0x00013971
			public AllEnumerator(NodeTree<T> root) : base(root)
			{
			}

			// Token: 0x170001B0 RID: 432
			// (get) Token: 0x0600045C RID: 1116 RVA: 0x00014980 File Offset: 0x00013980
			public override IEnumerableCollection<INode<T>> Nodes
			{
				get
				{
					return new NodeTree<T>.AllEnumerator.NodesEnumerableCollection(base.Root);
				}
			}

			// Token: 0x0200004A RID: 74
			protected class NodesEnumerableCollection : NodeTree<T>.BaseEnumerableCollectionPair.BaseNodesEnumerableCollection
			{
				// Token: 0x0600045D RID: 1117 RVA: 0x0001499D File Offset: 0x0001399D
				public NodesEnumerableCollection(NodeTree<T> root) : base(root)
				{
				}

				// Token: 0x0600045E RID: 1118 RVA: 0x000149B0 File Offset: 0x000139B0
				protected NodesEnumerableCollection(NodeTree<T>.AllEnumerator.NodesEnumerableCollection o) : base(o.Root)
				{
				}

				// Token: 0x0600045F RID: 1119 RVA: 0x000149C8 File Offset: 0x000139C8
				protected override NodeTree<T>.BaseEnumerableCollectionPair.BaseNodesEnumerableCollection CreateCopy()
				{
					return new NodeTree<T>.AllEnumerator.NodesEnumerableCollection(this);
				}

				// Token: 0x06000460 RID: 1120 RVA: 0x000149E0 File Offset: 0x000139E0
				public override void Reset()
				{
					base.Reset();
					this._First = true;
				}

				// Token: 0x06000461 RID: 1121 RVA: 0x000149F4 File Offset: 0x000139F4
				public override bool MoveNext()
				{
					if (base.MoveNext())
					{
						if (base.CurrentNode == null)
						{
							throw new InvalidOperationException("Current is null");
						}
						if (base.CurrentNode.IsRoot)
						{
							base.CurrentNode = base.CurrentNode.Child;
							if (base.CurrentNode == null)
							{
								goto IL_116;
							}
						}
						if (this._First)
						{
							this._First = false;
							return true;
						}
						if (base.CurrentNode.Child != null)
						{
							base.CurrentNode = base.CurrentNode.Child;
							return true;
						}
						while (base.CurrentNode.Parent != null)
						{
							if (base.CurrentNode == base.Root)
							{
								break;
							}
							if (base.CurrentNode.Next != null)
							{
								base.CurrentNode = base.CurrentNode.Next;
								return true;
							}
							base.CurrentNode = base.CurrentNode.Parent;
						}
					}
					IL_116:
					base.AfterLast = true;
					return false;
				}

				// Token: 0x040001AA RID: 426
				private bool _First = true;
			}
		}

		// Token: 0x0200004B RID: 75
		private class AllChildrenEnumerator : NodeTree<T>.BaseEnumerableCollectionPair
		{
			// Token: 0x06000462 RID: 1122 RVA: 0x00014B24 File Offset: 0x00013B24
			public AllChildrenEnumerator(NodeTree<T> root) : base(root)
			{
			}

			// Token: 0x170001B1 RID: 433
			// (get) Token: 0x06000463 RID: 1123 RVA: 0x00014B30 File Offset: 0x00013B30
			public override IEnumerableCollection<INode<T>> Nodes
			{
				get
				{
					return new NodeTree<T>.AllChildrenEnumerator.NodesEnumerableCollection(base.Root);
				}
			}

			// Token: 0x0200004C RID: 76
			protected class NodesEnumerableCollection : NodeTree<T>.BaseEnumerableCollectionPair.BaseNodesEnumerableCollection
			{
				// Token: 0x06000464 RID: 1124 RVA: 0x00014B4D File Offset: 0x00013B4D
				public NodesEnumerableCollection(NodeTree<T> root) : base(root)
				{
				}

				// Token: 0x06000465 RID: 1125 RVA: 0x00014B59 File Offset: 0x00013B59
				protected NodesEnumerableCollection(NodeTree<T>.AllChildrenEnumerator.NodesEnumerableCollection o) : base(o.Root)
				{
				}

				// Token: 0x06000466 RID: 1126 RVA: 0x00014B6C File Offset: 0x00013B6C
				protected override NodeTree<T>.BaseEnumerableCollectionPair.BaseNodesEnumerableCollection CreateCopy()
				{
					return new NodeTree<T>.AllChildrenEnumerator.NodesEnumerableCollection(this);
				}

				// Token: 0x06000467 RID: 1127 RVA: 0x00014B84 File Offset: 0x00013B84
				public override bool MoveNext()
				{
					if (base.MoveNext())
					{
						if (base.CurrentNode == null)
						{
							throw new InvalidOperationException("Current is null");
						}
						if (base.CurrentNode.Child != null)
						{
							base.CurrentNode = base.CurrentNode.Child;
							return true;
						}
						while (base.CurrentNode.Parent != null)
						{
							if (base.CurrentNode == base.Root)
							{
								break;
							}
							if (base.CurrentNode.Next != null)
							{
								base.CurrentNode = base.CurrentNode.Next;
								return true;
							}
							base.CurrentNode = base.CurrentNode.Parent;
						}
					}
					base.AfterLast = true;
					return false;
				}
			}
		}

		// Token: 0x0200004D RID: 77
		private class DirectChildrenEnumerator : NodeTree<T>.BaseEnumerableCollectionPair
		{
			// Token: 0x06000468 RID: 1128 RVA: 0x00014C5D File Offset: 0x00013C5D
			public DirectChildrenEnumerator(NodeTree<T> root) : base(root)
			{
			}

			// Token: 0x170001B2 RID: 434
			// (get) Token: 0x06000469 RID: 1129 RVA: 0x00014C6C File Offset: 0x00013C6C
			public override IEnumerableCollection<INode<T>> Nodes
			{
				get
				{
					return new NodeTree<T>.DirectChildrenEnumerator.NodesEnumerableCollection(base.Root);
				}
			}

			// Token: 0x0200004E RID: 78
			protected class NodesEnumerableCollection : NodeTree<T>.BaseEnumerableCollectionPair.BaseNodesEnumerableCollection
			{
				// Token: 0x0600046A RID: 1130 RVA: 0x00014C89 File Offset: 0x00013C89
				public NodesEnumerableCollection(NodeTree<T> root) : base(root)
				{
				}

				// Token: 0x0600046B RID: 1131 RVA: 0x00014C95 File Offset: 0x00013C95
				protected NodesEnumerableCollection(NodeTree<T>.DirectChildrenEnumerator.NodesEnumerableCollection o) : base(o.Root)
				{
				}

				// Token: 0x0600046C RID: 1132 RVA: 0x00014CA8 File Offset: 0x00013CA8
				protected override NodeTree<T>.BaseEnumerableCollectionPair.BaseNodesEnumerableCollection CreateCopy()
				{
					return new NodeTree<T>.DirectChildrenEnumerator.NodesEnumerableCollection(this);
				}

				// Token: 0x170001B3 RID: 435
				// (get) Token: 0x0600046D RID: 1133 RVA: 0x00014CC0 File Offset: 0x00013CC0
				public override int Count
				{
					get
					{
						return base.Root.DirectChildCount;
					}
				}

				// Token: 0x0600046E RID: 1134 RVA: 0x00014CE0 File Offset: 0x00013CE0
				public override bool MoveNext()
				{
					if (base.MoveNext())
					{
						if (base.CurrentNode == null)
						{
							throw new InvalidOperationException("Current is null");
						}
						if (base.CurrentNode == base.Root)
						{
							base.CurrentNode = base.Root.Child;
						}
						else
						{
							base.CurrentNode = base.CurrentNode.Next;
						}
						if (base.CurrentNode != null)
						{
							return true;
						}
					}
					base.AfterLast = true;
					return false;
				}
			}
		}

		// Token: 0x0200004F RID: 79
		private class DirectChildrenInReverseEnumerator : NodeTree<T>.BaseEnumerableCollectionPair
		{
			// Token: 0x0600046F RID: 1135 RVA: 0x00014D6E File Offset: 0x00013D6E
			public DirectChildrenInReverseEnumerator(NodeTree<T> root) : base(root)
			{
			}

			// Token: 0x170001B4 RID: 436
			// (get) Token: 0x06000470 RID: 1136 RVA: 0x00014D7C File Offset: 0x00013D7C
			public override IEnumerableCollection<INode<T>> Nodes
			{
				get
				{
					return new NodeTree<T>.DirectChildrenInReverseEnumerator.NodesEnumerableCollection(base.Root);
				}
			}

			// Token: 0x02000050 RID: 80
			protected class NodesEnumerableCollection : NodeTree<T>.BaseEnumerableCollectionPair.BaseNodesEnumerableCollection
			{
				// Token: 0x06000471 RID: 1137 RVA: 0x00014D99 File Offset: 0x00013D99
				public NodesEnumerableCollection(NodeTree<T> root) : base(root)
				{
				}

				// Token: 0x06000472 RID: 1138 RVA: 0x00014DA5 File Offset: 0x00013DA5
				protected NodesEnumerableCollection(NodeTree<T>.DirectChildrenInReverseEnumerator.NodesEnumerableCollection o) : base(o.Root)
				{
				}

				// Token: 0x06000473 RID: 1139 RVA: 0x00014DB8 File Offset: 0x00013DB8
				protected override NodeTree<T>.BaseEnumerableCollectionPair.BaseNodesEnumerableCollection CreateCopy()
				{
					return new NodeTree<T>.DirectChildrenInReverseEnumerator.NodesEnumerableCollection(this);
				}

				// Token: 0x170001B5 RID: 437
				// (get) Token: 0x06000474 RID: 1140 RVA: 0x00014DD0 File Offset: 0x00013DD0
				public override int Count
				{
					get
					{
						return base.Root.DirectChildCount;
					}
				}

				// Token: 0x06000475 RID: 1141 RVA: 0x00014DF0 File Offset: 0x00013DF0
				public override bool MoveNext()
				{
					if (base.MoveNext())
					{
						if (base.CurrentNode == null)
						{
							throw new InvalidOperationException("Current is null");
						}
						if (base.CurrentNode == base.Root)
						{
							base.CurrentNode = base.Root.LastChild;
						}
						else
						{
							base.CurrentNode = base.CurrentNode.Previous;
						}
						if (base.CurrentNode != null)
						{
							return true;
						}
					}
					base.AfterLast = true;
					return false;
				}
			}
		}
	}
}

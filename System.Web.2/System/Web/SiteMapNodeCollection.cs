using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace System.Web
{
	// Token: 0x020000F7 RID: 247
	public class SiteMapNodeCollection : IHierarchicalEnumerable, IEnumerable, IList, ICollection
	{
		// Token: 0x06000ECE RID: 3790 RVA: 0x0002A31C File Offset: 0x0002851C
		public SiteMapNodeCollection()
		{
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x0002A32C File Offset: 0x0002852C
		public SiteMapNodeCollection(int capacity)
		{
			this._initialSize = capacity;
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0002A343 File Offset: 0x00028543
		public SiteMapNodeCollection(SiteMapNode value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this._initialSize = 1;
			this.List.Add(value);
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0002A375 File Offset: 0x00028575
		public SiteMapNodeCollection(SiteMapNode[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this._initialSize = value.Length;
			this.AddRangeInternal(value);
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0002A3A3 File Offset: 0x000285A3
		public SiteMapNodeCollection(SiteMapNodeCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this._initialSize = value.Count;
			this.AddRangeInternal(value);
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x0002A3D4 File Offset: 0x000285D4
		public virtual int Count
		{
			get
			{
				return this.List.Count;
			}
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x0002A3E1 File Offset: 0x000285E1
		public virtual bool IsSynchronized
		{
			get
			{
				return this.List.IsSynchronized;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x0002A3EE File Offset: 0x000285EE
		public virtual object SyncRoot
		{
			get
			{
				return this.List.SyncRoot;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x0002A3FB File Offset: 0x000285FB
		private ArrayList List
		{
			get
			{
				if (this._innerList == null)
				{
					this._innerList = new ArrayList(this._initialSize);
				}
				return this._innerList;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x00007722 File Offset: 0x00005922
		public virtual bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000529 RID: 1321
		public virtual SiteMapNode this[int index]
		{
			get
			{
				return (SiteMapNode)this.List[index];
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.List[index] = value;
			}
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0002A44C File Offset: 0x0002864C
		public virtual int Add(SiteMapNode value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return this.List.Add(value);
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0002A468 File Offset: 0x00028668
		public virtual void AddRange(SiteMapNode[] value)
		{
			this.AddRangeInternal(value);
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0002A468 File Offset: 0x00028668
		public virtual void AddRange(SiteMapNodeCollection value)
		{
			this.AddRangeInternal(value);
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0002A471 File Offset: 0x00028671
		private void AddRangeInternal(IList value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.List.AddRange(value);
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0002A48D File Offset: 0x0002868D
		public virtual void Clear()
		{
			this.List.Clear();
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0002A49A File Offset: 0x0002869A
		public virtual bool Contains(SiteMapNode value)
		{
			return this.List.Contains(value);
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0002A4A8 File Offset: 0x000286A8
		public virtual void CopyTo(SiteMapNode[] array, int index)
		{
			this.CopyToInternal(array, index);
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0002A4B2 File Offset: 0x000286B2
		internal virtual void CopyToInternal(Array array, int index)
		{
			this.List.CopyTo(array, index);
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0002A4C1 File Offset: 0x000286C1
		public SiteMapDataSourceView GetDataSourceView(SiteMapDataSource owner, string viewName)
		{
			return new SiteMapDataSourceView(owner, viewName, this);
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0002A4CB File Offset: 0x000286CB
		public virtual IEnumerator GetEnumerator()
		{
			return this.List.GetEnumerator();
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0002A4D8 File Offset: 0x000286D8
		public SiteMapHierarchicalDataSourceView GetHierarchicalDataSourceView()
		{
			return new SiteMapHierarchicalDataSourceView(this);
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0002A4E0 File Offset: 0x000286E0
		public virtual IHierarchyData GetHierarchyData(object enumeratedItem)
		{
			return enumeratedItem as IHierarchyData;
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0002A4E8 File Offset: 0x000286E8
		public virtual int IndexOf(SiteMapNode value)
		{
			return this.List.IndexOf(value);
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0002A4F6 File Offset: 0x000286F6
		public virtual void Insert(int index, SiteMapNode value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.List.Insert(index, value);
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0002A513 File Offset: 0x00028713
		protected virtual void OnValidate(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (!(value is SiteMapNode))
			{
				throw new ArgumentException(SR.GetString("SiteMapNodeCollection_Invalid_Type", new object[]
				{
					value.GetType().ToString()
				}));
			}
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0002A54F File Offset: 0x0002874F
		public static SiteMapNodeCollection ReadOnly(SiteMapNodeCollection collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			return new SiteMapNodeCollection.ReadOnlySiteMapNodeCollection(collection);
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0002A565 File Offset: 0x00028765
		public virtual void Remove(SiteMapNode value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.List.Remove(value);
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0002A581 File Offset: 0x00028781
		public virtual void RemoveAt(int index)
		{
			this.List.RemoveAt(index);
		}

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06000EED RID: 3821 RVA: 0x0002A58F File Offset: 0x0002878F
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x0002A597 File Offset: 0x00028797
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.IsSynchronized;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x0002A59F File Offset: 0x0002879F
		object ICollection.SyncRoot
		{
			get
			{
				return this.SyncRoot;
			}
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0002A4A8 File Offset: 0x000286A8
		void ICollection.CopyTo(Array array, int index)
		{
			this.CopyToInternal(array, index);
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0002A5A7 File Offset: 0x000287A7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0002A5AF File Offset: 0x000287AF
		IHierarchyData IHierarchicalEnumerable.GetHierarchyData(object enumeratedItem)
		{
			return this.GetHierarchyData(enumeratedItem);
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x0002A5B8 File Offset: 0x000287B8
		bool IList.IsFixedSize
		{
			get
			{
				return this.IsFixedSize;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x0002A5C0 File Offset: 0x000287C0
		bool IList.IsReadOnly
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		// Token: 0x1700052F RID: 1327
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this.OnValidate(value);
				this[index] = (SiteMapNode)value;
			}
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0002A5E7 File Offset: 0x000287E7
		int IList.Add(object value)
		{
			this.OnValidate(value);
			return this.Add((SiteMapNode)value);
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0002A5FC File Offset: 0x000287FC
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0002A604 File Offset: 0x00028804
		bool IList.Contains(object value)
		{
			this.OnValidate(value);
			return this.Contains((SiteMapNode)value);
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0002A619 File Offset: 0x00028819
		int IList.IndexOf(object value)
		{
			this.OnValidate(value);
			return this.IndexOf((SiteMapNode)value);
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0002A62E File Offset: 0x0002882E
		void IList.Insert(int index, object value)
		{
			this.OnValidate(value);
			this.Insert(index, (SiteMapNode)value);
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x0002A644 File Offset: 0x00028844
		void IList.Remove(object value)
		{
			this.OnValidate(value);
			this.Remove((SiteMapNode)value);
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x0002A659 File Offset: 0x00028859
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x040005B9 RID: 1465
		internal static SiteMapNodeCollection Empty = new SiteMapNodeCollection.ReadOnlySiteMapNodeCollection(new SiteMapNodeCollection());

		// Token: 0x040005BA RID: 1466
		private int _initialSize = 10;

		// Token: 0x040005BB RID: 1467
		private ArrayList _innerList;

		// Token: 0x020008EB RID: 2283
		private sealed class ReadOnlySiteMapNodeCollection : SiteMapNodeCollection
		{
			// Token: 0x06006868 RID: 26728 RVA: 0x00173F6A File Offset: 0x0017216A
			internal ReadOnlySiteMapNodeCollection(SiteMapNodeCollection collection)
			{
				if (collection == null)
				{
					throw new ArgumentNullException("collection");
				}
				this._internalCollection = collection;
			}

			// Token: 0x17001CFE RID: 7422
			// (get) Token: 0x06006869 RID: 26729 RVA: 0x00173F87 File Offset: 0x00172187
			public override int Count
			{
				get
				{
					return this._internalCollection.Count;
				}
			}

			// Token: 0x17001CFF RID: 7423
			// (get) Token: 0x0600686A RID: 26730 RVA: 0x000097B7 File Offset: 0x000079B7
			public override bool IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001D00 RID: 7424
			// (get) Token: 0x0600686B RID: 26731 RVA: 0x000097B7 File Offset: 0x000079B7
			public override bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001D01 RID: 7425
			// (get) Token: 0x0600686C RID: 26732 RVA: 0x00173F94 File Offset: 0x00172194
			public override bool IsSynchronized
			{
				get
				{
					return this._internalCollection.IsSynchronized;
				}
			}

			// Token: 0x17001D02 RID: 7426
			// (get) Token: 0x0600686D RID: 26733 RVA: 0x00173FA1 File Offset: 0x001721A1
			public override object SyncRoot
			{
				get
				{
					return this._internalCollection.SyncRoot;
				}
			}

			// Token: 0x0600686E RID: 26734 RVA: 0x00173FAE File Offset: 0x001721AE
			public override int Add(SiteMapNode value)
			{
				throw new NotSupportedException(SR.GetString("Collection_readonly"));
			}

			// Token: 0x0600686F RID: 26735 RVA: 0x00173FAE File Offset: 0x001721AE
			public override void AddRange(SiteMapNode[] value)
			{
				throw new NotSupportedException(SR.GetString("Collection_readonly"));
			}

			// Token: 0x06006870 RID: 26736 RVA: 0x00173FAE File Offset: 0x001721AE
			public override void AddRange(SiteMapNodeCollection value)
			{
				throw new NotSupportedException(SR.GetString("Collection_readonly"));
			}

			// Token: 0x06006871 RID: 26737 RVA: 0x00173FAE File Offset: 0x001721AE
			public override void Clear()
			{
				throw new NotSupportedException(SR.GetString("Collection_readonly"));
			}

			// Token: 0x06006872 RID: 26738 RVA: 0x00173FBF File Offset: 0x001721BF
			public override bool Contains(SiteMapNode node)
			{
				return this._internalCollection.Contains(node);
			}

			// Token: 0x06006873 RID: 26739 RVA: 0x00173FCD File Offset: 0x001721CD
			internal override void CopyToInternal(Array array, int index)
			{
				this._internalCollection.List.CopyTo(array, index);
			}

			// Token: 0x17001D03 RID: 7427
			public override SiteMapNode this[int index]
			{
				get
				{
					return this._internalCollection[index];
				}
				set
				{
					throw new NotSupportedException(SR.GetString("Collection_readonly"));
				}
			}

			// Token: 0x06006876 RID: 26742 RVA: 0x00173FEF File Offset: 0x001721EF
			public override IEnumerator GetEnumerator()
			{
				return this._internalCollection.GetEnumerator();
			}

			// Token: 0x06006877 RID: 26743 RVA: 0x00173FFC File Offset: 0x001721FC
			public override int IndexOf(SiteMapNode value)
			{
				return this._internalCollection.IndexOf(value);
			}

			// Token: 0x06006878 RID: 26744 RVA: 0x00173FAE File Offset: 0x001721AE
			public override void Insert(int index, SiteMapNode value)
			{
				throw new NotSupportedException(SR.GetString("Collection_readonly"));
			}

			// Token: 0x06006879 RID: 26745 RVA: 0x00173FAE File Offset: 0x001721AE
			public override void Remove(SiteMapNode value)
			{
				throw new NotSupportedException(SR.GetString("Collection_readonly"));
			}

			// Token: 0x0600687A RID: 26746 RVA: 0x00173FAE File Offset: 0x001721AE
			public override void RemoveAt(int index)
			{
				throw new NotSupportedException(SR.GetString("Collection_readonly"));
			}

			// Token: 0x0400365A RID: 13914
			private SiteMapNodeCollection _internalCollection;
		}
	}
}

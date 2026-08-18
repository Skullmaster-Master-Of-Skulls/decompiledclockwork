using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Telerik.Web.UI.PivotGrid.Core
{
	// Token: 0x020006CA RID: 1738
	[CollectionDataContract]
	[Serializable]
	public class SetConditionHashCollection : IList, ICollection, IList<object>, ICollection<object>, IEnumerable<object>, IEnumerable
	{
		// Token: 0x06003E46 RID: 15942 RVA: 0x000C7805 File Offset: 0x000C5A05
		public SetConditionHashCollection()
		{
			this.hashSet = new HashSet<object>();
		}

		// Token: 0x06003E47 RID: 15943 RVA: 0x000C7818 File Offset: 0x000C5A18
		public SetConditionHashCollection(IEnumerable items)
		{
			this.hashSet = new HashSet<object>();
			foreach (object item in items)
			{
				this.hashSet.Add(item);
			}
		}

		// Token: 0x17001468 RID: 5224
		// (get) Token: 0x06003E48 RID: 15944 RVA: 0x000C7880 File Offset: 0x000C5A80
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001469 RID: 5225
		// (get) Token: 0x06003E49 RID: 15945 RVA: 0x000C7883 File Offset: 0x000C5A83
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700146A RID: 5226
		// (get) Token: 0x06003E4A RID: 15946 RVA: 0x000C7886 File Offset: 0x000C5A86
		public int Count
		{
			get
			{
				return this.hashSet.Count;
			}
		}

		// Token: 0x1700146B RID: 5227
		// (get) Token: 0x06003E4B RID: 15947 RVA: 0x000C7893 File Offset: 0x000C5A93
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700146C RID: 5228
		// (get) Token: 0x06003E4C RID: 15948 RVA: 0x000C7896 File Offset: 0x000C5A96
		public object SyncRoot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700146D RID: 5229
		public object this[int index]
		{
			get
			{
				throw new InvalidOperationException("Indexer is not supported.");
			}
			set
			{
				throw new InvalidOperationException("Indexer is not supported.");
			}
		}

		// Token: 0x06003E4F RID: 15951 RVA: 0x000C78B1 File Offset: 0x000C5AB1
		public int Add(object value)
		{
			this.hashSet.Add(value);
			return -1;
		}

		// Token: 0x06003E50 RID: 15952 RVA: 0x000C78C1 File Offset: 0x000C5AC1
		public void Clear()
		{
			this.hashSet.Clear();
		}

		// Token: 0x06003E51 RID: 15953 RVA: 0x000C78CE File Offset: 0x000C5ACE
		public bool Contains(object value)
		{
			return this.hashSet.Contains(value);
		}

		// Token: 0x06003E52 RID: 15954 RVA: 0x000C78DC File Offset: 0x000C5ADC
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "IndexOf", Justification = "Design choice.")]
		public int IndexOf(object value)
		{
			throw new InvalidOperationException("IndexOf is not supported.");
		}

		// Token: 0x06003E53 RID: 15955 RVA: 0x000C78E8 File Offset: 0x000C5AE8
		public void Insert(int index, object value)
		{
			throw new InvalidOperationException("Insert is not supported. Use Add instead.");
		}

		// Token: 0x06003E54 RID: 15956 RVA: 0x000C78F4 File Offset: 0x000C5AF4
		public void Remove(object value)
		{
			this.hashSet.Remove(value);
		}

		// Token: 0x06003E55 RID: 15957 RVA: 0x000C7903 File Offset: 0x000C5B03
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "RemoveAt", Justification = "Design choice.")]
		public void RemoveAt(int index)
		{
			throw new InvalidOperationException("RemoveAt is not supported. Use Remove instead.");
		}

		// Token: 0x06003E56 RID: 15958 RVA: 0x000C790F File Offset: 0x000C5B0F
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "CopyTo", Justification = "Design choice.")]
		public void CopyTo(Array array, int index)
		{
			throw new InvalidOperationException("CopyTo is not supported.");
		}

		// Token: 0x06003E57 RID: 15959 RVA: 0x000C791B File Offset: 0x000C5B1B
		public IEnumerator GetEnumerator()
		{
			return this.hashSet.GetEnumerator();
		}

		// Token: 0x06003E58 RID: 15960 RVA: 0x000C792D File Offset: 0x000C5B2D
		void ICollection<object>.Add(object item)
		{
			this.hashSet.Add(item);
		}

		// Token: 0x06003E59 RID: 15961 RVA: 0x000C793C File Offset: 0x000C5B3C
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "CopyTo", Justification = "Design choice.")]
		void ICollection<object>.CopyTo(object[] array, int arrayIndex)
		{
			throw new InvalidOperationException("CopyTo is not supported.");
		}

		// Token: 0x06003E5A RID: 15962 RVA: 0x000C7948 File Offset: 0x000C5B48
		bool ICollection<object>.Remove(object item)
		{
			return this.hashSet.Remove(item);
		}

		// Token: 0x06003E5B RID: 15963 RVA: 0x000C7956 File Offset: 0x000C5B56
		IEnumerator<object> IEnumerable<object>.GetEnumerator()
		{
			return this.hashSet.GetEnumerator();
		}

		// Token: 0x040010A1 RID: 4257
		private HashSet<object> hashSet;
	}
}

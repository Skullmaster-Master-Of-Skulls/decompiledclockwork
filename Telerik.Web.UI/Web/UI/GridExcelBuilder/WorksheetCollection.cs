using System;
using System.Collections;
using Telerik.Web.UI.GridExcelBuilder.Abstract;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B2F RID: 6959
	public class WorksheetCollection : IWorksheetCollection, IList, ICollection, IEnumerable
	{
		// Token: 0x06010D5E RID: 68958 RVA: 0x003BC36D File Offset: 0x003BA56D
		public WorksheetCollection(IList list)
		{
			this._list = list;
		}

		// Token: 0x06010D5F RID: 68959 RVA: 0x003BC37C File Offset: 0x003BA57C
		public WorksheetCollection() : this(new ArrayList())
		{
		}

		// Token: 0x06010D60 RID: 68960 RVA: 0x003BC389 File Offset: 0x003BA589
		public virtual int Add(WorksheetElement value)
		{
			return this._list.Add(value);
		}

		// Token: 0x06010D61 RID: 68961 RVA: 0x003BC397 File Offset: 0x003BA597
		public virtual void Clear()
		{
			this._list.Clear();
		}

		// Token: 0x06010D62 RID: 68962 RVA: 0x003BC3A4 File Offset: 0x003BA5A4
		public virtual bool Contains(WorksheetElement value)
		{
			return this._list.Contains(value);
		}

		// Token: 0x06010D63 RID: 68963 RVA: 0x003BC3B2 File Offset: 0x003BA5B2
		public virtual int IndexOf(WorksheetElement value)
		{
			return this._list.IndexOf(value);
		}

		// Token: 0x06010D64 RID: 68964 RVA: 0x003BC3C0 File Offset: 0x003BA5C0
		public virtual void Insert(int index, WorksheetElement value)
		{
			this._list.Insert(index, value);
		}

		// Token: 0x1700520C RID: 21004
		// (get) Token: 0x06010D65 RID: 68965 RVA: 0x003BC3CF File Offset: 0x003BA5CF
		public virtual bool IsFixedSize
		{
			get
			{
				return this._list.IsFixedSize;
			}
		}

		// Token: 0x1700520D RID: 21005
		// (get) Token: 0x06010D66 RID: 68966 RVA: 0x003BC3DC File Offset: 0x003BA5DC
		public virtual bool IsReadOnly
		{
			get
			{
				return this._list.IsReadOnly;
			}
		}

		// Token: 0x06010D67 RID: 68967 RVA: 0x003BC3E9 File Offset: 0x003BA5E9
		public virtual void Remove(WorksheetElement value)
		{
			this._list.Remove(value);
		}

		// Token: 0x06010D68 RID: 68968 RVA: 0x003BC3F7 File Offset: 0x003BA5F7
		public virtual void RemoveAt(int index)
		{
			this._list.RemoveAt(index);
		}

		// Token: 0x1700520E RID: 21006
		public virtual WorksheetElement this[int index]
		{
			get
			{
				if (index > this._list.Count)
				{
					throw new IndexOutOfRangeException();
				}
				return (WorksheetElement)this._list[index];
			}
			set
			{
				if (index > this._list.Count)
				{
					throw new IndexOutOfRangeException();
				}
				this._list[index] = value;
			}
		}

		// Token: 0x06010D6B RID: 68971 RVA: 0x003BC44F File Offset: 0x003BA64F
		int IList.Add(object value)
		{
			return this.Add((WorksheetElement)value);
		}

		// Token: 0x06010D6C RID: 68972 RVA: 0x003BC45D File Offset: 0x003BA65D
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x06010D6D RID: 68973 RVA: 0x003BC465 File Offset: 0x003BA665
		bool IList.Contains(object value)
		{
			return this.Contains((WorksheetElement)value);
		}

		// Token: 0x06010D6E RID: 68974 RVA: 0x003BC473 File Offset: 0x003BA673
		int IList.IndexOf(object value)
		{
			return this.IndexOf((WorksheetElement)value);
		}

		// Token: 0x06010D6F RID: 68975 RVA: 0x003BC481 File Offset: 0x003BA681
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (WorksheetElement)value);
		}

		// Token: 0x1700520F RID: 21007
		// (get) Token: 0x06010D70 RID: 68976 RVA: 0x003BC490 File Offset: 0x003BA690
		bool IList.IsFixedSize
		{
			get
			{
				return this.IsFixedSize;
			}
		}

		// Token: 0x17005210 RID: 21008
		// (get) Token: 0x06010D71 RID: 68977 RVA: 0x003BC498 File Offset: 0x003BA698
		bool IList.IsReadOnly
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		// Token: 0x06010D72 RID: 68978 RVA: 0x003BC4A0 File Offset: 0x003BA6A0
		void IList.Remove(object value)
		{
			this.Remove((WorksheetElement)value);
		}

		// Token: 0x06010D73 RID: 68979 RVA: 0x003BC4AE File Offset: 0x003BA6AE
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x17005211 RID: 21009
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (WorksheetElement)value;
			}
		}

		// Token: 0x06010D76 RID: 68982 RVA: 0x003BC4CF File Offset: 0x003BA6CF
		public virtual void CopyTo(WorksheetElement[] array, int index)
		{
			this._list.CopyTo(array, index);
		}

		// Token: 0x17005212 RID: 21010
		// (get) Token: 0x06010D77 RID: 68983 RVA: 0x003BC4DE File Offset: 0x003BA6DE
		public virtual int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x17005213 RID: 21011
		// (get) Token: 0x06010D78 RID: 68984 RVA: 0x003BC4EB File Offset: 0x003BA6EB
		public virtual bool IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		// Token: 0x17005214 RID: 21012
		// (get) Token: 0x06010D79 RID: 68985 RVA: 0x003BC4F8 File Offset: 0x003BA6F8
		public virtual object SyncRoot
		{
			get
			{
				return this._list.SyncRoot;
			}
		}

		// Token: 0x06010D7A RID: 68986 RVA: 0x003BC505 File Offset: 0x003BA705
		void ICollection.CopyTo(Array array, int index)
		{
			this.CopyTo((WorksheetElement[])array, index);
		}

		// Token: 0x17005215 RID: 21013
		// (get) Token: 0x06010D7B RID: 68987 RVA: 0x003BC514 File Offset: 0x003BA714
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x17005216 RID: 21014
		// (get) Token: 0x06010D7C RID: 68988 RVA: 0x003BC51C File Offset: 0x003BA71C
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.IsSynchronized;
			}
		}

		// Token: 0x17005217 RID: 21015
		// (get) Token: 0x06010D7D RID: 68989 RVA: 0x003BC524 File Offset: 0x003BA724
		object ICollection.SyncRoot
		{
			get
			{
				return this.SyncRoot;
			}
		}

		// Token: 0x06010D7E RID: 68990 RVA: 0x003BC52C File Offset: 0x003BA72C
		public virtual IEnumerator GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x06010D7F RID: 68991 RVA: 0x003BC539 File Offset: 0x003BA739
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04004B47 RID: 19271
		private IList _list;
	}
}

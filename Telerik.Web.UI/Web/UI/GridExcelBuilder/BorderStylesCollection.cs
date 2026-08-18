using System;
using System.Collections;

namespace Telerik.Web.UI.GridExcelBuilder
{
	// Token: 0x02001B0F RID: 6927
	public class BorderStylesCollection : IBorderStylesCollection, IList, ICollection, IEnumerable
	{
		// Token: 0x06010C04 RID: 68612 RVA: 0x003B9058 File Offset: 0x003B7258
		public BorderStylesCollection() : this(new ArrayList())
		{
		}

		// Token: 0x06010C05 RID: 68613 RVA: 0x003B9065 File Offset: 0x003B7265
		public BorderStylesCollection(IList list)
		{
			this._list = list;
		}

		// Token: 0x06010C06 RID: 68614 RVA: 0x003B9074 File Offset: 0x003B7274
		public virtual int Add(BorderStyles value)
		{
			return this._list.Add(value);
		}

		// Token: 0x06010C07 RID: 68615 RVA: 0x003B9082 File Offset: 0x003B7282
		public virtual void Clear()
		{
			this._list.Clear();
		}

		// Token: 0x06010C08 RID: 68616 RVA: 0x003B908F File Offset: 0x003B728F
		public virtual bool Contains(BorderStyles value)
		{
			return this._list.Contains(value);
		}

		// Token: 0x06010C09 RID: 68617 RVA: 0x003B909D File Offset: 0x003B729D
		public virtual int IndexOf(BorderStyles value)
		{
			return this._list.IndexOf(value);
		}

		// Token: 0x06010C0A RID: 68618 RVA: 0x003B90AB File Offset: 0x003B72AB
		public virtual void Insert(int index, BorderStyles value)
		{
			this._list.Insert(index, value);
		}

		// Token: 0x1700518A RID: 20874
		// (get) Token: 0x06010C0B RID: 68619 RVA: 0x003B90BA File Offset: 0x003B72BA
		public virtual bool IsFixedSize
		{
			get
			{
				return this._list.IsFixedSize;
			}
		}

		// Token: 0x1700518B RID: 20875
		// (get) Token: 0x06010C0C RID: 68620 RVA: 0x003B90C7 File Offset: 0x003B72C7
		public virtual bool IsReadOnly
		{
			get
			{
				return this._list.IsReadOnly;
			}
		}

		// Token: 0x06010C0D RID: 68621 RVA: 0x003B90D4 File Offset: 0x003B72D4
		public virtual void Remove(BorderStyles value)
		{
			this._list.Remove(value);
		}

		// Token: 0x06010C0E RID: 68622 RVA: 0x003B90E2 File Offset: 0x003B72E2
		public virtual void RemoveAt(int index)
		{
			this._list.RemoveAt(index);
		}

		// Token: 0x1700518C RID: 20876
		public virtual BorderStyles this[int index]
		{
			get
			{
				if (index > this._list.Count)
				{
					throw new ArgumentOutOfRangeException("Index out of range.");
				}
				return (BorderStyles)this._list[index];
			}
			set
			{
				if (index > this._list.Count)
				{
					throw new ArgumentOutOfRangeException("Index out of range.");
				}
				this._list[index] = value;
			}
		}

		// Token: 0x06010C11 RID: 68625 RVA: 0x003B9144 File Offset: 0x003B7344
		int IList.Add(object value)
		{
			return this.Add((BorderStyles)value);
		}

		// Token: 0x06010C12 RID: 68626 RVA: 0x003B9152 File Offset: 0x003B7352
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x06010C13 RID: 68627 RVA: 0x003B915A File Offset: 0x003B735A
		bool IList.Contains(object value)
		{
			return this.Contains((BorderStyles)value);
		}

		// Token: 0x06010C14 RID: 68628 RVA: 0x003B9168 File Offset: 0x003B7368
		int IList.IndexOf(object value)
		{
			return this.IndexOf((BorderStyles)value);
		}

		// Token: 0x06010C15 RID: 68629 RVA: 0x003B9176 File Offset: 0x003B7376
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (BorderStyles)value);
		}

		// Token: 0x1700518D RID: 20877
		// (get) Token: 0x06010C16 RID: 68630 RVA: 0x003B9185 File Offset: 0x003B7385
		bool IList.IsFixedSize
		{
			get
			{
				return this.IsFixedSize;
			}
		}

		// Token: 0x1700518E RID: 20878
		// (get) Token: 0x06010C17 RID: 68631 RVA: 0x003B918D File Offset: 0x003B738D
		bool IList.IsReadOnly
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		// Token: 0x06010C18 RID: 68632 RVA: 0x003B9195 File Offset: 0x003B7395
		void IList.Remove(object value)
		{
			this.Remove((BorderStyles)value);
		}

		// Token: 0x06010C19 RID: 68633 RVA: 0x003B91A3 File Offset: 0x003B73A3
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x1700518F RID: 20879
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (BorderStyles)value;
			}
		}

		// Token: 0x06010C1C RID: 68636 RVA: 0x003B91C4 File Offset: 0x003B73C4
		public virtual void CopyTo(BorderStyles[] array, int index)
		{
			this._list.CopyTo(array, index);
		}

		// Token: 0x17005190 RID: 20880
		// (get) Token: 0x06010C1D RID: 68637 RVA: 0x003B91D3 File Offset: 0x003B73D3
		public virtual int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x17005191 RID: 20881
		// (get) Token: 0x06010C1E RID: 68638 RVA: 0x003B91E0 File Offset: 0x003B73E0
		public virtual bool IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		// Token: 0x17005192 RID: 20882
		// (get) Token: 0x06010C1F RID: 68639 RVA: 0x003B91ED File Offset: 0x003B73ED
		public virtual object SyncRoot
		{
			get
			{
				return this._list.SyncRoot;
			}
		}

		// Token: 0x06010C20 RID: 68640 RVA: 0x003B91FA File Offset: 0x003B73FA
		void ICollection.CopyTo(Array array, int index)
		{
			this.CopyTo((BorderStyles[])array, index);
		}

		// Token: 0x17005193 RID: 20883
		// (get) Token: 0x06010C21 RID: 68641 RVA: 0x003B9209 File Offset: 0x003B7409
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x17005194 RID: 20884
		// (get) Token: 0x06010C22 RID: 68642 RVA: 0x003B9211 File Offset: 0x003B7411
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.IsSynchronized;
			}
		}

		// Token: 0x17005195 RID: 20885
		// (get) Token: 0x06010C23 RID: 68643 RVA: 0x003B9219 File Offset: 0x003B7419
		object ICollection.SyncRoot
		{
			get
			{
				return this.SyncRoot;
			}
		}

		// Token: 0x06010C24 RID: 68644 RVA: 0x003B9221 File Offset: 0x003B7421
		public virtual IEnumerator GetEnumerator()
		{
			return this._list.GetEnumerator();
		}

		// Token: 0x06010C25 RID: 68645 RVA: 0x003B922E File Offset: 0x003B742E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04004AC5 RID: 19141
		private IList _list;
	}
}

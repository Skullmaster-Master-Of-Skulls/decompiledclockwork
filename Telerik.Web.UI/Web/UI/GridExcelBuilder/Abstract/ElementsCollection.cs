using System;
using System.Collections;

namespace Telerik.Web.UI.GridExcelBuilder.Abstract
{
	// Token: 0x02001AFF RID: 6911
	public class ElementsCollection : IElementsCollection, IList, ICollection, IEnumerable
	{
		// Token: 0x06010B6E RID: 68462 RVA: 0x003B86C2 File Offset: 0x003B68C2
		public ElementsCollection()
		{
			this._list = new ArrayList();
		}

		// Token: 0x06010B6F RID: 68463 RVA: 0x003B86D5 File Offset: 0x003B68D5
		protected virtual void CopyTo(Array array, int index)
		{
			this._list.CopyTo(array, index);
		}

		// Token: 0x1700514B RID: 20811
		// (get) Token: 0x06010B70 RID: 68464 RVA: 0x003B86E4 File Offset: 0x003B68E4
		protected virtual int Count
		{
			get
			{
				return this._list.Count;
			}
		}

		// Token: 0x1700514C RID: 20812
		// (get) Token: 0x06010B71 RID: 68465 RVA: 0x003B86F1 File Offset: 0x003B68F1
		protected virtual bool IsSynchronized
		{
			get
			{
				return this._list.IsSynchronized;
			}
		}

		// Token: 0x1700514D RID: 20813
		// (get) Token: 0x06010B72 RID: 68466 RVA: 0x003B86FE File Offset: 0x003B68FE
		protected virtual object SyncRoot
		{
			get
			{
				return this._list.SyncRoot;
			}
		}

		// Token: 0x06010B73 RID: 68467 RVA: 0x003B870B File Offset: 0x003B690B
		void ICollection.CopyTo(Array array, int index)
		{
			this.CopyTo(array, index);
		}

		// Token: 0x1700514E RID: 20814
		// (get) Token: 0x06010B74 RID: 68468 RVA: 0x003B8715 File Offset: 0x003B6915
		int ICollection.Count
		{
			get
			{
				return this.Count;
			}
		}

		// Token: 0x1700514F RID: 20815
		// (get) Token: 0x06010B75 RID: 68469 RVA: 0x003B871D File Offset: 0x003B691D
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.IsSynchronized;
			}
		}

		// Token: 0x17005150 RID: 20816
		// (get) Token: 0x06010B76 RID: 68470 RVA: 0x003B8725 File Offset: 0x003B6925
		object ICollection.SyncRoot
		{
			get
			{
				return this.SyncRoot;
			}
		}

		// Token: 0x06010B77 RID: 68471 RVA: 0x003B872D File Offset: 0x003B692D
		protected virtual IEnumerator GetEnumerator()
		{
			return new ElementsCollection.ElementsEnumerator(this._list);
		}

		// Token: 0x06010B78 RID: 68472 RVA: 0x003B873A File Offset: 0x003B693A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06010B79 RID: 68473 RVA: 0x003B8742 File Offset: 0x003B6942
		public virtual int Add(IElement value)
		{
			return this._list.Add(value);
		}

		// Token: 0x06010B7A RID: 68474 RVA: 0x003B8750 File Offset: 0x003B6950
		public virtual void Clear()
		{
			this._list.Clear();
		}

		// Token: 0x06010B7B RID: 68475 RVA: 0x003B875D File Offset: 0x003B695D
		public virtual bool Contains(IElement value)
		{
			return this._list.Contains(value);
		}

		// Token: 0x06010B7C RID: 68476 RVA: 0x003B876B File Offset: 0x003B696B
		public virtual int IndexOf(IElement value)
		{
			return this._list.IndexOf(value);
		}

		// Token: 0x06010B7D RID: 68477 RVA: 0x003B8779 File Offset: 0x003B6979
		public virtual void Insert(int index, IElement value)
		{
			this._list.Insert(index, value);
		}

		// Token: 0x17005151 RID: 20817
		// (get) Token: 0x06010B7E RID: 68478 RVA: 0x003B8788 File Offset: 0x003B6988
		public virtual bool IsFixedSize
		{
			get
			{
				return this._list.IsFixedSize;
			}
		}

		// Token: 0x17005152 RID: 20818
		// (get) Token: 0x06010B7F RID: 68479 RVA: 0x003B8795 File Offset: 0x003B6995
		public virtual bool IsReadOnly
		{
			get
			{
				return this._list.IsReadOnly;
			}
		}

		// Token: 0x06010B80 RID: 68480 RVA: 0x003B87A2 File Offset: 0x003B69A2
		public virtual void Remove(IElement value)
		{
			this._list.Remove(value);
		}

		// Token: 0x06010B81 RID: 68481 RVA: 0x003B87B0 File Offset: 0x003B69B0
		public virtual void RemoveAt(int index)
		{
			this._list.RemoveAt(index);
		}

		// Token: 0x17005153 RID: 20819
		public virtual IElement this[int index]
		{
			get
			{
				if (index > this._list.Count)
				{
					throw new IndexOutOfRangeException();
				}
				return (IElement)this._list[index];
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

		// Token: 0x06010B84 RID: 68484 RVA: 0x003B8808 File Offset: 0x003B6A08
		int IList.Add(object value)
		{
			return this.Add((IElement)value);
		}

		// Token: 0x06010B85 RID: 68485 RVA: 0x003B8816 File Offset: 0x003B6A16
		void IList.Clear()
		{
			this.Clear();
		}

		// Token: 0x06010B86 RID: 68486 RVA: 0x003B881E File Offset: 0x003B6A1E
		bool IList.Contains(object value)
		{
			return this.Contains((IElement)value);
		}

		// Token: 0x06010B87 RID: 68487 RVA: 0x003B882C File Offset: 0x003B6A2C
		int IList.IndexOf(object value)
		{
			return this.IndexOf((IElement)value);
		}

		// Token: 0x06010B88 RID: 68488 RVA: 0x003B883A File Offset: 0x003B6A3A
		void IList.Insert(int index, object value)
		{
			this.Insert(index, (IElement)value);
		}

		// Token: 0x17005154 RID: 20820
		// (get) Token: 0x06010B89 RID: 68489 RVA: 0x003B8849 File Offset: 0x003B6A49
		bool IList.IsFixedSize
		{
			get
			{
				return this.IsFixedSize;
			}
		}

		// Token: 0x17005155 RID: 20821
		// (get) Token: 0x06010B8A RID: 68490 RVA: 0x003B8851 File Offset: 0x003B6A51
		bool IList.IsReadOnly
		{
			get
			{
				return this.IsReadOnly;
			}
		}

		// Token: 0x06010B8B RID: 68491 RVA: 0x003B8859 File Offset: 0x003B6A59
		void IList.Remove(object value)
		{
			this.Remove((IElement)value);
		}

		// Token: 0x06010B8C RID: 68492 RVA: 0x003B8867 File Offset: 0x003B6A67
		void IList.RemoveAt(int index)
		{
			this.RemoveAt(index);
		}

		// Token: 0x17005156 RID: 20822
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				this[index] = (IElement)value;
			}
		}

		// Token: 0x04004A96 RID: 19094
		private IList _list;

		// Token: 0x02001B00 RID: 6912
		private class ElementsEnumerator : IEnumerator
		{
			// Token: 0x06010B8F RID: 68495 RVA: 0x003B8888 File Offset: 0x003B6A88
			public ElementsEnumerator(IList entries)
			{
				this._entries = (ArrayList)entries;
				this._position = -1;
			}

			// Token: 0x17005157 RID: 20823
			// (get) Token: 0x06010B90 RID: 68496 RVA: 0x003B88A4 File Offset: 0x003B6AA4
			public virtual IElement Current
			{
				get
				{
					IElement result;
					try
					{
						result = (IElement)this._entries[this._position];
					}
					catch (IndexOutOfRangeException)
					{
						throw new InvalidOperationException();
					}
					return result;
				}
			}

			// Token: 0x06010B91 RID: 68497 RVA: 0x003B88E4 File Offset: 0x003B6AE4
			public virtual bool MoveNext()
			{
				this._position++;
				return this._position < this._entries.Count;
			}

			// Token: 0x06010B92 RID: 68498 RVA: 0x003B8907 File Offset: 0x003B6B07
			public virtual void Reset()
			{
				this._position = -1;
			}

			// Token: 0x17005158 RID: 20824
			// (get) Token: 0x06010B93 RID: 68499 RVA: 0x003B8910 File Offset: 0x003B6B10
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06010B94 RID: 68500 RVA: 0x003B8918 File Offset: 0x003B6B18
			bool IEnumerator.MoveNext()
			{
				return this.MoveNext();
			}

			// Token: 0x06010B95 RID: 68501 RVA: 0x003B8920 File Offset: 0x003B6B20
			void IEnumerator.Reset()
			{
				this.Reset();
			}

			// Token: 0x04004A97 RID: 19095
			private ArrayList _entries;

			// Token: 0x04004A98 RID: 19096
			private int _position;
		}
	}
}

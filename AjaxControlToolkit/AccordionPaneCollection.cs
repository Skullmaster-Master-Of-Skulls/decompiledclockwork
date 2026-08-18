using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace AjaxControlToolkit
{
	// Token: 0x0200000E RID: 14
	public sealed class AccordionPaneCollection : IList, ICollection, IEnumerable<AccordionPane>, IEnumerable
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00003B27 File Offset: 0x00001D27
		internal AccordionPaneCollection(Accordion parent)
		{
			if (parent == null)
			{
				throw new ArgumentNullException("parent", "Parent Accordion cannot be null.");
			}
			this._parent = parent;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003B4C File Offset: 0x00001D4C
		public int Count
		{
			get
			{
				int num = 0;
				foreach (object obj in this._parent.Controls)
				{
					if (obj is AccordionPane)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00003BB0 File Offset: 0x00001DB0
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700003C RID: 60
		public AccordionPane this[int index]
		{
			get
			{
				return this._parent.Controls[this.ToRawIndex(index)] as AccordionPane;
			}
		}

		// Token: 0x1700003D RID: 61
		public AccordionPane this[string id]
		{
			get
			{
				for (int i = 0; i < this._parent.Controls.Count; i++)
				{
					AccordionPane accordionPane = this._parent.Controls[i] as AccordionPane;
					if (accordionPane != null && accordionPane.ID == id)
					{
						return accordionPane;
					}
				}
				return null;
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003C28 File Offset: 0x00001E28
		private int ToRawIndex(int paneIndex)
		{
			if (paneIndex < 0)
			{
				return -1;
			}
			int num = -1;
			for (int i = 0; i < this._parent.Controls.Count; i++)
			{
				if (this._parent.Controls[i] is AccordionPane && ++num == paneIndex)
				{
					return i;
				}
			}
			throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "No AccordionPane at position {0}", new object[]
			{
				paneIndex
			}));
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003CA0 File Offset: 0x00001EA0
		private int FromRawIndex(int index)
		{
			if (index < 0)
			{
				return -1;
			}
			int num = -1;
			for (int i = 0; i < this._parent.Controls.Count; i++)
			{
				if (this._parent.Controls[i] is AccordionPane)
				{
					num++;
				}
				if (index == i)
				{
					return num;
				}
			}
			throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, "No AccordionPane at position {0}", new object[]
			{
				index
			}));
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003D17 File Offset: 0x00001F17
		public void Add(AccordionPane item)
		{
			this._parent.Controls.Add(item);
			this._version++;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003D38 File Offset: 0x00001F38
		public void Clear()
		{
			this._parent.ClearPanes();
			this._version++;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003D53 File Offset: 0x00001F53
		public bool Contains(AccordionPane item)
		{
			return this._parent.Controls.Contains(item);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003D68 File Offset: 0x00001F68
		public void CopyTo(Array array, int index)
		{
			AccordionPane[] array2 = array as AccordionPane[];
			if (array2 == null)
			{
				throw new ArgumentException("Expected an array of AccordionPanes.");
			}
			this.CopyTo(array2, index);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003D94 File Offset: 0x00001F94
		public void CopyTo(AccordionPane[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", "Cannot copy into a null array.");
			}
			int num = 0;
			for (int i = 0; i < this._parent.Controls.Count; i++)
			{
				AccordionPane accordionPane = this._parent.Controls[i] as AccordionPane;
				if (accordionPane != null)
				{
					if (num + index == array.Length)
					{
						throw new ArgumentException("Array is not large enough for the AccordionPanes");
					}
					array[num++ + index] = accordionPane;
				}
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003E08 File Offset: 0x00002008
		public int IndexOf(AccordionPane item)
		{
			return this.FromRawIndex(this._parent.Controls.IndexOf(item));
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003E21 File Offset: 0x00002021
		public void Insert(int index, AccordionPane item)
		{
			this._parent.Controls.AddAt(this.ToRawIndex(index), item);
			this._version++;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00003E49 File Offset: 0x00002049
		public void Remove(AccordionPane item)
		{
			this._parent.Controls.Remove(item);
			this._version++;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00003E6A File Offset: 0x0000206A
		public void RemoveAt(int index)
		{
			this._parent.Controls.RemoveAt(this.ToRawIndex(index));
			this._version++;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00003E91 File Offset: 0x00002091
		int IList.Add(object value)
		{
			this.Add(value as AccordionPane);
			return 0;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003EA0 File Offset: 0x000020A0
		bool IList.Contains(object value)
		{
			return this.Contains(value as AccordionPane);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003EAE File Offset: 0x000020AE
		int IList.IndexOf(object value)
		{
			return this.IndexOf(value as AccordionPane);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003EBC File Offset: 0x000020BC
		void IList.Insert(int index, object value)
		{
			this.Insert(index, value as AccordionPane);
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003ECB File Offset: 0x000020CB
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003ECE File Offset: 0x000020CE
		void IList.Remove(object value)
		{
			this.Remove(value as AccordionPane);
		}

		// Token: 0x1700003F RID: 63
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003EE7 File Offset: 0x000020E7
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003EEA File Offset: 0x000020EA
		object ICollection.SyncRoot
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003EF1 File Offset: 0x000020F1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new AccordionPaneCollection.AccordionPaneEnumerator(this);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003EF9 File Offset: 0x000020F9
		public IEnumerator<AccordionPane> GetEnumerator()
		{
			return new AccordionPaneCollection.AccordionPaneEnumerator(this);
		}

		// Token: 0x0400002F RID: 47
		private Accordion _parent;

		// Token: 0x04000030 RID: 48
		private int _version;

		// Token: 0x0200000F RID: 15
		private class AccordionPaneEnumerator : IEnumerator<AccordionPane>, IDisposable, IEnumerator
		{
			// Token: 0x060000C4 RID: 196 RVA: 0x00003F01 File Offset: 0x00002101
			public AccordionPaneEnumerator(AccordionPaneCollection parent)
			{
				this._collection = parent;
				this._parentEnumerator = parent._parent.Controls.GetEnumerator();
				this._version = parent._version;
			}

			// Token: 0x060000C5 RID: 197 RVA: 0x00003F32 File Offset: 0x00002132
			private void CheckVersion()
			{
				if (this._version != this._collection._version)
				{
					throw new InvalidOperationException("Enumeration can't continue because the collection has been modified.");
				}
			}

			// Token: 0x060000C6 RID: 198 RVA: 0x00003F52 File Offset: 0x00002152
			public void Dispose()
			{
				this._parentEnumerator = null;
				this._collection = null;
				GC.SuppressFinalize(this);
			}

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003F68 File Offset: 0x00002168
			public AccordionPane Current
			{
				get
				{
					this.CheckVersion();
					return this._parentEnumerator.Current as AccordionPane;
				}
			}

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x060000C8 RID: 200 RVA: 0x00003F80 File Offset: 0x00002180
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x060000C9 RID: 201 RVA: 0x00003F88 File Offset: 0x00002188
			public bool MoveNext()
			{
				this.CheckVersion();
				bool flag = this._parentEnumerator.MoveNext();
				if (flag && !(this._parentEnumerator.Current is AccordionPane))
				{
					flag = this.MoveNext();
				}
				return flag;
			}

			// Token: 0x060000CA RID: 202 RVA: 0x00003FC4 File Offset: 0x000021C4
			public void Reset()
			{
				this.CheckVersion();
				this._parentEnumerator.Reset();
			}

			// Token: 0x04000031 RID: 49
			private AccordionPaneCollection _collection;

			// Token: 0x04000032 RID: 50
			private IEnumerator _parentEnumerator;

			// Token: 0x04000033 RID: 51
			private int _version;
		}
	}
}

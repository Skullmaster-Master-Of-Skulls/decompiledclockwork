using System;
using System.Collections;

namespace System.Web.UI
{
	// Token: 0x02000263 RID: 611
	public class ControlCollection : ICollection, IEnumerable
	{
		// Token: 0x06001D29 RID: 7465 RVA: 0x0005ED6F File Offset: 0x0005CF6F
		public ControlCollection(Control owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this._owner = owner;
		}

		// Token: 0x06001D2A RID: 7466 RVA: 0x0005ED9A File Offset: 0x0005CF9A
		internal ControlCollection(Control owner, int defaultCapacity, int growthFactor)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this._owner = owner;
			this._defaultCapacity = defaultCapacity;
			this._growthFactor = growthFactor;
		}

		// Token: 0x06001D2B RID: 7467 RVA: 0x0005EDD4 File Offset: 0x0005CFD4
		public virtual void Add(Control child)
		{
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			if (this._readOnlyErrorMsg != null)
			{
				throw new HttpException(SR.GetString(this._readOnlyErrorMsg));
			}
			if (this._controls == null)
			{
				this._controls = new Control[this._defaultCapacity];
			}
			else if (this._size >= this._controls.Length)
			{
				Control[] array = new Control[this._controls.Length * this._growthFactor];
				Array.Copy(this._controls, array, this._controls.Length);
				this._controls = array;
			}
			int size = this._size;
			this._controls[size] = child;
			this._size++;
			this._version++;
			this._owner.AddedControl(child, size);
		}

		// Token: 0x06001D2C RID: 7468 RVA: 0x0005EE9C File Offset: 0x0005D09C
		public virtual void AddAt(int index, Control child)
		{
			if (index == -1)
			{
				this.Add(child);
				return;
			}
			if (child == null)
			{
				throw new ArgumentNullException("child");
			}
			if (index < 0 || index > this._size)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (this._readOnlyErrorMsg != null)
			{
				throw new HttpException(SR.GetString(this._readOnlyErrorMsg));
			}
			if (this._controls == null)
			{
				this._controls = new Control[this._defaultCapacity];
			}
			else if (this._size >= this._controls.Length)
			{
				Control[] array = new Control[this._controls.Length * this._growthFactor];
				Array.Copy(this._controls, array, index);
				array[index] = child;
				Array.Copy(this._controls, index, array, index + 1, this._size - index);
				this._controls = array;
			}
			else if (index < this._size)
			{
				Array.Copy(this._controls, index, this._controls, index + 1, this._size - index);
			}
			this._controls[index] = child;
			this._size++;
			this._version++;
			this._owner.AddedControl(child, index);
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x0005EFC0 File Offset: 0x0005D1C0
		public virtual void Clear()
		{
			if (this._controls != null)
			{
				for (int i = this._size - 1; i >= 0; i--)
				{
					this.RemoveAt(i);
				}
				if (this._owner is INamingContainer)
				{
					this._owner.ClearNamingContainer();
				}
			}
		}

		// Token: 0x06001D2E RID: 7470 RVA: 0x0005F008 File Offset: 0x0005D208
		public virtual bool Contains(Control c)
		{
			if (this._controls == null || c == null)
			{
				return false;
			}
			for (int i = 0; i < this._size; i++)
			{
				if (c == this._controls[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06001D2F RID: 7471 RVA: 0x0005F041 File Offset: 0x0005D241
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06001D30 RID: 7472 RVA: 0x0005F049 File Offset: 0x0005D249
		protected Control Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x0005F051 File Offset: 0x0005D251
		public virtual int IndexOf(Control value)
		{
			if (this._controls == null)
			{
				return -1;
			}
			return Array.IndexOf<Control>(this._controls, value, 0, this._size);
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x0005F070 File Offset: 0x0005D270
		public virtual IEnumerator GetEnumerator()
		{
			return new ControlCollection.ControlCollectionEnumerator(this);
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x0005F078 File Offset: 0x0005D278
		public virtual void CopyTo(Array array, int index)
		{
			if (this._controls == null)
			{
				return;
			}
			if (array != null && array.Rank != 1)
			{
				throw new HttpException(SR.GetString("InvalidArgumentValue", new object[]
				{
					"array"
				}));
			}
			Array.Copy(this._controls, 0, array, index, this._size);
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06001D34 RID: 7476 RVA: 0x00004335 File Offset: 0x00002535
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06001D35 RID: 7477 RVA: 0x0005F0CC File Offset: 0x0005D2CC
		public bool IsReadOnly
		{
			get
			{
				return this._readOnlyErrorMsg != null;
			}
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x0005F0D8 File Offset: 0x0005D2D8
		internal string SetCollectionReadOnly(string errorMsg)
		{
			string readOnlyErrorMsg = this._readOnlyErrorMsg;
			this._readOnlyErrorMsg = errorMsg;
			return readOnlyErrorMsg;
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06001D37 RID: 7479 RVA: 0x00007722 File Offset: 0x00005922
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000841 RID: 2113
		public virtual Control this[int index]
		{
			get
			{
				if (index < 0 || index >= this._size)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				return this._controls[index];
			}
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x0005F118 File Offset: 0x0005D318
		public virtual void RemoveAt(int index)
		{
			if (this._readOnlyErrorMsg != null)
			{
				throw new HttpException(SR.GetString(this._readOnlyErrorMsg));
			}
			Control control = this[index];
			this._size--;
			if (index < this._size)
			{
				Array.Copy(this._controls, index + 1, this._controls, index, this._size - index);
			}
			this._controls[this._size] = null;
			this._version++;
			this._owner.RemovedControl(control);
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x0005F1A4 File Offset: 0x0005D3A4
		public virtual void Remove(Control value)
		{
			int num = this.IndexOf(value);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
		}

		// Token: 0x04001940 RID: 6464
		private Control _owner;

		// Token: 0x04001941 RID: 6465
		private Control[] _controls;

		// Token: 0x04001942 RID: 6466
		private int _size;

		// Token: 0x04001943 RID: 6467
		private int _version;

		// Token: 0x04001944 RID: 6468
		private string _readOnlyErrorMsg;

		// Token: 0x04001945 RID: 6469
		private int _defaultCapacity = 5;

		// Token: 0x04001946 RID: 6470
		private int _growthFactor = 4;

		// Token: 0x02000964 RID: 2404
		private class ControlCollectionEnumerator : IEnumerator
		{
			// Token: 0x060069EF RID: 27119 RVA: 0x00178B39 File Offset: 0x00176D39
			internal ControlCollectionEnumerator(ControlCollection list)
			{
				this.list = list;
				this.index = -1;
				this.version = list._version;
			}

			// Token: 0x060069F0 RID: 27120 RVA: 0x00178B5C File Offset: 0x00176D5C
			public bool MoveNext()
			{
				if (this.index >= this.list.Count - 1)
				{
					this.index = this.list.Count;
					return false;
				}
				if (this.version != this.list._version)
				{
					throw new InvalidOperationException(SR.GetString("ListEnumVersionMismatch"));
				}
				this.index++;
				this.currentElement = this.list[this.index];
				return true;
			}

			// Token: 0x17001D33 RID: 7475
			// (get) Token: 0x060069F1 RID: 27121 RVA: 0x00178BDA File Offset: 0x00176DDA
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x17001D34 RID: 7476
			// (get) Token: 0x060069F2 RID: 27122 RVA: 0x00178BE4 File Offset: 0x00176DE4
			public Control Current
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

			// Token: 0x060069F3 RID: 27123 RVA: 0x00178C33 File Offset: 0x00176E33
			public void Reset()
			{
				if (this.version != this.list._version)
				{
					throw new InvalidOperationException(SR.GetString("ListEnumVersionMismatch"));
				}
				this.currentElement = null;
				this.index = -1;
			}

			// Token: 0x0400383D RID: 14397
			private ControlCollection list;

			// Token: 0x0400383E RID: 14398
			private int index;

			// Token: 0x0400383F RID: 14399
			private int version;

			// Token: 0x04003840 RID: 14400
			private Control currentElement;
		}
	}
}

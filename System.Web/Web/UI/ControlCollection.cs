using System;
using System.Collections;
using System.Security.Permissions;

namespace System.Web.UI
{
	// Token: 0x020003C2 RID: 962
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class ControlCollection : ICollection, IEnumerable
	{
		// Token: 0x06002F24 RID: 12068 RVA: 0x000D25CB File Offset: 0x000D15CB
		public ControlCollection(Control owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this._owner = owner;
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x000D25F6 File Offset: 0x000D15F6
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

		// Token: 0x06002F26 RID: 12070 RVA: 0x000D2630 File Offset: 0x000D1630
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

		// Token: 0x06002F27 RID: 12071 RVA: 0x000D26F8 File Offset: 0x000D16F8
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

		// Token: 0x06002F28 RID: 12072 RVA: 0x000D281C File Offset: 0x000D181C
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

		// Token: 0x06002F29 RID: 12073 RVA: 0x000D2864 File Offset: 0x000D1864
		public virtual bool Contains(Control c)
		{
			if (this._controls == null || c == null)
			{
				return false;
			}
			for (int i = 0; i < this._size; i++)
			{
				if (object.ReferenceEquals(c, this._controls[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06002F2A RID: 12074 RVA: 0x000D28A2 File Offset: 0x000D18A2
		public virtual int Count
		{
			get
			{
				return this._size;
			}
		}

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06002F2B RID: 12075 RVA: 0x000D28AA File Offset: 0x000D18AA
		protected Control Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x06002F2C RID: 12076 RVA: 0x000D28B2 File Offset: 0x000D18B2
		public virtual int IndexOf(Control value)
		{
			if (this._controls == null)
			{
				return -1;
			}
			return Array.IndexOf<Control>(this._controls, value, 0, this._size);
		}

		// Token: 0x06002F2D RID: 12077 RVA: 0x000D28D1 File Offset: 0x000D18D1
		public virtual IEnumerator GetEnumerator()
		{
			return new ControlCollection.ControlCollectionEnumerator(this);
		}

		// Token: 0x06002F2E RID: 12078 RVA: 0x000D28DC File Offset: 0x000D18DC
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

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06002F2F RID: 12079 RVA: 0x000D2932 File Offset: 0x000D1932
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06002F30 RID: 12080 RVA: 0x000D2935 File Offset: 0x000D1935
		public bool IsReadOnly
		{
			get
			{
				return this._readOnlyErrorMsg != null;
			}
		}

		// Token: 0x06002F31 RID: 12081 RVA: 0x000D2944 File Offset: 0x000D1944
		internal string SetCollectionReadOnly(string errorMsg)
		{
			string readOnlyErrorMsg = this._readOnlyErrorMsg;
			this._readOnlyErrorMsg = errorMsg;
			return readOnlyErrorMsg;
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06002F32 RID: 12082 RVA: 0x000D2960 File Offset: 0x000D1960
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000A47 RID: 2631
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

		// Token: 0x06002F34 RID: 12084 RVA: 0x000D2988 File Offset: 0x000D1988
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

		// Token: 0x06002F35 RID: 12085 RVA: 0x000D2A14 File Offset: 0x000D1A14
		public virtual void Remove(Control value)
		{
			int num = this.IndexOf(value);
			if (num >= 0)
			{
				this.RemoveAt(num);
			}
		}

		// Token: 0x040021C4 RID: 8644
		private Control _owner;

		// Token: 0x040021C5 RID: 8645
		private Control[] _controls;

		// Token: 0x040021C6 RID: 8646
		private int _size;

		// Token: 0x040021C7 RID: 8647
		private int _version;

		// Token: 0x040021C8 RID: 8648
		private string _readOnlyErrorMsg;

		// Token: 0x040021C9 RID: 8649
		private int _defaultCapacity = 5;

		// Token: 0x040021CA RID: 8650
		private int _growthFactor = 4;

		// Token: 0x020003C3 RID: 963
		private class ControlCollectionEnumerator : IEnumerator
		{
			// Token: 0x06002F36 RID: 12086 RVA: 0x000D2A34 File Offset: 0x000D1A34
			internal ControlCollectionEnumerator(ControlCollection list)
			{
				this.list = list;
				this.index = -1;
				this.version = list._version;
			}

			// Token: 0x06002F37 RID: 12087 RVA: 0x000D2A58 File Offset: 0x000D1A58
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

			// Token: 0x17000A48 RID: 2632
			// (get) Token: 0x06002F38 RID: 12088 RVA: 0x000D2AD6 File Offset: 0x000D1AD6
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x17000A49 RID: 2633
			// (get) Token: 0x06002F39 RID: 12089 RVA: 0x000D2AE0 File Offset: 0x000D1AE0
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

			// Token: 0x06002F3A RID: 12090 RVA: 0x000D2B2F File Offset: 0x000D1B2F
			public void Reset()
			{
				if (this.version != this.list._version)
				{
					throw new InvalidOperationException(SR.GetString("ListEnumVersionMismatch"));
				}
				this.currentElement = null;
				this.index = -1;
			}

			// Token: 0x040021CB RID: 8651
			private ControlCollection list;

			// Token: 0x040021CC RID: 8652
			private int index;

			// Token: 0x040021CD RID: 8653
			private int version;

			// Token: 0x040021CE RID: 8654
			private Control currentElement;
		}
	}
}

using System;
using System.Threading;

namespace System.Collections.Specialized
{
	// Token: 0x020003AD RID: 941
	[Serializable]
	public class ListDictionary : IDictionary, ICollection, IEnumerable
	{
		// Token: 0x06002321 RID: 8993 RVA: 0x000A6B4F File Offset: 0x000A4D4F
		public ListDictionary()
		{
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x000A6B57 File Offset: 0x000A4D57
		public ListDictionary(IComparer comparer)
		{
			this.comparer = comparer;
		}

		// Token: 0x170008E8 RID: 2280
		public object this[object key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", SR.GetString("ArgumentNull_Key"));
				}
				ListDictionary.DictionaryNode next = this.head;
				if (this.comparer == null)
				{
					while (next != null)
					{
						object key2 = next.key;
						if (key2 != null && key2.Equals(key))
						{
							return next.value;
						}
						next = next.next;
					}
				}
				else
				{
					while (next != null)
					{
						object key3 = next.key;
						if (key3 != null && this.comparer.Compare(key3, key) == 0)
						{
							return next.value;
						}
						next = next.next;
					}
				}
				return null;
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key", SR.GetString("ArgumentNull_Key"));
				}
				this.version++;
				ListDictionary.DictionaryNode dictionaryNode = null;
				ListDictionary.DictionaryNode next;
				for (next = this.head; next != null; next = next.next)
				{
					object key2 = next.key;
					if ((this.comparer == null) ? key2.Equals(key) : (this.comparer.Compare(key2, key) == 0))
					{
						break;
					}
					dictionaryNode = next;
				}
				if (next != null)
				{
					next.value = value;
					return;
				}
				ListDictionary.DictionaryNode dictionaryNode2 = new ListDictionary.DictionaryNode();
				dictionaryNode2.key = key;
				dictionaryNode2.value = value;
				if (dictionaryNode != null)
				{
					dictionaryNode.next = dictionaryNode2;
				}
				else
				{
					this.head = dictionaryNode2;
				}
				this.count++;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06002325 RID: 8997 RVA: 0x000A6CA4 File Offset: 0x000A4EA4
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x06002326 RID: 8998 RVA: 0x000A6CAC File Offset: 0x000A4EAC
		public ICollection Keys
		{
			get
			{
				return new ListDictionary.NodeKeyValueCollection(this, true);
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x06002327 RID: 8999 RVA: 0x000A6CB5 File Offset: 0x000A4EB5
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x06002328 RID: 9000 RVA: 0x000A6CB8 File Offset: 0x000A4EB8
		public bool IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06002329 RID: 9001 RVA: 0x000A6CBB File Offset: 0x000A4EBB
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x0600232A RID: 9002 RVA: 0x000A6CBE File Offset: 0x000A4EBE
		public object SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x0600232B RID: 9003 RVA: 0x000A6CE0 File Offset: 0x000A4EE0
		public ICollection Values
		{
			get
			{
				return new ListDictionary.NodeKeyValueCollection(this, false);
			}
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x000A6CEC File Offset: 0x000A4EEC
		public void Add(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", SR.GetString("ArgumentNull_Key"));
			}
			this.version++;
			ListDictionary.DictionaryNode dictionaryNode = null;
			for (ListDictionary.DictionaryNode next = this.head; next != null; next = next.next)
			{
				object key2 = next.key;
				if ((this.comparer == null) ? key2.Equals(key) : (this.comparer.Compare(key2, key) == 0))
				{
					throw new ArgumentException(SR.GetString("Argument_AddingDuplicate"));
				}
				dictionaryNode = next;
			}
			ListDictionary.DictionaryNode dictionaryNode2 = new ListDictionary.DictionaryNode();
			dictionaryNode2.key = key;
			dictionaryNode2.value = value;
			if (dictionaryNode != null)
			{
				dictionaryNode.next = dictionaryNode2;
			}
			else
			{
				this.head = dictionaryNode2;
			}
			this.count++;
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x000A6DA5 File Offset: 0x000A4FA5
		public void Clear()
		{
			this.count = 0;
			this.head = null;
			this.version++;
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x000A6DC4 File Offset: 0x000A4FC4
		public bool Contains(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", SR.GetString("ArgumentNull_Key"));
			}
			for (ListDictionary.DictionaryNode next = this.head; next != null; next = next.next)
			{
				object key2 = next.key;
				if ((this.comparer == null) ? key2.Equals(key) : (this.comparer.Compare(key2, key) == 0))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x000A6E2C File Offset: 0x000A502C
		public void CopyTo(Array array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (array.Length - index < this.count)
			{
				throw new ArgumentException(SR.GetString("Arg_InsufficientSpace"));
			}
			for (ListDictionary.DictionaryNode next = this.head; next != null; next = next.next)
			{
				array.SetValue(new DictionaryEntry(next.key, next.value), index);
				index++;
			}
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x000A6EB5 File Offset: 0x000A50B5
		public IDictionaryEnumerator GetEnumerator()
		{
			return new ListDictionary.NodeEnumerator(this);
		}

		// Token: 0x06002331 RID: 9009 RVA: 0x000A6EBD File Offset: 0x000A50BD
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new ListDictionary.NodeEnumerator(this);
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x000A6EC8 File Offset: 0x000A50C8
		public void Remove(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", SR.GetString("ArgumentNull_Key"));
			}
			this.version++;
			ListDictionary.DictionaryNode dictionaryNode = null;
			ListDictionary.DictionaryNode next;
			for (next = this.head; next != null; next = next.next)
			{
				object key2 = next.key;
				if ((this.comparer == null) ? key2.Equals(key) : (this.comparer.Compare(key2, key) == 0))
				{
					break;
				}
				dictionaryNode = next;
			}
			if (next == null)
			{
				return;
			}
			if (next == this.head)
			{
				this.head = next.next;
			}
			else
			{
				dictionaryNode.next = next.next;
			}
			this.count--;
		}

		// Token: 0x04001FC2 RID: 8130
		private ListDictionary.DictionaryNode head;

		// Token: 0x04001FC3 RID: 8131
		private int version;

		// Token: 0x04001FC4 RID: 8132
		private int count;

		// Token: 0x04001FC5 RID: 8133
		private IComparer comparer;

		// Token: 0x04001FC6 RID: 8134
		[NonSerialized]
		private object _syncRoot;

		// Token: 0x020007E8 RID: 2024
		private class NodeEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x060043EE RID: 17390 RVA: 0x0011DDEF File Offset: 0x0011BFEF
			public NodeEnumerator(ListDictionary list)
			{
				this.list = list;
				this.version = list.version;
				this.start = true;
				this.current = null;
			}

			// Token: 0x17000F5D RID: 3933
			// (get) Token: 0x060043EF RID: 17391 RVA: 0x0011DE18 File Offset: 0x0011C018
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x17000F5E RID: 3934
			// (get) Token: 0x060043F0 RID: 17392 RVA: 0x0011DE25 File Offset: 0x0011C025
			public DictionaryEntry Entry
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumOpCantHappen"));
					}
					return new DictionaryEntry(this.current.key, this.current.value);
				}
			}

			// Token: 0x17000F5F RID: 3935
			// (get) Token: 0x060043F1 RID: 17393 RVA: 0x0011DE5A File Offset: 0x0011C05A
			public object Key
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumOpCantHappen"));
					}
					return this.current.key;
				}
			}

			// Token: 0x17000F60 RID: 3936
			// (get) Token: 0x060043F2 RID: 17394 RVA: 0x0011DE7F File Offset: 0x0011C07F
			public object Value
			{
				get
				{
					if (this.current == null)
					{
						throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumOpCantHappen"));
					}
					return this.current.value;
				}
			}

			// Token: 0x060043F3 RID: 17395 RVA: 0x0011DEA4 File Offset: 0x0011C0A4
			public bool MoveNext()
			{
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumFailedVersion"));
				}
				if (this.start)
				{
					this.current = this.list.head;
					this.start = false;
				}
				else if (this.current != null)
				{
					this.current = this.current.next;
				}
				return this.current != null;
			}

			// Token: 0x060043F4 RID: 17396 RVA: 0x0011DF18 File Offset: 0x0011C118
			public void Reset()
			{
				if (this.version != this.list.version)
				{
					throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumFailedVersion"));
				}
				this.start = true;
				this.current = null;
			}

			// Token: 0x040034FC RID: 13564
			private ListDictionary list;

			// Token: 0x040034FD RID: 13565
			private ListDictionary.DictionaryNode current;

			// Token: 0x040034FE RID: 13566
			private int version;

			// Token: 0x040034FF RID: 13567
			private bool start;
		}

		// Token: 0x020007E9 RID: 2025
		private class NodeKeyValueCollection : ICollection, IEnumerable
		{
			// Token: 0x060043F5 RID: 17397 RVA: 0x0011DF4B File Offset: 0x0011C14B
			public NodeKeyValueCollection(ListDictionary list, bool isKeys)
			{
				this.list = list;
				this.isKeys = isKeys;
			}

			// Token: 0x060043F6 RID: 17398 RVA: 0x0011DF64 File Offset: 0x0011C164
			void ICollection.CopyTo(Array array, int index)
			{
				if (array == null)
				{
					throw new ArgumentNullException("array");
				}
				if (index < 0)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("ArgumentOutOfRange_NeedNonNegNum"));
				}
				for (ListDictionary.DictionaryNode dictionaryNode = this.list.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
				{
					array.SetValue(this.isKeys ? dictionaryNode.key : dictionaryNode.value, index);
					index++;
				}
			}

			// Token: 0x17000F61 RID: 3937
			// (get) Token: 0x060043F7 RID: 17399 RVA: 0x0011DFD4 File Offset: 0x0011C1D4
			int ICollection.Count
			{
				get
				{
					int num = 0;
					for (ListDictionary.DictionaryNode dictionaryNode = this.list.head; dictionaryNode != null; dictionaryNode = dictionaryNode.next)
					{
						num++;
					}
					return num;
				}
			}

			// Token: 0x17000F62 RID: 3938
			// (get) Token: 0x060043F8 RID: 17400 RVA: 0x0011E000 File Offset: 0x0011C200
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000F63 RID: 3939
			// (get) Token: 0x060043F9 RID: 17401 RVA: 0x0011E003 File Offset: 0x0011C203
			object ICollection.SyncRoot
			{
				get
				{
					return this.list.SyncRoot;
				}
			}

			// Token: 0x060043FA RID: 17402 RVA: 0x0011E010 File Offset: 0x0011C210
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new ListDictionary.NodeKeyValueCollection.NodeKeyValueEnumerator(this.list, this.isKeys);
			}

			// Token: 0x04003500 RID: 13568
			private ListDictionary list;

			// Token: 0x04003501 RID: 13569
			private bool isKeys;

			// Token: 0x02000925 RID: 2341
			private class NodeKeyValueEnumerator : IEnumerator
			{
				// Token: 0x0600467F RID: 18047 RVA: 0x001269A5 File Offset: 0x00124BA5
				public NodeKeyValueEnumerator(ListDictionary list, bool isKeys)
				{
					this.list = list;
					this.isKeys = isKeys;
					this.version = list.version;
					this.start = true;
					this.current = null;
				}

				// Token: 0x17000FE7 RID: 4071
				// (get) Token: 0x06004680 RID: 18048 RVA: 0x001269D5 File Offset: 0x00124BD5
				public object Current
				{
					get
					{
						if (this.current == null)
						{
							throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumOpCantHappen"));
						}
						if (!this.isKeys)
						{
							return this.current.value;
						}
						return this.current.key;
					}
				}

				// Token: 0x06004681 RID: 18049 RVA: 0x00126A10 File Offset: 0x00124C10
				public bool MoveNext()
				{
					if (this.version != this.list.version)
					{
						throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumFailedVersion"));
					}
					if (this.start)
					{
						this.current = this.list.head;
						this.start = false;
					}
					else if (this.current != null)
					{
						this.current = this.current.next;
					}
					return this.current != null;
				}

				// Token: 0x06004682 RID: 18050 RVA: 0x00126A84 File Offset: 0x00124C84
				public void Reset()
				{
					if (this.version != this.list.version)
					{
						throw new InvalidOperationException(SR.GetString("InvalidOperation_EnumFailedVersion"));
					}
					this.start = true;
					this.current = null;
				}

				// Token: 0x04003DBD RID: 15805
				private ListDictionary list;

				// Token: 0x04003DBE RID: 15806
				private ListDictionary.DictionaryNode current;

				// Token: 0x04003DBF RID: 15807
				private int version;

				// Token: 0x04003DC0 RID: 15808
				private bool isKeys;

				// Token: 0x04003DC1 RID: 15809
				private bool start;
			}
		}

		// Token: 0x020007EA RID: 2026
		[Serializable]
		private class DictionaryNode
		{
			// Token: 0x04003502 RID: 13570
			public object key;

			// Token: 0x04003503 RID: 13571
			public object value;

			// Token: 0x04003504 RID: 13572
			public ListDictionary.DictionaryNode next;
		}
	}
}

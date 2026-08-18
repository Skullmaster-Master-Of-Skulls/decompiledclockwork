using System;
using System.Collections;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000719 RID: 1817
	internal class MessageDictionaryEnumerator : IDictionaryEnumerator, IEnumerator
	{
		// Token: 0x060040DE RID: 16606 RVA: 0x000DCD67 File Offset: 0x000DBD67
		public MessageDictionaryEnumerator(MessageDictionary md, IDictionary hashtable)
		{
			this._md = md;
			if (hashtable != null)
			{
				this._enumHash = hashtable.GetEnumerator();
				return;
			}
			this._enumHash = null;
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x060040DF RID: 16607 RVA: 0x000DCD94 File Offset: 0x000DBD94
		public object Key
		{
			get
			{
				if (this.i < 0)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
				}
				if (this.i < this._md._keys.Length)
				{
					return this._md._keys[this.i];
				}
				return this._enumHash.Key;
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x060040E0 RID: 16608 RVA: 0x000DCDF0 File Offset: 0x000DBDF0
		public object Value
		{
			get
			{
				if (this.i < 0)
				{
					throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
				}
				if (this.i < this._md._keys.Length)
				{
					return this._md.GetMessageValue(this.i);
				}
				return this._enumHash.Value;
			}
		}

		// Token: 0x060040E1 RID: 16609 RVA: 0x000DCE48 File Offset: 0x000DBE48
		public bool MoveNext()
		{
			if (this.i == -2)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_InternalState"));
			}
			this.i++;
			if (this.i < this._md._keys.Length)
			{
				return true;
			}
			if (this._enumHash != null && this._enumHash.MoveNext())
			{
				return true;
			}
			this.i = -2;
			return false;
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x060040E2 RID: 16610 RVA: 0x000DCEB4 File Offset: 0x000DBEB4
		public object Current
		{
			get
			{
				return this.Entry;
			}
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x060040E3 RID: 16611 RVA: 0x000DCEC1 File Offset: 0x000DBEC1
		public DictionaryEntry Entry
		{
			get
			{
				return new DictionaryEntry(this.Key, this.Value);
			}
		}

		// Token: 0x060040E4 RID: 16612 RVA: 0x000DCED4 File Offset: 0x000DBED4
		public void Reset()
		{
			this.i = -1;
			if (this._enumHash != null)
			{
				this._enumHash.Reset();
			}
		}

		// Token: 0x040020B6 RID: 8374
		private int i = -1;

		// Token: 0x040020B7 RID: 8375
		private IDictionaryEnumerator _enumHash;

		// Token: 0x040020B8 RID: 8376
		private MessageDictionary _md;
	}
}

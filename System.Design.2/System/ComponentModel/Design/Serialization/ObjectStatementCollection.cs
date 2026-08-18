using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x020001EC RID: 492
	public sealed class ObjectStatementCollection : IEnumerable
	{
		// Token: 0x06001264 RID: 4708 RVA: 0x0000362F File Offset: 0x0000182F
		internal ObjectStatementCollection()
		{
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x0006A4C4 File Offset: 0x000686C4
		private void AddOwner(object statementOwner, CodeStatementCollection statements)
		{
			if (this._table == null)
			{
				this._table = new List<ObjectStatementCollection.TableEntry>();
			}
			else
			{
				int i = 0;
				while (i < this._table.Count)
				{
					if (this._table[i].Owner == statementOwner)
					{
						if (this._table[i].Statements != null)
						{
							throw new InvalidOperationException();
						}
						if (statements != null)
						{
							this._table[i] = new ObjectStatementCollection.TableEntry(statementOwner, statements);
						}
						return;
					}
					else
					{
						i++;
					}
				}
			}
			this._table.Add(new ObjectStatementCollection.TableEntry(statementOwner, statements));
			this._version++;
		}

		// Token: 0x17000416 RID: 1046
		public CodeStatementCollection this[object statementOwner]
		{
			get
			{
				if (statementOwner == null)
				{
					throw new ArgumentNullException("statementOwner");
				}
				if (this._table != null)
				{
					for (int i = 0; i < this._table.Count; i++)
					{
						if (this._table[i].Owner == statementOwner)
						{
							if (this._table[i].Statements == null)
							{
								this._table[i] = new ObjectStatementCollection.TableEntry(statementOwner, new CodeStatementCollection());
							}
							return this._table[i].Statements;
						}
					}
					foreach (ObjectStatementCollection.TableEntry tableEntry in this._table)
					{
						if (tableEntry.Owner == statementOwner)
						{
							return tableEntry.Statements;
						}
					}
				}
				return null;
			}
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x0006A640 File Offset: 0x00068840
		public bool ContainsKey(object statementOwner)
		{
			if (statementOwner == null)
			{
				throw new ArgumentNullException("statementOwner");
			}
			return this._table != null && this[statementOwner] != null;
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x0006A664 File Offset: 0x00068864
		public IDictionaryEnumerator GetEnumerator()
		{
			return new ObjectStatementCollection.TableEnumerator(this);
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x0006A674 File Offset: 0x00068874
		public void Populate(ICollection statementOwners)
		{
			if (statementOwners == null)
			{
				throw new ArgumentNullException("statementOwners");
			}
			foreach (object owner in statementOwners)
			{
				this.Populate(owner);
			}
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x0006A6D4 File Offset: 0x000688D4
		public void Populate(object owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this.AddOwner(owner, null);
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x0006A6EC File Offset: 0x000688EC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000A07 RID: 2567
		private List<ObjectStatementCollection.TableEntry> _table;

		// Token: 0x04000A08 RID: 2568
		private int _version;

		// Token: 0x020004AC RID: 1196
		private struct TableEntry
		{
			// Token: 0x06002BD4 RID: 11220 RVA: 0x001058E1 File Offset: 0x00103AE1
			public TableEntry(object owner, CodeStatementCollection statements)
			{
				this.Owner = owner;
				this.Statements = statements;
			}

			// Token: 0x04001E67 RID: 7783
			public object Owner;

			// Token: 0x04001E68 RID: 7784
			public CodeStatementCollection Statements;
		}

		// Token: 0x020004AD RID: 1197
		private struct TableEnumerator : IDictionaryEnumerator, IEnumerator
		{
			// Token: 0x06002BD5 RID: 11221 RVA: 0x001058F1 File Offset: 0x00103AF1
			public TableEnumerator(ObjectStatementCollection table)
			{
				this._table = table;
				this._version = this._table._version;
				this._position = -1;
			}

			// Token: 0x17000945 RID: 2373
			// (get) Token: 0x06002BD6 RID: 11222 RVA: 0x00105912 File Offset: 0x00103B12
			public object Current
			{
				get
				{
					return this.Entry;
				}
			}

			// Token: 0x17000946 RID: 2374
			// (get) Token: 0x06002BD7 RID: 11223 RVA: 0x00105920 File Offset: 0x00103B20
			public DictionaryEntry Entry
			{
				get
				{
					if (this._version != this._table._version)
					{
						throw new InvalidOperationException();
					}
					if (this._position < 0 || this._table._table == null || this._position >= this._table._table.Count)
					{
						throw new InvalidOperationException();
					}
					if (this._table._table[this._position].Statements == null)
					{
						this._table._table[this._position] = new ObjectStatementCollection.TableEntry(this._table._table[this._position].Owner, new CodeStatementCollection());
					}
					ObjectStatementCollection.TableEntry tableEntry = this._table._table[this._position];
					return new DictionaryEntry(tableEntry.Owner, tableEntry.Statements);
				}
			}

			// Token: 0x17000947 RID: 2375
			// (get) Token: 0x06002BD8 RID: 11224 RVA: 0x001059FC File Offset: 0x00103BFC
			public object Key
			{
				get
				{
					return this.Entry.Key;
				}
			}

			// Token: 0x17000948 RID: 2376
			// (get) Token: 0x06002BD9 RID: 11225 RVA: 0x00105A18 File Offset: 0x00103C18
			public object Value
			{
				get
				{
					return this.Entry.Value;
				}
			}

			// Token: 0x06002BDA RID: 11226 RVA: 0x00105A33 File Offset: 0x00103C33
			public bool MoveNext()
			{
				if (this._table._table != null && this._position + 1 < this._table._table.Count)
				{
					this._position++;
					return true;
				}
				return false;
			}

			// Token: 0x06002BDB RID: 11227 RVA: 0x00105A6D File Offset: 0x00103C6D
			public void Reset()
			{
				this._position = -1;
			}

			// Token: 0x04001E69 RID: 7785
			private ObjectStatementCollection _table;

			// Token: 0x04001E6A RID: 7786
			private int _version;

			// Token: 0x04001E6B RID: 7787
			private int _position;
		}
	}
}

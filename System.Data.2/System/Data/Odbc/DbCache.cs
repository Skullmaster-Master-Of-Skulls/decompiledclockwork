using System;

namespace System.Data.Odbc
{
	// Token: 0x02000289 RID: 649
	internal sealed class DbCache
	{
		// Token: 0x0600270F RID: 9999 RVA: 0x00108814 File Offset: 0x00107C14
		internal DbCache(OdbcDataReader record, int count)
		{
			this._count = count;
			this._record = record;
			this._randomaccess = !record.IsBehavior(CommandBehavior.SequentialAccess);
			this._values = new object[count];
			this._isBadValue = new bool[count];
		}

		// Token: 0x1700064E RID: 1614
		internal object this[int i]
		{
			get
			{
				if (this._isBadValue[i])
				{
					OverflowException ex = (OverflowException)this.Values[i];
					throw new OverflowException(ex.Message, ex);
				}
				return this.Values[i];
			}
			set
			{
				this.Values[i] = value;
				this._isBadValue[i] = false;
			}
		}

		// Token: 0x1700064F RID: 1615
		// (get) Token: 0x06002712 RID: 10002 RVA: 0x001088C4 File Offset: 0x00107CC4
		internal int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x001088D8 File Offset: 0x00107CD8
		internal void InvalidateValue(int i)
		{
			this._isBadValue[i] = true;
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06002714 RID: 10004 RVA: 0x001088F0 File Offset: 0x00107CF0
		internal object[] Values
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x00108904 File Offset: 0x00107D04
		internal object AccessIndex(int i)
		{
			object[] values = this.Values;
			if (this._randomaccess)
			{
				for (int j = 0; j < i; j++)
				{
					if (values[j] == null)
					{
						values[j] = this._record.GetValue(j);
					}
				}
			}
			return values[i];
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x00108944 File Offset: 0x00107D44
		internal DbSchemaInfo GetSchema(int i)
		{
			if (this._schema == null)
			{
				this._schema = new DbSchemaInfo[this.Count];
			}
			if (this._schema[i] == null)
			{
				this._schema[i] = new DbSchemaInfo();
			}
			return this._schema[i];
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x0010898C File Offset: 0x00107D8C
		internal void FlushValues()
		{
			int num = this._values.Length;
			for (int i = 0; i < num; i++)
			{
				this._values[i] = null;
			}
		}

		// Token: 0x040019FC RID: 6652
		private bool[] _isBadValue;

		// Token: 0x040019FD RID: 6653
		private DbSchemaInfo[] _schema;

		// Token: 0x040019FE RID: 6654
		private object[] _values;

		// Token: 0x040019FF RID: 6655
		private OdbcDataReader _record;

		// Token: 0x04001A00 RID: 6656
		internal int _count;

		// Token: 0x04001A01 RID: 6657
		internal bool _randomaccess = true;
	}
}

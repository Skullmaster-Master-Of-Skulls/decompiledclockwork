using System;

namespace System.Data.Odbc
{
	// Token: 0x020001BB RID: 443
	internal sealed class DbCache
	{
		// Token: 0x06001941 RID: 6465 RVA: 0x00258D18 File Offset: 0x00258118
		internal DbCache(OdbcDataReader record, int count)
		{
			this._count = count;
			this._record = record;
			this._randomaccess = !record.IsBehavior(CommandBehavior.SequentialAccess);
			this._values = new object[count];
			this._isBadValue = new bool[count];
		}

		// Token: 0x17000333 RID: 819
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

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06001944 RID: 6468 RVA: 0x00258DD8 File Offset: 0x002581D8
		internal int Count
		{
			get
			{
				return this._count;
			}
		}

		// Token: 0x06001945 RID: 6469 RVA: 0x00258DF8 File Offset: 0x002581F8
		internal void InvalidateValue(int i)
		{
			this._isBadValue[i] = true;
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06001946 RID: 6470 RVA: 0x00258E18 File Offset: 0x00258218
		internal object[] Values
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x00258E38 File Offset: 0x00258238
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

		// Token: 0x06001948 RID: 6472 RVA: 0x00258E78 File Offset: 0x00258278
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

		// Token: 0x06001949 RID: 6473 RVA: 0x00258EC8 File Offset: 0x002582C8
		internal void FlushValues()
		{
			int num = this._values.Length;
			for (int i = 0; i < num; i++)
			{
				this._values[i] = null;
			}
		}

		// Token: 0x04000E44 RID: 3652
		private bool[] _isBadValue;

		// Token: 0x04000E45 RID: 3653
		private DbSchemaInfo[] _schema;

		// Token: 0x04000E46 RID: 3654
		private object[] _values;

		// Token: 0x04000E47 RID: 3655
		private OdbcDataReader _record;

		// Token: 0x04000E48 RID: 3656
		internal int _count;

		// Token: 0x04000E49 RID: 3657
		internal bool _randomaccess = true;
	}
}

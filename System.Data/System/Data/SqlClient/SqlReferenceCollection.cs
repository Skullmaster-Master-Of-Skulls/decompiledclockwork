using System;
using System.Data.ProviderBase;

namespace System.Data.SqlClient
{
	// Token: 0x02000308 RID: 776
	internal sealed class SqlReferenceCollection : DbReferenceCollection
	{
		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x060028AA RID: 10410 RVA: 0x002B1AB8 File Offset: 0x002B0EB8
		internal bool MayHaveDataReader
		{
			get
			{
				return 0 != this._dataReaderCount;
			}
		}

		// Token: 0x060028AB RID: 10411 RVA: 0x002B1AD8 File Offset: 0x002B0ED8
		public override void Add(object value, int tag)
		{
			this._dataReaderCount++;
			base.AddItem(value, tag);
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x002B1B08 File Offset: 0x002B0F08
		internal void Deactivate()
		{
			if (this.MayHaveDataReader)
			{
				base.Notify(0);
				this._dataReaderCount = 0;
			}
			base.Purge();
		}

		// Token: 0x060028AD RID: 10413 RVA: 0x002B1B38 File Offset: 0x002B0F38
		internal SqlDataReader FindLiveReader(SqlCommand command)
		{
			if (this.MayHaveDataReader)
			{
				foreach (object obj in base.Filter(1))
				{
					SqlDataReader sqlDataReader = (SqlDataReader)obj;
					if (sqlDataReader != null && !sqlDataReader.IsClosed && (command == null || command == sqlDataReader.Command))
					{
						return sqlDataReader;
					}
				}
			}
			return null;
		}

		// Token: 0x060028AE RID: 10414 RVA: 0x002B1BC8 File Offset: 0x002B0FC8
		protected override bool NotifyItem(int message, int tag, object value)
		{
			SqlDataReader sqlDataReader = (SqlDataReader)value;
			if (!sqlDataReader.IsClosed)
			{
				sqlDataReader.CloseReaderFromConnection();
			}
			return false;
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x002B1BF8 File Offset: 0x002B0FF8
		public override void Remove(object value)
		{
			this._dataReaderCount--;
			base.RemoveItem(value);
		}

		// Token: 0x0400197D RID: 6525
		internal const int DataReaderTag = 1;

		// Token: 0x0400197E RID: 6526
		private int _dataReaderCount;
	}
}

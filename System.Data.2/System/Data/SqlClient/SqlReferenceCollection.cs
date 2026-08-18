using System;
using System.Data.ProviderBase;

namespace System.Data.SqlClient
{
	// Token: 0x020001F0 RID: 496
	internal sealed class SqlReferenceCollection : DbReferenceCollection
	{
		// Token: 0x06001F05 RID: 7941 RVA: 0x000D7C80 File Offset: 0x000D7080
		public override void Add(object value, int tag)
		{
			base.AddItem(value, tag);
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x000D7C98 File Offset: 0x000D7098
		internal void Deactivate()
		{
			base.Notify(0);
		}

		// Token: 0x06001F07 RID: 7943 RVA: 0x000D7CAC File Offset: 0x000D70AC
		internal SqlDataReader FindLiveReader(SqlCommand command)
		{
			if (command == null)
			{
				return base.FindItem<SqlDataReader>(1, (SqlDataReader dataReader) => !dataReader.IsClosed);
			}
			return base.FindItem<SqlDataReader>(1, (SqlDataReader dataReader) => !dataReader.IsClosed && command == dataReader.Command);
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x000D7D08 File Offset: 0x000D7108
		internal SqlCommand FindLiveCommand(TdsParserStateObject stateObj)
		{
			return base.FindItem<SqlCommand>(2, (SqlCommand command) => command.StateObject == stateObj);
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x000D7D38 File Offset: 0x000D7138
		protected override void NotifyItem(int message, int tag, object value)
		{
			if (tag == 1)
			{
				SqlDataReader sqlDataReader = (SqlDataReader)value;
				if (!sqlDataReader.IsClosed)
				{
					sqlDataReader.CloseReaderFromConnection();
					return;
				}
			}
			else
			{
				if (tag == 2)
				{
					((SqlCommand)value).OnConnectionClosed();
					return;
				}
				if (tag == 3)
				{
					((SqlBulkCopy)value).OnConnectionClosed();
				}
			}
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x000D7D80 File Offset: 0x000D7180
		public override void Remove(object value)
		{
			base.RemoveItem(value);
		}

		// Token: 0x0400119A RID: 4506
		internal const int DataReaderTag = 1;

		// Token: 0x0400119B RID: 4507
		internal const int CommandTag = 2;

		// Token: 0x0400119C RID: 4508
		internal const int BulkCopyTag = 3;
	}
}

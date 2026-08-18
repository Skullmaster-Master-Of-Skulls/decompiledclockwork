using System;
using System.Data.ProviderBase;

namespace System.Data.OleDb
{
	// Token: 0x02000239 RID: 569
	internal sealed class OleDbReferenceCollection : DbReferenceCollection
	{
		// Token: 0x06002041 RID: 8257 RVA: 0x0027F3B8 File Offset: 0x0027E7B8
		public override void Add(object value, int tag)
		{
			base.AddItem(value, tag);
		}

		// Token: 0x06002042 RID: 8258 RVA: 0x0027F3D8 File Offset: 0x0027E7D8
		protected override bool NotifyItem(int message, int tag, object value)
		{
			bool canceling = -1 == message;
			if (1 == tag)
			{
				((OleDbCommand)value).CloseCommandFromConnection(canceling);
			}
			else if (2 == tag)
			{
				((OleDbDataReader)value).CloseReaderFromConnection(canceling);
			}
			return false;
		}

		// Token: 0x06002043 RID: 8259 RVA: 0x0027F418 File Offset: 0x0027E818
		public override void Remove(object value)
		{
			base.RemoveItem(value);
		}

		// Token: 0x0400147E RID: 5246
		internal const int Closing = 0;

		// Token: 0x0400147F RID: 5247
		internal const int Canceling = -1;

		// Token: 0x04001480 RID: 5248
		internal const int CommandTag = 1;

		// Token: 0x04001481 RID: 5249
		internal const int DataReaderTag = 2;
	}
}

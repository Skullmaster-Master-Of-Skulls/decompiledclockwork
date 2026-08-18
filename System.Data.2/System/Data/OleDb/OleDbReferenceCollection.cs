using System;
using System.Data.ProviderBase;

namespace System.Data.OleDb
{
	// Token: 0x0200025F RID: 607
	internal sealed class OleDbReferenceCollection : DbReferenceCollection
	{
		// Token: 0x06002653 RID: 9811 RVA: 0x00103D70 File Offset: 0x00103170
		public override void Add(object value, int tag)
		{
			base.AddItem(value, tag);
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x00103D88 File Offset: 0x00103188
		protected override void NotifyItem(int message, int tag, object value)
		{
			bool canceling = -1 == message;
			if (1 == tag)
			{
				((OleDbCommand)value).CloseCommandFromConnection(canceling);
				return;
			}
			if (2 == tag)
			{
				((OleDbDataReader)value).CloseReaderFromConnection(canceling);
			}
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x00103DBC File Offset: 0x001031BC
		public override void Remove(object value)
		{
			base.RemoveItem(value);
		}

		// Token: 0x04001791 RID: 6033
		internal const int Closing = 0;

		// Token: 0x04001792 RID: 6034
		internal const int Canceling = -1;

		// Token: 0x04001793 RID: 6035
		internal const int CommandTag = 1;

		// Token: 0x04001794 RID: 6036
		internal const int DataReaderTag = 2;
	}
}

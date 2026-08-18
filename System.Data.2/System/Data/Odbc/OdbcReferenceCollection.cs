using System;
using System.Data.ProviderBase;

namespace System.Data.Odbc
{
	// Token: 0x020002AA RID: 682
	internal sealed class OdbcReferenceCollection : DbReferenceCollection
	{
		// Token: 0x060029A6 RID: 10662 RVA: 0x00114A94 File Offset: 0x00113E94
		public override void Add(object value, int tag)
		{
			base.AddItem(value, tag);
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x00114AAC File Offset: 0x00113EAC
		protected override void NotifyItem(int message, int tag, object value)
		{
			if (message != 0)
			{
				if (message == 1 && 1 == tag)
				{
					((OdbcCommand)value).RecoverFromConnection();
					return;
				}
			}
			else if (1 == tag)
			{
				((OdbcCommand)value).CloseFromConnection();
			}
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x00114AE0 File Offset: 0x00113EE0
		public override void Remove(object value)
		{
			base.RemoveItem(value);
		}

		// Token: 0x04001AE7 RID: 6887
		internal const int Closing = 0;

		// Token: 0x04001AE8 RID: 6888
		internal const int Recover = 1;

		// Token: 0x04001AE9 RID: 6889
		internal const int CommandTag = 1;
	}
}

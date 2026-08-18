using System;
using System.Data.ProviderBase;

namespace System.Data.Odbc
{
	// Token: 0x02000200 RID: 512
	internal sealed class OdbcReferenceCollection : DbReferenceCollection
	{
		// Token: 0x06001C69 RID: 7273 RVA: 0x00269068 File Offset: 0x00268468
		public override void Add(object value, int tag)
		{
			base.AddItem(value, tag);
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x00269088 File Offset: 0x00268488
		protected override bool NotifyItem(int message, int tag, object value)
		{
			switch (message)
			{
			case 0:
				if (1 == tag)
				{
					((OdbcCommand)value).CloseFromConnection();
				}
				break;
			case 1:
				if (1 == tag)
				{
					((OdbcCommand)value).RecoverFromConnection();
				}
				break;
			}
			return false;
		}

		// Token: 0x06001C6B RID: 7275 RVA: 0x002690D8 File Offset: 0x002684D8
		public override void Remove(object value)
		{
			base.RemoveItem(value);
		}

		// Token: 0x04001075 RID: 4213
		internal const int Closing = 0;

		// Token: 0x04001076 RID: 4214
		internal const int Recover = 1;

		// Token: 0x04001077 RID: 4215
		internal const int CommandTag = 1;
	}
}

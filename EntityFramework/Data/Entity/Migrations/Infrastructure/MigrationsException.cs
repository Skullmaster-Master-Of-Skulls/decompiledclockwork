using System;
using System.Runtime.Serialization;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x020002A9 RID: 681
	[Serializable]
	public class MigrationsException : Exception
	{
		// Token: 0x0600180D RID: 6157 RVA: 0x0007973D File Offset: 0x0007793D
		public MigrationsException()
		{
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x00079745 File Offset: 0x00077945
		public MigrationsException(string message) : base(message)
		{
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x0007974E File Offset: 0x0007794E
		public MigrationsException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x00079758 File Offset: 0x00077958
		protected MigrationsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

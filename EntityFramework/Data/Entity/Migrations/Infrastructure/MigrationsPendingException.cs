using System;
using System.Runtime.Serialization;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x020002AA RID: 682
	[Serializable]
	public sealed class MigrationsPendingException : MigrationsException
	{
		// Token: 0x06001811 RID: 6161 RVA: 0x00079762 File Offset: 0x00077962
		public MigrationsPendingException()
		{
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x0007976A File Offset: 0x0007796A
		public MigrationsPendingException(string message) : base(message)
		{
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x00079773 File Offset: 0x00077973
		public MigrationsPendingException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x0007977D File Offset: 0x0007797D
		private MigrationsPendingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

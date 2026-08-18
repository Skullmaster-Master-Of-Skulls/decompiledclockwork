using System;
using System.Data.Entity.Utilities;
using System.Runtime.Serialization;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x020006F8 RID: 1784
	[Serializable]
	public sealed class AutomaticDataLossException : MigrationsException
	{
		// Token: 0x0600476B RID: 18283 RVA: 0x001537BA File Offset: 0x001519BA
		public AutomaticDataLossException()
		{
		}

		// Token: 0x0600476C RID: 18284 RVA: 0x001537C2 File Offset: 0x001519C2
		public AutomaticDataLossException(string message) : base(message)
		{
			Check.NotEmpty(message, "message");
		}

		// Token: 0x0600476D RID: 18285 RVA: 0x001537D7 File Offset: 0x001519D7
		public AutomaticDataLossException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600476E RID: 18286 RVA: 0x001537E1 File Offset: 0x001519E1
		private AutomaticDataLossException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

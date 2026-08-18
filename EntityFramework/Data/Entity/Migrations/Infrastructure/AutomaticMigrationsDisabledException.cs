using System;
using System.Runtime.Serialization;

namespace System.Data.Entity.Migrations.Infrastructure
{
	// Token: 0x020006F9 RID: 1785
	[Serializable]
	public sealed class AutomaticMigrationsDisabledException : MigrationsException
	{
		// Token: 0x0600476F RID: 18287 RVA: 0x001537EB File Offset: 0x001519EB
		public AutomaticMigrationsDisabledException()
		{
		}

		// Token: 0x06004770 RID: 18288 RVA: 0x001537F3 File Offset: 0x001519F3
		public AutomaticMigrationsDisabledException(string message) : base(message)
		{
		}

		// Token: 0x06004771 RID: 18289 RVA: 0x001537FC File Offset: 0x001519FC
		public AutomaticMigrationsDisabledException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004772 RID: 18290 RVA: 0x00153806 File Offset: 0x00151A06
		private AutomaticMigrationsDisabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

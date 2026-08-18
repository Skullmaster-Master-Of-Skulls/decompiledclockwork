using System;
using System.Data.Entity.Core;
using System.Data.Entity.Internal;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000756 RID: 1878
	[SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = "SerializeObjectState used instead")]
	[Serializable]
	public class DbUpdateConcurrencyException : DbUpdateException
	{
		// Token: 0x0600551F RID: 21791 RVA: 0x0017293E File Offset: 0x00170B3E
		internal DbUpdateConcurrencyException(InternalContext context, OptimisticConcurrencyException innerException) : base(context, innerException, false)
		{
		}

		// Token: 0x06005520 RID: 21792 RVA: 0x00172949 File Offset: 0x00170B49
		public DbUpdateConcurrencyException()
		{
		}

		// Token: 0x06005521 RID: 21793 RVA: 0x00172951 File Offset: 0x00170B51
		public DbUpdateConcurrencyException(string message) : base(message)
		{
		}

		// Token: 0x06005522 RID: 21794 RVA: 0x0017295A File Offset: 0x00170B5A
		public DbUpdateConcurrencyException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

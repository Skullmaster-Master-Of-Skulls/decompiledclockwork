using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace System.Data.Entity.Validation
{
	// Token: 0x02000833 RID: 2099
	[Serializable]
	public class DbUnexpectedValidationException : DataException
	{
		// Token: 0x06005DEC RID: 24044 RVA: 0x00195C4F File Offset: 0x00193E4F
		public DbUnexpectedValidationException()
		{
		}

		// Token: 0x06005DED RID: 24045 RVA: 0x00195C57 File Offset: 0x00193E57
		public DbUnexpectedValidationException(string message) : base(message)
		{
		}

		// Token: 0x06005DEE RID: 24046 RVA: 0x00195C60 File Offset: 0x00193E60
		public DbUnexpectedValidationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06005DEF RID: 24047 RVA: 0x00195C6A File Offset: 0x00193E6A
		[ExcludeFromCodeCoverage]
		protected DbUnexpectedValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

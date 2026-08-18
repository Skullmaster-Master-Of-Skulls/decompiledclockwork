using System;
using System.Runtime.Serialization;

namespace System.Configuration
{
	// Token: 0x0200071A RID: 1818
	[Serializable]
	public class SettingsPropertyWrongTypeException : Exception
	{
		// Token: 0x060037BE RID: 14270 RVA: 0x000EC282 File Offset: 0x000EB282
		public SettingsPropertyWrongTypeException(string message) : base(message)
		{
		}

		// Token: 0x060037BF RID: 14271 RVA: 0x000EC28B File Offset: 0x000EB28B
		public SettingsPropertyWrongTypeException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060037C0 RID: 14272 RVA: 0x000EC295 File Offset: 0x000EB295
		protected SettingsPropertyWrongTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060037C1 RID: 14273 RVA: 0x000EC29F File Offset: 0x000EB29F
		public SettingsPropertyWrongTypeException()
		{
		}
	}
}

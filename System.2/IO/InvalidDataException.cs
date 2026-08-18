using System;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x020003F9 RID: 1017
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class InvalidDataException : SystemException
	{
		// Token: 0x0600265A RID: 9818 RVA: 0x000B0F0B File Offset: 0x000AF10B
		[__DynamicallyInvokable]
		public InvalidDataException() : base(SR.GetString("GenericInvalidData"))
		{
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x000B0F1D File Offset: 0x000AF11D
		[__DynamicallyInvokable]
		public InvalidDataException(string message) : base(message)
		{
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x000B0F26 File Offset: 0x000AF126
		[__DynamicallyInvokable]
		public InvalidDataException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x000B0F30 File Offset: 0x000AF130
		internal InvalidDataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

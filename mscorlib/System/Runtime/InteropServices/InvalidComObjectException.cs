using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000524 RID: 1316
	[ComVisible(true)]
	[Serializable]
	public class InvalidComObjectException : SystemException
	{
		// Token: 0x060032E7 RID: 13031 RVA: 0x000ABC37 File Offset: 0x000AAC37
		public InvalidComObjectException() : base(Environment.GetResourceString("Arg_InvalidComObjectException"))
		{
			base.SetErrorCode(-2146233049);
		}

		// Token: 0x060032E8 RID: 13032 RVA: 0x000ABC54 File Offset: 0x000AAC54
		public InvalidComObjectException(string message) : base(message)
		{
			base.SetErrorCode(-2146233049);
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x000ABC68 File Offset: 0x000AAC68
		public InvalidComObjectException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233049);
		}

		// Token: 0x060032EA RID: 13034 RVA: 0x000ABC7D File Offset: 0x000AAC7D
		protected InvalidComObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

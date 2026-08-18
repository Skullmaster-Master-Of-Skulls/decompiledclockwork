using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000517 RID: 1303
	[ComVisible(true)]
	[Serializable]
	public class MarshalDirectiveException : SystemException
	{
		// Token: 0x060032B0 RID: 12976 RVA: 0x000AB4B4 File Offset: 0x000AA4B4
		public MarshalDirectiveException() : base(Environment.GetResourceString("Arg_MarshalDirectiveException"))
		{
			base.SetErrorCode(-2146233035);
		}

		// Token: 0x060032B1 RID: 12977 RVA: 0x000AB4D1 File Offset: 0x000AA4D1
		public MarshalDirectiveException(string message) : base(message)
		{
			base.SetErrorCode(-2146233035);
		}

		// Token: 0x060032B2 RID: 12978 RVA: 0x000AB4E5 File Offset: 0x000AA4E5
		public MarshalDirectiveException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233035);
		}

		// Token: 0x060032B3 RID: 12979 RVA: 0x000AB4FA File Offset: 0x000AA4FA
		protected MarshalDirectiveException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

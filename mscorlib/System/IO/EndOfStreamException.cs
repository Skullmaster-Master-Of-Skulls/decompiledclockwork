using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.IO
{
	// Token: 0x020005B3 RID: 1459
	[ComVisible(true)]
	[Serializable]
	public class EndOfStreamException : IOException
	{
		// Token: 0x060035B1 RID: 13745 RVA: 0x000B2FDD File Offset: 0x000B1FDD
		public EndOfStreamException() : base(Environment.GetResourceString("Arg_EndOfStreamException"))
		{
			base.SetErrorCode(-2147024858);
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x000B2FFA File Offset: 0x000B1FFA
		public EndOfStreamException(string message) : base(message)
		{
			base.SetErrorCode(-2147024858);
		}

		// Token: 0x060035B3 RID: 13747 RVA: 0x000B300E File Offset: 0x000B200E
		public EndOfStreamException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2147024858);
		}

		// Token: 0x060035B4 RID: 13748 RVA: 0x000B3023 File Offset: 0x000B2023
		protected EndOfStreamException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

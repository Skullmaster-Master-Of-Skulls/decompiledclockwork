using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000512 RID: 1298
	[ComVisible(true)]
	[Serializable]
	public class InvalidOleVariantTypeException : SystemException
	{
		// Token: 0x060031E7 RID: 12775 RVA: 0x000AA2C3 File Offset: 0x000A92C3
		public InvalidOleVariantTypeException() : base(Environment.GetResourceString("Arg_InvalidOleVariantTypeException"))
		{
			base.SetErrorCode(-2146233039);
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x000AA2E0 File Offset: 0x000A92E0
		public InvalidOleVariantTypeException(string message) : base(message)
		{
			base.SetErrorCode(-2146233039);
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x000AA2F4 File Offset: 0x000A92F4
		public InvalidOleVariantTypeException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233039);
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x000AA309 File Offset: 0x000A9309
		protected InvalidOleVariantTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

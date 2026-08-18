using System;
using System.Runtime.Serialization;

namespace WebGrease.Preprocessing
{
	// Token: 0x020001B7 RID: 439
	[Serializable]
	public class PreprocessingException : Exception
	{
		// Token: 0x06001662 RID: 5730 RVA: 0x00081148 File Offset: 0x0007F348
		public PreprocessingException()
		{
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x00081150 File Offset: 0x0007F350
		public PreprocessingException(string message) : base(message)
		{
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x00081159 File Offset: 0x0007F359
		public PreprocessingException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x00081163 File Offset: 0x0007F363
		protected PreprocessingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security
{
	// Token: 0x02000697 RID: 1687
	[ComVisible(true)]
	[Serializable]
	public class VerificationException : SystemException
	{
		// Token: 0x06003D19 RID: 15641 RVA: 0x000D1197 File Offset: 0x000D0197
		public VerificationException() : base(Environment.GetResourceString("Verification_Exception"))
		{
			base.SetErrorCode(-2146233075);
		}

		// Token: 0x06003D1A RID: 15642 RVA: 0x000D11B4 File Offset: 0x000D01B4
		public VerificationException(string message) : base(message)
		{
			base.SetErrorCode(-2146233075);
		}

		// Token: 0x06003D1B RID: 15643 RVA: 0x000D11C8 File Offset: 0x000D01C8
		public VerificationException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetErrorCode(-2146233075);
		}

		// Token: 0x06003D1C RID: 15644 RVA: 0x000D11DD File Offset: 0x000D01DD
		protected VerificationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

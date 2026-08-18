using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Security.Cryptography
{
	// Token: 0x02000864 RID: 2148
	[ComVisible(true)]
	[Serializable]
	public class CryptographicUnexpectedOperationException : CryptographicException
	{
		// Token: 0x06004E71 RID: 20081 RVA: 0x0010FFB1 File Offset: 0x0010EFB1
		public CryptographicUnexpectedOperationException()
		{
			base.SetErrorCode(-2146233295);
		}

		// Token: 0x06004E72 RID: 20082 RVA: 0x0010FFC4 File Offset: 0x0010EFC4
		public CryptographicUnexpectedOperationException(string message) : base(message)
		{
			base.SetErrorCode(-2146233295);
		}

		// Token: 0x06004E73 RID: 20083 RVA: 0x0010FFD8 File Offset: 0x0010EFD8
		public CryptographicUnexpectedOperationException(string format, string insert) : base(string.Format(CultureInfo.CurrentCulture, format, new object[]
		{
			insert
		}))
		{
			base.SetErrorCode(-2146233295);
		}

		// Token: 0x06004E74 RID: 20084 RVA: 0x0011000D File Offset: 0x0010F00D
		public CryptographicUnexpectedOperationException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233295);
		}

		// Token: 0x06004E75 RID: 20085 RVA: 0x00110022 File Offset: 0x0010F022
		protected CryptographicUnexpectedOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

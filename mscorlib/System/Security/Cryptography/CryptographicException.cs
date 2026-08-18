using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Microsoft.Win32;

namespace System.Security.Cryptography
{
	// Token: 0x02000863 RID: 2147
	[ComVisible(true)]
	[Serializable]
	public class CryptographicException : SystemException
	{
		// Token: 0x06004E6A RID: 20074 RVA: 0x0010FEEC File Offset: 0x0010EEEC
		public CryptographicException() : base(Environment.GetResourceString("Arg_CryptographyException"))
		{
			base.SetErrorCode(-2146233296);
		}

		// Token: 0x06004E6B RID: 20075 RVA: 0x0010FF09 File Offset: 0x0010EF09
		public CryptographicException(string message) : base(message)
		{
			base.SetErrorCode(-2146233296);
		}

		// Token: 0x06004E6C RID: 20076 RVA: 0x0010FF20 File Offset: 0x0010EF20
		public CryptographicException(string format, string insert) : base(string.Format(CultureInfo.CurrentCulture, format, new object[]
		{
			insert
		}))
		{
			base.SetErrorCode(-2146233296);
		}

		// Token: 0x06004E6D RID: 20077 RVA: 0x0010FF55 File Offset: 0x0010EF55
		public CryptographicException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233296);
		}

		// Token: 0x06004E6E RID: 20078 RVA: 0x0010FF6A File Offset: 0x0010EF6A
		public CryptographicException(int hr) : this(Win32Native.GetMessage(hr))
		{
			if (((long)hr & (long)((ulong)-2147483648)) != (long)((ulong)-2147483648))
			{
				hr = ((hr & 65535) | -2147024896);
			}
			base.SetErrorCode(hr);
		}

		// Token: 0x06004E6F RID: 20079 RVA: 0x0010FF9F File Offset: 0x0010EF9F
		protected CryptographicException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06004E70 RID: 20080 RVA: 0x0010FFA9 File Offset: 0x0010EFA9
		private static void ThrowCryptogaphicException(int hr)
		{
			throw new CryptographicException(hr);
		}

		// Token: 0x04002893 RID: 10387
		private const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;

		// Token: 0x04002894 RID: 10388
		private const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;

		// Token: 0x04002895 RID: 10389
		private const int FORMAT_MESSAGE_ARGUMENT_ARRAY = 8192;
	}
}

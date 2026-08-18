using System;
using System.Runtime.Serialization;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x020000C7 RID: 199
	[Serializable]
	internal class CryptoSignedXmlRecursionException : XmlException
	{
		// Token: 0x060004D7 RID: 1239 RVA: 0x00018408 File Offset: 0x00017408
		public CryptoSignedXmlRecursionException()
		{
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00018410 File Offset: 0x00017410
		public CryptoSignedXmlRecursionException(string message) : base(message)
		{
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00018419 File Offset: 0x00017419
		public CryptoSignedXmlRecursionException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00018423 File Offset: 0x00017423
		protected CryptoSignedXmlRecursionException(SerializationInfo info, StreamingContext context)
		{
		}
	}
}

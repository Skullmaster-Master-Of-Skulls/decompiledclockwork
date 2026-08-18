using System;
using System.Runtime.Serialization;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000040 RID: 64
	[Serializable]
	internal class CryptoSignedXmlRecursionException : XmlException
	{
		// Token: 0x060001ED RID: 493 RVA: 0x00008616 File Offset: 0x00006816
		public CryptoSignedXmlRecursionException()
		{
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000861E File Offset: 0x0000681E
		public CryptoSignedXmlRecursionException(string message) : base(message)
		{
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00008627 File Offset: 0x00006827
		public CryptoSignedXmlRecursionException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00008616 File Offset: 0x00006816
		protected CryptoSignedXmlRecursionException(SerializationInfo info, StreamingContext context)
		{
		}
	}
}

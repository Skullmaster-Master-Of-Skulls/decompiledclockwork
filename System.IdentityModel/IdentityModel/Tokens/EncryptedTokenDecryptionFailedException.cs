using System;
using System.Runtime.Serialization;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200011E RID: 286
	[Serializable]
	public class EncryptedTokenDecryptionFailedException : SecurityTokenException
	{
		// Token: 0x060007DA RID: 2010 RVA: 0x000211A9 File Offset: 0x0001F3A9
		public EncryptedTokenDecryptionFailedException() : base(SR.GetString("ID4022"))
		{
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x000101E4 File Offset: 0x0000E3E4
		public EncryptedTokenDecryptionFailedException(string message) : base(message)
		{
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x000101ED File Offset: 0x0000E3ED
		public EncryptedTokenDecryptionFailedException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x000101F7 File Offset: 0x0000E3F7
		protected EncryptedTokenDecryptionFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

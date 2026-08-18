using System;
using System.IdentityModel.Tokens;
using System.Runtime.Serialization;

namespace System.IdentityModel
{
	// Token: 0x0200007C RID: 124
	[Serializable]
	public class SignatureVerificationFailedException : SecurityTokenException
	{
		// Token: 0x0600043F RID: 1087 RVA: 0x000101D2 File Offset: 0x0000E3D2
		public SignatureVerificationFailedException() : base(SR.GetString("ID4038"))
		{
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x000101E4 File Offset: 0x0000E3E4
		public SignatureVerificationFailedException(string message) : base(message)
		{
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x000101ED File Offset: 0x0000E3ED
		public SignatureVerificationFailedException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x000101F7 File Offset: 0x0000E3F7
		protected SignatureVerificationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

using System;
using System.Runtime.Serialization;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x02000214 RID: 532
	[Serializable]
	public class WSTrustSerializationException : Exception, ISerializable
	{
		// Token: 0x06001195 RID: 4501 RVA: 0x00048B1E File Offset: 0x00046D1E
		public WSTrustSerializationException() : this(SR.GetString("ID3063"))
		{
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0000544D File Offset: 0x0000364D
		public WSTrustSerializationException(string message) : base(message)
		{
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x00005456 File Offset: 0x00003656
		public WSTrustSerializationException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x00005473 File Offset: 0x00003673
		protected WSTrustSerializationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

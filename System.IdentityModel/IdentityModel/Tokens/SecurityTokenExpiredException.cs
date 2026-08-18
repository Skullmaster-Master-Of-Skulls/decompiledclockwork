using System;
using System.Runtime.Serialization;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000174 RID: 372
	[Serializable]
	public class SecurityTokenExpiredException : SecurityTokenValidationException
	{
		// Token: 0x06000BB4 RID: 2996 RVA: 0x000370EF File Offset: 0x000352EF
		public SecurityTokenExpiredException() : base(SR.GetString("ID4181"))
		{
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0001F63A File Offset: 0x0001D83A
		public SecurityTokenExpiredException(string message) : base(message)
		{
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0001F643 File Offset: 0x0001D843
		public SecurityTokenExpiredException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0001F64D File Offset: 0x0001D84D
		protected SecurityTokenExpiredException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

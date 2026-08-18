using System;
using System.Runtime.Serialization;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200010D RID: 269
	[Serializable]
	public class AudienceUriValidationFailedException : SecurityTokenValidationException
	{
		// Token: 0x06000766 RID: 1894 RVA: 0x0001F628 File Offset: 0x0001D828
		public AudienceUriValidationFailedException() : base(SR.GetString("ID4183"))
		{
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0001F63A File Offset: 0x0001D83A
		public AudienceUriValidationFailedException(string message) : base(message)
		{
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001F643 File Offset: 0x0001D843
		public AudienceUriValidationFailedException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0001F64D File Offset: 0x0001D84D
		protected AudienceUriValidationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

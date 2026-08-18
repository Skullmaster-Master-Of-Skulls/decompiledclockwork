using System;
using System.Runtime.Serialization;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000176 RID: 374
	[Serializable]
	public class SecurityTokenReplayDetectedException : SecurityTokenValidationException
	{
		// Token: 0x06000BBC RID: 3004 RVA: 0x00037113 File Offset: 0x00035313
		public SecurityTokenReplayDetectedException() : base(SR.GetString("ID1070"))
		{
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0001F63A File Offset: 0x0001D83A
		public SecurityTokenReplayDetectedException(string message) : base(message)
		{
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0001F643 File Offset: 0x0001D843
		public SecurityTokenReplayDetectedException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0001F64D File Offset: 0x0001D84D
		protected SecurityTokenReplayDetectedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

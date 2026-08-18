using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x0200000B RID: 11
	[TypeForwardedFrom("System.ServiceModel.Web, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	[Serializable]
	public class UriTemplateMatchException : SystemException
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00003E04 File Offset: 0x00002004
		public UriTemplateMatchException()
		{
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003E0C File Offset: 0x0000200C
		public UriTemplateMatchException(string message) : base(message)
		{
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00003E15 File Offset: 0x00002015
		public UriTemplateMatchException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003E1F File Offset: 0x0000201F
		protected UriTemplateMatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

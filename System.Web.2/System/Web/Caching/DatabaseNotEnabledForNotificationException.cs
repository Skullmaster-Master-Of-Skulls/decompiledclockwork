using System;
using System.Runtime.Serialization;

namespace System.Web.Caching
{
	// Token: 0x0200088F RID: 2191
	[Serializable]
	public sealed class DatabaseNotEnabledForNotificationException : SystemException
	{
		// Token: 0x060066F9 RID: 26361 RVA: 0x0003E2E8 File Offset: 0x0003C4E8
		public DatabaseNotEnabledForNotificationException()
		{
		}

		// Token: 0x060066FA RID: 26362 RVA: 0x0003E2D5 File Offset: 0x0003C4D5
		public DatabaseNotEnabledForNotificationException(string message) : base(message)
		{
		}

		// Token: 0x060066FB RID: 26363 RVA: 0x0003E2DE File Offset: 0x0003C4DE
		public DatabaseNotEnabledForNotificationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060066FC RID: 26364 RVA: 0x0016AF78 File Offset: 0x00169178
		internal DatabaseNotEnabledForNotificationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

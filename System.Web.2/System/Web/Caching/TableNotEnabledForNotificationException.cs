using System;
using System.Runtime.Serialization;

namespace System.Web.Caching
{
	// Token: 0x02000890 RID: 2192
	[Serializable]
	public sealed class TableNotEnabledForNotificationException : SystemException
	{
		// Token: 0x060066FD RID: 26365 RVA: 0x0003E2E8 File Offset: 0x0003C4E8
		public TableNotEnabledForNotificationException()
		{
		}

		// Token: 0x060066FE RID: 26366 RVA: 0x0003E2D5 File Offset: 0x0003C4D5
		public TableNotEnabledForNotificationException(string message) : base(message)
		{
		}

		// Token: 0x060066FF RID: 26367 RVA: 0x0003E2DE File Offset: 0x0003C4DE
		public TableNotEnabledForNotificationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06006700 RID: 26368 RVA: 0x0016AF78 File Offset: 0x00169178
		internal TableNotEnabledForNotificationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

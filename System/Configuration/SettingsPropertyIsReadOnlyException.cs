using System;
using System.Runtime.Serialization;

namespace System.Configuration
{
	// Token: 0x02000716 RID: 1814
	[Serializable]
	public class SettingsPropertyIsReadOnlyException : Exception
	{
		// Token: 0x06003798 RID: 14232 RVA: 0x000EB8E7 File Offset: 0x000EA8E7
		public SettingsPropertyIsReadOnlyException(string message) : base(message)
		{
		}

		// Token: 0x06003799 RID: 14233 RVA: 0x000EB8F0 File Offset: 0x000EA8F0
		public SettingsPropertyIsReadOnlyException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600379A RID: 14234 RVA: 0x000EB8FA File Offset: 0x000EA8FA
		protected SettingsPropertyIsReadOnlyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600379B RID: 14235 RVA: 0x000EB904 File Offset: 0x000EA904
		public SettingsPropertyIsReadOnlyException()
		{
		}
	}
}

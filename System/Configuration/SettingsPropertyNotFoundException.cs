using System;
using System.Runtime.Serialization;

namespace System.Configuration
{
	// Token: 0x02000717 RID: 1815
	[Serializable]
	public class SettingsPropertyNotFoundException : Exception
	{
		// Token: 0x0600379C RID: 14236 RVA: 0x000EB90C File Offset: 0x000EA90C
		public SettingsPropertyNotFoundException(string message) : base(message)
		{
		}

		// Token: 0x0600379D RID: 14237 RVA: 0x000EB915 File Offset: 0x000EA915
		public SettingsPropertyNotFoundException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600379E RID: 14238 RVA: 0x000EB91F File Offset: 0x000EA91F
		protected SettingsPropertyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600379F RID: 14239 RVA: 0x000EB929 File Offset: 0x000EA929
		public SettingsPropertyNotFoundException()
		{
		}
	}
}

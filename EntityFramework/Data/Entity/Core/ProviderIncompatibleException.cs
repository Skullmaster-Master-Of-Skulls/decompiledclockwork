using System;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core
{
	// Token: 0x020005C5 RID: 1477
	[Serializable]
	public sealed class ProviderIncompatibleException : EntityException
	{
		// Token: 0x06003B15 RID: 15125 RVA: 0x00117E8E File Offset: 0x0011608E
		public ProviderIncompatibleException()
		{
		}

		// Token: 0x06003B16 RID: 15126 RVA: 0x00117E96 File Offset: 0x00116096
		public ProviderIncompatibleException(string message) : base(message)
		{
		}

		// Token: 0x06003B17 RID: 15127 RVA: 0x00117E9F File Offset: 0x0011609F
		public ProviderIncompatibleException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06003B18 RID: 15128 RVA: 0x00117EA9 File Offset: 0x001160A9
		private ProviderIncompatibleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

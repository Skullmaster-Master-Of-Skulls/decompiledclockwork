using System;
using System.Data.Entity.Resources;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core
{
	// Token: 0x02000290 RID: 656
	[Serializable]
	public class EntityException : DataException
	{
		// Token: 0x06001700 RID: 5888 RVA: 0x00072C36 File Offset: 0x00070E36
		public EntityException() : base(Strings.EntityClient_ProviderGeneralError)
		{
		}

		// Token: 0x06001701 RID: 5889 RVA: 0x00072C43 File Offset: 0x00070E43
		public EntityException(string message) : base(message)
		{
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x00072C4C File Offset: 0x00070E4C
		public EntityException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x00072C56 File Offset: 0x00070E56
		protected EntityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

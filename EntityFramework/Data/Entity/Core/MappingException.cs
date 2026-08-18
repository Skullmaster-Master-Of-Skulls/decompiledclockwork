using System;
using System.Data.Entity.Resources;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core
{
	// Token: 0x020003A5 RID: 933
	[Serializable]
	public sealed class MappingException : EntityException
	{
		// Token: 0x060021B4 RID: 8628 RVA: 0x0009DE29 File Offset: 0x0009C029
		public MappingException() : base(Strings.Mapping_General_Error)
		{
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x0009DE36 File Offset: 0x0009C036
		public MappingException(string message) : base(message)
		{
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x0009DE3F File Offset: 0x0009C03F
		public MappingException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060021B7 RID: 8631 RVA: 0x0009DE49 File Offset: 0x0009C049
		private MappingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

using System;
using System.Data.Entity;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x02000011 RID: 17
	[Serializable]
	public sealed class MappingException : EntityException
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002FAE File Offset: 0x000011AE
		public MappingException() : base(Strings.Mapping_General_Error)
		{
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002F91 File Offset: 0x00001191
		public MappingException(string message) : base(message)
		{
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002F9A File Offset: 0x0000119A
		public MappingException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002FA4 File Offset: 0x000011A4
		private MappingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

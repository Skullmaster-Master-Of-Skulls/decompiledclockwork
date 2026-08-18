using System;
using System.Data.Entity;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x02000013 RID: 19
	[Serializable]
	public sealed class MetadataException : EntityException
	{
		// Token: 0x0600005B RID: 91 RVA: 0x0000300B File Offset: 0x0000120B
		public MetadataException() : base(Strings.Metadata_General_Error)
		{
			base.HResult = -2146232007;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003023 File Offset: 0x00001223
		public MetadataException(string message) : base(message)
		{
			base.HResult = -2146232007;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003037 File Offset: 0x00001237
		public MetadataException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232007;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002FA4 File Offset: 0x000011A4
		private MetadataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

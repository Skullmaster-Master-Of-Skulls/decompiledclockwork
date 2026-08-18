using System;
using System.Data.Entity.Resources;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core
{
	// Token: 0x020004AD RID: 1197
	[Serializable]
	public sealed class MetadataException : EntityException
	{
		// Token: 0x06002C24 RID: 11300 RVA: 0x000D6BC7 File Offset: 0x000D4DC7
		public MetadataException() : base(Strings.Metadata_General_Error)
		{
			base.HResult = -2146232007;
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x000D6BDF File Offset: 0x000D4DDF
		public MetadataException(string message) : base(message)
		{
			base.HResult = -2146232007;
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x000D6BF3 File Offset: 0x000D4DF3
		public MetadataException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232007;
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x000D6C08 File Offset: 0x000D4E08
		private MetadataException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0400104B RID: 4171
		private const int HResultMetadata = -2146232007;
	}
}
